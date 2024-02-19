class SupportDomain {
  Domain: string

  constructor(domain: string) {
    this.Domain = domain
  }
}

class ParseRule {
  Name: string
  SupportDomains: Array<SupportDomain>
  Rule: string

  constructor(ruleName: string, allowDomains: Array<SupportDomain>) {
    this.Name = ruleName
    this.SupportDomains = allowDomains
    this.Rule = ''
  }
}

export { ParseRule, SupportDomain }
