## How does it work?

The application is designed to manage the workout bookings of fitness club members. App allows Managers and Administrators to create a weekly training template (RepetitiveWorkouts), which at the start of a new week automatically creates a plan for the upcoming week (RealWorkouts). Moreover, it is possible to create and ocassional real workout. 
Visitors without accounts can view the training plan and read about workout types and instructors. Registered users can book their participation in selected workouts.
  
Application user roles:
  - Member
  - Manager
  - Bussiness Administrator
  - System Administrator
  
  The exact list of role permissions is included in the file `RolePermissionMatrix.cs` and can be modified using migration.
<br><br>

## How to download and run
1. Download the solution using the `git clone https://github.com/MontyPythonM/WorkoutReservationWebApp.git` command,
2. Overwrite the **appsettings.json** file (e.g. by user secrets) as recommended below:
    - Enter your database connection string in the **ConnectionStrings.localDbConnection** section (use MS SQL Server),
    - Enter your credentials in **FirstAdmin** section. Application will create a system administrator user based on this data during the first launch,
    - Enter the application key (min. 16 chars) used for user authentication purposes in the **Authentication.JwtKey** section
3. Backend:
    - Install dotnet ef cli `dotnet tool install --global dotnet-ef`,
    - Create database and apply migrations using `dotnet ef database update -s ../WorkoutReservation.API` (in *src/WorkoutReservation.Infrastructure*),
    - Run the backend application (WorkoutReservation.API starter project) in the `WorkoutReservation` configuration
4. Fronted: 
    - Make sure you have **node.js** installed,
    - Install packages `npm install`,
    - Run the frontend application using the `npm start` command in the console (in *src/WorkoutReservation.UI/src/app*)
<br>

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
- Angular 13
- Entity Framework Core 6
- MS SQL Server Express
- JWT Bearer authentication

<br>

## Libraries

- MediatR
- DevExtreme
- Quartz
- AutoMapper
- FluentValidation 
- xUnit
- Moq
- Shouldly
- Swagger
- NLog
