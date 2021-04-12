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
- 


