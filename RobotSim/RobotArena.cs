using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobotSim
{
	public class RobotArena
	{
		private bool DEBUG_VEC = true;
		private Vector2 theVec;
		private Vector2 StartPt;


		public int Width, Height;
		public List<WorldObject> Entities;

		public RobotArena()
		{
			Width = 1024;
			Height = 768;

			Entities = new List<WorldObject>();
		}

		public RobotArena(int w, int h)
		{
			Width = w;
			Height = h;

			Entities = new List<WorldObject>();
		}

		public void AddEntity(WorldObject obj)
		{
			obj.Owner = this;
			Entities.Add(obj);
			obj.OnAddToWorld();
		}

		public void RemoveEntity(WorldObject obj)
		{
			Entities.Remove(obj);
		}

		public void OnTick()
		{
			foreach (WorldObject obj in Entities)
			{
				obj.OnTick();
				obj.OnUpdate();
			}
		}

		public void OnPhysicsUpdate(double deltaTime)
		{
			foreach (WorldObject obj in Entities)
			{
				if (obj is PhysicsObject)
					((PhysicsObject)obj).OnPhysicsUpdate(deltaTime);
			}
		}

		public void Draw(Graphics g)
		{
			System.Drawing.SolidBrush b = new System.Drawing.SolidBrush(Color.White);
			g.FillRectangle(b, 0, 0, Width, Height);

			foreach (WorldObject obj in Entities)
			{
				obj.Draw(g);
			}

			if (DEBUG_VEC && theVec != null && StartPt != null)
			{
				Pen p = new Pen(Color.Red, 2.0f);

				g.DrawLine(p, (int)StartPt.X, (int)StartPt.Y, (int)StartPt.X + (int)theVec.X, (int)StartPt.Y + (int)theVec.Y);
				g.DrawEllipse(p, (int)StartPt.X + (int)theVec.X - 5, (int)StartPt.Y + (int)theVec.Y - 5, 10, 10);

				p.Dispose();
			}

			b.Dispose();
		}

		public void DrawVector(Vector2 v, Vector2 start)
		{
			theVec = v.Clone();
			StartPt = start.Clone();
		}

	}
}
