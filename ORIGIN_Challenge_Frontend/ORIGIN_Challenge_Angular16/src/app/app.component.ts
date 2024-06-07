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
  tarjetaSinGuiones: string = '';
  fechaVencimiento: Date = new Date();
  dineroEnCuenta: number = 0;
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

  verificarNumero(tipo: string, numero: string) {
    if (tipo == 'esTarjeta') this.verificarTarjeta(numero);
    if (tipo == 'esPin') this.verificarPin(numero);
    if (tipo == 'esRetiro') this.retirarDinero(numero);
  }

  verificarTarjeta(numeroTarjeta: string) {
    this.spinner = true;
    this.tarjetaSinGuiones = numeroTarjeta;
    this.TarjetaService.VerificarTarjeta(this.tarjetaSinGuiones).subscribe(
      (res: any) => {
        this.spinner = false;
        if (res.code == 200) {
          this.paso = 3;
          this.showAlert("OK", res.message, "");
        } else this.showAlert("WARN", res.message, "");
      },
      err => {
        this.spinner = false;
        this.showAlert("WARN", err.error, "");
      }
    );
  }

  verificarPin(numeroPin: string) {
    this.spinner = true;
    this.TarjetaService.VerificarPin(this.tarjetaSinGuiones, numeroPin).subscribe(
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
          this.showAlert("ERR", err.error, "");
        } else this.showAlert("WARN", err.error, "");
      }
    );
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

  retirarDinero(numeroRetiro: string) {
    this.OperacionesService.Retiro(this.tarjetaSinGuiones, numeroRetiro).subscribe(
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
  }

  salirAplicacion() {
    this.paso = 1;
    this.fechaVencimiento = new Date();
    this.dineroEnCuenta = 0;
    this.operaciones = [];
  }

}

