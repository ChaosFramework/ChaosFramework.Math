using static ChaosFramework.Math.Signs;

namespace ChaosFramework.Math
{
    public static class Comparisons
    {
        public static float Deviation(float f1, float f2)
            => Abs(Abs(f1) - Abs(f2));

        public static bool AreEqual(params float[] values)
        {
            if (values.Length <= 1)
                return true;

            for (int i = 0; i < values.Length - 1; i++)
                if (values[i] != values[i + 1])
                    return false;

            return true;
        }

        public static bool AreAlmostEqual(float maxDeviation, params float[] values)
        {
            if (values.Length <= 1)
                return true;

            for (int i = 0; i < values.Length - 1; i++)
                for (int j = i + 1; j < values.Length; j++)
                    if (Deviation(values[i], values[j]) > maxDeviation)
                        return false;

            return true;
        }
    }
}
