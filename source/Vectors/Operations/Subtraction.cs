using ChaosFramework.Math.Vectors.Definitions;

namespace ChaosFramework.Math.Vectors.Operations
{
    public interface Subtraction<Vector, Scalar, Subtrahend, Result>
        : Vector<Vector, Scalar>
        where Vector : struct
                     , Vector<Vector, Scalar>
        where Scalar : struct
        where Subtrahend : struct
        where Result : struct
    {
        Result Difference(Subtrahend subtrahend);
    }

    public interface Subtraction<Vector, Scalar>
        : Subtraction<Vector, Scalar, Vector, Vector>
        where Vector : struct
                     , Vector<Vector, Scalar>
        where Scalar : struct
    {
        void Subtract(Vector subtrahend);
    }
}
