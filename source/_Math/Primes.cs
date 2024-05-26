namespace ChaosFramework.Math
{
    public static class Primes
    {
        public static bool IsPrime(ulong a)
        {
            if (a <= 1) return false;
            if (a == 2) return true;
            ulong b = (ulong)System.Math.Ceiling(System.Math.Sqrt(a));
            for (ulong i = 3; i < b; i += 2)
                if (a % i == 0)
                    return false;
            return true;
        }

        public static bool IsPrimeTo(ulong a, ulong b)
            => GreatestCommonDivisor(a, b) == 1;

        public static uint GreatestCommonDivisor(uint a, uint b)
            => (uint)GreatestCommonDivisor((ulong)a, b);

        public static ulong GreatestCommonDivisor(ulong a, ulong b)
        {
            while (a != 0 && b != 0)
                if (a > b)
                    a %= b;
                else
                    b %= a;

            return a == 0 ? b : a;
        }
    }
}
