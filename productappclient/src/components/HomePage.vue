<template>
    <div class="container mt-5">
      <div class="jumbotron">
        <h1 class="display-4">Welcome to Product App</h1>
        <p class="lead">This is a simple application built with Vue.js and ASP.NET Core.</p>
        <hr class="my-4">
        <p>Use this application to manage your products.</p>
        <router-link class="btn btn-primary btn-lg" to="/products" role="button">View Products</router-link>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    name: 'HomePage'
  };
  </script>