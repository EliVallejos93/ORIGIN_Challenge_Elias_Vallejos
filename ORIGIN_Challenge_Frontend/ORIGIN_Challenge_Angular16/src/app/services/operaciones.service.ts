import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OperacionesService {
  constructor(private http: HttpClient) { }

  baseUrl = "https://localhost:7112";

  Balance(numeroTarjeta: string): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/Operaciones/Balance?numeroTarjeta=${numeroTarjeta}`).pipe(tap((res: any) => { }));
  }

  Retiro(numeroTarjeta: string, cantidadRetiro: string): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/Operaciones/Retiro?numeroTarjeta=${numeroTarjeta}&cantidadRetiro=${cantidadRetiro}`).pipe(tap((res: any) => { }));
  }
}
