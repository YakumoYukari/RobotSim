using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSim
{
	public abstract class RobotComponent : WorldObject
	{
		public Robot ParentRobot;
		
		public bool On = true;
		public double[] Data; //Outputs from Sensors, Inputs to Actuators
		public void DoProcess()
		{
			if (On)
				Process();
		}
		public abstract void Process();

		public override void OnUpdate()
		{

		}
	}
}
