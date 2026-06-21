using ChaosFramework.IO;
using ChaosFramework.Math.Vectors;
using System.Diagnostics;
using static ChaosFramework.Math.Exponentials;
using static ChaosFramework.Math.Trigonometry;

namespace ChaosFramework.Math
{
    [DebuggerDisplay("x={x}; y={y}; z={z}; w={w}")]
    public struct Quaternion
    {
        public static readonly Quaternion IDENTITY = new Quaternion(0, 0, 0, 1);

        [ChaosIO.RegisterType]
        static void RegisterIO() => ChaosIO.AddType(Read, Write);

        static Quaternion Read(System.IO.BinaryReader reader)
        {
            float w = reader.Read<float>();
            return new Quaternion(reader.Read<Vector3f>(), w);
        }

        static void Write(System.IO.BinaryWriter writer, Quaternion v)
        {
            writer.Write(v.w);
            writer.Write(v.x);
            writer.Write(v.y);
            writer.Write(v.z);
        }

        public static Quaternion Normalize(Quaternion q) { q.Normalize(); return q; }

        public static Quaternion Invert(Quaternion q) { q.Invert(); return q; }

        public static Quaternion Conjugate(Quaternion q) => new Quaternion(-q.xyz, q.w);

        public static Quaternion FromAxisAngle(Vector3f axis, float angle)
        {
            if (axis.LengthSq() == 0.0f)
                return IDENTITY;

            Quaternion result = IDENTITY;

            angle *= 0.5f;
            axis.Normalize();
            result.xyz = axis * Sin(angle);
            result.w = Cos(angle);

            return Normalize(result);
        }

        public static Quaternion FromYawPitchRoll(float yaw, float pitch, float roll)
        {
            yaw *= 0.5f;
            pitch *= 0.5f;
            roll *= 0.5f;

            float cosy = Cos(yaw);
            float siny = Sin(yaw);
            float cosp = Cos(pitch);
            float sinp = Sin(pitch);
            float cosr = Cos(roll);
            float sinr = Sin(roll);

            return new Quaternion(
                cosr * sinp * cosy + sinr * cosp * siny,
                cosr * cosp * siny - sinr * sinp * cosy,
                sinr * cosp * cosy - cosr * sinp * siny,
                cosr * cosp * cosy + sinr * sinp * siny
            );
        }

        public static Quaternion Slerp(Quaternion q1, Quaternion q2, float blend)
        {
            if (q1.lengthSq == 0)
                if (q2.lengthSq == 0)
                    return IDENTITY; // both axis are 0
                else
                    return q2; // axis 1 is 0
            else if (q2.lengthSq == 0)
                return q1; // axis 2 is 0

            float cos = q1.w * q2.w + Vector3f.Dot(q1.xyz, q2.xyz);
            if (cos >= 1.0f || cos <= -1.0f)
                return q1; // axis are parallel
            else if (cos < 0.0f)
            {
                q2.xyz = -q2.xyz;
                q2.w = -q2.w;
                cos = -cos;
            }

            float f1, f2;
            if (cos < 0.99f) // spherical lerp for legit angles
            {
                float angle = ACos(cos);
                float sin = Sin(angle);
                float invSin = 1 / sin;
                f1 = Sin(angle * (1 - blend)) * invSin;
                f2 = Sin(angle * blend) * invSin;
            }
            else // lerp for small angles to avoid div/0
            {
                f1 = 1.0f - blend;
                f2 = blend;
            }

            Quaternion result = new Quaternion(f1 * q1.xyz + f2 * q2.xyz, f1 * q1.w + f2 * q2.w);
            if (result.lengthSq > 0.0f)
                return Normalize(result);
            else
                return IDENTITY;
        }

        public Vector3f xyz;
        public float w;
        public float x { get { return xyz.x; } set { xyz.x = value; } }
        public float y { get { return xyz.y; } set { xyz.y = value; } }
        public float z { get { return xyz.z; } set { xyz.z = value; } }
        public float length => Sqrt(lengthSq);
        public float lengthSq => xyz.LengthSq() + w * w;

        public Quaternion(Vector3f v, float w) { xyz = v; this.w = w; }
        public Quaternion(float x, float y, float z, float w) : this(new Vector3f(x, y, z), w) { }

        public Vector4f GetAxisAngle()
        {
            Quaternion q = this;
            if (System.Math.Abs(q.w) > 1)
                q.Normalize();

            Vector4f axisAngle = new Vector4f();
            axisAngle.w = 2f * ACos(q.w);
            float d = Sqrt(1.0f - q.w * q.w);
            axisAngle.xyz = (d > 0.0001f) ? q.xyz / d : axisAngle.xyz = new Vector3f(1, 0, 0);
            return axisAngle;
        }

        public void Invert()
        {
            float lenSq = lengthSq;
            if (lenSq != 0)
            {
                float invLenSq = 1f / lenSq;
                xyz *= -invLenSq;
                w *= invLenSq;
            }
        }

        public void Normalize(float newLength = 1)
        {
            float f = newLength / length;
            xyz *= f; w *= f;
        }

        public static Quaternion operator +(Quaternion left, Quaternion right)
            => new Quaternion(left.xyz + right.xyz, left.w + right.w);

        public static Quaternion operator -(Quaternion left, Quaternion right)
            => new Quaternion(left.xyz - right.xyz, left.w - right.w);

        public static Quaternion operator *(Quaternion left, Quaternion right) =>
            new Quaternion(
                left.w * right.x + left.x * right.w + left.y * right.z - left.z * right.y,
                left.w * right.y - left.x * right.z + left.y * right.w + left.z * right.x,
                left.w * right.z + left.x * right.y - left.y * right.x + left.z * right.w,
                left.w * right.w - left.x * right.x - left.y * right.y - left.z * right.z
            );

        public static Quaternion operator *(Quaternion quaternion, float scale)
            => new Quaternion(quaternion.xyz * scale, quaternion.w * scale);

        public override bool Equals(object obj) => obj is Quaternion ? Equals((Quaternion)obj) : false;
        public bool Equals(Quaternion compare) => this == compare;
        public static bool operator ==(Quaternion a, Quaternion b) => a.xyz == b.xyz && a.w == b.w;
        public static bool operator !=(Quaternion a, Quaternion b) => a.xyz != b.xyz || a.w != b.w;
        public override int GetHashCode() => xyz.GetHashCode() ^ w.GetHashCode();
    }
}
