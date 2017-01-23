using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robot
{
    class Node
    {
        public int[] position;
        public Node(int[] pos)
        {
            position = pos;
        }

        public int[] moveInto(int[] robotPos, GenericRobot.Facing robotFacing)
        {
            return new int[] { position[0], position[1], (int)robotFacing };
        }
    }
}
