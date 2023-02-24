import moment from 'moment'
import 'moment/dist/locale/tr'
moment.locale('tr')

function toISO(dateStr) {
	const date = new Date(dateStr);
	return new Date(date.getTime() - (date.getTimezoneOffset() * 60000)).toJSON();
}

export { moment, toISO }