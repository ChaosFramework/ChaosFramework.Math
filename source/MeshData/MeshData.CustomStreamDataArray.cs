using System;

namespace ChaosFramework.Math
{
    public partial class MeshData
    {
        public class CustomStreamDataArray<T> : CustomStream<T>
            where T : struct
        {
            public T[] data;

            public CustomStreamDataArray(params string[] semantics)
                : this(null, semantics)
            { }

            public CustomStreamDataArray(T[] data, params string[] semantics)
                : base(semantics)
            {
                this.data = data;
            }

            public override Array GetElements() => data;
        }
    }
}
