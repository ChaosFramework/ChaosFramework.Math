using ChaosFramework.Collections.Immutable;
using ChaosFramework.IO;
using ChaosFramework.Math.Vectors.Definitions;
using ChaosFramework.Math.Vectors.Operations;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static ChaosFramework.Math.Exponentials;

namespace ChaosFramework.Math.Vectors
{
    [DebuggerDisplay("x={x}; y={y}; z={z}; w={w}")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector4i
        : Vector<Vector4i, int>
        , Vector4<Vector4i, int, Vector3i, Vector2i>
        , Addition<Vector4i, int>
        , Subtraction<Vector4i, int>
        , Multiplication<Vector4i, int, int>
        , Multiplication<Vector4i, int, float, Vector4f>
        , Division<Vector4i, int, int>
        , Division<Vector4i, int, float, Vector4f>
        , DotProduct<Vector4i, int>
        , EuclideanLength<Vector4i, int, float>
        , EuclideanNormalization<Vector4i, int, Vector4f, float>
        , ChaosUtil.Primitives.Convertible<Vector4f>
        , ComponentwiseAllComparison<Vector4i, int>
        , ComponentwiseAllComparison<Vector4i, int, int>
        , Clamping<Vector4i, int, int>
        , Clamping<Vector4i, int, Vector4i>
    {
        [ChaosIO.RegisterType]
        static void RegisterType() => ChaosIO.AddType(Read, Write);

        public static Vector4i Read(System.IO.BinaryReader reader)
            => new Vector4i(reader.Read<int>(), reader.Read<int>(), reader.Read<int>(), reader.Read<int>());

        public static void Write(System.IO.BinaryWriter writer, Vector4i v)
        {
            writer.Write(v.x);
            writer.Write(v.y);
            writer.Write(v.z);
            writer.Write(v.w);
        }

        /// <summary> Contains all carthesian coordinate vectors with both negative and positive sign. </summary>
        public static readonly ImmutableArray<Vector4i> carthesianDirection = new Vector4i[] {
            new Vector4i(-1,  0,  0,  0),
            new Vector4i( 1,  0,  0,  0),
            new Vector4i( 0, -1,  0,  0),
            new Vector4i( 0,  1,  0,  0),
            new Vector4i( 0,  0, -1,  0),
            new Vector4i( 0,  0,  1,  0),
            new Vector4i( 0,  0,  0, -1),
            new Vector4i( 0,  0,  0,  1),
        };

        /// <summary> Returns an empty vector. (All coordinates are set to zero.) </summary>
        public static readonly Vector4i EMPTY = new Vector4i();

        /// <summary> Returns a vector with all coordinates set to <see cref="int.MinValue"/>. </summary>
        public static readonly Vector4i MIN_VALUE = new Vector4i(int.MinValue);

        /// <summary> Returns a vector with all coordinates set to <see cref="int.MaxValue"/>. </summary>
        public static readonly Vector4i MAX_VALUE = new Vector4i(int.MaxValue);

        public int x, y, z, w;

        int Vector2<Vector2i, int>.x { get { return x; } set { x = value; } }
        int Vector2<Vector2i, int>.y { get { return y; } set { y = value; } }
        int Vector3<Vector3i, int, Vector2i>.z { get { return z; } set { z = value; } }
        int Vector4<Vector4i, int, Vector3i, Vector2i>.w { get { return w; } set { w = value; } }

        public Vector2i xy { get { return new Vector2i(x, y); } set { x = value.x; y = value.y; } }
        public Vector2i xz { get { return new Vector2i(x, z); } set { x = value.x; z = value.y; } }
        public Vector2i xw { get { return new Vector2i(x, w); } set { x = value.x; w = value.y; } }
        public Vector2i yz { get { return new Vector2i(y, z); } set { y = value.x; z = value.y; } }
        public Vector2i yw { get { return new Vector2i(y, w); } set { y = value.x; w = value.y; } }
        public Vector2i zw { get { return new Vector2i(z, w); } set { z = value.x; w = value.y; } }
        public Vector3i xyz { get { return new Vector3i(x, y, z); } set { x = value.x; y = value.y; z = value.z; } }
        public Vector3i yzw { get { return new Vector3i(y, z, w); } set { y = value.x; z = value.y; w = value.z; } }

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
                    case 3: return w;
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
                    case 3: w = value; break;
                    default: throw new System.IndexOutOfRangeException();
                }
            }
        }

        public Vector4i(int v) { x = y = z = w = v; }
        public Vector4i(int x, int y, int z, int w) { this.x = x; this.y = y; this.z = z; this.w = w; }
        public Vector4i(Vector2i xy, int z, int w) { x = xy.x; y = xy.y; this.z = z; this.w = w; }
        public Vector4i(int x, Vector2i yz, int w) { this.x = x; y = yz.x; z = yz.y; this.w = w; }
        public Vector4i(int x, int y, Vector2i zw) { this.x = x; this.y = y; z = zw.x; w = zw.y; }
        public Vector4i(Vector2i xy, Vector2i zw) { x = xy.x; y = xy.y; z = zw.x; w = zw.y; }
        public Vector4i(Vector3i xyz, int w) { x = xyz.x; y = xyz.y; z = xyz.z; this.w = w; }
        public Vector4i(int x, Vector3i yzw) { this.x = x; y = yzw.x; z = yzw.y; w = yzw.z; }

        public Vector4i(float x, float y, float z, float w) { this.x = (int)x; this.y = (int)y; this.z = (int)z; this.w = (int)w; }
        public Vector4i(Vector2f xy, float z, float w) { x = (int)xy.x; y = (int)xy.y; this.z = (int)z; this.w = (int)w; }
        public Vector4i(float x, Vector2f yz, float w) { this.x = (int)x; y = (int)yz.x; z = (int)yz.y; this.w = (int)w; }
        public Vector4i(float x, float y, Vector2f zw) { this.x = (int)x; this.y = (int)y; z = (int)zw.x; w = (int)zw.y; }
        public Vector4i(Vector2f xy, Vector2f zw) { x = (int)xy.x; y = (int)xy.y; z = (int)zw.x; w = (int)zw.y; }
        public Vector4i(Vector3f xyz, float w) { x = (int)xyz.x; y = (int)xyz.y; z = (int)xyz.z; this.w = (int)w; }
        public Vector4i(float x, Vector3f yzw) { this.x = (int)x; y = (int)yzw.x; z = (int)yzw.y; w = (int)yzw.z; }
        public Vector4i(Vector4f xyzw) { x = (int)xyzw.x; y = (int)xyzw.y; z = (int)xyzw.z; w = (int)xyzw.w; }

        public static Vector4i operator -(Vector4i v) => new Vector4i(-v.x, -v.y, -v.z, -v.w);
        public static Vector4i operator +(Vector4i v1, Vector4i v2) => new Vector4i(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z, v1.w + v2.w);
        public static Vector4i operator -(Vector4i v1, Vector4i v2) => new Vector4i(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z, v1.w - v2.w);
        public static Vector4i operator *(Vector4i v, int i) => new Vector4i(i * v.x, i * v.y, i * v.z, i * v.w);
        public static Vector4i operator *(int i, Vector4i v) => v * i;
        public static Vector4f operator *(Vector4i v, float f) => new Vector4f(f * v.x, f * v.y, f * v.z, f * v.w);
        public static Vector4f operator *(float f, Vector4i v) => v * f;
        public static Vector4i operator /(Vector4i v, int i) => new Vector4i(v.x / i, v.y / i, v.z / i, v.w / i);
        public static Vector4i operator /(int i, Vector4i v) => new Vector4i(i / v.x, i / v.y, i / v.z, i / v.w);
        public static Vector4f operator /(Vector4i v, float f) => new Vector4f(v.x / f, v.y / f, v.z / f, v.w / f);
        public static Vector4f operator /(float f, Vector4i v) => new Vector4f(f / v.x, f / v.y, f / v.z, f / v.w);
        public static bool operator ==(Vector4i v1, Vector4i v2) => v1.x == v2.x && v1.y == v2.y && v1.z == v2.z && v1.w == v2.w;
        public static bool operator !=(Vector4i v1, Vector4i v2) => v1.x != v2.x || v1.y != v2.y || v1.z != v2.z || v1.w != v2.w;
        public override bool Equals(object other) => other is Vector4i && Equals((Vector4i)other);
        public bool Equals(Vector4i other) => this == other;
        public override int GetHashCode() => x ^ y ^ z ^ w;

        public static implicit operator Vector4i(int i) => new Vector4i(i, i, i, i);
        public static implicit operator Vector4f(Vector4i obj) => new Vector4f(obj.x, obj.y, obj.z, obj.w);
        public static explicit operator Vector4i(Vector4f obj) => new Vector4i(obj);

        public int LengthSq() => Dot(this);
        public float Length() => Sqrt(LengthSq());
        public int Dot(Vector4i other) => x * other.x + y * other.y + z * other.z + w * other.w;
        public Vector4f GetNormalized() => this / Length();

        public void Add(Vector4i other) => this += other;
        public void Subtract(Vector4i other) => this -= other;
        public void Multiply(int factor) => this *= factor;
        public void Divide(int divisor) => this /= divisor;

        public Vector4i Sum(Vector4i other) => this + other;
        public Vector4i Difference(Vector4i other) => this - other;
        public Vector4i Product(int factor) => this * factor;
        public Vector4f Product(float factor) => this * factor;
        public Vector4i Quotient(int divisor) => this / divisor;
        public Vector4f Quotient(float divisor) => this / divisor;

        Vector4f ChaosUtil.Primitives.Convertible<Vector4f>.Convert() => this;

        public bool LessEquals(int compare) => x <= compare && y <= compare && z <= compare && w <= compare;
        public bool LessEquals(Vector4i compare) => x <= compare.x && y <= compare.y && z <= compare.z && w <= compare.w;
        public bool Less(int compare) => x < compare && y < compare && z < compare && w < compare;
        public bool Less(Vector4i compare) => x < compare.x && y < compare.y && z < compare.z && w < compare.w;
        public bool GreaterEquals(int compare) => x >= compare && y >= compare && z >= compare && w >= compare;
        public bool GreaterEquals(Vector4i compare) => x >= compare.x && y >= compare.y && z >= compare.z && w >= compare.w;
        public bool Greater(int compare) => x > compare && y > compare && z > compare && w > compare;
        public bool Greater(Vector4i compare) => x > compare.x && y > compare.y && z > compare.z && w > compare.w;

        public Vector4i Min(int compare) => Clamping.Min(this, compare);
        public Vector4i Min(Vector4i compare) => Clamping.Min(this, compare);
        public Vector4i Max(int compare) => Clamping.Max(this, compare);
        public Vector4i Max(Vector4i compare) => Clamping.Max(this, compare);
    }
}
