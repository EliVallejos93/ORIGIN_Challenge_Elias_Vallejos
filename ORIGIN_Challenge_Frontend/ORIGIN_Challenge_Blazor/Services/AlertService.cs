using ORIGIN_Challenge_Blazor.DTOs;

namespace ORIGIN_Challenge_Blazor.Services
{
    public class AlertService
    {
        public event Action<Modal> OnShowAlert;

        public void ShowAlert(string tipo, string titulo, string contenido)
        {
            var modal = new Modal
            {
                Tipo = tipo,
                Titulo = titulo,
                Contenido = contenido,
                Visible = true
            };

            OnShowAlert?.Invoke(modal);
        }
    }
}
