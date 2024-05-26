using ChaosFramework.Math.Vectors;

namespace ChaosFramework.Math
{
    public static partial class Clamping
    {
        /// <summary>
        ///     Returns the smallest of the given values.
        ///     In case of equality the return value is <paramref name="a"/>.
        /// </summary>
        /// <param name="a"> First value to be compared. </param>
        /// <param name="b"> Second value to be compared. </param>
        public static T Min<T>(T a, T b)
            where T : System.IComparable
            => a.CompareTo(b) <= 0 ? a : b;

        /// <summary>
        ///     Returns the smallest of the given values.
        ///     In case of equality the return value is whichever is listed earlier.
        /// </summary>
        /// <param name="v0"> First value to be compared. </param>
        /// <param name="v"> Further values to be compared. </param>
        public static T Min<T>(T v0, params T[] v)
            where T : System.IComparable
        {
            foreach (T b in v)
                v0 = Min(v0, b);
            return v0;
        }

        /// <summary> Returns the smallest of the given values. </summary>
        /// <param name="a"> First value to be compared. </param>
        /// <param name="b"> Second value to be compared. </param>
        public static byte Min(byte a, byte b) => a < b ? a : b;

        /// <summary> Returns the smallest of the given values. </summary>
        /// <param name="v0"> First value to be compared. </param>
        /// <param name="v"> Further values to be compared. </param>
        public static byte Min(byte v0, params byte[] v)
        {
            foreach (byte b in v)
                v0 = Min(v0, b);
            return v0;
        }

        /// <summary> Returns the smallest of the given values. </summary>
        /// <param name="a"> First value to be compared. </param>
        /// <param name="b"> Second value to be compared. </param>
        public static short Min(short a, short b) => a < b ? a : b;

        /// <summary> Returns the smallest of the given values. </summary>
        /// <param name="v0"> First value to be compared. </param>
        /// <param name="v"> Further values to be compared. </param>
        public static short Min(short v0, params short[] v)
        {
            foreach (short b in v)
                v0 = Min(v0, b);
            return v0;
        }

        /// <summary> Returns the smallest of the given values. </summary>
        /// <param name="a"> First value to be compared. </param>
        /// <param name="b"> Second value to be compared. </param>
        public static ushort Min(ushort a, ushort b) => a < b ? a : b;

        /// <summary> Returns the smallest of the given values. </summary>
        /// <param name="v0"> First value to be compared. </param>
        /// <param name="v"> Further values to be compared. </param>
        public static ushort Min(ushort v0, params ushort[] v)
        {
            foreach (ushort b in v)
                v0 = Min(v0, b);
            return v0;
        }

        /// <summary> Returns the smallest of the given values. </summary>
        /// <param name="a"> First value to be compared. </param>
        /// <param name="b"> Second value to be compared. </param>
        public static int Min(int a, int b) => a < b ? a : b;

        /// <summary> Returns the smallest of the given values. </summary>
        /// <param name="v0"> First value to be compared. </param>
        /// <param name="v"> Further values to be compared. </param>
        public static int Min(int v0, params int[] v)
        {
            foreach (int b in v)
                v0 = Min(v0, b);
            return v0;
        }

        /// <summary> Returns the smallest of the given values. </summary>
        /// <param name="a"> First value to be compared. </param>
        /// <param name="b"> Second value to be compared. </param>
        public static uint Min(uint a, uint b) => a < b ? a : b;

        /// <summary> Returns the smallest of the given values. </summary>
        /// <param name="v0"> First value to be compared. </param>
        /// <param name="v"> Further values to be compared. </param>
        public static uint Min(uint v0, params uint[] v)
        {
            foreach (uint b in v)
                v0 = Min(v0, b);
            return v0;
        }

        /// <summary> Returns the smallest of the given values. </summary>
        /// <param name="a"> First value to be compared. </param>
        /// <param name="b"> Second value to be compared. </param>
        public static long Min(long a, long b) => a < b ? a : b;

