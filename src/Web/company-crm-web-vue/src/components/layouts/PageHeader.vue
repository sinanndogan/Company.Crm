<script setup>
defineProps(['title'])
</script>
<template>
	<div class="page-header d-print-none">
		<div class="container-xl">
			<div class="row g-2 align-items-center">
				<div class="col">
					<h2 class="page-title">
						{{ title }}
					</h2>
				</div>
			</div>
		</div>
	</div>
</template>