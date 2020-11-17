namespace CarMobileApp
{
    public class FletcherCheksum
    {
        public static int CalculateChecksum(byte[] data)
        {
            byte a = 0;
            byte b = 0;

            foreach (byte item in data)
            {
                a = (byte)((a + item) % 255);
                b = (byte)((a + b) % 255);
            }

            return ((b << 8 ) | a) % 256;
        }
    }
}
