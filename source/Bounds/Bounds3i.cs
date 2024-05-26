using ChaosFramework.IO;
using ChaosFramework.Math.Vectors;
using static ChaosFramework.Math.Clamping;
using static ChaosFramework.Math.Signs;

namespace ChaosFramework.Math
{
    public class Bounds3i : Bounds<Bounds3i, Vector3i, int, Vector3f>
    {
        [ChaosIO.RegisterType]
        static void RegisterType() => ChaosIO.AddType(Read, Write);

        public static Bounds3i Read(System.IO.BinaryReader reader)
            => new Bounds3i(Vector3i.Read(reader), Vector3i.Read(reader));

        public static void Write(System.IO.BinaryWriter writer, Bounds3i v)
        {
            Vector3i.Write(writer, v.low);
            Vector3i.Write(writer, v.high);
        }

        public int left => low.x;
        public int right => high.x;
        public int bottom => low.y;
        public int top => high.y;
        public int front => low.z;
        public int back => high.z;

        public int width => high.x - low.x;
        public int height => high.y - low.y;
        public int depth => high.z - low.z;

        public int volume => width * height * depth;

        public int surface
        {
            get
            {
                int x = width, y = height, z = depth;
                return 2 * x * y + 2 * x * z + 2 * y * z;
            }
        }

        public int maxDistanceFromOriginX => Max(Abs(low.x), Abs(high.x));
        public int maxDistanceFromOriginY => Max(Abs(low.y), Abs(high.y));
        public int maxDistanceFromOriginZ => Max(Abs(low.z), Abs(high.z));
        public int maxDistanceFromOrigin => Max(maxDistanceFromOriginX, maxDistanceFromOriginY, maxDistanceFromOriginZ);

        public Bounds3i()
        {
            low = Vector3i.MAX_VALUE;
            high = Vector3i.MIN_VALUE;
        }

        public Bounds3i(Vector3i low, Vector3i high)
        {
            this.low = low;
            this.high = high;
        }

        public Bounds3i(int lowX, int lowY, int lowZ, int highX, int highY, int highZ)
        {
            low = new Vector3i(lowX, lowY, lowZ);
            high = new Vector3i(highX, highY, highZ);
        }
    }
}
