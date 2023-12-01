Dungeon game was also a very fun module. However, for some reason I managed to break the game before delivering and I could not fix it.

For some reason you can't choose either rogue or warrior in the menu. It's a simple if(rogue) do this) if(warrior) do this. But for some reason it does not work :c Currently the attributes are hardcoded. 

In the settings tab I wanted to implement language. It would be as simple as copy pasting from my previous assignments but I did not have time to implement it. 

Currently in the game you can attack, dmg, and kill mobs. While also taking damage or die. The mobs drops gold.
There are NPCs, with randomized types and level. Meaning, if you get a merchant you get gold, if its a warrior you get strength etc. I also wanted to scale the amount you get with your level, the higher or lower your level, the higher or lower the amount of stats you get. If you go to the NPC twice, it does get annoyed and leaves you. 

There are two marks for items you can interact with. ? is treasure or a side quest. ! is a progression items. The plan for the ! was to have to instances. If you pick up the ! without killing the mobs, you are prompted to kill the mobs and then go to the boss fight. If you kill the mobs without picking up the !, you are prompted to explore the map until you find it. 

The * symbol is supposed to be a debuff which will interact with you. Either poison for health damage, weakness pot for less strength etc. When interacting with the * it does get removed and you do get the debuff status. However, for some reason it wont damage your health. The plan was to use my poison tick implementation from module 4 so that every move you make the debuff takes place.

module_8_Dungon  
├── .vscode  
├── module_8_dungon.csproj  
├── module_7_dungon.sln  
├── \*.\*  
└── README.md  

About assesment:

Assessment for this project will be based not just on feature completion, but also on your attention to detail, the cleanliness and readability of your code, and the thoughtfulness of your README file reflections. This is your opportunity to demonstrate not just what you've learned, but how you can apply it creatively and effectively in a real programming project.


