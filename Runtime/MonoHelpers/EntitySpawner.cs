using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

namespace Voody.UniLeo
{
    public static class EntitySpawner
    {
        public static void Instatiate(
            GameObject gameObject,
            Vector3 position,
            Quaternion rotation,
            EcsWorld _world
        )
        {
            EcsEntity entity = _world.NewEntity();
            var spawner =
                new InstantiateComponent()
                {
                    gameObject = gameObject,
                    position = position,
                    rotation = rotation
                };
            entity.Replace(spawner);
        }
    }
}