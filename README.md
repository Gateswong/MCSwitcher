MineCraft Switcher
========
MCSwitcher is a simple tool that can easily switch between multiple Minecraft clients, especially useful when you have a lot Minecraft clients that are in different versions, or with different Mods.

Put all your Minecraft clients' `.minecraft` folders into `clients`, rename them to proper ones(e.g. `myMCMods-1.4.7` or `ServerOfJohnDoe-1.2.5`), MCSwitcher will list them out for you to choose.

Usage
--------
After start, you can see two columns on the main window. In the left one, all the possible clients are listed, and on the right are all the configured loaders.

To use a specific client, just click to check it in the left column, then choose a loader at the right.

How it works
--------
MCSwitcher scans the default Minecraft install folder and the `clients` folder at startup, list all sub-folders at the left of its screen. Every sub-folder in `clients` is considered as a sigle Minecraft client, witch should be the same as the `.minecraft` folder in normal installations.

By clicking a specific loader, MCSwitcher creates a [Symbolic Link](http://en.wikipedia.org/wiki/Symbolic_link) from the root folder to the client's, like this: `MCSwitcher/.minecraft -> MCSwitcher/clients/myMCMods-1.4.7`.

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

Known Issue
--------
When starting `Minecraft` while there is one doesn't exited. The symbol link may failed to be created. If happends, follow this:

* Del the folder `%AppData%\.minecraft` and `<MCSwitcher folder>\.minecraft`
* Run Mcswitcher.exe

----

MineCraft 切换器
========
MCSwitcher 是一个用于在多个 Minecraft 客户端之间快速进行切换的小工具，在同时拥有多个不同版本或加载了不同 Mods 的客户端时显得尤为方便。

只要将所有客户端的 `.minecraft` 文件夹放入 `clients` 中并重命名为便于识别的名称（如 `本地用模组包-1.4.7` 或 `基友生存服-1.2.5` 等），MCSwitcher 会自动将其列出以供选择。

使用
--------
程序启动后，主窗口分为两栏。左栏中列出所有可能的客户端，右栏则是所有配置好的启动器。

要启动某一客户端，只需在左栏中选中相应客户端，再在右栏中选择适当的启动器即可。

工作原理
--------
MCSwitcher 启动时会扫描 Minecraft 的默认安装目录和 `clients` 目录，将所有的二级目录名列在主窗口左栏中。在 `clients` 目录中的每一个子目录都被视作一个 Minecraft 客户端，其内容应当同正常安装中的 `.minecraft` 目录相一致。

在点击某一启动器时，MCSwitcher 创建一个从程序根目录指向客户端目录的[符号链接](http://zh.wikipedia.org/zh-cn/%E7%AC%A6%E5%8F%B7%E9%93%BE%E6%8E%A5)，形如：`MCSwitcher/.minecraft -> MCSwitcher/clients/本地用模组包-1.4.7`。

需要注意，这一方法在 `exFAT` 文件系统上无效，而如果在 Windows 8 中运行 MCSwitcher 则需要提供管理员权限。

启动器与 "buttons" 文件
--------
MCSwitcher 使用 `buttons` 文件来配置启动器按钮，只需要将所有的启动器置于 MCSwitcher 目录中即可。

在更多的情况下，需要手动创建和编辑 `buttons` 文件，否则可能没有可用的启动器。

**启动器请自行设法获取。**

配置文件的简单示例如下：

    <?xml version="1.0" ?>
    <Buttons>
        <Button Text="Minecraft 官方启动器" Cmd="Minecraft.exe" />
        <Button Text="使用 MCPatcher 安装模组" Cmd="MCPatcher.exe" />
    </Buttons>

已知问题
--------
有一种情况下：当通过MCSwitcher运行的现有的`Minecraft`未退出前开启了新的启动操作，可能会导致符号链接建立失败。如果出现了此问题，请按照以下方式操作：

* 删除`%AppData%\.minecraft`文件夹和`<MCSwitcher程序目录>\.minecraft`文件夹
* 重新启动MCSwitcher
