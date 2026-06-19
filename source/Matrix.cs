using ChaosFramework.IO;
using ChaosFramework.Math.Vectors;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static ChaosFramework.Math.Trigonometry;

namespace ChaosFramework.Math
{
    // TODO: Figure out if (and where) line breaks have effect in debugger display
    [DebuggerDisplay("m00={m00}; m01={m01}; m02={m02}; m03={m03};\n"
                   + "m10={m10}; m11={m11}; m12={m12}; m13={m13};\n"
                   + "m20={m20}; m21={m21}; m22={m22}; m23={m23};\n"
                   + "m30={m30}; m31={m31}; m32={m32}; m33={m33}"
                   )]
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Matrix
    {
        /// <summary> Returns an identity matrix. </summary>
        public static readonly Matrix IDENTITY
            = new Matrix(
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);

        /// <summary> Returns a scaling Matrix that scales with <see cref="float.NaN"/>. </summary>
        public static readonly Matrix NAN = float.NaN;

        [ChaosIO.RegisterType]
        static void RegisterType() => ChaosIO.AddType(Read, Write);

        static Matrix Read(System.IO.BinaryReader reader)
            => new Matrix(
                reader.Read<float>(), reader.Read<float>(), reader.Read<float>(), reader.Read<float>(),
                reader.Read<float>(), reader.Read<float>(), reader.Read<float>(), reader.Read<float>(),
                reader.Read<float>(), reader.Read<float>(), reader.Read<float>(), reader.Read<float>(),
                reader.Read<float>(), reader.Read<float>(), reader.Read<float>(), reader.Read<float>()
                );

        static void Write(System.IO.BinaryWriter writer, Matrix mat)
        {
            writer.WriteAs(mat.m00); writer.WriteAs(mat.m01); writer.WriteAs(mat.m02); writer.WriteAs(mat.m03);
            writer.WriteAs(mat.m10); writer.WriteAs(mat.m11); writer.WriteAs(mat.m12); writer.WriteAs(mat.m13);
            writer.WriteAs(mat.m20); writer.WriteAs(mat.m21); writer.WriteAs(mat.m22); writer.WriteAs(mat.m23);
            writer.WriteAs(mat.m30); writer.WriteAs(mat.m31); writer.WriteAs(mat.m32); writer.WriteAs(mat.m33);
        }

        public float m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23, m30, m31, m32, m33;

        public Vector4f row0
        {
            get { return new Vector4f(m00, m01, m02, m03); }
            set { m00 = value.x; m01 = value.y; m02 = value.z; m03 = value.w; }
        }
        public Vector4f row1
        {
            get { return new Vector4f(m10, m11, m12, m13); }
            set { m10 = value.x; m11 = value.y; m12 = value.z; m13 = value.w; }
        }
        public Vector4f row2
        {
            get { return new Vector4f(m20, m21, m22, m23); }
            set { m20 = value.x; m21 = value.y; m22 = value.z; m23 = value.w; }
        }
        public Vector4f row3
        {
            get { return new Vector4f(m30, m31, m32, m33); }
            set { m30 = value.x; m31 = value.y; m32 = value.z; m33 = value.w; }
        }
        public Vector4f col0
        {
            get { return new Vector4f(m00, m10, m20, m30); }
            set { m00 = value.x; m10 = value.y; m20 = value.z; m30 = value.w; }
        }
        public Vector4f col1
        {
            get { return new Vector4f(m01, m11, m21, m31); }
            set { m01 = value.x; m11 = value.y; m21 = value.z; m31 = value.w; }
        }
        public Vector4f col2
        {
            get { return new Vector4f(m02, m12, m22, m32); }
            set { m02 = value.x; m12 = value.y; m22 = value.z; m32 = value.w; }
        }
        public Vector4f col3
        {
            get { return new Vector4f(m03, m13, m23, m33); }
            set { m03 = value.x; m13 = value.y; m23 = value.z; m33 = value.w; }
        }
        public Vector4f diagonal
        {
            get { return new Vector4f(m00, m11, m22, m33); }
            set { m00 = value.x; m11 = value.y; m22 = value.z; m33 = value.w; }
        }

        public Matrix(
            float m00, float m01, float m02, float m03,
            float m10, float m11, float m12, float m13,
            float m20, float m21, float m22, float m23,
            float m30, float m31, float m32, float m33
            )
        {
            this.m00 = m00; this.m01 = m01; this.m02 = m02; this.m03 = m03;
            this.m10 = m10; this.m11 = m11; this.m12 = m12; this.m13 = m13;
            this.m20 = m20; this.m21 = m21; this.m22 = m22; this.m23 = m23;
            this.m30 = m30; this.m31 = m31; this.m32 = m32; this.m33 = m33;
        }

        public Matrix(Vector4f row0, Vector4f row1, Vector4f row2, Vector4f row3)
        {
            m00 = row0.x; m01 = row0.y; m02 = row0.z; m03 = row0.w;
            m10 = row1.x; m11 = row1.y; m12 = row1.z; m13 = row1.w;
            m20 = row2.x; m21 = row2.y; m22 = row2.z; m23 = row2.w;
            m30 = row3.x; m31 = row3.y; m32 = row3.z; m33 = row3.w;
        }

        public Matrix(
            Vector3f row0xyz, float row0w,
            Vector3f row1xyz, float row1w,
            Vector3f row2xyz, float row2w,
            Vector3f row3xyz, float row3w
            )
            : this(
            row0xyz.x, row0xyz.y, row0xyz.z, row0w,
            row1xyz.x, row1xyz.y, row1xyz.z, row1w,
            row2xyz.x, row2xyz.y, row2xyz.z, row2w,
            row3xyz.x, row3xyz.y, row3xyz.z, row3w)
        { }

        public bool IsNaN() => row0.IsNaN() || row1.IsNaN() || row2.IsNaN() || row3.IsNaN();

        public float determinant4x4
            => m30 * (m21 * (m03 * m12 - m02 * m13) - m22 * (m03 * m11 - m01 * m13) + m23 * (m02 * m11 - m01 * m12))
             - m31 * (m20 * (m03 * m12 - m02 * m13) - m22 * (m03 * m10 - m00 * m13) + m23 * (m02 * m10 - m00 * m12))
             + m32 * (m20 * (m03 * m11 - m01 * m13) - m21 * (m03 * m10 - m00 * m13) + m23 * (m01 * m10 - m00 * m11))
             - m33 * (m20 * (m02 * m11 - m01 * m12) - m21 * (m02 * m10 - m00 * m12) + m22 * (m01 * m10 - m00 * m11));

        public float determinant
            => m00 * (m11 * m22 - m12 * m21)
             + m01 * (m12 * m20 - m10 * m22)
             + m02 * (m10 * m21 - m11 * m20);

        public static Matrix Invert(Matrix m)
            => Matrix4x4.Invert(Unsafe.As<Matrix, Matrix4x4>(ref m), out Matrix4x4 result)
                ? Unsafe.As<Matrix4x4, Matrix>(ref result)
                : NAN;

        public static Matrix Transpose(Matrix m)
            => Unsafe.As<Matrix4x4, Matrix>(ref Unsafe.AsRef(Matrix4x4.Transpose(Unsafe.As<Matrix, Matrix4x4>(ref m))));

        public static Matrix RotationX(float angle) => Unsafe.As<Matrix4x4, Matrix>(ref Unsafe.AsRef(Matrix4x4.CreateRotationX(angle)));
        public static Matrix RotationY(float angle) => Unsafe.As<Matrix4x4, Matrix>(ref Unsafe.AsRef(Matrix4x4.CreateRotationY(angle)));
        public static Matrix RotationZ(float angle) => Unsafe.As<Matrix4x4, Matrix>(ref Unsafe.AsRef(Matrix4x4.CreateRotationZ(angle)));
        public static Matrix RotationAxis(Vector3f axis, float angle)
            => Unsafe.As<Matrix4x4, Matrix>(
                ref Unsafe.AsRef(Matrix4x4.CreateFromAxisAngle(Unsafe.As<Vector3f, Vector3>(ref axis), angle))
                );

        public static Matrix RotationQuaternion(Quaternion q)
            => Unsafe.As<Matrix4x4, Matrix>(
                ref Unsafe.AsRef(Matrix4x4.CreateFromQuaternion(Unsafe.As<Quaternion, System.Numerics.Quaternion>(ref q)))
                );

        public static Matrix RotationYawPitchRoll(float yaw, float pitch, float roll)
            => RotationZ(roll) * RotationX(pitch) * RotationY(yaw);

        public static Matrix Scaling(float scale) => Scaling(scale, scale, scale);
        public static Matrix Scaling(Vector2f xy, float z) => Scaling(xy.x, xy.y, z);
        public static Matrix Scaling(float x, Vector2f yz) => Scaling(x, yz.x, yz.y);
        public static Matrix Scaling(Vector3f scale) => Scaling(scale.x, scale.y, scale.z);
        public static Matrix Scaling(float x, float y, float z) => new Matrix { m00 = x, m11 = y, m22 = z, m33 = 1 };
        public static Matrix Translation(Vector3f v) => Translation(v.x, v.y, v.z);
        public static Matrix Translation(Vector2f xy, float z = 0) => Translation(xy.x, xy.y, z);
        public static Matrix Translation(float x, Vector2f yz) => Translation(x, yz.x, yz.y);
        public static Matrix Translation(float x, float y, float z = 0)
            => new Matrix { m00 = 1, m11 = 1, m22 = 1, m33 = 1, m30 = x, m31 = y, m32 = z };

        public static Matrix LookAtLH(Vector3f pos, Vector3f target, Vector3f up)
        {
            Vector3f zaxis = Vector3f.Normalize(target - pos);
            Vector3f xaxis = Vector3f.Normalize(Vector3f.Cross(up, zaxis));
            Vector3f yaxis = Vector3f.Cross(zaxis, xaxis);
            return new Matrix(
                xaxis.x, yaxis.x, zaxis.x, 0,
                xaxis.y, yaxis.y, zaxis.y, 0,
                xaxis.z, yaxis.z, zaxis.z, 0,
                -Vector3f.Dot(xaxis, pos), -Vector3f.Dot(yaxis, pos), -Vector3f.Dot(zaxis, pos), 1
                );
        }

        public static Matrix PerspectiveFovLH(float fovY, float ratio, float zNear, float zFar)
        {
            float h = 1.0f / Tan(fovY * 0.5f);
            float w = h / ratio;
            float d = zFar - zNear;
            return new Matrix()
            {
                m00 = w,
                m11 = h,
                m22 = zFar / d,
                m32 = -zNear * zFar / d,
                m23 = 1,
                m33 = 0
            };
        }

        public static Matrix LocalSpaceNormalized(Vector3f localY, Vector3f localZ)
        {
            localY.Normalize();
            Vector3f localX = Vector3f.Cross(localZ, localY);
            localX.Normalize();
            localZ = Vector3f.Cross(localX, localY);
            return LocalSpace(localX, localY, localZ);
        }

        public static Matrix LocalSpace(Vector3f localY, Vector3f localZ)
            => LocalSpace(Vector3f.Cross(localZ, localY), localY, localZ);

        public static Matrix LocalSpace(Vector3f localX, Vector3f localY, Vector3f localZ)
            => new Matrix(
                localX, 0,
                localY, 0,
                localZ, 0,
                0, 1);

        public static Matrix operator -(Matrix m)
            => new Matrix(
                -m.m00, -m.m01, -m.m02, -m.m03,
                -m.m10, -m.m11, -m.m12, -m.m13,
                -m.m20, -m.m21, -m.m22, -m.m23,
                -m.m30, -m.m31, -m.m32, -m.m33);

        public static Matrix operator +(Matrix a, Matrix b)
            => new Matrix(
                a.m00 + b.m00, a.m01 + b.m01, a.m02 + b.m02, a.m03 + b.m03,
                a.m10 + b.m10, a.m11 + b.m11, a.m12 + b.m12, a.m13 + b.m13,
                a.m20 + b.m20, a.m21 + b.m21, a.m22 + b.m22, a.m23 + b.m23,
                a.m30 + b.m30, a.m31 + b.m31, a.m32 + b.m32, a.m33 + b.m33);

        public static Matrix operator -(Matrix a, Matrix b)
            => new Matrix(
                a.m00 - b.m00, a.m01 - b.m01, a.m02 - b.m02, a.m03 - b.m03,
                a.m10 - b.m10, a.m11 - b.m11, a.m12 - b.m12, a.m13 - b.m13,
                a.m20 - b.m20, a.m21 - b.m21, a.m22 - b.m22, a.m23 - b.m23,
                a.m30 - b.m30, a.m31 - b.m31, a.m32 - b.m32, a.m33 - b.m33);

        public static Matrix operator *(float f, Matrix m) => m * f;
        public static Matrix operator *(Matrix m, float f)
            => new Matrix(
                m.m00 * f, m.m01 * f, m.m02 * f, m.m03 * f,
                m.m10 * f, m.m11 * f, m.m12 * f, m.m13 * f,
                m.m20 * f, m.m21 * f, m.m22 * f, m.m23 * f,
                m.m30 * f, m.m31 * f, m.m32 * f, m.m33 * f);

        public static Matrix operator *(Matrix a, Matrix b)
            => new Matrix(
                a.m00 * b.m00 + a.m01 * b.m10 + a.m02 * b.m20 + a.m03 * b.m30,
                a.m00 * b.m01 + a.m01 * b.m11 + a.m02 * b.m21 + a.m03 * b.m31,
                a.m00 * b.m02 + a.m01 * b.m12 + a.m02 * b.m22 + a.m03 * b.m32,
                a.m00 * b.m03 + a.m01 * b.m13 + a.m02 * b.m23 + a.m03 * b.m33,

                a.m10 * b.m00 + a.m11 * b.m10 + a.m12 * b.m20 + a.m13 * b.m30,
                a.m10 * b.m01 + a.m11 * b.m11 + a.m12 * b.m21 + a.m13 * b.m31,
                a.m10 * b.m02 + a.m11 * b.m12 + a.m12 * b.m22 + a.m13 * b.m32,
                a.m10 * b.m03 + a.m11 * b.m13 + a.m12 * b.m23 + a.m13 * b.m33,

                a.m20 * b.m00 + a.m21 * b.m10 + a.m22 * b.m20 + a.m23 * b.m30,
                a.m20 * b.m01 + a.m21 * b.m11 + a.m22 * b.m21 + a.m23 * b.m31,
                a.m20 * b.m02 + a.m21 * b.m12 + a.m22 * b.m22 + a.m23 * b.m32,
                a.m20 * b.m03 + a.m21 * b.m13 + a.m22 * b.m23 + a.m23 * b.m33,

                a.m30 * b.m00 + a.m31 * b.m10 + a.m32 * b.m20 + a.m33 * b.m30,
                a.m30 * b.m01 + a.m31 * b.m11 + a.m32 * b.m21 + a.m33 * b.m31,
                a.m30 * b.m02 + a.m31 * b.m12 + a.m32 * b.m22 + a.m33 * b.m32,
                a.m30 * b.m03 + a.m31 * b.m13 + a.m32 * b.m23 + a.m33 * b.m33
                );

        public static Matrix operator /(Matrix m, float f) => m * (1 / f);
        public static Matrix operator /(float f, Matrix m)
            => new Matrix(
                f / m.m00, f / m.m01, f / m.m02, f / m.m03,
                f / m.m10, f / m.m11, f / m.m12, f / m.m13,
                f / m.m20, f / m.m21, f / m.m22, f / m.m23,
                f / m.m30, f / m.m31, f / m.m32, f / m.m33);

        public static Matrix operator /(Matrix a, Matrix b) => a * Invert(b);

        public static bool operator ==(Matrix a, Matrix b)
            => a.m00 == b.m00 && a.m01 == b.m01 && a.m02 == b.m02 && a.m03 == b.m03
            && a.m10 == b.m10 && a.m11 == b.m11 && a.m12 == b.m12 && a.m13 == b.m13
            && a.m20 == b.m20 && a.m21 == b.m21 && a.m22 == b.m22 && a.m23 == b.m23
            && a.m30 == b.m30 && a.m31 == b.m31 && a.m32 == b.m32 && a.m33 == b.m33;

        public static bool operator !=(Matrix a, Matrix b)
            => a.m00 != b.m00 || a.m01 != b.m01 || a.m02 != b.m02 || a.m03 != b.m03
            || a.m10 != b.m10 || a.m11 != b.m11 || a.m12 != b.m12 || a.m13 != b.m13
            || a.m20 != b.m20 || a.m21 != b.m21 || a.m22 != b.m22 || a.m23 != b.m23
            || a.m30 != b.m30 || a.m31 != b.m31 || a.m32 != b.m32 || a.m33 != b.m33;

        public override bool Equals(object other) => other is Matrix && Equals((Matrix)other);
        public bool Equals(Matrix other) => this == other;
        public override int GetHashCode() { float det = determinant; return *(int*)&det; }

        public static implicit operator Matrix(float f) => Scaling(f);
    }
}
