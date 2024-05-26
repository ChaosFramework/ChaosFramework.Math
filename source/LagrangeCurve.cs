using ChaosFramework.Math.Vectors;
using System;

namespace ChaosFramework.Math
{
    public abstract class LagrangeCurve<T>
    {
        readonly T[] verts;
        readonly float[] t;

        public LagrangeCurve(params T[] verts)
        {
            this.verts = verts;
            t = new float[verts.Length];
            for (int i = 0; i < t.Length; i++)
                t[i] = (float)i / (t.Length - 1);
        }

        protected abstract T Add(T v1, T v2);
        protected abstract T Mul(float f, T v);

        public T Evaluate(float x)
        {
            T value = default(T);
            for (int i = 0; i < verts.Length; i++)
            {
                float lagrangeBasis = 1.0f;
                for (int j = 0; j < verts.Length; j++)
                    if (i != j)
                        lagrangeBasis *= (x - t[j]) / (t[i] - t[j]);

                value = Add(value, Mul(lagrangeBasis, verts[i]));
            }

            return value;
        }
    }

    public class LagrangeCurveT<T> : LagrangeCurve<T>
    {
        readonly Func<T, T, T> add;
        readonly Func<float, T, T> mul;

        public LagrangeCurveT(Func<T, T, T> add, Func<float, T, T> mul, params T[] verts)
            : base(verts)
        {
            this.add = add;
            this.mul = mul;
        }

        protected override T Add(T v1, T v2) => add(v1, v2);
        protected override T Mul(float f, T v) => mul(f, v);
    }

    public class LagrangeCurve3 : LagrangeCurve<Vector3f>
    {
        public LagrangeCurve3(params Vector3f[] verts) : base(verts) { }
        protected override Vector3f Add(Vector3f v1, Vector3f v2) => v1 + v2;
        protected override Vector3f Mul(float f, Vector3f v2) => f * v2;
    }

    public class LagrangeCurve4 : LagrangeCurve<Vector4f>
    {
        public LagrangeCurve4(params Vector4f[] verts) : base(verts) { }
        protected override Vector4f Add(Vector4f v1, Vector4f v2) => v1 + v2;
        protected override Vector4f Mul(float f, Vector4f v2) => f * v2;
    }
}
