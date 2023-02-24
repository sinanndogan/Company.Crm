<template>
	<div class="page-body">
		<div class="container-xl d-flex flex-column justify-content-center">
			<div class="empty">
				<div class="empty-img">
					<img src="/template/static/illustrations/undraw_printing_invoices_5r4r.svg" height="128" alt="">
				</div>
				<p class="empty-title">Welcome</p>
				<p class="empty-subtitle text-muted">
					Welcome to Company.CRM web application.
				</p>
			</div>


			<div class="row row-cards">
				<div class="col-lg-6">
					<div class="card">
						<div class="card-body">
							<div id="chart-tasks-overview"></div>
						</div>
					</div>
				</div>
				<div class="col-lg-6">
					<div class="card">
						<div class="card-body">
							<div id="chart-demo-pie"></div>
						</div>
					</div>
				</div>
			</div>

		</div>
	</div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import _, { map } from 'underscore';

const chartData = ref();

onMounted(() => {

	axios.get('sale/chartData').then(response => {
		chartData.value = response.data.data

		// 1. yöntem
		// const years = [];
		// const counts = [];
		// const totalSalePrices = [];
		// chartData.value.forEach(data => {
		// 	years.push(data.year)
		// 	counts.push(data.saleCount)
		// 	totalSalePrices.push(data.totalSalePrice)
		// })

		// underscore ile veriden kolon dizisi yapma
		const years = _.pluck(chartData.value, 'year')
		const counts = _.pluck(chartData.value, 'saleCount');
		const totalSalePrices = _.pluck(chartData.value, 'totalSalePrice');


		window.ApexCharts && (new ApexCharts(document.getElementById('chart-tasks-overview'), {
			chart: {
				type: "bar",
				fontFamily: 'inherit',
				height: 320,
				parentHeightOffset: 0,
				toolbar: {
					show: false,
				},
				animations: {
					enabled: false
				},
			},
			plotOptions: {
				bar: {
					columnWidth: '50%',
				}
			},
			dataLabels: {
				enabled: true,
			},
			fill: {
				opacity: 1,
			},
			tooltip: {
				theme: 'dark'
			},
			grid: {
				padding: {
					top: -20,
					right: 0,
					left: -4,
					bottom: -4
				},
				strokeDashArray: 4,
			},
			series: [{
				name: "A",
				data: counts
			}],
			xaxis: {
				labels: {
					padding: 0,
				},
				tooltip: {
					enabled: false
				},
				axisBorder: {
					show: false,
				},
				categories: years,
			},
			yaxis: {
				labels: {
					padding: 4
				},
			},
			colors: [tabler.getColor("primary")],
			legend: {
				show: false,
			},
		})).render();

		window.ApexCharts && (new ApexCharts(document.getElementById('chart-demo-pie'), {
			chart: {
				type: "donut",
				fontFamily: 'inherit',
				height: 360,
				sparkline: {
					enabled: true
				},
				animations: {
					enabled: false
				},
			},
			fill: {
				opacity: 1,
			},
			series: totalSalePrices,
			labels: years,
			grid: {
				strokeDashArray: 4,
			},
			colors: [tabler.getColor("primary"), tabler.getColor("primary", 0.8), tabler.getColor("primary", 0.6), tabler.getColor("gray-300")],
			legend: {
				show: true,
				position: 'bottom',
				offsetY: 12,
				markers: {
					width: 10,
					height: 10,
					radius: 100,
				},
				itemMargin: {
					horizontal: 8,
					vertical: 8
				},
			},
			tooltip: {
				theme: 'dark',
				fillSeriesColor: false,
				formatter: function (value) {
					return value + " $";
				}
			}
		})).render();

	})
})
</script>