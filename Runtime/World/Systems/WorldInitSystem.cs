using System;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

namespace Voody.UniLeo
{
    /// <summary>
    /// This class handle global init to ECS World
    /// </summary>

    #if ENABLE_IL2CPP
        [Unity.IL2CPP.CompilerServices.Il2CppSetOption (Unity.IL2CPP.CompilerServices.Option.NullChecks, false)]
        [Unity.IL2CPP.CompilerServices.Il2CppSetOption (Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false)]
    #endif

    class WorldInitSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        EcsWorld _world = null;

        EcsFilter<InstantiateComponent> _filter = null;

        public void PreInit()
        {
            var convertableGameObjects =
                GameObject.FindObjectsOfType<ConvertToEntity>();
            // Iterate throught all gameobjects, that has ECS Components
            foreach (var convertable in convertableGameObjects)
            {
                AddEntity (convertable.gameObject);
            }
        }

        public void Run()
        {
            foreach (var i in _filter) {
                ref var entity = ref _filter.GetEntity (i);
                ref InstantiateComponent component = ref entity.Get<InstantiateComponent> ();
                if (component.gameObject) {
                    AddEntity(component.gameObject);
                    GameObject.Instantiate(component.gameObject, component.position, Quaternion.identity);
                }
                entity.Del<InstantiateComponent> ();
            }
        }

        // Creating New Entity with components function
        private void AddEntity(GameObject gameObject)
        {
            // Creating new Entity
            EcsEntity entity = _world.NewEntity();
            foreach (var component in gameObject.GetComponents<Component>())
            {
                if (component is IConvertToEntity entityComponent)
                {
                    // Adding Component to entity
                    entityComponent.Convert (entity);
                }
            }
        }
    }
}