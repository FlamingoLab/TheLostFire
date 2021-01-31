using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voidless;

namespace Flamingo
{
public class ObstacleRandomizerController : Singleton<ObstacleRandomizerController>
{
	[SerializeField] private CollectionIndex _loopID; 						/// <summary>Loop's ID.</summary>
	[SerializeField] private MeshRenderer _pitMeshRenderer; 				/// <summary>Pit's MeshRenderer Component.</summary>
	[SerializeField] private RandomDistributionSystem _distributionSystem; 	/// <summary>Random's Distribution System.</summary>
	[SerializeField] private IntRange _obstaclesPerCreation; 				/// <summary>Obstacles created per time.</summary>
	[SerializeField] private FloatRange _separation; 						/// <summary>Separation range by units.</summary>
	[SerializeField] private NormalizedVector3 _rotationAxis; 				/// <summary>Obstacle's Rotation Axis.</summary>
	[SerializeField] private NormalizedVector3 _directionAxis; 				/// <summary>Direction's Axis.</summary>
	[SerializeField] private float _initialDistance; 						/// <summary>Initial's spawning distance.</summary>
	[SerializeField] private float _seconds; 								/// <summary>Seconds for the Music to reach the high point.</summary>
	private float _trailLength; 											/// <summary>Trail's Length.</summary>
	private Vector3 _position; 												/// <summary>Accumulated Position.</summary>

	/// <summary>Gets loopID property.</summary>
	public CollectionIndex loopID { get { return _loopID; } }

	/// <summary>Gets pitMeshRenderer property.</summary>
	public MeshRenderer pitMeshRenderer { get { return _pitMeshRenderer; } }

	/// <summary>Gets distributionSystem property.</summary>
	public RandomDistributionSystem distributionSystem { get { return _distributionSystem; } }

	/// <summary>Gets obstaclesPerCreation property.</summary>
	public IntRange obstaclesPerCreation { get { return _obstaclesPerCreation; } }

	/// <summary>Gets separation property.</summary>
	public FloatRange separation { get { return _separation; } }

	/// <summary>Gets rotationAxis property.</summary>
	public NormalizedVector3 rotationAxis { get { return _rotationAxis; } }

	/// <summary>Gets directionAxis property.</summary>
	public NormalizedVector3 directionAxis { get { return _directionAxis; } }

	/// <summary>Gets initialDistance property.</summary>
	public float initialDistance { get { return _initialDistance; } }

	/// <summary>Gets seconds property.</summary>
	public float seconds { get { return _seconds; } }

	/// <summary>Gets and Sets trailLength property.</summary>
	public float trailLength
	{
		get { return _trailLength; }
		set { _trailLength = value; }
	}

	/// <summary>Gets and Sets position property.</summary>
	public Vector3 position
	{
		get { return _position; }
		set { _position = value; }
	}

	/// <summary>Callback called on Awake if this Object is the Singleton's Instance.</summary>
   	protected override void OnAwake()
    {
   		position = directionAxis.normalized * initialDistance;
   	}

   	/// <summary>Callback invoked when scene loads, one frame before the first Update's tick.</summary>
   	private void Start()
   	{
   		GeneratePitTrail();
   		GenerateObstacleSetOnTrail();
   		//GenerateObstacleSet();
   	}

   	public void GeneratePitTrail()
   	{
   		float pitWidth = pitMeshRenderer.bounds.size.y;
   		float pitHalfWidth = (pitWidth * 0.5f);
   		float flameSpeed = Game.flame.speed.z;
   		float loopLength = /*Game.data.musicClips[loopID].length*/seconds;
   		trailLength = (loopLength * flameSpeed);
   		float width = Mathf.Ceil(trailLength / pitWidth);
   		int pitsToCreate = (int)width;
   		Vector3 normalizedDirection = directionAxis.normalized;
   		Vector3 distance = position + (normalizedDirection * pitHalfWidth);
   		Quaternion pitRotation = pitMeshRenderer.transform.rotation;

   		for(int i = 0; i < pitsToCreate; i++)
   		{
   			PoolManager.RequestPit(distance, pitRotation);
   			distance += (normalizedDirection * pitWidth);
   		}
   	}

   	public void GenerateObstacleSetOnTrail()
   	{
   		float offset = 0.0f;
   		float accumulatedWeight = 0.0f;
   		float angle = 0.0f;
   		int obstacleID = 0;
   		PoolGameObject obstacle = null;
   		Vector3 distance = position;

   		while(accumulatedWeight < trailLength)
   		{
   			obstacleID = distributionSystem.GetRandomIndex();
			offset = separation.Random();
			angle = VMath.RandomDegree();
			obstacle = PoolManager.RequestObstacle(obstacleID, distance, Quaternion.identity);

			obstacle.transform.localRotation = Quaternion.Euler(rotationAxis.normalized * angle);
			distance += directionAxis.normalized * offset;
			accumulatedWeight += offset;
   		}
   	}

	public void GenerateObstacleSet()
	{
		int length = obstaclesPerCreation.Random();
		int obstacleID = 0;
		PoolGameObject obstacle = null;
		Vector3 distance = position;
		float offset = 0.0f;
		float angle = 0.0f;

		for(int i = 0; i < length; i++)
		{
			obstacleID = distributionSystem.GetRandomIndex();
			offset = separation.Random();
			angle = VMath.RandomDegree();
			obstacle = PoolManager.RequestObstacle(obstacleID, distance, Quaternion.identity);

			obstacle.transform.localRotation = Quaternion.Euler(rotationAxis.normalized * angle);
			distance += directionAxis.normalized * offset;
		}

		Vector3 triggerPosition = (distance - position) * 0.5f;
	}
}
}