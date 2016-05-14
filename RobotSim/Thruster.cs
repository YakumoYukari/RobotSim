using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSim
{
	class Thruster : Actuator
	{
		double Power;

		public Thruster(double power)
		{
			Data = new double[1];
			Power = power;
		}

		public override void SetShape()
		{
			Drawing.Color = Color.Red;
			Drawing.Thickness = 1.5f;

			Frame.SetPointCloud(new double[][] {
			new double[] {-1, -.5},
			new double[] {0, -1},
			new double[] {0, +1},
			new double[] {-1, +.5},
			});
		}

		public Vector2 GetForce()
		{
			Vector2 force = RobotMath.AngleToVector(-RobotMath.NormalizeAngle(ParentRobot.WorldTransform.Angle + LocalOffset.Angle)) * Power * RobotMath.Clamp(Data[0], -1.0, 1.0);
			return force;
		}

		public override void Process()
		{
			//Owner.DrawVector(GetForce(), WorldTransform.Position);
			ParentRobot.ApplyForceOffset(GetForce(), LocalOffset.Position);
		}
	}
}
