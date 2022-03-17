# UniLeo - Unity Conversion Workflow for [Leopotam ECS](https://github.com/Leopotam/ecs)
## Easy convert GameObjects to Entity

Important! This repository is extension to [Leopotam ECS](https://github.com/Leopotam/ecs) - Engine independent ECS that works with any Game Engine. But Unity Engineers often ask how to integrate Leo with Unity Inspector and deal with Prefabs.
This lightweight repository is intended to help with this.

Thanks to [SinyavtsevIlya](https://github.com/SinyavtsevIlya) and [Leopotam](https://github.com/Leopotam/ecs)

# How to start

**First** you need to install [Leopotam ECS](https://github.com/Leopotam/ecs), you can do it with Unity Package Manager

Add new line to `Packages/manifest.json`
```
"com.leopotam.ecs": "https://github.com/voody2506/ecs.git",
```
**Second** install this repository

```
"com.voody.unileo": "https://github.com/voody2506/UniLeo.git",
```

<details>
  <summary>How to add through Package Manager</summary>
Unity Editor -> Window -> Package Manager
	
	
![](https://i.ibb.co/4gHj69R/2021-04-20-00-23-10.png)
</details>

## Don't forget NameSpace 

```csharp
using Voody.UniLeo;
```

## Create your first component
```csharp
[Serializable] // <- Important to add Serializable attribute
public struct PlayerComponent {
    public float health;
}
```
Now you need to control health value within the Unity Inspector,  but Unity Engine works only with MonoBehavior classes. Thats mean you need to create MonoBehavior Provider for our component.

Create new script with Mono provider.
```csharp
public sealed class PlayerComponentProvider : MonoProvider<PlayerComponent> { }
```
Add PlayerComponentProvider into Inspector
<details>
  <summary>Inspector Preview</summary>

![](https://i.ibb.co/wWQcFg4/2021-04-18-23-43-16.png)
</details>

Now you can control component values within the Inspector. Congratulations!

 > At this moment you can not control values from Inspector at Runtime

<details>
  <summary>Choose conversion method</summary>

![](https://i.ibb.co/GprVL54/2021-04-21-01-43-28.png)

 > Convert And Inject - Just creates entitie with components based on GameObject
 
 > Convert And Destroy - Deletes GameObject after conversion
	
 > Convert And Save - Stores associated GameObject entity in ConvertToEntity Script
```csharp
	// Than just get value from ConvertToEntity MonoBehavior
	if (ConvertToEntity.TryGetEntity().HasValue) {
	  ConvertToEntity.TryGetEntity().Value
	}
```

</details>

## Convert your GameObjects to Entity

If you read the [Leo's documentation](https://github.com/Leopotam/ecs), you know that for successful work with Leo ECS, you should to create Startup ECS Monobehavior. To Automatically convert GameObjects to Entity add `ConvertScene()` method.

```csharp
void Start() 
{
    _world = new  EcsWorld ();    
    _systems = new  EcsSystems (_world);
    _systems
        .ConvertScene() // <- Need to add this method
        .Add(new ExampleSystem())
        // Other ECS Systems   
        .Init(); 
 }
```

> ConvertScene - method that automatically scan world, finds GameObjects with MonoProvider, creates entity and adds initial Components to the Entity.


## Spawn Prefabs

Starting from `1.0.2` spawning prefabs aviabale from any Instantiate method, including 3rd party Assets

```csharp
GameObject.Instantiate(gameObject, position, rotation);
PhotonNetwork.Instantiate <- works in 3rd party Assets
```

 > Every Prefab initialize with new entity. Components will be added automatically


## Thanks!
