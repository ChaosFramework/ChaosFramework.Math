namespace ChaosFramework.Math
{
    public static class Modulus
    {
        /// <summary> Returns the representation of a in the Z<paramref name="b"/>+ ring. </summary>
        /// <param name="a"> The dividend. </param>
        /// <param name="b"> The modulus. Must be positive. </param>
        public static int Mod(int a, int b)
        {
            int c = a % b;
            if (a < 0) c += b;
            return c;
        }

        /// <summary> Returns the representation of a in the R<paramref name="b"/>+ ring. </summary>
        /// <param name="a"> The dividend. </param>
        /// <param name="b"> The modulus. Must be positive. </param>
        public static float Mod(float a, float b)
        {
            float c = a % b;
            if (a < 0) c += b;
            return c;
        }

        /// <summary> Returns the representation of a in the R<paramref name="b"/>+ ring. </summary>
        /// <param name="a"> The dividend. </param>
        /// <param name="b"> The modulus. Must be positive. </param>
        public static double Mod(double a, double b)
        {
            double c = a % b;
            if (a < 0) c += b;
            return c;
        }
    }
}
