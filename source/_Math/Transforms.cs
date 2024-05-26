using ChaosFramework.Math.Vectors;
using static ChaosFramework.Math.Clamping;

namespace ChaosFramework.Math
{
    public static class Transforms
    {
        public static Quaternion IncrementOrientation(
            Quaternion orientation,
            Vector3f angularIncrement,
            float step
            )
        {
            Quaternion spin = new Quaternion(
                angularIncrement.x * 0.5f,
                angularIncrement.y * 0.5f,
                angularIncrement.z * 0.5f, 0
                ) * orientation;
            return Quaternion.Normalize(
                orientation
                + new Quaternion(spin.x * step, spin.y * step, spin.z * step, spin.w * step)
                );
        }

        public static Vector2f InterpolatePosition(Vector2f position, Vector2f targetPos, float f)
            => position
               + Clamp(0, 1, f)
               * new Vector2f(
                   targetPos.x - position.x,
                   targetPos.y - position.y
                   );

        public static float SmoothStep(float edge0, float edge1, float x)
        {
            float t = Clamp(0, 1, (x - edge0) / (edge1 - edge0));
            return t * t * (3 - 2 * t);
        }

        public static float MapRange(
            float value,
            float fromMin,
            float fromMax,
            float toMin,
            float toMax
            )
        {
            if (fromMin == fromMax)
                return toMin;

            float newRange = toMax - toMin;
            float oldRange = fromMax - fromMin;
            return toMin + ((value - fromMin) / oldRange) * newRange;
        }
    }
}
