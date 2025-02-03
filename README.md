# User Management

## Overview
User Management application provides a simple CRUD interface for managing users.

## Features
- View, Add, Edit, and Delete users.
- Uses **Blazor WebAssembly** for the frontend.
- ASP.NET Web API for backend services.
- .NET 9

##  Database
Project uses SQLite as the database for storing user data.

## 🧪 Testing
This project includes unit tests for the repository layer and model validation. Tools used:

- MSTest
- FluentAssertions
- Entity Framework Core In-Memory Database
- DataAnnotations Validator
  
## API Documentation
This project uses **Swagger** for API documentation. To access the Swagger UI, run the API and navigate to:
```
https://localhost:7174/index.html
```

## 💻 Getting Started
### 1️⃣ Clone the Repository
```sh
git clone https://github.com/robertaben/users-assignment.git
```

### 2️⃣ Run the API Backend
```sh
cd BlazorUserApp.Server
dotnet run
```
🚀 **API runs on** `https://localhost:7174/`

### 3️⃣ Run the Blazor Client
```sh
cd ../BlazorUserApp.Client
dotnet run
```
🚀 **Client runs on** `https://localhost:7181/`

## 🔄 API Endpoints
| Method | Endpoint            | Description           |
|--------|---------------------|-----------------------|
| GET    | /api/users          | Get all users         |
| POST   | /api/users          | Add a new user        |
| PUT    | /api/users/{userId} | Update an existing user |
| DELETE | /api/users/{userId} | Delete a user        |

