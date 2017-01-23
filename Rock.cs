using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robot
{
    class Rock : Obstacle
    {
        private int x; private int y;
        public int[] position { get { return new int[] { x, y }; } set { x = value[0]; y = value[1]; } }
        public Obstacles obstacleType { get; set; }
        public Rock(int[] pos)
        {
            position = pos;
            obstacleType = Obstacles.ROCK;
        }

        public int[] moveInto(int[] robotPos, GenericRobot.Facing robotFacing)
        {
            return new int[] { robotPos[0], robotPos[1], (int)robotFacing }; // no change in pos or facing for robot
        }
    }
}
