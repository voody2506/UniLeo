using System;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

namespace Voody.UniLeo
{
    /// <summary>
    /// This class handle global init to ECS World
    /// <summary>
#if ENABLE_IL2CPP
    [Unity.IL2CPP.CompilerServices.Il2CppSetOption (Unity.IL2CPP.CompilerServices.Option.NullChecks, false)]
    [Unity.IL2CPP.CompilerServices.Il2CppSetOption (Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false)]
#endif
    class WorldInitSystem : IEcsPreInitSystem, IEcsRunSystem, IEcsDestroySystem
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
                AddEntity(convertable.gameObject);
            }

            // After adding all entitites from the begining of the scene, we need to handle global World value
            WorldHandler.Init(_world);
        }

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref InstantiateComponent component = ref entity.Get<InstantiateComponent>();
                if (component.gameObject)
                {
                    AddEntity(component.gameObject);
                }

                entity.Del<InstantiateComponent>();
            }
        }

        public void Destroy()
        {
            WorldHandler.Destroy();
        }

        // Creating New Entity with components function
        private void AddEntity(GameObject gameObject)
        {
            // Creating new Entity
            EcsEntity entity = _world.NewEntity();
            ConvertToEntity convertComponent = gameObject.GetComponent<ConvertToEntity>();
            if (convertComponent)
            {
                foreach (var component in gameObject.GetComponents<Component>())
                {
                    if (component is IConvertToEntity entityComponent)
                    {
                        // Adding Component to entity
                        entityComponent.Convert(entity);
                        GameObject.Destroy(component);
                    }
                }
                
                convertComponent.setProccessed();
                switch (convertComponent.convertMode)
                {
                    case ConvertMode.ConvertAndDestroy:
                        GameObject.Destroy(gameObject);
                        break;
                    case ConvertMode.ConvertAndInject:
                        GameObject.Destroy(convertComponent);
                        break;
                    case ConvertMode.ConvertAndSave:
                        convertComponent.Set(entity);
                        break;
                }
            }
        }
    }
}
