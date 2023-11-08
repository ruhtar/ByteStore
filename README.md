# ByteStore Ecommerce project - WIP 🤑

This fullstack project is an implementation of an E-commerce called ByteStore. The application has some entities like Users and their Shopping Carts, Products and OrderItems. The project also has a user authentication and authorization system using JSON Web Tokens (JWT). The project has been developed using .NET, Entity Framework Core, Angular, MySQL, JWT, Docker, Firebase and Redis cache.

## Features

ByteStore system, includes the following features:

- **Signup**: Users can register in the system by providing a username, password and their addresses. Usernames must be unique. Passwords must have capital letters, number and special characters and are processed using a hash and salt algorithm before being stored in the database. This ensures the security of passwords, protecting users' sensitive information.

- **Signin**: Registered users can log in by providing their credentials (username and password). The system validates the credentials and generates a valid JWT token, which is stored on Local Storage. This token can be used later to give the system some crucial informations and access protected routes and resources.

- **Show products**: The user can view the products available in the store. The user can use name and price filters for better navigation and can also sort the list of products in alphabetical and price order, in ascending and descending order. 

- **Add to shopping cart**: Users can add products to their shopping carts. After that, they can make a buy order. If a user is not logged in, they cannot check their carts and will be warned about that.

- **Edit User Information**: Also, the user can edit some informations that where given in the registration screen, like their address and password. 

- **In-Memory Cache**: An in-memory cache has been implemented using the MemoryCache class provided by ASP.NET Core within the Product CRUD. The cache is used to temporarily store the data of products retrieved from the database. This cache reduces response time by serving data directly from memory instead of querying the database on every request.

- **Distributed Caching with Redis**: The system has an in-memory cache with a distributed cache using Redis. The Redis distributed cache improves the performance and scalability of the application by storing cached data in a distributed memory store accessible by all instances of the application. 

    - When the `GetAllProductsAsync` method in the `ProductRepository` is called, it first checks if the requested data exists in the Redis cache. If the data is found, it is returned directly from the cache. If not, the data is fetched from the database, serialized, and stored in the Redis cache for future use.

    - This distributed cache allows the application to share cached data across multiple instances, reducing the need to query the database frequently and improving overall performance.
