<script setup lang="ts">
import { deleteArticleByIdApi } from '@/Api/Api'
import type { Action } from '@/Models/Action'
import { ArticleInfo } from '@/Models/ArticleInfo'
import ArticleCard from '@/components/MainPage/ArticleCard.vue'
import router from '@/router'
import { ref } from 'vue'

const emit = defineEmits(['update-list'])

defineProps({
  items: {
    type: Object,
    default() {
      return [] as Array<ArticleInfo>
    }
  }
})

const articleActions = ref<Array<Action>>([
  {
    name: 'Delete',
    action: async (info: ArticleInfo) => {
      console.log(info.id)
      await deleteArticleByIdApi(info.id)
      emit('update-list')
    }
  }
])

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
          <ArticleCard :item="item" :action-items="articleActions"></ArticleCard>
        </v-card>
      </template>
    </v-list-item>
  </v-list>
</template>

<style scoped></style>
