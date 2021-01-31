using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voidless;

namespace Flamingo
{
public class FlameController : Singleton<FlameController>
{
	[SerializeField] private Flame _flame; 			/// <summary>Flame's Reference.</summary>
	[Space(5f)]
	[Header("Input Mapping:")]
	[SerializeField] private int _lightEmissionID; 	/// <summary>Light Emission's Input ID.</summary>
	private Vector2 _leftAxes; 						/// <summary>Input's Left Axes.</summary>

	/// <summary>Gets and Sets flame property.</summary>
	public Flame flame
	{
		get { return _flame; }
		set { _flame = value; }
	}

	/// <summary>Gets and Sets lightEmissionID property.</summary>
	public int lightEmissionID
	{
		get { return _lightEmissionID; }
		set { _lightEmissionID = value; }
	}

	/// <summary>Gets and Sets leftAxes property.</summary>
	public Vector2 leftAxes
	{
		get { return _leftAxes; }
		set { _leftAxes = value; }
	}

	/// <summary>FlameController's tick at each frame.</summary>
	private void Update ()
	{
		if(flame == null) return;
		leftAxes = InputController.Instance.leftAxes;

		//if(InputController.InputBegin(lightEmissionID)) flame.EmitLight();
	}

	/// <summary>Updates FlameController's instance at each Physics Thread's frame.</summary>
	private void FixedUpdate()
	{
		if(flame == null) return;

		if(leftAxes.sqrMagnitude > 0.0f) flame.Move(leftAxes);
	}
}
}