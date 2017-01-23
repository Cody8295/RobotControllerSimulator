using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robot
{
    class GenericRobot
    {
        private Stack<Obstacle> obstacles;
        private Stack<Node> nodeStack;

        public enum Direction
        {
            FORWARD = 0,
            LEFT = 1,
            RIGHT = 2
        }

        public enum Facing
        {
            NORTH = 0,
            EAST = 1,
            SOUTH = 2,
            WEST = 3
        }

        public int[] curPos;
        public Facing curDir;
        private string moveSeq;

        private int[] checkForObstacles(int[] atPos)
        {
            foreach (Obstacle o in obstacles)
            {
                if (o.position[0] == atPos[0] && o.position[1] == atPos[1])
                {
                    int[] retVal = o.moveInto(curPos, curDir);
                    curDir = (Facing)retVal[2];
                    return new int[]{retVal[0], retVal[1]};
                }
            }
            foreach (Node n in nodeStack)
            {
                if (n.position[0] == atPos[0] && n.position[1] == atPos[1])
                {
                    int[] retVal = n.moveInto(curPos, curDir);
                    curDir = (Facing)retVal[2];
                    return new int[] { retVal[0], retVal[1] };
                }
            }
            return curPos; // if it's not a node or an obstacle, stay put
        }

        private void forwardMove()
        {
            int[] curPosNew = new int[]{curPos[0], curPos[1]};
            switch (curDir)
            {
                case Facing.NORTH:
                    curPosNew[1]--;
                    break;
                case Facing.EAST:
                    curPosNew[0]++;
                    break;
                case Facing.SOUTH:
                    curPosNew[1]++;
                    break;
                case Facing.WEST:
                    curPosNew[0]--;
                    break;
                default: break;
            }
            curPos = checkForObstacles(curPosNew);
        }

        private void leftMove()
        {
            int[] curPosNew = new int[] { curPos[0], curPos[1] };
            switch (curDir)
            {
                case Facing.NORTH:
                    curPosNew[0]--;
                    break;
                case Facing.EAST:
                    curPosNew[1]--;
                    break;
                case Facing.SOUTH:
                    curPosNew[0]++;
                    break;
                case Facing.WEST:
                    curPosNew[1]++;
                    break;
                default: break;
            }
            curPos = checkForObstacles(curPosNew);
        }

        private void rightMove()
        {
            int[] curPosNew = new int[] { curPos[0], curPos[1] };
            switch (curDir)
            {
                case Facing.NORTH:
                    curPosNew[0]++;
                    break;
                case Facing.EAST:
                    curPosNew[1]++;
                    break;
                case Facing.SOUTH:
                    curPosNew[0]--;
                    break;
                case Facing.WEST:
                    curPosNew[1]--;
                    break;
                default: break;
            }
            curPos = checkForObstacles(curPosNew);
        }

        private int[] moveRobot(Direction d)
        {
            switch(d)
            {
                case Direction.FORWARD:
                    forwardMove();
                    break;
                case Direction.LEFT:
                    leftMove();
                    break;
                case Direction.RIGHT:
                    rightMove();
                    break;
                default: break;
            }
            Console.WriteLine(curPos[0] + ", " + curPos[1]);
            return curPos;
        }

        private void traverseGrid()
        {
            foreach (char c in moveSeq)
            {
                if (c.Equals('F')) { moveRobot(Direction.FORWARD); continue; }
                if (c.Equals('L')) { moveRobot(Direction.LEFT); continue; }
                if (c.Equals('R')) { moveRobot(Direction.RIGHT); continue; }
                Console.WriteLine("Wrong format: '" + c + "', command ignored!"); 
            }
        }

        public GenericRobot(int[] startingPos, Facing startingDir, string moveSequence, Stack<Obstacle> obs, Stack<Node> nodes)
        {
            curPos = startingPos; // set starting location
            curDir = startingDir; //                                    and direction
            moveSeq = moveSequence;
            obstacles = obs;
            nodeStack = nodes;
            traverseGrid();
        }
    }
}
