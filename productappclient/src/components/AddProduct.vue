<template>
  <div class="container mt-5">
    <h1>Add Product</h1>
    <form @submit.prevent="addProduct">
      <div class="mb-3">
        <label for="name" class="form-label">Name</label>
        <input 
          type="text" 
          id="name" 
          v-model="product.name" 
          @blur="validateName"
          :class="{'is-invalid': errors.name}" 
          class="form-control" 
          required>
        <div v-if="errors.name" class="invalid-feedback">
          {{ errors.name }}
        </div>
      </div>
      <div class="mb-3">
        <label for="description" class="form-label">Description</label>
        <textarea 
          id="description" 
          v-model="product.description" 
          @blur="validateDescription"
          :class="{'is-invalid': errors.description}" 
          class="form-control" 
          required></textarea>
        <div v-if="errors.description" class="invalid-feedback">
          {{ errors.description }}
        </div>
      </div>
      <div class="mb-3">
        <label for="price" class="form-label">Price</label>
        <input 
          type="number" 
          id="price" 
          step="0.01"
          v-model="product.price" 
          @blur="validatePrice"
          :class="{'is-invalid': errors.price}" 
          class="form-control" 
          required>
        <div v-if="errors.price" class="invalid-feedback">
          {{ errors.price }}
        </div>
      </div>
      <div class="mb-3">
        <label for="quantity" class="form-label">Quantity</label>
        <input 
          type="number" 
          id="quantity" 
          v-model="product.quantity" 
          @blur="validateQuantity"
          :class="{'is-invalid': errors.quantity}" 
          class="form-control" 
          required>
        <div v-if="errors.quantity" class="invalid-feedback">
          {{ errors.quantity }}
        </div>
      </div>
      <button type="submit" class="btn btn-primary" :disabled="hasErrors">Add Product</button>
    </form>
  </div>
</template>

<script>
import axios from 'axios';
import { ref, reactive, computed } from 'vue';
import { useRouter } from 'vue-router';

export default {
  name: 'AddProduct',
  setup() {
    const router = useRouter();
    const product = ref({
      name: '',
      description: '',
      price: 0,
      quantity: 0
    });

    const errors = reactive({
      name: null,
      description: null,
      price: null,
      quantity: null
    });

    const serverError = ref('');

    const validateName = () => {
      errors.name = product.value.name.length > 0 ? null : 'Name is required.';
    };

    const validateDescription = () => {
      errors.description = product.value.description.length > 0 ? null : 'Description is required.';
    };

    const validatePrice = () => {
      errors.price = product.value.price > 0 ? null : 'Price must be greater than 0.';
    };

    const validateQuantity = () => {
      errors.quantity = Number.isInteger(product.value.quantity) && product.value.quantity > 0 ? null : 'Quantity must be a natural number.';
    };

    const hasErrors = computed(() => {
      return !!errors.name || !!errors.description || !!errors.price || !!errors.quantity;
    });

    const addProduct = async () => {
      validateName();
      validateDescription();
      validatePrice();
      validateQuantity();

      if (hasErrors.value) {
        return;
      }

      try {
        await axios.post('https://localhost:7144/api/Products', product.value, {
          headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${localStorage.getItem('token')}`
          }
        });
        router.push('/products');
      } catch (error) {
        console.log('Error response:', error.response);
        if (error.response && error.response.status === 400) {
          const errorData = error.response.data;
          console.log('Error data:', errorData);
          if (Array.isArray(errorData.errors)) {
            errorData.errors.forEach(err => {
              if (err.toLowerCase().includes('name')) {
                errors.name = err;
              } else if (err.toLowerCase().includes('description')) {
                errors.description = err;
              } else if (err.toLowerCase().includes('price')) {
                errors.price = err;
              } else if (err.toLowerCase().includes('quantity')) {
                errors.quantity = err;
              }
            });
          } else if (typeof errorData.errors === 'object') {
            Object.keys(errorData.errors).forEach(key => {
              if (key.toLowerCase() === 'name') {
                errors.name = errorData.errors[key].join(', ');
              } else if (key.toLowerCase() === 'description') {
                errors.description = errorData.errors[key].join(', ');
              } else if (key.toLowerCase() === 'price') {
                errors.price = errorData.errors[key].join(', ');
              } else if (key.toLowerCase() === 'quantity') {
                errors.quantity = errorData.errors[key].join(', ');
              }
            });
          } else if (errorData.message) {
            serverError.value = errorData.message;
          } else {
            serverError.value = 'An error occurred during product addition.';
          }
        } else {
          serverError.value = 'An error occurred during product addition.';
        }
      }
    };

    return {
      product,
      errors,
      serverError,
      validateName,
      validateDescription,
      validatePrice,
      validateQuantity,
      hasErrors,
      addProduct
    };
  }
};
</script>
