using ChaosFramework.Math.Vectors;
using ChaosUtil.Primitives;
using System;
using static ChaosFramework.Math.Clamping;
using static ChaosFramework.Math.Exponentials;
using static ChaosFramework.Math.Signs;

namespace ChaosFramework.Math
{
    public partial class MeshData
    {
        static readonly Vector3f X_AXIS = new Vector3f(1, 0, 0);
        static readonly Vector3f Z_AXIS = new Vector3f(0, 0, 1);

        public static MeshData sprite
            => new MeshData(
                new[] {
                    new Vector3f(-0.5f, 0.5f, 0),
                    new Vector3f(0.5f, 0.5f, 0),
                    new Vector3f(-0.5f, -0.5f, 0),
                    new Vector3f(0.5f, -0.5f, 0)
                },
                new[] {
                    new Vector3f(0, 0, -1),
                    new Vector3f(0, 0, -1),
                    new Vector3f(0, 0, -1),
                    new Vector3f(0, 0, -1)
                },
                new[] {
                    new Vector4f(1, 0, 0, 1),
                    new Vector4f(1, 0, 0, 1),
                    new Vector4f(1, 0, 0, 1),
                    new Vector4f(1, 0, 0, 1)
                },
                new[] {
                    new[] {
                        new Vector2f(0, 0),
                        new Vector2f(1, 0),
                        new Vector2f(0, 1),
                        new Vector2f(1, 1)
                    }
                },
                new uint[] { 0, 1, 2, 2, 1, 3 }
            );

        public static Vector4f[] ComputeTangents(
            uint[] inds,
            Vector3f[] pos,
            Vector2f[] tex,
            Vector3f[] nor
            )
        {
            Vector4f[] tan = new Vector4f[pos.Length];
            Vector3f[] tan1 = new Vector3f[pos.Length];
            Vector3f[] tan2 = new Vector3f[pos.Length];

            for (int i = 0; i < inds.Length; i += 3)
            {
                uint i0 = inds[i],
                     i1 = inds[i + 1],
                     i2 = inds[i + 2];

                Vector3f pos0 = pos[i0],
                         pos1 = pos[i1],
                         pos2 = pos[i2];
                Vector2f tex0 = tex[i0],
                         tex1 = tex[i1],
                         tex2 = tex[i2];

                Vector3f edge0_pos = pos1 - pos0,
                         edge1_pos = pos2 - pos0;
                Vector2f edge0_tex = tex1 - tex0,
                         edge1_tex = tex2 - tex0;

                float r = 1 / (edge0_tex.x * edge1_tex.y - edge1_tex.x * edge0_tex.y);
                if (!float.IsInfinity(r))
                {
                    Vector3f t1 = r * (edge1_tex.y * edge0_pos - edge0_tex.y * edge1_pos);
                    tan1[i0] += t1;
                    tan1[i1] += t1;
                    tan1[i2] += t1;

                    Vector3f t2 = r * (edge0_tex.x * edge1_pos - edge1_tex.x * edge0_pos);
                    tan2[i0] += t2;
                    tan2[i1] += t2;
                    tan2[i2] += t2;
                }
            }

            for (int i = 0; i < tan.Length; i++)
            {
                Vector3f n = nor[i];
                Vector3f t1 = tan1[i];

                Vector3f t = t1 - n * Vector3f.Dot(n, t1);
                if (t.LengthSq() == 0)
                    t = nor[i] == X_AXIS
                        ? Z_AXIS
                        : Vector3f.Cross(nor[i], X_AXIS);

                float dot = Vector3f.Dot(Vector3f.Cross(n, t1), tan2[i]);
                tan[i] = new Vector4f(Vector3f.Normalize(t), dot < 0 ? -1 : 1);
            }

            return tan;
        }

        public static MeshData Assemble(MeshData[] meshes, Matrix[] transforms, Vector2f[] texCoordScale)
        {
            int numTotalVerts = 0;
            int numTexCoordPairs = 0;
            int numTotalInds = 0;

            foreach (MeshData m in meshes)
            {
                numTotalVerts += m.vertexCount;
                numTexCoordPairs = Max(m.tex.Length, numTexCoordPairs);
                numTotalInds += m.ind.Length;
            }

            Vector3f[] pos = new Vector3f[numTotalVerts],
                       nor = new Vector3f[numTotalVerts];
            Vector4f[] tan = new Vector4f[numTotalVerts];
            Vector2f[][] tex = new Vector2f[numTexCoordPairs][];
            uint[] inds = new uint[numTotalInds];
            for (int i = 0; i < numTexCoordPairs; i++)
                tex[i] = new Vector2f[numTotalVerts];

            uint vertexOffset = 0,
                 indexOffset = 0;

            for (int meshID = 0; meshID < meshes.Length; meshID++)
            {
                MeshData m = meshes[meshID];
                for (int i = 0; i < m.ind.Length; i++)
                    inds[indexOffset++] = m.ind[i] + vertexOffset;

                Vector2f texScale = meshID < texCoordScale.Length
                                    ? texCoordScale[meshID]
                                    : new Vector2f(1, 1);

                bool untransformed = meshID >= transforms.Length;
                Vector3f[] transformedPos = untransformed
                                            ? m.pos
                                            : Vector3f.TransformCoordinate(m.pos, transforms[meshID]);
                Vector3f[] transformedNor = untransformed
                                            ? m.nor
                                            : Vector3f.TransformNormal(m.nor, transforms[meshID]);

                Vector4f[] transformedTan = m.tan;
                if (!untransformed)
                {
                    Matrix tanTransform = transforms[meshID];
                    tanTransform.m30 = tanTransform.m31 = tanTransform.m32 = 0;
                    transformedTan = Vector4f.Transform(transformedTan, tanTransform);
                }

                Array.Copy(transformedPos, 0, pos, vertexOffset, m.vertexCount);
                Array.Copy(transformedNor, 0, nor, vertexOffset, m.vertexCount);
                Array.Copy(transformedTan, 0, tan, vertexOffset, m.vertexCount);
                for (int i = 0; i < m.vertexCount; i++, vertexOffset++)
                    for (int k = 0; k < m.tex.Length; k++)
                        tex[k][vertexOffset] = Vector2f.ComponentWiseMul(m.tex[k][i], texScale);
            }

            return new MeshData(pos, nor, tan, tex, inds);
        }

