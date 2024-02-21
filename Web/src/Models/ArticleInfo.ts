class ArticleInfo {
  id: number
  title: string
  content: string
  coverImageBase64: string
  author: string
  srcUrl: string
  constructor() {
    this.id = 0
    this.title = ''
    this.content = ''
    this.coverImageBase64 = ''
    this.author = ''
    this.srcUrl = ''
  }
}
export { ArticleInfo }
