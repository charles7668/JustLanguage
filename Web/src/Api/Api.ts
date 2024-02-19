import { ParseRule } from '@/Models/ParseRule'

const apiUrl = import.meta.env.VITE_API_URL || ''

const postParseRules = async (data: ParseRule) => {
  return await fetch(apiUrl + '/api/ParseRules', {
    method: 'POST',
    body: JSON.stringify(data),
    headers: {
      'Content-Type': 'application/json'
    }
  })
}

const getParseRuleNames = async () => {
  return await fetch(apiUrl + '/api/ParseRuleNames')
}

const getParseRuleByName = async (name: string) => {
  return await fetch(apiUrl + '/api/ParseRules/' + name)
}

const updateParseRule = async (data: ParseRule) => {
  return await fetch(apiUrl + '/api/ParseRules/' + data.name, {
    method: 'PUT',
    body: JSON.stringify(data),
    headers: {
      'Content-Type': 'application/json'
    }
  })
}

export { postParseRules, getParseRuleNames, getParseRuleByName, updateParseRule }
