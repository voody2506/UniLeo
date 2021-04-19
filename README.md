# UniLeo - Conversion Workflow for [Leopotam ECS](https://github.com/Leopotam/ecs)
## Easy convert GameObjects to Entity

Important! This repository based on [Leopotam ECS](https://github.com/Leopotam/ecs) - Engine independent ECS that works with any Game Engine. But Unity Engineers often ask how to integrate Leo with Unity Inspector and deal with Prefabs.
This lightweight repository is intended to help with this.

Thanks to [SinyavtsevIlya](https://github.com/SinyavtsevIlya) and [Leopotam](https://github.com/Leopotam/ecs)

# How to start

**First** you need to install [Leopotam ECS](https://github.com/Leopotam/ecs), you can do it with Unity Package Manager

```
"com.leopotam.ecs": "https://github.com/Leopotam/ecs.git",
```
**Second** install this repository

Package URL will be added Soon, you can clone this Repo now.

## Create your first component

     public struct PlayerComponent {
	    public float health;
    }

Now you need to control health value within the Unity Inspector,  but Unity Engine works only with MonoBehavior classes. Thats mean you need to create MonoBehavior Provider for our component.

Create new script with Mono provider.

    public sealed class PlayerComponentProvider : MonoProvider<PlayerComponent> { }

Add PlayerComponentProvider into Inspector

![](https://i.ibb.co/wWQcFg4/2021-04-18-23-43-16.png)

Now you can control component values within the Inspector. Congratulations!

## Convert your GameObjects to Entity

If you read the [Leo's documentation](https://github.com/Leopotam/ecs), you know that for successful work with Leo ECS, you should to create Startup ECS Monobehavior. To Automatically convert GameObjects to Entity add `WorldInitSystem` as the first system

     void  Start() {
	     _world = new  EcsWorld ();    
	     _systems = new  EcsSystems (_world)
	       .Add (new  WorldInitSystem())
	        // Other ECS Systems   
	     _systems.Init (); 
        }


> WorldInitSystem - system that automatically scan world, finds GameObjects with MonoProvider, creates entity and adds initial Components to the Entity.


## Spawn Prefabs

Not all GameObjects needs to be created at the beginning of the gameplay. If you need to Spawn Prefab, just create entity with `InstantiateComponent` in any System or use built in EntitySpawner class

    EntitySpawner.Instatiate(gameObject, position, rotation, _world);
    
 > Every ECS System has _world reference

