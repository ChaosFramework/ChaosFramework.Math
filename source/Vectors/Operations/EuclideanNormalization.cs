using ChaosFramework.Math.Vectors.Definitions;

namespace ChaosFramework.Math.Vectors.Operations
{
    public interface EuclideanNormalization<Vector, Scalar, NormalizationVector, NormalizationScalar>
        where Vector : struct
                     , Vector<Vector, Scalar>
        where Scalar : struct
        where NormalizationVector : struct
                                  , Vector<NormalizationVector, NormalizationScalar>
        where NormalizationScalar : struct
    {
        NormalizationVector GetNormalized();
    }

    public interface EuclideanNormalization<Vector, Scalar>
        : EuclideanNormalization<Vector, Scalar, Vector, Scalar>
        where Vector : struct
                     , Vector<Vector, Scalar>
        where Scalar : struct
    {
        /// <summary> Rescales this instance such that its euclidean length is equal to 1. </summary>
        void Normalize();
    }
}
