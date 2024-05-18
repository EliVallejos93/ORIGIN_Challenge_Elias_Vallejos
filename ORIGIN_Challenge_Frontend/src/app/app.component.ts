import { Component } from '@angular/core';
import { TarjetaService } from 'src/app/services/tarjeta.service';
import { OperacionesService } from 'src/app/services/operaciones.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  spinner = false;
  paso = 1;
  numeroTarjeta: string = 'XXXX-XXXX-XXXX-XXXX';
  tarjetaSinGuiones: string = '';
  numeroPin: string = 'XXXX';
  fechaVencimiento: Date = new Date();
  dineroEnCuenta: number = 0;
  cantidadRetiro: string = "0";
  operaciones: any[] = [];
  modal = {
    visible: false,
    tipo: "",
    titulo: "",
    contenido: ""
  };

  constructor(private TarjetaService: TarjetaService, private OperacionesService: OperacionesService) {
    this.showAlert('', 'Hola', 'Bienvenido a mi app');
  }

  showAlert(tipoA: any, tituloA: any, contenidoA: any) {
    this.modal = {
      visible: true,
      tipo: tipoA,
      titulo: tituloA,
      contenido: contenidoA
    };
    setTimeout(() => {
      this.modal.visible = false;
    }, 2000);
  }

  tecleoNumero(numero: string) {
    // ingreso tarjeta
    if (this.paso == 2) {
      if (this.numeroTarjeta == 'XXXX-XXXX-XXXX-XXXX') {
        this.numeroTarjeta = '';
      }
      if (this.numeroTarjeta.length < 19) {
        if (this.numeroTarjeta.length == 4 || this.numeroTarjeta.length == 9 || this.numeroTarjeta.length == 14) {
          this.numeroTarjeta = this.numeroTarjeta + '-';
        }
        this.numeroTarjeta = this.numeroTarjeta + numero;
      }
    }
    // ingreso pin
    else if (this.paso == 3) {
      if (this.numeroPin == 'XXXX') {
        this.numeroPin = '';
      }
      if (this.numeroPin.length < 4) {
        this.numeroPin = this.numeroPin + numero;
      }
    }
    // ingreso retiro
    else if (this.paso == 6) {
      if (this.cantidadRetiro == '0') {
        this.cantidadRetiro = '';
      }
      this.cantidadRetiro = this.cantidadRetiro + numero;
    }
  }

  agregarTarjetaAleatoria() {
    this.spinner = true;
    this.TarjetaService.InsertarDatosAleatorios().subscribe(
      (res: any) => {
        this.spinner = false;
        if (res.code == 200) this.showAlert("OK", res.message, "");
        else this.showAlert("WARN", res.message, "");
      },
      err => {
        this.spinner = false;
        this.showAlert("WARN", err.error?.message, "");
      }
    );
  }

  verificarTarjeta() {
    if (this.numeroTarjeta.length == 19 && this.numeroTarjeta != 'XXXX-XXXX-XXXX-XXXX') {
      this.spinner = true;
      this.tarjetaSinGuiones = this.numeroTarjeta.replace(/-/g, '').trim();
      this.TarjetaService.VerificarTarjeta(this.tarjetaSinGuiones).subscribe(
        (res: any) => {
          this.spinner = false;
          if (res.code == 200) {
            this.paso = 3;
            this.showAlert("OK", res.message, "");
          }
          else this.showAlert("WARN", res.message, "");
        },
        err => {
          this.spinner = false;
          this.showAlert("WARN", err.error, "");
        }
      );
    } else this.showAlert("WARN", "Por favor, ingrese una tarjeta válida.", "");
  }

  verificarPin() {
    if (this.numeroPin.length == 4 && this.numeroPin != 'XXXX') {
      this.spinner = true;
      this.TarjetaService.VerificarPin(this.tarjetaSinGuiones, this.numeroPin).subscribe(
        (res: any) => {
          this.spinner = false;
          if (res.code == 200) {
            //entro con exito
            this.paso = 4;
            this.showAlert("OK", res.message, "");
          } else this.showAlert("WARN", res.message, "");
        },
        err => {
          this.spinner = false;
          if (err.status == 423) {
            this.paso = 1;
            this.limpiarCampos();
            this.showAlert("ERR", err.error, "");
          } else this.showAlert("WARN", err.error, "");
        }
      );
    } else this.showAlert("WARN", "Por favor, ingrese un pin válido.", "");
  }

  abrirBalance() {
    this.OperacionesService.Balance(this.tarjetaSinGuiones).subscribe(
      (res: any) => {
        if (res.code == 200) {
          this.paso = 5;
          this.dineroEnCuenta = res.data.dineroEnCuenta;
          this.fechaVencimiento = res.data.fechaVencimiento;
          this.operaciones = res.data.operaciones;
        } else this.showAlert("WARN", res.message, "");
      },
      err => {
        this.showAlert("WARN", err.error, "");
      }
    )
  }

  retirarDinero() {
    if (this.cantidadRetiro != "0") {
      this.OperacionesService.Retiro(this.tarjetaSinGuiones, this.cantidadRetiro).subscribe(
        (res: any) => {
          if (res.code == 200) {
            this.paso = 4;
            this.showAlert("OK", res.message, "");
          } else this.showAlert("WARN", res.message, "");
        },
        err => {
          this.showAlert("WARN", err.error, "");
        }
      )
    } else this.showAlert("WARN", "Por favor, ingrese una cantidad válida.", "");
  }

  limpiarCampos() {
    this.numeroTarjeta = 'XXXX-XXXX-XXXX-XXXX';
    this.numeroPin = 'XXXX';
    this.tarjetaSinGuiones = '';
  }

  salirAplicacion() {
    this.limpiarCampos();
    this.paso = 1;
    this.fechaVencimiento = new Date();
    this.dineroEnCuenta = 0;
    this.operaciones = [];
  }

}

