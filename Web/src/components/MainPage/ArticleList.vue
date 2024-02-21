<script setup lang="ts">
import { onMounted, ref } from 'vue'
import ArticleCard from '@/components/MainPage/ArticleCard.vue'
import { getArticles } from '@/Api/Api'

const items = ref<
  Array<{
    title: string
    coverUrl: string
    brief: string
  }>
>([])

onMounted(async () => {
  let temp = []
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
  console.log(data)
  for (let i = 0; i < data.length; i++) {
    temp.push({
      title: data[i].title,
      coverUrl: data[i].coverImageBase64,
      brief: data[i].content
    })
  }
  items.value = temp
  console.log(items.value)
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
