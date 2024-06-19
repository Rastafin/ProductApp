<template>
  <div class="container mt-5">
    <h1>Products</h1>
    <table class="table text-center">
      <thead>
        <tr>
          <th scope="col">#</th>
          <th scope="col">Name</th>
          <th scope="col">Description</th>
          <th scope="col">Price</th>
          <th scope="col">Quantity</th>
          <th scope="col">Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="product in products" :key="product.id">
          <th scope="row">{{ product.id }}</th>
          <td>{{ product.name }}</td>
          <td>{{ product.description }}</td>
          <td>{{ product.price }}</td>
          <td>{{ product.quantity }}</td>
          <td class="px-2">
            <router-link :to="'/products/' + product.id" class="btn btn-primary btn-sm mx-1">Details</router-link>
            <router-link :to="'/edit-product/' + product.id" class="btn btn-warning btn-sm mx-1">Edit</router-link>
            <button @click="deleteProduct(product.id)" class="btn btn-danger btn-sm mx-1">Delete</button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name: 'ProductsPage',
  data() {
    return {
      products: []
    };
  },
  created() {
    this.fetchProducts();
  },
  methods: {
    fetchProducts() {
      axios.get('https://localhost:7144/api/Products', {
          headers: {
            'Authorization': `Bearer ${localStorage.getItem('token')}`
          }
        })
        .then(response => {
          this.products = response.data;
        })
        .catch(error => {
          console.log(error);
        });
    },
    deleteProduct(id) {
      if (confirm('Are you sure you want to delete this product?')) {
        axios.delete(`https://localhost:7144/api/Products/${id}`,{
          headers: {
            'Authorization': `Bearer ${localStorage.getItem('token')}`
          }
        })
          .then(() => {
            this.fetchProducts(); 
          })
          .catch(error => {
            console.log(error);
          });
      }
    }
  }
};
</script>