import MasterLayout from '../views/_layouts/MasterLayout.vue'
import CustomerList from '../views/customer/CustomerList.vue'
import HomePage from '../views/HomePage.vue'
import UserImport from '../views/user/UserImport.vue'
import RegionList from '../views/region/RegionList.vue'
import TaskList from '../views/task/TaskList.vue'
import NotificationList from '../views/notification/NotificationList.vue'
import UserAddressList from '../views/userAddress/UserAddressList.vue'
import UserStatusList from '../views/userStatus/UserStatusList.vue'
import TaskStatusList from '../views/taskStatus/TaskStatusList.vue'
import OfferStatusList from '../views/offerStatus/OfferStatusList.vue'

export default {
	path: '/',
	name: 'admin',
	component: MasterLayout,
	meta: { requiresAuth: true },
	children: [
		{ path: '/', name: 'home', component: HomePage },
		{ path: '/customer/list', component: CustomerList },
		{ path: '/user/import', component: UserImport },
		{ path: '/user-address/list', component: UserAddressList, meta: { requiresAuth: true } },
		{ path: '/user-status/list', component: UserStatusList, meta: { requiresAuth: true } },
		{ path: '/notification/list', component: NotificationList, meta: { requiresAuth: true } },
		{ path: '/offer-status/list', component: OfferStatusList, meta: { requiresAuth: true } },
		{ path: '/task-status/list', component: TaskStatusList, meta: { requiresAuth: true } },
		{ path: '/region/list', component: RegionList, meta: { requiresAuth: true } },
		{ path: '/task/list', component: TaskList },
		{
			path: '/about',
			name: 'about',
			// route level code-splitting
			// this generates a separate chunk (About.[hash].js) for this route
			// which is lazy-loaded when the route is visited.
			component: () => import('../views/AboutPage.vue')
		}
	]
}