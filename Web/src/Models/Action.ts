export class Action {
  name: string
  action: Function
  constructor(name: string, action: Function) {
    this.name = name
    this.action = action
  }
}
