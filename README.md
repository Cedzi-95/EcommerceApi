# EcommerceApi

A **modern e-commerce REST API** built with **ASP.NET Core Web API** and **Entity Framework Core**, deployed to **Azure App Service** with a **Supabase PostgreSQL** database and automated CI/CD via **GitHub Actions**.

This project demonstrates backend development best practices including clean architecture, JWT authentication, repository/service patterns, and cloud deployment.

---

##  Live Demo

**API (Swagger):** https://cedricecommerce-cbacexb4epb9fdek.swedencentral-01.azurewebsites.net

---

##  Features

- RESTful API design with proper HTTP status codes
- ASP.NET Core Web API (.NET 9)
- Entity Framework Core with PostgreSQL
- JWT-based authentication & role-based authorization (Admin/User)
- Product & category management
- Shopping cart with add/remove items
- Order management with stock decrement on checkout
- Order status & payment status tracking
- DTO-based request/response models
- Repository & service layer pattern
- AutoMapper for object mapping
- Global error handling
- Database migrations

---

##  Tech Stack

- **.NET 9 / ASP.NET Core Web API**
- **Entity Framework Core**
- **PostgreSQL (Supabase)**
- **JWT Authentication**
- **ASP.NET Identity**
- **AutoMapper**
- **Swagger / OpenAPI**

---

##  Deployment

- **API Hosting:** Azure App Service (Linux)
- **Database:** Supabase PostgreSQL (Session Pooler)
- **CI/CD:** GitHub Actions -> auto-deploys on push to `main`
- **Environment config:** Azure App Service Environment Variables

---

##  API Endpoints

### Auth
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/user/register` | Register new user |
| POST | `/user/login` | Login and get JWT token |

### Products
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Product/all` | Get all products |
| GET | `/api/Product/{id}` | Get product by ID |
| GET | `/api/Product/category/{id}` | Get products by category |
| POST | `/api/Product/create` | Create product (Admin) |
| PUT | `/api/Product/update/{id}` | Update product (Admin) |
| DELETE | `/api/Product/delete/{id}` | Delete product (Admin) |

### Categories
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Category/GetAll` | Get all categories |
| GET | `/api/Category/{id}` | Get category by ID |
| POST | `/api/Category/create` | Create category (Admin) |

### Cart
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Cart/Get` | Get current user's cart |
| POST | `/api/Cart/Add` | Add item to cart |
| DELETE | `/api/Cart/remove/{productId}` | Remove item from cart |

### Orders
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/Order/create` | Create order from cart |
| GET | `/api/Order/my-orders` | Get current user's orders |
| GET | `/api/Order/{orderId}` | Get order by ID |
| GET | `/api/Order/GetAll` | Get all orders (Admin) |
| PUT | `/api/Order/OrderStatus` | Update order status |
| PUT | `/api/Order/PaymentStatus` | Update payment status |

---

##  Getting Started

### Prerequisites
- .NET 9 SDK
- PostgreSQL or Docker
- Git

### Run locally

```bash
git clone https://github.com/Cedzi-95/EcommerceApi.git
cd EcommerceApi
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "your-connection-string"
dotnet user-secrets set "ApplicationSettings:JWT_Secret" "your-jwt-secret"
dotnet ef database update
dotnet run
```

Navigate to `http://localhost:5181` to access Swagger UI.
