namespace ChaosFramework.Math.Vectors.Definitions
{
    /// <summary> Represents a <see cref="Vector{Self, Scalar}"/> with two components. </summary>
    /// <typeparam name="Vector"> The <see cref="Vector{Vector, Scalar}"/> type implementing this interface. </typeparam>
    /// <typeparam name="Scalar"> The scalar type of <typeparamref name="Vector"/>. </typeparam>
    public interface Vector2<Vector, Scalar>
        where Vector : struct
                     , Vector<Vector, Scalar>
                     , Vector2<Vector, Scalar>
        where Scalar : struct
    {
        /// <summary> The x-component of this <see cref="Vector"/>. </summary>
        Scalar x { get; set; }

        /// <summary> The y-component of this <see cref="Vector"/>. </summary>
        Scalar y { get; set; }
    }
}
