import { createRouter, createWebHistory } from 'vue-router';
import Home from '../components/HomePage.vue';
import ProductDetails from '../components/ProductDetails.vue';
import AddProduct from '../components/AddProduct.vue';
import EditProduct from '../components/EditProduct.vue';
import ProductsPage from '@/components/ProductsPage.vue';
import LoginPage from '@/components/LoginPage.vue';
import RegisterPage from '@/components/RegisterPage.vue';

const routes = [
  { path: '/', component: Home },
  { path: '/products', component: ProductsPage, meta: { requiresAuth: true } },
  { path: '/products/:id', component: ProductDetails, meta: {requiresAuth: true } },
  { path: '/add-product', component: AddProduct, meta: { requiresAuth: true } },
  { path: '/edit-product/:id', component: EditProduct, meta: { requiresAuth: true } },
  { path: '/login', component: LoginPage },
  { path: '/register', component: RegisterPage }
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach((to, from, next) => {
    const loggedIn = localStorage.getItem('token');
  
    if (to.matched.some(record => record.meta.requiresAuth) && !loggedIn) {
      next('/login');
    } else {
      next();
    }
  });

export default router;