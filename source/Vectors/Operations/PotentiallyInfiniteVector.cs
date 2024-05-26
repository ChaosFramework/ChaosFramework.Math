namespace ChaosFramework.Math.Vectors.Operations
{
    public interface PotentiallyInfiniteVector
    {
        /// <summary> Determines whether this instance has any components representing any infinity. </summary>
        bool IsInfinite();
    }
}
