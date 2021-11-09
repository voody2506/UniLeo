using Leopotam.Ecs;
using UnityEngine;
using System.Collections.Generic;

namespace Voody.UniLeo
{
    public abstract class MonoProvider <T> : BaseMonoProvider, IConvertToEntity where T : struct
    {
        [SerializeField]
         protected T value;

        void IConvertToEntity.Convert(EcsEntity entity)
        {
            entity.Replace(value);
        }
    }
}
