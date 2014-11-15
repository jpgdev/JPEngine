### TODOS

- Implements Unit Tests for current functionalities
	- EntityManager
		- Events
		- Entity Handling
	
	- Entity
		- Events
		- Components Handling
		
	- Components
	
	-...
		
- Console
	- Implement Selection
	
	- Implement Delete key
	
	- Implement Scrollbar
		- ConsoleOptions.IsScrollable
		- ConsoleOptions.ScrollUpKey
		- ConsoleOptions.ScrollDownKey
		
	- Split ScriptConsole (but keep this as a "main" class)
		- ConsoleRenderer
		- ConsoleInputProcessor  	
		
- Entity & Component IClonable (or Copy Constructor?)

- Add Events in Entity
	- OnComponentAdded
	- OnComponentRemoved

- Add Events in EntityManager
	- OnComponentAdded
	- OnComponentRemoved	
	
- Implement Scenes

- Implement Systems

- Implement PhysicsSystem
	- Make a FarseersEngine wrapper, but keep an interface so we can implement our own?

- Find a way to load Resources (Fonts, Textures etc..) directly in the engine, without relying on the game and its ContentManager

- SpriteManager
	- Make a wrapper for all the SpriteBatch.Draw calls, do not return the _spriteBatch instance.
	
	- Handle a HUD/GUI (maybe another SpriteBatch.Begin(), ..Draw(), ..End() when the layer is DrawingLayer.GUI ?)
	
		-  and handle multiples layers of GUI too

-  Implement a Lighting engine ([Krypton XNA](https://krypton.codeplex.com/))
-  
