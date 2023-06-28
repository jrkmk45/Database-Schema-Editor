import { Injectable } from '@angular/core';
import { IConnection } from '../models/connection';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_URL } from '../constants';

@Injectable({
  providedIn: 'root'
})
export class ConnectionService {

  constructor(private http: HttpClient) { }

  createConnectionAsync(schemeId : string, tableId : number, 
    attributeId : number, connection : any) : Observable<IConnection> {
    return this.http.post<IConnection>(`${API_URL}/schemes/${schemeId}/tables`
      +`/${tableId}/attributes/${attributeId}/connections`, connection);
  }

  deleteAttributeConnections(schemeId: string, tableId: number, attributeId: number) {
    return this.http.delete(`${API_URL}/schemes/${schemeId}/tables/${tableId}/attributes/${attributeId}/connections`);
  }
}
