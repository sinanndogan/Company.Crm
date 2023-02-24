<script setup>
import { ref, onMounted } from 'vue'
import Pagination from '@/components/Pagination.vue'
import Loading from '@/components/Loading.vue'
import { moment } from '@/plugins/datetime'
import pdfMake from "pdfmake/build/pdfmake"
import * as pdfFonts from "pdfmake/build/vfs_fonts.js";
import { Logo } from './report-logos.js'
import CustomerModal from './CustomerModal.vue'
import { storeToRefs } from 'pinia'
import { useLayoutStore } from '@/stores'
import { IconPlus, IconRefresh, IconPencil, IconTrash, IconExcel, IconPdf } from '@/components/icons'

let appModal
const dataItem = ref()
const dataList = ref()
const dataMeta = ref()
const store = useLayoutStore()
const { isTableLoading } = storeToRefs(store);

onMounted(() => {
	fetchItems()
})

function fetchItems(page = 1) {
	store.showTableLoading()
	axios.get('customer/getpaged', { params: { page, perPage: 10 } }).then(response => {
		dataList.value = response.data.data
		dataMeta.value = response.data.meta
		store.hideTableLoading()
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
	axios.get('customer/' + id).then(response => {
		dataItem.value = response.data.data

		// input date elemanı sadece tarih verilerini gösterebildiği için tarih kısmını aldık 10 karakter olarak yyyy-mm-dd
		//dataItem.value.birthDate = dataItem.value.birthDate.substring(0, 10)
		dataItem.value.birthDate = new Date(dataItem.value.birthDate)

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
			axios.delete("customer/" + id).then(res => {
				fetchItems()
			})
		}
	})
}

function itemSaved() {
	appModal.hide()
	fetchItems()
}

function exportExcel() {
	axios.post('customer/export-excel', null, {
		headers: {
			'Content-Disposition': "attachment; filename=CustomerReport.xlsx",
			'Content-Type': 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
		},
		responseType: 'arraybuffer',
	}).then(response => {
		const url = window.URL.createObjectURL(new Blob([response.data]));
		const link = document.createElement('a');
		link.href = url;
		link.setAttribute('download', 'CustomerReport.xlsx');
		document.body.appendChild(link);
		link.click();
		toastr.success("Export Excel", "Success!")
	})
}

function exportPdf() {
	if (pdfMake.vfs == undefined) {
		pdfMake.vfs = pdfFonts.pdfMake.vfs;
	}

	axios.get('customer').then(response => {

		let data = response.data.data;

		let tableBody = [
			['ID', 'User', 'Company Name']
		]

		data.forEach(item => {
			tableBody.push([item.id, item.userFullName, item.companyName])
		});

		let content = [
			{
				layout: 'noBorders',
				style: 'headerTable',
				table: {
					widths: ['*', 50, '*'],
					body: [
						['Company.CRM', { image: Logo, width: 50, fit: [50, 50] }, { text: new Date().toLocaleString(), alignment: 'right' }]
					]
				}
			},
			{
				text: 'Customers Report',
				style: 'header'
			},
			{
				style: 'tableExample',
				table: {
					body: tableBody
				}
			},
		]

		let docDefinition = {
			content: content,
			styles: {
				header: {
					fontSize: 18,
					bold: true
				},
				headerTable: {
					margin: [0, 0, 0, 0]
				},
				tableExample: {
					margin: [0, 0, 0, 0]
				},
			}
		}
		//pdfMake.createPdf(docDefinition).download('optionalName.pdf')
		pdfMake.createPdf(docDefinition).open()
	})
}
</script>

<template>
	<div class="page-body">
		<div class="container-xl">
			<div class="card">
				<div class="card-header">
					<h3 class="card-title">Customers</h3>
					<div class="btn-group ms-auto">
						<button class="btn btn-primary" @click="createItem">
							<IconPlus /> New
						</button>
						<button class="btn" @click.prevent="fetchItems(1)">
							<span class="spinner-border spinner-border-sm me-2" role="status" v-if="isTableLoading"></span>
							<IconRefresh v-if="!isTableLoading" />
							Refresh
						</button>
					</div>
					<div class="btn-group ms-2">
						<button class="btn" @click.prevent="exportExcel()">
							<IconExcel /> Export Excel
						</button>
						<button class="btn" @click.prevent="exportPdf()">
							<IconPdf /> Export PDF
						</button>
					</div>
				</div>
				<div class="table-responsive">
					<loading :visible="isTableLoading">Table Loading</loading>
					<table class="table table-vcenter card-table" v-if="dataList && dataList.length">
						<thead>
							<tr>
								<th>#</th>
								<th>Name</th>
								<th>Title</th>
								<th>Gender</th>
								<th>Company</th>
								<th>Birth Date</th>
								<th>Birth Date</th>
								<th>Action</th>
							</tr>
						</thead>
						<tbody>
							<tr v-for="{ id, userFullName, titleName, companyName, genderName, birthDate } in dataList" :key="id">
								<td>{{ id }}</td>
								<td>{{ userFullName }}</td>
								<td>{{ titleName }}</td>
								<td>{{ genderName }}</td>
								<td>{{ companyName }}</td>
								<td>{{ moment(birthDate).format("DD.MM.YYYY") }}</td>
								<td>{{ moment(birthDate).fromNow() }}</td>
								<td>
									<div class="btn-list flex-nowrap">
										<button class="btn btn-success" @click="editItem(id)">
											<IconPencil /> Edit
										</button>

										<button class="btn btn-danger" @click="deleteItem(id)">
											<IconTrash /> Delete
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

			<customer-modal :item="dataItem" @onSaved="itemSaved"></customer-modal>
		</div>
	</div>
</template>

