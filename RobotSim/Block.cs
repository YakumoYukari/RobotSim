using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSim
{
	class Block : WorldObject
	{

		public Block(Transform t)
		{
			WorldTransform = t;
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
			new double[] {-1, -1},
			new double[] {+1, +1},
			});
		}

		public override void OnUpdate()
		{
			
		}

	}
}
