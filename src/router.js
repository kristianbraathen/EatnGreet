import { createRouter, createWebHistory } from 'vue-router'
import LoginPageVue from './components/LoginPage.vue'

import NewRegisterVue from './components/NewRegister.vue'
import MyProfile from './components/MyProfile.vue'
import MainPageVue from './components/MainPage.vue'


const routes = [
    {
        path: '/',
        name: 'login',
        component: LoginPageVue
        
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        // component: () => import(/* webpackChunkName: "about" */ '../src/components/NewUserRegistration.vue')
    },
    {
        path: '/newregister',
        name: 'newregister',
        component: NewRegisterVue
       
    },
    {
        path: '/MyProfile',
        name: 'MyProfile',
        component: MyProfile
    },
    {
        path: '/MainPage',
        name: 'MainPage',
        component: MainPageVue
    }

]

const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router