<script setup lang="ts">
import { onMounted, ref } from 'vue'
import router from '@/router'
import { getArticleByIdApi, getTranslateResultApi } from '@/Api/Api'
import { ArticleInfo } from '@/Models/ArticleInfo'
import type { VContainer, VMain } from 'vuetify/components'

const info = ref<ArticleInfo>(new ArticleInfo())
const selectionTools = ref<InstanceType<typeof VContainer>>()
const selectionToolsLeft = ref(0)
const selectionToolsTop = ref(0)
const selectionToolsVisible = ref(false)
const translateText = ref('')
const translateShow = ref(false)
const markBtnIcon = ref('mdi-plus')

onMounted(async () => {
  const articleId = Number(router.currentRoute.value.params.articleId)
  let response: Response
  try {
    response = await getArticleByIdApi(articleId)
  } catch (e) {
    window.alert('Error: ' + e)
    router.push({ name: 'home' })
    return
  }
  if (response.status !== 200) {
    window.alert('Error: ' + response.status)
    router.push({ name: 'home' })
    return
  }
  info.value = await response.json()
  console.log(info.value.title)
})

const gotoHome = () => {
  router.push({ name: 'home' })
}

const mouseClick = () => {
  let selection = window.getSelection() as Selection
  if (selection.toString() === '') {
    selectionToolsVisible.value = false
    translateText.value = ''
    return
  }
  // get selection position
  let range = selection.getRangeAt(0)
  let rect = range.getBoundingClientRect()
  selectionToolsLeft.value = rect.x
  selectionToolsTop.value =
    rect.y + document.documentElement.scrollTop - selectionTools.value?.$el.clientHeight
  selectionToolsVisible.value = true
  // check if selection contain mark
  if (checkSelectionContainMark(selection)) {
    markBtnIcon.value = 'mdi-minus'
  } else {
    markBtnIcon.value = 'mdi-plus'
  }
}

const tryTranslate = async () => {
  let selection = window.getSelection()
  // if exist translate text, do nothing , this will reduce server request
  if (translateText.value !== '') return
  let response = await getTranslateResultApi({
    translateProvider: 'google',
    query: JSON.stringify({
      client: 'gtx',
      sl: 'auto',
      tl: 'zh-TW',
      hl: 'zh-TW',
      ie: 'UTF-8',
      oe: 'UTF-8',
      otf: '1',
      ssel: '0',
      tsel: '0',
      kc: '7',
      q: selection?.toString()
    })
  })
  translateText.value = await response.text()
}

const textToSpeech = () => {
  let selection = window.getSelection()
  if (selection === null || selection?.toString() === '') return
  let utterance = new SpeechSynthesisUtterance(selection.toString())
  speechSynthesis.speak(utterance)
}

const checkSelectionContainMark = (selection: Selection) => {
  let parent = selection.anchorNode?.parentElement as HTMLElement
  let parentNode = selection.anchorNode?.parentNode as Node
  let containMark = false
  if (parent?.nodeName == 'MARK') {
    return true
  } else {
    let childNodes = parentNode?.childNodes as NodeListOf<ChildNode>
    for (let i = 0; i < childNodes.length; i++) {
      if (childNodes[i].nodeName == 'MARK' && selection.containsNode(childNodes[i], true)) {
        containMark = true
        break
      }
    }
  }
  return containMark
}

const markText = () => {
  let selection = window.getSelection() as Selection
  let selectedText = selection.toString()
  let mark = document.createElement('mark')
  mark.textContent = selectedText
  let range = selection.getRangeAt(0)
  range.deleteContents()
  range.insertNode(mark)
}

const unmarkText = () => {
  let selection = window.getSelection() as Selection
  let parent = selection.anchorNode?.parentElement as HTMLElement
  let parentNode = selection.anchorNode?.parentNode as Node
  if (parent.nodeName == 'MARK') {
    let grandParent = parent.parentElement as HTMLElement
    parent.replaceWith(document.createTextNode(parent?.textContent || ''))
    grandParent.normalize()
    return
  } else {
    let childNodes = parentNode.childNodes as NodeListOf<ChildNode>
    for (let i = 0; i < childNodes.length; i++) {
      if (childNodes[i].nodeName == 'MARK' && selection.containsNode(childNodes[i], true)) {
        childNodes[i].replaceWith(document.createTextNode(childNodes[i].textContent || ''))
        parentNode.normalize()
      }
    }
  }
}
</script>

<template>
  <v-layout class="rounded rounded-md">
    <v-app-bar app color="cyan">
      <v-row justify="center">
        <v-btn variant="outlined" @click="gotoHome">Home</v-btn>
      </v-row>
    </v-app-bar>
    <v-main @click="mouseClick">
      <v-toolbar
        ref="selectionTools"
        id="selection-tools"
        density="compact"
        :style="{
          maxWidth: '235px',
          position: 'absolute',
          left: `${selectionToolsLeft}px`,
          top: `${selectionToolsTop}px`,
          visibility: selectionToolsVisible ? 'visible' : 'hidden',
          transitionDuration: '0s'
        }"
      >
        <v-tooltip
          v-model="translateShow"
          location="top"
          max-width="500px"
          :style="{
            visibility: translateText === '' ? 'hidden' : 'visible'
          }"
        >
          <template v-slot:activator="{ props }">
            <v-btn @click="tryTranslate" v-bind="props" color="primary">Translate</v-btn>
          </template>
          <span>{{ translateText }}</span>
        </v-tooltip>
        <v-btn icon="mdi-headphones" color="primary" @click="textToSpeech"></v-btn>
        <v-btn
          :icon="markBtnIcon"
          color="primary"
          @click="markBtnIcon === 'mdi-plus' ? markText() : unmarkText()"
        ></v-btn>
      </v-toolbar>
      <v-container class="d-flex flex-column justify-center">
        <v-row justify="center">
          <h1>{{ info.title }}</h1>
        </v-row>
        <v-row justify="center" class="ma-2">
          <v-img :src="info.coverImageBase64" max-width="500px"></v-img>
        </v-row>
        <v-row justify="center" id="article-content">
          <v-container v-dompurify-html="info.content" style="max-width: 800px"></v-container>
        </v-row>
      </v-container>
    </v-main>
  </v-layout>
</template>

<style scoped>
#article-content :deep(p) {
  padding-top: 10px;
  padding-bottom: 10px;
}
</style>
