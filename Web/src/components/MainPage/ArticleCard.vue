<script setup lang="ts">
const props = defineProps({
  title: String,
  coverUrl: String,
  brief: String
})
</script>

<template>
  <v-container style="display: flex; flex-direction: row">
    <v-img
      alt="test"
      src="https://cdn-icons-png.flaticon.com/512/1040/1040243.png"
      width="150px"
      height="150px"
    />
    <v-container style="display: flex; flex-direction: column">
      <h3>{{ props.title }}</h3>
      <p>{{ props.brief }}</p>
    </v-container>
  </v-container>
</template>

<style scoped></style>
