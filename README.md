# SmartEvent

SmartEvent est une plateforme complète de gestion d'événements avec un backend .NET Core et un frontend React qui permet aux utilisateurs de créer, gérer et s'inscrire à des événements.

## Structure du Projet

La solution suit une architecture N-Tier avec une composante d'Architecture Orientée Services (SOA) :

- **SmartEvent.API** : Projet API principal qui gère les requêtes HTTP et sert de point d'entrée pour l'application
- **SmartEvent.Core** : Contient les modèles de domaine et les interfaces
- **SmartEvent.Data** : Repositories EF Core et contexte de base de données
- **SmartEvent.Services** : Services de logique métier
- **SmartEvent.RegistrationService** : Microservice séparé pour gérer les inscriptions aux événements (SOA)
- **frontend** : Application client React.js

## Fonctionnalités

- Gestion des événements (opérations CRUD)
- Inscription aux événements
- Prévention des doubles inscriptions
- Liste des participants
- Implémentation SOA pour le service d'inscription

## Mise en Route

### Prérequis

- SDK .NET 8
- SQL Server (LocalDB ou instance complète)
- Node.js et npm

### Configuration de la Base de Données

1. Mettez à jour la chaîne de connexion dans `appsettings.json` si nécessaire
2. Exécutez les migrations Entity Framework :

```
cd SmartEvent.API
dotnet ef database update
```

### Exécution de l'API

1. Exécutez le projet API :

```
cd SmartEvent.API
dotnet run
```

2. Exécutez le Service d'Inscription (pour l'implémentation SOA) :

```
cd SmartEvent.RegistrationService
dotnet run
```

### Exécution du Frontend

1. Naviguez vers le répertoire frontend :

```
cd SmartEvent/frontend
```

2. Installez les dépendances :

```
npm install
```

3. Démarrez le serveur de développement :

```
npm start
```

## Points de Terminaison API

### Événements

- **GET /api/events** : Obtenir tous les événements
- **GET /api/events/{id}** : Obtenir un événement par ID
- **POST /api/events** : Créer un nouvel événement
- **PUT /api/events/{id}** : Mettre à jour un événement
- **DELETE /api/events/{id}** : Supprimer un événement

### Inscriptions

- **POST /api/registrations/events/{eventId}** : S'inscrire à un événement
- **GET /api/registrations/events/{eventId}/Participants** : Obtenir les participants à un événement
- **GET /api/registrations/events/{eventId}/full** : Vérifier si un événement est complet

## Architecture

L'application suit une approche d'architecture propre :

1. **Couche Core** : Contient les modèles de domaine et les interfaces
2. **Couche Data** : Implémente les repositories et l'accès à la base de données
3. **Couche Services** : Contient la logique métier
4. **Couche API** : Expose les points de terminaison REST

La fonctionnalité d'inscription a été extraite dans un microservice séparé (approche SOA) qui communique avec l'API principale via HTTP.

## Technologies Utilisées

- ASP.NET Core 8
- Entity Framework Core
- React.js
- SQL Server
- API RESTful 