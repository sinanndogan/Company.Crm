import { createRouter, createWebHistory } from 'vue-router'
import NotFound from '../views/NotFound.vue'
import session from '../plugins/session'
import { useLayoutStore } from '@/stores/layout.store'
import adminRoutes from './admin.routes'
import authRoutes from './auth.routes'

const routes = [
	{ ...adminRoutes },
	{ ...authRoutes },
	{ path: '/:pathMatch(.*)*', name: 'not-found', component: NotFound }
]

const router = createRouter({
	history: createWebHistory(import.meta.env.BASE_URL),
	linkActiveClass: 'active',
	routes
})

router.beforeEach((to, from) => {
	const { showPageLoading } = useLayoutStore()
	showPageLoading()

	if (
		to.name !== 'Login' &&
		!session.isLoggedIn() &&
		to.matched.some((record) => record.meta.requiresAuth)
	) {
		return {
			name: 'Login',
			// save the location we were at to come back later
			query: { redirect: to.fullPath }
		}
	}
})

router.afterEach((to, from) => {
	const { hidePageLoading } = useLayoutStore()
	hidePageLoading()
})

export default router
