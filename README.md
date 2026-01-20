# EcommerceApi

A **modern e-commerce REST API** built with **ASP.NET Core Web API** and **Entity Framework Core**.  
This project provides the backend foundation for an online store, including products, categories, carts, orders, and user authentication.

The goal of this project is to demonstrate **clean architecture**, **best practices**, and **scalable backend design** for a real-world e-commerce system.

---

##  Features

- RESTful API design
- ASP.NET Core Web API (.NET 9)
- Entity Framework Core with PostgreSQL
- JWT-based authentication & role-based authorization
- Product & category management
- Shopping cart functionality
- Order management
- DTO-based request/response models
- Repository & service layers
- AutoMapper for object mapping
- Database migrations

---

##  Tech Stack

- **.NET 9 / ASP.NET Core Web API**
- **Entity Framework Core**
- **PostgreSQL (Npgsql provider)**
- **JWT Authentication**
- **ASP.NET Identity**
- **AutoMapper**

---

##  Project Structure

```text
EcommerceApi
│
├── Controllers        # API endpoints
├── Data               # DbContext & database configuration
├── Helpers            # Authentication helpers & role seeding
├── Interfaces         # Repository & service contracts
├── Mappings           # AutoMapper profiles
├── Migrations         # EF Core migrations
├── Models             # Domain entities
│   └── DTO            # Data Transfer Objects
├── Repositories       # Data access layer
├── Services           # Business logic layer
├── Ecommerce          # Bruno API client collections
└── Program.cs         # Application entry point
