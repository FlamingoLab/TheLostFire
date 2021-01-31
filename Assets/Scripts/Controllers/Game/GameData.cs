using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voidless;

namespace Flamingo
{
[CreateAssetMenu]
public class GameData : ScriptableObject
{
	[SerializeField] private float _limitRadius; 					/// <summary>Limit's Radius.</summary>
	[Space(5f)]
	[Header("Music:")]
	[SerializeField] private AudioClip[] _musicClips; 				/// <summary>Music's Clips.</summary>
	[Space(5f)]
	[Header("Sounds:")]
	[SerializeField] private AudioClip[] _soundClips; 				/// <summary>Sound's Clips.</summary>
	[Space(5f)]
	[Header("Particle Effects:")]
	[SerializeField] private ParticleEffect[] _particleEffects; 	/// <summary>Particles' Effects.</summary>
	[Space(5f)]
	[Header("Obstacles:")]
	[SerializeField] private PoolGameObject[] _obstacles; 			/// <summary>Obstacle's Set.</summary>
	[Space(5f)]
	[Header("Scenario's Objects:")]
	[SerializeField] private PoolGameObject _pit; 					/// <summary>Pit's Pool GameObject.</summary>

	/// <summary>Gets limitRadius property.</summary>
	public float limitRadius { get { return _limitRadius; } }

	/// <summary>Gets musicClips property.</summary>
	public AudioClip[] musicClips { get { return _musicClips; } }

	/// <summary>Gets soundClips property.</summary>
	public AudioClip[] soundClips { get { return _soundClips; } }

	/// <summary>Gets particleEffects property.</summary>
	public ParticleEffect[] particleEffects { get { return _particleEffects; } }

	/// <summary>Gets obstacles property.</summary>
	public PoolGameObject[] obstacles { get { return _obstacles; } }

	/// <summary>Gets pit property.</summary>
	public PoolGameObject pit { get { return _pit; } }
}
}