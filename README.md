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
  - Enter your database connection string in the **ConnectionStrings.localDbConnection** section (use MS SQL Server or MS SQL Express),
  - Enter your credentials in **FirstAdmin** section. Application will create a system administrator user based on this data during the first launch,
  - Enter the application key (min. 16 chars) used for user authentication purposes in the **Authentication.JwtKey** section.
3. Run the backend application (WorkoutReservation.API starter project) in the `WorkoutReservation` configuration,
4. Run the frontend application using the `ng serve` command in the console (in WorkoutReservation.UI/src)
5. Verify that the WorkoutReservation database has been created and that a record has been created in the **WorkoutReservation.Permissions.Users** table with the user indicated in the appsettings.json file.
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
- MS SQL Express Database
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
- FluentAssertions
- Swagger
- NLog
