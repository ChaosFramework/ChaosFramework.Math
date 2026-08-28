using ChaosFramework.Math.Vectors.Definitions;
using ChaosFramework.Math.Vectors.Operations;

namespace ChaosFramework.Math
{
    [System.Diagnostics.DebuggerDisplay("low={" + nameof(low) + "}; high={" + nameof(high) + "}")]
    public abstract class Bounds<Self, Vector, Scalar, Center>
        where Self : Bounds<Self, Vector, Scalar, Center>
                   , new()
        where Vector : struct
                     , Vector<Vector, Scalar>
                     , Addition<Vector, Scalar>
                     , Subtraction<Vector, Scalar>
                     , Multiplication<Vector, Scalar, float, Center>
                     , ComponentwiseAllComparison<Vector, Scalar>
                     , Clamping<Vector, Scalar, Vector>
        where Scalar : struct
        where Center : struct
    {
        // TODO: decide whether negative width, height or depth should produce defined behavior

        public Vector low;
        public Vector high;

        public bool IsNegative() => !high.GreaterEquals(low);

        /// <summary> The componentwise difference between the <see cref="high"/> and <see cref="low"/> corners. </summary>
        public Vector size => high.Difference(low);
        public Center center => low.Sum(high).Product(0.5f);

        public bool Fits(Self other) => Fits(other.low, other.high);
        public bool Fits(Vector point) => point.GreaterEquals(low) && point.LessEquals(high);
        public bool Fits(Vector low, Vector high) => low.GreaterEquals(this.low) && high.LessEquals(this.high);

        public bool Intersects(Self other) => Intersects(other.low, other.high);
        public bool Intersects(Vector low, Vector high) => high.Greater(this.low) && low.Less(this.high);

        /// <summary>
        ///     Returns a new <typeparamref name="Self"/> that <see cref="Contains(Vector)"/> all points
        ///     that are inside <see langword="this"/> AND <paramref name="other"/>.
        /// </summary>
        /// <param name="other"> The other <typeparamref name="Self"/>. </param>
        public Self Intersect(Self other)
            => new Self()
            {
                low = low.Max(other.low),
                high = high.Min(other.high)
            };

        public bool Contains(Self other) => Contains(other.low, other.high);
        public bool Contains(Vector point) => point.Greater(low) && point.Less(high);
        public bool Contains(Vector low, Vector high) => low.Greater(this.low) && high.Less(this.high);

        /// <summary> Expands this <typeparamref name="Self"/> so the given <paramref name="point"/> is included. </summary>
        /// <param name="point"> The <typeparamref name=""/> to be included in this <typeparamref name="Self"/>. </param>
        public void Expand(Vector point)
        {
            low = low.Min(point);
            high = high.Max(point);
        }

        public void Expand(params Vector[] points)
        {
            foreach (Vector point in points)
                Expand(point);
        }

        public void Expand(Self other)
            => Expand(other.low, other.high);

        public void Expand(params Self[] others)
        {
            foreach (Self other in others)
                Expand(other);
        }

        public void Expand<ExpandVector, ExpandScalar>(params ExpandVector[] others)
            where ExpandVector : struct
                               , Vector<ExpandVector, ExpandScalar>
                               , ChaosUtil.Primitives.Convertible<Vector>
            where ExpandScalar : struct
        {
            foreach (ExpandVector other in others)
                Expand(other.Convert());
        }
    }
}
