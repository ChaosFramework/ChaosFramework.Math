using ChaosFramework.Math.Vectors.Definitions;

namespace ChaosFramework.Math.Vectors.Operations
{
    public interface Multiplication<Vector, Scalar, Factor, Result>
        : Vector<Vector, Scalar>
        where Vector : struct
                     , Vector<Vector, Scalar>
        where Scalar : struct
        where Factor : struct
        where Result : struct
    {
        Result Product(Factor factor);
    }

    public interface Multiplication<Vector, Scalar, Factor>
        : Multiplication<Vector, Scalar, Factor, Vector>
        where Vector : struct
                     , Vector<Vector, Scalar>
        where Scalar : struct
        where Factor : struct
    {
        void Multiply(Factor factor);
    }
}