        /// <summary> Returns the smallest of the given values. </summary>
        /// <param name="v0"> First value to be compared. </param>
        /// <param name="v"> Further values to be compared. </param>
        public static long Min(long v0, params long[] v)
        {
            foreach (long b in v)
                v0 = Min(v0, b);
            return v0;
        }

        /// <summary> Returns the smallest of the given values. </summary>
        /// <param name="a"> First value to be compared. </param>
        /// <param name="b"> Second value to be compared. </param>
        public static ulong Min(ulong a, ulong b) => a < b ? a : b;

        /// <summary> Returns the smallest of the given values. </summary>
        /// <param name="v0"> First value to be compared. </param>
        /// <param name="v"> Further values to be compared. </param>
        public static ulong Min(ulong v0, params ulong[] v)
        {
            foreach (ulong b in v)
                v0 = Min(v0, b);
            return v0;
        }

        /// <summary> Returns the smallest of the given values. </summary>
        /// <param name="a"> First value to be compared. </param>
        /// <param name="b"> Second value to be compared. </param>
        public static float Min(float a, float b) => a < b ? a : b;

        /// <summary> Returns the smallest of the given values. </summary>
        /// <param name="v0"> First value to be compared. </param>
        /// <param name="v"> Further values to be compared. </param>
        public static float Min(float v0, params float[] v)
        {
            foreach (float b in v)
                v0 = Min(v0, b);
            return v0;
        }

        /// <summary> Returns the smallest of the given values. </summary>
        /// <param name="a"> First value to be compared. </param>
        /// <param name="b"> Second value to be compared. </param>
        public static double Min(double a, double b) => a < b ? a : b;

        /// <summary> Returns the smallest of the given values. </summary>
        /// <param name="v0"> First value to be compared. </param>
        /// <param name="v"> Further values to be compared. </param>
        public static double Min(double v0, params double[] v)
        {
            foreach (double b in v)
                v0 = Min(v0, b);
            return v0;
        }

        /// <summary> Returns the smallest of the given values. </summary>
        /// <param name="a"> First value to be compared. </param>
        /// <param name="b"> Second value to be compared. </param>
        public static decimal Min(decimal a, decimal b) => a < b ? a : b;

        /// <summary> Returns the smallest of the given values. </summary>
        /// <param name="v0"> First value to be compared. </param>
        /// <param name="v"> Further values to be compared. </param>
        public static decimal Min(decimal v0, params decimal[] v)
        {
            foreach (decimal b in v)
                v0 = Min(v0, b);
            return v0;
        }

        /// <summary> Returns a new <see cref="Vector2f"/> consisting of the componentwise minimum of both given vectors. </summary>
        /// <param name="a"> First value to be compared. </param>
        /// <param name="b"> Second value to be compared. </param>
        public static Vector2f Min(Vector2f a, Vector2f b)
            => new Vector2f(Min(a.x, b.x), Min(a.y, b.y));

        /// <summary> Returns a new <see cref="Vector2f"/> consisting of the componentwise minimum of all given vectors. </summary>
        /// <param name="v0"> First value to be compared. </param>
        /// <param name="v"> Further values to be compared. </param>
        public static Vector2f Min(Vector2f v0, params Vector2f[] v)
        {
            foreach (Vector2f a in v)
            {
                v0.x = Min(v0.x, a.x);
                v0.y = Min(v0.y, a.y);
            }
            return v0;
        }

        /// <summary> Returns a new <see cref="Vector3f"/> consisting of the componentwise minimum of both given vectors. </summary>
        /// <param name="a"> First value to be compared. </param>
        /// <param name="b"> Second value to be compared. </param>
        public static Vector3f Min(Vector3f a, Vector3f b)
            => new Vector3f(Min(a.x, b.x), Min(a.y, b.y), Min(a.z, b.z));

        /// <summary> Returns a new <see cref="Vector3f"/> consisting of the componentwise minimum of all given vectors. </summary>
        /// <param name="v0"> First value to be compared. </param>
        /// <param name="v"> Further values to be compared. </param>
        public static Vector3f Min(Vector3f v0, params Vector3f[] v)
        {
            foreach (Vector3f a in v)
            {
                v0.x = Min(v0.x, a.x);
                v0.y = Min(v0.y, a.y);
                v0.z = Min(v0.z, a.z);
            }
            return v0;
        }

