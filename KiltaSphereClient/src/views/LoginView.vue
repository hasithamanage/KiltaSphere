<script setup>
import { ref } from 'vue';
import { useAuthStore } from '@/stores/authStore';

const authStore = useAuthStore();

const credentials = ref({
    username: '',
    password: ''
});

const handleLogin = async () => {
    try {
        await authStore.login(credentials.value);
    } catch (error) {
        // for now Error handling is managed in the store, 
        // later Add specific UI feedback here if needed.
    }
};
</script>

<template>
  <div class="login-container">
    <div class="login-header">
      <h2>Kirjaudu Sisään</h2>
      <p>KiltaSphere Hallintapaneeli</p>
    </div>

    <form @submit.prevent="handleLogin" class="login-form">
      <div class="form-group">
        <label>Käyttäjätunnus</label>
        <input 
          v-model="credentials.username" 
          type="text" 
          placeholder="esim. matti.meikäläinen" 
          required 
        />
      </div>

      <div class="form-group">
        <label>Salasana</label>
        <input 
          v-model="credentials.password" 
          type="password" 
          placeholder="••••••••" 
          required 
        />
      </div>

      <div v-if="authStore.error" class="error-box">
        {{ authStore.error }}
      </div>

      <button type="submit" class="login-button" :disabled="authStore.isLoading">
        {{ authStore.isLoading ? 'Kirjaudutaan...' : 'Kirjaudu' }}
      </button>
    </form>

    <div class="login-footer">
      <a href="#">Unohtuiko salasana?</a>
    </div>
  </div>
</template>

<style scoped>
.login-header {
  text-align: center;
  margin-bottom: 30px;
}

.login-header h2 {
  color: #002855;
  margin: 0;
}

.login-header p {
  color: #666;
  font-size: 0.9rem;
}

.form-group {
  margin-bottom: 20px;
}

label {
  display: block;
  margin-bottom: 8px;
  font-weight: 600;
  font-size: 0.85rem;
  color: #444;
}

input {
  width: 100%;
  padding: 12px;
  border: 1px solid #ddd;
  border-radius: 4px;
  font-size: 1rem;
}

.login-button {
  width: 100%;
  padding: 12px;
  background-color: #002855;
  color: white;
  border: none;
  border-radius: 4px;
  font-weight: 700;
  cursor: pointer;
  transition: background 0.3s;
}

.login-button:hover {
  background-color: #001a35;
}

.login-button:disabled {
  opacity: 0.7;
  cursor: not-allowed;
}

.error-box {
  background-color: #fdecec;
  color: #c0392b;
  padding: 10px;
  border-radius: 4px;
  font-size: 0.85rem;
  margin-bottom: 20px;
  text-align: center;
}

.login-footer {
  margin-top: 20px;
  text-align: center;
}

.login-footer a {
  font-size: 0.8rem;
  color: #002855;
  text-decoration: none;
}
</style>