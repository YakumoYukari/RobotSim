using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSim
{
	public abstract class PhysicsObject : WorldObject
	{
		#region Variables

		//kg
		public double Mass = 1.0d;
		public double Restitution = 1.0d;
		//units/frame
		public Vector2 Velocity = new Vector2(0,0);
		//Rad/sec
		public double AngularVelocity = 0.0d;

		#endregion

		#region Modifiers

		public void SetMass(double m)
		{
			Mass = m;
		}

		#endregion

		#region Processing

		public void OnPhysicsUpdate(double deltaTime)
		{
			double VelSqrMag = Velocity.SquareMagnitude();

			WorldTransform.Position += Velocity * deltaTime * SimConstants.Framerate;
			Velocity /= PhysicsConstants.SlidingFrictionCoefficient;
			if (VelSqrMag < PhysicsConstants.StaticFrictionEscapeVelocity)
				Velocity = new Vector2(0, 0);
			Velocity.ClampMagnitude(0, PhysicsConstants.MaxVelocity);

			WorldTransform.Angle += AngularVelocity * deltaTime;
			AngularVelocity /= VelSqrMag > 0 ? PhysicsConstants.RotationalFrictionCoefficient : PhysicsConstants.StaticRotationalFrictionCoefficient;
			if (Math.Abs(AngularVelocity) < PhysicsConstants.RotationalFrictionEscapeVelocity)
				AngularVelocity = 0.0d;
			AngularVelocity = RobotMath.Clamp(AngularVelocity, -PhysicsConstants.MaxAngularVelocity, PhysicsConstants.MaxAngularVelocity);

			OnMove();
		}

		#endregion

		#region Force Application

		public void ApplyForce(Vector2 Force)
		{
			Velocity += new Vector2(Force.X / Mass, Force.Y / Mass);
		}

		public void ApplyRotationalForce(double Force)
		{
			AngularVelocity += Force / Mass;
		}

		public void ApplyForceOffset(Vector2 Force, Vector2 Offset)
		{
			ApplyForce(Force);

			Vector2 OffsetNormal = Offset.GetNormalized();
			Vector2 Projection = Force.DotProduct(OffsetNormal) * OffsetNormal;
			double RotationScale = (Force - Projection).Magnitude() * Offset.Magnitude();
			double RotationDirection =  Math.Sign(RobotMath.AngleDifference(Offset.Angle(), Force.Angle()));

			ApplyRotationalForce(RotationScale * RotationDirection);
		}

		public void PushFromAngle(double force, double ang)
		{
			ApplyForce(RobotMath.AngleToVector(WorldTransform.Angle + ang) * force);
		}

		#endregion

		#region Collision

		public Vector2 EdgeIntersection(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
		{
			double det = Determinant(b - a, c - d);
			double t = Determinant(c - a, c - d) / det;
			double u = Determinant(b - a, c - a) / det;
			if ((t < 0) || (u < 0) || (t > 1) || (u > 1))
			{
				return new Vector2(0, 0);
			}
			else
			{
				return a * (1 - t) + t * b;
			}
		}

		private void ResolveCollision(PhysicsObject other)
		{
			Vector2 RelVel = other.Velocity - Velocity;
			Vector2 Normal = (other.WorldTransform.Position - WorldTransform.Position).GetNormalized();

			double oneInvMass = 1.0 / Mass;
			double twoInvMass = 1.0 / other.Mass;

			double VecAlongNormal = RelVel.DotProduct(Normal);

			if (VecAlongNormal > 0)
				return;

			double e = Math.Min(Restitution, other.Restitution);

			double j = -(1 - e) * VecAlongNormal;
			j /= oneInvMass + twoInvMass;

			Vector2 Impulse = j * Normal;
			Velocity -= oneInvMass * Impulse;
			other.Velocity += twoInvMass * Impulse;
		}

		#endregion

		#region Helpers

		public double Determinant(Vector2 vec1, Vector2 vec2)
		{
			return vec1.X * vec2.Y - vec1.Y * vec2.X;
		}

		#endregion
	}
}
