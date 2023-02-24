<template>
	<div class="modal modal-blur fade" tabindex="-1" role="dialog" id="appModal">
		<div class="modal-dialog">
			<div class="modal-content" v-if="item">
				<form @submit.prevent="true">
					<div class="modal-header">
						<h5 class="modal-title" v-text="item.id > 0 ? 'Edit task' : 'New task'"></h5>
						<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
					</div>
					<div class="modal-body">
						<!-- <div class="row mb-3">
							<label class="col-sm-4 col-form-label">Request</label>
							<div class="col-sm-8">
								<Dropdown v-model="item.requestId" :options="requestList" optionLabel="request" optionValue="id" placeholder="Select a Request" :filter="true" style="width: 100%" />
							</div>
						</div> -->
					<div class="row mb-3">
						<label class="col-sm-4 col-form-label required">Request Id</label>
						<div class="col-sm-8">
							<input name="employeeUserId" v-model="item.requestId" class="form-control" required>
						</div>
					</div>
						<div class="row mb-3">
							<label class="col-sm-4 col-form-label required">Employee User Id</label>
							<div class="col-sm-8">
								<input name="employeeUserId" v-model="item.employeeUserId" class="form-control" required>
							</div>
						</div>
						<!-- <div class="row mb-3">
							<label class="col-sm-4 col-form-label">Birth Date</label>
							<div class="col-sm-8">
								<input type="date" v-model="item.birthDate" class="form-control" />
							</div>
						</div> -->
						<div class="row mb-3">
							<label class="col-sm-4 col-form-label">Task Start Date</label>
							<div class="col-sm-8">
								<Calendar v-model="item.taskStartDate" dateFormat="dd.mm.yy" />
							</div>
						</div>

						<div class="row mb-3">
							<label class="col-sm-4 col-form-label">Task End Date</label>
							<div class="col-sm-8">
								<Calendar v-model="item.taskEndDate" dateFormat="dd.mm.yy" />
							</div>
						</div>

						<div class="row mb-3">
							<label class="col-sm-4 col-form-label">Description</label>
							<div class="col-sm-8">
								<input name="employeeUserId" v-model="item.description" class="form-control" required>
							</div>
						</div>

						<div class="row mb-3">
						<label class="col-sm-4 col-form-label">Task Status</label>
						<div class="col-sm-8">
							<Dropdown v-model="item.taskStatusId" :options="taskStatusList" optionLabel="name" optionValue="id" placeholder="Select a Task Status" :filter="true" style="width: 100%" />
						</div>
						</div> 

					</div>
					<div class="modal-footer">
						<button class="btn me-auto" data-bs-dismiss="modal">Close</button>
						<button class="btn btn-primary" v-text="item.id > 0 ? 'Update' : 'Save'" @click="saveItem"></button>
					</div>
				</form>
			</div>
		</div>
	</div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import { toISO } from '@/plugins/datetime'
import { Clone } from '@/plugins/helper'
const props = defineProps(['item'])
const emit = defineEmits(['onSaved'])
const taskStatusList = ref()

const isValid = computed(() => {
	return props.item.requestId != null && 
	props.item.employeeUserId != null && 
	props.item.taskStartDate != null &&
	props.item.taskStatusId != null 
	
})

onMounted(() => {
	fillItems()
})

function fillItems() {
	axios.get("TaskStatus").then(res => {
		taskStatusList.value = res.data
	})
}

function saveItem() {
	if (!isValid.value) {
		toastr.error('Form is not valid!')
		return;
	}

	// Fix ISO Date timezone
	const item = Clone(props.item);
	item.taskStartDate = toISO(item.taskStartDate);
	item.taskEndDate = toISO(item.taskEndDate);

	if (item.id > 0) {
		axios.put("task/" + item.id, item).then(res => {
			toastr.success("task updated!", "Updated")
			emit("onSaved", item)
		})
	} else {
		axios.post("task", item).then(res => {
			toastr.success("task created!", "Created")
			emit("onSaved", item)
		})
	}
}
</script>