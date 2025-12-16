<script setup>
// No script added for now
</script>

<template>
  <div class="welcome-container">
    <h1>👋 TERVETULOA JÄSENHALLINTAAN</h1>
    <p>Aloita siirtymällä <RouterLink to="/members">Jäsenet-sivulle</RouterLink> nähdäksesi, muokataksesi ja poistaaksesi jäseniä.</p>
    <p>(Start by navigating to the Members page to view, edit and delete members.)</p>
  </div>
</template>

<style scoped>
.welcome-container {
  padding: 40px;
  text-align: center;
  background-color: #f4f4f4;
  border-radius: 8px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  margin-top: 50px;
}
h1 {
  color: #333;
  margin-bottom: 15px;
}
p {
  color: #555;
  font-size: 1.1em;
}
</style>