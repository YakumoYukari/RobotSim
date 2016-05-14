using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSim
{
	public class PointCloud
	{
		List<Point> Points;
		Vector2 Position;
		double Rotation = 0.0d;
		double Scale = 0.0d;

		public PointCloud() { Points = new List<Point>(); Position = new Vector2(0, 0); }

		public PointCloud(double[][] p)
		{
			Points = new List<Point>();
			Position = new Vector2(0,0);
			SetPoints(p);
		}

		public void SetPoints(double[][] p)
		{
			Points.Clear();
			for (int i = 0; i < p.Length; i++)
			{
				if (p[i].Length != 2)
					continue;

				AddPoint(new Point(p[i][0], p[i][1]));
			}
		}

		public void AddPoint(Point p)
		{
			Points.Add(p);
		}

		public List<Point> GetPoints()
		{
			return Points;
		}

		public void Clear()
		{
			Points.Clear();
		}

		public void SetPosition(Vector2 pos)
		{
			Position = pos.Clone();
		}

		public void SetScale(double s)
		{
			Scale = s;
		}

		public void SetRotation(double ang)
		{
			if (Rotation != ang)
			{
				Rotate(Rotation - ang);
				Rotation = ang;
			}
		}

		public void Rotate(double ang)
		{
			ang = RobotMath.DegToRad(ang);
			foreach (Point p in Points)
			{
				double oldX = p.X, oldY = p.Y;

				p.X = oldX * Math.Cos(ang) - oldY * Math.Sin(ang);
				p.Y = oldY * Math.Cos(ang) + oldX * Math.Sin(ang);
			}
		}

		public System.Drawing.Point[] GetGraphicsArray()
		{
			System.Drawing.Point[] arr = new System.Drawing.Point[Points.Count];

			for (int i = 0; i < Points.Count; i++)
			{
				arr[i] = new System.Drawing.Point((int)Math.Round(Points[i].X * Scale + Position.X), (int)Math.Round(Points[i].Y * Scale + Position.Y));
			}

			return arr;
		}

	}
}
