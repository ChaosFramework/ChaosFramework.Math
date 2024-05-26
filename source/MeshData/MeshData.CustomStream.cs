using System;

namespace ChaosFramework.Math
{
    partial class MeshData
    {
        public abstract class CustomStream
        {
            public readonly string[] semantics;

            public CustomStream(params string[] semantics)
            {
                this.semantics = semantics;
            }

            public abstract Array GetElements();
            public abstract Type ElementType();
        }

        public abstract class CustomStream<T> : CustomStream
            where T : struct
        {
            public CustomStream(params string[] semantics)
                : base(semantics)
            { }

            public override Type ElementType() => typeof(T);
        }
    }
}
