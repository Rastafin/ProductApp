<template>
  <div>
    <nav class="navbar navbar-expand-lg navbar-light bg-light">
      <div class="container-fluid">
        <router-link class="navbar-brand" to="/">ProductApp</router-link>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarNav">
          <ul class="navbar-nav me-auto mb-2 mb-lg-0">
            <li class="nav-item" v-if="loggedIn">
              <router-link class="nav-link" to="/products">Products</router-link>
            </li>
            <li class="nav-item" v-if="loggedIn">
              <router-link class="nav-link" to="/add-product">Add Product</router-link>
            </li>
            <li class="nav-item" v-if="!loggedIn">
              <router-link class="nav-link" to="/login">Login</router-link>
            </li>
            <li class="nav-item" v-if="!loggedIn">
              <router-link class="nav-link" to="/register">Register</router-link>
            </li>
            <li class="nav-item" v-if="loggedIn">
              <a class="nav-link" href="#" @click="logout">Logout</a>
            </li>
          </ul>
        </div>
      </div>
    </nav>
    <router-view></router-view>
  </div>
</template>

<script>
import { ref, onMounted } from 'vue';
import axios from 'axios';

export default {
  name: 'App',
  setup() {
    const loggedIn = ref(!!localStorage.getItem('token'));

    const logout = () => {
      localStorage.removeItem('token');
      delete axios.defaults.headers.common['Authorization'];
      loggedIn.value = false;
      window.dispatchEvent(new Event('logout'));
    };

    const updateLoginStatus = () => {
      loggedIn.value = !!localStorage.getItem('token');
    };

    onMounted(() => {
      window.addEventListener('login', updateLoginStatus);
      window.addEventListener('logout', updateLoginStatus);
    });

    return {
      loggedIn,
      logout
    };
  }
};
</script>
