<template>
  <div class="container mt-5">
    <h1>Register</h1>
    <form @submit.prevent="register">
      <div class="mb-3">
        <label for="username" class="form-label">Username</label>
        <input 
          type="text" 
          class="form-control" 
          id="username" 
          v-model="user.username" 
          @blur="validateUsername"
          :class="{'is-invalid': errors.username}" 
          required>
        <div v-if="errors.username" class="invalid-feedback">
          {{ errors.username }}
        </div>
      </div>
      <div class="mb-3">
        <label for="password" class="form-label">Password</label>
        <input 
          type="password" 
          class="form-control" 
          id="password" 
          v-model="user.passwordHash" 
          @blur="validatePassword"
          :class="{'is-invalid': errors.passwordHash}" 
          required>
        <div v-if="errors.passwordHash" class="invalid-feedback">
          {{ errors.passwordHash }}
        </div>
      </div>
      <button type="submit" class="btn btn-primary" :disabled="hasErrors">Register</button>

    </form>
  </div>
</template>

<script>
import axios from 'axios';
import { ref, reactive, computed } from 'vue';
import { useRouter } from 'vue-router';

export default {
  name: 'RegisterPage',
  setup() {
    const router = useRouter();
    const user = ref({
      username: '',
      passwordHash: ''
    });

    const errors = reactive({
      username: null,
      passwordHash: null
    });

    const serverError = ref('');

    const validateUsername = () => {
      errors.username = user.value.username.length >= 5 ? null : 'Username must be at least 5 characters long.';
    };

    const validatePassword = () => {
      errors.passwordHash = user.value.passwordHash.length >= 5 ? null : 'Password must be at least 5 characters long.';
    };

    const hasErrors = computed(() => {
      return !!errors.username || !!errors.passwordHash;
    });

    const register = async () => {
      validateUsername();
      validatePassword();
      
      if (hasErrors.value) {
        return;
      }

      try {
        await axios.post('https://localhost:7144/api/Auth/register', {
          username: user.value.username,
          password: user.value.passwordHash
        });
        router.push('/login');
      } catch (error) {
        console.log('Error response:', error.response);
        if (error.response && error.response.status === 400) {
          const errorData = error.response.data;
          console.log('Error data:', errorData);
          if (Array.isArray(errorData.errors)) {

            errorData.errors.forEach(err => {
              if (err.toLowerCase().includes('username')) {
                errors.username = err;
              } else if (err.toLowerCase().includes('password')) {
                errors.passwordHash = err;
              }
            });
          } else if (typeof errorData.errors === 'object') {

            Object.keys(errorData.errors).forEach(key => {
              if (key.toLowerCase() === 'username') {
                errors.username = errorData.errors[key].join(', ');
              } else if (key.toLowerCase() === 'password') {
                errors.passwordHash = errorData.errors[key].join(', ');
              }
            });
          } else if (errorData.message) {
            serverError.value = errorData.message;
          } else {
            serverError.value = 'An error occurred during registration.';
          }
        } else {
          serverError.value = 'An error occurred during registration.';
        }
      }
    };

    return {
      user,
      errors,
      serverError,
      validateUsername,
      validatePassword,
      hasErrors,
      register
    };
  }
};
</script>
