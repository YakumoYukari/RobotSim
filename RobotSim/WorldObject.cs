using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSim
{
	public abstract class WorldObject
	{
		public RobotArena Owner;

		public List<WorldObject> Children = new List<WorldObject>();
		public Transform LocalOffset = new Transform();

		public DrawingInfo Drawing = new DrawingInfo();
		public CollisionHull Frame = new CollisionHull();

		private Transform m_WorldTransform = new Transform();
		public Transform WorldTransform
		{
			get { return m_WorldTransform; }
			set { m_WorldTransform = value.Clone();
				m_WorldTransform.Angle = RobotMath.NormalizeAngle(m_WorldTransform.Angle);
			}
		}

		public String Name;
		public abstract void SetShape();

		public void SetPosition(Vector2 pos)
		{
			m_WorldTransform.Position = pos.Clone();
			OnMove();
		}
		public void SetAngle(double ang)
		{
			m_WorldTransform.Angle = ang;
			OnMove();
		}
		public void SetScale(double scale)
		{
			m_WorldTransform.Scale = scale;
			OnMove();
		}
		public void UpdateChildren()
		{
			foreach (WorldObject w in Children)
			{
				w.LocalOffset.Position.SetRotation(WorldTransform.Angle);
				w.WorldTransform = WorldTransform + w.LocalOffset;
			}
		}
		public void AddChild(WorldObject other)
		{
			if (!other.Children.Contains(other)) //No infinite recursion
				Children.Add(other);
		}

		public bool IsOutOfBounds()
		{
			return WorldTransform.Position.X < 0 ||
				WorldTransform.Position.X > Owner.Width ||
				WorldTransform.Position.Y < 0 ||
				WorldTransform.Position.Y > Owner.Height;
		}
		public void ClampBounds()
		{
			WorldTransform.Position.X = RobotMath.Clamp(WorldTransform.Position.X, 0, Owner.Width);
			WorldTransform.Position.Y = RobotMath.Clamp(WorldTransform.Position.Y, 0, Owner.Height);
		}

		public void OnAddToWorld()
		{
			SetShape();
		}
		public void Destroy()
		{
			Owner.RemoveEntity(this);
		}
		public void Draw(System.Drawing.Graphics g)
		{
			System.Drawing.Pen pen1 = new System.Drawing.Pen(Drawing.Color, Drawing.Thickness);
			System.Drawing.Drawing2D.GraphicsState state = g.Save();

			g.DrawPolygon(pen1, Frame.Points.GetGraphicsArray());

			g.Restore(state);
			pen1.Dispose();
		}
		public void OnTick()
		{
			Frame.UpdatePointCloud(WorldTransform);
		}
		public abstract void OnUpdate();
		public void OnMove()
		{
			ClampBounds();
			UpdateChildren();
		}
	}
}
