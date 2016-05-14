using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSim
{
	public static class PhysicsConstants
	{
		//Still object starting to move
		public static readonly double StaticFrictionCoefficient = 1.0d;
		public static readonly double StaticFrictionEscapeVelocity = 0.01d;

		//Moving objects
		public static readonly double SlidingFrictionCoefficient = 1.01d;

		//Useful for objects like wheels
		public static readonly double RollingFrictionCoefficient = 1.0d;

		//Spinning while not moving
		public static readonly double StaticRotationalFrictionCoefficient = 1.04d;
		//Spinning while moving
		public static readonly double RotationalFrictionCoefficient = 1.01d;
		public static readonly double RotationalFrictionEscapeVelocity = 0.1d;

		public static readonly double MaxVelocity = 100.0d;
		public static readonly double MaxAngularVelocity = 360.0d;
	}
}
