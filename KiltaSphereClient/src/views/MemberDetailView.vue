<script setup>
import { onMounted, computed } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { useMemberStore } from '@/stores/memberStore';
import { usePaymentStore } from '@/stores/paymentStore'; // Import the payment store

const route = useRoute();
const router = useRouter();
const memberStore = useMemberStore();
const paymentStore = usePaymentStore(); // Initialize the payment store

const memberId = parseInt(route.params.id); // Get the ID from the route parameters (/member/5 -> id = 5)

// 1. Fetch the data on component mount
onMounted(async() => {
    // Only fetch the data if the ID is valid
    if (memberId) {
        // Fetch from both domains
        await memberStore.fetchMemberById(memberId);
        await paymentStore.fetchPaymentsByMemberId(memberId);
    }
});

// 2. Computed property to easily access the member currently loaded in the store
// this will temporarily hold the currently viewed member
// allows other components (like MemberEditView) to access that same data instantly without having to call the API again
const member = computed(() => memberStore.currentMember);
const payments = computed(() => paymentStore.payments);

// Function to handle the "Mark as Paid" button
const handleMarkAsPaid = async (paymentId) => {
    if (confirm('Haluatko varmasti merkitä tämän maksun suoritetuksi?')) {
        try {
            await paymentStore.markAsPaid(paymentId, memberId);
            alert('Maksu päivitetty!');
        } catch (error) {
            alert('Virhe päivityksessä.');
        }
    }
};

// 3. Function to handle navigation back to the list
const goBack = () => {
    router.push({ name: 'members' });
};

// Delete function
const handleDelete = async () => {

    // 1. Safety check: make sure the data actually exists
    if (!member.value) return;

    // 2. Now it is safe to access member.value.firstName
    if (confirm(`Oletko varma, että haluat poistaa jäsenen ${member.value.firstName} ${member.value.lastName} (ID: ${memberId})?`)) {
        try {
            await memberStore.deleteMember(memberId);
            
            alert('Jäsen poistettu onnistuneesti! (Member deleted successfully!)');
            // Redirect back to the member list after successful deletion
            router.push({ name: 'members' });
            
        } catch (error) {
            // Error handling is done in the store; the error message is already set.
            alert(`Poisto epäonnistui: ${memberStore.error || 'Tuntematon virhe.'}`);
        }
    }
};

</script>

<template>
  <div class="dashboard-wrapper">
    <div class="header-actions">
      <button @click="goBack" class="back-button">← Takaisin Jäseniin</button>
    </div>

    <div v-if="memberStore.isLoading" class="loading-state">Ladataan tietoja...</div>
    <div v-else-if="memberStore.error" class="error-message">Virhe: {{ memberStore.error }}</div>

    <div v-else-if="member" class="dashboard-grid">
      
      <section class="info-column">
        <h2>👤 Jäsenen Tiedot</h2>
        <div class="details-card">
          <div class="detail-list">
            <div class="detail-item">
              <span class="label">ID</span>
              <span class="value">{{ member.memberId }}</span>
            </div>
            <div class="detail-item">
              <span class="label">Nimi</span>
              <span class="value">{{ member.firstName }} {{ member.lastName }}</span>
            </div>
            <div class="detail-item">
              <span class="label">Sähköposti</span>
              <span class="value">{{ member.email }}</span>
            </div>
            <div class="detail-item">
              <span class="label">Status</span>
              <span class="value">
                <span :class="['status-badge', member.membershipStatus.toLowerCase()]">
                  {{ member.membershipStatus }}
                </span>
              </span>
            </div>
            <div class="detail-item">
              <span class="label">Liittynyt</span>
              <span class="value">{{ new Date(member.joiningDate).toLocaleDateString('fi-FI') }}</span>
            </div>
          </div>

          <div class="actions">
            <RouterLink :to="`/member/edit/${member.memberId}`" class="action-button edit-button">Muokkaa</RouterLink>
            <button @click="handleDelete" class="action-button delete-button">Poista</button>
          </div>
        </div>
      </section>

      <section class="payment-column">
        <h2>💳 Maksuseuranta</h2>
        <div class="payments-card">
          <div v-if="paymentStore.isLoading">Ladataan maksuja...</div>
          <div v-else-if="payments.length === 0" class="no-payments">
            Ei maksuhistoriaa tälle jäsenelle.
          </div>

          <table v-else class="payments-table">
            <thead>
              <tr>
                <th>Viite</th>
                <th>Summa</th>
                <th>Eräpäivä</th>
                <th>Tila</th>
                <th>Toiminnot</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="pay in payments" :key="pay.paymentId">
                <td><small>{{ pay.referenceNumber }}</small></td>
                <td>{{ pay.amount }} €</td>
                <td>{{ new Date(pay.dueDate).toLocaleDateString('fi-FI') }}</td>
                <td>
                  <span :class="['status-badge', pay.paymentStatus.toLowerCase()]">
                    {{ pay.paymentStatus }}
                  </span>
                </td>
                <td>
                  <button 
                    v-if="pay.paymentStatus === 'Pending'" 
                    @click="handleMarkAsPaid(pay.paymentId)"
                    class="pay-btn"
                  >
                    Maksa
                  </button>
                  <span v-else class="paid-date">
                    {{ new Date(pay.paidDate).toLocaleDateString('fi-FI') }}
                  </span>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </section>

    </div>

    <div v-else class="error-message">Jäsentä ei löytynyt.</div>
  </div>
