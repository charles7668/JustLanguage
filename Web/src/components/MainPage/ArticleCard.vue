<script setup lang="ts">
import { ArticleInfo } from '@/Models/ArticleInfo'

defineProps({
  item: {
    type: Object,
    default() {
      return new ArticleInfo()
    }
  }
})
</script>

<template>
  <v-container style="display: flex; flex-direction: row">
    <v-img alt="test" :src="item.coverImageBase64" width="150px" height="150px" />
    <v-container
      style="display: flex; flex-direction: column; max-height: 150px"
      id="article-container"
    >
      <h3>{{ item.title }}</h3>
      <p>{{ item.content }}</p>
    </v-container>
  </v-container>
</template>

<style scoped>
#article-container p {
  overflow: hidden;
  text-overflow: ellipsis;
}
</style>
