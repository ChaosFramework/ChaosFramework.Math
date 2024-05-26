using ChaosFramework.Math.Vectors;

namespace ChaosFramework.Math
{
    public class Plane
    {
        const float PARALLEL_PLANE_THRESHOLD = 1e-4f;

        public static float Dot(Plane p, Vector3f v) => p.Dot(v);

        public static Plane Normalize(Plane p)
        {
            float invLen = 1f / p.normal.Length();
            return new Plane(p.normal * invLen, p.d * invLen);
        }

        public static Plane FromPoints(Vector3f a, Vector3f b, Vector3f c)
        {
            Vector3f cross = Vector3f.Cross(b - a, c - a);
            return new Plane(cross, Vector3f.Dot(a, cross));
        }

        public readonly Vector3f normal;
        public readonly float d;

        /// <summary> The a in this plane's ax + by + cz + d = 0 form. </summary>
        public float a => normal.x;

        /// <summary> The b in this plane's ax + by + cz + d = 0 form. </summary>
        public float b => normal.y;

        /// <summary> The c in this plane's ax + by + cz + d = 0 form. </summary>
        public float c => normal.z;

        bool? _normalized = null;

        /// <summary> Returns whether this plane is normalized. </summary>
        public bool normalized => (_normalized ?? (_normalized = normal.IsNormalized())).Value;

        public Vector3f origin => normal * d;

        public Plane(float nX, float nY, float nZ, float d)
            : this(new Vector3f(nX, nY, nZ), d)
        { }

        public Plane(Vector3f normal, float d)
        {
            this.normal = normal;
            this.d = d;
        }

        public float Dot(Vector3f v) => Vector3f.Dot(normal, v) - d;

        public bool IsNaN() => float.IsNaN(d) || normal.IsNaN();

        /// <summary> Returns whether this plane is visible by the given <paramref name="look"/> vector. </summary>
        /// <param name="look"> The look vector. </param>
        public bool IsVisible(Vector3f look) => Vector3f.Dot(normal, look) < -PARALLEL_PLANE_THRESHOLD;

        /// <summary>
        ///     Returns the signed distance between <paramref name="point"/> and this plane.
        ///     <para>
        ///         If the plane is not normalized, the distance ist scaled by the length of <see cref="normal"/>.
        ///     </para>
        /// </summary>
        /// <param name="point"> The point to compute the distance from. </param>
        public float Distance(Vector3f point) => Vector3f.Dot(normal, point - origin);

        /// <summary>
        ///     Returns the closest point on this plane to the given <paramref name="point"/>
        ///     <para>
        ///         Only works for normalized planes.
        ///     </para>
        /// </summary>
        /// <param name="point"> The point to project. </param>
        public Vector3f Project(Vector3f point) => point - Distance(point) * normal;

        public static explicit operator Vector4f(Plane p) => new Vector4f(p.normal, p.d);

        public static bool operator ==(Plane a, Plane b) => a.normal == b.normal && a.d == b.d;
        public static bool operator !=(Plane a, Plane b) => a.normal != b.normal || a.d != b.d;

        public override bool Equals(object obj) => Equals(obj as Plane);
        public bool Equals(Plane compare) => compare != null && this == compare;
        public override int GetHashCode() => normal.GetHashCode() ^ d.GetHashCode();
    }
}
