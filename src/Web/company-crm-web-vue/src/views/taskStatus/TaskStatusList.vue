<script setup>
import { ref, onMounted } from 'vue'
import Pagination from '../../components/Pagination.vue'
import TaskStatusModal from './TaskStatusModal.vue'
import { moment } from '@/plugins/datetime'

let appModal
const dataItem = ref()
const dataList = ref()
const dataMeta = ref()

onMounted(() => {
	fetchItems()
})

function fetchItems(page = 1) {
	axios
		.get('taskStatus/getpaged', { params: { page, perPage: 10 } })
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
	axios.get('taskStatus/' + id).then((response) => {
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
			if (result.value) {
				axios.delete('taskStatus/' + id).then((res) => {
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
					<h3 class="card-title">Task Status</h3>
					<div class="btn-group ms-auto">
						<button class="btn btn-primary" @click="createItem">New</button>
						<button class="btn btn-primary" @click.prevent="fetchItems(1)">
							Refresh
						</button>
					</div>
				</div>
				<div class="table-responsive" v-if="dataList && dataList.length">
					<table class="table table-vcenter card-table">
						<thead>
							<tr>
								<th>#</th>
								<th>Name</th>
								<th>Actions</th>
							</tr>
						</thead>
						<tbody>
							<tr v-for="{ id, name } in dataList" :key="id">
								<td>{{ id }}</td>
								<td>{{ name }}</td>
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
				</div>

				<div v-else>No records!</div>

				<pagination class="mt-3" v-if="dataMeta" :meta="dataMeta" v-on:pageChange="fetchItems" />
			</div>

			<TaskStatusModal :item="dataItem" @onSaved="itemSaved"></TaskStatusModal>
		</div>
	</div>
</template>
