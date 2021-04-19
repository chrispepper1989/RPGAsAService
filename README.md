# RPGAsAService
a silly little attempt to distil RPG elements into a service

# Tech Debt
- characters feel like they need to be in some kind of data store, to remove responsibility from battlesystem 
- Trait system needs to be in its own class (remove responsibility from battlesystem)

# Arch questions
- Are triggerd buffs and trait based buffs really the same thing? should they be combined?
- Characters are using a simple DDD approach at the moment but feel they would benefit from some good old OOD encapsulation

# Future Features?
- Plan out how the character JSON would look like and how it would be sent in an endpoint
- create basic numeric tile map that can be displayed in html /webgl (consider making it layered)
- plan out how battles are triggered and how the (turn based?) combat is done via endpoints
- "modifier" system needs expanding to take into account more then just character health
- "Tell" system so characters can be "told" to attack etc must also support chaining e.g. "Tell Joanne To Tell Morag to Attack Centaur"  
- "location" system. effectively a grid representation of where key items / locations and characters are


# Intended use

to play about with some basic RPG elements that could be used as a service in a game. 
going to try as text adventure first and see if it can apply to mulitple interfaces. 

Mainly going to be and excuse for me to try out some tech id like to understand more, mainly MongoDb 

# Endpoint design

- Push CreateCharacter (json with attack points, etc)
- Get EncounterEnemy (json return with attack points etc), the query string could have the enemy level  
- Subscribe to map changes
- Push CharacterMoved, takes character name and the direction, location system should take care of rest

# Big Questions:
- What should the front end handle and what should it definitely not handle
- 



