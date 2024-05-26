using ChaosFramework.Math.Vectors;

namespace ChaosFramework.Math
{
    public struct LineSegment
    {
        public readonly Vector3f a, b;

        public LineSegment(Vector3f a, Vector3f b)
        {
            this.a = a;
            this.b = b;
        }

        public static bool operator ==(LineSegment a, LineSegment b) => a.a == b.a && a.b == b.b;
        public static bool operator !=(LineSegment a, LineSegment b) => a.a != b.a || a.b != b.b;
        public override bool Equals(object obj) => (obj is LineSegment) ? (this == (LineSegment)obj) : false;
        public override int GetHashCode() => a.GetHashCode() ^ b.GetHashCode();
    }
}
