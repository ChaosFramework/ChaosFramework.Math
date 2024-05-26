using ChaosFramework.IO;
using ChaosFramework.Math.Vectors;
using static ChaosFramework.Math.Clamping;
using static ChaosFramework.Math.Signs;

namespace ChaosFramework.Math
{
    public class Bounds2i : Bounds<Bounds2i, Vector2i, int, Vector2f>
    {
        [ChaosIO.RegisterType]
        static void RegisterType() => ChaosIO.AddType(Read, Write);

        public static Bounds2i Read(System.IO.BinaryReader reader)
            => new Bounds2i(Vector2i.Read(reader), Vector2i.Read(reader));

        public static void Write(System.IO.BinaryWriter writer, Bounds2i v)
        {
            Vector2i.Write(writer, v.low);
            Vector2i.Write(writer, v.high);
        }

        public int left => low.x;
        public int right => high.x;
        public int bottom => low.y;
        public int top => high.y;

        public Vector2i topLeft => new Vector2i(left, top);
        public Vector2i topRight => new Vector2i(right, top);
        public Vector2i bottomLeft => new Vector2i(left, bottom);
        public Vector2i bottomRight => new Vector2i(right, bottom);

        public int width => high.x - low.x;
        public int height => high.y - low.y;

        public Vector4i xywh => new Vector4i(left, bottom, width, height);

        public int area => width * height;
        public int circumference => 2 * width + 2 * height;
        public float ratio => (float)width / height;

        public float maxDistanceFromOriginX => Max(Abs(low.x), Abs(high.x));
        public float maxDistanceFromOriginY => Max(Abs(low.y), Abs(high.y));
        public float maxDistanceFromOrigin => Max(maxDistanceFromOriginX, maxDistanceFromOriginY);

        public Bounds2i()
        {
            low = Vector2i.MAX_VALUE;
            high = Vector2i.MIN_VALUE;
        }

        public Bounds2i(Vector2i low, Vector2i high)
        {
            this.low = low;
            this.high = high;
        }

        public Bounds2i(int xLow, int yLow, int xHigh, int yHigh)
            : this(new Vector2i(xLow, yLow), new Vector2i(xHigh, yHigh))
        { }

        public static implicit operator System.Drawing.Rectangle(Bounds2i rect)
            => new System.Drawing.Rectangle(rect.low.x, rect.low.y, rect.width, rect.height);

        public static implicit operator Bounds2i(System.Drawing.Rectangle rect)
            => new Bounds2i(rect.X, rect.Y, rect.X + rect.Width, rect.Y + rect.Height);
    }
}
