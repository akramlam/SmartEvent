# SmartEvent

SmartEvent is a comprehensive event management platform with a .NET Core backend and React frontend that allows users to create, manage, and register for events.

## Project Structure

The solution follows an N-Tier architecture with a Service-Oriented Architecture (SOA) component:

- **SmartEvent.API**: Main API project that handles HTTP requests and acts as the entry point for the application
- **SmartEvent.Core**: Contains domain models and interfaces
- **SmartEvent.Data**: EF Core repositories and database context
- **SmartEvent.Services**: Business logic services
- **SmartEvent.RegistrationService**: Separate microservice for handling event registrations (SOA)
- **frontend**: React.js client application

## Features

- Event management (CRUD operations)
- Event registration
- Prevention of double registrations
- Participant listing
- SOA implementation for registration service

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server (LocalDB or full instance)
- Node.js and npm

### Database Setup

1. Update the connection string in `appsettings.json` if needed
2. Run Entity Framework migrations:

```
cd SmartEvent.API
dotnet ef database update
```

### Running the API

1. Run the API project:

```
cd SmartEvent.API
dotnet run
```

2. Run the Registration Service (for SOA implementation):

```
cd SmartEvent.RegistrationService
dotnet run
```

### Running the Frontend

1. Navigate to the frontend directory:

```
cd SmartEvent/frontend
```

2. Install dependencies:

```
npm install
```

3. Start the development server:

```
npm start
```

## API Endpoints

### Events

- **GET /api/events**: Get all events
- **GET /api/events/{id}**: Get event by ID
- **POST /api/events**: Create a new event
- **PUT /api/events/{id}**: Update an event
- **DELETE /api/events/{id}**: Delete an event

### Registrations

- **POST /api/registrations/events/{eventId}**: Register for an event
- **GET /api/registrations/events/{eventId}/Participants**: Get event Participants
- **GET /api/registrations/events/{eventId}/full**: Check if an event is full

## Architecture

The application follows a clean architecture approach:

1. **Core Layer**: Contains domain models and interfaces
2. **Data Layer**: Implements repositories and database access
3. **Services Layer**: Contains business logic
4. **API Layer**: Exposes REST endpoints

The registration functionality has been extracted into a separate microservice (SOA approach) that communicates with the main API via HTTP.

## Technologies Used

- ASP.NET Core 8
- Entity Framework Core
- React.js
- SQL Server
- RESTful API 