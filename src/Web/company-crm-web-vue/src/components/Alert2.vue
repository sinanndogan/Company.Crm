<script setup>
// Composition API
defineProps(['title']);
</script>
<template>
	<div class="alert alert-primary" role="alert">
		{{ title }}
	</div>
</template>