import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

@Injectable({
  providedIn: 'root'
})
export class BowlingService {
  baseHref: string;

  constructor(private http: HttpClient) {
    this.baseHref = window.location.origin.indexOf('localhost') != -1 ? 'http://localhost:55703' : '';
  }

  startNewGame(player)
  {
    return this.http.post(this.baseHref + '/api/bowling/newgame/',{name:player});
  }

  addFrame(gameId,bowls)
  {
    return this.http.post(this.baseHref + '/api/bowling/' + gameId + '/addframe/',bowls);
  }
}
