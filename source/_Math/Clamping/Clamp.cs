using ChaosFramework.Math.Vectors;

namespace ChaosFramework.Math
{
    public static partial class Clamping
    {
        /// <summary> Clamps a value between a given minimum and maximum. </summary>
        /// <param name="min"> Will be returned if <paramref name="value"/> is less than <paramref name="min"/>. </param>
        /// <param name="max"> Will be returned if <paramref name="value"/> is greater than <paramref name="max"/>. </param>
        /// <param name="value"> The value to be clamped between <paramref name="min"/> and <paramref name="max"/>. </param>
        public static byte Clamp(byte min, byte max, byte value) => value < min ? min : value > max ? max : value;

        /// <summary> Clamps a value between a given minimum and maximum. </summary>
        /// <param name="min"> Will be returned if <paramref name="value"/> is less than <paramref name="min"/>. </param>
        /// <param name="max"> Will be returned if <paramref name="value"/> is greater than <paramref name="max"/>. </param>
        /// <param name="value"> The value to be clamped between <paramref name="min"/> and <paramref name="max"/>. </param>
        public static short Clamp(short min, short max, short value) => value < min ? min : value > max ? max : value;

        /// <summary> Clamps a value between a given minimum and maximum. </summary>
        /// <param name="min"> Will be returned if <paramref name="value"/> is less than <paramref name="min"/>. </param>
        /// <param name="max"> Will be returned if <paramref name="value"/> is greater than <paramref name="max"/>. </param>
        /// <param name="value"> The value to be clamped between <paramref name="min"/> and <paramref name="max"/>. </param>
        public static ushort Clamp(ushort min, ushort max, ushort value) => value < min ? min : value > max ? max : value;

        /// <summary> Clamps a value between a given minimum and maximum. </summary>
        /// <param name="min"> Will be returned if <paramref name="value"/> is less than <paramref name="min"/>. </param>
        /// <param name="max"> Will be returned if <paramref name="value"/> is greater than <paramref name="max"/>. </param>
        /// <param name="value"> The value to be clamped between <paramref name="min"/> and <paramref name="max"/>. </param>
        public static int Clamp(int min, int max, int value) => value < min ? min : value > max ? max : value;

        /// <summary> Clamps a value between a given minimum and maximum. </summary>
        /// <param name="min"> Will be returned if <paramref name="value"/> is less than <paramref name="min"/>. </param>
        /// <param name="max"> Will be returned if <paramref name="value"/> is greater than <paramref name="max"/>. </param>
        /// <param name="value"> The value to be clamped between <paramref name="min"/> and <paramref name="max"/>. </param>
        public static uint Clamp(uint min, uint max, uint value) => value < min ? min : value > max ? max : value;

        /// <summary> Clamps a value between a given minimum and maximum. </summary>
        /// <param name="min"> Will be returned if <paramref name="value"/> is less than <paramref name="min"/>. </param>
        /// <param name="max"> Will be returned if <paramref name="value"/> is greater than <paramref name="max"/>. </param>
        /// <param name="value"> The value to be clamped between <paramref name="min"/> and <paramref name="max"/>. </param>
        public static long Clamp(long min, long max, long value) => value < min ? min : value > max ? max : value;

        /// <summary> Clamps a value between a given minimum and maximum. </summary>
        /// <param name="min"> Will be returned if <paramref name="value"/> is less than <paramref name="min"/>. </param>
        /// <param name="max"> Will be returned if <paramref name="value"/> is greater than <paramref name="max"/>. </param>
        /// <param name="value"> The value to be clamped between <paramref name="min"/> and <paramref name="max"/>. </param>
        public static ulong Clamp(ulong min, ulong max, ulong value) => value < min ? min : value > max ? max : value;

        /// <summary> Clamps a value between a given minimum and maximum. </summary>
        /// <param name="min"> Will be returned if <paramref name="value"/> is less than <paramref name="min"/>. </param>
        /// <param name="max"> Will be returned if <paramref name="value"/> is greater than <paramref name="max"/>. </param>
        /// <param name="value"> The value to be clamped between <paramref name="min"/> and <paramref name="max"/>. </param>
        public static float Clamp(float min, float max, float value) => value < min ? min : value > max ? max : value;

        /// <summary> Clamps a value between a given minimum and maximum. </summary>
        /// <param name="min"> Will be returned if <paramref name="value"/> is less than <paramref name="min"/>. </param>
        /// <param name="max"> Will be returned if <paramref name="value"/> is greater than <paramref name="max"/>. </param>
        /// <param name="value"> The value to be clamped between <paramref name="min"/> and <paramref name="max"/>. </param>
        public static double Clamp(double min, double max, double value) => value < min ? min : value > max ? max : value;

