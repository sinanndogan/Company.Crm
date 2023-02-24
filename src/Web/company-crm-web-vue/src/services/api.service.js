import axios from 'axios'
import session from '@/plugins/session'
import router from '@/router'

let instance = axios.create({
	baseUrl: import.meta.env.VITE_API_URL || '',
	timeout: 5000,
	headers: {
		'Content-Type': 'application/json'
	}
})

//instance.defaults.baseURL = import.meta.env.VITE_API_URL || ''
//instance.defaults.headers.common['Content-Type'] = 'application/json'

// Request Interceptor
instance.interceptors.request.use((config) => {
	if (!import.meta.env.PROD)
		console.log('[Request][' + config.method + ']', config.baseURL + config.url)

	const token = session.getSession()
	if (token) {
		config.headers["Authorization"] = `Bearer ${token.accessToken}`
	}

	return config
});

// Response Interceptor
instance.interceptors.response.use((response) => {
	if (!import.meta.env.PROD)
		console.log('[Response][' + response.status + ']', response)
	return response
}, (error) => {
	const { config, response } = error
	const originalRequest = config
	const { status, statusText, data } = response

	if (error.code == "ERR_NETWORK" || status >= 500) {
		const errorMessage = (data && data.message) || error.message || error.response.data.message
		toastr.error(errorMessage, 'Error')
		return Promise.reject(error)
	}

	if (status === 401) {
		if (!originalRequest._retry) {
			originalRequest._retry = true;
			const token = session.getSession()
			const refreshToken = token.refreshToken;
			if (!import.meta.env.PROD)
				console.log('[Refresh Token]', refreshToken)

			return axios.post("auth/refresh-token", refreshToken, {
				headers: { "Content-Type": "application/json" }
			}).then((res) => {
				if (res && res.data && res.data.accessToken) {
					if (!import.meta.env.PROD)
						console.log('[New Token]', res.data)

					session.setSession(res.data)
					axios.defaults.headers.common["Authorization"] = "Bearer " + res.data.accessToken;
					originalRequest.headers["Authorization"] = "Bearer " + res.data.accessToken;
					return axios(originalRequest);
				}
			});
		}
		else {
			router.push({ name: 'login' });
		}
	}
});


export default instance
