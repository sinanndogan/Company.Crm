<script setup>
const props = defineProps(['visible'])
</script>
<template>
	<div class="loading-container" v-if="props.visible">
		<span class="btn btn-primary">
			<span class="spinner-border spinner-border-sm me-2" role="status"></span>
			<slot>Loading</slot> <span class="animated-dots"></span>
		</span>
	</div>
</template>
<style>
.table-responsive {
	position: relative;
}

.loading-container {
	position: absolute;
	width: 100%;
	display: flex;
	background: rgba(0, 0, 0, .5);
	height: 100%;
	align-items: center;
	justify-content: center;
	z-index: 1;
}
</style>