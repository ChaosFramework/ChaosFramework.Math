namespace ChaosFramework.Math
{
    public static class Exponentials
    {
        public static float Sqrt(float f) => (float)System.Math.Sqrt(f);

        /// <summary> Returns <paramref name="a"/> to the power of <paramref name="b">. </summary>
        /// <param name="a"> The base. </param>
        /// <param name="b"> The exponent. </param>
        public static int Pow(int a, uint b)
        {
            if (b == 0)
                return 1;

            int result = a;
            for (int i = 1; i < b; i++)
                result *= a;

            return result;
        }

        /// <summary>
        ///     Evaluates a gaussian curve
        ///     defined by the parameters <paramref name="a"/>, <paramref name="b"/> and <paramref name="w"/>
        ///     at offset <paramref name="x"/>.
        /// </summary>
        /// <param name="a"> Peak. </param>
        /// <param name="b"> X offset. </param>
        /// <param name="w"> X scale. </param>
        /// <param name="x"> The input to evaluate the curve at. </param>
        /// <returns> The value of the gaussian curve at offset <paramref name="x"/>. </returns>
        public static float Gauss(float a, float b, float w, float x)
        {
            float exp = (x - b) / w;
            return a * (float)System.Math.Exp(-4 * Constants.LOG2 * exp * exp);
        }

        /// <summary>
        ///     Approximates eˣ using Nicol N. Schraudolph's algorithm for
        ///     'A Fast, Compact Approximation of the Exponential Function'.
        ///
        ///     <para>
        ///         Source: <br/>
        ///         A Fast, Compact Approximation of the Exponential Function <br/>
        ///         Neural Computation, Volume 11, Issue 4, Pages 853-862 <br/>
        ///         May 15th 1999 <br/>
        ///         DOI: 10.1162/089976699300016467 <br/>
        ///         <see href="https://nic.schraudolph.org/pubs/Schraudolph99.pdf"/>
        ///     </para>
        /// </summary>
        /// <param name="x">
        ///     The exponent to evaluate the exponential function for.
        ///     Should be in range [-700;700] for reasonable results.
        /// </param>
        /// <returns> An approximation for eˣ. </returns>
        public static double FastExp(double x)
        {
            const int A = 1512775;    // 2²⁰ / ln(2)
            const int B = 1072693248; // 1023 · 2²⁰
            const int C = 60801;
            const int X = B - C;

            long d = (long)(A * x + X);
            return System.BitConverter.Int64BitsToDouble(d << 32 | d >> 32);
        }

        public static float EaseIn(float f)
            => 1 - 1 / (float)System.Math.Exp(f);
    }
}
