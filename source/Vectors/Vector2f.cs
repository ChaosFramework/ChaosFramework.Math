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
using TK_Vec2 = OpenTK.Mathematics.Vector2;

namespace ChaosFramework.Math.Vectors
{
    [DebuggerDisplay("x={x}; y={y}")]
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Vector2f
        : Vector<Vector2f, float>
        , Vector2<Vector2f, float>
        , PotentiallyNaNVector
        , PotentiallyInfiniteVector
        , Addition<Vector2f, float>
        , Subtraction<Vector2f, float>
        , Multiplication<Vector2f, float, float>
        , Division<Vector2f, float, float>
        , DotProduct<Vector2f, float>
        , EuclideanLength<Vector2f, float>
        , EuclideanNormalization<Vector2f, float>
        , ComponentwiseAllComparison<Vector2f, float>
        , ComponentwiseAllComparison<Vector2f, float, float>
        , Clamping<Vector2f, float, float>
        , Clamping<Vector2f, float, Vector2f>
    {
        [ChaosIO.RegisterType]
        static void RegisterIO() => ChaosIO.AddType(Read, Write);

        internal static Vector2f Read(System.IO.BinaryReader reader)
            => new Vector2f(reader.Read<float>(), reader.Read<float>());

        internal static void Write(System.IO.BinaryWriter writer, Vector2f value)
        {
            writer.WriteAs(value.x);
            writer.WriteAs(value.y);
        }

        public static bool TryParse(string str, out Vector2f v)
            => TryParse(str, Constants.VECTOR_COMPONENT_SEPARATOR, out v);

        public static bool TryParse(string str, char[] splitCharacters, out Vector2f v)
        {
            v = EMPTY;
            string[] values = str.Split(splitCharacters);
            if (values.Length == 1)
            {
                if (Parse.TryParse(values[0], out v.x))
                {
                    v.y = v.x;
                    return true;
                }
            }
            else if (values.Length == 2)
                if (Parse.TryParse(values[0], out v.x))
                    if (Parse.TryParse(values[1], out v.y))
                        return true;
            v = EMPTY;
            return false;
        }

        static Vector2f()
        {
            Parse.AddParser<Vector2f>(TryParse);
        }

        /// <summary> Contains all carthesian coordinate vectors with both negative and positive sign. </summary>
        public static readonly ImmutableArray<Vector2f> carthesianDirection = new Vector2f[] {
            new Vector2f(-1,  0),
            new Vector2f( 1,  0),
            new Vector2f( 0, -1),
            new Vector2f( 0,  1)
        };

        /// <summary> Returns an empty vector. (All coordinates are set to zero.) </summary>
        public static readonly Vector2f EMPTY = new Vector2f();

        /// <summary> Returns a vector with all coordinates set to <see cref="float.NaN"/>. </summary>
        public static readonly Vector2f NAN = new Vector2f(float.NaN, float.NaN);

        /// <summary> Returns a vector with all coordinates set to <see cref="float.MinValue"/>. </summary>
        public static readonly Vector2f MIN_VALUE = new Vector2f(float.MinValue, float.MinValue);

        /// <summary> Returns a vector with all coordinates set to <see cref="float.MaxValue"/>. </summary>
        public static readonly Vector2f MAX_VALUE = new Vector2f(float.MaxValue, float.MaxValue);

        /// <summary> Return a vector with all coordinates set to <see cref="float.PositiveInfinity"/>. </summary>
        public static readonly Vector2f POSITIVE_INFINITY = new Vector2f(float.PositiveInfinity);

        /// <summary> Return a vector with all coordinates set to <see cref="float.NegativeInfinity"/>. </summary>
        public static readonly Vector2f NEGATIVE_INFINITY = new Vector2f(float.NegativeInfinity);

        public float x, y;

        float Vector2<Vector2f, float>.x { get { return x; } set { x = value; } }
        float Vector2<Vector2f, float>.y { get { return y; } set { y = value; } }

        public float this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return x;
                    case 1: return y;
                    default: throw new IndexOutOfRangeException();
                }
            }
            set
            {
                switch (i)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        public Vector2f(float v) { x = y = v; }
        public Vector2f(float x, float y) { this.x = x; this.y = y; }
        public Vector2f(Vector2f v) { x = v.x; y = v.y; }

        public float LengthSq() => x * x + y * y;
        public float Length() => Sqrt(LengthSq());
        public bool IsNaN() => float.IsNaN(x) || float.IsNaN(y);
        public bool IsInfinite() => float.IsInfinity(x) || float.IsInfinity(y);
        public bool IsNormalized(float threshold = 1e-4f) => Abs(Length() - 1) < threshold;

        public void Normalize()
        {
            float len = Length();
            if (len == 0)
                x = y = 0;
            else
            {
                x /= len;
                y /= len;
            }
        }
        public void Normalize(float newLength = 1) { Normalize(); x *= newLength; y *= newLength; }
        public static float Dot(Vector2f a, Vector2f b) => a.x * b.x + a.y * b.y;
        public static Vector2f Normalize(Vector2f v) { v.Normalize(); return v; }
        public static Vector2f Normalize(Vector2f v, float newLength) { v.Normalize(newLength); return v; }

        public static Vector2f TransformNormal(Vector2f v, Matrix m) => Vector4f.Transform(new Vector4f(v, 0, 0), m).xy;
        public static Vector2f TransformCoordinate(Vector2f v, Matrix m)
        {
            Vector4f vec = Vector4f.Transform(new Vector4f(v, 0, 1), m);
            return vec.xy / vec.w;
        }
        public static Vector2f[] TransformNormal(Vector2f[] v, Matrix m) => Array.ConvertAll(v, _ => TransformNormal(_, m));
        public static Vector2f[] TransformCoordinate(Vector2f[] v, Matrix m) => Array.ConvertAll(v, _ => TransformCoordinate(_, m));

        /// <summary> Returns a new <see cref="Vector2f"/> consisting of the componentwise products of all given vectors. </summary>
        public static Vector2f ComponentWiseMul(Vector2f v0, Vector2f v1) => new Vector2f(v0.x * v1.x, v0.y * v1.y);

        /// <summary> Returns a new <see cref="Vector2f"/> consisting of the componentwise products of all given vectors. </summary>
        public static Vector2f ComponentWiseMul(Vector2f v0, params Vector2f[] v)
        {
            foreach (Vector2f a in v)
                v0 = new Vector2f(v0.x * a.x, v0.y * a.y);
            return v0;
        }

        public Vector2i SignBits() => new Vector2i(SignBit(x), SignBit(y));

        public static Vector2f operator -(Vector2f v) => new Vector2f(-v.x, -v.y);
        public static Vector2f operator +(Vector2f a, Vector2f b) => new Vector2f(a.x + b.x, a.y + b.y);
        public static Vector2f operator -(Vector2f a, Vector2f b) => new Vector2f(a.x - b.x, a.y - b.y);
        public static Vector2f operator *(Vector2f v, float f) => new Vector2f(v.x * f, v.y * f);
        public static Vector2f operator *(float f, Vector2f v) => new Vector2f(v.x * f, v.y * f);
        public static Vector2f operator /(Vector2f v, float f) => new Vector2f(v.x / f, v.y / f);
        public static Vector2f operator /(float f, Vector2f v) => new Vector2f(f / v.x, f / v.y);
        public static bool operator ==(Vector2f a, Vector2f b) => a.x == b.x && a.y == b.y;
        public static bool operator !=(Vector2f a, Vector2f b) => a.x != b.x || a.y != b.y;

        public static implicit operator TK_Vec2(Vector2f v) => *(TK_Vec2*)&v;
        public static implicit operator Vector2f(TK_Vec2 v) => *(Vector2f*)&v;
        public static implicit operator Vector2f(float f) { return new Vector2f(f, f); }

        public override bool Equals(object other) => other is Vector2f && Equals((Vector2f)other);
        public bool Equals(Vector2f other) => this == other;
        public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode();

        public float Dot(Vector2f other) => Dot(this, other);
        public Vector2f GetNormalized() => Normalize(this);

        public void Add(Vector2f other) => this += other;
        public void Subtract(Vector2f other) => this -= other;
        public void Multiply(float factor) => this *= factor;
        public void Divide(float divisor) => this /= divisor;

        public Vector2f Sum(Vector2f a) => this + a;
        public Vector2f Difference(Vector2f a) => this - a;
        public Vector2f Product(float f) => this * f;
        public Vector2f Quotient(float f) => this / f;

        public bool LessEquals(float compare) => x <= compare && y <= compare;
        public bool LessEquals(Vector2f compare) => x <= compare.x && y <= compare.y;
        public bool Less(float compare) => x < compare && y < compare;
        public bool Less(Vector2f compare) => x < compare.x && y < compare.y;
        public bool GreaterEquals(float compare) => x >= compare && y >= compare;
        public bool GreaterEquals(Vector2f compare) => x >= compare.x && y >= compare.y;
        public bool Greater(float compare) => x > compare && y > compare;
        public bool Greater(Vector2f compare) => x > compare.x && y > compare.y;

        public Vector2f Min(float compare) => Clamping.Min(this, compare);
        public Vector2f Min(Vector2f compare) => Clamping.Min(this, compare);
        public Vector2f Max(float compare) => Clamping.Max(this, compare);
        public Vector2f Max(Vector2f compare) => Clamping.Max(this, compare);
    }
}
