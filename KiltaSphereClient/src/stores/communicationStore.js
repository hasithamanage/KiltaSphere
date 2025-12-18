import { defineStore } from 'pinia';
import http from '@/api/http';

export const useCommunicationStore = defineStore('communications', {
  state: () => ({
    logs: [],
    isLoading: false,
    error: null,
  }),
  actions: {
    async fetchLogsByMemberId(memberId) {
      this.isLoading = true;
      try {
        const response = await http.get(`/Communications/member/${memberId}`);
        this.logs = response.data;
      } catch (err) {
        this.error = 'Lokitietojen haku epäonnistui.';
      } finally {
        this.isLoading = false;
      }
    },

    async addLog(logData) {
      try {
        const response = await http.post('/Communications', logData);
        // Add the new log to the top of the list immediately
        this.logs.unshift(response.data);
      } catch (err) {
        console.error('Error adding log:', err);
        throw err;
      }
    }
  }
});