<script setup lang="ts">
import { ref } from 'vue'
import { ParseRule } from '@/Models/DefaultParseRule'
import UploadParseRule from '../Inputs/UploadParseRule.vue'
import { postParseRules } from '@/Api/Api'

let dialog = ref(false)

const closeDialog = () => {
  dialog.value = false
}

const addAction = async (rule: ParseRule) => {
  console.log(rule)
  let response = await postParseRules(rule)
  if (response.status !== 200) {
    window.alert(
      'error : ' + response.status + ' ' + response.statusText + '\n' + (await response.text())
    )
    return
  }
  dialog.value = false
}
</script>

<template>
  <v-layout>
    <v-app-bar app color="cyan">
      <v-row justify="center">
        <v-btn variant="outlined"
          >UPLOAD
          <v-dialog v-model="dialog" activator="parent" width="1000px" persistent>
            <UploadParseRule :AddAction="addAction" :CloseAction="closeDialog" />
          </v-dialog>
        </v-btn>
      </v-row>
    </v-app-bar>
  </v-layout>
</template>

<style scoped></style>
