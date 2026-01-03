import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'

// 1. Define the routes array first
const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView,
    meta: { layout: 'admin' }
  },
  {
    path: '/members',
    name: 'members',
    component: () => import('../views/MemberListView.vue'),
    meta: { layout: 'admin' }
  },
  {
    path: '/member/:id',
    name: 'member-detail',
    component: () => import('../views/MemberDetailView.vue'),
    meta: { layout: 'admin' }
  },
  {
    path: '/members/create',
    name: 'member-create',
    component: () => import('../views/MemberCreateView.vue'),
    meta: { layout: 'admin' }
  },
  {
    path: '/member/edit/:id',
    name: 'member-edit',
    component: () => import('../views/MemberEditView.vue'),
    meta: { layout: 'admin' }
  }
]

// 2. Initialize the router instance
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes
})

// 3. Export the router 
export default router