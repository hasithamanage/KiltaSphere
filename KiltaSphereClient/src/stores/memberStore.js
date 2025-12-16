import { defineStore } from 'pinia';
import http from '@/api/http'; // Import the HTTP client

export const useMemberStore = defineStore('members', {
  state: () => ({
    members: [],
    isLoading: false,
    error: null,
    currentMember: null, // State to hold the member currently being viewed/edited
    validationErrors: null,
  }),
  actions: {
    // Get members via API
    async fetchMembers() {
      this.isLoading = true;
      this.error = null;
      try {
        // API Call: calls the /api/Members endpoint on .NET API
        const response = await http.get('/Members');
        // State Update: Overwrite the members array with the fetched list
        this.members = response.data;
      } catch (err) {
        console.error('API Fetch Error:', err.response || err);
        this.error = 'Jäsentietojen haku epäonnistui (Failed to fetch member data). Varmista, että API on käynnissä ja CORS on määritetty.';
      } finally {
        this.isLoading = false;
      }
    },

    // Create a new member via API
    async createMember(memberData) {
      this.isLoading = true;
      this.error = null;
      try {
        // HTTP POST request to /api/Members
        const response = await http.post('/Members', memberData);

        // Optionally update the local state with the newly created member 
        this.members.push(response.data);

        // Return the newly created member data for the calling view (MemberCreateView)
        return response.data;

      } catch (err) {
        console.error('API Error:', err.response || err);
        if (err.response && err.response.status === 400 && err.response.data.errors) {
          this.validationErrors = err.response.data.errors;
          this.error = 'Tarkista lomakkeen tiedot.';
        } else {
          this.error = 'Toiminto epäonnistui.';
        }
        throw err;
      } finally {
        this.isLoading = false;
      }
    },

    // Action to fetch single member
    async fetchMemberById(id) {
      this.isLoading = true;
      this.error = null;
      this.currentMember = null; // Clear previous member data

      try {
        // API Call: GET /api/Members/{id}
        const response = await http.get(`/Members/${id}`);

        // State Update: Store the single member entity
        this.currentMember = response.data;
        return response.data;

      } catch (err) {
        console.error(`API Fetch Detail Error for ID ${id}:`, err.response || err);

        // Set a specific error message for 404 Not Found
        if (err.response && err.response.status === 404) {
          this.error = `Jäsentä ID:llä ${id} ei löytynyt. (Member with ID ${id} not found.)`;
        } else {
          this.error = 'Jäsentietojen haku epäonnistui. (Failed to fetch member details.)';
        }

        // Re throw to allow the calling component to handle navigation/alerting if needed
        throw err;
      } finally {
        this.isLoading = false;
      }
    },

    // Action to update a member
    async updateMember(id, memberUpdateData) {
      this.isLoading = true;
      this.error = null;
      try {
        // API Call: PUT /api/Members/{id}
        const response = await http.put(`/Members/${id}`, memberUpdateData);

        // If successful, update the currentMember state with the returned DTO
        this.currentMember = response.data;

        // Optionally update the member list array (members) if it's currently loaded
        const index = this.members.findIndex(m => m.memberId === id);
        if (index !== -1) {
          this.members[index] = response.data;
        }

        return response.data; // Return the updated member object
      } catch (err) {
        console.error('API Error:', err.response || err);
        if (err.response && err.response.status === 400 && err.response.data.errors) {
          this.validationErrors = err.response.data.errors;
          this.error = 'Tarkista lomakkeen tiedot.';
        } else {
          this.error = 'Toiminto epäonnistui.';
        }
        throw err;
      } finally {
        this.isLoading = false;
      }
    },

    // Action to delete a member
    async deleteMember(id) {
      this.isLoading = true;
      this.error = null;
      try {
        // API Call: DELETE /api/Members/{id} (Expects 204 No Content)
        await http.delete(`/Members/${id}`);

        // State cleanup 1: Remove the deleted member from the list
        this.members = this.members.filter(m => m.memberId !== id);

        // State cleanup 2: Clear the currentMember state if it was the one deleted
        if (this.currentMember && this.currentMember.memberId === id) {
          this.currentMember = null;
        }

        // Return true to indicate success (no data is returned from 204)
        return true;
      } catch (err) {
        console.error(`API Delete Error for ID ${id}:`, err.response || err);
        this.error = 'Jäsenen poistaminen epäonnistui. (Failed to delete member.)';
        throw err;
      } finally {
        this.isLoading = false;
      }
    },

    // Helper to clear errors
    clearErrors() {
      this.error = null;
      this.validationErrors = null;
    },
  },
  getters: {
    memberCount: (state) => state.members.length,
  },
});