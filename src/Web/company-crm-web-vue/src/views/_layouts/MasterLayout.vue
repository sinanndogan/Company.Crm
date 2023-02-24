<template>
	<Navbar />
	<div class="page-wrapper">
		<router-view></router-view>
		<Footer />
	</div>
</template>

<script setup>
import Navbar from '@/components/Navbar.vue'
import Footer from '@/components/Footer.vue'
</script>