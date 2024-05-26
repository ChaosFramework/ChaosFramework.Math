using ChaosFramework.Collections.Immutable;
using ChaosFramework.IO;
using ChaosFramework.Math.Vectors.Definitions;
using ChaosFramework.Math.Vectors.Operations;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static ChaosFramework.Math.Exponentials;

namespace ChaosFramework.Math.Vectors
{
    [DebuggerDisplay("x={x}; y={y}; z={z}")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3i
        : Vector<Vector3i, int>
        , Vector3<Vector3i, int, Vector2i>
        , Addition<Vector3i, int>
        , Subtraction<Vector3i, int>
        , Multiplication<Vector3i, int, int>
        , Multiplication<Vector3i, int, float, Vector3f>
        , Division<Vector3i, int, int>
        , Division<Vector3i, int, float, Vector3f>
        , DotProduct<Vector3i, int>
        , EuclideanLength<Vector3i, int, float>
        , EuclideanNormalization<Vector3i, int, Vector3f, float>
        , ChaosUtil.Primitives.Convertible<Vector3f>
        , ComponentwiseAllComparison<Vector3i, int>
        , ComponentwiseAllComparison<Vector3i, int, int>
        , Clamping<Vector3i, int, int>
        , Clamping<Vector3i, int, Vector3i>
    {
        [ChaosIO.RegisterType]
        static void RegisterType() => ChaosIO.AddType(Read, Write);

        public static Vector3i Read(System.IO.BinaryReader reader)
            => new Vector3i(reader.Read<int>(), reader.Read<int>(), reader.Read<int>());

        public static void Write(System.IO.BinaryWriter writer, Vector3i v)
        {
            writer.Write(v.x);
            writer.Write(v.y);
            writer.Write(v.z);
        }

        /// <summary> Contains all carthesian coordinate vectors with both negative and positive sign. </summary>
        public static readonly ImmutableArray<Vector3i> carthesianDirection = new Vector3i[] {
            new Vector3i(-1,  0,  0),
            new Vector3i( 1,  0,  0),
            new Vector3i( 0, -1,  0),
            new Vector3i( 0,  1,  0),
            new Vector3i( 0,  0, -1),
            new Vector3i( 0,  0,  1),
        };

        /// <summary> Returns an empty vector. (All coordinates are set to zero.) </summary>
        public static readonly Vector3i EMPTY = new Vector3i();

        /// <summary> Returns a vector with all coordinates set to <see cref="int.MinValue"/>. </summary>
        public static readonly Vector3i MIN_VALUE = new Vector3i(int.MinValue);

        /// <summary> Returns a vector with all coordinates set to <see cref="int.MaxValue"/>. </summary>
        public static readonly Vector3i MAX_VALUE = new Vector3i(int.MaxValue);

        public int x, y, z;

        int Vector2<Vector2i, int>.x { get { return x; } set { x = value; } }
        int Vector2<Vector2i, int>.y { get { return y; } set { y = value; } }
        int Vector3<Vector3i, int, Vector2i>.z { get { return z; } set { z = value; } }

        public Vector2i xy { get { return new Vector2i(x, y); } set { x = value.x; y = value.y; } }
        public Vector2i yz { get { return new Vector2i(y, z); } set { y = value.x; z = value.y; } }
        public Vector2i xz { get { return new Vector2i(x, z); } set { x = value.x; z = value.y; } }

        public Vector3i x0z => new Vector3i(x, 0, z);
        public Vector3i xy0 => new Vector3i(x, y, 0);

        public int this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                    default: throw new System.IndexOutOfRangeException();
                }
            }
            set
            {
                switch (i)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    case 2: z = value; break;
                    default: throw new System.IndexOutOfRangeException();
                }

            }
        }

        public Vector3i(int x, int y, int z) { this.x = x; this.y = y; this.z = z; }
        public Vector3i(Vector2i xy, int z) { x = xy.x; y = xy.y; this.z = z; }
        public Vector3i(int x, Vector2i yz) { this.x = x; y = yz.x; z = yz.y; }

        public Vector3i(int v) { x = y = z = v; }
        public Vector3i(Vector3f v) { x = (int)v.x; y = (int)v.y; z = (int)v.z; }
        public Vector3i(Vector2f xy, float z) { x = (int)xy.x; y = (int)xy.y; this.z = (int)z; }
        public Vector3i(float x, Vector2f yz) { this.x = (int)x; y = (int)yz.x; z = (int)yz.y; }

        public static Vector3i operator -(Vector3i v) => new Vector3i(-v.x, -v.y, -v.z);
        public static Vector3i operator +(Vector3i v1, Vector3i v2) => new Vector3i(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        public static Vector3i operator -(Vector3i v1, Vector3i v2) => new Vector3i(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        public static Vector3i operator *(Vector3i v, int i) => new Vector3i(i * v.x, i * v.y, i * v.z);
        public static Vector3i operator *(int i, Vector3i v) => v * i;
        public static Vector3f operator *(Vector3i v, float f) => new Vector3f(f * v.x, f * v.y, f * v.z);
        public static Vector3f operator *(float f, Vector3i v) => v * f;
        public static Vector3i operator /(Vector3i v, int i) => new Vector3i(v.x / i, v.y / i, v.z / i);
        public static Vector3i operator /(int i, Vector3i v) => new Vector3i(i / v.x, i / v.y, i / v.z);
        public static Vector3f operator /(Vector3i v, float f) => new Vector3f(v.x / f, v.y / f, v.z / f);
        public static Vector3f operator /(float f, Vector3i v) => new Vector3f(f / v.x, f / v.y, f / v.z);
        public static bool operator ==(Vector3i v1, Vector3i v2) => v1.x == v2.x && v1.y == v2.y && v1.z == v2.z;
        public static bool operator !=(Vector3i v1, Vector3i v2) => v1.x != v2.x || v1.y != v2.y || v1.z != v2.z;
        public override bool Equals(object other) => other is Vector3i && Equals((Vector3i)other);
        public bool Equals(Vector3i other) => this == other;
        public override int GetHashCode() => x ^ y ^ z;

        public static implicit operator Vector3i(int i) => new Vector3i(i, i, i);
        public static implicit operator Vector3f(Vector3i obj) => new Vector3f(obj.x, obj.y, obj.z);
        public static explicit operator Vector3i(Vector3f obj) => new Vector3i(obj);

        public int LengthSq() => Dot(this);
        public float Length() => Sqrt(LengthSq());
        public int Dot(Vector3i other) => x * other.x + y * other.y + z * other.z;
        public Vector3f GetNormalized() => this / Length();

        public void Add(Vector3i other) => this += other;
        public void Subtract(Vector3i other) => this -= other;
        public void Multiply(int factor) => this *= factor;
        public void Divide(int divisor) => this /= divisor;

        public Vector3i Sum(Vector3i other) => this + other;
        public Vector3i Difference(Vector3i other) => this - other;
        public Vector3i Product(int factor) => this * factor;
        public Vector3f Product(float factor) => this * factor;
        public Vector3i Quotient(int divisor) => this / divisor;
        public Vector3f Quotient(float divisor) => this / divisor;

        Vector3f ChaosUtil.Primitives.Convertible<Vector3f>.Convert() => this;

        public bool LessEquals(int compare) => x <= compare && y <= compare && z <= compare;
        public bool LessEquals(Vector3i compare) => x <= compare.x && y <= compare.y && z <= compare.z;
        public bool Less(int compare) => x < compare && y < compare && z < compare;
        public bool Less(Vector3i compare) => x < compare.x && y < compare.y && z < compare.z;
        public bool GreaterEquals(int compare) => x >= compare && y >= compare && z >= compare;
        public bool GreaterEquals(Vector3i compare) => x >= compare.x && y >= compare.y && z >= compare.z;
        public bool Greater(int compare) => x > compare && y > compare && z > compare;
        public bool Greater(Vector3i compare) => x > compare.x && y > compare.y && z > compare.z;

        public Vector3i Min(int compare) => Clamping.Min(this, compare);
        public Vector3i Min(Vector3i compare) => Clamping.Min(this, compare);
        public Vector3i Max(int compare) => Clamping.Max(this, compare);
        public Vector3i Max(Vector3i compare) => Clamping.Max(this, compare);
    }
}
