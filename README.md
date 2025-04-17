#ProductAPI
Designed and developed a RESTful Web API using ASP.NET Core, with Entity Framework Core and a Code First approach, utilizing SQLite as the database provider. Integrated ASP.NET Core Identity for handling user authentication and role-based authorization. Implemented JWT Bearer Authentication to secure specific endpoints, allowing only authenticated users with valid tokens to access protected resources.

Configured CORS policies to enable secure cross-origin requests and customized Swagger UI to support JWT token authentication for testing protected endpoints directly from the interface. Applied DTOs (Data Transfer Objects) to decouple domain models from the API contract and ensure secure and efficient data handling between client and server.

Included endpoints for user registration and login, where successfully authenticated users receive a JWT token to maintain session and authorization. Additionally, implemented basic CRUD operations for core entities, demonstrating clean API architecture and adherence to REST principles.

