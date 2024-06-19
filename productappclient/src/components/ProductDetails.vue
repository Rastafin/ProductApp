<template>
  <div class="col-md-2 offset-md-5 text-center mt-5">
    <div v-if="product">
      <h1 class="mb-5 text-muted">Product Details</h1>
      <form class="form-floating m-4">
        <input type="text" id="name" class="form-control fs-3" :value="product.name" disabled>
        <label for="name">Name</label>
      </form>

      <form class="form-floating m-4">
        <input type="text" id="description" class="form-control fs-3" :value="product.description" disabled>
        <label for="description">Description</label>
      </form>
        
      <form class="form-floating m-4">
        <input type="text" id="price" class="form-control fs-3" :value="product.price" disabled>
        <label for="price">Price</label>
      </form>

      <form class="form-floating m-4">
        <input type="text" id="quantity" class="form-control fs-3" :value="product.quantity" disabled>
        <label for="quantity">Quantity</label>
      </form>

      <button class="btn btn-primary mt-4" @click="goToEdit">Go to Edit</button>
    </div>
    <div v-else>
      <p v-if="error" class="text-danger">{{ error }}</p>
      <p v-else>Loading...</p>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      product: null,
      error: null
    };
  },
  created() {
    this.fetchProductDetails();
  },
  methods: {
    fetchProductDetails() {
      const productId = this.$route.params.id;
      axios.get(`https://localhost:7144/api/Products/${productId}`,{
          headers: {
            'Authorization': `Bearer ${localStorage.getItem('token')}`
          }
        })
        .then(response => {
          this.product = response.data;
        })
        .catch(error => {
          console.error('There was an error fetching the product details:', error);
          this.error = 'Failed to load product details.';
        });
    },
    goToEdit() {
      this.$router.push(`/edit-product/${this.$route.params.id}`);
    }
  }
};
</script>
