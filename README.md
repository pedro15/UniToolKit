<div align="center">
    <a href="https://github.com/pedro15/UniToolKit/blob/master/LICENSE"><img alt="GitHub license" src="https://img.shields.io/github/license/pedro15/UniToolKit.svg"></a>
    <a href="https://www.paypal.me/pedrojdm/5"><img alt="Paypal Donate" src="https://img.shields.io/badge/Paypal-Donate-blue.svg"></a>
</div>

# UniToolKit
Utility toolbox for unity game development.

This libray contains helpful modules that are useful for unity game development.

Aviable Modules:

- **Safe PlayerPrefs:** Uses unity's PlayerPrefs system as a base, but it encrypts them. It also adds support for some unity data types like:
    - Vector3
    - Vector2
    - Quaternion
    - Color

- **Safe Structs**: Secured Data types to prevent memory hacking (SafeFloat, SafeInt, SafeString,SafeBool)

- **Serialization**: Serialization Modules with encryption option. Supported Modules are:
    - Binary
    - JSON
    - XML

- **Gameplay**: Some Extention methods that maybe would be useful on gameplay development.

- **Utility**: Utility classes Such as an great Singleton Pattern implementation that can be really useful on your game systems.

# Installation

To install this project as a Git dependency using the Unity Package Manager, add the following line to your project's manifest.json (unity 2018.3 or higher):

`"com.github.pedro15.unitoolkit": "https://github.com/pedro15/UniToolKit.git"`
