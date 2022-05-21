using System;
using System.Collections.Generic;

namespace func_rocket
{
	public class LevelsTask
	{
		static readonly Physics standardPhysics = new Physics();

		public static IEnumerable<Level> CreateLevels()
		{
			yield return new Level("Zero", 
				new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
				new Vector(600, 200), 
				(size, v) => Vector.Zero, standardPhysics);
			
			yield return new Level("Heavy",
				new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
				new Vector(600, 200),
				(size, v) => new Vector(0, 0.9), standardPhysics);

			yield return new Level("Up",
				new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
				new Vector(700, 500),
				(size, v) => new Vector(0, -300 / (size.Height - v.Y + 300)),
				standardPhysics);

			yield return new Level("WhiteHole",
				new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
				new Vector(600, 200),
				(size, v) =>
				{
					var inWhiteHole = new Vector(v.X - 600, v.Y - 200);
					return inWhiteHole.Normalize() * 140 * inWhiteHole.Length
					/ (inWhiteHole.Length * inWhiteHole.Length + 1);
				},
				standardPhysics);

			yield return new Level("BlackHole",
				new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
				new Vector(600, 200),
				(size, v) =>
				{
					var holePosition = new Vector((600 + 200) / 2, (500 + 200) / 2);
					var inBlackHole = holePosition - v;
					return new Vector(holePosition.X - v.X, holePosition.Y - v.Y).Normalize() *
						300 * inBlackHole.Length / (inBlackHole.Length * inBlackHole.Length + 1);
				}
				, standardPhysics);

			yield return new Level("BlackAndWhite",
				new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
				new Vector(600, 200),
				(size, v) =>
				{
					var inWhiteHole = new Vector(v.X - 600, v.Y - 200);
					var blackHolePosition = new Vector((600 + 200) / 2, (500 + 200) / 2);
					var inBlackHole = blackHolePosition - v;
					return (inWhiteHole.Normalize() * 140 * inWhiteHole.Length
					/ (inWhiteHole.Length * inWhiteHole.Length + 1) +
					new Vector(blackHolePosition.X - v.X, blackHolePosition.Y - v.Y).Normalize() *
						300 * inBlackHole.Length / (inBlackHole.Length * inBlackHole.Length + 1)) / 2;
				}
				, standardPhysics);
		}
	}
}