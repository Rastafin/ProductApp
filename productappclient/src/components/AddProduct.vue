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
import { reactive} from 'vue';

export default {
  name: 'AddProduct',
  data() {
    return {
      product: {
        name: '',
        description: '',
        price: 0,
        quantity: 0
      },
      errors: reactive({
        name: null,
        description: null,
        price: null,
        quantity: null
      })
    };
  },
  computed: {
    hasErrors() {
      return !!this.errors.name || !!this.errors.description || !!this.errors.price || !!this.errors.quantity;
    }
  },
  methods: {
    validateName() {
      this.errors.name = this.product.name.length > 0 ? null : 'Name is required.';
    },
    validateDescription() {
      this.errors.description = this.product.description.length > 0 ? null : 'Description is required.';
    },
    validatePrice() {
      this.errors.price = this.product.price > 0 ? null : 'Price must be greater than 0.';
    },
    validateQuantity() {
      this.errors.quantity = Number.isInteger(this.product.quantity) && this.product.quantity > 0 ? null : 'Quantity must be a natural number.';
    },
    addProduct() {
      this.validateName();
      this.validateDescription();
      this.validatePrice();
      this.validateQuantity();

      if (this.hasErrors) {
        return;
      }

      axios.post('https://localhost:7144/api/Products', this.product, {
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${localStorage.getItem('token')}`
        }
      })
      .then(() => {
        this.$router.push('/products');
      })
      .catch(error => {
        console.log(error);
      });
    }
  }
};
</script>
