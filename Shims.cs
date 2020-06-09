// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using MessagePack;

#pragma warning disable SA1307 // Field should begin with upper-case letter
#pragma warning disable SA1300 // Field should begin with upper-case letter
#pragma warning disable IDE1006 // Field should begin with upper-case letter
#pragma warning disable SA1649 // type name matches file name
#pragma warning disable SA1401 // Fields should be private (we need fields rather than auto-properties for .NET Native compilation to work).

namespace UnityEngine
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    [Flags]
    public enum RigidbodyConstraints2D
    {
        /// <summary>
        ///   <para>No constraints.</para>
        /// </summary>
        None = 0,
        /// <summary>
        ///   <para>Freeze motion along the X-axis.</para>
        /// </summary>
        FreezePositionX = 1,
        /// <summary>
        ///   <para>Freeze motion along the Y-axis.</para>
        /// </summary>
        FreezePositionY = 2,
        /// <summary>
        ///   <para>Freeze rotation along the Z-axis.</para>
        /// </summary>
        FreezeRotation = 4,
        /// <summary>
        ///   <para>Freeze motion along the X-axis and Y-axis.</para>
        /// </summary>
        FreezePosition = FreezePositionY | FreezePositionX, // 0x00000003
        /// <summary>
        ///   <para>Freeze rotation and motion along all axes.</para>
        /// </summary>
        FreezeAll = FreezePosition | FreezeRotation, // 0x00000007
    }
    /// <summary>
    ///   <para>Controls how collisions are detected when a Rigidbody2D moves.</para>
    /// </summary>
    public enum CollisionDetectionMode2D
    {
        /// <summary>
        ///   <para>When a Rigidbody2D moves, only collisions at the new position are detected.</para>
        /// </summary>
        Discrete = 0,
        /// <summary>
        ///   <para>This mode is obsolete.  You should use Discrete mode.</para>
        /// </summary>
        [Obsolete("Enum member CollisionDetectionMode2D.None has been deprecated. Use CollisionDetectionMode2D.Discrete instead (UnityUpgradable) -> Discrete", true)] 
        None = 0,
        /// <summary>
        ///   <para>Ensures that all collisions are detected when a Rigidbody2D moves.</para>
        /// </summary>
        Continuous = 1,
    }
    public enum RigidbodyInterpolation2D
    {
        /// <summary>
        ///   <para>Do not apply any smoothing to the object's movement.</para>
        /// </summary>
        None,
        /// <summary>
        ///   <para>Smooth movement based on the object's positions in previous frames.</para>
        /// </summary>
        Interpolate,
        /// <summary>
        ///   <para>Smooth an object's movement based on an estimate of its position in the next frame.</para>
        /// </summary>
        Extrapolate,
    }

    [MessagePackObject]
    public struct Vector2
    {
        [Key(0)] public float x;
        [Key(1)] public float y;

        [SerializationConstructor]
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        private static readonly Vector2 zeroVector = new Vector2(0.0f, 0.0f);
        private static readonly Vector2 oneVector = new Vector2(1f, 1f);
        private static readonly Vector2 upVector = new Vector2(0.0f, 1f);
        private static readonly Vector2 downVector = new Vector2(0.0f, -1f);
        private static readonly Vector2 leftVector = new Vector2(-1f, 0.0f);
        private static readonly Vector2 rightVector = new Vector2(1f, 0.0f);

        private static readonly Vector2 positiveInfinityVector =
            new Vector2(float.PositiveInfinity, float.PositiveInfinity);

        private static readonly Vector2 negativeInfinityVector =
            new Vector2(float.NegativeInfinity, float.NegativeInfinity);

        public const float kEpsilon = 1E-05f;
        public const float kEpsilonNormalSqrt = 1E-15f;

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.x;
                    case 1:
                        return this.y;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector2 index!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        this.x = value;
                        break;
                    case 1:
                        this.y = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector2 index!");
                }
            }
        }

        public void Set(float newX, float newY)
        {
            this.x = newX;
            this.y = newY;
        }
        /// <summary>
        ///   <para>Multiplies two vectors component-wise.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        [MethodImpl((MethodImplOptions)256)]
        public static Vector2 Scale(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x * b.x, a.y * b.y);
        }

        /// <summary>
        ///   <para>Multiplies every component of this vector by the same component of scale.</para>
        /// </summary>
        /// <param name="scale"></param>
        [MethodImpl((MethodImplOptions)256)]
        public void Scale(Vector2 scale)
        {
            this.x *= scale.x;
            this.y *= scale.y;
        }

        /// <summary>
        ///   <para>Returns a formatted string for this vector.</para>
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <param name="formatProvider">An object that specifies culture-specific formatting.</param>
        public override string ToString()
        {
            return this.ToString((string)null, (IFormatProvider)CultureInfo.InvariantCulture.NumberFormat);
        }

        /// <summary>
        ///   <para>Returns a formatted string for this vector.</para>
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <param name="formatProvider">An object that specifies culture-specific formatting.</param>
        public string ToString(string format)
        {
            return this.ToString(format, (IFormatProvider)CultureInfo.InvariantCulture.NumberFormat);
        }

        /// <summary>
        ///   <para>Returns a formatted string for this vector.</para>
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <param name="formatProvider">An object that specifies culture-specific formatting.</param>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format))
                format = "F1";
            return String.Format("({0}, {1})", (object)this.x.ToString(format, formatProvider), (object)this.y.ToString(format, formatProvider));
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2;
        }

        /// <summary>
        ///   <para>Returns true if the given vector is exactly equal to this vector.</para>
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is Vector2 other1 && this.Equals(other1);
        }

        public bool Equals(Vector2 other)
        {
            return (double)this.x == (double)other.x && (double)this.y == (double)other.y;
        }

    }

    [MessagePackObject]
    public struct Vector3
    {
        [Key(0)]
        public float x;
        [Key(1)]
        public float y;
        [Key(2)]
        public float z;

        [SerializationConstructor]
        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Vector3 operator *(Vector3 a, float d)
        {
            return new Vector3(a.x * d, a.y * d, a.z * d);
        }
        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2;
        }

        /// <summary>
        ///   <para>Returns true if the given vector is exactly equal to this vector.</para>
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is Vector3 other1 && this.Equals(other1);
        }

        public bool Equals(Vector3 other)
        {
            return (double)this.x == (double)other.x && (double)this.y == (double)other.y && (double)this.z == (double)other.z;
        }
        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Vector3 operator -(Vector3 a)
        {
            return new Vector3(-a.x, -a.y, -a.z);
        }

        public static Vector3 operator *(float d, Vector3 a)
        {
            return new Vector3(a.x * d, a.y * d, a.z * d);
        }

        public static Vector3 operator /(Vector3 a, float d)
        {
            return new Vector3(a.x / d, a.y / d, a.z / d);
        }

        public static bool operator ==(Vector3 lhs, Vector3 rhs)
        {
            float num1 = lhs.x - rhs.x;
            float num2 = lhs.y - rhs.y;
            float num3 = lhs.z - rhs.z;
            return (double)num1 * (double)num1 + (double)num2 * (double)num2 + (double)num3 * (double)num3 < 9.99999943962493E-11;
        }

        public static bool operator !=(Vector3 lhs, Vector3 rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        ///   <para>Returns a formatted string for this vector.</para>
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <param name="formatProvider">An object that specifies culture-specific formatting.</param>
        public override string ToString()
        {
            return this.ToString((string)null, (IFormatProvider)CultureInfo.InvariantCulture.NumberFormat);
        }

        /// <summary>
        ///   <para>Returns a formatted string for this vector.</para>
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <param name="formatProvider">An object that specifies culture-specific formatting.</param>
        public string ToString(string format)
        {
            return this.ToString(format, (IFormatProvider)CultureInfo.InvariantCulture.NumberFormat);
        }

        /// <summary>
        ///   <para>Returns a formatted string for this vector.</para>
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <param name="formatProvider">An object that specifies culture-specific formatting.</param>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format))
                format = "F1";
            return String.Format("({0}, {1}, {2})", (object)this.x.ToString(format, formatProvider), (object)this.y.ToString(format, formatProvider), (object)this.z.ToString(format, formatProvider));
        }
    }

    [MessagePackObject]
    public struct Vector4
    {
        [Key(0)]
        public float x;
        [Key(1)]
        public float y;
        [Key(2)]
        public float z;
        [Key(3)]
        public float w;

        [SerializationConstructor]
        public Vector4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
    }

    [MessagePackObject]
    public struct Quaternion
    {
        [Key(0)]
        public float x;
        [Key(1)]
        public float y;
        [Key(2)]
        public float z;
        [Key(3)]
        public float w;

        [SerializationConstructor]
        public Quaternion(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
        /// <summary>
        ///   <para>The dot product between two rotations.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static float Dot(Quaternion a, Quaternion b)
        {
            return (float) ((double) a.x * (double) b.x + (double) a.y * (double) b.y + (double) a.z * (double) b.z + (double) a.w * (double) b.w);
        }
        private static bool IsEqualUsingDot(float dot)
        {
            return (double) dot > 0.999998986721039;
        }

        public static bool operator ==(Quaternion lhs, Quaternion rhs)
        {
            return Quaternion.IsEqualUsingDot(Quaternion.Dot(lhs, rhs));
        }

        public static bool operator !=(Quaternion lhs, Quaternion rhs)
        {
            return !(lhs == rhs);
        }
    }

    [MessagePackObject]
    public struct Color
    {
        [Key(0)]
        public float r;
        [Key(1)]
        public float g;
        [Key(2)]
        public float b;
        [Key(3)]
        public float a;

        public Color(float r, float g, float b)
            : this(r, g, b, 1.0f)
        {
        }

        [SerializationConstructor]
        public Color(float r, float g, float b, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }
    }

    [MessagePackObject]
    public struct Bounds
    {
        [Key(0)]
        public Vector3 center;

        [IgnoreMember]
        public Vector3 extents;

        [Key(1)]
        public Vector3 size
        {
            get
            {
                return this.extents * 2f;
            }

            set
            {
                this.extents = value * 0.5f;
            }
        }

        [SerializationConstructor]
        public Bounds(Vector3 center, Vector3 size)
        {
            this.center = center;
            this.extents = size * 0.5f;
        }
    }

    [MessagePackObject]
    public struct Rect
    {
        [Key(0)]
        public float x;

        [Key(1)]
        public float y;

        [Key(2)]
        public float width;

        [Key(3)]
        public float height;

        [SerializationConstructor]
        public Rect(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public Rect(Vector2 position, Vector2 size)
        {
            this.x = position.x;
            this.y = position.y;
            this.width = size.x;
            this.height = size.y;
        }

        public Rect(Rect source)
        {
            this.x = source.x;
            this.y = source.y;
            this.width = source.width;
            this.height = source.height;
        }
    }

    // additional from 1.7.3.3
    [MessagePackObject]
    public sealed class AnimationCurve
    {
        [Key(0)]
        public Keyframe[] keys;

        [IgnoreMember]
        public int length
        {
            get { return this.keys.Length; }
        }

        [Key(1)]
        public WrapMode postWrapMode;

        [Key(2)]
        public WrapMode preWrapMode;
    }

    [MessagePackObject]
    public struct Keyframe
    {
        [Key(0)]
        public float time;

        [Key(1)]
        public float value;

        [Key(2)]
        public float inTangent;

        [Key(3)]
        public float outTangent;

        public Keyframe(float time, float value)
        {
            this.time = time;
            this.value = value;
            this.inTangent = 0f;
            this.outTangent = 0f;
        }

        [SerializationConstructor]
        public Keyframe(float time, float value, float inTangent, float outTangent)
        {
            this.time = time;
            this.value = value;
            this.inTangent = inTangent;
            this.outTangent = outTangent;
        }
    }

    public enum WrapMode
    {
        Once = 1,
        Loop,
        PingPong = 4,
        Default = 0,
        ClampForever = 8,
        Clamp = 1,
    }

    [MessagePackObject]
    public struct Matrix4x4
    {
        [Key(0)]
        public float m00;
        [Key(1)]
        public float m10;
        [Key(2)]
        public float m20;
        [Key(3)]
        public float m30;
        [Key(4)]
        public float m01;
        [Key(5)]
        public float m11;
        [Key(6)]
        public float m21;
        [Key(7)]
        public float m31;
        [Key(8)]
        public float m02;
        [Key(9)]
        public float m12;
        [Key(10)]
        public float m22;
        [Key(11)]
        public float m32;
        [Key(12)]
        public float m03;
        [Key(13)]
        public float m13;
        [Key(14)]
        public float m23;
        [Key(15)]
        public float m33;
    }

    [MessagePackObject]
    public sealed class Gradient
    {
        [Key(0)]
        public GradientColorKey[] colorKeys;

        [Key(1)]
        public GradientAlphaKey[] alphaKeys;

        [Key(2)]
        public GradientMode mode;
    }

    [MessagePackObject]
    public struct GradientColorKey
    {
        [Key(0)]
        public Color color;
        [Key(1)]
        public float time;

        public GradientColorKey(Color col, float time)
        {
            this.color = col;
            this.time = time;
        }
    }

    [MessagePackObject]
    public struct GradientAlphaKey
    {
        [Key(0)]
        public float alpha;
        [Key(1)]
        public float time;

        public GradientAlphaKey(float alpha, float time)
        {
            this.alpha = alpha;
            this.time = time;
        }
    }

    public enum GradientMode
    {
        Blend,
        Fixed,
    }

    [MessagePackObject]
    public struct Color32
    {
        [Key(0)]
        public byte r;
        [Key(1)]
        public byte g;
        [Key(2)]
        public byte b;
        [Key(3)]
        public byte a;

        public Color32(byte r, byte g, byte b, byte a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }
    }

    [MessagePackObject]
    public sealed class RectOffset
    {
        [Key(0)]
        public int left;

        [Key(1)]
        public int right;

        [Key(2)]
        public int top;

        [Key(3)]
        public int bottom;

        public RectOffset()
        {
        }

        public RectOffset(int left, int right, int top, int bottom)
        {
            this.left = left;
            this.right = right;
            this.top = top;
            this.bottom = bottom;
        }
    }

    [MessagePackObject]
    public struct LayerMask
    {
        [Key(0)]
        public int value;
    }

    // from Unity2017.2
    [MessagePackObject]
    public struct Vector2Int
    {
        [Key(0)]
        public int x;
        [Key(1)]
        public int y;

        [SerializationConstructor]
        public Vector2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    [MessagePackObject]
    public struct Vector3Int
    {
        [Key(0)]
        public int x;
        [Key(1)]
        public int y;
        [Key(2)]
        public int z;

        [SerializationConstructor]
        public Vector3Int(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Vector3Int operator *(Vector3Int a, int d)
        {
            return new Vector3Int(a.x * d, a.y * d, a.z * d);
        }
    }

    [MessagePackObject]
    public struct RangeInt
    {
        [Key(0)]
        public int start;
        [Key(1)]
        public int length;

        public RangeInt(int start, int length)
        {
            this.start = start;
            this.length = length;
        }
    }

    [MessagePackObject]
    public struct RectInt
    {
        [Key(0)]
        public int x;

        [Key(1)]
        public int y;

        [Key(2)]
        public int width;

        [Key(3)]
        public int height;

        [SerializationConstructor]
        public RectInt(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public RectInt(Vector2Int position, Vector2Int size)
        {
            this.x = position.x;
            this.y = position.y;
            this.width = size.x;
            this.height = size.y;
        }

        public RectInt(RectInt source)
        {
            this.x = source.x;
            this.y = source.y;
            this.width = source.width;
            this.height = source.height;
        }
    }

    [MessagePackObject]
    public struct BoundsInt
    {
        [Key(0)]
        public Vector3Int position;

        [Key(1)]
        public Vector3Int size;

        [SerializationConstructor]
        public BoundsInt(Vector3Int position, Vector3Int size)
        {
            this.position = position;
            this.size = size;
        }
    }
}
