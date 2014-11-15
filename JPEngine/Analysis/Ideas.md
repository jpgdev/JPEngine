
# Ideas & Notes

### Implement TDD (with Unit Tests)
- Continue adding tests for already implemented features
	- Entity Manager
	- Entity
	- Components
	- ...

###  Entity/Components System

- [Interesting link about ECS](http://gameprogrammingpatterns.com/component.html)
- [XNA ECS Implementation Example](https://xnaentitycomponents.codeplex.com/)	

- Entity & Components
	- Add a <b>Clone()</b> for Entity & Components
		- Useful to simulate Prefabs (à la Unity).
		
	- Implement events :
		- <b>Entity</b> 
			- OnComponentAdded, OnComponentRemoved
		- <b>EntityManager</b> 
			- OnComponentAdded, OnComponentRemoved 
			- OnEntityAdded, OnEntityRemoved

- Make Basic components
 
	- CircleCollider &  BoxCollider (Base class -> Abstract/Interface Collider?)

		- example usage : gameObject1.collider.IsColliding(gameObject2.collider)	
			- Override for each type of collider?

		- Handle basic physics
			- Collision
			- Collision Response
			- IsSolid? -> Only trigger an event

		- Maybe a RigidBody?

	- Moveable 
		- Tie it to the Physics? Apply velocity? OR Leave the chose to Move it with Transform.Position or with the Physics.

	- Transform Component
		- Use a Vector3D for the position to add a Z value? Which the DrawableComponent use?
		- Add an Origin property for the rotation and etc.. instead of having it in DrawableSpriteComponent which makes no sense if we want to use it outside of drawing?
	
#### Systems

- Implements multiples Systems handled by the EntityManager

	##### Physics System 	
		
	- Basic Physics for collisions, apply force, jump?, gravity (y-direction pull force) etc..
		- Check out :
			- [Game physics 101](http://www.rodedev.com/tutorials/gamephysics/) 	
			- [How to Create a Custom 2D Physics Engine: The Basics and Impulse  Resolution](http://gamedevelopment.tutsplus.com/tutorials/how-to-create-a-custom-2d-physics-engine-the-basics-and-impulse-resolution--gamedev-6331 )
			- [Physics Engine Tutorial](http://physics.gac.edu/~miller/jterm_2013/physics_engine_tutorial.html)
				
		- Implement Box2D / [Farseer Physics](https://farseerphysics.codeplex.com/)
			- [Artemis + Box2d](http://blog.gemserk.com/2012/02/02/how-we-use-box2d-with-artemis/)
			- [Another try at implementing in ECS](http://www.aymericlamboley.fr/blog/an-entity-component-systems-attempt-using-box2d/)
		- Make a wrapper ?
		
- Manage the order of the system are Updated/Drawn: 
	- Replace the OrderUpdate value?
	- Ex: Inputs, then Movement, then Physics


### Console & Lua interpretter

- Split up the ScriptConsole.cs
	- ConsoleRenderer
	- ConsoleInputProcessor
	- and keep ScriptConsole as a main.
	
- Enable an easier way to setup the options (ConsoleOptions object?)

- Add a way to load the SpriteFont in the Engine, not from the game.Content
	- Usefull for the Console Font and others 

- [LUA in C# -> NLua](https://github.com/NLua/NLua)
- [How to: Lua + XNA](http://xnacoding.blogspot.ca/2010/07/how-to-lua-xna.html)		
- [Console in XNA](http://gamedev.stackexchange.com/questions/45107/input-output-console-window-in-xna)
- [Console implementation in XNA](https://code.google.com/r/jameswalkoski-xnagameconsole-xna4/source/browse)
- [Use LUA with XNA as events](http://www.gamedev.net/topic/612571-using-lua-with-xna-as-events/)
- [Scripting with LUA in c#](http://www.godpatterns.com/2006/05/scripting-with-lua-in-c.html)

### Screen Manager / Scenes (à la Unity)

- Create a Screen/Scene manager
	- Create a SceneManager and the abstract Scene class

	- Use one EntityManager per scene/screen?
		- Or split the entities in the EntityManager per scene?

- void OnUnload & OnLoad
	- list of names of Textures, Sounds etc.. to load/unload when changing Screen
	- Same for the mapping engine?

### Sprite manager

- <b>Take the Transform.Position.Y + Height (+ HANLE ROTATION & SCALING!) into account for Z-Index correctly</b>

- Keep a list of sprites to draw?
	- A Dictionnary with a Name as Key, and a Pair of Sprite & Layer as Value.

- Create a Layer system

	- Using the Z-index
		- ex. 
			- Layer 0 = GUI 
				- Layer 1...n = GameObjects 
				- Layer n+1 = Ground

	- The layers occupy brackets of the z-index
		- ex: 
			- Layer 0: Z-index [0-255]
			- Layer 0: Z-index [256-512]
			- etc...
