using ChaosFramework.Math.Vectors;

namespace ChaosFramework.Math
{
    public struct Ray
    {
        public static readonly Ray NAN = new Ray(Vector3f.NAN, Vector3f.NAN);

        public readonly Vector3f origin;
        public readonly Vector3f direction;

        public Ray(Vector3f origin, Vector3f direction)
        {
            this.origin = origin;
            this.direction = direction;
        }

        /// <summary> Returns the distance of <paramref name="point"/> to <see cref="origin"/> along <see cref="direction"/>. </summary>
        /// <param name="point"> The point to determine the distance from. </param>
        public float Distance(Vector3f point) => Vector3f.Dot(point - origin, direction) / direction.LengthSq();

        /// <summary>
        ///     Returns the point with the smallest euclidean distance to <paramref name="point"/> on this ray.
        ///     Also projects onto the negative part of the ray.
        /// </summary>
        /// <param name="point"> The point to project. </param>
        public Vector3f Project(Vector3f point) => origin + Distance(point) * direction;

        public bool IsNaN() => origin.IsNaN() || direction.IsNaN();

        public static bool operator ==(Ray a, Ray b) => a.origin == b.origin && a.direction == b.direction;
        public static bool operator !=(Ray a, Ray b) => a.origin != b.origin || a.direction != b.direction;
        public override bool Equals(object obj) => obj is Ray ? Equals((Ray)obj) : false;
        public bool Equals(Ray compare) => this == compare;
        public override int GetHashCode() => origin.GetHashCode() ^ direction.GetHashCode();
    }
}
