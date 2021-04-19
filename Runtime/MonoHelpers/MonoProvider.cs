using System;
using Leopotam.Ecs;
using UnityEngine;

namespace Voody.UniLeo
{
    public abstract class MonoProvider: BaseMonoProvider, IConvertToEntity where T : struct
    {
        [SerializeField]
        private T value;

        void IConvertToEntity.Convert(EcsEntity entity)
        {
            entity.Replace (value);
        }
    }
}
