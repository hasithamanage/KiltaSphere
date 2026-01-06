import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import { useAuthStore } from '@/stores/authStore';

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
  },
  {
  path: '/login',
  name: 'login',
  component: () => import('../views/LoginView.vue'),
  meta: { layout: 'auth' } // This triggers the centered AuthLayout
}
]

// 2. Initialize the router instance
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes
})

// Navigation guard to prevent unauthorized access

router.beforeEach(async (to, from, next) => {
  const authStore = useAuthStore();
  const isAuthenticated = authStore.isAuthenticated;
  const userRole = authStore.user?.role;

  // 1. Check if the route requires a specific role (e.g., Admin)
  if (to.meta.layout === 'admin' && userRole !== 'Admin') {
    // If trying to access admin area but not an admin, redirect to login
    // or to a "Forbidden" page. For now, let's go to login.
    return next({ name: 'login' });
  }

  // 2. Check if the route is for Members only
  if (to.meta.layout === 'member' && !isAuthenticated) {
    return next({ name: 'login' });
  }

  // 3. Prevent logged-in users from going back to the Login page
  if (to.name === 'login' && isAuthenticated) {
    return next({ name: 'home' });
  }

  // Otherwise, allow navigation
  next();
});

// 3. Export the router 
export default router