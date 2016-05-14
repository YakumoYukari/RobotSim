using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Neuro;

namespace RobotSim
{
	public class Robot : PhysicsObject
	{
		public List<Actuator> Actuators;
		public List<Sensor> Sensors;

		public double[] SensorInputs;
		public double[] ActuatorOutputs;

		public Robot()
		{
			this.WorldTransform.Position = new Vector2(0, 0);
			this.WorldTransform.Angle = 0.0d;
			this.WorldTransform.Scale = 1.0d;

			InitComponents();
		}
		public Robot(Point p)
		{
			this.WorldTransform.Position = new Vector2(p.X, p.Y);
			this.WorldTransform.Angle = 0.0d;
			this.WorldTransform.Scale = 1.0d;

			InitComponents();
		}
		public Robot(Vector2 v)
		{
			this.WorldTransform.Position = new Vector2(v.X, v.Y);
			this.WorldTransform.Angle = 0.0d;
			this.WorldTransform.Scale = 1.0d;

			InitComponents();
		}
		public Robot(Transform t)
		{
			WorldTransform = t;
			InitComponents();
		}

		public override void SetShape()
		{
			Drawing.Color = Color.Blue;
			Drawing.Thickness = 2.0f;

			Frame.SetPointCloud(new double[][] {
			new double[] {-1, -1},
			new double[] {+1, -1},
			new double[] {+1, +1},
			new double[] {-1, +1},
			});
		}

		public void InitComponents()
		{
			Actuators = new List<Actuator>();
			Sensors = new List<Sensor>();

			InitIO();
		}

		public void AddSensor(Sensor s)
		{
			s.ParentRobot = this;
			
			s.LocalOffset = s.LocalOffset.Clone();
			s.WorldTransform = WorldTransform + s.LocalOffset;

			AddChild(s);

			Sensors.Add(s);
			Owner.AddEntity(s);

			AdjustSensorArray();
		}
		public void AddActuator(Actuator a)
		{
			a.ParentRobot = this;

			a.LocalOffset = a.LocalOffset.Clone();
			a.WorldTransform = WorldTransform + a.LocalOffset;

			AddChild(a);

			Actuators.Add(a);
			Owner.AddEntity(a);

			AdjustActuatorArray();
		}

		private void InitIO()
		{
			AdjustActuatorArray();
			AdjustSensorArray();
		}

		private void AdjustActuatorArray()
		{
			int size = 0;

			for (int i = 0; i < Actuators.Count; i++)
			{
				size += Actuators[i].Data.Length;
			}

			ActuatorOutputs = new double[size];
		}
		private void AdjustSensorArray()
		{
			int size = 0;

			for (int i = 0; i < Sensors.Count; i++)
			{
				size += Sensors[i].Data.Length;
			}

			SensorInputs = new double[size];
		}

		private void GetSensorInputs()
		{
			int count = 0;
			foreach (Sensor s in Sensors)
			{
				for (int i = 0; i < s.Data.Length; i++)
				{
					SensorInputs[count] = s.Data[i];
					count++;
				}
			}
		}
		private void SetActuatorOutputs()
		{
			int count = 0;
			foreach (Actuator a in Actuators)
			{
				for (int i = 0; i < a.Data.Length; i++)
				{
					a.Data[i] = ActuatorOutputs[count];
					count++;
				}
			}
		}

		public void ProcessIO()
		{
			foreach(Sensor s in Sensors)
			{
				s.DoProcess();
			}
			foreach(Actuator a in Actuators)
			{
				a.DoProcess();
			}
		}

		public override void OnUpdate()
		{
			GetSensorInputs();

			double angTo = RobotMath.AngleFrom(WorldTransform.Position, new Vector2(SensorInputs[0], SensorInputs[1]));
			double ourAng = RobotMath.NormalizeAngle(WorldTransform.Angle);

			//Do thinking stuff
			if ( angTo > ourAng)
			{
				ActuatorOutputs[2] = 1;
				ActuatorOutputs[1] = 1.0;
				ActuatorOutputs[0] = 0;
			}
			if (angTo < ourAng)
			{
				ActuatorOutputs[2] = .5;
				ActuatorOutputs[1] = 0;
				ActuatorOutputs[0] = 1.0;
			}
			if (Math.Abs(angTo) > 90)
			{
				ActuatorOutputs[2] = -1;
			}

			//WorldTransform.Angle = RobotMath.ApproachAngle(WorldTransform.Angle, Velocity.Angle(), 1);

			SetActuatorOutputs();
			ProcessIO();

			if (IsOutOfBounds())
			{
				WorldTransform = new Transform();
			}
		}

	}
}
