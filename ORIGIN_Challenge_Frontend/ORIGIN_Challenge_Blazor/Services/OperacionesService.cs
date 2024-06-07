namespace ORIGIN_Challenge_Blazor.Services
{
    public class OperacionesService
    {
        private readonly HttpClient _httpClient;

        public OperacionesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> Balance(string numeroTarjeta)
        {
            return await _httpClient.GetAsync($"/Operaciones/Balance?numeroTarjeta={numeroTarjeta}");
        }

        public async Task<HttpResponseMessage> Retiro(string numeroTarjeta, string cantidadRetiro)
        {
            return await _httpClient.GetAsync($"/Operaciones/Retiro?numeroTarjeta={numeroTarjeta}&cantidadRetiro={cantidadRetiro}");
        }
    }
}
