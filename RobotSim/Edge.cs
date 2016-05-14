using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSim
{
	class Edge
	{
		Vector2 P1, P2;

		public Edge(Vector2 one, Vector2 two)
		{
			P1 = one.Clone();
			P2 = two.Clone();
		}

	}
}
