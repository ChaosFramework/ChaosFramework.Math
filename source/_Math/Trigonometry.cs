namespace ChaosFramework.Math
{
    public static class Trigonometry
    {
        // TODO: provide double overloads in order not to require the user to juggle APIs

        public static float Sin(float f) => (float)System.Math.Sin(f);
        public static float Cos(float f) => (float)System.Math.Cos(f);
        public static float Tan(float f) => (float)System.Math.Tan(f);
        public static float ACos(float f) => (float)System.Math.Acos(f);
        public static float ASin(float f) => (float)System.Math.Asin(f);
        public static float ATan(float f) => (float)System.Math.Atan(f);
        public static float ATan2(float x, float y) => (float)System.Math.Atan2(x, y);
    }
}
