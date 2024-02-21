<script setup lang="ts">
import { getArticles } from '@/Api/Api'
import { ref, onMounted } from 'vue'
import ActionBar from '@/components/MainPage/ActionBar.vue'
import ArticleList from '@/components/MainPage/ArticleList.vue'
import type { ArticleInfo } from '@/Models/ArticleInfo'
const articles = ref<Array<ArticleInfo>>([])

onMounted(async () => {
  await updateArticles()
})

const updateArticles = async () => {
  let response: Response
  try {
    response = await getArticles()
  } catch (e) {
    console.log('Failed to get articles : ' + e)
    return
  }
  if (response.status !== 200) {
    console.log('Failed to get articles : ' + response.status)
    return
  }
  let data = await response.json()
  articles.value = data
}

const updateListEvent = async () => {
  await updateArticles()
}
</script>

<template>
  <v-layout class="rounded rounded-md">
    <ActionBar @update-list="updateListEvent" />
    <v-navigation-drawer location="left"></v-navigation-drawer>
    <v-navigation-drawer location="right"></v-navigation-drawer>
    <v-main class="d-flex align-center justify-center">
      <ArticleList :items="articles"></ArticleList>
    </v-main>
  </v-layout>
</template>

<style scoped></style>
