<script setup>
import Loading from "@/components/Loading.vue";
import { useLayoutStore } from "./stores/layout.store";
const store = useLayoutStore();
</script>

<template>
	<Loading :visible="store.isPageLoading">Page Loading</Loading>
	<router-view></router-view>
</template>
