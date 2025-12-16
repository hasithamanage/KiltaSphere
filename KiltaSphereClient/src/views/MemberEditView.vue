<script setup>
import { ref, onMounted, computed, watch } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { useMemberStore } from '@/stores/memberStore';

const route = useRoute();
const router = useRouter();
const memberStore = useMemberStore();
const memberId = parseInt(route.params.id);

const formData = ref({ firstName: '', lastName: '', email: '', membershipStatus: '' });
const member = computed(() => memberStore.currentMember);

watch(member, (newMember) => {
    if (newMember) {
        formData.value = { ...newMember };
    }
}, { immediate: true });

// Reset validation errors when editing starts
watch(() => formData.value.firstName, () => { if(memberStore.validationErrors) memberStore.validationErrors.FirstName = null });
watch(() => formData.value.lastName, () => { if(memberStore.validationErrors) memberStore.validationErrors.LastName = null });
watch(() => formData.value.email, () => { if(memberStore.validationErrors) memberStore.validationErrors.Email = null });

onMounted(() => {
    memberStore.clearErrors(); // Reset UI state
    if (memberId && memberId > 0) {
        memberStore.fetchMemberById(memberId);
    }
});

const handleSubmit = async () => {
    try {
        memberStore.validationErrors = null;
        const updatedMember = await memberStore.updateMember(memberId, formData.value);
        if (updatedMember) {
            alert(`Päivitetty!`);
            router.push({ name: 'member-details', params: { id: memberId } });
        }
    } catch (error) { /* Store handles error state */ }
};

const goBack = () => router.push({ name: 'member-details', params: { id: memberId } });
</script>

<template>
    <div class="member-edit-container">
        <button @click="goBack" class="back-button">← Takaisin</button>

        <div v-if="memberStore.isLoading">Ladataan...</div>
        
        <div v-else-if="memberStore.error && !memberStore.validationErrors" class="error-message">
            {{ memberStore.error }}
        </div>
        
        <div v-else-if="member">
            <h2>✍️ Muokkaa Jäsentä</h2>
            
            <form @submit.prevent="handleSubmit" class="edit-form" novalidate>
                <div class="form-group">
                    <label for="firstName">Etunimi:</label>
                    <input type="text" id="firstName" v-model="formData.firstName" 
                           :class="{ 'input-error': memberStore.validationErrors?.FirstName }">
                    <span v-if="memberStore.validationErrors?.FirstName" class="field-error">
                        {{ memberStore.validationErrors.FirstName[0] }}
                    </span>
                </div>

                <div class="form-group">
                    <label for="lastName">Sukunimi:</label>
                    <input type="text" id="lastName" v-model="formData.lastName"
                           :class="{ 'input-error': memberStore.validationErrors?.LastName }">
                    <span v-if="memberStore.validationErrors?.LastName" class="field-error">
                        {{ memberStore.validationErrors.LastName[0] }}
                    </span>
                </div>

                <div class="form-group">
                    <label for="email">Sähköposti:</label>
                    <input type="email" id="email" v-model="formData.email"
                           :class="{ 'input-error': memberStore.validationErrors?.Email }">
                    <span v-if="memberStore.validationErrors?.Email" class="field-error">
                        {{ memberStore.validationErrors.Email[0] }}
                    </span>
                </div>

                <div class="form-group">
                    <label for="membershipStatus">Jäsenyyden Tila:</label>
                    <select id="membershipStatus" v-model="formData.membershipStatus">
                        <option value="Active">Active</option>
                        <option value="Pending">Pending</option>
                        <option value="Lapsed">Lapsed</option>
                    </select>
                </div>
                
                <button type="submit" class="submit-button" :disabled="memberStore.isLoading">Tallenna</button>
            </form>
        </div>
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

.member-edit-container, .member-create-container {
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

.edit-form, .create-form {
    display: grid;
    gap: 20px; /* Space between form groups */
    padding-bottom: 10px;
}

.form-group label {
    display: block;
    margin-bottom: 6px;
    font-weight: 600;
    color: #555;
    font-size: 0.95em;
}

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
    /* Use the dark professional green for submit/save */
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

/* Back Button (for Edit View) */
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

.error-message {
    color: #C0392B;
    background-color: #f8d7da;
    border: 1px solid #f5c6cb;
    padding: 15px;
    border-radius: 5px;
    margin-top: 20px;
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