using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSim
{
	public struct Vector2
	{
		public double X, Y;
		public double Rotation = 0;

		public Vector2(double x, double y)
		{
			X = x;
			Y = y;
		}

		public double SquareMagnitude()
		{
			return X * X + Y * Y;
		}
		public double Magnitude()
		{
			return Math.Sqrt(SquareMagnitude());
		}
		public double Angle()
		{
			return RobotMath.RadToDeg(Math.Atan2(Y, X));
		}

		public Vector2 GetNormalized()
		{
			double Len = Magnitude();
			return new Vector2(X / Len, Y / Len);
		}
		public Vector2 Normalize()
		{
			Vector2 Normal = GetNormalized();
			X = Normal.X;
			Y = Normal.Y;
			return this;
		}

		public void Scale(double scale)
		{
			X = X * scale;
			Y = Y * scale;
		}
		public void ClampMagnitude(double min, double max)
		{
			double mag = Magnitude();
			if (mag < min)
			{
				Normalize();
				Scale(min);
			}
			if (mag > max)
			{
				Normalize();
				Scale(max);
			}
		}

		public double DotProduct(Vector2 other)
		{
			return X * other.X + Y * other.Y;
		}

		public Vector2 SetRotation(double ang)
		{
			if (Rotation != ang)
			{
				Rotate(Rotation - ang);
				Rotation = ang;
			}
			return this;
		}
		public void Rotate(double ang)
		{
			double oldX = X, oldY = Y;
			ang = RobotMath.DegToRad(ang);

			X = oldX * Math.Cos(ang) - oldY * Math.Sin(ang);
			Y = oldY * Math.Cos(ang) + oldX * Math.Sin(ang);
		}

		public Point ToPoint()
		{
			return new Point(X, Y);
		}
		public Vector2 Clone()
		{
			return new Vector2(X,Y);
		}

		#region Operators

		public static Vector2 operator +(Vector2 one, Vector2 other)
		{
			return new Vector2(one.X + other.X, one.Y + other.Y);
		}
		public static Vector2 operator +(Vector2 one)
		{
			return new Vector2(one.X, one.Y);
		}
		public static Vector2 operator -(Vector2 one, Vector2 other)
		{
			return new Vector2(one.X - other.X, one.Y - other.Y);
		}
		public static Vector2 operator -(Vector2 one)
		{
			return new Vector2(-one.X , -one.Y);
		}
		public static Vector2 operator *(Vector2 one, int other)
		{
			return new Vector2(one.X * other, one.Y * other);
		}
		public static Vector2 operator /(Vector2 one, int other)
		{
			return new Vector2(one.X / other, one.Y / other);
		}
		public static Vector2 operator *(Vector2 one, double other)
		{
			return new Vector2(one.X * other, one.Y * other);
		}
		public static Vector2 operator /(Vector2 one, double other)
		{
			return new Vector2(one.X / other, one.Y / other);
		}
		public static Vector2 operator *(int other, Vector2 one)
		{
			return new Vector2(one.X * other, one.Y * other);
		}
		public static Vector2 operator /(int other, Vector2 one)
		{
			return new Vector2(one.X / other, one.Y / other);
		}
		public static Vector2 operator *(double other, Vector2 one)
		{
			return new Vector2(one.X * other, one.Y * other);
		}
		public static Vector2 operator /(double other, Vector2 one)
		{
			return new Vector2(one.X / other, one.Y / other);
		}

		#endregion
	}
}
