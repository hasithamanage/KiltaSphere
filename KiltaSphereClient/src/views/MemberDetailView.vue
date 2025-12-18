<script setup>
import { onMounted, computed, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { useMemberStore } from '@/stores/memberStore'; // Import the member store
import { usePaymentStore } from '@/stores/paymentStore'; // Import the payment store
import { useCommunicationStore } from '@/stores/communicationStore'; // Import the communication store

const route = useRoute();
const router = useRouter();
const memberStore = useMemberStore(); // Initialize the member store
const paymentStore = usePaymentStore(); // Initialize the payment store
const commStore = useCommunicationStore(); // Initialize the communication store

const memberId = parseInt(route.params.id); // Get the ID from the route parameters (/member/5 -> id = 5)

// State for the Log form
const newLog = ref({
    communicationType: 'Sähköposti',
    subjectSummary: ''
});

// 1. Fetch the data on component mount
onMounted(async () => {
  // Only fetch the data if the ID is valid
  if (memberId) {
    // Fetch from all 3 domains
        await memberStore.fetchMemberById(memberId);
        await paymentStore.fetchPaymentsByMemberId(memberId);
        await commStore.fetchLogsByMemberId(memberId); 
    }
});

// 2. Computed property to easily access the member currently loaded in the store
// this will temporarily hold the currently viewed member
// allows other components (like MemberEditView) to access that same data instantly without having to call the API again
const member = computed(() => memberStore.currentMember);
const payments = computed(() => paymentStore.payments);
const logs = computed(() => commStore.logs);

// Logic to add a new communication log
const handleAddLog = async () => {
    if (!newLog.value.subjectSummary) return;
    
    try {
        await commStore.addLog({
            memberId: memberId,
            ...newLog.value
        });
        newLog.value.subjectSummary = ''; // Reset form
    } catch (error) {
        alert('Lokin tallennus epäonnistui.');
    }
};

// Function to handle the Mark as Paid button
const handleMarkAsPaid = async (paymentId) => {
    if (confirm('Haluatko varmasti merkitä tämän maksun suoritetuksi?')) {
        await paymentStore.markAsPaid(paymentId, memberId);
    }
};

// 3. Function to handle navigation back to the list
const goBack = () => router.push({ name: 'members' });

// Delete function
const handleDelete = async () => {
    if (confirm(`Haluatko varmasti poistaa jäsenen ${member.value.firstName}?`)) {
        await memberStore.deleteMember(memberId);
        router.push({ name: 'members' });
    }
};
</script>

<template>
  <div class="dashboard-wrapper">
    <div class="header-actions">
      <button @click="goBack" class="back-button">← Takaisin Jäseniin</button>
    </div>

    <div v-if="memberStore.isLoading" class="loading-state">Ladataan tietoja...</div>

    <div v-else-if="member" class="dashboard-grid">
      
      <section class="info-column">
        <h2>👤 Jäsenen Tiedot</h2>
        <div class="card details-card">
          <div class="detail-list">
            <div class="detail-item"><span class="label">ID</span><span class="value">{{ member.memberId }}</span></div>
            <div class="detail-item"><span class="label">Nimi</span><span class="value">{{ member.firstName }} {{ member.lastName }}</span></div>
            <div class="detail-item"><span class="label">Sähköposti</span><span class="value">{{ member.email }}</span></div>
            <div class="detail-item">
              <span class="label">Status</span>
              <span :class="['status-badge', member.membershipStatus.toLowerCase()]">{{ member.membershipStatus }}</span>
            </div>
          </div>
          <div class="actions">
            <RouterLink :to="`/member/edit/${member.memberId}`" class="action-button edit-button">Muokkaa</RouterLink>
            <button @click="handleDelete" class="action-button delete-button">Poista</button>
          </div>
        </div>
      </section>

      <section class="activity-column">
        
        <div class="section-group">
            <h2>💳 Maksuseuranta</h2>
            <div class="card">
                <table v-if="payments.length > 0" class="activity-table">
                    <thead>
                        <tr><th>Viite</th><th>Summa</th><th>Eräpäivä</th><th>Tila</th><th></th></tr>
                    </thead>
                    <tbody>
                        <tr v-for="pay in payments" :key="pay.paymentId">
                            <td><small>{{ pay.referenceNumber }}</small></td>
                            <td>{{ pay.amount }} €</td>
                            <td>{{ new Date(pay.dueDate).toLocaleDateString('fi-FI') }}</td>
                            <td><span :class="['status-badge', pay.paymentStatus.toLowerCase()]">{{ pay.paymentStatus }}</span></td>
                            <td>
                                <button v-if="pay.paymentStatus === 'Pending'" @click="handleMarkAsPaid(pay.paymentId)" class="pay-btn">Maksa</button>
                                <span v-else class="paid-date"> {{ new Date(pay.paidDate).toLocaleDateString('fi-FI') }}</span>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <p v-else class="empty-text">Ei maksuhistoriaa.</p>
            </div>
        </div>

        <div class="section-group">
            <h2>📜Viestintälokitus</h2>
            <div class="card">
                <div class="log-form">
                    <select v-model="newLog.communicationType">
                        <option>Sähköposti</option>
                        <option>Puhelu</option>
                        <option>Kirje</option>
                        <option>Järjestelmä</option>
                    </select>
                    <input v-model="newLog.subjectSummary" placeholder="Mitä viestittiin?" @keyup.enter="handleAddLog" />
                    <button @click="handleAddLog" :disabled="!newLog.subjectSummary" class="add-log-btn">Lisää merkintä</button>
                </div>

                <div class="log-timeline">
                    <div v-if="commStore.isLoading">Ladataan lokia...</div>
                    <div v-else-if="logs.length === 0" class="empty-text">Ei aiempia merkintöjä.</div>
                    <div v-for="log in logs" :key="log.logId" class="log-item">
                        <div class="log-meta">
                            <span class="log-date">{{ new Date(log.logDate).toLocaleString('fi-FI', { dateStyle: 'short', timeStyle: 'short' }) }}</span>
                            <span class="log-type">{{ log.communicationType }}</span>
                        </div>
                        <div class="log-content">{{ log.subjectSummary }}</div>
                    </div>
                </div>
            </div>
        </div>

      </section>
    </div>
  </div>
</template>

<style scoped>
.dashboard-wrapper {
  --color-primary: #002855;
  --color-accent: #DAA520;
  --color-success: #1E8449;
  --color-bg: #f4f7f9;
  padding: 20px;
  background-color: var(--color-bg);
  min-height: 100vh;
}

.dashboard-grid {
  display: grid;
  grid-template-columns: 1fr 2.5fr;
  gap: 30px;
  max-width: 1600px;
  margin: 0 auto;
}

.card {
  background: white;
  padding: 20px;
  border-radius: 8px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.08);
  margin-bottom: 20px;
}

