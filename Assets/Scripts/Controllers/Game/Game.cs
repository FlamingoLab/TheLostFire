using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voidless;

namespace Flamingo
{
public class Game : Singleton<Game>
{
	[SerializeField] private GameData _data; 									/// <summary>Game's Data.</summary>
	[SerializeField] private ThirdPersonCamera _camera; 						/// <summary>ThirdPersonCamera.</summary>
	[SerializeField] private Flame _flame; 										/// <summary>Flame's Reference.</summary>
	[Space(5f)]
	[SerializeField] private ObstacleRandomizerController _obstacleRandomizer; 	/// <summary>Obstacle's Randomizer.</summary>
	[SerializeField] private Animator _mateoAnimator; 							/// <summary>Mateo's Animator.</summary>
	[SerializeField] private AnimatorCredential _animationCredential; 			/// <summary>Animation's Credential.</summary>
	[SerializeField] private int _inputID; 										/// <summary>Input's ID to mash.</summary>
	[SerializeField] private float _acceleration; 								/// <summary>Button Mashin's Acceleration.</summary>
	[SerializeField] private float _decceleration; 								/// <summary>Button Mashing's Decceleration.</summary>
	[SerializeField] private float _minLimit; 									/// <summary>Button Mashing's Minimum Limit.</summary>
	[SerializeField] private float _maxLimit; 									/// <summary>Button Mashing's Maximum Limit.</summary>

	/// <summary>Gets data property.</summary>
	public static GameData data { get { return Instance._data; } }

	/// <summary>Gets flame property.</summary>
	public static Flame flame { get { return Instance._flame; } }

	/// <summary>Gets camera property.</summary>
	public ThirdPersonCamera camera { get { return _camera; } }

	/// <summary>Gets obstacleRandomizer property.</summary>
	public ObstacleRandomizerController obstacleRandomizer { get { return _obstacleRandomizer; } }

	/// <summary>Gets mateoAnimator property.</summary>
	public Animator mateoAnimator { get { return _mateoAnimator; } }

	/// <summary>Gets animationCredential property.</summary>
	public AnimatorCredential animationCredential { get { return _animationCredential; } }

	/// <summary>Gets inputID property.</summary>
	public int inputID { get { return _inputID; } }

	/// <summary>Gets acceleration property.</summary>
	public float acceleration { get { return _acceleration; } }

	/// <summary>Gets decceleration property.</summary>
	public float decceleration { get { return _decceleration; } }

	/// <summary>Gets minLimit property.</summary>
	public float minLimit { get { return _minLimit; } }

	/// <summary>Gets maxLimit property.</summary>
	public float maxLimit { get { return _maxLimit; } }

	/// <summary>Draws Gizmos on Editor mode.</summary>
	private void OnDrawGizmos()
	{
		if(data == null) return;
		
		Gizmos.DrawWireSphere(Vector3.zero, data.limitRadius);
	}

	/// <summary>Callback internally called after Awake.</summary>
	protected override void OnAwake()
	{
		//InitialRoutine();
	}

	private void InitialRoutine()
	{
		EnableFlame(false);
		GiveTargetToCamera(false);
		StartCoroutine(ButtonMashingHandle());
	}

	private void GiveControl(bool _give = true)
	{
		InputController.Instance.enabled = _give;
	}

	private void EnableFlame(bool _enable = true)
	{
		flame.enabled = _enable;
	}

	private void GiveTargetToCamera(bool _give = true)
	{
		switch(_give)
		{
			case false:
			camera.target = null;
			camera.physicsTarget = null;
			break;

			case true:
			camera.target = flame.transform;
			camera.physicsTarget = flame.rigidbody;
			break;
		}
	}

	private IEnumerator ButtonMashingHandle()
	{
		IEnumerator<float> buttonMashingHandler = VCoroutines.InputMashingSequence(inputID, acceleration, decceleration, minLimit, maxLimit, OnGameOver, OnButtonMashingSuccess);

		while(buttonMashingHandler.MoveNext())
		{
			float progress = buttonMashingHandler.Current;
			mateoAnimator.Play(animationCredential, 0, progress);
			yield return null;
		}
	}

	private void OnButtonMashingSuccess()
	{

	}

	private void OnGameOver()
	{
		Debug.Log("[Game] Game Over. Fucking Gay.");
	}
}
}