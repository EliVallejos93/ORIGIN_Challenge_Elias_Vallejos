namespace ORIGIN_Challenge_Blazor.Services
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    public class TarjetasService
    {
        private readonly HttpClient _httpClient;

        public TarjetasService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> InsertarDatosAleatorios()
        {
            return await _httpClient.GetAsync("Tarjetas/InsertarDatosAleatorios");
        }

        public async Task<HttpResponseMessage> VerificarTarjeta(string numeroTarjeta)
        {
            return await _httpClient.GetAsync($"Tarjetas/VerificarTarjeta?numeroTarjeta={numeroTarjeta}");
        }

        public async Task<HttpResponseMessage> VerificarPin(string numeroTarjeta, string numeroPin)
        {
            return await _httpClient.GetAsync($"Tarjetas/VerificarPin?numeroTarjeta={numeroTarjeta}&numeroPin={numeroPin}");
        }
    }

}
