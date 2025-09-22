import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '../views/HomeView.vue';
import StatusView from '../views/StatusView.vue';
import TeamView from '../views/TeamView.vue';

const routes = [
  { path: '/', name: 'Home', component: HomeView },
  { path: '/team', name: 'Team', component: TeamView },

  { path: '/status', name: 'Status', component: StatusView}
];

// roteador 
const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;