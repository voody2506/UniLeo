using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;

namespace Voody.UniLeo
{
    public enum ConvertMode
    {
        ConvertAndInject,
        ConvertAndDestroy,
        ConvertAndSave
    }
    public class ConvertToEntity : MonoBehaviour
    {
        public ConvertMode convertMode;
        private EcsEntity? entity;
        
        private bool isProccessed = false;
        private void Start()
        {
            var world = WorldHandler.GetWorld();
            if (world != null && !isProccessed)
            {
                var instantiateEntity = world.NewEntity();
                var instantiateComponent = new InstantiateComponent() { gameObject = gameObject };
                instantiateEntity.Replace(instantiateComponent);
            }
        }

        public EcsEntity? TryGetEntity()
        {
            if (entity.HasValue)
            {
                if (entity.Value.IsAlive())
                {
                    return entity.Value;
                }
            }

            return null;
        }
        
        public void setProccessed()
        {
            this.isProccessed = true;
        }

        public void Set(EcsEntity entity)
        {
            this.entity = entity;
        }
    }
}