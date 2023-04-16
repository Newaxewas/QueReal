import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { LoginRequest, RegisterRequest } from '../models/user';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseService {

  public constructor(public httpClient: HttpClient) {
    super();
  }

  public login(request: LoginRequest): Observable<unknown> {
    return this.httpClient.post(this.getUrl("/user/login"), request);
  }

  public register(request: RegisterRequest): Observable<unknown> {
    return this.httpClient.post(this.getUrl("/user/register"), request);
  }

  public logout(): Observable<unknown> {
    return this.httpClient.post(this.getUrl("/user/logout"), {});
  }

}
