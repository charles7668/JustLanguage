<script setup lang="ts">
import {onMounted, ref} from 'vue'
import ArticleCard from '@/components/MainPage/ArticleCard.vue'

const items = ref<
    Array<{
      title: string
      coverUrl: string
      brief: string
    }>
>([])

onMounted(() => {
  // let temp = []
  for (let i = 0; i < 10; i++) {
    items.value.push({
      title: 'test' + i,
      coverUrl: 'https://cdn-icons-png.flaticon.com/512/1040/1040243.png',
      brief: 'test'
    })
  }
  // items.value =
  console.log(items.value)
})
</script>

<template>
  <v-list :items="items" width="1000">
    <v-list-item v-for="(item, i) in items" :key="i" :value="item">
      <template v-slot:default>
        <v-card>
          <ArticleCard title="test" brief="test"></ArticleCard>
        </v-card>
      </template>
    </v-list-item>
  </v-list>
</template>

<style scoped></style>
