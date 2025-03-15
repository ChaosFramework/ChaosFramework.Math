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
using TK_Vec3 = OpenTK.Vector3;

namespace ChaosFramework.Math.Vectors
{
    [DebuggerDisplay("x={x}; y={y}; z={z}")]
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Vector3f
        : Vector<Vector3f, float>
        , Vector3<Vector3f, float, Vector2f>
        , PotentiallyNaNVector
        , PotentiallyInfiniteVector
        , Addition<Vector3f, float>
        , Subtraction<Vector3f, float>
        , Multiplication<Vector3f, float, float>
        , Division<Vector3f, float, float>
        , DotProduct<Vector3f, float>
        , EuclideanLength<Vector3f, float>
        , EuclideanNormalization<Vector3f, float>
        , ComponentwiseAllComparison<Vector3f, float>
        , ComponentwiseAllComparison<Vector3f, float, float>
        , Clamping<Vector3f, float, float>
        , Clamping<Vector3f, float, Vector3f>
    {
        [ChaosIO.RegisterType]
        static void RegisterIO() => ChaosIO.AddType(Read, Write);

        internal static Vector3f Read(System.IO.BinaryReader reader)
            => new Vector3f(reader.Read<float>(), reader.Read<float>(), reader.Read<float>());

        internal static void Write(System.IO.BinaryWriter writer, Vector3f value)
        {
            writer.WriteAs(value.x);
            writer.WriteAs(value.y);
            writer.WriteAs(value.z);
        }

        public static bool TryParse(string str, out Vector3f v)
            => TryParse(str, Constants.VECTOR_COMPONENT_SEPARATOR, out v);

        public static bool TryParse(string str, char[] splitCharacters, out Vector3f v)
        {
            v = EMPTY;
            string[] values = str.Split(splitCharacters);
            if (values.Length == 1)
            {
                if (Parse.TryParse(values[0], out v.x))
                {
                    v.z = v.y = v.x;
                    return true;
                }
            }
            else if (values.Length == 3)
                if (Parse.TryParse(values[0], out v.x))
                    if (Parse.TryParse(values[1], out v.y))
                        if (Parse.TryParse(values[2], out v.z))
                            return true;
            v = EMPTY;
            return false;
        }

        static Vector3f()
        {
            Parse.AddParser<Vector3f>(TryParse);
        }

        /// <summary> Contains all carthesian coordinate vectors with both negative and positive sign. </summary>
        public static readonly ImmutableArray<Vector3f> carthesianDirection = new Vector3f[] {
            new Vector3f(-1,  0,  0),
            new Vector3f( 1,  0,  0),
            new Vector3f( 0, -1,  0),
            new Vector3f( 0,  1,  0),
            new Vector3f( 0,  0, -1),
            new Vector3f( 0,  0,  1),
        };

        /// <summary> Returns an empty vector. (All coordinates are set to zero.) </summary>
        public static readonly Vector3f EMPTY = new Vector3f();

        /// <summary> Returns a vector with all coordinates set to <see cref="float.NaN"/>. </summary>
        public static readonly Vector3f NAN = new Vector3f(float.NaN, float.NaN, float.NaN);

        /// <summary> Returns a vector with all coordinates set to <see cref="float.MinValue"/>. </summary>
        public static readonly Vector3f MIN_VALUE = new Vector3f(float.MinValue, float.MinValue, float.MinValue);

        /// <summary> Returns a vector with all coordinates set to <see cref="float.MaxValue"/>. </summary>
        public static readonly Vector3f MAX_VALUE = new Vector3f(float.MaxValue, float.MaxValue, float.MaxValue);

        /// <summary> Return a vector with all coordinates set to <see cref="float.PositiveInfinity"/>. </summary>
        public static readonly Vector3f POSITIVE_INFINITY = new Vector3f(float.PositiveInfinity);

        /// <summary> Return a vector with all coordinates set to <see cref="float.NegativeInfinity"/>. </summary>
        public static readonly Vector3f NEGATIVE_INFINITY = new Vector3f(float.NegativeInfinity);

        public float x, y, z;

        float Vector2<Vector2f, float>.x { get { return x; } set { x = value; } }
        float Vector2<Vector2f, float>.y { get { return y; } set { y = value; } }
        float Vector3<Vector3f, float, Vector2f>.z { get { return z; } set { z = value; } }

        public Vector2f xy { get { return new Vector2f(x, y); } set { x = value.x; y = value.y; } }
        public Vector2f yz { get { return new Vector2f(y, z); } set { y = value.x; z = value.y; } }
        public Vector2f xz { get { return new Vector2f(x, z); } set { x = value.x; z = value.y; } }

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
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        public Vector3f(float v) { x = y = z = v; }
        public Vector3f(float x, float y, float z) { this.x = x; this.y = y; this.z = z; }
        public Vector3f(Vector2f xy, float z) { x = xy.x; y = xy.y; this.z = z; }
        public Vector3f(float x, Vector2f yz) { this.x = x; y = yz.x; z = yz.y; }

        public float LengthSq() => x * x + y * y + z * z;
        public float Length() => Sqrt(LengthSq());
        public bool IsNaN() => float.IsNaN(x) || float.IsNaN(y) || float.IsNaN(z);
        public bool IsInfinite() => float.IsInfinity(x) || float.IsInfinity(y) || float.IsInfinity(z);
        public bool IsNormalized(float threshold = 1e-4f) => Abs(Length() - 1) < threshold;
        public void Normalize()
        {
            float len = Length();
            if (len == 0)
                x = y = z = 0;
            else
            {
                x /= len;
                y /= len;
                z /= len;
            }
        }
        public void Normalize(float newLength) { Normalize(); x *= newLength; y *= newLength; z *= newLength; }
        public static float Dot(Vector3f a, Vector3f b) => a.x * b.x + a.y * b.y + a.z * b.z;
        public static Vector3f Cross(Vector3f a, Vector3f b)
            => new Vector3f(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);
        public static Vector3f Normalize(Vector3f v) { v.Normalize(); return v; }
        public static Vector3f Normalize(Vector3f v, float newLength) { v.Normalize(newLength); return v; }

        public static Vector3f TransformNormal(Vector3f v, Matrix m) => Vector4f.Transform(new Vector4f(v, 0), m).xyz;
        public static Vector3f TransformCoordinate(Vector3f v, Matrix m)
        {
            Vector4f vec = Vector4f.Transform(new Vector4f(v, 1), m);
            return vec.xyz / vec.w;
        }
        public static Vector3f[] TransformNormal(Vector3f[] v, Matrix m) => Array.ConvertAll(v, _ => TransformNormal(_, m));
        public static Vector3f[] TransformCoordinate(Vector3f[] v, Matrix m) => Array.ConvertAll(v, _ => TransformCoordinate(_, m));

        /// <summary> Returns a new <see cref="Vector3f"/> consisting of the componentwise products of all given vectors. </summary>
        public static Vector3f ComponentWiseMul(Vector3f v0, Vector3f v1) => new Vector3f(v0.x * v1.x, v0.y * v1.y, v0.z * v1.z);

        /// <summary> Returns a new <see cref="Vector3f"/> consisting of the componentwise products of all given vectors. </summary>
        public static Vector3f ComponentWiseMul(Vector3f v0, params Vector3f[] v)
        {
            foreach (Vector3f a in v)
                v0 = new Vector3f(v0.x * a.x, v0.y * a.y, v0.z * a.z);
            return v0;
        }

        public Vector3i SignBits() => new Vector3i(SignBit(x), SignBit(y), SignBit(z));

        public static Vector3f operator -(Vector3f v) => new Vector3f(-v.x, -v.y, -v.z);
        public static Vector3f operator +(Vector3f a, Vector3f b) => new Vector3f(a.x + b.x, a.y + b.y, a.z + b.z);
        public static Vector3f operator -(Vector3f a, Vector3f b) => new Vector3f(a.x - b.x, a.y - b.y, a.z - b.z);
        public static Vector3f operator *(Vector3f v, float f) => new Vector3f(v.x * f, v.y * f, v.z * f);
        public static Vector3f operator *(float f, Vector3f v) => new Vector3f(v.x * f, v.y * f, v.z * f);
        public static Vector3f operator /(Vector3f v, float f) => new Vector3f(v.x / f, v.y / f, v.z / f);
        public static Vector3f operator /(float f, Vector3f v) => new Vector3f(f / v.x, f / v.y, f / v.z);
        public static bool operator ==(Vector3f a, Vector3f b) => a.x == b.x && a.y == b.y && a.z == b.z;
        public static bool operator !=(Vector3f a, Vector3f b) => a.x != b.x || a.y != b.y || a.z != b.z;
        public override bool Equals(object other) => other is Vector3f && Equals((Vector3f)other);
        public bool Equals(Vector3f other) => this == other;
        public override int GetHashCode() => (int)(x + y + z);

        public static implicit operator TK_Vec3(Vector3f v) => *(TK_Vec3*)&v;
        public static implicit operator Vector3f(TK_Vec3 v) => *(Vector3f*)&v;
        public static implicit operator Vector3f(float f) => new Vector3f(f, f, f);

        public float Dot(Vector3f other) => Dot(this, other);
        public Vector3f GetNormalized() => Normalize(this);

        public void Add(Vector3f other) => this += other;
        public void Subtract(Vector3f other) => this -= other;
        public void Multiply(float factor) => this *= factor;
        public void Divide(float divisor) => this /= divisor;

        public Vector3f Sum(Vector3f a) => this + a;
        public Vector3f Difference(Vector3f a) => this - a;
        public Vector3f Product(float f) => this * f;
        public Vector3f Quotient(float f) => this / f;

        public bool LessEquals(float compare) => x <= compare && y <= compare && z <= compare;
        public bool LessEquals(Vector3f compare) => x <= compare.x && y <= compare.y && z <= compare.z;
        public bool Less(float compare) => x < compare && y < compare && z < compare;
        public bool Less(Vector3f compare) => x < compare.x && y < compare.y && z < compare.z;
        public bool GreaterEquals(float compare) => x >= compare && y >= compare && z >= compare;
        public bool GreaterEquals(Vector3f compare) => x >= compare.x && y >= compare.y && z >= compare.z;
        public bool Greater(float compare) => x > compare && y > compare && z > compare;
        public bool Greater(Vector3f compare) => x > compare.x && y > compare.y && z > compare.z;

        public Vector3f Min(float compare) => Clamping.Min(this, compare);
        public Vector3f Min(Vector3f compare) => Clamping.Min(this, compare);
        public Vector3f Max(float compare) => Clamping.Max(this, compare);
        public Vector3f Max(Vector3f compare) => Clamping.Max(this, compare);
    }
}
