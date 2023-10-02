# ByteStore Ecommerce project - WIP 🤑

This fullstack project is an implementation of an E-commerce called ByteStore. The project has some entities like Users and their Shopping Carts, Products and Orders. The project also has a user authentication and authorization system using JSON Web Tokens (JWT). The project has been developed using .NET, Entity Framework Core, Angular, MySQL, JWT and Redis cache.

## Features

The user authentication system includes the following features:

- **Signup**: Users can register in the system by providing a username and password. Passwords are processed using a hash and salt algorithm before being stored in the database. This ensures the security of passwords, protecting users' sensitive information. Usernames must be unique.

- **Signin**: Registered users can log in by providing their credentials (username and password). The system validates the credentials and generates a valid JWT token, which is returned to the user. This token can be used later to access protected routes and resources.

- **Add to cart and make a buy order**: Users can add products to their shopping carts. After that, they can make a buy order.

- **Edit a User**: Also, the user can edit some informations that where given in the registration screen. 

- **In-Memory Cache**: An in-memory cache has been implemented using the MemoryCache class provided by ASP.NET Core within the Product CRUD. The cache is used to temporarily store the data of products retrieved from the database. This cache reduces response time by serving data directly from memory instead of querying the database on every request.

- **Distributed Caching with Redis**: In the newer version of the project, I have created an in-memory cache with a distributed cache using Redis. To do so, I created a controller, service and repository only to use this feature. The Redis distributed cache improves the performance and scalability of the application by storing cached data in a distributed memory store accessible by all instances of the application. 

    - To use the Redis distributed cache, I have added the `StackExchange.Redis` package and configured it in the `Program.cs` file. Besides that, I'm running a local instance of Redis on docker: ```docker run -d -p 6379:6379 --name redis redis```

    - When the `GetAllProductsAsync` method in the `ProductRepository` is called, it first checks if the requested data exists in the Redis cache. If the data is found, it is returned directly from the cache. If not, the data is fetched from the database, serialized, and stored in the Redis cache for future use.

    - This distributed cache allows the application to share cached data across multiple instances, reducing the need to query the database frequently and improving overall performance.
