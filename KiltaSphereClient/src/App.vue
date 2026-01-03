<script setup>
import { computed } from 'vue';
import { useRoute } from 'vue-router';
import AdminLayout from '@/layouts/AdminLayout.vue';
import AuthLayout from '@/layouts/AuthLayout.vue';
import MemberLayout from '@/layouts/MemberLayout.vue';

const route = useRoute();

// Map layout names to components
const layouts = {
  admin: AdminLayout,
  auth: AuthLayout,
  member: MemberLayout
};

// Default to 'admin' if no layout is specified in the route meta
const layout = computed(() => layouts[route.meta.layout] || AdminLayout);
</script>

<template>
  <component :is="layout">
    <RouterView />
  </component>
</template>