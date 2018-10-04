# CS DeskBand
A Library to create [DeskBands](https://msdn.microsoft.com/en-us/library/windows/desktop/cc144099(v=vs.85).aspx) on windows using C#. Deskbands are toolbars that are docked on the taskbar and provide additional functionality to an otherwise unused space.

CSDeskBand makes it easy to create a deskband using Winforms or WPF.

## Screenshots
![Example 1](images/ex1.png)

![Example 2](images/ex2.png)

_Images taken from the sample projects_

## Usage

### Installation
Nuget packages are available here:
- [CSDeskBand.Win](https://www.nuget.org/packages/CSDeskBand.Win)
- [CSDeskBand.Wpf](https://www.nuget.org/packages/CSDeskBand.Wpf)

### Usage
Use `CSDeskBandWin` for winforms or `CSDeskBandWpf` for wpf
- For a winforms usercontrol, inherit the `CSDeskBandWin` base class. _See Sample.Win_
- For a wpf usercontol, Set `CSdeskBandWpf` as the root element in the XAML. _See Sample.Wpf_

Add `[ComVisible(true)]`, `[Guid("xx-xx-xx-xx-xx")]`, `[CSDeskBandRegistration()]` attributes to the class.
The `CSDeskBandRegistration` attribute allows you to configure:
- Name : The name of the deskband shown in the toolbars menu
- ShowDeskBand : True if the deskband should be shown automatically after registration


```C#
using CSDeskBand.Win;
using CSDeskBand;

[ComVisible(true)]
[Guid("5731FC61-8530-404C-86C1-86CCB8738D06")]
[CSDeskBandRegistration(Name = "Sample Winforms Deskband", ShowDeskBand = True)]
public partial class UserControl1 : CSDeskBandWin
{
...
```

Now you are ready to start working on it like a normal user control.

**Check the [Wiki](https://github.com/dsafa/CSDeskBand/wiki) for more details.**
**Patch notes will be in the [release](https://github.com/dsafa/CSDeskBand/releases) page**

### Deskband Installation
You need to start an elevated command prompt and be able to use `regasm.exe`. Make sure that you use the correct version of regasm that matches your platform (x86/x64).
```
cd Sample.Win\bin\Debug

regasm /codebase Sample.Win.dll
```
The `/codebase` switch will add the path of the dll into the registry entry.

Alternatively, register the assemblies into the Global Assembly Cache.
```
gacutil -i CSDeskBand.dll
gacutil -i CSDeskBand.Win.dll
gacutil -i Sample.Win.dll
regasm Sample.Win.dll
```
_Note that GAC installation requires the assemblies to be [Strong-Named](https://docs.microsoft.com/en-us/dotnet/framework/app-domains/strong-named-assemblies)_

Here is an [example .bat file](./tools/install-example.bat) for installing a deskband.

## Examples
There are example deskbands included for Winforms and WPF in the [Sample.Win](https://github.com/dsafa/CSDeskBand/tree/master/src/Sample.Win) and [Sample.Wpf](https://github.com/dsafa/CSDeskBand/tree/master/src/Sample.Wpf) projects.

### Compatibility
Tested on Windows 10 x64
