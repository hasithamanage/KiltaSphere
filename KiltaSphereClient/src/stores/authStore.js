import { defineStore } from 'pinia';
import http from '@/api/http'; // axios instance location
import router from '@/router';

export const useAuthStore = defineStore('auth', {
  state: () => ({
    user: JSON.parse(localStorage.getItem('kilta_user')) || null,
    token: localStorage.getItem('kilta_token') || null,
    isLoading: false,
    error: null,
  }),

  getters: {
    isAuthenticated: (state) => !!state.token,
    isAdmin: (state) => state.user?.role === 'Admin',
    isMember: (state) => state.user?.role === 'Member',
  },

  actions: {
    async login(credentials) {
      this.isLoading = true;
      this.error = null;
      try {
        // backend endpoint 
        const response = await http.post('/account/login', credentials);
        
        const { token, user } = response.data;

        // Save to state
        this.token = token;
        this.user = user;

        // Persist to localStorage so refresh doesn't log them out
        localStorage.setItem('kilta_token', token);
        localStorage.setItem('kilta_user', JSON.stringify(user));

        // Redirect based on role
        if (this.isAdmin) {
          router.push({ name: 'home' });
        } else {
          router.push({ name: 'member-portal' });
        }
      } catch (err) {
        this.error = 'Kirjautuminen epäonnistui. Tarkista tunnus ja salasana.';
        throw err;
      } finally {
        this.isLoading = false;
      }
    },

    logout() {
      this.user = null;
      this.token = null;
      localStorage.removeItem('kilta_token');
      localStorage.removeItem('kilta_user');
      router.push({ name: 'login' });
    }
  }
});