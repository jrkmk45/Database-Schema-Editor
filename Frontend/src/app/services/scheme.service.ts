import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IScheme } from '../models/scheme';
import { API_URL } from '../constants';
import { ISchemeListItem } from '../models/scheme-list-item';

@Injectable({
  providedIn: 'root'
})
export class SchemeService {

  constructor(private http: HttpClient) { }

  getScheme(id: string) : Observable<IScheme> {
    return this.http.get<IScheme>(`${API_URL}/schemes/${id}`);
  }

  getUserSchemes() : Observable<ISchemeListItem[]> {
    return this.http.get<ISchemeListItem[]>(`${API_URL}/users/me/schemes`);
  }

  createScheme(scheme: any) : Observable<ISchemeListItem> {
    return this.http.post<ISchemeListItem>(`${API_URL}/schemes`, scheme);
  }

  deleteScheme(schemeId: number) {
    return this.http.delete(`${API_URL}/schemes/${schemeId}`);
  }
}
