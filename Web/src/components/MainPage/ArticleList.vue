<script setup lang="ts">
import ArticleCard from '@/components/MainPage/ArticleCard.vue'
defineProps({
  items: Array<{
    title: string
    coverUrl: string
    brief: string
  }>
})
</script>

<template>
  <v-list :items="items" width="1000" lines="Three">
    <v-list-item v-for="(item, i) in items" :key="i" :value="item">
      <template v-slot:default>
        <v-card>
          <ArticleCard
            :title="item.title"
            :brief="item.brief"
            :cover-url="item.coverUrl"
          ></ArticleCard>
        </v-card>
      </template>
    </v-list-item>
  </v-list>
</template>

<style scoped></style>
