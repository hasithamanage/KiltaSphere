<script setup>
import { reactive, watch } from 'vue';
import { useMemberStore } from '@/stores/memberStore';
import { useRouter } from 'vue-router'; 

const memberStore = useMemberStore();
const router = useRouter();

const memberForm = reactive({
  firstName: '',
  lastName: '',
  email: '',
  membershipStatus: 'Active',
});

// Clear validation errors when user types in a field
watch(() => memberForm.firstName, () => { if(memberStore.validationErrors) memberStore.validationErrors.FirstName = null });
watch(() => memberForm.lastName, () => { if(memberStore.validationErrors) memberStore.validationErrors.LastName = null });
watch(() => memberForm.email, () => { if(memberStore.validationErrors) memberStore.validationErrors.Email = null });

const handleSubmit = async () => {
  memberStore.clearErrors(); // Use the clear error helper from memberStore

  try {
    const newMember = await memberStore.createMember(memberForm);
    alert(`Jäsen ${newMember.firstName} ${newMember.lastName} luotu!`); 
    router.push({ name: 'members' });
  } catch (err) {
    // Errors handled by store
  }
};
</script>

<template>
  <div class="member-create-container">
    <h2>➕ Luo Uusi Jäsen</h2>

    <div v-if="memberStore.error && !memberStore.validationErrors" class="error-message">
        {{ memberStore.error }}
    </div>

    <form @submit.prevent="handleSubmit" class="create-form" novalidate> 
      
      <div class="form-group">
        <label for="firstName">Etunimi:</label>
        <input id="firstName" v-model="memberForm.firstName" type="text" 
               :class="{ 'input-error': memberStore.validationErrors?.FirstName }">
        <span v-if="memberStore.validationErrors?.FirstName" class="field-error">
          {{ memberStore.validationErrors.FirstName[0] }}
        </span>
      </div>
      
      <div class="form-group">
        <label for="lastName">Sukunimi:</label>
        <input id="lastName" v-model="memberForm.lastName" type="text"
               :class="{ 'input-error': memberStore.validationErrors?.LastName }">
        <span v-if="memberStore.validationErrors?.LastName" class="field-error">
          {{ memberStore.validationErrors.LastName[0] }}
        </span>
      </div>
      
      <div class="form-group">
        <label for="email">Sähköposti:</label>
        <input id="email" v-model="memberForm.email" type="email"
               :class="{ 'input-error': memberStore.validationErrors?.Email }">
        <span v-if="memberStore.validationErrors?.Email" class="field-error">
          {{ memberStore.validationErrors.Email[0] }}
        </span>
      </div>
      
      <div class="form-group">
        <label for="membershipStatus">Jäsenyyden Tila:</label>
        <select id="membershipStatus" v-model="memberForm.membershipStatus">
          <option value="Active">Aktiivinen</option>
          <option value="Pending">Odottaa</option>
          <option value="Lapsed">Vanhentunut</option>
        </select>
      </div>

      <button type="submit" :disabled="memberStore.isLoading" class="submit-button">
        {{ memberStore.isLoading ? 'Luodaan...' : 'Luo Jäsen' }}
      </button>
    </form>
  </div>
</template>

<style scoped>
/* Color Palette */
:root {
    --color-primary: #002855; /* Deep Navy Blue */
    --color-accent: #DAA520; /* Goldenrod/Gold Accent */
    --color-success: #1E8449; /* Dark Green for Submit */
    --color-text-dark: #333333;
}

.member-create-container {
    max-width: 650px;
    margin: 40px auto;
    padding: 30px;
    background-color: white;
    /* Consistent card style with List/Detail views */
    border-radius: 6px; 
    box-shadow: 0 4px 10px rgba(0, 0, 0, 0.08);
}

h2 {
    color: var(--color-primary);
    border-bottom: 2px solid var(--color-accent); /* Gold line under title */
    padding-bottom: 15px;
    margin-bottom: 30px;
    font-size: 1.8em;
    font-weight: 600;
}

/* Selector for the form wrapper */
.create-form {
    display: grid;
    gap: 20px; /* Space between form groups */
    padding-bottom: 10px;
}

/* Selector for the input wrapper */
.form-group label {
    display: block;
    margin-bottom: 6px;
    font-weight: 600;
    color: #555;
    font-size: 0.95em;
}

/* Selector for the input/select element itself */
.form-group input, .form-group select {
    width: 100%;
    padding: 12px;
    border: 1px solid #ddd; /* Light gray border for inputs */
    border-radius: 4px;
    box-sizing: border-box; 
    transition: border-color 0.2s, box-shadow 0.2s;
    font-size: 1em;
    color: var(--color-text-dark);
}

.form-group input:focus, .form-group select:focus {
    border-color: var(--color-primary); /* Navy border on focus */
    box-shadow: 0 0 0 3px rgba(0, 40, 85, 0.1); /* Subtle navy shadow */
    outline: none;
    background-color: #faffff;
}

/* --- Buttons --- */
.submit-button {
    background-color: var(--color-success);
    color: white;
    padding: 12px 25px;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    font-size: 1.05em;
    font-weight: 600;
    transition: background-color 0.3s;
    margin-top: 20px;
}

.submit-button:hover {
    background-color: #145A32;
}

/* Error Message */
.error-message {
    color: #C0392B;
    background-color: #f8d7da;
    border: 1px solid #f5c6cb;
    padding: 15px;
    border-radius: 5px;
    margin-bottom: 20px; /* Adjusted margin */
    font-weight: 500;
    text-align: center;
}

.field-error {
    color: #C0392B;
    font-size: 0.85em;
    margin-top: 5px;
    font-weight: 500;
    display: block;
}

.input-error {
    border-color: #C0392B !important;
    background-color: #fff8f8;
}

</style>