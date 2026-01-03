<template>
  <div class="admin-layout">
    <aside class="sidebar">
      <div class="sidebar-brand">KiltaSphere</div>
      <nav class="sidebar-nav">
        <RouterLink to="/members">Jäsenet</RouterLink>
      </nav>
    </aside>
    
    <div class="main-container">
      <header class="admin-header">
        <span>Admin Dashboard</span>
      </header>
      <main class="admin-content">
        <slot /> </main>
    </div>
  </div>
</template>

<style scoped>
.admin-layout { display: flex; min-height: 100vh; }
.sidebar { width: 250px; background: #001a35; color: white; }
.main-container { flex-grow: 1; display: flex; flex-direction: column; }
.admin-header { height: 60px; background: white; border-bottom: 1px solid #eee; }
.admin-content { padding: 30px; }
</style>