using ChaosFramework.Math.Vectors.Definitions;

namespace ChaosFramework.Math.Vectors.Operations
{
    public interface EuclideanLength<Vector, Scalar, LengthScalar>
        : Vector<Vector, Scalar>
        where Vector : struct
                     , Vector<Vector, Scalar>
        where Scalar : struct
        where LengthScalar : struct
    {
        LengthScalar Length();
    }

    public interface EuclideanLength<Vector, Scalar>
        : EuclideanLength<Vector, Scalar, Scalar>
        where Vector : struct
                     , Vector<Vector, Scalar>
        where Scalar : struct
    {
    }
}
