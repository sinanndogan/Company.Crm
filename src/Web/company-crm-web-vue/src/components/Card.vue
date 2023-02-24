<script setup>
defineProps(['title', 'photoId']);
</script>
<template>
	<div class="card" style="width: 18rem;">
		<img :src="'https://picsum.photos/id/' + (photoId ?? '1') + '/300/300'" class="card-img-top" alt="...">
		<div class="card-body">
			<h5 class="card-title">{{ title }}</h5>
			<p class="card-text">
				<slot />
			</p>
		</div>
	</div>
</template>
<style scoped>
/* scoped: Bir stil tanımlamasının sadece component içerisinde geçerli olmasını sağlar. */
h5 {
	color: chocolate !important;
}
</style>