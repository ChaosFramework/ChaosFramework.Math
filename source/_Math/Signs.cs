namespace ChaosFramework.Math
{
    public static class Signs
    {
        public static int Abs(int i) => System.Math.Abs(i);
        public static float Abs(float f) => System.Math.Abs(f);

        public static float Sign(float f) => System.Math.Sign(f);
        public static int SignBit(float f) => f < 0 ? 1 : 0;
        public static int InvSignBit(float f) => f >= 0 ? 1 : 0;
    }
}
