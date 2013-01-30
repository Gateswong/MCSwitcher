MineCraft Switcher
========
You may need to manually write a `buttons` file, otherwise there would be no loader for you to choose.

**You need to get loaders by yourself.**

The "buttons" file
--------
MCSwitcher uses a `buttons` file to configure the loader buttons, you may want to put all the loaders at the root of your minecraft folder.

Here is a simple example:

    <?xml version="1.0" ?>
    <Buttons>
        <Button Text="Minecraft Official Loader" Cmd="Minecraft.exe" />
        <Button Text="Patch Mods with MCPatcher" Cmd="MCPatcher.exe" />
    </Buttons>