.section-group { margin-bottom: 40px; }

h2 { color: var(--color-primary); font-size: 1.3em; margin-bottom: 15px; }

/* Log Form Styling */
.log-form {
    display: flex;
    gap: 10px;
    margin-bottom: 20px;
    padding-bottom: 20px;
    border-bottom: 1px dashed #ccc;
}
.log-form select { padding: 8px; border-radius: 4px; border: 1px solid #ddd; }
.log-form input { flex-grow: 1; padding: 8px; border-radius: 4px; border: 1px solid #ddd; }
.add-log-btn { background: var(--color-primary); color: white; border: none; padding: 8px 15px; border-radius: 4px; cursor: pointer; }
.add-log-btn:disabled { opacity: 0.5; }

/* Timeline Styling */
.log-timeline { max-height: 400px; overflow-y: auto; }
.log-item {
    padding: 12px;
    border-left: 3px solid var(--color-accent);
    background: #fdfdfd;
    margin-bottom: 10px;
}
.log-meta { display: flex; justify-content: space-between; font-size: 0.85em; color: #666; margin-bottom: 5px; }
.log-type { font-weight: bold; color: var(--color-primary); }
.log-content { color: #333; font-size: 0.95em; }

/* Existing Styles */
.detail-list { border: 1px solid #eee; border-radius: 4px; }
.detail-item { display: flex; justify-content: space-between; padding: 12px; border-bottom: 1px solid #eee; }
.label { font-weight: 600; color: #666; font-size: 0.85em; }
.activity-table { width: 100%; border-collapse: collapse; }
.activity-table th { text-align: left; border-bottom: 2px solid #eee; padding: 10px; font-size: 0.9em; }
.activity-table td { padding: 10px; border-bottom: 1px solid #eee; }
.status-badge { padding: 4px 8px; border-radius: 4px; font-size: 0.8em; font-weight: 700; }
.status-badge.paid { background: #e6f7ee; color: var(--color-success); }
.status-badge.pending { background: #fff3cd; color: #856404; }
.pay-btn { background: var(--color-success); color: white; border: none; padding: 5px 10px; border-radius: 4px; cursor: pointer; }
.empty-text { color: #999; font-style: italic; }
.action-button { padding: 8px 16px; border-radius: 4px; border: none; font-weight: 600; cursor: pointer; text-decoration: none; }
.edit-button { background: var(--color-accent); color: white; margin-right: 10px; }
.delete-button { background: #C0392B; color: white; }
.back-button { background: none; border: none; color: var(--color-primary); cursor: pointer; font-weight: bold; margin-bottom: 20px; }
</style>