<script setup lang="ts">
import { ref } from 'vue'
import UploadParseRule from '../Inputs/ManageParseRules.vue'
import UploadArticle from '@/components/Inputs/UploadArticle.vue'

let dialog = ref(false)
let uploadArticleDialog = ref(false)

const closeDialog = () => {
  dialog.value = false
}

const closeUploadArticleDialog = () => {
  uploadArticleDialog.value = false
}
</script>

<template>
  <v-app-bar app color="cyan">
    <v-row justify="center">
      <v-btn variant="outlined"
        >MANAGE RULES
        <v-dialog v-model="dialog" activator="parent" width="1000px" persistent>
          <UploadParseRule :CloseAction="closeDialog" />
        </v-dialog>
      </v-btn>
      <v-btn variant="outlined">
        UPLOAD ARTICLE
        <v-dialog v-model="uploadArticleDialog" activator="parent" width="auto" persistent>
          <UploadArticle
            @update-list="$emit('update-list')"
            :CloseAction="closeUploadArticleDialog"
          ></UploadArticle>
        </v-dialog>
      </v-btn>
    </v-row>
  </v-app-bar>
</template>

<style scoped></style>
