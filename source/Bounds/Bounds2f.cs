using ChaosFramework.IO;
using ChaosFramework.Math.Vectors;
using static ChaosFramework.Math.Clamping;
using static ChaosFramework.Math.Signs;

namespace ChaosFramework.Math
{
    public class Bounds2f : Bounds<Bounds2f, Vector2f, float, Vector2f>
    {
        [ChaosIO.RegisterType]
        static void RegisterType() => ChaosIO.AddType(Read, Write);

        public static Bounds2f Read(System.IO.BinaryReader reader)
            => new Bounds2f(Vector2f.Read(reader), Vector2f.Read(reader));

        public static void Write(System.IO.BinaryWriter writer, Bounds2f rect)
        {
            Vector2f.Write(writer, rect.low);
            Vector2f.Write(writer, rect.high);
        }

        public float left => low.x;
        public float right => high.x;
        public float bottom => low.y;
        public float top => high.y;

        public Vector2f topLeft => new Vector2f(left, top);
        public Vector2f topRight => new Vector2f(right, top);
        public Vector2f bottomLeft => new Vector2f(left, bottom);
        public Vector2f bottomRight => new Vector2f(right, bottom);

        /// <summary> The difference between the x-coordinates of the <see cref="high"/> and <see cref="low"/> corners. </summary>
        public float width => Max(0, high.x - low.x);

        /// <summary> The difference between the y-coordinates of the <see cref="high"/> and <see cref="low"/> corners. </summary>
        public float height => Max(0, high.y - low.y);

        public float area => width * height;
        public float circumference => 2 * width + 2 * height;
        public float ratio => width / height;

        public float maxDistanceFromOriginX => Max(Abs(low.x), Abs(high.x));
        public float maxDistanceFromOriginY => Max(Abs(low.y), Abs(high.y));
        public float maxDistanceFromOrigin => Max(maxDistanceFromOriginX, maxDistanceFromOriginY);

        /// <summary>
        ///     Creates a <see cref="Bounds2f"/> with <see cref="Vector2f.MAX_VALUE"/> as minimum
        ///     and <see cref="Vector2f.MIN_VALUE"/> as maximum coordinates.
        /// </summary>
        public Bounds2f()
        {
            low = Vector2f.MAX_VALUE;
            high = Vector2f.MIN_VALUE;
        }

        /// <summary> Creates a <see cref="Bounds2f"/> with given <paramref name="low"/> and <paramref name="high"/> corners. </summary>
        /// <param name="low"> The <see cref="low"/> corners of this <see cref="Bounds2f"/>. </param>
        /// <param name="high"> The <see cref="high"/> corners of this <see cref="Bounds2f"/>. </param>
        public Bounds2f(Vector2f low, Vector2f high)
        {
            this.low = low;
            this.high = high;
        }

        public Bounds2f(float lowX, float lowY, float highX, float highY)
        {
            low = new Vector2f(lowX, lowY);
            high = new Vector2f(highX, highY);
        }
    }
}
