<script setup lang="ts">
import { useVuelidate } from '@vuelidate/core'
import { reactive } from 'vue'
import { required, url } from '@vuelidate/validators'
import { postArticle } from '@/Api/Api'

const emit = defineEmits(['update-list'])

const props = defineProps({
  CloseAction: {
    type: Function,
    required: true
  }
})

const initState = {
  articleUrl: ''
}

const state = reactive({ ...initState })
const validationRules = {
  articleUrl: { required, url }
}

const v$ = useVuelidate(validationRules, state)

const uploadAction = async () => {
  if (v$.value.$invalid) {
    window.alert('Please fill in all required fields')
    return
  }
  let response: Response
  try {
    response = await postArticle({ articleUrl: state.articleUrl })
  } catch (e) {
    window.alert('Error uploading article : ' + e)
    return
  }
  if (response.status !== 200) {
    window.alert('Error uploading article : ' + (await response.text()))
    return
  }
  emit('update-list')
  props.CloseAction()
}
</script>

<template>
  <v-card>
    <v-card-title> Upload Article </v-card-title>
    <v-card-item>
      <v-responsive class="mx-auto" width="500px">
        <v-text-field
          v-model="state.articleUrl"
          :error-messages="
            v$.articleUrl.$errors.map((e) =>
              typeof e.$message === 'string' ? e.$message : e.$message.value
            )
          "
          @input="v$.articleUrl.$touch"
          @blur="v$.articleUrl.$touch"
        ></v-text-field>
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

<style scoped></style>
