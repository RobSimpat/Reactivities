import axios, { AxiosResponse } from "axios"
import { User, UserFormValues } from "../models/user";
import { store } from "../stores/store";


const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay)
    })
}

axios.defaults.baseURL = 'https://localhost:7152/api';

axios.interceptors.request.use(config =>{
    const token = store.commonStore.token;
    if(token) config.headers.Authorization =`Bearer ${token}`
    return config;
})

axios.interceptors.response.use(async response => {
    try {
        await sleep(1000);
        return response;
    } catch (error) {
        console.log(error);
        return await Promise.reject(error);
    }
})
const responseBody = <T>(response: AxiosResponse<T>) => response.data;

const requests = {
    get: <T>(url: string) => axios.get<T>(url).then(responseBody),
    post: <T>(url: string, body: {}) => axios.post<T>(url, body).then(responseBody),
    put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
    del: <T>(url: string) => axios.delete<T>(url).then(responseBody),
}

const Account = {
    current: () => requests.get<User>('/Account'),
    login: (user: UserFormValues)=> requests.post<User>('/Account/login',user),
    register: (user: UserFormValues) => requests.post<User>('/Account/register', user),
}

const agent={
    Account
}
export default agent;