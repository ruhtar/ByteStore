# ByteStore🛒🤑

![ByteStoreLogo](https://i.postimg.cc/zD355jBh/bytestore.jpg)

Welcome to ByteStore, your go-to fullstack e-commerce platform! 🚀

This project is a personal endeavor, crafted to enhance proficiency in various technologies and patterns. ByteStore offers a comprehensive range of features that cater to both customers and the security-conscious, all built on a robust tech stack/patterns that includes:

- .NET 6,
- Entity Framework Core,
- Angular,
- MySQL,
- JWT,
- Docker, 
- Firebase,
- Redis cache distributed cache,
- Unit tests and integration tests using XUnit and Moq,
- Repository Pattern,
- Domain Services,
- Domain Driven Design

## Project Overview

### Core Entities:

- **Users and Shopping Carts**: ByteStore allows users to create accounts and manage their shopping carts. Users can seamlessly modify the quantity of products, remove items, view the total balance of their cart, and proceed to checkout.

- **Products and OrderItems**: The platform offers a catalog of products and users can add them to their shopping carts.

- **Products reviews**: The platform shows how many people have reviewed a product. You can also check each individual rate followed by a comment about the purchase.

### User Authentication/Authorization and Security:

- **Signup**: Users can sign up by providing a unique username, a secure password, and their address information. Passwords undergo hashing (`PBKDF2`) and salting for maximum security, ensuring the safety of sensitive user data. Passwords must have capital letters, number and special characters.

- **Signin**: Registered users can log in with their credentials, and the system generates a JWT token that is stored in Local Storage. This token is validated on the backend and gives the system important information and grants access to protected routes and resources.

### Shopping Features:

- **View Products**: Users can explore the product listings in the store and check products photos, prices, number of items in stock, descriptions and check other user's reviews about a specific product. All of this is complete with filters for names and prices. Sorting options by alphabetical and price order, in ascending and descending order, make navigation easier. Also, there is a pagination system in frontend and backend for more usability and scalability.

- **Shopping Cart**: Logged-in users can add and remove products to their shopping carts and proceed to make purchases. If a user isn't logged in, they'll be prompted to do so before accessing their cart.

- **Edit Shopping Cart Items**: Users have the flexibility to adjust item quantities or remove items from their shopping carts. They can also check their Cart Summary to see the total cost of their purchase.

### User Interactions:

- **Product Reviews**: Authenticated users can leave reviews for products they've purchased. Only items in their purchase history are available for review. Users can rate products from 0 to 5 and leave comments to share their experiences.

- **Edit User Information**: Users can update their registration information, including addresses and passwords, at their convenience.

- **Check purchase history**: Users can check on `My Account` a list of bought products, their quantity, their price and the date that the purchase was made.

### Data storage:

- **Database Design**: ByteStore utilizes the MySQL relational database with the following relationships:

    - 1:1 (One-to-One): Linking related data in a one-to-one relationship, like Users and their ShoppingCarts.
    - 1:N (One-to-Many): Managing relationships where one entity is related to multiple others, like Reviews of a Product.
    - N:N (Many-to-Many): Handling complex associations between multiple entities, like Products and ShoppingCarts.

- **Image Storage**: In adherence to best practices, ByteStore avoids storing images directly in the database. Instead, images are securely managed in a FileStorage system hosted on Firebase. This approach optimizes performance and promotes cleaner database management.

### Caching:

- **Distributed Caching with Redis**: The system takes performance a step further with distributed caching using Redis. This distributed cache enhances application performance and scalability by storing cached data in a distributed memory store accessible by all instances of the application.

    - When the `GetProductById` method in the `ProductRepository` is invoked, it first checks if the requested data exists in the Redis cache. If found, it's served directly from the cache. If not, the data is fetched from the database, serialized, and stored in Redis for future use.
 
    - To keep cache's consistency, the `UpdateProduct` method in `ProductRepository` also updates cache's data. Also, when a product is deleted via `DeleteProduct` method, its cache is invalidated.

    - This distributed cache enables the application to share cached data across multiple instances, reducing the need for frequent database queries and delivering improved overall performance.
 
# Running the app:

- **Auto Migrator**: ByteStore has an Auto Migrator that... duh... runs the database migrations automatically, so don't worry about them.

- **Seeder**: ByteStore also has a builtin Seeder Hosted Service that will populate the database on the first time you run the application. It will create some products and create a user called `Admin`, with a password that is `!123Qwe`, that you can use right away.
  
- **Docker and Docker Compose**: To run the application with no problems and little setup, just clone the repository using `git clone https://github.com/ruhtar/ByteStore.git` and run a `docker compose up -d` on the root folder and the application will do the rest for you. After that, browse to `http://localhost:4200/`.

# API Endpoint Description

| Resource                                           | Method | Endpoint                                    |
| -------------------------------------------------- | ------ | ------------------------------------------- |
| Retrieve a list of available products.             | GET    | /products                                  |
| Create a new product.                               | POST   | /products                                  |
| Retrieve details of a specific product.          | GET    | /products/{id}                             |
| Update details of a specific product.           | PUT    | /products/{id}                             |
| Delete a specific product.                           | DELETE | /products/{id}                             |
| Add a review for a product.                          | POST   | /products/reviews                          |
| Retrieve a list of product reviews.                | GET    | /products/reviews                          |
| Make an order of a product to the user's cart.   | POST   | /carts                                     |
| Complete the purchase of the user's cart.     | GET    | /carts                                     |
| Retrieve the shopping cart for a user.           | GET    | /carts/users/{id}            |
| Register a new user.                                    | POST   | /users/signup                              |
| Authenticate a user.                                    | POST   | /users/signin                              |
| Update the address of a user.                   | PUT    | /users/{id}/address                   |
| Retrieve the address of a user.                  | GET    | /users/{id}/address                   |
| Change the password of a user.                | PUT    | /users/change-password                    |
| Retrieve the purchase history of a user.     | GET    | /users/purchase-history                   |
| Check if a product has been purchased by a user. | GET    | /users/purchase-history/check             |


ByteStore is an evolving e-commerce platform that's continually being improved and expanded. We're excited to offer a seamless shopping experience while prioritizing security and performance. Thank you for being a part of ByteStore! 🛒🚀
