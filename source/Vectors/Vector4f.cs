using ChaosFramework.Collections.Immutable;
using ChaosFramework.IO;
using ChaosFramework.Math.Vectors.Definitions;
using ChaosFramework.Math.Vectors.Operations;
using ChaosUtil.Serialization.Text;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static ChaosFramework.Math.Exponentials;
using static ChaosFramework.Math.Signs;
using TK_Vec4 = OpenTK.Vector4;

namespace ChaosFramework.Math.Vectors
{
    [DebuggerDisplay("x={x}; y={y}; z={z}; w={w}")]
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Vector4f
        : Vector<Vector4f, float>
        , Vector4<Vector4f, float, Vector3f, Vector2f>
        , PotentiallyNaNVector
        , PotentiallyInfiniteVector
        , Addition<Vector4f, float>
        , Subtraction<Vector4f, float>
        , Multiplication<Vector4f, float, float>
        , Division<Vector4f, float, float>
        , DotProduct<Vector4f, float>
        , EuclideanLength<Vector4f, float>
        , EuclideanNormalization<Vector4f, float>
        , ComponentwiseAllComparison<Vector4f, float>
        , ComponentwiseAllComparison<Vector4f, float, float>
        , Clamping<Vector4f, float, float>
        , Clamping<Vector4f, float, Vector4f>
    {
        [ChaosIO.RegisterType]
        static void RegisterIO() => ChaosIO.AddType(Read, Write);

        static Vector4f Read(System.IO.BinaryReader reader)
            => new Vector4f(reader.Read<float>(), reader.Read<float>(), reader.Read<float>(), reader.Read<float>());

        static void Write(System.IO.BinaryWriter writer, Vector4f value)
        {
            writer.WriteAs(value.x);
            writer.WriteAs(value.y);
            writer.WriteAs(value.z);
            writer.WriteAs(value.w);
        }

        public static bool TryParse(string str, out Vector4f v)
            => TryParse(str, Constants.VECTOR_COMPONENT_SEPARATOR, out v);

        public static bool TryParse(string str, char[] splitCharacters, out Vector4f v)
        {
            v = EMPTY;
            string[] values = str.Split(splitCharacters);
            if (values.Length == 1)
                if (Parse.TryParse(values[0], out v.x))
                {
                    v.w = v.z = v.y = v.x;
                    return true;
                }
            if (values.Length == 2)
                if (Parse.TryParse(values[0], out v.x))
                    if (Parse.TryParse(values[1], out v.z))
                    {
                        v.y = v.x;
                        v.w = v.z;
                        return true;
                    }
            if (values.Length == 4)
                if (Parse.TryParse(values[0], out v.x))
                    if (Parse.TryParse(values[1], out v.y))
                        if (Parse.TryParse(values[2], out v.z))
                            if (Parse.TryParse(values[3], out v.w))
                                return true;
            v = EMPTY;
            return false;
        }

        static Vector4f()
        {
            Parse.AddParser<Vector4f>(TryParse);
        }

        /// <summary> Contains all carthesian coordinate vectors with both negative and positive sign. </summary>
        public static readonly ImmutableArray<Vector4f> carthesianDirection = new Vector4f[] {
            new Vector4f(-1,  0,  0,  0),
            new Vector4f( 1,  0,  0,  0),
            new Vector4f( 0, -1,  0,  0),
            new Vector4f( 0,  1,  0,  0),
            new Vector4f( 0,  0, -1,  0),
            new Vector4f( 0,  0,  1,  0),
            new Vector4f( 0,  0,  0, -1),
            new Vector4f( 0,  0,  0,  1),
        };

        /// <summary> Returns an empty vector. (All coordinates are set to zero.) </summary>
        public static readonly Vector4f EMPTY = new Vector4f();

        /// <summary> Returns a vector with all coordinates set to <see cref="float.NaN"/>. </summary>
        public static readonly Vector4f NAN = new Vector4f(float.NaN, float.NaN, float.NaN, float.NaN);

        /// <summary> Returns a vector with all coordinates set to <see cref="float.MinValue"/>. </summary>
        public static readonly Vector4f MIN_VALUE = new Vector4f(float.MinValue, float.MinValue, float.MinValue, float.MinValue);

        /// <summary> Returns a vector with all coordinates set to <see cref="float.MaxValue"/>. </summary>
        public static readonly Vector4f MAX_VALUE = new Vector4f(float.MaxValue, float.MaxValue, float.MaxValue, float.MaxValue);

        /// <summary> Return a vector with all coordinates set to <see cref="float.PositiveInfinity"/>. </summary>
        public static readonly Vector4f POSITIVE_INFINITY = new Vector4f(float.PositiveInfinity);

        /// <summary> Return a vector with all coordinates set to <see cref="float.NegativeInfinity"/>. </summary>
        public static readonly Vector4f NEGATIVE_INFINITY = new Vector4f(float.NegativeInfinity);

        public float x, y, z, w;

        float Vector2<Vector2f, float>.x { get { return x; } set { x = value; } }
        float Vector2<Vector2f, float>.y { get { return y; } set { y = value; } }
        float Vector3<Vector3f, float, Vector2f>.z { get { return z; } set { z = value; } }
        float Vector4<Vector4f, float, Vector3f, Vector2f>.w { get { return w; } set { w = value; } }

        public Vector2f xy { get { return new Vector2f(x, y); } set { x = value.x; y = value.y; } }
        public Vector2f xz { get { return new Vector2f(x, z); } set { x = value.x; z = value.y; } }
        public Vector2f xw { get { return new Vector2f(x, w); } set { x = value.x; w = value.y; } }
        public Vector2f yz { get { return new Vector2f(y, z); } set { y = value.x; z = value.y; } }
        public Vector2f yw { get { return new Vector2f(y, w); } set { y = value.x; w = value.y; } }
        public Vector2f zw { get { return new Vector2f(z, w); } set { z = value.x; w = value.y; } }
        public Vector3f xyz { get { return new Vector3f(x, y, z); } set { x = value.x; y = value.y; z = value.z; } }
        public Vector3f yzw { get { return new Vector3f(y, z, w); } set { y = value.x; z = value.y; w = value.z; } }

        public Vector3f x0z => new Vector3f(x, 0, z);
        public Vector3f xy0 => new Vector3f(x, y, 0);

        public float this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                    case 3: return w;
                    default: throw new IndexOutOfRangeException();
                }
            }
            set
            {
                switch (i)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    case 2: z = value; break;
                    case 3: w = value; break;
                    default: throw new IndexOutOfRangeException();
                }

            }
        }

        public Vector4f(float v) { x = y = z = w = v; }
        public Vector4f(float x, float y, float z, float w) { this.x = x; this.y = y; this.z = z; this.w = w; }
        public Vector4f(Vector2f xy, float z, float w) { x = xy.x; y = xy.y; this.z = z; this.w = w; }
        public Vector4f(float x, Vector2f yz, float w) { this.x = x; y = yz.x; z = yz.y; this.w = w; }
        public Vector4f(float x, float y, Vector2f zw) { this.x = x; this.y = y; z = zw.x; w = zw.y; }
        public Vector4f(Vector2f xy, Vector2f zw) { x = xy.x; y = xy.y; z = zw.x; w = zw.y; }
        public Vector4f(Vector3f xyz, float w) { x = xyz.x; y = xyz.y; z = xyz.z; this.w = w; }
        public Vector4f(float x, Vector3f yzw) { this.x = x; y = yzw.x; z = yzw.y; w = yzw.z; }

        public bool IsNaN() => float.IsNaN(x) || float.IsNaN(y) || float.IsNaN(z) || float.IsNaN(w);
        public bool IsInfinite() => float.IsInfinity(x) || float.IsInfinity(y) || float.IsInfinity(z) || float.IsInfinity(w);
        public bool IsNormalized(float threshold = 1e-4f) => Abs(Length() - 1) < threshold;
        public float LengthSq() => x * x + y * y + z * z + w * w;
        public float Length() => Sqrt(LengthSq());
        public void Normalize()
        {
            float len = Length();
            if (len == 0)
                x = y = z = w = 0;
            else
            {
                x /= len;
                y /= len;
                z /= len;
                w /= len;
            }
        }
        public void Normalize(float newLength) { Normalize(); x *= newLength; y *= newLength; z *= newLength; w *= newLength; }
        public static float Dot(Vector4f a, Vector4f b) => a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        public static Vector4f Normalize(Vector4f v) { v.Normalize(); return v; }
        public static Vector4f Normalize(Vector4f v, float newLength) { v.Normalize(newLength); return v; }

        public static Vector4f Transform(Vector4f v, Matrix m) => new Vector4f(
            v.x * m.m00 + v.y * m.m10 + v.z * m.m20 + v.w * m.m30,
            v.x * m.m01 + v.y * m.m11 + v.z * m.m21 + v.w * m.m31,
            v.x * m.m02 + v.y * m.m12 + v.z * m.m22 + v.w * m.m32,
            v.x * m.m03 + v.y * m.m13 + v.z * m.m23 + v.w * m.m33);

        public static Vector4f[] Transform(Vector4f[] v, Matrix m) => Array.ConvertAll(v, a => Transform(a, m));

        /// <summary> Returns a new <see cref="Vector4f"/> consisting of the componentwise products of all given vectors </summary>
        public static Vector4f ComponentWiseMul(Vector4f v0, Vector4f v1)
            => new Vector4f(v0.x * v1.x, v0.y * v1.y, v0.z * v1.z, v0.w * v1.w);

        /// <summary> Returns a new <see cref="Vector4f"/> consisting of the componentwise products of all given vectors </summary>
        public static Vector4f ComponentWiseMul(Vector4f v0, params Vector4f[] v)
        {
            foreach (Vector4f a in v)
                v0 = new Vector4f(v0.x * a.x, v0.y * a.y, v0.z * a.z, v0.w *= a.w);
            return v0;
        }

        public Vector4i SignBits() => new Vector4i(SignBit(x), SignBit(y), SignBit(z), SignBit(w));

        public static Vector4f operator -(Vector4f v) => new Vector4f(-v.x, -v.y, -v.z, -v.w);
        public static Vector4f operator +(Vector4f a, Vector4f b) => new Vector4f(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        public static Vector4f operator -(Vector4f a, Vector4f b) => new Vector4f(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        public static Vector4f operator *(Vector4f v, float f) => new Vector4f(v.x * f, v.y * f, v.z * f, v.w * f);
        public static Vector4f operator *(float f, Vector4f v) => new Vector4f(v.x * f, v.y * f, v.z * f, v.w * f);
        public static Vector4f operator /(Vector4f v, float f) => new Vector4f(v.x / f, v.y / f, v.z / f, v.w / f);
        public static Vector4f operator /(float f, Vector4f v) => new Vector4f(f / v.x, f / v.y, f / v.z, f / v.w);
        public static bool operator ==(Vector4f a, Vector4f b) => a.x == b.x && a.y == b.y && a.z == b.z && a.w == b.w;
        public static bool operator !=(Vector4f a, Vector4f b) => a.x != b.x || a.y != b.y || a.z != b.z || a.w != b.w;
        public override bool Equals(object other) => other is Vector4f && Equals((Vector4f)other);
        public bool Equals(Vector4f other) => this == other;
        public override int GetHashCode() => (int)(x + y + z + w);

        public static implicit operator TK_Vec4(Vector4f v) => *(TK_Vec4*)&v;
        public static implicit operator Vector4f(TK_Vec4 v) => *(Vector4f*)&v;
        public static implicit operator Vector4f(float f) => new Vector4f(f, f, f, f);

        public float Dot(Vector4f other) => Dot(this, other);
        public Vector4f GetNormalized() => Normalize(this);

        public void Add(Vector4f other) => this += other;
        public void Subtract(Vector4f other) => this -= other;
        public void Multiply(float factor) => this *= factor;
        public void Divide(float divisor) => this /= divisor;

        public Vector4f Sum(Vector4f a) => this + a;
        public Vector4f Difference(Vector4f a) => this - a;
        public Vector4f Product(float f) => this * f;
        public Vector4f Quotient(float f) => this / f;

        public bool LessEquals(float compare) => x <= compare && y <= compare && z <= compare && w <= compare;
        public bool LessEquals(Vector4f compare) => x <= compare.x && y <= compare.y && z <= compare.z && w <= compare.w;
        public bool Less(float compare) => x < compare && y < compare && z < compare && w < compare;
        public bool Less(Vector4f compare) => x < compare.x && y < compare.y && z < compare.z && w < compare.w;
        public bool GreaterEquals(float compare) => x >= compare && y >= compare && z >= compare && w >= compare;
        public bool GreaterEquals(Vector4f compare) => x >= compare.x && y >= compare.y && z >= compare.z && w >= compare.w;
        public bool Greater(float compare) => x > compare && y > compare && z > compare && w > compare;
        public bool Greater(Vector4f compare) => x > compare.x && y > compare.y && z > compare.z && w > compare.w;

        public Vector4f Min(float compare) => Clamping.Min(this, compare);
        public Vector4f Min(Vector4f compare) => Clamping.Min(this, compare);
        public Vector4f Max(float compare) => Clamping.Max(this, compare);
        public Vector4f Max(Vector4f compare) => Clamping.Max(this, compare);
    }
}
