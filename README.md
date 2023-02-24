# SF002 - CRM Project

### Modules

- Customers
- Employees
- Requests
- Tasks
- Offers
- Sales
- User Addresses
- User Phones
- User Emails
- Departments
- Documents
- Notifications
- Offer Statuses
- Regions
- Titles
- User Management (Users, Roles, User Roles, Permissions, Role Permissions)
- User Statuses

### Features
- ASP.NET Core N-Layer Architecture
- EntityFramework Core
  - Code First Approach
  - Fluent API Entity Configuration
  - Migration
  - Seeding Database
  - Generic Repository
- AutoMapper
- Fluent Validation
- ASP.NET MVC
  - Global Exception Middleware
  - IP Logging Middleware
  - Ajax CRUD Views
- ASP.NET Web API
    - Log Action Filter
- Email Service
- Hosted Services, FluentScheduler
- MongoDB Logging
- Redis Caching
- RabbitMQ


### Installation

Run MongoDb container for Logging Infrastructure
> docker run -d -p 27017:27017 --name mymongodb mongo

Run Redis container for Caching Infrastructure
> docker run -d -p 6379:6379 --name myredis redis

Run RabbitMQ Server and Management Panel container for Message Queue Examples
> docker run -it -p 5672:5672 -p 15672:15672 --name myrabbitmq rabbitmq:3.11-management