        public byte flags;
        public Vector3f[] pos, nor;
        public Vector4f[] tan;
        public Vector2f[][] tex;
        public uint[] ind;
        public CustomStream[] customData;

        public int numTexCoordPairs => tex == null ? 0 : tex.GetLength(0);
        public int faceCount => ind == null ? 0 : ind.Length / 3;
        public int vertexCount => pos == null ? 0 : pos.Length;

        public float hullRadius { get; private set; } = -1;
        public Bounds3f bounds { get; private set; }

        public MeshData(Vector3f[] pos, Vector3f[] nor, Vector4f[] tan, Vector2f[][] tex, uint[] inds)
            : this(0, pos, nor, tan, tex, inds)
        { }

        public MeshData(Vector3f[] pos, Vector3f[] nor, Vector2f[][] tex, uint[] inds)
            : this(0, pos, nor, ComputeTangents(inds, pos, tex[0], nor), tex, inds)
        { }

        public MeshData(Vector3f[] pos, Vector3f[] nor, Vector2f[] tex, uint[] inds)
            : this(0, pos, nor, ComputeTangents(inds, pos, tex, nor), new Vector2f[][] { tex }, inds)
        { }

        public MeshData(
            byte flags,
            Vector3f[] pos,
            Vector3f[] nor,
            Vector4f[] tan,
            Vector2f[][] tex,
            uint[] ind,
            params CustomStream[] custom
            )
        {
            this.flags = flags;
            this.pos = pos;
            this.nor = nor;
            this.tan = tan;
            this.tex = tex;
            this.ind = ind;

            System.Collections.Generic.HashSet<string> streamSemantics = new System.Collections.Generic.HashSet<string>();
            foreach (CustomStream stream in custom)
                foreach (string semantic in stream.semantics)
                    if (!streamSemantics.Add(semantic))
                        throw new ArgumentException("Semantics must be unique.");

            customData = custom;
            ComputeHullRadius();
        }

        public void ComputeHullRadius()
        {
            hullRadius = 0;
            bounds = new Bounds3f();
            foreach (Vector3f p in pos)
            {
                hullRadius = Max(hullRadius, p.LengthSq());
                bounds.Expand(p);
            }
            hullRadius = Sqrt(hullRadius);
        }

        public void NormalizeCube(float borderLength = 0.5f)
        {
            float invMax = borderLength / Max(
                Abs(bounds.high.x),
                Abs(bounds.high.y),
                Abs(bounds.high.z),
                Abs(bounds.low.x),
                Abs(bounds.low.y),
                Abs(bounds.low.z)
                );

            for (int i = 0; i < pos.Length; i++)
                pos[i] *= invMax;

            ComputeHullRadius();
        }

        public void NormalizeSphere(float radius = 0.5f)
        {
            float lenSq = 0;
            foreach (Vector3f v in pos)
                lenSq = Max(lenSq, v.LengthSq());
            float invMax = radius / Sqrt(lenSq);
            for (int i = 0; i < pos.Length; i++)
                pos[i] *= invMax;
            ComputeHullRadius();
        }

        public override int GetHashCode() => vertexCount ^ faceCount ^ flags;

        public override bool Equals(object obj) => Equals(obj as MeshData);

        public bool Equals(MeshData compare)
            => compare != null
            && flags == compare.flags
            && Array<Vector3f>.ValueEquals(pos, compare.pos)
            && Array<Vector3f>.ValueEquals(nor, compare.nor)
            && Array<Vector4f>.ValueEquals(tan, compare.tan)
            && Array<Vector2f>.ValueEquals(tex, compare.tex)
            && Array<uint>.ValueEquals(ind, compare.ind)
            ;

        public static bool operator ==(MeshData a, MeshData b)
            => ((object)a == null ^ (object)b == null) ? false : ((object)a == null ? (object)b == null : a.Equals(b));

        public static bool operator !=(MeshData a, MeshData b) => !(a == b);
    }
}
