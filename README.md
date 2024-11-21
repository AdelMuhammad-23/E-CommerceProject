
# **E-Commerce API**

## **Overview**
The **E-Commerce API** is a backend solution for managing e-commerce operations like product management, category organization, and secure user authentication.  
It is structured as a **3-Layered Architecture**, inspired by **Clean Architecture** and **Onion Architecture**, ensuring high scalability, maintainability, and code reusability.

---

## **Features**
### **Core Architecture**
1. **Layered Design**:  
   - **API Layer**: Handles HTTP requests and responses (Presentation Layer).  
   - **Core Layer**: Contains business logic, domain entities, and service interfaces.  
   - **Infrastructure Layer**: Manages data access, repositories, and external services.  
2. **CQRS Pattern**: Command-query separation for better readability and performance.  
3. **Generic Repository**: Reusable design for database operations.  

### **API Enhancements**
4. **Pagination & Filtering**: Efficient data retrieval with optional filters for name and price.  
5. **Validation**: Ensures input integrity with **FluentValidation**.  
6. **API Documentation**: Integrated with **Swagger** (JWT support included).  

### **Security**
7. **JWT Authentication**: Secures API endpoints.  
8. **Role-Based Access Control**: Restricts permissions based on roles.  
9. **CORS Policy**: Controlled cross-origin access.  

### **File Management**
10. **Image Handling**: Supports image upload and management for products.

### **Utilities**
11. **Logging**: Tracks application activity using **Serilog** (or any configured logger).  
12. **Unit Testing**: Reliable endpoint testing with **XUnit**.

---

## **Technologies Used**
- **ASP.NET Core Web API**  
- **Entity Framework Core**  
- **JWT Authentication**  
- **FluentValidation**  
- **Swagger UI**  
- **XUnit**  
- **Serilog** (or your preferred logging tool)

---

## **Setup Instructions**
1. Clone the repository:  
   ```bash
   git clone https://github.com/AdelMuhammad-23/ECommerceAPI.git
   cd ECommerceAPI
   ```
2. Configure database connection in `appsettings.json`.
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
  - API (Presentation Layer: Controllers & Middleware)
  - Core (Business Logic: Entities, DTOs, Interfaces)
  - Infrastructure (Data Access: Repositories, Migrations)
```

---

## **Endpoints**
### **Categories**
- **GET** `/api/Categories/CategoryList`  
  Retrieves paginated category list with optional filters.
- **GET** `/api/Categories/Get-Category-By{id}`  
  Fetches a specific category by ID.
- **POST** `/api/Categories/Add-Category`  
  Adds a new category.
- **PUT** `/api/Categories/Update-Category`  
  Updates an existing category.
- **DELETE** `/api/Categories/Delete-Category-By{id}`  
  Deletes a category by ID.

### **Products**
- **GET** `/api/Products/ProductList`  
  Retrieves paginated product list with filters (name, price).  
  Example filter usage:  
  ```json
  {
    "nameFilter": "phone",
    "priceFilter": 500
  }
  ```
- **GET** `/api/Products/Get-Product-By{id}`  
  Retrieves product details by ID.
- **POST** `/api/Products/AddProduct`  
  Adds a new product with image upload.
- **PUT** `/api/Products/Update-Product`  
  Updates product details.
- **DELETE** `/api/Products/Delete-Product-{id}`  
  Deletes a product by ID.

---

## **Future Enhancements**
- Integration with a payment gateway for secure transactions.  
- Adding user-friendly dashboards for admin analytics.  
- Real-time notifications for order updates.  

---

## **Database Schema (Simplified)**
### **Categories**
| Column Name | Data Type | Constraints       |
|-------------|-----------|-------------------|
| Id          | int       | Primary Key       |
| Name        | string    | Required, Unique  |

### **Products**
| Column Name      | Data Type | Constraints       |
|------------------|-----------|-------------------|
| Id               | int       | Primary Key       |
| Name             | string    | Required          |
| Price            | decimal   | Required          |
| CategoryId       | int       | Foreign Key       |
| ImagePath        | string    | Nullable          |
