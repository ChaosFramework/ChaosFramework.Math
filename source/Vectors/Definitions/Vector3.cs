namespace ChaosFramework.Math.Vectors.Definitions
{
    /// <summary> Represents a <see cref="Vector{Self, Scalar}"/> with three components. </summary>
    /// <typeparam name="Vector"> The <see cref="Vector{Vector, Scalar}"/> type implementing this interface. </typeparam>
    /// <typeparam name="Scalar"> The scalar type of <typeparamref name="Vector"/>. </typeparam>
    /// <typeparam name="Vec2">
    ///     The resulting <see cref="Vector2{Vector, Scalar}"/> type when decreasing the number
    ///     of this <typeparamref name="Vector"> type's dimensions by one.
    /// </typeparam>
    public interface Vector3<Vector, Scalar, Vec2>
        : Vector2<Vec2, Scalar>
        where Vector : struct
                     , Vector<Vector, Scalar>
                     , Vector3<Vector, Scalar, Vec2>
        where Scalar : struct
        where Vec2 : struct
                   , Vector<Vec2, Scalar>
                   , Vector2<Vec2, Scalar>
    {
        /// <summary> The z-component of this <see cref="Vector"/>. </summary>
        Scalar z { get; set; }

        /// <summary> A <typeparamref name="Vec2"/> consisting of the x and y components of this <see cref="Vector"/>. </summary>
        Vec2 xy { get; set; }

        /// <summary> A <typeparamref name="Vec2"/> consisting of the x and z components of this <see cref="Vector"/>. </summary>
        Vec2 xz { get; set; }

        /// <summary> A <typeparamref name="Vec2"/> consisting of the y and z components of this <see cref="Vector"/>. </summary>
        Vec2 yz { get; set; }
    }
}
