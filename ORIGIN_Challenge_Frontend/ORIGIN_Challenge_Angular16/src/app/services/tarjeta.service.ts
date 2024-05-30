import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TarjetaService {
  constructor(private http: HttpClient) { }

  baseUrl = "https://localhost:7112";

  InsertarDatosAleatorios(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/Tarjetas/InsertarDatosAleatorios`).pipe(tap((res: any) => { }));
  }
  VerificarTarjeta(numeroTarjeta: string): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/Tarjetas/VerificarTarjeta?numeroTarjeta=${numeroTarjeta}`).pipe(tap((res: any) => { }));
  }
  VerificarPin(numeroTarjeta: string, numeroPin: string): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/Tarjetas/VerificarPin?numeroTarjeta=${numeroTarjeta}&numeroPin=${numeroPin}`).pipe(tap((res: any) => { }));
  }
}
