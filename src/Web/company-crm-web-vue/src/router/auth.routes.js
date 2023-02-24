import Login from '../views/auth/Login.vue'
import Register from '../views/auth/Register.vue'
import BasicLayout from '../views/_layouts/BasicLayout.vue'

export default {
	path: '/auth',
	name: 'auth',
	redirect: '/auth/login',
	component: BasicLayout,
	children: [
		{ path: '/auth/login', name: 'Login', component: Login },
		{ path: '/auth/register', name: 'Register', component: Register }
	]
}