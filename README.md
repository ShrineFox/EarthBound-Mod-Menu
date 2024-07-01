![](https://i.imgur.com/eL0DBtP.png)  
**EarthBound Mod Menu** is a mod for the Super Nintendo RPG "EarthBound."  
Play EarthBound YOUR way!  
  
This mod collects several quality of life enhancements, some made by myself, some made by the PK Hack community.  
It's open source, so anyone can use parts of it for their own mods, or create their own derivatives.  
## How to Install
Download ``earthbound_mod_menu.ips`` from the [Releases page](https://github.com/ShrineFox/EarthBound-Mod-Menu/releases) and use a tool such as this [online ROM patcher](https://www.marcrobledo.com/RomPatcher.js/) or an offline version such as [LunarIPS](https://www.romhacking.net/utilities/240/) or [Coilsnake](https://pk-hack.github.io/CoilSnake/) to apply it to your ``EarthBound.smc`` ROM.

# Features
These are things that are built-in.  
- Hold Y in the overworld to walk faster or automatically advance texboxes  
- No periodic calls from your dad urging you to save and quit  
- Faster swim/climbing speed and transitions between rooms  
- Check/talk To by pressing A near NPCs and objects, like in MOTHER 3  
- Equip items from the Goods menu  
- Enemies can miss while crying  
- PSI is renamed to PK, like MOTHER 3

# Optional Features
These are things you can toggle from the New Game/Load Save screen, or at any point from the main menu.
- **Key Items**: Separate quest items from your inventory, just like MOTHER 3  
- **Mod Menu**: Custom menu for changing party members, health, stats, location, items, BGM, starting battles or events, saving etc.
- **No Photo Guy**: Significantly shortened Photo Guy cutscenes, just Ness doing a peace sign and the shutter effect (takes ~2 seconds)
- **Chaos Mode**: Random effects can happen while playing. Warps, battles, visual distortions, or shuffled party members.
- **Unrestricted Bike**: Allows you to use the Bicycle item anywhere, even when you have party members following you (temporarily reverts party to Ness only)
- **Skip Lvlup Text**: Don't get notified of new moves unlocked or level increases at the end of battle.
- **Easy Deaths**: Fully revive and heal party after a game over, without taking half your money.
- **Fast Saving**: Shortens the dialogue when calling your dad on the phone.
- **No Homesickness**: Prevents Ness from becoming homesick.

# Known Bugs (& workarounds)
Not everything is fully tested, so let me know if you discover more by [opening a GitHub Issue](https://github.com/ShrineFox/EarthBound-Mod-Menu/issues/new).
- When using the bike, Ness will be shown as the character riding it regardless of party leader.
- The Attract Mode on the title screen does not play (this is because for some reason it hangs halfway through).
- Configuring optional mod settings may sometimes freeze the game when the third option is selected in a busy area.
- Chaos Mode has a mild potential to sometimes crash the game.
- Sometimes it says the wrong character received or used a key item, or the item's name displays incorrectly.
- When toggling off the Key Items mod, you may end up lacking the items required to advance. Use the Mod Menu to give yourself key items as needed.
- Sometimes the spacing between "PK" and the name of an attack may be incorrect in battle.
- Key Item inventory is currently based on who is in your party, so you may find yourself unable to advance if you remove i.e. Ness or Jeff.

## Credits
- The **PK Hack Discord Server** for all their kind assistance. You guys PK Rock <3
- **Mr. Tenda**: Creating [CoilSnake](https://github.com/pk-hack/CoilSnake)
- **Mr. Accident**: Creating the [CCScript Compiler](https://github.com/tripped/ccscript_legacy)
- **JTolmar**: Y Button run with momentum, fast movement in water, Repel Sandwich, changing text speed on the fly, custom save/load/newgame menu
- **phoenixbound**: Name printing commands, helped design/debug custom menus with variable options and descriptions
- **Catador**: Toggle-able noclip option for Mod Menu, change bootup music
- **phoenixbound, ShadowOne333, vince94, Chaz**: Goods Equip Menu
- **jtolmar, phoenixbound, and Catador**: Fast Doors
- **cooprocks123e**: Ability to call battle backgrounds from anywhere, CCExpand for custom control codes, extended flags
- **ShadowOne333**: From [MaternalBound-Redux](https://github.com/ShadowOne333/MaternalBound-Redux): Changed controls, crying also affects enemies, lower HP/PP windows one tile, PSI => PK, restore spank sfx
- **H.S.**: asm65816 and ASMRef for assembly patch support via CCScript  
  
## About this Mod's Development
For developers, see [the wiki](https://github.com/ShrineFox/EarthBound-Mod-Menu/wiki) for information on how the mod's codebase works.  
  
I started making an [EarthBound Mod Menu hack](https://www.youtube.com/watch?v=rNmggC3eQz4) way back in September 2015, intended as a more user friendly version of the game's cryptic Debug Menu.  
When EarthBound re-released on the Switch, replaying it inspired me to revisit the modding scene. Thanks to the PK Hack Discord Server, 
I discovered tons of quality of life patches made by the community that alleviate a lot of my personal gripes with the game.  
Watching [AVGN's review of EarthBound](https://www.youtube.com/watch?v=LZ5nX0FTH6Q) also gave me ideas for how the gameplay could be improved, citing limited inventory space, 
inefficient shopping and storage, the uselessness of the bike... All things that I already knew how to correct through [the game's powerful scripting engine](https://github.com/pk-hack/CoilSnake/wiki/CCScript).  
  
While other hacks aim to re-localize EarthBound or retool several systems of the game, I'm aiming to keep it as vanilla as possible outside these small enhancements.  
And any HUGE changes, like the Key Items, Mod Menu, Photo Guy, can be turned on and off at will from within the game to suit a player's preferences.  
I feel fortunate, being able to create my own definitive version of one of my most beloved games, all thanks to an incredibly dedicated fanbase that continues to push the game to its limits.
