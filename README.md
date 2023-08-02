# User Authentication Project Using JWT and In-Memory Cache

This project is an implementation of a user authentication system using JSON Web Tokens (JWT). It has been developed using the .NET programming language, Entity Framework Core, and the MySQL database.

## Features

The user authentication system includes the following features:

- **Signup**: Users can register in the system by providing a username and password. User passwords are processed using a hash and salt algorithm before being stored in the database. This ensures the security of passwords, protecting users' sensitive information.

- **Signin**: Registered users can log in by providing their credentials (username and password). The system validates the credentials and generates a valid JWT token, which is returned to the user. This token can be used later to access protected routes and resources.

- **In-Memory Cache**: An in-memory cache has been implemented using the MemoryCache class provided by ASP.NET Core within the Product CRUD. The cache is used to temporarily store the data of products retrieved from the database. This cache reduces response time by serving data directly from memory instead of querying the database on every request.

- **Distributed Caching with Redis**: In the newer version of the project, I have replaced the in-memory cache with a distributed cache using Redis. The Redis distributed cache improves the performance and scalability of the application by storing cached data in a distributed memory store accessible by all instances of the application. 

    - To use the Redis distributed cache, I have added the `StackExchange.Redis` package and configured it in the `Program.cs` file. Besides that, I'm running a local instance of Redis on docker: ```docker run -d -p 6379:6379 --name redis redis```

    - When the `GetAllProductsAsync` method in the `ProductRepository` is called, it first checks if the requested data exists in the Redis cache. If the data is found, it is returned directly from the cache. If not, the data is fetched from the database, serialized, and stored in the Redis cache for future use. We have set an absolute expiration of 15 seconds for the cached data to ensure it remains up-to-date with changes in the database.

    - This distributed cache allows the application to share cached data across multiple instances, reducing the need to query the database frequently and improving overall performance.
