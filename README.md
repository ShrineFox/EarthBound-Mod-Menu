# EarthBound-Mod-Menu
In-game trainer for EarthBound (SNES). Made using CoilSnake.
## Credits
- The **PK Hack Discord Server** for all their kind assistance. You guys PK Rock <3
- **Mr. Tenda**: Creating [CoilSnake](https://github.com/pk-hack/CoilSnake)
- **Mr. Accident**: Creating the [CCScript Compiler](https://github.com/tripped/ccscript_legacy)
- **JTolmar**: Y Button run with momentum, fast movement in water, Repel Sandwich, changing text speed on the fly, custom save/load/newgame menu
- **phoenixbound**: Name printing commands, helped design/debug custom menus with variable options and descriptions
- **Catador**: Toggle-able noclip option for Mod Menu, change bootup music
- **phoenixbound, ShadowOne333, vince94, Chaz**: Goods Equip Menu
- **jtolmar, phoenixbound, and Catador**: Fast Doors
- **cooprocks123e**: Ability to call battle backgrounds from anywhere, CCExpand for custom control codes
- **ShadowOne333**: From [MaternalBound-Redux](https://github.com/ShadowOne333/MaternalBound-Redux): Changed controls, crying also affects enemies, lower HP/PP windows one tile, PSI => PK, restore spank sfx
- **H.S.**: asm65816 and ASMRef for assembly patch support via CCScript
- **Rufus**: ebpp, ebpp_mem, and ebpp_std for custom flags, ASM commmands, window labels and more
## Built-in Features
These features are not toggleable in-game, but can be changed by editing the CoilSnake project and rebuilding the ROM.
- Run button when holding Y (faster than bike/skip sandwich)
- Check/talk To by pressing A near NPCs and objects, like in MOTHER 3
- Custom configuration menu when starting a new game or loading a save
- Faster swim/climbing speed
- Faster opening logos
- Skipped Gas Station screen
- Skip Sandwich repels enemy spawns
- Equip items from the Goods menu
- Transitions between areas load much faster
- Enemies can miss while crying
- PSI is renamed to PK, like MOTHER 3
## Toggleable Features
When starting a new game or loading a save, you can turn these on or off.
- **Key Items**: Separate quest items from your inventory, just like MOTHER 3
- **Mod Menu**: Custom menu for changing party members, health, stats, location, items, BGM, starting battles or events, saving etc.
- **No Photo Guy**: Significantly shortened Photo Guy cutscenes, just Ness doing a peace sign and the shutter effect (takes ~2 seconds)
- **No Dad Calls**: Stops periodic calls from your dad asking you to stop playing
- **Unrestricted Bike**: Allows you to use the Bicycle item anywhere, even when you have party members following you (temporarily reverts party to Ness only)
- **Auto-Advance**: Skips through Level Up messages automatically for you, no more mashing the A button!
## About this Mod
I started making an [EarthBound Mod Menu hack](https://www.youtube.com/watch?v=rNmggC3eQz4) way back in September 2015, intended as a more user friendly version of the game's cryptic Debug Menu.  
When EarthBound re-released on the Switch, replaying it inspired me to revisit the modding scene. Thanks to the PK Hack Discord Server, 
I discovered tons of quality of life patches made by the community that alleviate a lot of my personal gripes with the game.  
Watching [AVGN's review of EarthBound](https://www.youtube.com/watch?v=LZ5nX0FTH6Q) also gave me ideas for how the gameplay could be improved, citing limited inventory space, 
inefficient shopping and storage, the uselessness of the bike... All things that I already knew how to correct through [the game's powerful scripting engine](https://github.com/pk-hack/CoilSnake/wiki/CCScript).  
  
While other hacks aim to re-localize EarthBound or retool several systems of the game, I'm aiming to keep it as vanilla as possible outside these small enhancements.  
And any HUGE changes, like the Key Items, Mod Menu, Photo Guy, can be turned on and off at will from within the game to suit a player's preferences.  
I feel fortunate, being able to create my own definitive version of one of my most beloved games, all thanks to an incredibly dedicated fanbase that continues to push the game to its limits.
