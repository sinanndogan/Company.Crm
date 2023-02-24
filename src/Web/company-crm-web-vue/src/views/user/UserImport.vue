<script setup>
import { ref } from 'vue'
const excelFile = ref();
function importExcel() {
	debugger;
	//console.log("selected file", excelFile.value.files)

	let formData = new FormData();
	formData.append("excelFile", excelFile.value.files[0]);

	axios.post('user/import-excel', formData, {
		headers: {
			"Content-Type": "multipart/form-data",
		}
	}).then((res) => {
		if (res.data) {
			swal.fire({
				type: "success",
				title: "Import Excel",
				text: "Import Excel successfully",
			})
		}
	})
}
</script>
<template>
	<div class="page-body">
		<div class="container-xl">
			<div class="card">
				<div class="card-header">
					<h3 class="card-title">Import Users</h3>
				</div>
				<div class="card-body">
					<div class="alert alert-info">
						Download <a href="/Users.xlsx">Example File</a> to import..
					</div>

					<input type="file" class="form-control" accept=".xls,.xlsx" ref="excelFile">
				</div>
				<div class="card-footer">
					<div class="row align-items-center">
						<div class="col-auto">
							<button @click="importExcel" class="btn btn-success">Import</button>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
</template>