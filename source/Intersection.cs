using ChaosFramework.Math.Vectors;

namespace ChaosFramework.Math
{
    public static class Intersection
    {
        /// <summary>
        ///     Calculates the intersection point of the provided ray with the given triangle
        ///     using Möller and Trumbore's 'Fast, minimum storage ray-triangle intersection' algorithm.
        ///
        ///     <para>
        ///         Source: <br/>
        ///         Fast, minimum storage ray-triangle intersection <br/>
        ///         Journal of Graphics Tools, Volume 2, Issue 1, pages 21-28 <br/>
        ///         1997 <br/>
        ///         DOI: 10.1080/10867651.1997.10487468 <br/>
        ///         <see href="https://www.graphics.cornell.edu/pubs/1997/MT97.pdf"/>
        ///     </para>
        /// </summary>
        /// <param name="ray"> The ray to be tested against. </param>
        /// <param name="a"> The first vertex of the triangle △<paramref name="a"/><paramref name="b"/><paramref name="c"/>. </param>
        /// <param name="b"> The second vertex of the triangle △<paramref name="a"/><paramref name="b"/><paramref name="c"/>. </param>
        /// <param name="c"> The third vertex of the triangle △<paramref name="a"/><paramref name="b"/><paramref name="c"/>. </param>
        /// <param name="intersection">
        ///     The intersection point between ray and △<paramref name="a"/><paramref name="b"/><paramref name="c"/>.
        ///     <see cref="Vector3f.NAN"/> if <paramref name="ray"/> does not intersect
        ///     △<paramref name="a"/><paramref name="b"/><paramref name="c"/>.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if <paramref name="ray"/> hits △<paramref name="a"/><paramref name="b"/><paramref name="c"/>;
        ///     <see langword="false"/> otherwise.
        /// </returns>
        public static bool Intersect(Ray ray, Vector3f a, Vector3f b, Vector3f c, out Vector3f intersection)
        {
            const float EPSILON = 0.0000001f;

            intersection = Vector3f.NAN;

            Vector3f edge1 = b - a;
            Vector3f edge2 = c - a;

            Vector3f pvec = Vector3f.Cross(ray.direction, edge2);
            float det = Vector3f.Dot(edge1, pvec);
            if (det > -EPSILON && det < EPSILON)
                return false; // parallel

            Vector3f tvec = ray.origin - a;
            float u = Vector3f.Dot(tvec, pvec);
            if (u < 0.0f || u > det)
                return false; // miss

            Vector3f qvec = Vector3f.Cross(tvec, edge1);
            float v = Vector3f.Dot(ray.direction, qvec);
            if (v < 0.0f || u + v > det)
                return false; // miss

            float t = Vector3f.Dot(edge2, qvec) / det;
            if (t > EPSILON) // hit
            {
                // calculate the intersection point instead of returning (t, u, v)
                intersection = ray.origin + ray.direction * t;
                return true;
            }
            else
                return false; // intersection behind ray origin
        }

        // TODO: Specify where intersection.origin will be
        /// <summary> Calculates the intersection ray of two planes. </summary>
        /// <param name="p1"> The first plane. </param>
        /// <param name="p2"> The second plane. </param>
        /// <param name="intersection">
        ///     The intersection ray of <paramref name="p1"/> and <paramref name="p2"/>.
        ///     <see cref="Ray.NAN"/> if no intersection is detected.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if <paramref name="p1"/> and <paramref name="p2"/> intersect.
        ///     <see langword="false"/> otherwise.
        /// </returns>
        public static bool Intersect(Plane p1, Plane p2, out Ray intersection)
        {
            const float EPSILON = 0.0000001f;

            Vector3f direction = Vector3f.Cross(p1.normal, p2.normal);
            float denom = direction.LengthSq();

            if (denom > EPSILON)
            {
                Vector3f origin = (
                    Vector3f.Cross(direction, p2.normal) * p1.d +
                    Vector3f.Cross(p1.normal, direction) * p2.d
                    ) / denom;
                intersection = new Ray(origin, direction);
                return true;
            }
            else
            {
                intersection = Ray.NAN;
                return false;
            }
        }

        public static Vector3f Intersect(Ray ray, Plane plane)
        {
            Vector3f diff = ray.origin - plane.origin;
            float dot1 = Vector3f.Dot(diff, plane.normal);
            float dot2 = Vector3f.Dot(ray.direction, plane.normal);
            float d = dot1 / dot2;
            return ray.origin - ray.direction * d;
        }

        public static bool Intersect(Ray ray, Bounds3f box)
        {
            float lowXY_f = ((box.low.z - ray.origin.z) / ray.direction.z);
            if (lowXY_f >= 0)
            {
                Vector2f lowXY = ray.origin.xy + ray.direction.xy * lowXY_f;
                if (lowXY.x > box.low.x && lowXY.x < box.high.x && lowXY.y > box.low.y && lowXY.y < box.high.y)
                    return true;
            }

            float highXY_f = ((box.high.z - ray.origin.z) / ray.direction.z);
            if (highXY_f >= 0)
            {
                Vector2f highXY = ray.origin.xy + ray.direction.xy * highXY_f;
                if (highXY.x > box.low.x && highXY.x < box.high.x && highXY.y > box.low.y && highXY.y < box.high.y)
                    return true;
            }

            float lowXZ_f = ((box.low.y - ray.origin.y) / ray.direction.y);
            if (lowXZ_f >= 0)
            {
                Vector2f lowXZ = ray.origin.xz + ray.direction.xz * lowXZ_f;
                if (lowXZ.x > box.low.x && lowXZ.x < box.high.x && lowXZ.y > box.low.z && lowXZ.y < box.high.z)
                    return true;
            }

            float highXZ_f = ((box.high.y - ray.origin.y) / ray.direction.y);
            if (highXZ_f >= 0)
            {
                Vector2f highXZ = ray.origin.xz + ray.direction.xz * highXZ_f;
                if (highXZ.x > box.low.x && highXZ.x < box.high.x && highXZ.y > box.low.z && highXZ.y < box.high.z)
                    return true;
            }

            float lowYZ_f = ((box.low.x - ray.origin.x) / ray.direction.x);
            if (lowYZ_f >= 0)
            {
                Vector2f lowYZ = ray.origin.yz + ray.direction.yz * lowYZ_f;
                if (lowYZ.x > box.low.y && lowYZ.x < box.high.y && lowYZ.y > box.low.z && lowYZ.y < box.high.z)
                    return true;
            }

            float highYZ_f = ((box.high.x - ray.origin.x) / ray.direction.x);
            if (highYZ_f >= 0)
            {
                Vector2f highYZ = ray.origin.yz + ray.direction.yz * highYZ_f;
                if (highYZ.x > box.low.y && highYZ.x < box.high.y && highYZ.y > box.low.z && highYZ.y < box.high.z)
                    return true;
            }

            return false;
        }
    }
}