        /// <summary> Clamps a value between a given minimum and maximum. </summary>
        /// <param name="min"> Will be returned if <paramref name="value"/> is less than <paramref name="min"/>. </param>
        /// <param name="max"> Will be returned if <paramref name="value"/> is greater than <paramref name="max"/>. </param>
        /// <param name="value"> The value to be clamped between <paramref name="min"/> and <paramref name="max"/>. </param>
        public static decimal Clamp(decimal min, decimal max, decimal value) => value < min ? min : value > max ? max : value;

        /// <summary>
        ///     Clamps all components of the given <see cref="Vector2f"/> between <paramref name="min"/> and <paramref name="max"/>.
        ///     (See <seealso cref="Clamp(float, float, float)"/>)
        /// </summary>
        /// <param name="min"> The minimum value for each component of <paramref name="value"/>. </param>
        /// <param name="max"> The maximum value for each component of <paramref name="value"/>. </param>
        /// <param name="value"> The vector to be clamped. </param>
        public static Vector2f Clamp(float min, float max, Vector2f value)
            => new Vector2f(Clamp(min, max, value.x), Clamp(min, max, value.y));

        /// <summary>
        ///     Clamps all components of the given <see cref="Vector3f"/> between <paramref name="min"/> and <paramref name="max"/>.
        ///     (See <seealso cref="Clamp(float, float, float)"/>)
        /// </summary>
        /// <param name="min"> The minimum value for each component of <paramref name="value"/>. </param>
        /// <param name="max"> The maximum value for each component of <paramref name="value"/>. </param>
        /// <param name="value"> The vector to be clamped. </param>
        public static Vector3f Clamp(float min, float max, Vector3f value)
            => new Vector3f(Clamp(min, max, value.x), Clamp(min, max, value.y), Clamp(min, max, value.z));

        /// <summary>
        ///     Clamps all components of the given <see cref="Vector4f"/> between <paramref name="min"/> and <paramref name="max"/>.
        ///     (See <seealso cref="Clamp(float, float, float)"/>)
        /// </summary>
        /// <param name="min"> The minimum value for each component of <paramref name="value"/>. </param>
        /// <param name="max"> The maximum value for each component of <paramref name="value"/>. </param>
        /// <param name="value"> The vector to be clamped. </param>
        public static Vector4f Clamp(float min, float max, Vector4f value)
            => new Vector4f(Clamp(min, max, value.x), Clamp(min, max, value.y), Clamp(min, max, value.z), Clamp(min, max, value.w));

        /// <summary>
        ///     Clamps all components of the given <see cref="Vector2i"/> between <paramref name="min"/> and <paramref name="max"/>.
        ///     (See <seealso cref="Clamp(int, int, int)"/>)
        /// </summary>
        /// <param name="min"> The minimum value for each component of <paramref name="value"/>. </param>
        /// <param name="max"> The maximum value for each component of <paramref name="value"/>. </param>
        /// <param name="value"> The vector to be clamped. </param>
        public static Vector2i Clamp(int min, int max, Vector2i value)
            => new Vector2i(Clamp(min, max, value.x), Clamp(min, max, value.y));

        /// <summary>
        ///     Clamps all components of the given <see cref="Vector3i"/> between <paramref name="min"/> and <paramref name="max"/>.
        ///     (See <seealso cref="Clamp(int, int, int)"/>)
        /// </summary>
        /// <param name="min"> The minimum value for each component of <paramref name="value"/>. </param>
        /// <param name="max"> The maximum value for each component of <paramref name="value"/>. </param>
        /// <param name="value"> The vector to be clamped. </param>
        public static Vector3i Clamp(int min, int max, Vector3i value)
            => new Vector3i(Clamp(min, max, value.x), Clamp(min, max, value.y), Clamp(min, max, value.z));

        /// <summary>
        ///     Clamps all components of the given <see cref="Vector4i"/> between <paramref name="min"/> and <paramref name="max"/>.
        ///     (See <seealso cref="Clamp(int, int, int)"/>)
        /// </summary>
        /// <param name="min"> The minimum value for each component of <paramref name="value"/>. </param>
        /// <param name="max"> The maximum value for each component of <paramref name="value"/>. </param>
        /// <param name="value"> The vector to be clamped. </param>
        public static Vector4i Clamp(int min, int max, Vector4i value)
            => new Vector4i(Clamp(min, max, value.x), Clamp(min, max, value.y), Clamp(min, max, value.z), Clamp(min, max, value.w));
    }
}
