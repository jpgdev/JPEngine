### TODOS

- Add a global (to the solution) 'libs' directory for all externals dll.

- Implements Unit Tests for current functionalities
	- EntityManager
		- Events
		- Entity Handling
	
	- Entity
		- Events
		- Components Handling
		
	- Components
	
	- WindowManager 
		- FormWindowManager
		- OpenTKWindowManager
		- BaseWindowManager
		
- Console
	- Implement Selection
	
	- Implement Delete key
	
	- Implement Scrollbar
		- ConsoleOptions.IsScrollable
		- ConsoleOptions.ScrollUpKey
		- ConsoleOptions.ScrollDownKey		
		
- Entity & Component IClonable (or Copy Constructor?)

- Call the Start() function only when the Component is Enabled, otherwise subscribe to the OnEnabledChanged event to do it the first time it is enabled

- Add Events in Entity
	- OnComponentAdded
	- OnComponentRemoved

- Add Events in EntityManager
	- OnComponentAdded
	- OnComponentRemoved
	
- Implement Scenes

- Implement Systems 
	- Loop in Entitymanager
	- System Order
	- Constant Systems (ex. PhysicsSystem in always last and is directly accessible (Engine.Entities.Physics)

- Implement PhysicsSystem
	- Make a FarseersEngine wrapper, but keep an interface so we can implement our own?

- Find a way to load Resources (Fonts, Textures etc..) directly in the engine, without relying on the game and its ContentManager

- SpriteManager
	- Make a wrapper for all the SpriteBatch.Draw calls, do not return the _spriteBatch instance.
	
	- Handle a HUD/GUI (maybe another SpriteBatch.Begin(), ..Draw(), ..End() when the layer is DrawingLayer.GUI ?)
	
		-  and handle multiples layers of GUI too

-  Implement a Lighting engine ([Krypton XNA](https://krypton.codeplex.com/))
