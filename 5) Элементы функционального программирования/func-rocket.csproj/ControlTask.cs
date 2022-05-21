using System;
using System.Linq;

namespace func_rocket
{
	public class ControlTask
	{
		public static Turn ControlRocket(Rocket rocket, Vector target)
		{
            var distance = new Vector(target.X - rocket.Location.X, target.Y - rocket.Location.Y);
            var angle1 = distance.Angle - rocket.Direction;
            var angle2 = distance.Angle - rocket.Velocity.Angle;
            var totalAngle = angle1;

            if (Math.Abs(distance.Angle - rocket.Direction) < 0.5
                || Math.Abs(distance.Angle - rocket.Velocity.Angle) < 0.5)
            {
                totalAngle = (distance.Angle - rocket.Direction + distance.Angle - rocket.Velocity.Angle) / 2;
            }
            else
            {
                totalAngle = distance.Angle - rocket.Direction;
            }

            if (totalAngle > 0)
                return Turn.Right;
            return totalAngle < 0 ? Turn.Left : Turn.None;
        }
	}
}