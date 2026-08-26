using ChaosFramework.Math.Vectors;

namespace ChaosFramework.Math
{
    public static partial class Clamping
    {
        /// <summary>
        ///     Returns the greatest of the given values.
        ///     In case of equality the return value is <paramref name="a"/>.
        /// </summary>
        /// <param name="a"> First value to be compared. </param>
        /// <param name="b"> Second value to be compared. </param>
        public static T Max<T>(T a, T b)
            where T : System.IComparable
            => a.CompareTo(b) >= 0 ? a : b;

        /// <summary>
        ///     Returns the greatest of the given values.
        ///     In case of equality the return value is whichever is listed earlier.
        /// </summary>
        /// <param name="v0"> First value to be compared. </param>
        /// <param name="v"> Further values to be compared. </param>
        public static T Max<T>(T v0, params T[] v)
            where T : System.IComparable
        {
            foreach (T b in v)
                v0 = Max(v0, b);
            return v0;
        }

        /// <summary> Returns the greatest of the given values. </summary>
        /// <param name="a"> First value to be compared. </param>
        /// <param name="b"> Second value to be compared. </param>
        public static byte Max(byte a, byte b) => a > b ? a : b;

        /// <summary> Returns the greatest of the given values. </summary>
        /// <param name="v0"> First value to be compared. </param>
        /// <param name="v"> Further values to be compared. </param>
        public static byte Max(byte v0, params byte[] v)
        {
            foreach (byte b in v)
                v0 = Max(v0, b);
            return v0;
        }

        /// <summary> Returns the greatest of the given values. </summary>
        /// <param name="a"> First value to be compared. </param>
        /// <param name="b"> Second value to be compared. </param>
        public static short Max(short a, short b) => a > b ? a : b;

        /// <summary> Returns the greatest of the given values. </summary>
        /// <param name="v0"> First value to be compared. </param>
        /// <param name="v"> Further values to be compared. </param>
        public static short Max(short v0, params short[] v)
        {
            foreach (short b in v)
                v0 = Max(v0, b);
            return v0;
        }

        /// <summary> Returns the greatest of the given values. </summary>
        /// <param name="a"> First value to be compared. </param>
        /// <param name="b"> Second value to be compared. </param>
        public static ushort Max(ushort a, ushort b) => a > b ? a : b;

        /// <summary> Returns the greatest of the given values. </summary>
        /// <param name="v0"> First value to be compared. </param>
        /// <param name="v"> Further values to be compared. </param>
        public static ushort Max(ushort v0, params ushort[] v)
        {
            foreach (ushort b in v)
                v0 = Max(v0, b);
            return v0;
        }

        /// <summary> Returns the greatest of the given values. </summary>
        /// <param name="a"> First value to be compared. </param>
        /// <param name="b"> Second value to be compared. </param>
        public static int Max(int a, int b) => a > b ? a : b;

        /// <summary> Returns the greatest of the given values. </summary>
        /// <param name="v0"> First value to be compared. </param>
        /// <param name="v"> Further values to be compared. </param>
        public static int Max(int v0, params int[] v)
        {
            foreach (int b in v)
                v0 = Max(v0, b);
            return v0;
        }

        /// <summary> Returns the greatest of the given values. </summary>
        /// <param name="a"> First value to be compared. </param>
        /// <param name="b"> Second value to be compared. </param>
        public static uint Max(uint a, uint b) => a > b ? a : b;

        /// <summary> Returns the greatest of the given values. </summary>
        /// <param name="v0"> First value to be compared. </param>
        /// <param name="v"> Further values to be compared. </param>
        public static uint Max(uint v0, params uint[] v)
        {
            foreach (uint b in v)
                v0 = Max(v0, b);
            return v0;
        }

        /// <summary> Returns the greatest of the given values. </summary>
        /// <param name="a"> First value to be compared. </param>
        /// <param name="b"> Second value to be compared. </param>
        public static long Max(long a, long b) => a > b ? a : b;

        /// <summary> Returns the greatest of the given values. </summary>
        /// <param name="v0"> First value to be compared. </param>
        /// <param name="v"> Further values to be compared. </param>
        public static long Max(long v0, params long[] v)
        {
            foreach (long b in v)
                v0 = Max(v0, b);
            return v0;
        }

