
# TODOS

...

# Ideas

#### Texture manager:

-Loader les textures juste quand on en a besoin:
	- ex: Entre dans une map, on load les textures d'après les strings (nom des texture). 


#### Sprite manager
    
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
