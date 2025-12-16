<script setup>
import { onMounted, computed } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { useMemberStore } from '@/stores/memberStore';

const route = useRoute();
const router = useRouter();
const memberStore = useMemberStore();

// Get the ID from the route parameters (/member/5 -> id = 5)
const memberId = parseInt(route.params.id);

// 1. Fetch the data on component mount
onMounted(() => {
    // Only fetch the data if the ID is valid
    if (memberId) {
        memberStore.fetchMemberById(memberId);
    }
});

// 2. Computed property to easily access the member currently loaded in the store
// this will temporarily hold the currently viewed member
// allows other components (like MemberEditView) to access that same data instantly without having to call the API again
const member = computed(() => memberStore.currentMember);

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
    <div class="member-detail-container">
        <button @click="goBack" class="back-button">← Takaisin Jäseniin</button>

        <div v-if="memberStore.isLoading">Ladataan jäsentietoja... (Loading member data...)</div>
        <div v-else-if="memberStore.error" class="error-message">
            Virhe: {{ memberStore.error }}
        </div>
        
        <div v-else-if="member">
            <h2>👤 Jäsenen Tiedot: {{ member.firstName }} {{ member.lastName }}</h2>
            
            <div class="details-card">
                <p><strong>Jäsen ID (Member ID):</strong> {{ member.memberId }}</p>
                <p><strong>Sähköposti (Email):</strong> {{ member.email }}</p>
                <p><strong>Tila (Status):</strong> <span :class="{'status-active': member.membershipStatus === 'Active', 'status-lapsed': member.membershipStatus === 'Lapsed'}">{{ member.membershipStatus }}</span></p>
                <p><strong>Liittymispäivä (Joining Date):</strong> {{ new Date(member.joiningDate).toLocaleDateString('fi-FI') }}</p>
                
                <div class="actions">
                    <RouterLink :to="`/member/edit/${member.memberId}`" class="action-button edit-button">Muokkaa</RouterLink>
                    <button @click="handleDelete" class="action-button delete-button">Poista</button>
                </div>
            </div>
            
        </div>
        <div v-else>
            <p>Jäsentä ei löytynyt. (Member not found.)</p>
        </div>
    </div>
</template>

<style scoped>
/* Color Palette */
:root {
    --color-primary: #002855; /* Deep Navy Blue */
    --color-accent: #DAA520; /* Goldenrod/Gold Accent */
    --color-danger: #C0392B; /* Deep Red for Delete */
    --color-success: #1E8449; /* Dark Green for Success */
    --color-text-dark: #333333;
}

.member-detail-container {
    max-width: 750px;
    margin: 40px auto;
    padding: 30px;
    background-color: white;
    /* Adopting the list view's container aesthetic: clean edges, soft shadow */
    border-radius: 6px; 
    box-shadow: 0 4px 10px rgba(0, 0, 0, 0.08); 
}

h2 {
    color: var(--color-primary);
    border-bottom: 2px solid var(--color-accent); /* Gold line under title */
    padding-bottom: 15px;
    margin-bottom: 30px;
    font-size: 1.8em;
    text-align: left; /* Aligning titles left, like the list view */
    font-weight: 600;
}

.back-button {
    background: none;
    border: none;
    color: var(--color-primary);
    cursor: pointer;
    margin-bottom: 20px;
    padding: 0;
    font-size: 1em;
    font-weight: 500;
}
.back-button:hover {
    text-decoration: underline;
}

/* --- Detail List (Table Vibe) --- */
.detail-list {
    list-style: none;
    padding: 0;
    margin: 0;
    /* Use the same subtle border style as the list view table */
    border: 1px solid #ddd;
    border-radius: 4px;
    overflow: hidden;
}

.detail-item {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 12px 15px;
    border-bottom: 1px solid #eee; /* Thin separator */
    background-color: #ffffff; /* Default background */
    transition: background-color 0.2s;
}

/* Zebra striping for readability, matching list view */
.detail-item:nth-child(even) {
    background-color: #f9f9f9;
}

.detail-item:last-child {
    border-bottom: none;
}

.label {
    font-weight: 600;
    color: #555;
    width: 40%;
    text-align: left;
    text-transform: uppercase;
    font-size: 0.9em; /* Smaller, like table headers */
}

.value {
    font-weight: 500;
    color: var(--color-text-dark);
    text-align: right;
    width: 60%;
    font-size: 1em;
}

/* Status Badges */
.status-badge {
    padding: 4px 8px;
    border-radius: 3px;
    font-weight: 600;
    font-size: 0.9em;
}
.status-active {
    background-color: #e6f7ee;
    color: var(--color-success); 
}
.status-pending {
    background-color: #fff8e1;
    color: var(--color-accent); 
}
.status-lapsed {
    background-color: #fdecec;
    color: var(--color-danger);
}

/* --- Action Buttons --- */
.actions {
    margin-top: 30px;
    display: flex;
    gap: 15px;
    justify-content: flex-end; /* Aligning buttons to the right */
}

.action-button {
    text-decoration: none;
    padding: 10px 20px;
    border-radius: 5px;
    cursor: pointer;
    font-size: 1em;
    font-weight: 600;
    transition: background-color 0.3s;
    border: none;
}

.edit-button {
    background-color: var(--color-accent); 
    color: var(--color-primary);
}

.edit-button:hover {
    background-color: #e0a800;
}

.delete-button {
    background-color: var(--color-danger); 
    color: white;
}

.delete-button:hover {
    background-color: #A93226;
}

.error-message {
    color: var(--color-danger);
    background-color: #f8d7da;
    border: 1px solid #f5c6cb;
    padding: 15px;
    border-radius: 5px;
    margin-top: 20px;
}
</style>