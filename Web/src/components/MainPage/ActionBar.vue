<script setup lang="ts">
import { ref } from 'vue'
import UploadParseRule from '../Inputs/UploadParseRule.vue'

let dialog = ref(false)

const closeDialog = () => {
  dialog.value = false
}
</script>

<template>
  <v-layout>
    <v-app-bar app color="cyan">
      <v-row justify="center">
        <v-btn variant="outlined"
          >MANAGE RULES
          <v-dialog v-model="dialog" activator="parent" width="1000px" persistent>
            <UploadParseRule :CloseAction="closeDialog" />
          </v-dialog>
        </v-btn>
      </v-row>
    </v-app-bar>
  </v-layout>
</template>

<style scoped></style>
