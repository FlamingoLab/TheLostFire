using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voidless;

namespace Flamingo
{
public class RotateEachFrame : MonoBehaviour
{
	[SerializeField] private Space _space; 						/// <summary>Space's Relativeness.</summary>
	[SerializeField] private NormalizedVector3 _rotationAxes; 	/// <summary>Rotation's Axes.</summary>
	[SerializeField] private FloatRange _rotationRange; 		/// <summary>Rotation's Range.</summary>
	private float _rotation; 									/// <summary>Current Rotation.</summary>
	private float _sign; 										/// <summary>Rotation's Sign.</summary>

	/// <summary>Gets and Sets space property.</summary>
	public Space space
	{
		get { return _space; }
		set { _space = value; }
	}

	/// <summary>Gets and Sets rotationAxes property.</summary>
	public NormalizedVector3 rotationAxes
	{
		get { return _rotationAxes; }
		set { _rotationAxes = value; }
	}

	/// <summary>Gets and Sets rotationRange property.</summary>
	public FloatRange rotationRange
	{
		get { return _rotationRange; }
		set { _rotationRange = value; }
	}

	/// <summary>Gets and Sets sign property.</summary>
	public float sign
	{
		get { return _sign; }
		set { _sign = value; }
	}

	/// <summary>Gets and Sets rotation property.</summary>
	public float rotation
	{
		get { return _rotation; }
		set { _rotation = value; }
	}

	/// <summary>Callback invoked when RotateEachFrame's instance is enabled.</summary>
	private void OnEnable()
	{
		sign = UnityEngine.Random.Range(0, 2) == 1 ? 1.0f : -1.0f;
		rotation = rotationRange.Random();
	}
	
	/// <summary>RotateEachFrame's tick at each frame.</summary>
	private void Update ()
	{
		Vector3 v = rotationAxes;
		Vector3 axes = space == Space.Self ? transform.rotation * v : v;
		transform.rotation *= Quaternion.Euler(axes.normalized * rotation * Time.deltaTime * sign);
	}
}
}