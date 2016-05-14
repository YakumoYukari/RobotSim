using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace RobotSim
{
	public partial class Form1 : Form
	{
		RobotArena TheWorld;
		System.Timers.Timer timer;

		public Form1()
		{
			InitializeComponent();

			this.DoubleBuffered = true;
			this.SetStyle(ControlStyles.UserPaint |
						  ControlStyles.AllPaintingInWmPaint |
						  ControlStyles.ResizeRedraw |
						  ControlStyles.ContainerControl |
						  ControlStyles.OptimizedDoubleBuffer |
						  ControlStyles.SupportsTransparentBackColor
						  , true);
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			timer = new System.Timers.Timer(1000.0 / SimConstants.Framerate);
			timer.Elapsed += OnTimedEvent;
			timer.AutoReset = true;

			TheWorld = new RobotArena(1024, 768);

			InitRobotWorld();

			Invalidate();
			Update();
			timer.Start();
		}

		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);

			timer.Stop();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			TheWorld.Draw(e.Graphics);
		}

		private void InitRobotWorld()
		{
			Random r = new Random();
			for (int LOL = 0; LOL < 10; LOL++)
			{
				

				Robot a = new Robot(new Transform(r.Next(0, TheWorld.Width), r.Next(0, TheWorld.Height), r.Next(0, 360), 25));
				a.SetMass(10);
				TheWorld.AddEntity(a);

				
				for (int i = 0; i < 10; i++)
				{
					Block b = new Block(new Transform(r.NextDouble() * TheWorld.Width, r.NextDouble() * TheWorld.Height, r.NextDouble() * 360, r.NextDouble() * 20 + 10));
					TheWorld.AddEntity(b);
					b.Drawing.Color = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));
				}



				Thruster thrust = new Thruster(1);
				thrust.LocalOffset = new Transform(12, 25, 90, .3);
				a.AddActuator(thrust);
				thrust.Drawing.Color = Color.Navy;
				thrust.Drawing.Thickness = 2.0f;

				thrust = new Thruster(1);
				thrust.LocalOffset = new Transform(12, -25, -90, .3);
				a.AddActuator(thrust);
				thrust.Drawing.Color = Color.ForestGreen;
				thrust.Drawing.Thickness = 2.0f;

				thrust = new Thruster(.2);
				thrust.LocalOffset = new Transform(-25, 0, 0, .5);
				a.AddActuator(thrust);

				BlockFinder finder = new BlockFinder();
				finder.LocalOffset = new Transform(10, 0, 45, .2);
				a.AddSensor(finder);

			}
			//a.PushFromAngle(40, 45);
			//a.ApplyForceOffset(new Vector2(0, 20), new Vector2(-100, 0));

			//Testing
			//ApplyForce(new Vector2(2, 2));
			//a.ApplyForceOffset(new Vector2(3, 0), new Vector2(0, 2));
			//a.ApplyForceOffset(new Vector2(2, 02), new Vector2(01, -2));
		}

		private void OnTimedEvent(Object source, ElapsedEventArgs e)
		{
			TheWorld.OnPhysicsUpdate(1.0 / SimConstants.Framerate);

			TheWorld.OnTick();

			try
			{
				if (InvokeRequired && !IsDisposed)
				{
					Invoke(new MethodInvoker(delegate
					{
						Invalidate();
						Update();
					}));
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				// Else we get an error complaining about accessing something when it's disposed of.
				// TODO: Figure out how to stop this properly
			}
		}
	}
}
