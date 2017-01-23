using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robot
{
    enum Obstacles
    {
        ROCK = 0,
        HOLE = 1,
        SPINNER = 2
    }
    interface Obstacle
    {
        int[] position { get; set; }
        Obstacles obstacleType { get; set; }
        int[] moveInto(int[] robotPos, GenericRobot.Facing robotFacing); // returns a pos and direction the robot will be at, after moving into the obstacle
    }
}
