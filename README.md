# ByteStore Ecommerce Project - Work in Progress 🤑

Welcome to ByteStore, your go-to fullstack e-commerce platform! ByteStore offers a comprehensive range of features that cater to both customers and the security-conscious, all built on a robust tech stack that includes .NET, Entity Framework Core, Angular, MySQL, JWT, Docker, Firebase, and Redis cache.

## Project Overview

### Core Entities:

- **Users and Shopping Carts**: ByteStore allows users to create accounts and manage their shopping carts.

- **Products and OrderItems**: The platform offers a catalog of products and users can add items to their shopping carts.

### User Authentication and Security:

- **Signup**: Users can sign up by providing a unique username, a secure password, and their address information. Passwords undergo hashing and salting for maximum security, ensuring the safety of sensitive user data. Passwords must have capital letters, number and special characters.

- **Signin**: Registered users can log in with their credentials, and the system generates a JWT token that is stored in Local Storage. This token gives the system important information and grants access to protected routes and resources.

### Shopping Features:

- **View Products**: Users can explore the product listings in the store and check products photos, prices, number of items in stock, descriptions and check other user's reviews about a specific product. All of this is complete with filters for names and prices. Sorting options by alphabetical and price order, in ascending and descending order, make navigation easier. Also, there is a pagination system in frontend and backend for more usability.

- **Shopping Cart**: Logged-in users can add and remove products to their shopping carts and proceed to make purchases. If a user isn't logged in, they'll be prompted to do so before accessing their cart.

- **Edit Shopping Cart Items**: Users have the flexibility to adjust item quantities or remove items from their shopping carts.

### User Interactions:

- **Product Reviews**: Authenticated users can leave reviews for products they've purchased. Only items in their purchase history are available for review. Users can rate products from 0 to 5 and leave comments to share their experiences.

- **Edit User Information**: Users can update their registration information, including addresses and passwords, at their convenience.

### Caching for Performance:

- **Distributed Caching with Redis**: The system takes performance a step further with distributed caching using Redis. This distributed cache enhances application performance and scalability by storing cached data in a distributed memory store accessible by all instances of the application.

    - When the `GetAllProductsAsync` method in the `ProductRepository` is invoked, it first checks if the requested data exists in the Redis cache. If found, it's served directly from the cache. If not, the data is fetched from the database, serialized, and stored in Redis for future use.

    - This distributed cache enables the application to share cached data across multiple instances, reducing the need for frequent database queries and delivering improved overall performance.

# API Endpoint Description

## Products

### 1. List Products
- **Method:** GET
- **Endpoint:** /products

### 2. Create New Product
- **Method:** POST
- **Endpoint:** /products

### 3. Get Product Details
- **Method:** GET
- **Endpoint:** /products/{id}

### 4. Update Product
- **Method:** PUT
- **Endpoint:** /products/{id}

### 5. Delete Product
- **Method:** DELETE
- **Endpoint:** /products/{id}

### 6. Add Product Review
- **Method:** POST
- **Endpoint:** /products/reviews

### 7. List Product Reviews
- **Method:** GET
- **Endpoint:** /products/reviews

## Shopping Cart

### 8. Create Cart
- **Method:** POST
- **Endpoint:** /carts

### 9. List Carts
- **Method:** GET
- **Endpoint:** /carts

### 10. Get User's Cart
- **Method:** GET
- **Endpoint:** /carts/users/{userAggregateId}

### 11. Delete User's Cart
- **Method:** DELETE
- **Endpoint:** /carts/users/{userAggregateId}

## User

### 12. Sign Up
- **Method:** POST
- **Endpoint:** /users/signup

### 13. Sign In
- **Method:** POST
- **Endpoint:** /users/signin

### 14. Update User Address
- **Method:** PUT
- **Endpoint:** /users/{userId}/address

### 15. Get User Address
- **Method:** GET
- **Endpoint:** /users/{userId}/address

### 16. Change User Password
- **Method:** PUT
- **Endpoint:** /users/change-password

### 17. User Purchase History
- **Method:** GET
- **Endpoint:** /users/purchase-history

### 18. Check User Purchase History
- **Method:** GET
- **Endpoint:** /users/purchase-history/check


ByteStore is an evolving e-commerce platform that's continually being improved and expanded. We're excited to offer a seamless shopping experience while prioritizing security and performance. Thank you for being a part of ByteStore! 🛒🚀
