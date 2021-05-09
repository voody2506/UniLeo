using System;
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

                switch (convertComponent.convertMode)
                {
                    case ConvertMode.ConvertAndDestroy:
                        GameObject.Destroy(gameObject);
                        break;
                    case ConvertMode.ConvertAndInject:
                        GameObject.Destroy(convertComponent);
                        break;
                }
            }
        }
    }
}

namespace Unity.IL2CPP.CompilerServices
{
    /// <summary>
    /// The code generation options available for IL to C++ conversion.
    /// Enable or disabled these with caution.
    /// </summary>
    public enum Option
    {
        /// <summary>
        /// Enable or disable code generation for null checks.
        ///
        /// Global null check support is enabled by default when il2cpp.exe
        /// is launched from the Unity editor.
        ///
        /// Disabling this will prevent NullReferenceException exceptions from
        /// being thrown in generated code. In *most* cases, code that dereferences
        /// a null pointer will crash then. Sometimes the point where the crash
        /// happens is later than the location where the null reference check would
        /// have been emitted though.
        /// </summary>
        NullChecks = 1,
        /// <summary>
        /// Enable or disable code generation for array bounds checks.
        ///
        /// Global array bounds check support is enabled by default when il2cpp.exe
        /// is launched from the Unity editor.
        ///
        /// Disabling this will prevent IndexOutOfRangeException exceptions from
        /// being thrown in generated code. This will allow reading and writing to
        /// memory outside of the bounds of an array without any runtime checks.
        /// Disable this check with extreme caution.
        /// </summary>
        ArrayBoundsChecks = 2,
        /// <summary>
        /// Enable or disable code generation for divide by zero checks.
        ///
        /// Global divide by zero check support is disabled by default when il2cpp.exe
        /// is launched from the Unity editor.
        ///
        /// Enabling this will cause DivideByZeroException exceptions to be
        /// thrown in generated code. Most code doesn't need to handle this
        /// exception, so it is probably safe to leave it disabled.
        /// </summary>
        DivideByZeroChecks = 3,
    }

    /// <summary>
    /// Use this attribute on a class, method, or property to inform the IL2CPP code conversion utility to override the
    /// global setting for one of a few different runtime checks.
    ///
    /// Example:
    ///
    ///     [Il2CppSetOption(Option.NullChecks, false)]
    ///     public static string MethodWithNullChecksDisabled()
    ///     {
    ///         var tmp = new Object();
    ///         return tmp.ToString();
    ///     }
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public class Il2CppSetOptionAttribute : Attribute
    {
        public Option Option { get; private set; }
        public object Value { get; private set; }

        public Il2CppSetOptionAttribute(Option option, object value)
        {
            Option = option;
            Value = value;
        }
    }
}
