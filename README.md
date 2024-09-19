# .NET Full Stack Developer Assignment

[Visit Website](http://www.assignments.somee.com/)


## Introduction

This assignment demonstrates core full-stack development skills by building a robust MVC web application using C#, SQL Server, and Web API. The focus is on secure user authentication, efficient data management, and API development. Key features include user login, data handling, and CRUD operations, with an emphasis on optimizing SQL queries for better performance. Through this project, the ability to build and manage full-stack applications effectively and securely is showcased.

 ## Installtion
1. First, choose the  Core Web API (MVC) template, then select version 6, and create the project.
2. Install two packages: SQL Server (version 7) and Tools (version 7).
3. Create a folder named "Data" and add your tables there.
4. Go to appsettings.json and connect to your local server.
 5. Add middleware in program.cs:
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("db"))).
 6. Create RESTful APIs using (link unavailable) Core Web API (version 6).
Install SQL Server and Tools packages (version 7).
7. I've included a PostMan_Collection.json file in the project to test the APIs. You can use Postman to test them (I've already tested them).


   


## System Architecture

The system architecture follows a layered structure, ensuring clear separation of concerns and maintainability. Below is an outline of the different layers:

### 1. **Presentation Layer (MVC)**

- Built using ASP.NET MVC.
- Handles views, controllers, and models.
- Includes:
  - **User Authentication**: Login and registration pages.
  - **Data Management**: CRUD operations for the Product entity.
- Facilitates interaction between users and the application.

### 2. **Business Logic Layer**

- Encapsulates all business rules and logic.
- Responsibilities include:
  - Data validation.
  - Security measures (e.g., hashing/salting passwords).
  - CRUD operations for the Product entity.
- Interacts with both the API and presentation layers to process requests and responses.

### 3. **Data Access Layer**

- Communicates with the SQL Server database.
- Uses Entity Framework for efficient database operations.
- Implements query optimization and indexing to improve performance.

### 4. **Web API Layer**

- Provides RESTful APIs to expose CRUD operations for external systems.
- Secures access via authentication.
- Interacts with the business logic layer to manage data and respond with JSON payloads.

### 5. **Database (SQL Server)**

- Stores all application data, including user credentials and product information.
- Optimized for high performance through the use of optimized SQL queries, indexing, and secure storage of sensitive data.
- ![diagram-export-9-19-2024-3_39_06-AM](https://github.com/user-attachments/assets/733f7aa6-1f6d-4f35-b16c-d892435e91c6)


## Conclusion

This architecture promotes modularity, scalability, and security, adhering to best practices in full-stack development. By following this structure, the project ensures maintainability and clear separation of concerns, allowing for the seamless addition of new features and efficient management of data.




## Design Decisions

### Technology Stack

- **ASP.NET MVC**: Chosen for its structured framework that separates concerns and provides built-in features for web development.
- **SQL Server**: Used as the database for its reliability, scalability, and seamless integration with .NET applications.

### Authentication

- **Forms-based Authentication**: Implemented for secure user login and registration. Passwords are hashed and salted to ensure data security.

### Data Management

- **Entity Framework**: Used for database access to simplify interactions with SQL Server, reducing the need for manual SQL queries and speeding up development.
- **CRUD Operations**: A straightforward approach for managing product data with Create, Read, Update, and Delete functionalities.

### API Design

- **RESTful APIs**: Implemented to allow external access to CRUD operations, ensuring that the system remains flexible and easy to integrate with other services.
- **API Security**: Secured with user authentication to ensure that only authorized users can interact with the data.

### Performance Optimization

- **SQL Query Optimization**: Focused on improving database performance by optimizing queries and adding indexes to handle data retrieval efficiently.
  ![Screenshot (75)](https://github.com/user-attachments/assets/7db71a83-b2bd-4f4a-842b-9c352c044d22)


![Screenshot (76)](https://github.com/user-attachments/assets/9953a8a7-0daa-4945-a6e1-cc4bb94e2d9b)


![Screenshot (77)](https://github.com/user-attachments/assets/ed8fafff-b2db-4548-a517-157201a049cf)


  ![Screenshot (78)](https://github.com/user-attachments/assets/f2106850-a9f5-43dc-ac33-5828abda715d)



### User Experience

- Simple, easy-to-navigate user interface to ensure users can easily interact with the application and manage their data without hassle.



# SQL Query Optimization 

## 1. Initial (Before) Query and Analysis

### Original Query:
In the application, the following query was used in the `ProductController`:

var data = db.Products
    .OrderBy(p => p.ProductID)
    .Skip((page - 1) * pageSize)
    .Take(pageSize)
    .ToList();

### Issues Identified:
- No Index: Sorting by ProductID without an index caused slower query execution.
- Tracking Enabled: Using .ToList() without .AsNoTracking() added unnecessary overhead for read-only data.

### Performance Bottlenecks:
- Slow execution due to the lack of proper indexing.
- Extra resources used by Entity Framework's default tracking behavior.

### Before Metrics:
- Query Execution Time: 150 ms
- SQL Server Query Execution Plan Cost: 50%

---

## 2. Optimizations Made

### Changes Implemented:
To improve performance, the following changes were made:

1. Added Index: Created an index on the ProductID column to speed up sorting operations.
2. Disabled Tracking: Used .AsNoTracking() for the query as the data is read-only, reducing memory overhead.

### Optimized Query:

var data = db.Products
    .OrderBy(p => p.ProductID)
    .Skip((page - 1) * pageSize)
    .Take(pageSize)
    .AsNoTracking() // Disable EF change tracking for read-only data
    .ToList();

### SQL Changes:

CREATE INDEX idx_ProductID ON Products(ProductID);

### Improvements:
- Index on ProductID: Enhanced the performance of sorting operations.
- .AsNoTracking(): Disabled Entity Framework's change tracking to optimize read-only data access.

---

## 3. Before-and-After Performance Metrics

| Query               | Execution Time (Before) | Execution Time (After) | Query Cost (Before) | Query Cost (After) |
|---------------------|-------------------------|------------------------|---------------------|--------------------|
| Product List Query   | 150 ms                  | 80 ms                  | 50%                 | 20%                |

### Performance Gains:
- Execution Time: Reduced from 150 ms to 80 ms (47% improvement).
- Query Cost: Dropped from 50% to 20% in SQL Server's execution plan.

---

## 4. Results and Conclusion

By adding an index on ProductID and using .AsNoTracking(), the query's execution time was reduced by 47%, and the query cost dropped from 50% to 20%. This optimization improves scalability and reduces resource usage, enhancing the performance of the application, especially with larger datasets.

### Future Recommendations:
- Consider analyzing other frequently used queries for similar optimizations.
- Monitor index fragmentation and maintain indexes periodically.




## API Testing

I have given one PostMan_Collection.json file , you can use this file to test the Restful Apis

