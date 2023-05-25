namespace WorkoutReservation.Application.Contracts;

public interface IPasswordManager
{ 
    string Secure(string password);
    bool Validate(string password, string hashedPassword);
}