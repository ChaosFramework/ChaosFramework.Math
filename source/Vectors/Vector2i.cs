using ChaosFramework.Collections.Immutable;
using ChaosFramework.IO;
using ChaosFramework.Math.Vectors.Definitions;
using ChaosFramework.Math.Vectors.Operations;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static ChaosFramework.Math.Exponentials;

namespace ChaosFramework.Math.Vectors
{
    [DebuggerDisplay("x={x}; y={y}")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2i
        : Vector<Vector2i, int>
        , Vector2<Vector2i, int>
        , Addition<Vector2i, int>
        , Subtraction<Vector2i, int>
        , Multiplication<Vector2i, int, int>
        , Multiplication<Vector2i, int, float, Vector2f>
        , Division<Vector2i, int, int>
        , Division<Vector2i, int, float, Vector2f>
        , DotProduct<Vector2i, int>
        , EuclideanLength<Vector2i, int, float>
        , EuclideanNormalization<Vector2i, int, Vector2f, float>
        , ChaosUtil.Primitives.Convertible<Vector2f>
        , ComponentwiseAllComparison<Vector2i, int>
        , ComponentwiseAllComparison<Vector2i, int, int>
        , Clamping<Vector2i, int, int>
        , Clamping<Vector2i, int, Vector2i>
    {
        [ChaosIO.RegisterType]
        static void RegisterType() => ChaosIO.AddType(Read, Write);

        public static Vector2i Read(System.IO.BinaryReader reader)
            => new Vector2i(reader.Read<int>(), reader.Read<int>());

        public static void Write(System.IO.BinaryWriter writer, Vector2i v)
        {
            writer.Write(v.x);
            writer.Write(v.y);
        }

        /// <summary> Contains all carthesian coordinate vectors with both negative and positive sign. </summary>
        public static readonly ImmutableArray<Vector2i> carthesianDirection = new Vector2i[] {
            new Vector2i(-1,  0),
            new Vector2i( 1,  0),
            new Vector2i( 0, -1),
            new Vector2i( 0,  1)
        };

        /// <summary> Returns an empty vector. (All coordinates are set to zero.) </summary>
        public static readonly Vector2i EMPTY = new Vector2i();

        /// <summary> Returns a vector with all coordinates set to <see cref="int.MinValue"/>. </summary>
        public static readonly Vector2i MIN_VALUE = new Vector2i(int.MinValue);

        /// <summary> Returns a vector with all coordinates set to <see cref="int.MaxValue"/>. </summary>
        public static readonly Vector2i MAX_VALUE = new Vector2i(int.MaxValue);

        public int x, y;

        int Vector2<Vector2i, int>.x { get { return x; } set { x = value; } }
        int Vector2<Vector2i, int>.y { get { return y; } set { y = value; } }

        public int this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return x;
                    case 1: return y;
                    default: throw new System.IndexOutOfRangeException();
                }
            }
            set
            {
                switch (i)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    default: throw new System.IndexOutOfRangeException();
                }
            }
        }

        public Vector2i(int v) { x = y = v; }
        public Vector2i(int x, int y) { this.x = x; this.y = y; }
        public Vector2i(Vector2f v) { x = (int)v.x; y = (int)v.y; }
        public Vector2i(float x, float y) { this.x = (int)x; this.y = (int)y; }

        public static Vector2i operator -(Vector2i v) => new Vector2i(-v.x, -v.y);
        public static Vector2i operator +(Vector2i v1, Vector2i v2) => new Vector2i(v1.x + v2.x, v1.y + v2.y);
        public static Vector2i operator -(Vector2i v1, Vector2i v2) => new Vector2i(v1.x - v2.x, v1.y - v2.y);
        public static Vector2i operator *(Vector2i v, int i) => new Vector2i(i * v.x, i * v.y);
        public static Vector2i operator *(int i, Vector2i v) => v * i;
        public static Vector2f operator *(Vector2i v, float f) => new Vector2f(v.x * f, v.y * f);
        public static Vector2f operator *(float f, Vector2i v) => v * f;
        public static Vector2i operator /(Vector2i v, int i) => new Vector2i(v.x / i, v.y / i);
        public static Vector2i operator /(int i, Vector2i v) => new Vector2i(i / v.x, i / v.y);
        public static Vector2f operator /(Vector2i v, float f) => new Vector2f(v.x / f, v.y / f);
        public static Vector2f operator /(float f, Vector2i v) => new Vector2f(f / v.x, f / v.y);
        public static bool operator ==(Vector2i v1, Vector2i v2) => v1.x == v2.x && v1.y == v2.y;
        public static bool operator !=(Vector2i v1, Vector2i v2) => v1.x != v2.x || v1.y != v2.y;
        public override bool Equals(object other) => other is Vector2i && Equals((Vector2i)other);
        public bool Equals(Vector2i other) => this == other;
        public override int GetHashCode() => x ^ y;

        public static implicit operator Vector2i(int i) => new Vector2i(i, i);
        public static implicit operator Vector2f(Vector2i obj) => new Vector2f(obj.x, obj.y);
        public static explicit operator Vector2i(Vector2f obj) => new Vector2i(obj);

        public static implicit operator System.Drawing.Size(Vector2i v) => new System.Drawing.Size(v.x, v.y);
        public static implicit operator System.Drawing.Point(Vector2i v) => new System.Drawing.Point(v.x, v.y);
        public static implicit operator Vector2i(System.Drawing.Size v) => new Vector2i(v.Width, v.Height);
        public static implicit operator Vector2i(System.Drawing.Point v) => new Vector2i(v.X, v.Y);

        public override string ToString() => "{ " + x.ToString() + " ; " + y.ToString() + " }";

        public int LengthSq() => Dot(this);
        public float Length() => Sqrt(LengthSq());
        public int Dot(Vector2i other) => x * other.x + y * other.y;
        public Vector2f GetNormalized() => this / Length();

        public void Add(Vector2i other) => this += other;
        public void Subtract(Vector2i other) => this -= other;
        public void Multiply(int factor) => this *= factor;
        public void Divide(int divisor) => this /= divisor;

        public Vector2i Sum(Vector2i other) => this + other;
        public Vector2i Difference(Vector2i other) => this - other;
        public Vector2i Product(int factor) => this * factor;
        public Vector2f Product(float factor) => this * factor;
        public Vector2i Quotient(int divisor) => this / divisor;
        public Vector2f Quotient(float divisor) => this / divisor;

        Vector2f ChaosUtil.Primitives.Convertible<Vector2f>.Convert() => this;

        public bool LessEquals(int compare) => x <= compare && y <= compare;
        public bool LessEquals(Vector2i compare) => x <= compare.x && y <= compare.y;
        public bool Less(int compare) => x < compare && y < compare;
        public bool Less(Vector2i compare) => x < compare.x && y < compare.y;
        public bool GreaterEquals(int compare) => x >= compare && y >= compare;
        public bool GreaterEquals(Vector2i compare) => x >= compare.x && y >= compare.y;
        public bool Greater(int compare) => x > compare && y > compare;
        public bool Greater(Vector2i compare) => x > compare.x && y > compare.y;

        public Vector2i Min(int compare) => Clamping.Min(this, compare);
        public Vector2i Min(Vector2i compare) => Clamping.Min(this, compare);
        public Vector2i Max(int compare) => Clamping.Max(this, compare);
        public Vector2i Max(Vector2i compare) => Clamping.Max(this, compare);
    }
}
