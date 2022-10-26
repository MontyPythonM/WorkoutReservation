export class RegisterForm {
    email?: string;
    password?: string;
    confirmPassword?: string;
    firstName?: string;
    lastName?: string;
    gender?: number;
    dateOfBirth?: Date | string;

    constructor(data?: {email: string, password: string, confirmPassword: string, firstName: string, lastName: string, gender: number, dateOfBirth: Date | string}) {
        this.email = data?.email;
        this.password = data?.password;
        this.confirmPassword = data?.confirmPassword;
        this.firstName = data?.firstName;
        this.lastName = data?.lastName;
        this.gender = data?.gender;
        this.dateOfBirth = data?.dateOfBirth;
    }
}