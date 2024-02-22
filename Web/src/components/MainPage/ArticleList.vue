<script setup lang="ts">
import type { ArticleInfo } from '@/Models/ArticleInfo'
import ArticleCard from '@/components/MainPage/ArticleCard.vue'
import router from '@/router'

defineProps({
  items: {
    type: Array<ArticleInfo>,
    default() {
      return []
    }
  }
})

const routeToArticlePage = (item: ArticleInfo) => {
  console.log(item.id)
  router.push({ name: 'ArticlePage', params: { articleId: item.id } })
}
</script>

<template>
  <v-list :items="items" width="1000" lines="Three">
    <v-list-item
      v-for="(item, i) in items"
      :key="i"
      :value="item"
      @click="routeToArticlePage(item)"
    >
      <template v-slot:default>
        <v-card>
          <ArticleCard :item="item"></ArticleCard>
        </v-card>
      </template>
    </v-list-item>
  </v-list>
</template>

<style scoped></style>
