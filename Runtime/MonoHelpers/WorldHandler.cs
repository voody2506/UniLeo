using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

namespace Voody.UniLeo
{
	public static class WorldHandler
	{
    	private static EcsWorld world;
    
    	public static void Init(EcsWorld ecsWorld) 
    	{
        	world = ecsWorld;
    	}
    	public static EcsWorld GetWorld()
    	{
        	return world;
    	}

    	public static void Destroy()
    	{
        	world = null;
    	}
	}
}
