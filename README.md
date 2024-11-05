# E-Commerce API

## Overview

This E-Commerce API provides a robust and scalable solution for managing an online shopping platform. It offers essential features such as user authentication, product management, order processing, and review systems, allowing developers to build a complete e-commerce experience.

## Features

- **User Management**: Create, read, update, and delete user accounts. Supports user authentication and session management.
- **Product Management**: Add, update, delete, and retrieve products. Each product can have multiple categories, reviews, and associated images.
- **Category Management**: Create and manage product categories for better organization and searchability.
- **Shopping Cart**: Users can manage their shopping cart, add or remove items, and update quantities before checkout.
- **Order Processing**: Users can place orders, view their order history, and check the status of current orders.
- **Payment Integration**: Process payments securely and manage payment status for orders.
- **Reviews and Ratings**: Allow users to leave reviews and ratings for products they have purchased, helping others make informed decisions.
- **Wishlist**: Users can create and manage wishlists to save products for future purchases.

## API Endpoints

- **User Endpoints**
  - `POST /api/users/register`: Register a new user.
  - `POST /api/users/login`: Authenticate a user and retrieve a token.
  - `GET /api/users/{id}`: Retrieve user details.
  - `PUT /api/users/{id}`: Update user information.
  - `DELETE /api/users/{id}`: Delete a user account.

- **Product Endpoints**
  - `POST /api/products`: Add a new product.
  - `GET /api/products`: Retrieve a list of all products.
  - `GET /api/products/{id}`: Retrieve details of a specific product.
  - `PUT /api/products/{id}`: Update product information.
  - `DELETE /api/products/{id}`: Delete a product.

- **Category Endpoints**
  - `POST /api/categories`: Create a new category.
  - `GET /api/categories`: Retrieve a list of all categories.
  - `GET /api/categories/{id}`: Retrieve details of a specific category.
  - `PUT /api/categories/{id}`: Update category information.
  - `DELETE /api/categories/{id}`: Delete a category.

- **Order Endpoints**
  - `POST /api/orders`: Create a new order.
  - `GET /api/orders`: Retrieve a list of all orders.
  - `GET /api/orders/{id}`: Retrieve details of a specific order.
  - `PUT /api/orders/{id}`: Update order status.
  - `DELETE /api/orders/{id}`: Cancel an order.

- **Review Endpoints**
  - `POST /api/reviews`: Add a review for a product.
  - `GET /api/reviews/product/{productId}`: Retrieve all reviews for a specific product.
  - `GET /api/reviews/{id}`: Retrieve a specific review.
  - `PUT /api/reviews/{id}`: Update a review.
  - `DELETE /api/reviews/{id}`: Delete a review.

- **Cart Endpoints**
  - `POST /api/cart`: Create a shopping cart for a user.
  - `GET /api/cart/{userId}`: Retrieve items in a user's shopping cart.
  - `POST /api/cart/items`: Add an item to the cart.
  - `PUT /api/cart/items/{itemId}`: Update item quantity in the cart.
  - `DELETE /api/cart/items/{itemId}`: Remove an item from the cart.

## Technologies Used

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT for authentication
- Swagger for API documentation

## Getting Started

To get a local copy of this API up and running, follow these simple steps:

1. Clone the repository:
   ```bash
   git clone https://github.com/AdelMuhammad-23/E-CommerceProject.git

