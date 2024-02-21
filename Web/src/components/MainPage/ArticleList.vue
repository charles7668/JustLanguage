<script setup lang="ts">
import type { ArticleInfo } from '@/Models/ArticleInfo'
import ArticleCard from '@/components/MainPage/ArticleCard.vue'
defineProps({
  items: Array<ArticleInfo>
})
</script>

<template>
  <v-list :items="items" width="1000" lines="Three">
    <v-list-item v-for="(item, i) in items" :key="i" :value="item">
      <template v-slot:default>
        <v-card>
          <ArticleCard :item="item"></ArticleCard>
        </v-card>
      </template>
    </v-list-item>
  </v-list>
</template>

<style scoped></style>
