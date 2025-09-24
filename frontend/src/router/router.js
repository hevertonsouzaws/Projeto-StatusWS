import { createRouter, createWebHistory } from 'vue-router';
import { usuarioLogado, usuarioLogadoId } from '../modeState.js';
import Login from '../views/Login.vue'; 
import HomeView from '../views/HomeView.vue';
import TeamView from '../views/TeamView.vue';
import StatusView from '../views/StatusView.vue'
import UserStatusView from '../views/UserStatusView.vue';

const routes = [
  { path: '/', name: 'login', component: Login},
  { path: '/home', name: 'home', component: HomeView, meta: { requiresAuth: true } },
  { path: '/team', name: 'team', component: TeamView, meta: { requiresAuth: true, requiresAdmin: true } },
  { path: '/status', name: 'status', component: StatusView, meta: { requiresAuth: true }  },
  { path: '/user', name: 'user', component: UserStatusView, meta: { requiresAuth: true }}
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach((to, from, next) => {
  const adminsNames = ['Heverton Souza', 'Aline Gallo'];
  const isAdmin = usuarioLogado.value && adminsNames.includes(usuarioLogado.value.name);

  if (to.meta.requiresAuth && !usuarioLogadoId.value) {
    next({ name: 'login' });
  } else if (to.meta.requiresAdmin && !isAdmin) {
    next({ name: 'home' });
  } else {
    next();
  }
});

export default router;