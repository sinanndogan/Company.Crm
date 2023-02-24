<template>
	<div class="modal modal-blur fade" tabindex="-1" role="dialog" id="appModal">
		<div class="modal-dialog">
			<div class="modal-content" v-if="item">
				<form @submit.prevent="true">
					<div class="modal-header">
						<h5 class="modal-title" v-text="item.id > 0 ? 'Edit Customer' : 'New Customer'"></h5>
						<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
					</div>
					<div class="modal-body">
						<div class="row mb-3">
							<label class="col-sm-4 col-form-label">User</label>
							<div class="col-sm-8">
								<Dropdown v-model="item.userId" :options="usersList" optionLabel="email" optionValue="id" placeholder="Select a User" :filter="true" style="width: 100%" />
							</div>
						</div>
						<div class="row mb-3">
							<label class="col-sm-4 col-form-label required">Company Name</label>
							<div class="col-sm-8">
								<input name="companyName" v-model="item.companyName" class="form-control" required>
							</div>
						</div>
						<!-- <div class="row mb-3">
							<label class="col-sm-4 col-form-label">Birth Date</label>
							<div class="col-sm-8">
								<input type="date" v-model="item.birthDate" class="form-control" />
							</div>
						</div> -->
						<div class="row mb-3">
							<label class="col-sm-4 col-form-label">Birth Date</label>
							<div class="col-sm-8">
								<Calendar v-model="item.birthDate" dateFormat="dd.mm.yy" style="width: 100%" />
							</div>
						</div>
						<div class="form-group row">
							<label class="col-sm-4 col-form-label required">Gender</label>
							<div class="col-sm-8">
								<select v-model="item.genderId" class="form-select" required>
									<option :value="undefined">Please select</option>
									<option v-for="gender in gendersList" v-bind:value="gender.id" v-bind:key="gender.id">
										{{ gender.name }}
									</option>
								</select>
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
const gendersList = ref()
const usersList = ref()

const isValid = computed(() => {
	return props.item.companyName != null && props.item.companyName != '' && props.item.genderId > 0
})

onMounted(() => {
	fillItems()
})

function fillItems() {
	axios.get("gender").then(res => {
		gendersList.value = res.data
	})

	axios.get("user").then(res => {
		usersList.value = res.data
	})
}

function saveItem() {
	if (!isValid.value) {
		toastr.error('Form is not valid!')
		return;
	}

	// Fix ISO Date timezone
	const item = Clone(props.item);
	item.birthDate = toISO(item.birthDate);

	if (item.id > 0) {
		axios.patch("customer/" + item.id, item).then(res => {
			toastr.success("Customer updated!", "Updated")
			emit("onSaved", item)
		})
	} else {
		axios.post("customer", item).then(res => {
			toastr.success("Customer created!", "Created")
			emit("onSaved", item)
		})
	}
}
</script>