</template>

<style scoped>
/* Color Palette & Variables */
.dashboard-wrapper {
  --color-primary: #002855;
  --color-accent: #DAA520;
  --color-danger: #C0392B;
  --color-success: #1E8449;
  --color-bg: #f4f7f9;
  
  padding: 20px;
  background-color: var(--color-bg);
  min-height: 100vh;
}

/* Dashboard Grid Layout */
.dashboard-grid {
  display: grid;
  grid-template-columns: 1fr 2fr; /* Member info is 1/3, Payments is 2/3 width */
  gap: 25px;
  max-width: 1400px;
  margin: 0 auto;
}

@media (max-width: 1100px) {
  .dashboard-grid {
    grid-template-columns: 1fr; /* Stack on smaller screens */
  }
}

/* Content Cards */
.details-card, .payments-card {
  background: white;
  padding: 25px;
  border-radius: 8px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.08);
}

h2 {
  color: var(--color-primary);
  font-size: 1.4em;
  margin-bottom: 15px;
  display: flex;
  align-items: center;
  gap: 10px;
}

/* Detail List Styling */
.detail-list {
  border: 1px solid #eee;
  border-radius: 4px;
}

.detail-item {
  display: flex;
  justify-content: space-between;
  padding: 12px 15px;
  border-bottom: 1px solid #eee;
}

.detail-item:last-child { border-bottom: none; }
.detail-item:nth-child(even) { background-color: #fafafa; }

.label { font-weight: 600; color: #666; font-size: 0.85em; text-transform: uppercase; }
.value { font-weight: 500; color: var(--color-primary); }

/* Table Styling */
.payments-table {
  width: 100%;
  border-collapse: collapse;
}

.payments-table th {
  text-align: left;
  border-bottom: 2px solid var(--color-primary);
  padding: 10px;
  font-size: 0.9em;
  color: #555;
}

.payments-table td {
  padding: 12px 10px;
  border-bottom: 1px solid #eee;
  font-size: 0.95em;
}

/* Status Badges */
.status-badge {
  padding: 4px 8px;
  border-radius: 4px;
  font-size: 0.8em;
  font-weight: 700;
  text-transform: uppercase;
}
.status-badge.active, .status-badge.paid { background: #e6f7ee; color: var(--color-success); }
.status-badge.pending { background: #fff3cd; color: #856404; }
.status-badge.lapsed { background: #fdecec; color: var(--color-danger); }

/* Buttons */
.back-button {
  background: none; border: none; color: var(--color-primary);
  cursor: pointer; font-weight: 600; margin-bottom: 20px;
}

.action-button {
  padding: 8px 16px; border-radius: 4px; border: none;
  font-weight: 600; cursor: pointer; text-decoration: none;
}

.edit-button { background: var(--color-accent); color: var(--color-primary); margin-right: 10px; }
.delete-button { background: var(--color-danger); color: white; }

.pay-btn {
  background: var(--color-success); color: white; border: none;
  padding: 5px 10px; border-radius: 4px; cursor: pointer;
}

.actions { margin-top: 20px; display: flex; justify-content: flex-end; }
</style>