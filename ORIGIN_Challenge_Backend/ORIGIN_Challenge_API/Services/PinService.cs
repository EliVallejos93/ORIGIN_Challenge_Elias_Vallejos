namespace ORIGIN_Challenge_Backend.Services
{
    public class PinService : IPinService
    {
        private int contIntentosPin = 0;
        private int maxIntentos = 4;
        public PinService()
        {
        }

        public bool IntentoLimiteAlcanzado()
        {
            contIntentosPin++;
            if (contIntentosPin >= maxIntentos) return true;
            else return false;
        }

        public void Reset()
        {
            contIntentosPin = 0;
        }
    }
}
