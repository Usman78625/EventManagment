# Event Management System

A full-stack application for managing events, built with Angular and .NET Core, following Clean Architecture and SOLID principles.

## Features

- User authentication and authorization
- Event management (CRUD operations)
- Ticket booking system
- Role-based access control (Admin, Organizer, Attendee)

## Tech Stack

### Backend
- .NET Core Web API
- Entity Framework Core
- MS SQL Server
- JWT Authentication
- Clean Architecture
- SOLID Principles

### Frontend
- Angular
- TypeScript
- Bootstrap
- RxJS

## Project Structure

```
/EventManagement
  /EventManagement.API         // .NET Core Web API
  /EventManagement.Application // Application layer
  /EventManagement.Domain      // Domain layer
  /EventManagement.Infrastructure // Infrastructure layer
  /event-management-web       // Angular frontend
```

## Getting Started

### Prerequisites
- .NET Core SDK
- Node.js and npm
- MS SQL Server
- Angular CLI

### Backend Setup
1. Update the connection string in `appsettings.json`
2. Run the following commands:
   ```bash
   cd EventManagement.API
   dotnet restore
   dotnet ef database update
   dotnet run
   ```

### Frontend Setup
1. Install dependencies:
   ```bash
   cd event-management-web
   npm install
   ```
2. Start the development server:
   ```bash
   ng serve
   ```

## API Documentation
The API documentation is available at `/swagger` when running the backend.

## Contributing
1. Fork the repository
2. Create your feature branch
3. Commit your changes
4. Push to the branch
5. Create a new Pull Request

## License
This project is licensed under the MIT License. 
