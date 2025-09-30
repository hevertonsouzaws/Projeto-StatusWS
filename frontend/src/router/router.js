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
  { path: '/user', name: 'user', component: UserStatusView, meta: { requiresAuth: true }}, 
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach((to, from, next) => {
  const authRequired = to.meta.requiresAuth;
  const adminRequired = to.meta.requiresAdmin;
  const isLoginPage = to.name === 'login';
  
  const isLogged = usuarioLogadoId.value; 
  const adminsNames = ['Heverton Souza', 'Aline Gallo'];
  const isAdmin = isLogged && usuarioLogado.value && adminsNames.includes(usuarioLogado.value.name);
  
  if (isLogged && isLoginPage) {
    return next({ name: 'home' });
  }

  if (authRequired && !isLogged) {
    return next({ name: 'login' });
  }

  if (adminRequired && !isAdmin) {
    return next({ name: 'home' }); 
  }

  next();
});

export default router;