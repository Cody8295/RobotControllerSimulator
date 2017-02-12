Robot Controller Programming Assignment
 
Design a robot controller that will be able to navigate a 2 dimensional grid of known size.  The robot will be given a starting grid location and direction along with a series of commands to control its movement (e.g. LFFFRFFLFFFFRLFFFR)

·       L - Move left one space 
·       R - Move right one space 
·       F - Move forward one space. 

The robot controller will be provided at least the grid dimensions along with some topographical obstructions on the grid.  These will be provided separately so we can change the topography for a given grid. 

Depending on the obstacle at the location how the robot moves will be altered.  Current options are

·       Rock - Can't move to this location 
·       Hole - A hole will have a connected grid location.  The robot will fall into the hole and end up at the connected location facing same direction 
·       Spinner - A spinner will contain a rotation parameter.  The robot will enter the location and be rotated by the parameter amount.  The amount will be in 90 degree increments.  
