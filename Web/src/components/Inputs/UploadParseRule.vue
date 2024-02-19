<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue'
import { useVuelidate } from '@vuelidate/core'
import { required } from '@vuelidate/validators'
import { ParseRule, SupportDomain } from '@/Models/ParseRule'
import { postParseRules, getParseRuleNames, getParseRuleByName, updateParseRule } from '@/Api/Api'

onMounted(async () => {
  let response = await getParseRuleNames()
  let body = JSON.parse(await response.text())
  ruleNames.value = [''].concat(body)
})

const props = defineProps({
  CloseAction: {
    type: Function,
    required: true
  }
})

const submitButtonText = ref('Add')
const ruleNames = ref<string[]>([])
const selectedItem = ref('')
const ruleNameReadonly = ref(false)

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

const clickAdd = async () => {
  if (v$.value.$invalid) {
    window.alert('Please fill in all required fields')
    return
  }
  let convertParseRule = () => {
    let domains = state.allowDomain.split(',').map((d) => new SupportDomain(d))
    let rule = JSON.stringify({
      getCoverXPath: state.getCoverXPath,
      getAuthorXPath: state.getAuthorXPath,
      getTitleXPath: state.getTitleXPath,
      getArticleXPath: state.getArticleXPath
    })
    let parseRule = new ParseRule(state.ruleName, domains)
    parseRule.rule = rule
    return parseRule
  }
  let response: Response = null!
  if (submitButtonText.value === 'Add') {
    response = await postParseRules(convertParseRule())
  } else {
    if (selectedItem.value !== state.ruleName) {
      window.alert('RuleName cannot be changed')
      return
    }
    response = await updateParseRule(convertParseRule())
  }
  if (response.status !== 200) {
    window.alert(
      'error : ' + response.status + ' ' + response.statusText + '\n' + (await response.text())
    )
    return
  }
  props.CloseAction()
}

const selectionChange = async () => {
  let name = selectedItem.value
  if (name === '') {
    submitButtonText.value = 'Add'
    ruleNameReadonly.value = false
    return
  }
  let response = await getParseRuleByName(name)
  if (response.status !== 200) {
    window.alert(
      'error : ' + response.status + ' ' + response.statusText + '\n' + (await response.text())
    )
    selectedItem.value = ''
    return
  }
  let rule: ParseRule = JSON.parse(await response.text())
  state.ruleName = rule.name
  state.allowDomain = rule.supportDomains.map((d) => d.domain).join(',')
  let ruleObject = JSON.parse(rule.rule)
  state.getCoverXPath = ruleObject.getCoverXPath
  state.getAuthorXPath = ruleObject.getAuthorXPath
  state.getTitleXPath = ruleObject.getTitleXPath
  state.getArticleXPath = ruleObject.getArticleXPath
  submitButtonText.value = 'Update'
  ruleNameReadonly.value = true
}
</script>

<template>
  <v-card height="500px">
    <v-select
      v-model="selectedItem"
      label="Select Rule"
      :items="ruleNames"
      bg-color="gray"
      v-on:update:modelValue="selectionChange"
    >
    </v-select>
    <form>
      <v-text-field
        :readonly="ruleNameReadonly"
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
        <v-btn @click="clickAdd" :text="submitButtonText"></v-btn>
        <v-btn @click="props.CloseAction"> close</v-btn>
      </v-row>
    </form>
  </v-card>
</template>
