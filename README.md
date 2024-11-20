
# **E-Commerce API**

## **Overview**
The **E-Commerce API** is a backend solution built using **ASP.NET Core**, designed to handle e-commerce operations like product management, category organization, and secure user authentication. The project architecture emphasizes separation of concerns and maintainability, drawing concepts from **Clean Architecture** and **Onion Architecture** principles. This ensures high scalability and code reusability.

---

## **Features**
### **Core Architecture**
1. **Layered Structure**: Organized into layers (API, Core, and Infrastructure) for maintainability and scalability.  
2. **CQRS Pattern**: Separates command and query responsibilities for better performance and readability.  
3. **Generic Repository Pattern**: Simplifies database interactions and enhances reusability.  

### **API Capabilities**
4. **Pagination**: Enables efficient data retrieval for large datasets.  
5. **Input Validation**: Ensures data integrity using **FluentValidation**.  
6. **Swagger UI**: Built-in API documentation with support for JWT authentication.

### **Security**
7. **JWT Authentication**: Secures API endpoints with token-based authentication.  
8. **Role-Based Access**: Manages user permissions using roles.  
9. **CORS Support**: Allows controlled cross-origin requests.

### **File Management**
10. **Image Upload**: Handles product image uploads with proper storage integration.
    
### **Database Operations**
11. **Entity Configuration**: Managed using **Fluent API** and conventions.  
12. **Database Initialization**: Easy setup with automated migrations.  

### **Utilities**
13. **File Management**: Handles file uploads and storage efficiently.  
14. **Logging**: Tracks application activity for debugging and monitoring.

### **Testing**
15. **Unit Tests**: Ensures API reliability using **XUnit**.

---

## **Technologies Used**
- **ASP.NET Core Web API**  
- **Entity Framework Core**  
- **JWT Authentication**  
- **FluentValidation**  
- **Swagger UI**  
- **XUnit**  

---

## **Setup Instructions**
1. Clone the repository:  
   ```bash
   git clone https://github.com/AdelMuhammad-23/ECommerceAPI.git
   cd ECommerceAPI
   ```
2. Configure the database connection in `appsettings.json`.
3. Apply database migrations:  
   ```bash
   dotnet ef database update
   ```
4. Run the application:  
   ```bash
   dotnet run
   ```

---

## **Project Structure**
```plaintext
- ECommerceAPI
  - API (Presentation layer)
  - Core (Business logic and domain layer)
  - Infrastructure (Data access and external services)
```

---

## **Endpoints**
### **Categories**
- **GET** `/api/Categories/CategoryList`  
  Retrieves a paginated list of all categories.  
- **GET** `/api/Categories/Get-Category-By{id}`  
  Retrieves details of a specific category by its ID.  
- **POST** `/api/Categories/Add-Category`  
  Adds a new category to the system.  
- **PUT** `/api/Categories/Update-Category`  
  Updates an existing category.  
- **DELETE** `/api/Categories/Delete-Category-By{id}`  
  Deletes a category by ID.

### **Products**
- **GET** `/api/Products/ProductList`  
  Retrieves a paginated list of products, with optional filters for name and price.  
- **GET** `/api/Products/Get-Product-By{id}`  
  Retrieves details of a specific product by its ID.  
- **POST** `/api/Products/AddProduct`  
  Adds a new product to the system.  
- **PUT** `/api/Products/Update-Product`  
  Updates an existing product.  
- **DELETE** `/api/Products/Delete-Product-{id}`  
  Deletes a product by ID.

---

## **Future Enhancements**
- Integration with a payment gateway for orders.  
- Real-time order tracking and notifications.  
- Analytics dashboard for admin users.  

---
