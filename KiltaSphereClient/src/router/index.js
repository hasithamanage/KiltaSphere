import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import MemberListView from '../views/MemberListView.vue'; // 1. Import the MemberList view
import MemberCreateView from '../views/MemberCreateView.vue'; // 2. Import the MemberCreate view
import MemberDetailView from '../views/MemberDetailView.vue'; // 3. Import the Member Detail view
import MemberEditView from '../views/MemberEditView.vue'; // 4. Import the Member edit view

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
    },

    // 1. Add the route for the member list
    {
      path: '/members',
      name: 'members',
      component: MemberListView,
    },

    // 2. Add the route for the member creation form
    {
      path: '/members/create',
      name: 'createMember',
      component: MemberCreateView,
    },

    // 3. Add the path for the member details 
    {
      path: '/member/:id', // Dynamic path parameter :id
      name: 'member-details', // New unique name
      component: MemberDetailView,
    },

    // 4.  edit route
    {
      path: '/member/edit/:id', // Note: /member/edit/ followed by the ID
      name: 'member-edit',
      component: MemberEditView,
    },
  ],
})

export default router
