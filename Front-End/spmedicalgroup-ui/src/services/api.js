import axios from 'axios'

const api = axios.create({
    baseURL: 'http://localhost:38976/api'
})

export default api;