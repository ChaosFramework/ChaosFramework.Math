using ChaosFramework.Math.Vectors.Definitions;

namespace ChaosFramework.Math.Vectors.Operations
{
    /// <summary>
    ///     Specifies that the implementing <see cref="Vector"/> defines component-wise comparison operations
    ///     with the specified right hand <typeparamref name="Compare"/> type.
    /// </summary>
    /// <typeparam name="Vector"> The <see cref="Vector"/> defining the operations. </typeparam>
    /// <typeparam name="Scalar"> The scalar type of <typeparamref name="Vector"/>. </typeparam>
    /// <typeparam name="Compare"> The right hand operand type to be compared to. </typeparam>
    public interface ComponentwiseAllComparison<Vector, Scalar, Compare>
        where Vector : struct, Vector<Vector, Scalar>
        where Scalar : struct
        where Compare : struct
    {
        /// <summary>
        ///     Determines whether all of <see langword="this"/> instance's components are greater than or equal to
        ///     their corresponding components of <paramref name="compare"/>.
        /// </summary>
        /// <param name="compare"> The right hand operand to be compared to. </param>
        bool GreaterEquals(Compare compare);

        /// <summary>
        ///     Determines whether all of <see langword="this"/> instance's components are less than or equal to
        ///     their corresponding components of <paramref name="compare"/>.
        /// </summary>
        /// <param name="compare"> The right hand operand to be compared to. </param>
        bool LessEquals(Compare compare);

        /// <summary>
        ///     Determines whether all of <see langword="this"/> instance's components are greater than
        ///     their corresponding components of <paramref name="compare"/>.
        /// </summary>
        /// <param name="compare"> The right hand operand to be compared to. </param>
        bool Greater(Compare compare);

        /// <summary>
        ///     Determines whether all of <see langword="this"/> instance's components are less than
        ///     their corresponding components of <paramref name="compare"/>.
        /// </summary>
        /// <param name="compare"> The right hand operand to be compared to. </param>
        bool Less(Compare compare);
    }

    /// <summary>
    ///     Specifies that the implementing <see cref="Vector"/> defines component-wise comparison operations
    ///     with another instance of the same <see cref="Vector"/> type.
    /// </summary>
    /// <typeparam name="Vector"> The <see cref="Vector"/> defining the operations. </typeparam>
    /// <typeparam name="Scalar"> The scalar type of <typeparamref name="Vector"/>. </typeparam>
    public interface ComponentwiseAllComparison<Vector, Scalar>
        : ComponentwiseAllComparison<Vector, Scalar, Vector>
        where Vector : struct, Vector<Vector, Scalar>
        where Scalar : struct
    {
    }
}
