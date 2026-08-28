using System.Diagnostics;
using System.Runtime.InteropServices;
using ChaosFramework.Collections.Immutable;
using ChaosFramework.IO;
using ChaosFramework.Math.Vectors.Definitions;
using ChaosFramework.Math.Vectors.Operations;
using ChaosUtil.Primitives;
using static ChaosFramework.Math.Exponentials;

namespace ChaosFramework.Math.Vectors
{
    [DebuggerDisplay("x={x}; y={y}")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2ui
        : Vector<Vector2ui, uint>
        , Vector2<Vector2ui, uint>
        , Addition<Vector2ui, uint>
        , Addition<Vector2ui, uint, uint, Vector2ui>
        , Subtraction<Vector2ui, uint, Vector2ui, Vector2i>
        , Subtraction<Vector2ui, uint, uint, Vector2i>
        , Multiplication<Vector2ui, uint, uint>
        , Multiplication<Vector2ui, uint, float, Vector2f>
        , Division<Vector2ui, uint, uint>
        , Division<Vector2ui, uint, float, Vector2f>
        , DotProduct<Vector2ui, uint>
        , EuclideanLength<Vector2ui, uint, float>
        , EuclideanNormalization<Vector2ui, uint, Vector2f, float>
        , Convertible<Vector2f>
        , ComponentwiseAllComparison<Vector2ui, uint>
        , ComponentwiseAllComparison<Vector2ui, uint, uint>
        , Clamping<Vector2ui, uint, uint>
        , Clamping<Vector2ui, uint, Vector2ui>
    {
        [ChaosIO.RegisterType]
        static void RegisterType() => ChaosIO.AddType(Read, Write);

        public static Vector2ui Read(System.IO.BinaryReader reader)
            => new Vector2ui(reader.ReadUInt32(), reader.ReadUInt32());

        public static void Write(System.IO.BinaryWriter writer, Vector2ui v)
        {
            writer.Write(v.x);
            writer.Write(v.y);
        }

        /// <summary> Contains all carthesian coordinate vectors. </summary>
        public static readonly ImmutableArray<Vector2ui> carthesianDirection = new Vector2ui[] {
            new Vector2ui(1, 0),
            new Vector2ui(0, 1)
        };

        /// <summary> Returns an empty vector. (All coordinates are set to zero.) </summary>
        public static readonly Vector2ui EMPTY = new Vector2ui();

        /// <summary> Returns a vector with all coordinates set to <see cref="uint.MinValue"/>. </summary>
        public static readonly Vector2ui MIN_VALUE = new Vector2ui(uint.MinValue);

        /// <summary> Returns a vector with all coordinates set to <see cref="uint.MaxValue"/>. </summary>
        public static readonly Vector2ui MAX_VALUE = new Vector2ui(uint.MaxValue);

        public uint x, y;

        uint Vector2<Vector2ui, uint>.x { get { return x; } set { x = value; } }
        uint Vector2<Vector2ui, uint>.y { get { return y; } set { y = value; } }

        public uint this[int i]
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

        public Vector2ui(uint v) { x = y = v; }
        public Vector2ui(uint x, uint y) { this.x = x; this.y = y; }

        public static Vector2i operator -(Vector2ui v) => new Vector2i(-v.x, -v.y);
        public static Vector2ui operator +(Vector2ui v1, Vector2ui v2) => new Vector2ui(v1.x + v2.x, v1.y + v2.y);
        public static Vector2ui operator +(Vector2ui v1, uint v2) => new Vector2ui(v1.x + v2, v1.y + v2);
        public static Vector2i operator -(Vector2ui v1, Vector2ui v2) => new Vector2i(v1.x - v2.x, v1.y - v2.y);
        public static Vector2i operator -(Vector2ui v1, uint v2) => new Vector2i(v1.x - v2, v1.y - v2);
        public static Vector2ui operator *(Vector2ui v, uint i) => new Vector2ui(i * v.x, i * v.y);
        public static Vector2ui operator *(uint i, Vector2ui v) => v * i;
        public static Vector2f operator *(Vector2ui v, float f) => new Vector2f(v.x * f, v.y * f);
        public static Vector2f operator *(float f, Vector2ui v) => v * f;
        public static Vector2ui operator /(Vector2ui v, uint i) => new Vector2ui(v.x / i, v.y / i);
        public static Vector2ui operator /(uint i, Vector2ui v) => new Vector2ui(i / v.x, i / v.y);
        public static Vector2f operator /(Vector2ui v, float f) => new Vector2f(v.x / f, v.y / f);
        public static Vector2f operator /(float f, Vector2ui v) => new Vector2f(f / v.x, f / v.y);
        public static bool operator ==(Vector2ui v1, Vector2ui v2) => v1.x == v2.x && v1.y == v2.y;
        public static bool operator !=(Vector2ui v1, Vector2ui v2) => v1.x != v2.x || v1.y != v2.y;
        public override bool Equals(object other) => other is Vector2ui && Equals((Vector2ui)other);
        public bool Equals(Vector2ui other) => this == other;
        public override int GetHashCode() => HashCode.Combine(x, y);

        public static implicit operator Vector2ui(uint i) => new Vector2ui(i, i);
        public static implicit operator Vector2i(Vector2ui obj) => new Vector2i(obj.x, obj.y);
        public static implicit operator Vector2f(Vector2ui obj) => new Vector2f(obj.x, obj.y);
        public static explicit operator Vector2ui(Vector2i obj) => new Vector2ui((uint)obj.x, (uint)obj.y);
        public static explicit operator Vector2ui(Vector2f obj) => new Vector2ui((uint)obj.x, (uint)obj.y);

        public static explicit operator System.Drawing.Size(Vector2ui v) => new System.Drawing.Size((int)v.x, (int)v.y);
        public static explicit operator System.Drawing.Point(Vector2ui v) => new System.Drawing.Point((int)v.x, (int)v.y);
        public static explicit operator Vector2ui(System.Drawing.Size v) => new Vector2ui((uint)v.Width, (uint)v.Height);
        public static explicit operator Vector2ui(System.Drawing.Point v) => new Vector2ui((uint)v.X, (uint)v.Y);

        public override string ToString() => $"{{ {x.ToString()} ; {y.ToString()} }}";

        public uint LengthSq() => Dot(this);
        public float Length() => Sqrt(LengthSq());
        public uint Dot(Vector2ui other) => x * other.x + y * other.y;
        public Vector2f GetNormalized() => this / Length();

        public void Add(Vector2ui other) => this += other;
        public void Multiply(uint factor) => this *= factor;
        public void Divide(uint divisor) => this /= divisor;

        public Vector2ui Sum(Vector2ui other) => this + other;
        public Vector2ui Sum(uint other) => this + other;
        public Vector2i Difference(Vector2ui other) => this - other;
        public Vector2i Difference(uint other) => this - other;
        public Vector2ui Product(uint factor) => this * factor;
        public Vector2f Product(float factor) => this * factor;
        public Vector2ui Quotient(uint divisor) => this / divisor;
        public Vector2f Quotient(float divisor) => this / divisor;

        Vector2f ChaosUtil.Primitives.Convertible<Vector2f>.Convert() => this;

        public bool LessEquals(uint compare) => x <= compare && y <= compare;
        public bool LessEquals(Vector2ui compare) => x <= compare.x && y <= compare.y;
        public bool Less(uint compare) => x < compare && y < compare;
        public bool Less(Vector2ui compare) => x < compare.x && y < compare.y;
        public bool GreaterEquals(uint compare) => x >= compare && y >= compare;
        public bool GreaterEquals(Vector2ui compare) => x >= compare.x && y >= compare.y;
        public bool Greater(uint compare) => x > compare && y > compare;
        public bool Greater(Vector2ui compare) => x > compare.x && y > compare.y;

        public Vector2ui Min(uint compare) => Clamping.Min(this, compare);
        public Vector2ui Min(Vector2ui compare) => Clamping.Min(this, compare);
        public Vector2ui Max(uint compare) => Clamping.Max(this, compare);
        public Vector2ui Max(Vector2ui compare) => Clamping.Max(this, compare);
    }
}
