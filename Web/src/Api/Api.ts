import { ParseRule } from '@/Models/ParseRule'
import { UploadArticle } from '@/Models/UploadArticle'

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

const postArticle = async (data: UploadArticle) => {
  return await fetch(apiUrl + '/api/Articles', {
    method: 'POST',
    body: JSON.stringify(data),
    headers: {
      'Content-Type': 'application/json'
    }
  })
}

const deleteParseRule = async (name: string) => {
  return await fetch(apiUrl + '/api/ParseRules/' + name, {
    method: 'DELETE'
  })
}

const getArticles = async (page = 0) => {
  return await fetch(apiUrl + '/api/Articles?page=' + page)
}

export {
  postParseRules,
  getParseRuleNames,
  getParseRuleByName,
  updateParseRule,
  postArticle,
  deleteParseRule,
  getArticles
}
