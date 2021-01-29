using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voidless;

namespace Flamingo
{
public class Game : Singleton<Game>
{
	[SerializeField] private GameData _data; 	/// <summary>Game's Data.</summary>

	/// <summary>Gets data property.</summary>
	public static GameData data { get { return Instance._data; } }

	/// <summary>Callback internally called after Awake.</summary>
	protected override void OnAwake()
	{
		
	}
}
}