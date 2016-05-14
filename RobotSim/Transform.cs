using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSim
{
	public class Transform
	{
		public Vector2 Position;
		public double Angle;
		public double Scale;

		public Transform()
		{
			Position = new Vector2(0,0);
			Angle = 0.0d;
			Scale = 1.0d;
		}
		public Transform(double x, double y, double ang, double s)
		{
			Position = new Vector2(x, y);
			Angle = ang;
			Scale = s;
		}
		public Transform(Vector2 pos, double ang, double scale)
		{
			Position = new Vector2(pos.X, pos.Y);
			Angle = ang % 360;
			Scale = scale;
		}

		public Transform Clone()
		{
			return new Transform(Position, Angle, Scale);
		}

		public static Transform operator +(Transform one, Transform two)
		{
			return new Transform(
				one.Position + two.Position,
				one.Angle + two.Angle,
				one.Scale * two.Scale );
		}

		public static Transform operator -(Transform one, Transform two)
		{
			return new Transform(
				one.Position - two.Position,
				one.Angle - two.Angle,
				one.Scale);
		}
	}
}
