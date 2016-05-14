using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSim
{
	class BlockFinder : Sensor
	{

		Random r = new Random(System.DateTime.Now.Millisecond);

		WorldObject target = null;

		public BlockFinder()
		{
			Data = new double[4];
		}

		public override void Process()
		{
			FindNextBlock();
			WorldObject wo = target;
			Data[0] = wo.WorldTransform.Position.X;
			Data[1] = wo.WorldTransform.Position.Y;
			Data[2] = wo.WorldTransform.Angle;
			Data[3] = wo.WorldTransform.Scale;

			if (r.NextDouble() < .001) FindNextBlock();
		}

		public void FindNextBlock()
		{
			target = Owner.Entities.ElementAt(r.Next(0, Owner.Entities.Count));
			if (!(target is Block))
				FindNextBlock();
		}

		public override void SetShape()
		{
			Drawing.Color = Color.Black;
			Drawing.Thickness = 2.0f;

			Frame.SetPointCloud(new double[][] {
			new double[] {-1, -1},
			new double[] {+1, -1},
			new double[] {+1, +1},
			new double[] {-1, +1},
			});
		}
	}
}
