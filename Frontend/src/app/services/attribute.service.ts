import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IAttribute } from '../models/attribute';
import { API_URL } from '../constants';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AttributeService {

  constructor(private http: HttpClient) { }

  createAttribute(schemeId: string, tableId : number, attribute : any) : Observable<IAttribute> {
    return this.http.post<IAttribute>(`${API_URL}/schemes/${schemeId}/tables`
      +`/${tableId}/attributes/`, attribute);
  }

  deleteAttribute(schemeId: string, tableId : number, attributeId : number) {
    return this.http.delete(`${API_URL}/schemes/${schemeId}/tables`
      +`/${tableId}/attributes/${attributeId}`);
  }
}
