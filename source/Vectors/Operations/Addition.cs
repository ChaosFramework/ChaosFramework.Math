using ChaosFramework.Math.Vectors.Definitions;

namespace ChaosFramework.Math.Vectors.Operations
{
    /// <summary>
    ///     Specifies that the implementing <see cref="Vector"/> defines an addition operation
    ///     with the specified <typeparamref name="Addend"/> and <typeparamref name="Result"/> type.
    /// </summary>
    /// <typeparam name="Vector"> The <see cref="Vector"/> defining the operation. </typeparam>
    /// <typeparam name="Scalar"> The scalar type of <typeparamref name="Vector"/>. </typeparam>
    /// <typeparam name="Addend"> The type of the addend. </typeparam>
    /// <typeparam name="Result"> The type of the resulting sum. </typeparam>
    public interface Addition<Vector, Scalar, Addend, Result>
        : Vector<Vector, Scalar>
        where Vector : struct
                     , Vector<Vector, Scalar>
        where Scalar : struct
        where Addend : struct
        where Result : struct
    {
        /// <summary> Returns the sum of <see langword="this"/> instance and the provided <paramref name="addend"/>. </summary>
        /// <param name="addend"> The addend to be added. </param>
        Result Sum(Addend addend);
    }

    /// <summary>
    ///     Specifies that the implementing <see cref="Vector"/> defines an addition operation
    ///     with its own <typeparamref name="Vector"/> type as addend and result.
    /// </summary>
    /// <typeparam name="Vector"> The <see cref="Vector"/> defining the operation. </typeparam>
    /// <typeparam name="Scalar"> The scalar type of <typeparamref name="Vector"/>. </typeparam>
    public interface Addition<Vector, Scalar>
        : Addition<Vector, Scalar, Vector, Vector>
        where Vector : struct
                     , Vector<Vector, Scalar>
        where Scalar : struct
    {
        /// <summary> Adds the provided <paramref name="addend"/> to <see langword="this"/> instance. </summary>
        /// <param name="addend"> The addend to be added. </param>
        void Add(Vector addend);
    }
}