        /// <summary> Returns a new <see cref="Vector4f"/> consisting of the componentwise minimum of both given vectors. </summary>
        /// <param name="a"> First value to be compared. </param>
        /// <param name="b"> Second value to be compared. </param>
        public static Vector4f Min(Vector4f a, Vector4f b)
            => new Vector4f(Min(a.x, b.x), Min(a.y, b.y), Min(a.z, b.z), Min(a.w, b.w));

        /// <summary> Returns a new <see cref="Vector4f"/> consisting of the componentwise minimum of all given vectors. </summary>
        /// <param name="v0"> First value to be compared. </param>
        /// <param name="v"> Further values to be compared. </param>
        public static Vector4f Min(Vector4f v0, params Vector4f[] v)
        {
            foreach (Vector4f a in v)
            {
                v0.x = Min(v0.x, a.x);
                v0.y = Min(v0.y, a.y);
                v0.z = Min(v0.z, a.z);
                v0.w = Min(v0.w, a.w);
            }
            return v0;
        }

        /// <summary> Returns a new <see cref="Vector2i"/> consisting of the componentwise minimum of both given vectors. </summary>
        /// <param name="a"> First value to be compared. </param>
        /// <param name="b"> Second value to be compared. </param>
        public static Vector2i Min(Vector2i a, Vector2i b)
            => new Vector2i(Min(a.x, b.x), Min(a.y, b.y));

        /// <summary> Returns a new <see cref="Vector2i"/> consisting of the componentwise minimum of all given vectors. </summary>
        /// <param name="v0"> First value to be compared. </param>
        /// <param name="v"> Further values to be compared. </param>
        public static Vector2i Min(Vector2i v0, params Vector2i[] v)
        {
            foreach (Vector2i a in v)
            {
                v0.x = Min(v0.x, a.x);
                v0.y = Min(v0.y, a.y);
            }
            return v0;
        }

        /// <summary> Returns a new <see cref="Vector3i"/> consisting of the componentwise minimum of both given vectors. </summary>
        /// <param name="a"> First value to be compared. </param>
        /// <param name="b"> Second value to be compared. </param>
        public static Vector3i Min(Vector3i a, Vector3i b)
            => new Vector3i(Min(a.x, b.x), Min(a.y, b.y), Min(a.z, b.z));

        /// <summary> Returns a new <see cref="Vector3i"/> consisting of the componentwise minimum of all given vectors. </summary>
        /// <param name="v0"> First value to be compared. </param>
        /// <param name="v"> Further values to be compared. </param>
        public static Vector3i Min(Vector3i v0, params Vector3i[] v)
        {
            foreach (Vector3i a in v)
            {
                v0.x = Min(v0.x, a.x);
                v0.y = Min(v0.y, a.y);
                v0.z = Min(v0.z, a.z);
            }
            return v0;
        }

        /// <summary> Returns a new <see cref="Vector4i"/> consisting of the componentwise minimum of both given vectors. </summary>
        /// <param name="a"> First value to be compared. </param>
        /// <param name="b"> Second value to be compared. </param>
        public static Vector4i Min(Vector4i a, Vector4i b)
            => new Vector4i(Min(a.x, b.x), Min(a.y, b.y), Min(a.z, b.z), Min(a.w, b.w));

        /// <summary> Returns a new <see cref="Vector4i"/> consisting of the componentwise minimum of all given vectors. </summary>
        /// <param name="v0"> First value to be compared. </param>
        /// <param name="v"> Further values to be compared. </param>
        public static Vector4i Min(Vector4i v0, params Vector4i[] v)
        {
            foreach (Vector4i a in v)
            {
                v0.x = Min(v0.x, a.x);
                v0.y = Min(v0.y, a.y);
                v0.z = Min(v0.z, a.z);
                v0.w = Min(v0.w, a.w);
            }
            return v0;
        }
    }
}
