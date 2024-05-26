namespace ChaosFramework.Math.Vectors.Operations
{
    public interface PotentiallyNaNVector
    {
        /// <summary> Determines whether this instance has any components representing NaN (Not a Number). </summary>
        bool IsNaN();
    }
}
