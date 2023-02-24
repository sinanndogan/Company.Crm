export default {
	setSession: function (data) {
		window.localStorage.setItem('token', JSON.stringify(data))
	},
	getSession: function () {
		return JSON.parse(window.localStorage.getItem('token'))
	},
	clearSession: function () {
		window.localStorage.removeItem('token')
	},
	hashSession: function (data) {
		let token = this.getSession()
		//return btoa(JSON.stringify(token))
		return Buffer.from(JSON.stringify(token)).toString('base64')
	},
	needsAuthentication: function () {
		let token = window.localStorage.getItem('token')
		if (!token) {
			return true
		}
		return false
	},
	isLoggedIn: function () {
		return !this.needsAuthentication()
	}
}
