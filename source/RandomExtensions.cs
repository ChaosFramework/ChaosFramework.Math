using ChaosFramework.Collections.Immutable;
using ChaosFramework.Math.Vectors;
using ChaosUtil.Primitives;
using SysCol = System.Collections.Generic;
using static ChaosFramework.Math.Constants;

namespace ChaosFramework.Math
{
    public static class RandomExtensions
    {
        static readonly Wrapper<bool> staticConstructorLock = new Wrapper<bool>(false);
        static readonly ImmutableArray<Matrix> orientations;

        static RandomExtensions()
        {
            lock (staticConstructorLock)
            {
                if (staticConstructorLock)
                    return;

                SysCol.HashSet<Matrix> orientations = new SysCol.HashSet<Matrix>();
                for (int x = -1; x <= 2; x++)
                    for (int y = -1; y <= 2; y++)
                        for (int z = -1; z <= 2; z++)
                            orientations.Add(
                                Matrix.RotationX(x * PI_HALF)
                                * Matrix.RotationY(y * PI_HALF)
                                * Matrix.RotationZ(z * PI_HALF)
                            );

                RandomExtensions.orientations = System.Linq.Enumerable.ToArray(orientations);
                staticConstructorLock.value = true;
            }
        }

        /// <summary> Returns a random combination of 90° rotations around the 3D euler axes. </summary>
        public static Matrix CubeOrientation(this Random rnd) => orientations[rnd.RndInt(orientations.length)];

        /// <summary> Returns a normalized <see cref="Vector2f"/> with a random direction. </summary>
        public static Vector2f RndVector2(this Random rnd)
            => Vector2f.Normalize(new Vector2f(rnd.RndIncl() - 0.5f, rnd.RndIncl() - 0.5f));

        /// <summary> Returns a <see cref="Vector2f"/> with a random direction and a given <paramref name="length"/>. </summary>
        public static Vector2f RndVector2(this Random rnd, float length) => length * rnd.RndVector2();

        /// <summary> Returns a random normalized <see cref="Vector2f"/> whose angle to the given normal is lower than 90°. </summary>
        public static Vector2f RndVector2(this Random rnd, Vector2f normalizedNormal)
        {
            Vector2f v = rnd.RndVector2();
            float dot = Vector2f.Dot(v, normalizedNormal);
            if (dot < 0) v -= 2 * dot * normalizedNormal;
            return v;
        }

        /// <summary>
        ///     Returns a random <see cref="Vector2f"/> with a given length
        ///     whose angle to the given normal is lower than 90°.
        /// </summary>
        public static Vector2f RndVector2(this Random rnd, Vector2f normalizedNormal, float length)
            => rnd.RndVector2(normalizedNormal) * length;

        /// <summary> Returns a normalized <see cref="Vector3f"/> with a random direction. </summary>
        public static Vector3f RndVector3(this Random rnd)
            => Vector3f.Normalize(new Vector3f(rnd.RndIncl() - 0.5f, rnd.RndIncl() - 0.5f, rnd.RndIncl() - 0.5f));

        /// <summary> Returns a <see cref="Vector3f"/> with a random direction and a given <param name="length"/>. </summary>
        public static Vector3f RndVector3(this Random rnd, float length) => length * rnd.RndVector3();

        /// <summary> Returns a random <see cref="Vector3f"/> in the given bounding box. </summary>
        public static Vector3f RndVector3(this Random rnd, Vector3f low, Vector3f high)
            => new Vector3f(rnd.Rnd(low.x, high.x), rnd.Rnd(low.y, high.y), rnd.Rnd(low.z, high.z));

        /// <summary> Returns a random normalized <see cref="Vector3f"/> whose angle to the given normal is lower than 90°. </summary>
        public static Vector3f RndVector3(this Random rnd, Vector3f normalizedNormal)
        {
            Vector3f v = rnd.RndVector3();
            float dot = Vector3f.Dot(v, normalizedNormal);
            if (dot < 0)
                v -= 2 * dot * normalizedNormal;

            return v;
        }

        /// <summary>
        ///     Returns a random <see cref="Vector3f"/> with a given length
        ///     whose angle to the given normal is lower than 90°.
        /// </summary>
        /// <param name="normalizedAxis"> Axis to which the return value is orthogonal. </param>
        /// <param name="length"> The desired length of the return vector. </param>
        public static Vector3f RndVector3(this Random rnd, Vector3f normalizedAxis, float length)
            => rnd.RndVector3(normalizedAxis) * length;

        /// <summary>
        ///     Returns a random <see cref="Vector3f"/> that is orthogonal to the given <paramref name="normalizedAxis"/>.
        /// </summary>
        /// <param name="normalizedAxis"> Axis to which the return value is orthogonal. </param>
        /// <param name="length"> The desired length of the return vector. </param>
        public static Vector3f RndOrthogonal(this Random rnd, Vector3f normalizedAxis, float length = 1)
        {
            Vector3f vec = rnd.RndVector3();
            vec -= normalizedAxis * Vector3f.Dot(normalizedAxis, vec);
            return length * Vector3f.Normalize(vec);
        }

        /// <summary> Returns a normalized <see cref="Vector4f"/> with a random direction. </summary>
        public static Vector4f RndVector4(this Random rnd)
            => Vector4f.Normalize(new Vector4f(rnd.RndIncl() - 0.5f, rnd.RndIncl() - 0.5f, rnd.RndIncl() - 0.5f, rnd.RndIncl() - 0.5f));

        /// <summary> Returns a <see cref ="Vector4f"/> with a random direction and a given <param name="length">/>. </summary>
        public static Vector4f RndVector4(this Random rnd, float length) => length * rnd.RndVector4();

        /// <summary>
        ///     Returns a random <see cref="Vector2i"/> with
        ///     <see cref="Vector2i.x"/> in range [0, <paramref name="max"/>.<see cref="Vector2i.x"/>[
        ///     and
        ///     <see cref="Vector2i.y"/> in range [0, <paramref name="max"/>.<see cref="Vector2i.y"/>[
        ///     .
        /// </summary>
        public static Vector2i RndIntVector2(this Random rnd, Vector2i max) => rnd.RndIntVector2(max.x, max.y);

        /// <summary>
        ///     Returns a random <see cref="Vector2i"/> with
        ///     <see cref="Vector2i.x"/> in range [0, <paramref name="maxX"/>[
        ///     and
        ///     <see cref="Vector2i.y"/> in range [0, <paramref name="maxY"/>[
        ///     .
        /// </summary>
        public static Vector2i RndIntVector2(this Random rnd, int maxX, int maxY)
            => new Vector2i(rnd.RndInt(maxX), rnd.RndInt(maxY));
    }
}
