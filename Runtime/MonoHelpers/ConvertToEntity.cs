using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voody.UniLeo
{
    public enum ConvertMode
    {
        ConvertAndInject,
        ConvertAndDestroy
    }
    public class ConvertToEntity : MonoBehaviour
    {
        public ConvertMode convertMode;
        private void Start()
        {
            var world = WorldHandler.GetWorld();
            Debug.Log(world);
            if (world != null)
            {
                var entity = world.NewEntity();
                var instantiateComponent = new InstantiateComponent() { gameObject = gameObject };
                entity.Replace(instantiateComponent);
            }
        }
    }
}
