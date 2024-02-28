import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import ArticlePageVue from '@/components/ArticlePage/ArticlePage.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
      props: (route) => ({ page: route.query.page })
    },
    {
      path: '/articles/:articleId',
      name: 'ArticlePage',
      component: ArticlePageVue
    }
  ]
})

export default router
