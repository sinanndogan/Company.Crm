<template>
	<div class="modal modal-blur fade" tabindex="-1" role="dialog" id="appModal">
		<div class="modal-dialog">
			<div class="modal-content" v-if="item">
				<form @submit.prevent="true">
					<div class="modal-header">
						<h5 class="modal-title" v-text="item.id > 0 ? 'Edit OfferStatus' : 'New OfferStatus'"></h5>
						<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
					</div>
					<div class="modal-body">
						<div class="row mb-3">
							<label class="col-sm-4 col-form-label required">OfferStatus Name</label>
							<div class="col-sm-8">
								<input name="name" v-model="item.name" class="form-control" required>
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
import { ref, computed } from 'vue'
import { Clone } from '@/plugins/helper'
import { stringifyExpression } from '@vue/compiler-core';
const props = defineProps(['item'])
const emit = defineEmits(['onSaved'])


const isValid = computed(() => {
	return props.item.name != null && props.item.name != '' && props.item.name.length !=0
})


function saveItem() {
	if (!isValid.value) {
		toastr.error('Form is not valid!')
		return;
	}
	// Fix ISO Date timezone
	const item = Clone(props.item);

	if (item.id > 0) {
		axios.patch("offerstatus/" + item.id, item).then(res => {
			toastr.success("OfferStatus updated!", "Updated")
			emit("onSaved", item)
		})
	} else {
		axios.post("offerstatus", item).then(res => {
			toastr.success("OfferStatus created!", "Created")
			emit("onSaved", item)
		})
	}
}
</script>