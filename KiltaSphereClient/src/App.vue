<script setup>
import { RouterView, RouterLink } from 'vue-router'
</script>

<template>
  <div id="app-wrapper">
    
    <header class="app-header">
      <div class="logo-container">
        <RouterLink to="/" class="site-title">
          👤 KiltaSphere
        </RouterLink>
      </div>
      
      <nav class="main-nav">
        <RouterLink to="/">Koti</RouterLink>
        <RouterLink to="/members">Jäsenet</RouterLink>
        <RouterLink to="/members/create" class="nav-button">+ Lisää Jäsen</RouterLink>
      </nav>
    </header>

    <main class="app-main-content">
      <RouterView />
    </main>

  </div>
</template>

<style>
/* --- Professional Colors --- */
/* Variables for consistency */
:root {
    --color-primary: #002855; /* Deep Navy Blue */
    --color-accent: #DAA520; /* Goldenrod/Gold Accent */
    --color-success: #1E8449; /* Darker Green for Success */
    --color-danger: #C0392B; /* Deep Red for Danger/Delete */
    --color-text: #333333; /* Dark Charcoal Text */
    --color-background-light: #f9f9f9;
}

/* Base Styles */
body {
    margin: 0;
    font-family: 'Arial', sans-serif;
    background-color: var(--color-background-light);
    color: var(--color-text);
}
/* ... (existing #app-wrapper, body, etc. are fine) ... */

/* Header Styles */
.app-header {
    background-color: var(--color-primary); /* Use Navy */
    color: white;
    padding: 15px 30px;
    display: flex;
    justify-content: space-between;
    align-items: center;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

.logo-container .site-title {
    font-size: 1.6em;
    font-weight: 700;
    text-decoration: none;
    color: white;
}

/* Navigation Styles */
.main-nav a {
    color: white;
    text-decoration: none;
    margin-left: 30px;
    font-size: 1.05em;
    padding: 5px 0;
    transition: color 0.3s;
}

.main-nav a:hover {
    color: var(--color-accent); /* Gold on Hover */
}

.main-nav .nav-button {
    background-color: var(--color-accent); /* Gold button for Create */
    color: var(--color-primary); /* Navy text on Gold button */
    padding: 8px 18px;
    border-radius: 4px;
    font-weight: 600;
    margin-left: 35px;
    transition: background-color 0.3s;
}

.main-nav .nav-button:hover {
    background-color: #ffc107; /* Slightly lighter Gold on hover */
    color: var(--color-primary);
}

/* Main Content Styles */
.app-main-content {
    flex-grow: 1; 
    padding: 30px; /* Increased padding */
    max-width: 1200px;
    width: 100%;
    margin: 0 auto;
}
</style>