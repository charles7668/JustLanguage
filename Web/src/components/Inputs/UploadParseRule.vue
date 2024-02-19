<script setup lang="ts">
import { reactive } from 'vue'
import { useVuelidate } from '@vuelidate/core'
import { required } from '@vuelidate/validators'

const props = defineProps({
  AddAction: {
    type: Function,
    required: true
  },
  CloseAction: {
    type: Function,
    required: true
  }
})

const initialState = {
  ruleName: '',
  allowDomain: '',
  getCoverXPath: '',
  getAuthorXPath: '',
  getTitleXPath: '',
  getArticleXPath: ''
}
const state = reactive({ ...initialState })
const rules = {
  ruleName: { required },
  allowDomain: { required },
  getCoverXPath: {},
  getAuthorXPath: {},
  getTitleXPath: {},
  getArticleXPath: {}
}
const v$ = useVuelidate(rules, state)
</script>

<template>
  <v-card height="500px">
    <form>
      <v-text-field
        v-model="state.ruleName"
        :error-messages="
          v$.ruleName.$errors.map((e) =>
            typeof e.$message === 'string' ? e.$message : e.$message.value
          )
        "
        label="RuleName"
        required
        @input="v$.ruleName.$touch"
        @blur="v$.ruleName.$touch"
      ></v-text-field>
      <v-text-field
        v-model="state.allowDomain"
        :error-messages="
          v$.allowDomain.$errors.map((e) =>
            typeof e.$message === 'string' ? e.$message : e.$message.value
          )
        "
        label="Domains(split by ',')"
        required
        @input="v$.allowDomain.$touch"
        @blur="v$.allowDomain.$touch"
      >
      </v-text-field>
      <v-text-field
        v-model="state.getCoverXPath"
        :error-messages="
          v$.getCoverXPath.$errors.map((e) =>
            typeof e.$message === 'string' ? e.$message : e.$message.value
          )
        "
        label="XPath for cover image"
        @input="v$.getCoverXPath.$touch"
        @blur="v$.getCoverXPath.$touch"
      >
      </v-text-field>
      <v-text-field
        v-model="state.getAuthorXPath"
        :error-messages="
          v$.getAuthorXPath.$errors.map((e) =>
            typeof e.$message === 'string' ? e.$message : e.$message.value
          )
        "
        label="XPath for author"
        required
        @input="v$.getAuthorXPath.$touch"
        @blur="v$.getAuthorXPath.$touch"
      >
      </v-text-field>
      <v-text-field
        v-model="state.getTitleXPath"
        :error-messages="
          v$.getTitleXPath.$errors.map((e) =>
            typeof e.$message === 'string' ? e.$message : e.$message.value
          )
        "
        label="XPath for title"
        required
        @input="v$.getTitleXPath.$touch"
        @blur="v$.getTitleXPath.$touch"
      >
      </v-text-field>
      <v-text-field
        v-model="state.getArticleXPath"
        :error-messages="
          v$.getArticleXPath.$errors.map((e) =>
            typeof e.$message === 'string' ? e.$message : e.$message.value
          )
        "
        label="XPath for article"
        required
        @input="v$.getArticleXPath.$touch"
        @blur="v$.getArticleXPath.$touch"
      >
      </v-text-field>
      <v-row justify="center">
        <v-btn @click="props.CloseAction"> add </v-btn>
        <v-btn @click="props.AddAction"> close </v-btn>
      </v-row>
    </form>
  </v-card>
</template>
