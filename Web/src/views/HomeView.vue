<script setup lang="ts">
import ActionBar from '../components/MainPage/ActionBar.vue'
import ArticleList from '@/components/MainPage/ArticleList.vue'
</script>

<template>
  <v-layout class="rounded rounded-md">
    <ActionBar />
    <v-navigation-drawer location="left"></v-navigation-drawer>
    <v-navigation-drawer location="right"></v-navigation-drawer>
    <v-main class="d-flex align-center justify-center">
      <ArticleList></ArticleList>
    </v-main>
  </v-layout>
</template>

<style scoped></style>
