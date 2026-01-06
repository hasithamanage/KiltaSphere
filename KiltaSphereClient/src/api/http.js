import axios from 'axios';
import { useAuthStore } from '@/stores/authStore';

const http = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL || 'https://localhost:7282/api',
});

// Request Interceptor: Attach token to every request
http.interceptors.request.use((config) => {
  const authStore = useAuthStore();
  if (authStore.token) {
    config.headers.Authorization = `Bearer ${authStore.token}`;
  }
  return config;
}, (error) => {
  return Promise.reject(error);
});

// Response Interceptor: Handle expired tokens (401 Unauthorized)
http.interceptors.response.use((response) => response, (error) => {
  const authStore = useAuthStore();
  if (error.response && error.response.status === 401) {
    // If the token is invalid or expired, force logout
    authStore.logout();
  }
  return Promise.reject(error);
});

export default http;