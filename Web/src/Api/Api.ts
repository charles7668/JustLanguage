import { ParseRule } from '@/Models/DefaultParseRule'

const postParseRules = async (data: ParseRule) => {
  return await fetch('api/ParseRules', {
    method: 'POST',
    body: JSON.stringify(data),
    headers: {
      'Content-Type': 'application/json'
    }
  })
}

export { postParseRules }
