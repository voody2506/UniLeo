using System;
using UnityEngine;
using Leopotam.Ecs;

public abstract class MonoProvider<T> : BaseMonoProvider, IConvertToEntity where T : struct {
    [SerializeField]
     public T value;

     public void Convert(EcsEntity entity) {
         entity.Replace (value);
     }
}
