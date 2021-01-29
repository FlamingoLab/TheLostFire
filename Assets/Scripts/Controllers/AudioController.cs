using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voidless;

namespace Flamingo
{
[RequireComponent(typeof(AudioSource))]
public class AudioController : Singleton<AudioController>
{
	private AudioSource _audioSource; 	/// <summary>AudioSource's component.</summary>

	/// <summary>Gets audioSource Component.</summary>
	public AudioSource audioSource
	{ 
		get
		{
			if(_audioSource == null) _audioSource = GetComponent<AudioSource>();
			return _audioSource;
		}
	}

	/*/// <summary>Stops AudioSource, then assigns and plays AudioClip.</summary>
	/// <param name="_audioSource">AudioSource to play sound.</param>
	/// <param name="_aucioClip">AudioClip to play.</param>
	/// <param name="_loop">Loop AudioClip? false as default.</param>
	public static void PlaySound(this AudioSource _audioSource, AudioClip _audioClip, bool _loop = false)
	(CollectionIndex _index, bool _loop = false)
	{
		AudioClip clip = Game.data.[]
		Instance.audioSource.PlaySound();
	}

	/// <summary>Stacks and plays AudioClip.</summary>
	/// <param name="_audioSource">AudioSource to play sound.</param>
	/// <param name="_aucioClip">AudioClip to play.</param>
	/// <param name="_volumeScale">Normalized Volume's Scale.</param>
	public static void PlaySoundOneShot(this AudioSource _audioSource, AudioClip _audioClip, float _volumeScale = 1.0f)
	{

	}*/
}
}