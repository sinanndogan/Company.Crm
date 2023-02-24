<script setup>
import { ref, onMounted } from 'vue'
import Pagination from '../../components/Pagination.vue'
import TaskModal from './TaskModal.vue'
import { moment } from '@/plugins/datetime'

let appModal
const dataItem = ref()
const dataList = ref()
const dataMeta = ref()

onMounted(() => {
	fetchItems()
})

function fetchItems(page = 1) {
	axios.get('task/getpaged', { params: { page, perPage: 10 } }).then(response => {
		dataList.value = response.data.data
		dataMeta.value = response.data.meta
	})
}

function showModal() {
	appModal = new bootstrap.Modal('#appModal', {
		keyboard: false
	})
	appModal.show();
}

function createItem() {
	dataItem.value = {}
	showModal();
}

function editItem(id) {
	axios.get('task/' + id).then(response => {
		dataItem.value = response.data.data

		// input date elemanı sadece tarih verilerini gösterebildiği için tarih kısmını aldık 10 karakter olarak yyyy-mm-dd
		//dataItem.value.birthDate = dataItem.value.birthDate.substring(0, 10)
		dataItem.value.taskStartDate = new Date(dataItem.value.taskStartDate)
		dataItem.value.taskEndDate = new Date(dataItem.value.taskEndDate)

		showModal()
	})
}

function deleteItem(id) {
	swal.fire({
		icon: "question",
		title: "Are you sure?",
		text: "Are you sure you want to delete this item?",
		showCancelButton: true,
		confirmButtonText: "Delete",
		confirmButtonColor: '#ff0000'
	}).then(result => {
		if (result.value) {
			axios.delete("task/" + id).then(res => {
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
					<h3 class="card-title">Tasks</h3>
					<div class="btn-group ms-auto">
						<button class="btn btn-primary" @click="createItem">New</button>
						<button class="btn btn-primary" @click.prevent="fetchItems(1)">Refresh</button>
					</div>
				</div>
				<div class="table-responsive">
					<table class="table table-vcenter card-table" v-if="dataList && dataList.length">
						<thead>
							<tr>
								<th>#</th>
								<th>Request</th>
								<th>EmployeeUser</th>
								<th>Task Start Date</th>
								<th>Task End Date</th>
								<th>Description</th>
								<th>Task Status</th>
								<th>Action</th>
							</tr>
						</thead>
						<tbody>
							<tr v-for="{ id, requestId, employeeUserId, taskStartDate, taskEndDate, description, taskStatusName } in dataList" :key="id">
								<td>{{ id }}</td>
								<td>{{ requestId }}</td>
								<td>{{ employeeUserId }}</td>
								<td>{{ moment(taskStartDate).format("DD.MM.YYYY") }}</td>
								<td>{{ moment(taskEndDate).format("DD.MM.YYYY") }}</td>
								<td>{{ description }}</td>
								<td>{{ taskStatusName }}</td>
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

			<task-modal :item="dataItem" @onSaved="itemSaved"></task-modal>
		</div>
	</div>
</template>