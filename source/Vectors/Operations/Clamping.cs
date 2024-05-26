using ChaosFramework.Math.Vectors.Definitions;

namespace ChaosFramework.Math.Vectors.Operations
{
    /// <summary>
    ///     Specifies that the implementing <see cref="Vector"/> defines component-wise clamping operations
    ///     with the specified <typeparamref name="Compare"/> and <typeparamref name="Result"/> type.
    /// </summary>
    /// <typeparam name="Vector"> The <see cref="Vector"/> defining the operations. </typeparam>
    /// <typeparam name="Scalar"> The scalar type of <typeparamref name="Vector"/>. </typeparam>
    /// <typeparam name="Compare"> The type of the operand to be compared to. </typeparam>
    /// <typeparam name="Result"> The type of the clamped value. </typeparam>
    public interface Clamping<Vector, Scalar, Compare, Result>
        where Vector : struct, Vector<Vector, Scalar>
        where Scalar : struct
        where Compare : struct
    {
        /// <summary>
        ///     Returns the component-wise minimum of <see langword="this"/> instance
        ///     and the provided <paramref name="compare"/> value.
        /// </summary>
        /// <param name="compare"> The value to compare the components of <see langword="this"/> instance to. </param>
        Result Min(Compare compare);

        /// <summary>
        ///     Returns the component-wise maximum of <see langword="this"/> instance
        ///     and the provided <paramref name="compare"/> value.
        /// </summary>
        /// <param name="compare"> The value to compare the components of <see langword="this"/> instance to. </param>
        Result Max(Compare compare);
    }

    /// <summary>
    ///     Specifies that the implementing <see cref="Vector"/> defines component-wise clamping operations
    ///     with the specified <typeparamref name="Compare"/> type and its own <typeparam name="Vector"/> type as result.
    /// </summary>
    /// <typeparam name="Vector"> The <see cref="Vector"/> defining the operations. </typeparam>
    /// <typeparam name="Scalar"> The scalar type of <typeparamref name="Vector"/>. </typeparam>
    public interface Clamping<Vector, Scalar, Compare>
        : Clamping<Vector, Scalar, Compare, Vector>
        where Vector : struct, Vector<Vector, Scalar>
        where Scalar : struct
        where Compare : struct
    {
    }
}
