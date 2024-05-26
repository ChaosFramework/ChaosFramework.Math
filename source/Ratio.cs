using ChaosFramework.Math.Vectors;
using static ChaosFramework.Math.Clamping;

namespace ChaosFramework.Math
{
    public static class Ratio
    {
        public static Vector2f FitAinB(Vector2f b, Vector2f a) => a * FitAinBf(b, a);
        public static float FitAinBf(Vector2f b, Vector2f a) => Min(b.x / a.x, b.y / a.y);
    }
}
