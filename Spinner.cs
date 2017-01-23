using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robot
{
    class Spinner : Obstacle
    {
        private int x; private int y; private int spinFactor;
        public int[] position { get { return new int[] { x, y }; } set { x = value[0]; y = value[1]; } }
        public Obstacles obstacleType { get; set; }

        public Spinner(int[] pos, int spinAmount)
        {
            position = pos;
            spinFactor = spinAmount;
            obstacleType = Obstacles.SPINNER;
        }

        private int spinRobot(GenericRobot.Facing currentFace)
        { // new direction will be (currentFace.value + spinFactor)%4 where 0 is north, 1 is east, 2 is south, 3 is west
            return ((int)currentFace + spinFactor)%4;
        }

        public int[] moveInto(int[] robotPos, GenericRobot.Facing robotFacing)
        {
            int newDir = spinRobot(robotFacing);
            Console.WriteLine("Spinner(" + spinFactor + ")... Old direction: " + robotFacing.ToString() + ". New direction: " + ((GenericRobot.Facing)newDir).ToString());
            return new int[] { position[0], position[1], newDir }; // move to spinner and change facing direction
        }
    }
}
