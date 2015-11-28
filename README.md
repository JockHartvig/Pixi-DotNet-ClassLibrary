# Pixi-DotNet-ClassLibrary

is a .NET class library with a collection of simple helper classes....

**PixiSimpleConfigurationXml** is a class for handling very simple configuration files in XML format.  

Supports reading, creating and updating simple configuration files in XML format like:
````
<?xml version="1.0" encoding="utf-8"?>
<!--XML Database.-->
<Settings>
  <Item1>123</Item1>
  <Item2>ABC</Item2>
</Settings>
````
Further documentation https://github.com/JockHartvig/Pixi-DotNet-ClassLibrary/wiki/PixiSimpleConfigurationXml

Class library is available on NuGet.
Package name Pixi-DotNet-ClassLibrary.

#Releases

## Ver 1.1.0
- Changed return of GetItemValue from Object to string.
- Fixed bug in ReadConfigFile allways returning space.
- Added Sample application 1

## Ver 1.0.1
- FilePath changed to read-only and added unit test for this.
- Reorganised source files in folders pr class.
- Changed Unit tests.

## Ver 1.0.0.0
Initial release.

