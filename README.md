# BlackJack
A simple game of BlackJack


### Thought Process

•	I tried to keep code coupling to a minimum by adding events and simple Scriptable Object Libraries. This allowed me to change the variables I needed without dependencies. 

•	The reason why I used Dictionaries for the cards picked from the deck and the cards in play is so that I could reference their Key to tell which card is which and assign the proper value to the card when instantiated.

•	The card prefab holds the faces so that it can be accessed when instantiated.

•	The HandEvents class was made to try to avoid script dependencies. This allows for multiple things to be called at a given point without having to reference everything. They were added to one static class so that it is easy to extend and referenced when needed. 

•	The reason the cards appear in a circle is just for fun. It calculates in angles the spacing of the cards and gives it an interesting effect.

•	I saved the game by passing all scriptable objects I needed into Jsons so that I could load them easier. I ran into a difficult problem of Dictionary serialization which could probably be bypassed with the Odin plugin.

•	The game goes one round as a demo, to extend it I would add a continue button after the end of the round, which would then enable the BettingUI and then repeat that loop until the player runs out of money or cashes out (which would be a button as well after each round)¸and then enable the end game buttons to replay or go back to the main menu.

## Known Issues

•	Later in development I ran into some issues that I didn’t manage to fix properly due to time constraints. 

•	The loading of the game deletes the dictionaries as they are not serialized. This causes the Cards In Play to be deleted and the dealer is not able to flip his first card when his turn comes. This was “fixed” (not really…) by instantiating the card with the same value on top of the face down one.

•	The cards when loaded don’t appear on the same place they left off. This should be fixed by changing their start position on the Dealer/Player script.

•	The loading could be improved by changing the now used LoadedGame bool into something more beneficial, since this causes unnecessary If checks.

•	In the build version, the game object cards in play do not appear due to serialization issues.
