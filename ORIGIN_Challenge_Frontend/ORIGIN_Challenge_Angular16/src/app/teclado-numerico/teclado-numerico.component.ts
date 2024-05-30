import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-teclado-numerico',
  templateUrl: './teclado-numerico.component.html',
  styleUrls: ['./teclado-numerico.component.css']
})
export class TecladoNumericoComponent {
  @Input() titulo: string = '';
  @Input() tipo: string = '';
  @Input() formato: string = '';
  @Output() mensaje = new EventEmitter<string>();
  @Output() presionoAceptar = new EventEmitter<{ tipo: string, numero: string }>();

  campoVisualizacion: string = '';

  constructor() {
    this.campoVisualizacion = this.formato;
  }

  //----------------------------------INGRESO DE DATOS----------------------------------
  tecleoNumero(numero: string): void {
    // tarjeta
    if (this.tipo == 'esTarjeta') this.filtrarTarjeta(numero);
    // pin
    if (this.tipo == 'esPin') this.filtrarPin(numero);
    // retiro
    if (this.tipo == 'esRetiro') this.filtrarRetiro(numero);
  }

  filtrarTarjeta(numero: string) {
    if (this.campoVisualizacion == this.formato) {
      this.campoVisualizacion = '';
    }
    if (this.campoVisualizacion.length < 19) {
      if (this.campoVisualizacion.length == 4 || this.campoVisualizacion.length == 9 || this.campoVisualizacion.length == 14) {
        this.campoVisualizacion = this.campoVisualizacion + '-';
      }
      this.campoVisualizacion = this.campoVisualizacion + numero;
    }
  }

  filtrarPin(numero: string) {
    if (this.campoVisualizacion == this.formato) {
      this.campoVisualizacion = '';
    }
    if (this.campoVisualizacion.length < 4) {
      this.campoVisualizacion = this.campoVisualizacion + numero;
    }
  }

  filtrarRetiro(numero: string) {
    if (this.campoVisualizacion == this.formato) {
      this.campoVisualizacion = '';
    }
    this.campoVisualizacion = this.campoVisualizacion + numero;
  }

  //----------------------------------VERIFICAR FORMATO AL ACEPTAR----------------------------------
  verificarFormato() {
    // tarjeta
    if (this.tipo == 'esTarjeta') this.verificarTarjeta();
    // pin
    if (this.tipo == 'esPin') this.verificarPin();
    // retiro
    if (this.tipo == 'esRetiro') this.verificarRetiro();
  }

  verificarTarjeta() {
    if (this.campoVisualizacion.length == 19 && this.campoVisualizacion != this.formato) {
      var tarjetaSinGuiones = this.campoVisualizacion.replace(/-/g, '').trim();
      this.emitir(this.tipo, tarjetaSinGuiones);
    } else this.mensaje.emit("Por favor, ingrese una tarjeta válida.");
  }

  verificarPin() {
    if (this.campoVisualizacion.length == 4 && this.campoVisualizacion != this.formato) this.emitir(this.tipo, this.campoVisualizacion);
    else this.mensaje.emit("Por favor, ingrese un pin válido.");
  }

  verificarRetiro() {
    if (this.campoVisualizacion != this.formato) this.emitir(this.tipo, this.campoVisualizacion);
    else this.mensaje.emit("Por favor, ingrese una cantidad válida.");
  }

  emitir(_tipo: string, _numero: string) {
    this.presionoAceptar.emit({
      tipo: _tipo,
      numero: _numero
    });
  }

  //----------------------------------LIMPIAR CAMPO----------------------------------
  limpiarCampos() {
    this.campoVisualizacion = this.formato;
  }
}
