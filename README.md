# User Authentication Project Using JWT and In-Memory Cache

This project is an implementation of a user authentication system using JSON Web Tokens (JWT). It has been developed using the .NET programming language, Entity Framework Core, and the MySQL database.

## Features

The user authentication system includes the following features:

- **Signup**: Users can register in the system by providing a username and password. User passwords are processed using a hash and salt algorithm before being stored in the database. This ensures the security of passwords, protecting users' sensitive information.

- **Signin**: Registered users can log in by providing their credentials (username and password). The system validates the credentials and generates a valid JWT token, which is returned to the user. This token can be used later to access protected routes and resources.

- **In-Memory Cache**: An in-memory cache has been implemented using the MemoryCache class provided by ASP.NET Core within the Product CRUD. The cache is used to temporarily store the data of products retrieved from the database. This cache reduces response time by serving data directly from memory instead of querying the database on every request.
