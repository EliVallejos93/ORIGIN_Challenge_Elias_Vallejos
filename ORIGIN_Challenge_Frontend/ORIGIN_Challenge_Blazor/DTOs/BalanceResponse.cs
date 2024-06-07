using ORIGIN_Challenge_Blazor.Models;

namespace ORIGIN_Challenge_Blazor.DTOs
{
    public class BalanceResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public DataBalanceResponse Data { get; set; }
    }

    public class DataBalanceResponse
    {
        public decimal DineroEnCuenta { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public List<Operaciones> Operaciones { get; set; }
    }
}
