<script setup lang="ts">

const props = defineProps({
  CloseAction: {
    type: Function,
    required: true
  }
})

const uploadAction = () => {
  props.CloseAction()
}

</script>

<template>
  <v-card>
    <v-card-title>
      Upload Article
    </v-card-title>
    <v-card-item>
      <v-responsive
          class="mx-auto"
          width="500px">
        <v-text-field></v-text-field>
      </v-responsive>
    </v-card-item>
    <v-card-actions>
      <v-row>
        <v-spacer></v-spacer>
        <v-btn text="Upload" @click="uploadAction"></v-btn>
        <v-btn text="Cancel" @click="props.CloseAction()"></v-btn>
        <v-spacer></v-spacer>
      </v-row>
    </v-card-actions>
  </v-card>
</template>

<style scoped>

</style>