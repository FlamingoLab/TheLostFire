using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voidless;

namespace Flamingo
{
[CreateAssetMenu]
public class GameData : ScriptableObject
{
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

	/// <summary>Gets musicClips property.</summary>
	public AudioClip[] musicClips { get { return _musicClips; } }

	/// <summary>Gets soundClips property.</summary>
	public AudioClip[] soundClips { get { return _soundClips; } }

	/// <summary>Gets particleEffects property.</summary>
	public ParticleEffect[] particleEffects { get { return _particleEffects; } }

	/// <summary>Gets obstacles property.</summary>
	public PoolGameObject[] obstacles { get { return _obstacles; } }
}
}