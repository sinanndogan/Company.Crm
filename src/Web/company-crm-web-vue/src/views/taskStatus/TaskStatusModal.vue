<script setup>
import { ref, computed, onMounted } from 'vue'
import { toISO } from '@/plugins/datetime'
import { Clone } from '@/plugins/helper'
const props = defineProps(['item'])
const emit = defineEmits(['onSaved'])

const isValid = computed(() => {
  return props.item.name != null && props.item.name != ''
})

onMounted(() => {})

function saveItem() {
  if (!isValid.value) {
    toastr.error('Form is not valid!')
    return
  }

 

  if (item.id > 0) {
    axios.patch('taskStatus/' + item.id, item).then((res) => {
      toastr.success('Task Status updated!', 'Updated')
      emit('onSaved', item)
    })
  } else {
    axios.post('taskStatus/', item).then((res) => {
      toastr.success('Task Status created!', 'Created')
      emit('onSaved', item)
    })
  }
}
</script>
<template>
  <div class="modal modal-blur fade" tabindex="-1" role="dialog" id="appModal">
    <div class="modal-dialog">
      <div class="modal-content" v-if="item">
        <form @submit.prevent="true">
          <div class="modal-header">
            <h5
              class="modal-title"
              v-text="item.id > 0 ? 'Edit Task Status' : 'New Task Status'"
            ></h5>
            <button
              type="button"
              class="btn-close"
              data-bs-dismiss="modal"
              aria-label="Close"
            ></button>
          </div>
          <div class="modal-body">
            <div class="row mb-3">
              <label class="col-sm-4 col-form-label required">Name</label>
              <div class="col-sm-8">
                <input
                  name="name"
                  v-model="item.name"
                  class="form-control"
                  required
                />
              </div>
            </div>
          </div>
          <div class="modal-footer">
            <button class="btn me-auto" data-bs-dismiss="modal">Close</button>
            <button
              class="btn btn-primary"
              v-text="item.id > 0 ? 'Update' : 'Save'"
              @click="saveItem"
            ></button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>
