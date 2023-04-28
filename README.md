# How does it work?

The application is designed to manage the workout bookings of fitness club members. App allows Managers and Administrators to create a weekly training template (RepetitiveWorkouts), which at the start of a new week automatically creates a plan for the upcoming week (RealWorkouts). Moreover, it is possible to create and ocassional real workout. 
Visitors without accounts can view the training plan and read about workout types and instructors. Registered users can book their participation in selected workouts.
  
Application user roles:
  - Member
  - Manager
  - Bussiness Administrator
  - System Administrator
  
  The exact list of role permissions is included in the file `RolePermissionMatrix.cs` and can be modified using migration.
<br><br>

## Design Patterns
  
- Clean architecture
- CQRS
- Mediator
- Repository
- Domain Events
- Outbox messages
<br>

## Technologies
  
- ASP.NET Core 6
- Entity Framework Core 6
- MS SQL Express Database
- Angular 13
- JWT Bearer authentication

<br>

## Libraries

- MediatR
- Quartz
- AutoMapper
- FluentValidation 
- xUnit
- Moq
- FluentAssertions
- Swagger
- NLog
- DevExtreme

