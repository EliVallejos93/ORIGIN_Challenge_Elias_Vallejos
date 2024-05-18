namespace ORIGIN_Challenge_Backend.Services
{
    public class IntentosPinService
    {
        private int contIntentosPin = 0;
        private int maxIntentos = 4;
        public IntentosPinService()
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
