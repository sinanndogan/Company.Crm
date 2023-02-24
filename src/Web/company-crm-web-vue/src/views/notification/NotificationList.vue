<script setup>
import { ref, onMounted } from 'vue'
import Pagination from '../../components/Pagination.vue'
import { moment } from '@/plugins/datetime'
import NotificationModal from './NotificationModal.vue'
import Checkbox from 'primevue/checkbox'
let appModal
const dataItem = ref()
const dataList = ref()
const dataMeta = ref()

onMounted(() => {
	fetchItems()
})

function fetchItems(page = 1) {
	axios
		.get('notification/getpaged', { params: { page, perPage: 10 } })
		.then((response) => {
			dataList.value = response.data.data
			dataMeta.value = response.data.meta
		})
}

function showModal() {
	appModal = new bootstrap.Modal('#appModal', {
		keyboard: false
	})
	appModal.show()
}

function createItem() {
	dataItem.value = {}
	showModal()
}

function editItem(id) {
	axios.get(`notification/${id}`).then((response) => {
		dataItem.value = response.data.data
		showModal()
	})
}

function deleteItem(id) {
	swal
		.fire({
			icon: 'question',
			title: 'Are you sure?',
			text: 'Are you sure you want to delete this item?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
			confirmButtonColor: '#ff0000'
		})
		.then((result) => {
			if (result.isConfirmed) {
				axios.delete(`notification/${id}`).then((res) => {
					if (res.data.data) {
						swal.fire({
							icon: 'success',
							title: 'Successfully deleted',
							timer: 1350
						})
					}
					fetchItems()
				})
			}
		})
}

function toggleReadSituation(id) {
	console.log(id)

	axios.patch(`notification/ToggleReadSituation/${id}`).then((response) => {
		if (response.data) {
			swal
				.fire({
					icon: 'success',
					title: 'Successfull',
					timer: 650,
					timerProgressBar: true,
				})
				.then(() => {
					fetchItems()
				})
		}
	})
}

function itemSaved() {
	appModal.hide()
	fetchItems()
}
</script>
<template>
	<div class="page-body">
		<div class="container-xl">
			<div class="card">
				<div class="card-header">
					<h3 class="card-title">Notifications</h3>
					<div class="btn-group ms-auto">
						<button class="btn btn-primary" @click="createItem">New</button>
						<button class="btn btn-primary" @click.prevent="fetchItems(1)">
							Refresh
						</button>
					</div>
				</div>
				<div class="table-responsive">
					<table class="table table-vcenter card-table" v-if="dataList && dataList.length">
						<thead>
							<tr>
								<th>#</th>
								<th>User Id</th>
								<th>Title</th>
								<th>Description</th>
								<th>Created At</th>
								<th>Is Read</th>
								<th>Action</th>
							</tr>
						</thead>
						<tbody>
							<tr v-for="{
								id,
								userId,
								title,
								description,
								createdAt,
								isRead
							} in dataList" :key="id">
								<td>{{ id }}</td>
								<td>{{ userId }}</td>
								<td>{{ title }}</td>
								<td>{{ description }}</td>
								<td>{{ moment(createdAt).format('DD.MM.YYYY') }}</td>
								<td>
									<input type="checkbox" :checked="isRead" v-on:click="toggleReadSituation(id)" />
								</td>
								<!-- <td><Checkbox :trueValue="isRead" :binary="true" /></td> 
                https://www.primefaces.org/primevue/checkbox
                https://github.com/primefaces/primevue/issues/1320
                -->
								<td>
									<div class="btn-list flex-nowrap">
										<button class="btn btn-success" @click="editItem(id)">
											Edit
										</button>

										<button class="btn btn-danger" @click="deleteItem(id)">
											Delete
										</button>
									</div>
								</td>
							</tr>
						</tbody>
					</table>
					<div class="card-body" v-else>
						No records!
					</div>
				</div>

				<pagination class="mt-3" v-if="dataMeta && dataList && dataList.length" :meta="dataMeta" v-on:pageChange="fetchItems" />
			</div>
			<notification-modal :item="dataItem" @onSaved="itemSaved"></notification-modal>
		</div>
	</div>
</template>
