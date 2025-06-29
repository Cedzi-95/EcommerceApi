# Improved Ecommerce API Design

## Data Models

### User (Identity)
```
- id: UUID (primary key)
- username: string (unique, indexed)
- email: string (unique, indexed)
- password_hash: string (hashed with bcrypt)
- first_name: string
- last_name: string
- phone: string (optional)
- email_verified: boolean
- is_active: boolean
- role: enum (CUSTOMER, ADMIN, VENDOR)
- created_at: timestamp
- updated_at: timestamp
- last_login: timestamp
```

### Address
```
- id: UUID (primary key)
- user_id: UUID (foreign key)
- type: enum (SHIPPING, BILLING)
- street_address: string
- city: string
- state: string
- postal_code: string
- country: string
- is_default: boolean
- created_at: timestamp
```

### Category
```
- id: UUID (primary key)
- name: string (indexed)
- slug: string (unique, indexed)
- description: text
- parent_id: UUID (foreign key, self-reference)
- image_url: string
- is_active: boolean
- sort_order: integer
- created_at: timestamp
- updated_at: timestamp
```

### Product
```
- id: UUID (primary key)
- name: string (indexed)
- slug: string (unique, indexed)
- description: text
- short_description: string
- sku: string (unique, indexed)
- price: decimal(10,2)
- compare_price: decimal(10,2) (optional)
- cost_price: decimal(10,2)
- track_inventory: boolean
- inventory_quantity: integer
- allow_backorder: boolean
- weight: decimal(8,2)
- dimensions: JSON {length, width, height}
- category_id: UUID (foreign key)
- brand: string
- tags: JSON array
- is_active: boolean
- is_featured: boolean
- meta_title: string
- meta_description: string
- created_at: timestamp
- updated_at: timestamp
```

### ProductImage
```
- id: UUID (primary key)
- product_id: UUID (foreign key)
- image_url: string
- alt_text: string
- sort_order: integer
- is_primary: boolean
```

### ProductVariant (for size, color, etc.)
```
- id: UUID (primary key)
- product_id: UUID (foreign key)
- name: string
- sku: string (unique)
- price: decimal(10,2)
- inventory_quantity: integer
- attributes: JSON {color, size, material, etc.}
- is_active: boolean
```

### Cart
```
- id: UUID (primary key)
- user_id: UUID (foreign key)
- session_id: string (for guest users)
- expires_at: timestamp
- created_at: timestamp
- updated_at: timestamp
```

### CartItem
```
- id: UUID (primary key)
- cart_id: UUID (foreign key)
- product_id: UUID (foreign key)
- product_variant_id: UUID (foreign key, optional)
- quantity: integer
- unit_price: decimal(10,2)
- total_price: decimal(10,2)
- created_at: timestamp
- updated_at: timestamp
```

### Order
```
- id: UUID (primary key)
- order_number: string (unique, indexed)
- user_id: UUID (foreign key)
- status: enum (PENDING, CONFIRMED, PROCESSING, SHIPPED, DELIVERED, CANCELLED, REFUNDED)
- payment_status: enum (PENDING, PAID, FAILED, REFUNDED)
- subtotal: decimal(10,2)
- tax_amount: decimal(10,2)
- shipping_amount: decimal(10,2)
- discount_amount: decimal(10,2)
- total_amount: decimal(10,2)
- currency: string
- shipping_address: JSON
- billing_address: JSON
- notes: text
- created_at: timestamp
- updated_at: timestamp
- shipped_at: timestamp
- delivered_at: timestamp
```

### OrderItem
```
- id: UUID (primary key)
- order_id: UUID (foreign key)
- product_id: UUID (foreign key)
- product_variant_id: UUID (foreign key, optional)
- product_name: string (snapshot)
- product_sku: string (snapshot)
- quantity: integer
- unit_price: decimal(10,2)
- total_price: decimal(10,2)
```

### Payment
```
- id: UUID (primary key)
- order_id: UUID (foreign key)
- payment_method: enum (CREDIT_CARD, PAYPAL, STRIPE, BANK_TRANSFER)
- payment_gateway: string
- transaction_id: string
- amount: decimal(10,2)
- currency: string
- status: enum (PENDING, COMPLETED, FAILED, REFUNDED)
- gateway_response: JSON
- created_at: timestamp
- updated_at: timestamp
```

### Coupon
```
- id: UUID (primary key)
- code: string (unique, indexed)
- type: enum (PERCENTAGE, FIXED_AMOUNT)
- value: decimal(10,2)
- minimum_order_amount: decimal(10,2)
- usage_limit: integer
- used_count: integer
- is_active: boolean
- starts_at: timestamp
- expires_at: timestamp
- created_at: timestamp
```

### Review
```
- id: UUID (primary key)
- product_id: UUID (foreign key)
- user_id: UUID (foreign key)
- order_id: UUID (foreign key)
- rating: integer (1-5)
- title: string
- comment: text
- is_verified_purchase: boolean
- is_approved: boolean
- created_at: timestamp
- updated_at: timestamp
```

---

## API Endpoints

### Authentication & Authorization
```
POST   /api/v1/auth/register
POST   /api/v1/auth/login
POST   /api/v1/auth/logout
POST   /api/v1/auth/refresh
POST   /api/v1/auth/forgot-password
POST   /api/v1/auth/reset-password
POST   /api/v1/auth/verify-email
```

