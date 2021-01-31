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
	[SerializeField] private Flame _flame; 		/// <summary>Flame's Reference.</summary>
	
	/// <summary>Gets data property.</summary>
	public static GameData data { get { return Instance._data; } }

	/// <summary>Gets flame property.</summary>
	public static Flame flame { get { return Instance._flame; } }

	/// <summary>Draws Gizmos on Editor mode.</summary>
	private void OnDrawGizmos()
	{
		if(data == null) return;
		
		Gizmos.DrawWireSphere(Vector3.zero, data.limitRadius);
	}

	/// <summary>Callback internally called after Awake.</summary>
	protected override void OnAwake()
	{
		
	}
}
}