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
    <h2>🇫🇮 Jäsenrekisterin Hallinta (Member Registry Management)</h2>

    <div v-if="memberStore.isLoading">Ladataan jäsentietoja... (Loading member data...)</div>
    <div v-else-if="memberStore.error" class="error-message">
        Virhe: {{ memberStore.error }}
    </div>
    
    <div v-else>
      <p>Yhteensä jäseniä: **{{ memberStore.memberCount }}**</p>

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
              <RouterLink :to="`/member/${member.memberId}`">Katso (View)</RouterLink>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<style scoped>
/* Simple CSS for professionalism and scannability */
.member-list-container {
  padding: 20px;
}
table {
  width: 100%;
  border-collapse: collapse;
}
th, td {
  border: 1px solid #ddd;
  padding: 8px;
  text-align: left;
}
th {
  background-color: #f2f2f2;
}
.error-message {
  color: red;
  font-weight: bold;
  border: 1px solid red;
  padding: 10px;
  background-color: #ffeaea;
}
</style>