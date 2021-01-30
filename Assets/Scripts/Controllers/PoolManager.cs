using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voidless;

namespace Flamingo
{
public class PoolManager : Singleton<PoolManager>
{
	private GameObjectPool<PoolGameObject>[] _obstaclesPools; 	/// <summary>Set of Obstacles' Pools.</summary>

	/// <summary>Gets and Sets obstaclesPools property.</summary>
	public GameObjectPool<PoolGameObject>[] obstaclesPools
	{
		get { return _obstaclesPools; }
		set { _obstaclesPools = value; }
	}

	/// <summary>PoolManager's instance initialization.</summary>
	protected override void OnAwake()
	{
		obstaclesPools = GameObjectPool<PoolGameObject>.PopulatedPools(Game.data.obstacles);
	}

	/// <summary>Callback invoked when scene loads, one frame before the first Update's tick.</summary>
	private void Start()
	{
		
	}

	/// <summary>Returns an obstacle from a pool given an ID.</summary>
	/// <param name="_ID">Pool's ID.</param>
	/// <param name="_position">Obstacle's Spawn Position.</param>
	/// <param name="_rotation">Obstacle's Spawn Rotation.</param>
	/// <returns>Obstacle from pool at given position and rotation.</returns>
	public static PoolGameObject RequestObstacle(int _ID, Vector3 _position, Quaternion _rotation)
	{
		if(Instance == null) return null;

		return Instance.obstaclesPools[_ID].Recycle(_position, _rotation);
	}
}
}