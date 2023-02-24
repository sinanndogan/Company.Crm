<script setup>
import { ref, onMounted, computed } from 'vue'
import Auth from '@/services/auth.service'
import IconNotification from './icons/IconNotification.vue'
import IconUserAdress from './icons/IconUserAddress.vue'

const appName = ref('CRM Admin')
const version = ref('1.0')
const user = ref({})
onMounted(() => {
	axios.post('/Auth/Me').then(res => {
		if (res && res.data) {
			user.value = res.data
		}
	})
})
const avatarStyle = computed(() => {
	if (user.value.profilePhoto)
		return 'background-image: url(/template/static/avatars/' + user.value.profilePhoto + ')'
	else
		return 'background-image: url(/template/static/avatars/000m.jpg)'
})
function handleLogout() {
	Auth.Logout()
}
</script>

<template>
	<!-- Navbar -->
	<header class="navbar navbar-expand-md navbar-light d-print-none">
		<div class="container-xl">
			<button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbar-menu" aria-controls="navbar-menu" aria-expanded="false" aria-label="Toggle navigation">
				<span class="navbar-toggler-icon"></span>
			</button>
			<h1 class="navbar-brand navbar-brand-autodark d-none-navbar-horizontal pe-0 pe-md-3">
				<a href="javascript:" @click="handleMe">
					<img src="../assets/logo.svg" alt="Tabler" class="navbar-brand-image">
					{{ appName }} {{ version }}
				</a>
			</h1>
			<div class="navbar-nav flex-row order-md-last">
				<div class="d-none d-md-flex">
					<a href="?theme=dark" class="nav-link px-0 hide-theme-dark" title="Enable dark mode" data-bs-toggle="tooltip" data-bs-placement="bottom">
						<!-- Download SVG icon from http://tabler-icons.io/i/moon -->
						<svg xmlns="http://www.w3.org/2000/svg" class="icon" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">
							<path stroke="none" d="M0 0h24v24H0z" fill="none" />
							<path d="M12 3c.132 0 .263 0 .393 0a7.5 7.5 0 0 0 7.92 12.446a9 9 0 1 1 -8.313 -12.454z" />
						</svg>
					</a>
					<a href="?theme=light" class="nav-link px-0 hide-theme-light" title="Enable light mode" data-bs-toggle="tooltip" data-bs-placement="bottom">
						<!-- Download SVG icon from http://tabler-icons.io/i/sun -->
						<svg xmlns="http://www.w3.org/2000/svg" class="icon" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">
							<path stroke="none" d="M0 0h24v24H0z" fill="none" />
							<circle cx="12" cy="12" r="4" />
							<path d="M3 12h1m8 -9v1m8 8h1m-9 8v1m-6.4 -15.4l.7 .7m12.1 -.7l-.7 .7m0 11.4l.7 .7m-12.1 -.7l-.7 .7" />
						</svg>
					</a>
					<div class="nav-item dropdown d-none d-md-flex me-3">
						<a href="#" class="nav-link px-0" data-bs-toggle="dropdown" tabindex="-1" aria-label="Show notifications">
							<!-- Download SVG icon from http://tabler-icons.io/i/bell -->
							<svg xmlns="http://www.w3.org/2000/svg" class="icon" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">
								<path stroke="none" d="M0 0h24v24H0z" fill="none" />
								<path d="M10 5a2 2 0 0 1 4 0a7 7 0 0 1 4 6v3a4 4 0 0 0 2 3h-16a4 4 0 0 0 2 -3v-3a7 7 0 0 1 4 -6" />
								<path d="M9 17v1a3 3 0 0 0 6 0v-1" />
							</svg>
							<span class="badge bg-red"></span>
						</a>
						<div class="dropdown-menu dropdown-menu-arrow dropdown-menu-end dropdown-menu-card">
							<div class="card">
								<div class="card-header">
									<h3 class="card-title">Last updates</h3>
								</div>
								<div class="list-group list-group-flush list-group-hoverable">
									<div class="list-group-item">
										<div class="row align-items-center">
											<div class="col-auto"><span class="status-dot status-dot-animated bg-red d-block"></span></div>
											<div class="col text-truncate">
												<a href="#" class="text-body d-block">Example 1</a>
												<div class="d-block text-muted text-truncate mt-n1">
													Change deprecated html tags to text decoration classes (#29604)
												</div>
											</div>
											<div class="col-auto">
												<a href="#" class="list-group-item-actions">
													<!-- Download SVG icon from http://tabler-icons.io/i/star -->
													<svg xmlns="http://www.w3.org/2000/svg" class="icon text-muted" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">
														<path stroke="none" d="M0 0h24v24H0z" fill="none" />
														<path d="M12 17.75l-6.172 3.245l1.179 -6.873l-5 -4.867l6.9 -1l3.086 -6.253l3.086 6.253l6.9 1l-5 4.867l1.179 6.873z" />
													</svg>
												</a>
											</div>
										</div>
									</div>
									<div class="list-group-item">
										<div class="row align-items-center">
											<div class="col-auto"><span class="status-dot d-block"></span></div>
											<div class="col text-truncate">
												<a href="#" class="text-body d-block">Example 2</a>
												<div class="d-block text-muted text-truncate mt-n1">
													justify-content:between ⇒ justify-content:space-between (#29734)
												</div>
											</div>
											<div class="col-auto">
												<a href="#" class="list-group-item-actions show">
													<!-- Download SVG icon from http://tabler-icons.io/i/star -->
													<svg xmlns="http://www.w3.org/2000/svg" class="icon text-yellow" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">
														<path stroke="none" d="M0 0h24v24H0z" fill="none" />
														<path d="M12 17.75l-6.172 3.245l1.179 -6.873l-5 -4.867l6.9 -1l3.086 -6.253l3.086 6.253l6.9 1l-5 4.867l1.179 6.873z" />
													</svg>
												</a>
											</div>
										</div>
									</div>
									<div class="list-group-item">
										<div class="row align-items-center">
											<div class="col-auto"><span class="status-dot d-block"></span></div>
											<div class="col text-truncate">
												<a href="#" class="text-body d-block">Example 3</a>
												<div class="d-block text-muted text-truncate mt-n1">
													Update change-version.js (#29736)
												</div>
											</div>
											<div class="col-auto">
												<a href="#" class="list-group-item-actions">
													<!-- Download SVG icon from http://tabler-icons.io/i/star -->
													<svg xmlns="http://www.w3.org/2000/svg" class="icon text-muted" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">
														<path stroke="none" d="M0 0h24v24H0z" fill="none" />
														<path d="M12 17.75l-6.172 3.245l1.179 -6.873l-5 -4.867l6.9 -1l3.086 -6.253l3.086 6.253l6.9 1l-5 4.867l1.179 6.873z" />
													</svg>
												</a>
											</div>
										</div>
									</div>
									<div class="list-group-item">
										<div class="row align-items-center">
											<div class="col-auto"><span class="status-dot status-dot-animated bg-green d-block"></span></div>
											<div class="col text-truncate">
												<a href="#" class="text-body d-block">Example 4</a>
												<div class="d-block text-muted text-truncate mt-n1">
													Regenerate package-lock.json (#29730)
												</div>
											</div>
											<div class="col-auto">
												<a href="#" class="list-group-item-actions">
													<!-- Download SVG icon from http://tabler-icons.io/i/star -->
													<svg xmlns="http://www.w3.org/2000/svg" class="icon text-muted" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">
														<path stroke="none" d="M0 0h24v24H0z" fill="none" />
														<path d="M12 17.75l-6.172 3.245l1.179 -6.873l-5 -4.867l6.9 -1l3.086 -6.253l3.086 6.253l6.9 1l-5 4.867l1.179 6.873z" />
													</svg>
												</a>
											</div>
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>
				</div>
				<div class="nav-item dropdown">
					<a href="#" class="nav-link d-flex lh-1 text-reset p-0" data-bs-toggle="dropdown" aria-label="Open user menu">
						<span class="avatar avatar-sm" :style="avatarStyle"></span>
						<div class="d-none d-xl-block ps-2">
							<div>{{ user.fullName }}</div>
							<div class="mt-1 small text-muted">{{ user.title }}</div>
						</div>
					</a>
					<div class="dropdown-menu dropdown-menu-end dropdown-menu-arrow">
						<a href="#" class="dropdown-item">Status</a>
						<a href="#" class="dropdown-item">Profile</a>
						<a href="#" class="dropdown-item">Feedback</a>
						<div class="dropdown-divider"></div>
						<a href="./settings.html" class="dropdown-item">Settings</a>
						<button class="dropdown-item" @click.prevent="handleLogout">Logout</button>
					</div>
				</div>
			</div>
		</div>
	</header>
	<div class="navbar-expand-md">
		<div class="collapse navbar-collapse" id="navbar-menu">
			<div class="navbar navbar-light">
				<div class="container-xl">
					<ul class="navbar-nav">
						<li class="nav-item">
							<router-link class="nav-link" aria-current="page" to="/">
								<span class="nav-link-icon d-md-none d-lg-inline-block"><!-- Download SVG icon from http://tabler-icons.io/i/home -->
									<svg xmlns="http://www.w3.org/2000/svg" class="icon" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">
										<path stroke="none" d="M0 0h24v24H0z" fill="none" />
										<polyline points="5 12 3 12 12 3 21 12 19 12" />
										<path d="M5 12v7a2 2 0 0 0 2 2h10a2 2 0 0 0 2 -2v-7" />
										<path d="M9 21v-6a2 2 0 0 1 2 -2h2a2 2 0 0 1 2 2v6" />
									</svg>
								</span>
								<span class="nav-link-title">
									Home
								</span>
							</router-link>
						</li>

						<li class="nav-item dropdown">
							<button class="nav-link bg-transparent border-0 dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">
								<span class="nav-link-icon d-md-none d-lg-inline-block">
									<svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-brightness-2" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">
										<path stroke="none" d="M0 0h24v24H0z" fill="none"></path>
										<path d="M12 12m-3 0a3 3 0 1 0 6 0a3 3 0 1 0 -6 0"></path>
										<path d="M6 6h3.5l2.5 -2.5l2.5 2.5h3.5v3.5l2.5 2.5l-2.5 2.5v3.5h-3.5l-2.5 2.5l-2.5 -2.5h-3.5v-3.5l-2.5 -2.5l2.5 -2.5z"></path>
									</svg>
								</span>
								<span class="nav-link-title">
									Administrator
								</span>
							</button>
							<ul class="dropdown-menu">
								<li>
									<router-link class="dropdown-item" to="/user/list">
										Users
									</router-link>
								</li>
								<li>
									<router-link class="dropdown-item" to="/user-address/list">
										User Addresses
									</router-link>
								</li>
								<li>
									<router-link class="dropdown-item" to="/user/import">
										Import Users
									</router-link>
								</li>
							</ul>
						</li>

						<li class="nav-item dropdown">
							<button class="nav-link bg-transparent border-0 dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">
								<span class="nav-link-icon d-md-none d-lg-inline-block">
									<svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-users" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">
										<path stroke="none" d="M0 0h24v24H0z" fill="none"></path>
										<circle cx="9" cy="7" r="4"></circle>
										<path d="M3 21v-2a4 4 0 0 1 4 -4h4a4 4 0 0 1 4 4v2"></path>
										<path d="M16 3.13a4 4 0 0 1 0 7.75"></path>
										<path d="M21 21v-2a4 4 0 0 0 -3 -3.85"></path>
									</svg>
								</span>
								<span class="nav-link-title">
									Customers
								</span>
							</button>
							<ul class="dropdown-menu">
								<li>
									<router-link class="dropdown-item" to="/customer/list">
										All Customers
									</router-link>
								</li>
								<li>
									<router-link class="dropdown-item" to="/task/list">
										Tasks
									</router-link>
								</li>
							</ul>
						</li>

						<li class="nav-item dropdown">
							<button class="nav-link bg-transparent border-0 dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">
								<span class="nav-link-icon d-md-none d-lg-inline-block">
									<IconNotification></IconNotification>
								</span>
								<span class="nav-link-title">
									Notifications
								</span>
							</button>
							<ul class="dropdown-menu">
								<li>
									<router-link class="dropdown-item" to="/notification/list">
										List
									</router-link>
								</li>
							</ul>
						</li>

						<li class="nav-item dropdown">
							<button class="nav-link bg-transparent border-0 dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">
								<span class="nav-link-icon d-md-none d-lg-inline-block">
									<svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-list-details" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">
										<path stroke="none" d="M0 0h24v24H0z" fill="none"></path>
										<path d="M13 5h8"></path>
										<path d="M13 9h5"></path>
										<path d="M13 15h8"></path>
										<path d="M13 19h5"></path>
										<path d="M3 4m0 1a1 1 0 0 1 1 -1h4a1 1 0 0 1 1 1v4a1 1 0 0 1 -1 1h-4a1 1 0 0 1 -1 -1z"></path>
										<path d="M3 14m0 1a1 1 0 0 1 1 -1h4a1 1 0 0 1 1 1v4a1 1 0 0 1 -1 1h-4a1 1 0 0 1 -1 -1z"></path>
									</svg>
								</span>
								<span class="nav-link-title">
									Lists
								</span>
							</button>
							<ul class="dropdown-menu">
								<li>
									<router-link class="dropdown-item" to="/offer-status/list">
										Offer Status
									</router-link>
								</li>
								<li>
									<router-link class="dropdown-item" to="/task-status/list">
										Task Status
									</router-link>
								</li>
								<li>
									<router-link class="dropdown-item" to="/region/list">
										Region
									</router-link>
								</li>
								<li>
									<router-link class="dropdown-item" to="/user-status/list">
										User Status
									</router-link>
								</li>
							</ul>
						</li>

						<li class="nav-item dropdown">
							<button class="nav-link bg-transparent border-0 dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">
								<span class="nav-link-icon d-md-none d-lg-inline-block">
									<svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-file-code-2" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">
										<path stroke="none" d="M0 0h24v24H0z" fill="none"></path>
										<path d="M10 12h-1v5h1"></path>
										<path d="M14 12h1v5h-1"></path>
										<path d="M14 3v4a1 1 0 0 0 1 1h4"></path>
										<path d="M17 21h-10a2 2 0 0 1 -2 -2v-14a2 2 0 0 1 2 -2h7l5 5v11a2 2 0 0 1 -2 2z"></path>
									</svg>
								</span>
								<span class="nav-link-title">
									Dev
								</span>
							</button>
							<ul class="dropdown-menu">
								<li>
									<router-link class="dropdown-item" to="/auth/login">
										<span class="nav-link-icon d-md-none d-lg-inline-block">
											<svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-login" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">
												<path stroke="none" d="M0 0h24v24H0z" fill="none"></path>
												<path d="M14 8v-2a2 2 0 0 0 -2 -2h-7a2 2 0 0 0 -2 2v12a2 2 0 0 0 2 2h7a2 2 0 0 0 2 -2v-2"></path>
												<path d="M20 12h-13l3 -3m0 6l-3 -3"></path>
											</svg>
										</span>
										<span class="nav-link-title">
											Login
										</span>
									</router-link>
								</li>
								<li>
									<a class="dropdown-item" href="/template/index.html" target="_blank">
										<span class="nav-link-icon d-md-none d-lg-inline-block">
											<svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-template" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">
												<path stroke="none" d="M0 0h24v24H0z" fill="none"></path>
												<rect x="4" y="4" width="16" height="4" rx="1"></rect>
												<rect x="4" y="12" width="6" height="8" rx="1"></rect>
												<line x1="14" y1="12" x2="20" y2="12"></line>
												<line x1="14" y1="16" x2="20" y2="16"></line>
												<line x1="14" y1="20" x2="20" y2="20"></line>
											</svg>
										</span>
										<span class="nav-link-title">
											Template
										</span>
									</a>
								</li>
							</ul>
						</li>
					</ul>
					<div class="my-2 my-md-0 flex-grow-1 flex-md-grow-0 order-first order-md-last">
						<form action="./" method="get" autocomplete="off" novalidate>
							<div class="input-icon">
								<span class="input-icon-addon">
									<!-- Download SVG icon from http://tabler-icons.io/i/search -->
									<svg xmlns="http://www.w3.org/2000/svg" class="icon" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">
										<path stroke="none" d="M0 0h24v24H0z" fill="none" />
										<circle cx="10" cy="10" r="7" />
										<line x1="21" y1="21" x2="15" y2="15" />
									</svg>
								</span>
								<input type="text" value="" class="form-control" placeholder="Search…" aria-label="Search in website">
							</div>
						</form>
					</div>
				</div>
			</div>
		</div>
	</div>
</template>

<style scoped>
.navbar-brand img {
	height: 24px;
}
</style>
