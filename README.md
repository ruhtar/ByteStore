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

| Resource               | Method | Endpoint                                    |
| ---------------------- | ------ | ------------------------------------------- |
| List Products          | GET    | /products                                  |
| Create New Product     | POST   | /products                                  |
| Get Product Details    | GET    | /products/{id}                             |
| Update Product         | PUT    | /products/{id}                             |
| Delete Product         | DELETE | /products/{id}                             |
| Add Product Review     | POST   | /products/reviews                          |
| List Product Reviews   | GET    | /products/reviews                          |
| Create Cart            | POST   | /carts                                     |
| List Carts             | GET    | /carts                                     |
| Get User's Cart        | GET    | /carts/users/{userAggregateId}            |
| Delete User's Cart     | DELETE | /carts/users/{userAggregateId}            |
| Sign Up                | POST   | /users/signup                              |
| Sign In                | POST   | /users/signin                              |
| Update User Address    | PUT    | /users/{userId}/address                   |
| Get User Address       | GET    | /users/{userId}/address                   |
| Change User Password   | PUT    | /users/change-password                    |
| User Purchase History  | GET    | /users/purchase-history                   |
| Check User Purchase History | GET | /users/purchase-history/check             |


ByteStore is an evolving e-commerce platform that's continually being improved and expanded. We're excited to offer a seamless shopping experience while prioritizing security and performance. Thank you for being a part of ByteStore! 🛒🚀
