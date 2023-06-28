import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ITable } from '../models/table';
import { API_URL } from '../constants';

@Injectable({
  providedIn: 'root'
})
export class TableService {

  constructor(private http: HttpClient) { }

  createTable(table: any, schemeId: string) : Observable<ITable> {
    return this.http.post<ITable>(`${API_URL}/schemes/${schemeId}/tables`, table);
  }

  patchTable(table: any, tableId: number, schemeId: string) {
    return this.http.patch(`${API_URL}/schemes/${schemeId}/tables/${tableId}`, table);
  }

  deleteTable(tableId: number, schemeId: string) {
    return this.http.delete(`${API_URL}/schemes/${schemeId}/tables/${tableId}`);
  }
}
