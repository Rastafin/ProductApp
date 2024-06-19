<template>
  <div class="container mt-5">
    <h1>Edit Product</h1>
    <form @submit.prevent="updateProduct">
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
          step="0.01"
          id="price" 
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
      <button type="submit" class="btn btn-primary" :disabled="hasErrors">Update Product</button>
    </form>
  </div>
</template>

<script>
import axios from 'axios';
import { ref, reactive, computed, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';

export default {
  name: 'EditProduct',
  setup() {
    const route = useRoute();
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

    const fetchProduct = async () => {
      try {
        const response = await axios.get(`https://localhost:7144/api/Products/${route.params.id}`, {
          headers: {
            'Authorization': `Bearer ${localStorage.getItem('token')}`
          }
        });
        product.value = response.data;
      } catch (error) {
        console.log(error);
      }
    };

    const updateProduct = async () => {
      validateName();
      validateDescription();
      validatePrice();
      validateQuantity();

      if (hasErrors.value) {
        return;
      }

      try {
        await axios.put(`https://localhost:7144/api/Products/${route.params.id}`, product.value, {
          headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${localStorage.getItem('token')}`
          }
        });
        router.push('/products');
      } catch (error) {
        console.log(error);
      }
    };

    onMounted(fetchProduct);

    return {
      product,
      errors,
      validateName,
      validateDescription,
      validatePrice,
      validateQuantity,
      hasErrors,
      updateProduct
    };
  }
};
</script>
