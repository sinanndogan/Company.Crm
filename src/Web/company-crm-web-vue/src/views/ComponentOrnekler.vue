<script setup>
import Alert1 from '../components/Alert1.vue'
import Alert2 from '../components/Alert2.vue'
import Alert3 from '../components/Alert3.vue'
import Card from '../components/Card.vue'
</script>

<template>
	<div class="container">
		<Alert1 title="Cem"></Alert1>
		<Alert2 title="Mehmet"></Alert2>
		<Alert3>
			İçerik değeri
		</Alert3>

		<div class="row">
			<div class="col">
				<Card title="Black dog picture" photoId="237">
					Some quick example text 1
				</Card>
			</div>
			<div class="col">
				<Card title="Computer picture" photoId="5">
					Some quick example text 1
				</Card>
			</div>
			<div class="col">
				<Card title="Laptop desk picture" photoId="6">
					Some quick example text 1
				</Card>
			</div>
		</div>
	</div>
</template>