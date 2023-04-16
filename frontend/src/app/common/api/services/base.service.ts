import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BaseService {

  protected constructor() { }

  protected getUrl(relativeUrl: string): string {
    const url = new URL(relativeUrl, environment.serverUrl);

    return url.toString();
  }
}
