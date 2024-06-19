<template>
  <div class="container mt-5">
    <h1>Login</h1>
    <form @submit.prevent="login">
      <div class="mb-3">
        <label for="username" class="form-label">Username</label>
        <input type="text" class="form-control" id="username" v-model="credentials.username" required>
      </div>
      <div class="mb-3">
        <label for="password" class="form-label">Password</label>
        <input type="password" class="form-control" id="password" v-model="credentials.password" required>
      </div>
      <button type="submit" class="btn btn-primary">Login</button>
    </form>
  </div>
</template>

<script>
import axios from 'axios';
import { ref } from 'vue';
import { useRouter } from 'vue-router';

export default {
  name: 'LoginPage',
  setup() {
    const router = useRouter();
    const credentials = ref({
      username: '',
      password: ''
    });

    const login = async () => {
      try {
        const response = await axios.post('https://localhost:7144/api/Auth/login', credentials.value);
        localStorage.setItem('token', response.data.token);
        axios.defaults.headers.common['Authorization'] = `Bearer ${response.data.token}`;
        router.push('/products');

        window.dispatchEvent(new Event('login'));
      } catch (error) {
        console.log(error);
      }
    };

    return {
      credentials,
      login
    };
  }
};
</script>