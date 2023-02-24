<script setup>
import { ref, computed, onMounted } from 'vue'
import { Clone } from '@/plugins/helper'
const props = defineProps(['item'])
const emit = defineEmits(['onSaved'])
const usersList = ref()

const isValid = computed(() => {
  return props.item.title != null && props.item.title != ''
  && props.item.description != null && props.item.description != ''
  && props.item.userId != null && props.item.userId != ''
})

onMounted(() => {
  fillItems()
})

function fillItems() {
  axios.get('user').then((res) => {
    usersList.value = res.data
  })
}

function saveItem() {
  if (!isValid.value) {
    toastr.error('Form is not valid!')
    return
  }
  const item = Clone(props.item)

  if (item.id > 0) {
    axios.patch(`notification/${item.id}`, item).then((res) => {
      toastr.success('Notification updated!', 'Updated')
      emit('onSaved', item)
    })
  } else {
    axios.post('notification', item).then((res) => {
      toastr.success('Notification created!', 'Created')
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
              v-text="item.id > 0 ? 'Edit Notification' : 'New Notification'"
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
              <label class="col-sm-4 col-form-label">User</label>
              <div class="col-sm-8">
                <Dropdown
                  v-model="item.userId"
                  :options="usersList"
                  optionLabel="email"
                  optionValue="id"
                  placeholder="Select a User"
                  :filter="true"
                  style="width: 100%"
                />
              </div>
            </div>
            <div class="row mb-3">
              <label class="col-sm-4 col-form-label required">Title</label>
              <div class="col-sm-8">
                <input
                  name="companyName"
                  v-model="item.title"
                  class="form-control"
                  required
                />
              </div>
            </div>
            <div class="row mb-3">
              <label class="col-sm-4 col-form-label required"
                >Description</label
              >
              <div class="col-sm-8">
                <input
                  name="companyName"
                  v-model="item.description"
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
