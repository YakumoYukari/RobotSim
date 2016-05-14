using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSim
{
	public class CollisionHull
	{

		public PointCloud Points;

		public CollisionHull()
		{
			Points = new PointCloud();
		}

		public void SetPointCloud(double[][] p)
		{
			Points.SetPoints(p);
		}

		public void UpdatePointCloud(Transform t)
		{
			Points.SetScale(t.Scale);
			Points.SetPosition(t.Position);
			Points.SetRotation(t.Angle);
		}

	}
}
