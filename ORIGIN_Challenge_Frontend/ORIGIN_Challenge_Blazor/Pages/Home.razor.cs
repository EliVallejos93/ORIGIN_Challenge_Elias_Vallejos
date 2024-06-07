using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using ORIGIN_Challenge_Blazor.DTOs;
using ORIGIN_Challenge_Blazor.Models;
using ORIGIN_Challenge_Blazor.Services;
using ORIGIN_Challenge_Blazor.Shared;

namespace ORIGIN_Challenge_Blazor.Pages
{
    public partial class Home
    {
        private bool spinner = false;
        private int paso = 1;
        private string tarjetaSinGuiones = string.Empty;
        private DateTime fechaVencimiento = DateTime.Now;
        private decimal dineroEnCuenta = 0;
        private ICollection<Operaciones> operaciones = new List<Operaciones>();
        private Modal modal = new Modal { Visible = false, Tipo = "", Titulo = "", Contenido = "" };
        private string campoVisualizacion = string.Empty;
        private string titulo = "";


        private void TecladoNumerico_ShowAlert(string titulo)
        {
            ShowAlert("WARN", titulo, "");
        }

        private void TecladoNumerico_VerificarNumero((string tipo, string numero) datosTeclado)
        {
            VerificarNumero(datosTeclado.tipo, datosTeclado.numero);
        }

        private void ShowAlert(string tipo, string titulo, string contenido)
        {
            try
            {
                modal = new Modal
                {
                    Visible = true,
                    Tipo = tipo,
                    Titulo = titulo,
                    Contenido = contenido
                };

                StateHasChanged();

                Task.Delay(2000).ContinueWith(_ =>
                {
                    modal.Visible = false;
                    InvokeAsync(StateHasChanged);
                });
            }
            catch (Exception ex)
            {
                AlertService.ShowAlert("WARN", ex.Message, "");
            }
        }

        private async Task InsertarDatosAleatorios()
        {
            try
            {
                spinner = true;
                await TarjetasService.InsertarDatosAleatorios();
                AlertService.ShowAlert("OK", "Datos aleatorios insertados correctamente", "");
            }
            catch (Exception ex)
            {
                AlertService.ShowAlert("WARN", ex.Message, "");
            }
            finally
            {
                spinner = false;
            }
        }

        private void VerificarNumero(string tipo, string numero)
        {
            try
            {
                if (tipo == "esTarjeta") VerificarTarjeta(numero);
                if (tipo == "esPin") VerificarPin(numero);
                if (tipo == "esRetiro") RetirarDinero(numero);
            }
            catch (Exception ex)
            {
                AlertService.ShowAlert("WARN", ex.Message, "");
            }
            finally
            {
                spinner = false;
            }
        }

        private async Task VerificarTarjeta(string numeroTarjeta)
        {
            spinner = true;
            tarjetaSinGuiones = numeroTarjeta;
            var res = await TarjetasService.VerificarTarjeta(tarjetaSinGuiones);

            string mensaje = ExtraerMensajeAPIResponse(await res.Content.ReadAsStringAsync());

            if (res.IsSuccessStatusCode)
            {
                paso = 3;
                ShowAlert("OK", mensaje, "");
            }
            else ShowAlert("WARN", mensaje, "");
        }

        private async Task VerificarPin(string numeroPin)
        {
            spinner = true;
            var res = await TarjetasService.VerificarPin(tarjetaSinGuiones, numeroPin);

            string mensaje = ExtraerMensajeAPIResponse(await res.Content.ReadAsStringAsync());

            if (res.IsSuccessStatusCode)
            {
                paso = 4;
                ShowAlert("OK", mensaje, "");
            }
            else ShowAlert("WARN", mensaje, "");
        }

        private async Task AbrirBalance()
        {
            try
            {
                spinner = true;
                var res = await OperacionesService.Balance(tarjetaSinGuiones);

                string mensaje = "";
                try
                {
                    var apiResponse = JsonConvert.DeserializeObject<BalanceResponse>(await res.Content.ReadAsStringAsync());

                    if (apiResponse != null && apiResponse.Data != null)
                    {
                        mensaje = apiResponse.Message;
                        dineroEnCuenta = apiResponse.Data.DineroEnCuenta;
                        fechaVencimiento = apiResponse.Data.FechaVencimiento;
                        operaciones = apiResponse.Data.Operaciones;
                    }
                }
                catch (JsonException)
                {
                    if (mensaje != null && mensaje != "") mensaje = await res.Content.ReadAsStringAsync();
                    else mensaje = "Error al deserializar la respuesta";
                }

                if (res.IsSuccessStatusCode) paso = 5;
                else ShowAlert("WARN", mensaje, "");
            }
            catch (Exception ex)
            {
                AlertService.ShowAlert("WARN", ex.Message, "");
            }
            finally
            {
                spinner = false;
            }
        }

        private async Task RetirarDinero(string numeroRetiro)
        {
            spinner = true;
            var res = await OperacionesService.Retiro(tarjetaSinGuiones, numeroRetiro);

            string mensaje = ExtraerMensajeAPIResponse(await res.Content.ReadAsStringAsync());

            if (res.IsSuccessStatusCode)
            {
                paso = 4;
                ShowAlert("OK", mensaje, "");
            }
            else ShowAlert("WARN", mensaje, "");
        }

        private void SalirAplicacion()
        {
            paso = 1;
            fechaVencimiento = DateTime.Now;
            dineroEnCuenta = 0;
            operaciones.Clear();
        }

        private string ExtraerMensajeAPIResponse(string contenido)
        {
            try
            {
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(contenido);
                return apiResponse?.Message ?? "No se pudo extraer el mensaje";
            }
            catch (JsonException)
            {
                if (contenido != null && contenido != "") return contenido;
                else return "Error al deserializar la respuesta";
            }
        }




        // OnInitialized es parte del ciclo de vida del componente. Al ser Home el componente inicial
        // voy a incluir toda la registracion de eventos aqui.
        protected override void OnInitialized()
        {
            try
            {
                // Esto suscribe el método ShowAlertHandler al evento OnShowAlert del servicio AlertService.
                // Cuando se muestra una alerta, se ejecutará el método ShowAlertHandler.
                AlertService.OnShowAlert += ShowAlertHandler;
                // la llamada a la alerta propia.
                ShowAlert("", "Hola", "Bienvenido a mi app");
            }
            catch (Exception ex)
            {
                AlertService.ShowAlert("WARN", ex.Message, "");
            }
        }
        // ShowAlertHandler() es el metodo que voy a pasar a OnShowAlert como registro de evento.
        // Esto no lo puedo tener dentro de AlertService por varias razones.
        // El método StateHasChanged() es específico de los componentes Blazor y se utiliza para notificar al sistema
        // de renderizado que se deben actualizar los componentes. En general, los servicios no deberían estar
        // directamente relacionados con el sistema de renderizado.
        private void ShowAlertHandler(Modal _modal)
        {
            try
            {
                modal = _modal;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                AlertService.ShowAlert("WARN", ex.Message, "");
            }
        }
    }
}
