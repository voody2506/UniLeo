using System;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

/// This class handle global init to ECS World
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
class WorldInitSystem : IEcsInitSystem, IEcsRunSystem
{
    EcsWorld _world = null;

    EcsFilter<InstantiateComponent> _filter = null;

    public void Init()
    {
        BaseMonoProvider[] foundMonoProviderObjects =
            GameObject.FindObjectsOfType<BaseMonoProvider>();

        HashSet<GameObject> all_founded_gameObjectForConversion =
            new HashSet<GameObject>();
        for (int i = 0; i < foundMonoProviderObjects.Length; i++)
        {
            GameObject monoEntity = foundMonoProviderObjects[i].gameObject;
            all_founded_gameObjectForConversion.Add (monoEntity);
        }

        // Iterate throught all gameobjects, that has ECS Components
        foreach (GameObject objects in all_founded_gameObjectForConversion)
        {
            AddEntity (objects);
        }
    }

    public void Run()
    {
        foreach (var i in _filter) {
            ref var entity = ref _filter.GetEntity (i);
            ref InstantiateComponent component = ref entity.Get<InstantiateComponent> ();
            if (component.gameObject != null) {
                GameObject.Instantiate(component.gameObject, component.position, component.rotation);
                entity.Destroy ();
            }
        }
    }

    // Creating New Entity with components function
    private void AddEntity(GameObject gameObject)
    {
        // Creating new Entity
        EcsEntity entity = _world.NewEntity();
        foreach (var component in gameObject.GetComponents<Component>())
        {
            IConvertToEntity entityComponent = component as IConvertToEntity;
            if (entityComponent != null)
            {
                // Adding Component to entity
                entityComponent.Convert (entity);
            }
        }
    }
}
