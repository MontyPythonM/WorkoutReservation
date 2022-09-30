export interface RegisterForm {
    email: string;
    password: string;
    confirmPassword: string;
    firstName: string;
    lastName: string;
    gender?: number;
    dateOfBirth?: Date | string;
}