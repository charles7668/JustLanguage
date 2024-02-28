<script setup lang="ts">
import { getArticleCountApi, getArticles } from '@/Api/Api'
import { ref, onMounted } from 'vue'
import ActionBar from '@/components/MainPage/ActionBar.vue'
import ArticleList from '@/components/MainPage/ArticleList.vue'
import type { ArticleInfo } from '@/Models/ArticleInfo'
import { VRow } from 'vuetify/components'
import router from '@/router'

const articles = ref<Array<ArticleInfo>>([])
const articleCount = ref(0)
const lastPage = ref(1)
const firstPage = ref(1)
const hasPrevPage = ref(false)
const hasNextPage = ref(false)
const selectedPage = ref(1)
const pages = ref([1])

const articlePerPage = 20

onMounted(async () => {
  selectedPage.value = Number(router.currentRoute.value.query.page) || 1
  let response = await getArticleCountApi()
  articleCount.value = (await response.text()) as unknown as number
  let pageCount = Math.ceil(articleCount.value / articlePerPage)
  lastPage.value = pageCount
  for (let i = 1; i < pageCount; i++) {
    pages.value.push(i + 1)
  }
  await updateArticles(selectedPage.value)
  await updatePageSwitchBtnState()
})

const updateArticles = async (pageNum: number = 1) => {
  let response: Response
  try {
    response = await getArticles(pageNum - 1)
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
  await updateArticles(selectedPage.value)
}

const updatePageSwitchBtnState = async () => {
  hasPrevPage.value = selectedPage.value > firstPage.value
  hasNextPage.value = selectedPage.value < lastPage.value
}

const pageChangeEvent = async () => {
  router.push({ name: 'home', query: { page: selectedPage.value } })
  await updateArticles(selectedPage.value)
}

const pageSwitch = async (direction: string) => {
  if (direction === 'prev') {
    selectedPage.value = selectedPage.value - 1
  } else if (direction === 'next') {
    selectedPage.value = selectedPage.value + 1
  }
  await updatePageSwitchBtnState()
  await pageChangeEvent()
}
</script>

<template>
  <v-layout class="rounded rounded-md">
    <ActionBar @update-list="updateListEvent" />
    <v-navigation-drawer location="left"></v-navigation-drawer>
    <v-navigation-drawer location="right"></v-navigation-drawer>
    <v-main class="d-flex align-center justify-center">
      <v-col align-self="center">
        <v-row justify="center">
          <ArticleList :items="articles" @update-list="updateListEvent"></ArticleList>
        </v-row>
        <v-row justify="center" class="py-4" :align="'center'">
          <v-btn
            ref="btnPrevPage"
            :disabled="!hasPrevPage"
            text="<"
            size="small"
            class="page-switch-btn"
            @click="pageSwitch('prev')"
          ></v-btn>
          <v-select
            v-model="selectedPage"
            :items="pages"
            density="compact"
            :style="{
              maxWidth: '100px'
            }"
            hide-details
            @update:modelValue="pageChangeEvent"
          >
          </v-select>
          <v-btn
            ref="btnNextPage"
            :disabled="!hasNextPage"
            text=">"
            size="small"
            class="page-switch-btn"
            @click="pageSwitch('next')"
          ></v-btn>
        </v-row>
      </v-col>
    </v-main>
  </v-layout>
</template>

<style scoped>
.v-select {
  height: auto;
}

.page-switch-btn {
  height: 100%;
  padding-top: 13px;
  padding-bottom: 13px;
  margin-left: 5px;
  margin-right: 5px;
}
</style>
