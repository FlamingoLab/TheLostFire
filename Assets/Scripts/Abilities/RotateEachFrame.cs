using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flamingo
{
public class RotateEachFrame : MonoBehaviour
{
	[SerializeField] private Space _space; 				/// <summary>Space's Relativeness.</summary>
	[SerializeField] private Vector3 _rotationAxes; 	/// <summary>Rotation's Axes.</summary>
	private float _sign; 								/// <summary>Rotation's Sign.</summary>

	/// <summary>Gets and Sets space property.</summary>
	public Space space
	{
		get { return _space; }
		set { _space = value; }
	}

	/// <summary>Gets and Sets rotationAxes property.</summary>
	public Vector3 rotationAxes
	{
		get { return _rotationAxes; }
		set { _rotationAxes = value; }
	}

	/// <summary>Gets and Sets sign property.</summary>
	public float sign
	{
		get { return _sign; }
		set { _sign = value; }
	}

	/// <summary>Callback invoked when RotateEachFrame's instance is enabled.</summary>
	private void OnEnable()
	{
		sign = UnityEngine.Random.Range(0, 2) == 1 ? 1.0f : -1.0f;
	}
	
	/// <summary>RotateEachFrame's tick at each frame.</summary>
	private void Update ()
	{
		Vector3 axes = space == Space.Self ? transform.rotation * rotationAxes : rotationAxes;
		transform.rotation *= Quaternion.Euler(axes * Time.deltaTime * sign);
	}
}
}