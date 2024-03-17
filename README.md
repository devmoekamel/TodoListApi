# TodoList API

Todo List API implemented using the .NET framework. It serves as a backend service for managing tasks in a todo list application.

## Table of Contents

- [Project Structure](#project-structure)
- [Key Features](#key-features)
- [Technologies Used](#technologies-used)
- [Getting Started](#getting-started)
- [Authentication](#authentication)
- [Testing](#testing)
- [Conclusion](#conclusion)

## Project Structure

The project structure follows a typical .NET Core Web API pattern:

- **Controllers:** Contains controller classes that handle HTTP requests and serve responses.
- **Services:** Contains service classes responsible for business logic and data manipulation.
- **Models and DTOs:** Defines data models and DTOs (Data Transfer Objects) used for communication.
- **Context:** Includes database context and migrations for Entity Framework Core.

**Model Relationship:**

```
+-------------------+        1          +---------------------+
|  ApplicationUser  |----------------->|       Taskk         |
+-------------------+   0..N          +---------------------+
| Id: int           |                   | Id: int             |
| UserName: string |                   | Name: string        |
| FirstName: string|                   | Description: string|
| LastName: string |                   | IsCompleted: bool  |
|                   |                   | CreatedOn: DateTime|
+-------------------+                   | UserId: string     |
                                        +---------------------+
``` 

In this horizontal diagram:
- `ApplicationUser` has a one-to-many relationship with `Taskk`, represented by the `Tasks` property in `ApplicationUser`.
- Each `Taskk` belongs to exactly one `ApplicationUser`, represented by the `UserId` foreign key in `Taskk`.
- The relationship is depicted by the arrows, indicating the direction of the relationship from `ApplicationUser` to `Taskk`.

## Key Features

- **Task Management:** Implements CRUD operations for tasks, allowing users to create, read, update, and delete tasks.
- **Authentication and Authorization:** Utilizes JWT-based authentication for secure access to endpoints. Supports user registration, login, and role-based access control.
- **Validation and Error Handling:** Implements robust validation checks and error handling mechanisms for data integrity and graceful error responses.
- **Documentation:** Provides API documentation using Swagger UI, enabling easy exploration and testing of endpoints.
- **Unit Testing:** Ensures code reliability and maintainability with a suite of unit tests covering critical functionalities.

## Technologies Used

- **.NET Core:** A cross-platform framework for building modern web applications and services.
- **C# Programming Language:** The primary language used for development.
- **ASP.NET Core Web API:** A framework for building HTTP-based services.
- **Entity Framework Core:** An ORM (Object-Relational Mapper) for database interactions.
- **JWT Authentication:** A token-based authentication mechanism for securing APIs.
- **Swagger/OpenAPI:** A tool for documenting and testing APIs.

## Getting Started

To run the TodoList API locally, follow these steps:

1. Clone this repository to your local machine.
2. Configure the database connection string in `appsettings.json`.
3. Run database migrations to create necessary tables (`dotnet ef database update`).
4. Build and run the application (`dotnet run`).
5. Access the API endpoints using your preferred HTTP client or Swagger UI (typically available at `http://localhost:5000/swagger`).

## Authentication

Authentication in the TodoList API is based on JWT tokens. To access protected endpoints, clients must include a valid JWT token in the `Authorization` header of the HTTP request. Users can obtain JWT tokens by registering or logging in through the appropriate endpoints.

## Testing

Unit tests are available in the `Tests` directory. To run the tests, execute `dotnet test` from the command line. Tests cover critical functionalities and ensure code reliability.

## Conclusion

Thank you for exploring the TodoList API project! I hope this repository demonstrates my proficiency in building scalable, secure, and well-documented APIs using .NET Core. If you have any questions or feedback, feel free to reach out.
