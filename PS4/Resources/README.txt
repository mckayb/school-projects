Name: McKay Broderick
Date: October 1, 2015


Initial Thoughts/Design:
 The hardest part is clearly going to be setting the contents of a cell just due to making sure
we are always keeping track of the dependencies. 

I should be able to combine all 3 of the SetCellContents into a single function,
since string and double inputs will be identical, and then I just need to add special checks if the content is
a formula or not. But if it gets too cumbersome, I can keep it broken out.

Finally, I will have to implement a lookup function so that I can evaluate formulas properly.
This should just be looking at the current nonempty cells, checking if those cells have a match
with the name that is passed in, and if it does, then making sure the value of the cell is a double.
If it is, then just return it. Otherwise, I'm going to return 0, since that seems to be what 
Excel does for empty cells and it seems reasonable.

PS2/PS3: I updated these projects to use XML Comments and made small changes for bug fixes, but the ones
that are currently in the repository should be the ones that I am using.

Notes for Graders:
	1 - How in the world do I view the unit tests that you guys test these things against
	( for past projects, not this one obviously )?. I would like to be able to view them and be able to go
	fix my code, but I can't seem to find anything. If you could send me a message, or let me know in the
	feedback, that would be perfect.
	2 - I was unsure on what to do for the lookup function of the formula and validation, etc.
	I basically just had it return zero if the variable didn't exist or was a string when using
	the validation. I hope that's alright, but if not, some clarification in the comments would be nice.