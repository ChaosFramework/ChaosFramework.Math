using ChaosFramework.IO;
using ChaosFramework.Math.Vectors;
using static ChaosFramework.Math.Clamping;
using static ChaosFramework.Math.Signs;

namespace ChaosFramework.Math
{
    /// <summary> Describes a 3D bounding box. </summary>
    public class Bounds3f : Bounds<Bounds3f, Vector3f, float, Vector3f>
    {
        [ChaosIO.RegisterType]
        static void RegisterType() => ChaosIO.AddType(Read, Write);

        public static Bounds3f Read(System.IO.BinaryReader reader)
            => new Bounds3f(Vector3f.Read(reader), Vector3f.Read(reader));

        public static void Write(System.IO.BinaryWriter writer, Bounds3f box)
        {
            Vector3f.Write(writer, box.low);
            Vector3f.Write(writer, box.high);
        }

        public static Bounds3f Union(Bounds3f a, Bounds3f b)
            => new Bounds3f(Min(a.low, b.low), Max(a.high, b.high));

        public float left => low.x;
        public float right => high.x;
        public float bottom => low.y;
        public float top => high.y;
        public float front => low.z;
        public float back => high.z;

        /// <summary> The difference between the x-coordinates of the <see cref="high"/> and <see cref="low"/> corners. </summary>
        public float width => high.x - low.x;

        /// <summary> The difference between the y-coordinates of the <see cref="high"/> and <see cref="low"/> corners. </summary>
        public float height => high.y - low.y;

        /// <summary> The difference between the z-coordinates of the <see cref="high"/> and <see cref="low"/> corners. </summary>
        public float depth => high.z - low.z;

        public float volume => width * height * depth;

        public float surface
        {
            get
            {
                float x = width, y = height, z = depth;
                return 2 * x * y + 2 * x * z + 2 * y * z;
            }
        }

        public float maxDistanceFromOriginX => Max(Abs(low.x), Abs(high.x));
        public float maxDistanceFromOriginY => Max(Abs(low.y), Abs(high.y));
        public float maxDistanceFromOriginZ => Max(Abs(low.z), Abs(high.z));
        public float maxDistanceFromOrigin => Max(maxDistanceFromOriginX, maxDistanceFromOriginY, maxDistanceFromOriginZ);

        /// <summary>
        ///     Creates a <see cref="Bounds3f"/> with <see cref="Vector3f.MAX_VALUE"/> as minimum
        ///     and <see cref="Vector3f.MIN_VALUE"/> as maximum coordinates.
        /// </summary>
        public Bounds3f()
        {
            low = Vector3f.MAX_VALUE;
            high = Vector3f.MIN_VALUE;
        }

        /// <summary> Creates a <see cref="Bounds3f"/> with given <paramref name="low"/> and <paramref name="high"/> corners. </summary>
        /// <param name="low"> The <see cref="low"/> corners of this <see cref="Bounds3f"/>. </param>
        /// <param name="high"> The <see cref="high"/> corners of this <see cref="Bounds3f"/>. </param>
        public Bounds3f(Vector3f low, Vector3f high)
        {
            this.low = low;
            this.high = high;
        }

        public Bounds3f(float lowX, float lowY, float lowZ, float highX, float highY, float highZ)
        {
            low = new Vector3f(lowX, lowY, lowZ);
            high = new Vector3f(highX, highY, highZ);
        }
    }
}
