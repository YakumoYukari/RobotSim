using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSim
{
	public static class RobotMath
	{
		public static double AngleDifference(double ang1, double ang2)
		{
			double Diff = RobotMath.NormalizeAngle(ang1 - ang2);

			if (Diff <= 180)
				return Diff;
			else
				return Diff - 360;
		}

		public static double Approach(double start, double end, double amt)
		{
			return start > end ? start - amt < end ? end : start - amt : start + amt > end ? end : start + amt;
		}

		public static double ApproachAngle(double ang, double target, double amt)
		{
			double Diff = AngleDifference(ang, target);
			return Approach(ang, ang + Diff, amt);
		}

		public static Point ApproachPoint(Point start, Point end, double amt)
		{
			return new Point(Approach(start.X, end.X, amt), Approach(start.Y, end.Y, amt));
		}

		public static double AngleFrom(Vector2 start, Vector2 end)
		{
			return NormalizeAngle((end - start).Angle());
		}

		public static double NormalizeAngle(double ang)
		{
			return (ang + 180) % 360 - 180;
		}

		public static Vector2 AngleToVector(double ang)
		{
			double rad = DegToRad(ang);
			return new Vector2(Math.Cos(rad), Math.Sin(rad));
		}

		public static double Remap(double val, double inMin, double inMax, double outMin, double outMax)
		{
			return outMin + (((val - inMin) / (inMax - inMin)) * (outMax - outMin));
		}

		public static double Clamp(double val, double min, double max)
		{
			return val > max ? max : val < min ? min : val;
		}

		public static bool IsInRange(double val, double min, double max)
		{
			return val >= min && val <= max;
		}

		public static double Random()
		{
			Random r = new Random();
			return r.NextDouble();
		}

		public static double Random(double min, double max)
		{
			return min + (max - min) * Random();
		}

		public static double DegToRad(double deg)
		{
			return deg * (Math.PI / 180);
		}

		public static double RadToDeg(double rad)
		{
			return rad * (180 / Math.PI);
		}

		public static double[] ConcatArray(double[][] input)
		{
			List<double> output = new List<double>();

			for (int i = 0; i < input.Length; i++)
			{
				for (int j = 0; j < input[i].Length; j++)
				{
					output.Add(input[i][j]);
				}
			}

			return output.ToArray();
		}
	}
}
