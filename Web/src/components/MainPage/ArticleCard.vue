<script setup lang="ts">
const props = defineProps({
  title: String,
  coverUrl: String,
  brief: String
})
</script>

<template>
  <v-container style="display: flex; flex-direction: row">
    <v-img alt="test" :src="props.coverUrl" width="150px" height="150px" />
    <v-container
      style="display: flex; flex-direction: column; max-height: 150px"
      id="article-container"
    >
      <h3>{{ props.title }}</h3>
      <p>{{ props.brief }}</p>
    </v-container>
  </v-container>
</template>

<style scoped>
#article-container p {
  overflow: hidden;
  text-overflow: ellipsis;
}
</style>
