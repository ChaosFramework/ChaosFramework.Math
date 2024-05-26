using ChaosFramework.Math.Vectors.Definitions;

namespace ChaosFramework.Math.Vectors.Operations
{
    /// <summary>
    ///     Specifies that the implementing <see cref="Vector"/> defines a division operation
    ///     with the specified <typeparamref name="Divisor"/> and <typeparamref name="Result"/> type.
    /// </summary>
    /// <typeparam name="Vector"> The <see cref="Vector"/> defining the operation. </typeparam>
    /// <typeparam name="Scalar"> The scalar type of <typeparamref name="Vector"/>. </typeparam>
    /// <typeparam name="Divisor"> The type of the divisor. </typeparam>
    /// <typeparam name="Result"> The type of the resulting quotient. </typeparam>
    public interface Division<Vector, Scalar, Divisor, Result>
        : Vector<Vector, Scalar>
        where Vector : struct
                     , Vector<Vector, Scalar>
        where Scalar : struct
        where Divisor : struct
        where Result : struct
    {
        /// <summary> Returns the quotient of <see langword="this"/> instance and the provided <paramref name="divisor"/>. </summary>
        /// <param name="divisor"> The divisor to be divided by. </param>
        Result Quotient(Divisor divisor);
    }

    /// <summary>
    ///     Specifies that the implementing <see cref="Vector"/> defines a division operation
    ///     with the specified <typeparam name="Divisor"/> and its own <typeparamref name="Vector"/> type as result.
    /// </summary>
    /// <typeparam name="Vector"> The <see cref="Vector"/> defining the operation. </typeparam>
    /// <typeparam name="Scalar"> The scalar type of <typeparamref name="Vector"/>. </typeparam>
    public interface Division<Vector, Scalar, Divisor>
        : Division<Vector, Scalar, Divisor, Vector>
        where Vector : struct
                     , Vector<Vector, Scalar>
        where Scalar : struct
        where Divisor : struct
    {
        /// <summary> Divides <see langword="this"/> instance by the provided <paramref name="divisor"/>. </summary>
        /// <param name="divisor"> The divisor to be divided by. </param>
        void Divide(Divisor divisor);
    }
}
