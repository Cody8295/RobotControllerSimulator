using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robot
{
    class Hole : Obstacle
    {
        private int x; private int y;
        private int otherX; private int otherY;
        public int[] position { get { return new int[] { x, y }; } set { x = value[0]; y = value[1]; } }
        public int[] otherSide { get { return new int[] {otherX, otherY}; } set { otherX = value[0]; otherY = value[1]; } }
        public Obstacles obstacleType { get; set; }
        public Hole(int[] pos, int[] altPos)
        {
            position = pos;
            otherSide = altPos;
            obstacleType = Obstacles.HOLE;
        }

        public int[] moveInto(int[] robotPos, GenericRobot.Facing robotFacing)
        {
            Console.WriteLine("Hole... " + x + ", " + y + " -> " + otherX + ", " + otherY);
            return new int[] { otherX, otherY, (int)robotFacing }; // teleport keeping same direction
        }
    }
}
