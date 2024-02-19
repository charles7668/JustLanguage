class SupportDomain {
  domain: string

  constructor(domain: string) {
    this.domain = domain
  }
}

class ParseRule {
  name: string
  supportDomains: Array<SupportDomain>
  rule: string

  constructor(ruleName: string, allowDomains: Array<SupportDomain>) {
    this.name = ruleName
    this.supportDomains = allowDomains
    this.rule = ''
  }
}

export { ParseRule, SupportDomain }
