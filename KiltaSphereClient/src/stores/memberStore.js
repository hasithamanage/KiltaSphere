// File: src/stores/memberStore.js
import { defineStore } from 'pinia';
import http from '@/api/http'; // Import the HTTP client

export const useMemberStore = defineStore('members', {
  state: () => ({
    members: [], 
    isLoading: false,
    error: null,
  }),
  actions: {
    async fetchMembers() {
      this.isLoading = true;
      this.error = null;
      try {
        // Calls the /api/Members endpoint on .NET API
        const response = await http.get('/Members'); 
        this.members = response.data;
      } catch (err) {
        console.error('API Fetch Error:', err);
        this.error = 'Jäsentietojen haku epäonnistui (Failed to fetch member data). Varmista, että API on käynnissä ja CORS on määritetty.';
      } finally {
        this.isLoading = false;
      }
    },
  },
  getters: {
    memberCount: (state) => state.members.length,
  },
});