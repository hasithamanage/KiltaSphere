<script setup>
import { useAuthStore } from '@/stores/authStore';
const authStore = useAuthStore();

const handleLogout = () => {
  if(confirm('Haluatko varmasti kirjautua ulos?')) {
    authStore.logout();
  }
};
</script>

<template>
  <div class="admin-wrapper">
    <aside class="sidebar">
      <div class="sidebar-logo">
        👤 KiltaSphere
      </div>
      <nav class="sidebar-nav">
        <RouterLink to="/" class="nav-item">🏠 Kojelauta</RouterLink>
        <RouterLink to="/members" class="nav-item">👥 Jäsenluettelo</RouterLink>
        
        <template v-if="authStore.isAdmin">
          <div class="nav-divider">Toiminnot</div>
          <RouterLink to="/members/create" class="nav-item btn-accent">+ Lisää Jäsen</RouterLink>
        </template>
      </nav>
      <div class="sidebar-footer">
        v1.0.0-beta
      </div>
    </aside>

    <div class="main-canvas">
      <header class="top-bar">
        <div class="breadcrumb">Ylläpito / {{ $route.name }}</div>
        <div class="user-profile">
          <span class="role-badge">{{ authStore.user?.role }}</span>
          <strong>{{ authStore.user?.username }}</strong>
          <button @click="handleLogout" class="logout-btn">Kirjaudu ulos</button>
        </div>
      </header>

      <main class="page-content">
        <slot />
      </main>
    </div>
  </div>
</template>

<style scoped>
.admin-wrapper {
  display: flex;
  height: 100vh;
  background-color: #f4f7f9;
}

/* Sidebar Styling */
.sidebar {
  width: 260px;
  background-color: #002855; /* Deep Navy */
  color: white;
  display: flex;
  flex-direction: column;
  box-shadow: 2px 0 10px rgba(0,0,0,0.1);
}

.sidebar-logo {
  padding: 25px;
  font-size: 1.4rem;
  font-weight: 700;
  border-bottom: 1px solid rgba(255,255,255,0.1);
}

.sidebar-nav {
  flex-grow: 1;
  padding: 20px 0;
}

.nav-item {
  display: block;
  padding: 12px 25px;
  color: #bdc3c7;
  text-decoration: none;
  transition: all 0.3s;
  font-weight: 500;
}

.nav-item:hover, .router-link-active {
  color: white;
  background: rgba(255,255,255,0.05);
  border-left: 4px solid #DAA520; /* Gold Accent */
}

.nav-divider {
  padding: 20px 25px 10px;
  font-size: 0.75rem;
  text-transform: uppercase;
  color: #7f8c8d;
  letter-spacing: 1px;
}

.btn-accent {
  color: #002855 !important;
  background-color: #DAA520;
  margin: 10px 20px;
  border-radius: 4px;
  text-align: center;
}

/* Main Content Area */
.main-canvas {
  flex-grow: 1;
  display: flex;
  flex-direction: column;
  overflow: hidden;
}

.top-bar {
  height: 65px;
  background: white;
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 0 30px;
  border-bottom: 1px solid #e0e6ed;
}

.page-content {
  flex-grow: 1;
  overflow-y: auto;
  padding: 30px;
}

.role-badge {
  background: #e1f0ff;
  color: #0056b3;
  padding: 2px 8px;
  border-radius: 4px;
  font-size: 0.7rem;
  margin-right: 10px;
}

.logout-btn {
  margin-left: 15px;
  background: none;
  border: 1px solid #e0e6ed;
  padding: 5px 10px;
  border-radius: 4px;
  color: #c0392b;
  cursor: pointer;
  font-size: 0.8rem;
  font-weight: 600;
  transition: all 0.2s;
}

.logout-btn:hover {
  background-color: #fdecec;
  border-color: #f5c6cb;
}
</style>