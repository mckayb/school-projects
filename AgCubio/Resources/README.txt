Partner1 Name  // P1: Aidan Tarufelli  (please put in alphabetical order by First name)
Partner2 Name  // P2: McKay Broderick

Repository     // Repository: https://github.com/uofu-cs3500-fall15/00806181
Contribution   // My Contribution:  50% 50%

Design Decisions:
We decided to paint directly on the form, not on a custom panel, using an onPaint event method to 
minimize the flickering. We implemented properties in the Cube class for the left, right, top, and bottom
to help with cube ghosting issues.
There were numerous helper methods in the World class for dealing with specific uids, and updating directly
from the server response.

Features for Grader:
We're still having some issues with dying and allowing the user to reconnect. It seems to be causing some
exception in our paint method, but we haven't been able to track it down.
