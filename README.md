# tactics-game
prototype of a grid-based, turn-based tactics game, plays somewhat similar to Fire Emblem games. The art assets and UI of the project are temporary.

I have decided to refactor the code into smaller self-contained units that I will create sepereate repos for. This will ensure no dependancy between the units, and allow for easier testing.
The repo for the RPG segment of the code can be found here: https://github.com/Rahiro1/tactics-game-character, this comes with a sample unity project to show and test aspects of the stats system.

Please feel free to play the prototype at https://play.unity.com/en/games/10c580e5-b18a-499c-b080-4f0043cf122b/tactics-rpg-prototype . Feedback is welcome. 

This project is a work in progress and is primarily for me to practise coding a complete project and learn what to watch out for while doing so. It is based on the game series Fire Emblem but with enough distinct features to, I feel, be considered it's own game. 

New Features
- Armour system (reduces damage from physical attacks, but can be depleted by rending attacks)
- Battalion system (you can see, by bringing up an enemies stats screen, how enemy AI will change and which enemies are linked together)
- Offence/Defence system (Units have stats which differentiate how they perform when attacking vs defending from an attack)
- Dual Weapons (Weapons with two or more different weapon rank requirements so a unit can be good with a javelin but not a glaive) 

TODO -
- Dialogue system
- Story Events
- Training Precombat Option
- Forge system (low priority)
- Rework Armour (I think armour items add too much unneccessary complexity - maybe innate forgable armour items that cannot be unequipped?)
- Difficulty Options (low priority)
- UI overhaul
- Add more Audio
- Add levels 6&7 (not included in the code here)
- Add control schemes for keyboard and controler
- Graphics overhaul (change all sprites and add in scope for animations)
- Add in all class and item Scriptable Objects with guesses for what the statistics should be to get a sense of game feel  
