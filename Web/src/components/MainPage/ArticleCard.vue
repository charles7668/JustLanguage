<script setup lang="ts">
import { defineProps } from 'vue'
import { ArticleInfo } from '@/Models/ArticleInfo'
import type { Action } from '@/Models/Action'

defineProps({
  item: {
    type: Object,
    default() {
      return new ArticleInfo()
    }
  },
  actionItems: {
    type: Object,
    default() {
      return [] as Array<Action>
    }
  }
})

const getBriefText = (htmlText: string) => {
  const div = document.createElement('div')
  div.innerHTML = htmlText
  return div.innerText
}
</script>

<template>
  <v-container style="display: flex; flex-direction: row">
    <v-img alt="test" :src="item.coverImageBase64" width="150px" height="150px" />
    <v-container
      style="display: flex; flex-direction: column; max-height: 160px"
      id="article-container"
    >
      <v-row style="margin: 0">
        <h3>{{ item.title }}</h3>
        <v-spacer></v-spacer>
        <v-menu>
          <template v-slot:activator="{ props }">
            <v-btn icon="mdi-dots-vertical" v-bind="props" size="x-small"></v-btn>
          </template>

          <v-list>
            <v-list-item v-for="(actionItem, i) in actionItems" :key="i" :value="actionItem">
              <v-list-item-title @click="actionItem.action(item as ArticleInfo)">{{
                actionItem.name
              }}</v-list-item-title>
            </v-list-item>
          </v-list>
        </v-menu>
      </v-row>
      <p>{{ getBriefText(item.content) }}</p>
    </v-container>
  </v-container>
</template>

<style scoped>
#article-container p {
  overflow: hidden;
  text-overflow: ellipsis;
}
</style>
