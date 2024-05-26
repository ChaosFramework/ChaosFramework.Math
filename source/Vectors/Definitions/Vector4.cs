namespace ChaosFramework.Math.Vectors.Definitions
{
    /// <summary> Represents a <see cref="Vector{Self, Scalar}"/> with four components. </summary>
    /// <typeparam name="Vector"> The <see cref="Vector{Vector, Scalar}"/> type implementing this interface. </typeparam>
    /// <typeparam name="Scalar"> The scalar type of <typeparamref name="Vector"/>. </typeparam>
    /// <typeparam name="Vec3">
    ///     The resulting <see cref="Vector3{Vector, Scalar, Vec2}"/> type when decreasing the number
    ///     of this <typeparamref name="Vector"> type's dimensions by one.
    /// </typeparam>
    /// <typeparam name="Vec2">
    ///     The resulting <see cref="Vector2{Vector, Scalar}"/> when decreasing the number of dimensions
    ///     <list type="bullet">
    ///         <item> of <typeparamref name="Vector"> by two. </item>
    ///         <item> of <typeparamref name="Vec3"/> by one. </item>
    ///     </list>
    /// </typeparam>
    public interface Vector4<Vector, Scalar, Vec3, Vec2>
        : Vector3<Vec3, Scalar, Vec2>
        where Vector : struct
                     , Vector<Vector, Scalar>
                     , Vector4<Vector, Scalar, Vec3, Vec2>
        where Scalar : struct
        where Vec3 : struct
                   , Vector<Vec3, Scalar>
                   , Vector3<Vec3, Scalar, Vec2>
        where Vec2 : struct
                   , Vector<Vec2, Scalar>
                   , Vector2<Vec2, Scalar>
    {
        /// <summary> The w-component of this <see cref="Vector"/>. </summary>
        Scalar w { get; set; }

        /// <summary> A <typeparamref name="Vec2"/> consisting of the x and w components of this <see cref="Vector"/>. </summary>
        Vec2 xw { get; set; }

        /// <summary> A <typeparamref name="Vec2"/> consisting of the y and w components of this <see cref="Vector"/>. </summary>
        Vec2 yw { get; set; }

        /// <summary> A <typeparamref name="Vec2"/> consisting of the z and w components of this <see cref="Vector"/>. </summary>
        Vec2 zw { get; set; }

        /// <summary> A <typeparamref name="Vec3"/> consisting of the x, y and z components of this <see cref="Vector"/>. </summary>
        Vec3 xyz { get; set; }

        /// <summary> A <typeparamref name="Vec3"/> consisting of the y, z and w components of this <see cref="Vector"/>. </summary>
        Vec3 yzw { get; set; }
    }
}
