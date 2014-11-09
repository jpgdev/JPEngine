
# TODOS

- Entity/Components System
	- [Interesting link about ECS](http://gameprogrammingpatterns.com/component.html)
	- [XNA ECS Implementation Example](https://xnaentitycomponents.codeplex.com/)	
	- Make Basic components
		- Collider (Circle and Rectangle)
		- Moveable / player
	- <b>Finish a basic version of SpriteManager or Drawable something in EntityManager which manages the layers & etc...</b>
		- Use the Sprite class as an example.
		- Who will manage the layers/z-index?
		
	- Implements multiples Systems handled by the EntityManager
		- PhysicsSystem 	
		
			- Basic Physics for collisions, apply force, jump?, gravity etc..
				- Check out :
					- [Game physics 101](http://www.rodedev.com/tutorials/gamephysics/) 	
					- [How to Create a Custom 2D Physics Engine: The Basics and Impulse  Resolution](http://gamedevelopment.tutsplus.com/tutorials/how-to-create-a-custom-2d-physics-engine-the-basics-and-impulse-resolution--gamedev-6331 )
					- [Physics Engine Tutorial](http://physics.gac.edu/~miller/jterm_2013/physics_engine_tutorial.html)
	
			- Usage example: Physics.IsColliding(gameObject1.collider, gameObject2.collider) or gameObject1.collider.IsColliding(gameObject2.collider)
	
				- Using a Collider/RigidBody component (à la Unity)?
				- Circle Colliders and Rectangle Collider (Rectangle already exists, base it on that)
				
		- Manage the order of the system are Updated/Drawn: 
			- Ex: Inputs, then Movement, then Physics
- Input Manager
	- Manage all input calls and provides methods like IsClicked(button1), IsDown(button), etc.. 

# Ideas

#### Texture manager:

-Loader les textures juste quand on en a besoin:
	- ex: Entre dans une map, on load les textures d'après les strings (nom des texture). 

#### Sprite manager

- <b>This may be replaced by a SpriteSystem handling the Drawable components with the ECS.</b>
- Create a Layer system?
	- Using the multiple sprites batches or a Z-index
  	- Handle the Z index depending of the Height, Y-position
		- ex. 
			- Layer 0 = GUI 
		    	- Layer 1...n = GameObjects 
		    	- Layer n+1 = Ground
	- The layers occupy brackets of the z-index
		- ex: 
			- Layer 0: Z-index [0-255]
			- Layer 0: Z-index [256-512]
			- etc...
- Keep a list of sprites to draw?
	- Dict < string, Pair < Sprite, Layer > > => < name, < Sprite, Layer > >
	



#### Music & Sound Manager
- Create a parent AudioManager
	- Contains a MusicManager and a SoundManager, both inherits from ResourcesManager
	- Using it (choices):
 		- Engine.Audio.Songs.Play("song name")
		- Engine.Audio.PlaySong("song name")
		- Engine.Audio.Play("song name", AudioType.Song)

#### Screen Manager

- Create a Screen/Scene manager
	- Game Screen
	- Menu Screen
	- Pause Screen
	- Defaults screen?

- void OnUnload & OnLoad
	- list of names of Textures, Sounds etc.. to load/unload when changing Screen
	- Same for the mapping engine?

#### Generique :
	
- Implanter des mutex pour toutes les classes statiques ?
	- Va juste être utile si je multi Thread le tout though