        /// <summary> Returns the greatest of the given values. </summary>
        /// <param name="a"> First value to be compared. </param>
        /// <param name="b"> Second value to be compared. </param>
        public static ulong Max(ulong a, ulong b) => a > b ? a : b;

        /// <summary> Returns the greatest of the given values. </summary>
        /// <param name="v0"> First value to be compared. </param>
        /// <param name="v"> Further values to be compared. </param>
        public static ulong Max(ulong v0, params ulong[] v)
        {
            foreach (ulong b in v)
                v0 = Max(v0, b);
            return v0;
        }

        /// <summary> Returns the greatest of the given values. </summary>
        /// <param name="a"> First value to be compared. </param>
        /// <param name="b"> Second value to be compared. </param>
        public static float Max(float a, float b) => a > b ? a : b;

        /// <summary> Returns the greatest of the given values. </summary>
        /// <param name="v0"> First value to be compared. </param>
        /// <param name="v"> Further values to be compared. </param>
        public static float Max(float v0, params float[] v)
        {
            foreach (float b in v)
                v0 = Max(v0, b);
            return v0;
        }

        /// <summary> Returns the greatest of the given values. </summary>
        /// <param name="a"> First value to be compared. </param>
        /// <param name="b"> Second value to be compared. </param>
        public static double Max(double a, double b) => a > b ? a : b;

        /// <summary> Returns the greatest of the given values. </summary>
        /// <param name="v0"> First value to be compared. </param>
        /// <param name="v"> Further values to be compared. </param>
        public static double Max(double v0, params double[] v)
        {
            foreach (double b in v)
                v0 = Max(v0, b);
            return v0;
        }

        /// <summary> Returns the greatest of the given values. </summary>
        /// <param name="a"> First value to be compared. </param>
        /// <param name="b"> Second value to be compared. </param>
        public static decimal Max(decimal a, decimal b) => a > b ? a : b;

        /// <summary> Returns the greatest of the given values. </summary>
        /// <param name="v0"> First value to be compared. </param>
        /// <param name="v"> Further values to be compared. </param>
        public static decimal Max(decimal v0, params decimal[] v)
        {
            foreach (decimal b in v)
                v0 = Max(v0, b);
            return v0;
        }

        /// <summary> Returns a new <see cref="Vector2f"/> consisting of the componentwise maximum of both given vectors. </summary>
        /// <param name="a"> First value to be compared. </param>
        /// <param name="b"> Second value to be compared. </param>
        public static Vector2f Max(Vector2f a, Vector2f b)
            => new Vector2f(Max(a.x, b.x), Max(a.y, b.y));

        /// <summary> Returns a new <see cref="Vector2f"/> consisting of the componentwise maximum of all given vectors. </summary>
        /// <param name="v0"> First value to be compared. </param>
        /// <param name="v"> Further values to be compared. </param>
        public static Vector2f Max(Vector2f v0, params Vector2f[] v)
        {
            foreach (Vector2f a in v)
            {
                v0.x = Max(v0.x, a.x);
                v0.y = Max(v0.y, a.y);
            }
            return v0;
        }

        /// <summary> Returns a new <see cref="Vector3f"/> consisting of the componentwise maximum of both given vectors. </summary>
        /// <param name="a"> First value to be compared. </param>
        /// <param name="b"> Second value to be compared. </param>
        public static Vector3f Max(Vector3f a, Vector3f b)
            => new Vector3f(Max(a.x, b.x), Max(a.y, b.y), Max(a.z, b.z));

        /// <summary> Returns a new <see cref="Vector3f"/> consisting of the componentwise maximum of all given vectors. </summary>
        /// <param name="v0"> First value to be compared. </param>
        /// <param name="v"> Further values to be compared. </param>
        public static Vector3f Max(Vector3f v0, params Vector3f[] v)
        {
            foreach (Vector3f a in v)
            {
                v0.x = Max(v0.x, a.x);
                v0.y = Max(v0.y, a.y);
                v0.z = Max(v0.z, a.z);
            }
            return v0;
        }

        /// <summary> Returns a new <see cref="Vector4f"/> consisting of the componentwise maximum of both given vectors. </summary>
        /// <param name="a"> First value to be compared. </param>
        /// <param name="b"> Second value to be compared. </param>
        public static Vector4f Max(Vector4f a, Vector4f b)
            => new Vector4f(Max(a.x, b.x), Max(a.y, b.y), Max(a.z, b.z), Max(a.w, b.w));

