using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Neuro;
using AForge;
using AForge.Math.Random;
using AForge.Neuro.Learning;
using AForge.Genetic;

namespace RobotSim
{
	class RobotBrain
	{
		Robot Owner;
		int PopSize;
		//Population Pop;

		public RobotBrain(Robot owner, int a_popsize)
		{
			Owner = owner;
			PopSize = a_popsize;
			Init();
		}

		public class UserFunction : OptimizationFunction1D
		{
			public UserFunction() :
				base(new Range(0, 255)) { }

			public override double OptimizationFunction(double x)
			{
				return Math.Cos(x / 23) * Math.Sin(x / 50) + 2;
			}
		}

		public void Init()
		{
			/*DoubleArrayChromosome chromosomeExample = new DoubleArrayChromosome(new UniformGenerator( new Range( -1, 1 ) ), new ExponentialGenerator( 1 ), new UniformGenerator( new Range( -0.5f, 0.5f ) ), Owner.SensorInputs.Length);

			Pop = new Population(PopSize, chromosomeExample, );
			// ... and configure it
			Pop.SelectionMethod = new EliteSelection();
			Pop.CrossoverRate = .5;
			Pop.MutationRate = .1;
			Pop.RandomSelectionPortion = .1;*/
		}

		public void Process()
		{

		}

	}
}
