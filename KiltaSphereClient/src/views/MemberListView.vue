<script setup>
import { onMounted } from 'vue';
import { useMemberStore } from '@/stores/memberStore'; // Import the store

// 1. Initialize the store
const memberStore = useMemberStore();

// 2. Fetch data when the component is first mounted (loaded)
onMounted(() => {
  memberStore.fetchMembers();
});
</script>

<template>
  <div class="member-list-container">
    <h2>Jäsenrekisterin Hallinta</h2>

    <div v-if="memberStore.isLoading">Ladataan jäsentietoja... (Loading member data...)</div>
    <div v-else-if="memberStore.error" class="error-message">
        Virhe: {{ memberStore.error }}
    </div>
    
    <div v-else>
      <p>Yhteensä jäseniä: {{ memberStore.memberCount }}</p>

      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Etunimi</th>
            <th>Sukunimi</th>
            <th>Sähköposti</th>
            <th>Tila</th>
            <th>Toiminnot</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="member in memberStore.members" :key="member.memberId">
            <td>{{ member.memberId }}</td>
            <td>{{ member.firstName }}</td>
            <td>{{ member.lastName }}</td>
            <td>{{ member.email }}</td>
            <td>{{ member.membershipStatus }}</td>
            <td>
              <RouterLink :to="`/member/${member.memberId}`">Katso</RouterLink>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<style scoped>
.member-list-container {
    padding: 20px 0;
}

h2 {
    color: #002855; /* Deep Navy */
    border-bottom: 2px solid #DAA520; /* Gold/Accent border */
    padding-bottom: 10px;
    margin-bottom: 25px;
    font-size: 1.8em;
}

/* --- Table Styling --- */
table {
    width: 100%;
    border-collapse: separate; /* Allows for border-radius on cells */
    border-spacing: 0;
    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.08); /* Soft shadow for depth */
    background-color: white;
    border-radius: 8px; /* Rounded corners for the table */
    overflow: hidden; /* Ensures rounded corners are visible */
}

thead tr {
    background-color: #f0f4f8; 
    color: #333333;
}

th, td {
    padding: 12px 15px;
    text-align: left;
    border-bottom: 1px solid #e0e0e0;
}

th {
    font-weight: 600;
    text-transform: uppercase;
    font-size: 0.9em;
}

/* Zebra striping for readability */
tbody tr:nth-child(even) {
    background-color: #fafafa;
}

tbody tr:hover {
    background-color: #f5f5f5;
    cursor: pointer;
}

/* Last row/cell cleanup */
tbody tr:last-child td {
    border-bottom: none;
}

/* --- Status and Action Link Styling --- */
td:nth-child(5) {
    /* Style for the Status column (column 5) */
    font-weight: bold;
}

/* --- Status and Action Link Styling --- */

/* Action links use the Navy/Gold scheme */
.router-link-active, a {
    color: #002855; /* Deep Navy Link Color */
    text-decoration: none;
    font-weight: 500;
    transition: color 0.3s;
}

a:hover {
    color: #DAA520; /* Gold on Hover */
    text-decoration: underline;
}

.error-message {
    color: #dc3545;
    background-color: #f8d7da;
    border: 1px solid #f5c6cb;
    padding: 15px;
    border-radius: 5px;
    margin-top: 20px;
}

p {
    margin-bottom: 15px;
    color: #555;
    font-size: 1.05em;
}
</style>