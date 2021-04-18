# LeoECS Inspector

Important! This repository based on Leopotam ECS - Engine independent ECS that works with any Game Engine. But Unity Engineers often ask how to integrate Leo with Unity Inspector and deal with Prefabs.
This lightweight repository is intended to help with this.

# How to start

**First** you need to install Leopotam ECS, you can do it with Unity Package Manager

```
"com.leopotam.ecs": "https://github.com/Leopotam/ecs.git",
```
**Second** install this repository

Package URL will be added Soon, you can clone this Repo now.

## Create your first component

     public struct PlayerComponent {
	    public float health;
    }

Now we need to control health value within the Unity Inspector,  but Unity Engine works only with MonoBehavior classes. Thats mean we need to create MonoBehavior Provider for our component.

Create new script with Mono provider.

    public  sealed  class  PlayerComponentProvider : MonoProvider<PlayerComponent> { }

Add PlayerComponentProvider into Inspector

![](https://i.ibb.co/wWQcFg4/2021-04-18-23-43-16.png)
