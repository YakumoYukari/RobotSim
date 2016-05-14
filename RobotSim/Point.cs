using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSim
{
	public struct Point
	{
		public double X, Y;

		public Point()
		{
			X = 0;
			Y = 0;
		}

		public Point(double x, double y)
		{
			X = x;
			Y = y;
		}

		public void Approach(Point other, double amt)
		{
			Point p = RobotMath.ApproachPoint(this, other, amt);
			X = p.X;
			Y = p.Y;
		}

		public double DistanceSquared(Point other)
		{
			return X * other.X + Y * other.Y;
		}

		public double Distance(Point other)
		{
			return Math.Sqrt(DistanceSquared(other));
		}

		public Vector2 ToVector()
		{
			return new Vector2(X, Y);
		}

		public static Point operator +(Point one, Point two)
		{
			return new Point(one.X + two.X, one.Y + two.Y);
		}

		public static Point operator -(Point one, Point two)
		{
			return new Point(one.X - two.X, one.Y - two.Y);
		}
	}
}
