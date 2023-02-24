# Unity PlayerPrefsEx

![npm](https://img.shields.io/npm/v/extensions.unity.playerprefsex) [![openupm](https://img.shields.io/npm/v/extensions.unity.playerprefsex?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/extensions.unity.playerprefsex/) ![License](https://img.shields.io/github/license/IvanMurzak/Unity-PlayerPrefsEx) [![Stand With Ukraine](https://raw.githubusercontent.com/vshymanskyy/StandWithUkraine/main/badges/StandWithUkraine.svg)](https://stand-with-ukraine.pp.ua)

PlayerPrefsEx is a lightweight package that is an extended version of PlayerPrefs from Unity. Under the hood, it uses the same PlayerPrefs system but creates a flexible wrapper for the default system.

### Static API Usage

``` C#
using Extensions.Unity.PlayerPrefsEx;

// Static API Getters                    // Static API Setters
PlayerPrefsEx.GetInt("key");             PlayerPrefsEx.SetInt("key", 10);         
PlayerPrefsEx.GetBool("key");            PlayerPrefsEx.SetBool("key", false);        
PlayerPrefsEx.GetFloat("key");           PlayerPrefsEx.SetFloat("key", 2.123f);       
PlayerPrefsEx.GetString("key");          PlayerPrefsEx.SetString("key", "hello world");      
PlayerPrefsEx.GetBigInt("key");          PlayerPrefsEx.SetBigInt("key", BigInteger.Parse("100"));
PlayerPrefsEx.GetDateTime("key");        PlayerPrefsEx.SetDateTime("key", DateTime.Now);    
PlayerPrefsEx.GetVector2("key");         PlayerPrefsEx.SetVector2("key", Vector2.up);     
PlayerPrefsEx.GetVector2Int("key");      PlayerPrefsEx.SetVector2Int("key", Vector2Int.up);  
PlayerPrefsEx.GetVector3("key");         PlayerPrefsEx.SetVector3("key", Vector3.up);     
PlayerPrefsEx.GetVector3Int("key");      PlayerPrefsEx.SetVector3Int("key", Vector3Int.up);

// Static API Getters Generic            // Static API Setters Generic
PlayerPrefsEx.GetJson<Player>("key");    PlayerPrefsEx.SetJson<Player>("key", new Player());
PlayerPrefsEx.GetJson<Enemy>("key");     PlayerPrefsEx.SetJson<Enemy>("key", new Enemy());
PlayerPrefsEx.GetJson<Data>("key");      PlayerPrefsEx.SetJson<Data>("key", new Data());
```

### Variables API Usage

``` C#
// Variables API
var score = new PlayerPrefsInt("score");
score.Value = 100;

// Variables API Generic
var player = new PlayerPrefsJson<Player>("player");
var enemy = new PlayerPrefsJson<Enemy>("enemy");

var tempPlayer = player.Value; // take value from PlayerPrefs
tempPlayer.Health -= enemy.Value.Damage; // change internal properties
player.Value = tempPlayer; // save back changed instance
```

### Extended default list of types to store

``` C#
PlayerPrefsInt     PlayerPrefsVector2     PlayerPrefsBigInt
PlayerPrefsFloat   PlayerPrefsVector2Int  PlayerPrefsDateTime
PlayerPrefsBool    PlayerPrefsVector3     PlayerPrefsJson<T>
PlayerPrefsString  PlayerPrefsVector3Int  PlayerPrefsEx<T>
```

# Installation 

When you package is distributed, you can install it into any Unity project.

- [Install OpenUPM-CLI](https://github.com/openupm/openupm-cli#installation)
- Open command line in Unity project folder
- Run the command:

``` CLI
openupm add extensions.unity.playerprefsex
```

# Features

 ✔️ Key is encrypted. Encrypted depends on a device. Much more harder for hackers to hack your data. Saved data at one device won't work on another one if someone copied it from device to device. In the same time for UnityEditor the device identifier is a constant. That means data copied between devices could be opened if you work on multiple machines and want to save/sent/load saved data on different machines.

 ✔️ Create variable instance of any PlayerPrefs, work with it as a regular variable

``` C#
var score = PlayerPrefsInt("score");
score.Value = 100;
```

 ✔️ Use generic types for creating PlayerPrefs variables

``` C#
var player = PlayerPrefsJson<Player>("player");
player.Value = new Player();
```

 ✔️ Using multiple variables with the same Type and Key shares data between them.

 ``` C#
 var variableA = new PlayerPrefsString("message");
 var variableB = new PlayerPrefsString("message");
 
 variableA.Value = "hello world";
 
 Debug.Log(variableA.Value); // "hello world";
 Debug.Log(variableB.Value); // "hello world";
 
 // variableA.Value == variableB.Value
 // the same memory spot, just two links 
 ```

 ✔️ Generating unique `Key` for each type. No way to overlap value of another type by same key.

``` C#
var variableInt = new PlayerPrefsInt("SAME_KEY");
var variableString = new PlayerPrefsString("SAME_KEY");

variableInt.value = 100;
variableString.Value = "abcd"

// variableInt.value != variableString.value
// there are dedicated storage for each type, generic inclusive
```

# Unique key per device

PlayerPrefsEx supports simple protection from hackers who change default values for PlayerPrefs. That is approach to use unique key per device keys, simply adding device id to the end of a key. As a result external modification of PlayerPrefs could work only at single device and can't be spread like a hacked build of a game.

> ⚠️ Keep in mind! Cloud safe could not work for single user with multiple devices.

> ⚠️ Highly not recommended to change the feature in runtime. Because data stored under disabled `unique key` feature are unaccessible from enabled `unique key`, the same works in opposite way.

## Activate Unique Key: option 1 (recommended)

Add `PLAYER_PREFS_UNIQUE_KEY` define to a project.
Follow `Edit -> Project Settings -> Player` to add the define.

This approach is better, because you have 100% guarantee that no one PlayerPrefsEx API gonna be used before your setup `Unique Key` option at runtime.

![image](https://user-images.githubusercontent.com/9135028/221062492-6801321e-ee4b-4aad-9eab-ed6da777584b.png)

## Activate Unique Key: option 2 (not recommended)

You can make it in runtime by simply change value of `PlayerPrefsEx.Settings.UniqueKeyPerDevice`.

> ⚠️ Highly not recommended to change the feature in runtime. Because data stored under disabled `unique key` feature are unaccessible from enabled `unique key`, the same works in opposite way.

``` C#
PlayerPrefsEx.Settings.UniqueKeyPerDevice = true;
```

# Custom data types

There are two ways to do that.

## 1. Using `PlayerPrefsJson<T>`

``` C#
// Static API
var player = PlayerPrefsEx.GetJson<Player>("player");
PlayerPrefsEx.SetJson<Player>("player", new Player());

// Variables API
var player = new PlayerPrefsJson<Player>("player");
player.Value = new Player();
```

## 2. Using `PlayerPrefsEx<T>`

Create dedicated class for more clean usage.

``` C#
// Variables API
var enemy = PlayerPrefsEnemy("enemy");
enemy.Value = new Enemy();

public class PlayerPrefsEnemy : PlayerPrefsEx<Enemy>
{
    public PlayerPrefsEnemy(string key, Enemy defaultValue = default) : base(key, defaultValue) 
    { 

    }

    protected override Enemy Deserialize(string value)
    {
        return JsonUtility.FromJson<Enemy>(value);
    }
    protected override string Serialize(Enemy value)
    {
        return JsonUtility.ToJson(value);
    }
}
```
