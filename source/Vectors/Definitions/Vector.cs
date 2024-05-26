namespace ChaosFramework.Math.Vectors.Definitions
{
    public interface Vector<Self, Scalar>
        where Self : struct, Vector<Self, Scalar>
        where Scalar : struct
    {
        /// <summary> Sets or gets the scalar value for the specified dimension. </summary>
        /// <param name="dimension"> The dimension for which to retrieve the scalar. </param>
        Scalar this[int dimension] { get; set; }

        bool Equals(Self other);
    }
}
