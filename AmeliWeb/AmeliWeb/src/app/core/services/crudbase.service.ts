import { Injectable } from '@angular/core';
import { BaseEntity } from '../models/baseEntity';
import { HttpService } from './http.service';

@Injectable({ providedIn: 'root' })
export class CRUDBaseService {
  public Endpoint = ``;
  constructor(private readonly httpBase: HttpService) { }

  getAll<T extends BaseEntity>() {
    return this.httpBase.get<T[]>(this.Endpoint);
  }

  getByID<T extends BaseEntity>(id: object) {
    return this.httpBase.get<T>(`${this.Endpoint}/${id}`);
  }

  update<T extends BaseEntity>(updated: T) {
    return this.httpBase.put<T>(`${this.Endpoint}/${updated.id}`, updated);
  }

  add<T extends BaseEntity>(added: T) {
    return this.httpBase.post<T>(this.Endpoint, added);
  }

  delete<T extends BaseEntity>(id: object) {
    return this.httpBase.delete<T>(`${this.Endpoint}/${id}`);
  }
}
