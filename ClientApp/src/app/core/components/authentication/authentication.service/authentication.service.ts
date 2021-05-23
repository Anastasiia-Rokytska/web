import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthenticationRequest } from 'src/app/common/models/authentication/AuthenticationRequest';
import { AuthenticationResponse } from 'src/app/common/models/authentication/AuthenticationResponse';
import { User } from 'src/app/common/models/authentication/User';



@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private userSubject: BehaviorSubject<User>;
    public user: Observable<User>;

    constructor(
        private router: Router,
        private http: HttpClient
    ) {
        this.userSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('user')));
        this.user = this.userSubject.asObservable();
    }

    public get userValue(): User {
        return this.userSubject.value;
    }

    async login(username: string, password: string) {
        return await this.sendAuthenticationRequest(null, username, password);
    }


    async sendAuthenticationRequest(accessToken?: string, username?: string, password?: string) {

        let authenticationRequest: AuthenticationRequest = {
            accessToken: accessToken,
            password: password,
            userName: username
        };

        let data = await this.http.post<AuthenticationResponse>(environment.apiUrl + "Authentication", authenticationRequest).toPromise().catch((error) => {
           throw error;

        });

        let user: User = {
            userId: +data.userId,
            roles: data.roles,
            userName: data.userName,
            accessToken: data.token,
            name: data.name,
            surname: data.surName,
            group: data.group,
            password: data.password
        }

       
        localStorage.setItem('user', JSON.stringify(user));
        this.userSubject.next(user);


        return user;

    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('user');

        this.userSubject.next(null);
        this.router.navigate(['/login']);
    }
}