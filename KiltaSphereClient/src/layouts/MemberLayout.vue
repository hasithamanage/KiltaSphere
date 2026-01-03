<template>
  <div class="member-layout">
    <header class="member-header">
      <div class="container header-flex">
        <div class="brand">👤 KiltaSphere <span class="portal-tag">Jäsenportaali</span></div>
        <nav class="member-nav">
          <RouterLink to="/member/me">Omat Tiedot</RouterLink>
          <RouterLink to="/member/payments">Maksuni</RouterLink>
          <button class="logout-link">Kirjaudu ulos</button>
        </nav>
      </div>
    </header>

    <main class="member-main container">
      <slot />
    </main>
  </div>
</template>

<style scoped>
.member-layout {
  min-height: 100vh;
  background-color: #f8fafc;
}

.member-header {
  background-color: white;
  border-bottom: 1px solid #e2e8f0;
  padding: 1rem 0;
  box-shadow: 0 1px 3px rgba(0,0,0,0.05);
}

.container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 0 20px;
}

.header-flex {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.brand {
  font-weight: 700;
  color: #002855;
  font-size: 1.2rem;
}

.portal-tag {
  font-size: 0.75rem;
  background: #e2e8f0;
  padding: 2px 8px;
  border-radius: 4px;
  margin-left: 8px;
  text-transform: uppercase;
}

.member-nav a {
  margin-left: 20px;
  text-decoration: none;
  color: #64748b;
  font-weight: 500;
}

.member-nav a.router-link-active {
  color: #002855;
  border-bottom: 2px solid #daa520;
}

.logout-link {
  background: none;
  border: none;
  color: #c0392b;
  cursor: pointer;
  margin-left: 20px;
  font-weight: 500;
}

.member-main {
  padding-top: 40px;
  padding-bottom: 40px;
}
</style>