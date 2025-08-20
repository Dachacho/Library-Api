# Library API

A simple ASP.NET Core Web API for managing library users, books, and loans.

--- 

## Features
- CRUD operations for Users, Books, and Loans
- Basic business logic for loaning and returning books

## Technologies
- ASP.NET Core
- Entity Framework Core
- SQLite

## Endpoints
- `GET /api/users`
- `POST /api/users`
- `PUT /api/users/{id}`
- `DELETE /api/users/{id}`
- (Similar for books and loans only difference being `/return` in loans)

## Setup (if you want it for learning purpouses)
1. Clone the repository
2. Restore dependencies: `dotnet restore`
3. Run the project: `dotnet run`
4. API available at `https://localhost:5090/api/`  
**Disclaimer**
This project is intended as a starter project for learning ASP.NET Core and Entity Framework Core.
It does not implement all recommended best practices (advanced validation, testing, security and such). Use it as a foundation to build and improve upon.
