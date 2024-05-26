using ChaosFramework.Math.Vectors.Definitions;

namespace ChaosFramework.Math.Vectors.Operations
{
    public interface DotProduct<Vector, Scalar, DotScalar>
        : Vector<Vector, Scalar>
        where Vector : struct, Vector<Vector, Scalar>
        where Scalar : struct
        where DotScalar : struct
    {
        /// <summary>
        ///     Returns the square of the euclidian length of this instance.
        ///     This is the same as the dot product of this instance with itself.
        /// </summary>
        DotScalar LengthSq();

        /// <summary> Returns the dot product of this instance and <paramref name="other"/>. </summary>
        /// <param name="other"> The right hand operand of the operation. </param>
        DotScalar Dot(Vector other);
    }

    public interface DotProduct<Vector, Scalar>
        : DotProduct<Vector, Scalar, Scalar>
        where Vector : struct, Vector<Vector, Scalar>
        where Scalar : struct
    {
    }
}
