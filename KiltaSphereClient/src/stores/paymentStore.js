import { defineStore } from 'pinia';
import http from '@/api/http';

export const usePaymentStore = defineStore('payments', {
  state: () => ({
    payments: [],
    isLoading: false,
    error: null,
  }),
  actions: {
    // Fetches payments for a specific member
    async fetchPaymentsByMemberId(memberId) {
      this.isLoading = true;
      this.error = null;
      try {
        const response = await http.get(`/Payments/member/${memberId}`);
        this.payments = response.data;
      } catch (err) {
        console.error('Error fetching payments:', err);
        this.error = 'Maksujen haku epäonnistui.';
      } finally {
        this.isLoading = false;
      }
    },

    // Marks a specific payment as paid
    async markAsPaid(paymentId, memberId) {
      try {
        await http.post(`/Payments/${paymentId}/pay`);
        // Refresh the list for the member to ensure UI sync
        await this.fetchPaymentsByMemberId(memberId);
      } catch (err) {
        console.error('Error marking payment as paid:', err);
        throw err; // Allow component to handle specific UI feedback
      }
    }
  }
});