### User Management
```
GET    /api/v1/users/profile
PUT    /api/v1/users/profile
DELETE /api/v1/users/account
GET    /api/v1/users/addresses
POST   /api/v1/users/addresses
PUT    /api/v1/users/addresses/{id}
DELETE /api/v1/users/addresses/{id}

# Admin only
GET    /api/v1/admin/users
GET    /api/v1/admin/users/{id}
PUT    /api/v1/admin/users/{id}
DELETE /api/v1/admin/users/{id}
```

### Categories
```
GET    /api/v1/categories
GET    /api/v1/categories/{id}
GET    /api/v1/categories/{id}/products

# Admin only
POST   /api/v1/admin/categories
PUT    /api/v1/admin/categories/{id}
DELETE /api/v1/admin/categories/{id}
```

### Products
```
GET    /api/v1/products
  Query params: 
  - page, limit (pagination)
  - category_id, brand, tags (filtering)
  - min_price, max_price (price range)
  - sort (price_asc, price_desc, name_asc, name_desc, created_desc)
  - search (full-text search)
  - is_featured (boolean)

GET    /api/v1/products/{id}
GET    /api/v1/products/{id}/variants
GET    /api/v1/products/{id}/reviews
POST   /api/v1/products/{id}/reviews (auth required)

# Admin only
POST   /api/v1/admin/products
PUT    /api/v1/admin/products/{id}
DELETE /api/v1/admin/products/{id}
POST   /api/v1/admin/products/{id}/images
DELETE /api/v1/admin/products/{id}/images/{image_id}
```

### Cart
```
GET    /api/v1/cart
POST   /api/v1/cart/items
PUT    /api/v1/cart/items/{id}
DELETE /api/v1/cart/items/{id}
DELETE /api/v1/cart/clear
POST   /api/v1/cart/apply-coupon
DELETE /api/v1/cart/remove-coupon
```

### Orders
```
POST   /api/v1/orders (create from cart)
GET    /api/v1/orders
GET    /api/v1/orders/{id}
PUT    /api/v1/orders/{id}/cancel

# Admin only
GET    /api/v1/admin/orders
PUT    /api/v1/admin/orders/{id}/status
```

### Payments
```
POST   /api/v1/payments/process
GET    /api/v1/payments/{id}/status
POST   /api/v1/payments/{id}/refund (admin only)
```

### Coupons
```
POST   /api/v1/coupons/validate
GET    /api/v1/admin/coupons (admin only)
POST   /api/v1/admin/coupons (admin only)
PUT    /api/v1/admin/coupons/{id} (admin only)
DELETE /api/v1/admin/coupons/{id} (admin only)
```

---

## Controllers & Services Architecture

### Controllers
- **AuthController**: Handle authentication, registration, password reset
- **UserController**: User profile, addresses management
- **CategoryController**: Category CRUD operations
- **ProductController**: Product catalog, search, filtering
- **CartController**: Shopping cart management
- **OrderController**: Order creation, tracking, history
- **PaymentController**: Payment processing integration
- **ReviewController**: Product reviews and ratings
- **CouponController**: Discount code management
- **AdminController**: Admin-specific operations

### Services
- **AuthService**: JWT token management, password hashing
- **UserService**: User business logic
- **ProductService**: Product catalog, inventory management
- **CartService**: Cart operations, price calculations
- **OrderService**: Order processing, status updates
- **PaymentService**: Payment gateway integration
- **EmailService**: Transactional emails
- **NotificationService**: Push notifications
- **SearchService**: Product search and filtering
- **InventoryService**: Stock management
- **CouponService**: Discount calculations

### Repositories
- **UserRepository**: User data access with Identity integration
- **ProductRepository**: Product queries with caching
- **CategoryRepository**: Hierarchical category queries
- **OrderRepository**: Order data with complex filtering
- **CartRepository**: Cart persistence
- **PaymentRepository**: Payment transaction logs

---

## Security & Best Practices

### Authentication & Authorization
- JWT tokens with refresh token rotation
- Role-based access control (RBAC)
- API rate limiting (different limits for auth/unauth users)
- Request/response logging
- Input validation and sanitization

### Data Protection
- Password hashing with bcrypt
- Sensitive data encryption at rest
- HTTPS only
- CORS configuration
- SQL injection prevention

### Performance
- Database indexing on frequently queried fields
- Redis caching for products, categories
- Image optimization and CDN
- API response compression
- Database connection pooling

### Monitoring & Logging
- Structured logging with correlation IDs
- Error tracking and alerting
- Performance monitoring
- Database query optimization
- Health check endpoints

### API Design
- RESTful conventions
- Consistent error responses
- API versioning
- OpenAPI/Swagger documentation
- Pagination for list endpoints
- Soft deletes for important data

---

## Integration Points

### External Services
- **Payment Gateways**: Stripe, PayPal, Square
- **Email Service**: SendGrid, AWS SES
- **File Storage**: AWS S3, Cloudinary for images
- **Search Engine**: Elasticsearch for advanced product search
- **Analytics**: Google Analytics, custom event tracking
- **Shipping**: FedEx, UPS, DHL API integration

### Additional Features
- **Wishlist**: Save products for later
- **Recently Viewed**: Track user browsing history
- **Recommendations**: Product recommendation engine
- **Notifications**: Order status updates, promotions
- **Multi-language**: i18n support
- **Multi-currency**: Currency conversion
- **Bulk Operations**: Admin bulk product updates
- **Export/Import**: CSV product imports
- **Audit Trail**: Track all admin actions