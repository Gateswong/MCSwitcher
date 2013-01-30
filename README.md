MineCraft Switcher
========
MCSwitcher is a simple tool that can easily switch between multiple Minecraft clients, especially useful when you have a lot Minecraft clients that are in different versions, or with different Mods.

Put all your Minecraft clients' `.minecraft` folders into `clients`, rename them to proper ones(e.g. `myMCMods-1.4.7` & `ServerOfJohnDoe-1.2.5`), MCSwitcher will list them out for you to choose.

Usage
--------
MCSwitcher scans the `clients` folder at startup, list all sub-folders at the left of its screen. Every sub-folder in `clients` is considered as a sigle Minecraft client, witch should be the same as the `.minecraft` folder in normal installations.

To use a specific client, just click to check it in the left of screen, and choose a loader at the right. MCSwitcher creates a [Symbolic Link](http://en.wikipedia.org/wiki/Symbolic_link) from the root folder to the client's, like this: `MCSwitcher/.minecraft -> MCSwitcher/clients/myMCMods-1.4.7`.

This seems not work on `exFAT` filesystem, and you need to run MCSwitcher as Administrator if you're using Windows 8.

The loaders & The "buttons" file
--------
MCSwitcher uses a `buttons` file to configure the loader buttons, just put all the loaders at the root of your MCSwitcher folder.

You may need to manually write a `buttons` file, otherwise there would be no loader for you to choose.

**You need to get loaders by yourself.**

Here is a simple example:

    <?xml version="1.0" ?>
    <Buttons>
        <Button Text="Minecraft Official Loader" Cmd="Minecraft.exe" />
        <Button Text="Patch Mods with MCPatcher" Cmd="MCPatcher.exe" />
    </Buttons>