        /// <summary> Returns a new <see cref="Vector4f"/> consisting of the componentwise maximum of all given vectors. </summary>
        /// <param name="v0"> First value to be compared. </param>
        /// <param name="v"> Further values to be compared. </param>
        public static Vector4f Max(Vector4f v0, params Vector4f[] v)
        {
            foreach (Vector4f a in v)
            {
                v0.x = Max(v0.x, a.x);
                v0.y = Max(v0.y, a.y);
                v0.z = Max(v0.z, a.z);
                v0.w = Max(v0.w, a.w);
            }
            return v0;
        }

        /// <summary> Returns a new <see cref="Vector2i"/> consisting of the componentwise maximum of both given vectors. </summary>
        /// <param name="a"> First value to be compared. </param>
        /// <param name="b"> Second value to be compared. </param>
        public static Vector2i Max(Vector2i a, Vector2i b)
            => new Vector2i(Max(a.x, b.x), Max(a.y, b.y));

        /// <summary> Returns a new <see cref="Vector2i"/> consisting of the componentwise maximum of all given vectors. </summary>
        /// <param name="v0"> First value to be compared. </param>
        /// <param name="v"> Further values to be compared. </param>
        public static Vector2i Max(Vector2i v0, params Vector2i[] v)
        {
            foreach (Vector2i a in v)
            {
                v0.x = Max(v0.x, a.x);
                v0.y = Max(v0.y, a.y);
            }
            return v0;
        }

        /// <summary> Returns a new <see cref="Vector2ui"/> consisting of the componentwise maximum of both given vectors. </summary>
        /// <param name="a"> First value to be compared. </param>
        /// <param name="b"> Second value to be compared. </param>
        public static Vector2ui Max(Vector2ui a, Vector2ui b)
            => new Vector2ui(Max(a.x, b.x), Max(a.y, b.y));

        /// <summary> Returns a new <see cref="Vector2i"/> consisting of the componentwise maximum of all given vectors. </summary>
        /// <param name="v0"> First value to be compared. </param>
        /// <param name="v"> Further values to be compared. </param>
        public static Vector2ui Max(Vector2ui v0, params Vector2ui[] v)
        {
            foreach (Vector2ui a in v)
            {
                v0.x = Max(v0.x, a.x);
                v0.y = Max(v0.y, a.y);
            }
            return v0;
        }

        /// <summary> Returns a new <see cref="Vector3i"/> consisting of the componentwise maximum of both given vectors. </summary>
        /// <param name="a"> First value to be compared. </param>
        /// <param name="b"> Second value to be compared. </param>
        public static Vector3i Max(Vector3i a, Vector3i b)
            => new Vector3i(Max(a.x, b.x), Max(a.y, b.y), Max(a.z, b.z));

        /// <summary> Returns a new <see cref="Vector3i"/> consisting of the componentwise maximum of all given vectors. </summary>
        /// <param name="v0"> First value to be compared. </param>
        /// <param name="v"> Further values to be compared. </param>
        public static Vector3i Max(Vector3i v0, params Vector3i[] v)
        {
            foreach (Vector3i a in v)
            {
                v0.x = Max(v0.x, a.x);
                v0.y = Max(v0.y, a.y);
                v0.z = Max(v0.z, a.z);
            }
            return v0;
        }

        /// <summary> Returns a new <see cref="Vector4i"/> consisting of the componentwise maximum of both given vectors. </summary>
        /// <param name="a"> First value to be compared. </param>
        /// <param name="b"> Second value to be compared. </param>
        public static Vector4i Max(Vector4i a, Vector4i b)
            => new Vector4i(Max(a.x, b.x), Max(a.y, b.y), Max(a.z, b.z), Max(a.w, b.w));

        /// <summary> Returns a new <see cref="Vector4i"/> consisting of the componentwise maximum of all given vectors. </summary>
        /// <param name="v0"> First value to be compared. </param>
        /// <param name="v"> Further values to be compared. </param>
        public static Vector4i Max(Vector4i v0, params Vector4i[] v)
        {
            foreach (Vector4i a in v)
            {
                v0.x = Max(v0.x, a.x);
                v0.y = Max(v0.y, a.y);
                v0.z = Max(v0.z, a.z);
                v0.w = Max(v0.w, a.w);
            }
            return v0;
        }
    }
}
