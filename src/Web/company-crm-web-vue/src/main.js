import { createApp } from 'vue'
import { createPinia } from 'pinia'
import App from './App.vue'
import router from './router'
import './assets/main.css'
import '@/plugins/axios.js'
import '@/plugins/notification.js'
import PrimeVue from 'primevue/config'
import Calendar from 'primevue/calendar'
import Dropdown from 'primevue/dropdown'
import 'primevue/resources/themes/saga-blue/theme.css'
import 'primevue/resources/primevue.min.css'
import { tr } from '@/assets/primevue-langs.json'

const app = createApp(App)

//app.config.globalProperties.$moment = moment

// PrimeVue Components
app.use(PrimeVue, {
	zIndex: {
		modal: 2100,        //dialog, sidebar
		overlay: 2000,      //dropdown, overlaypanel
		menu: 2000,         //overlay menus
		tooltip: 2100       //tooltip
	},
	locale: tr
})
app.component('Calendar', Calendar);
app.component('Dropdown', Dropdown);

app.use(createPinia())
app.use(router)
app.mount('#app')
