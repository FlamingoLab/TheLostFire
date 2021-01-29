using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voidless;

namespace Flamingo
{
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(DisplacementAccumulator))]
[RequireComponent(typeof(Light))]
public class Flame : MonoBehaviour
{
	[SerializeField] private Vector3 _speed; 				/// <summary>Flame's Speed on its respective 3 axes.</summary>
	[Space(5f)]
	[Header("Light Emission's Attributes:")]
	[SerializeField] private float _emissionRadius; 		/// <summary>Emission's Radius.</summary>
	[SerializeField] private float _emissionDuration; 		/// <summary>Emission's Duration.</summary>
	[SerializeField] private float _suppressionDuration; 	/// <summary>Light's Suppression Duration.</summary>
	[SerializeField] private float _maxPointWait; 			/// <summary>Wait duration when the emission reaches its highest point.</summary>
	[SerializeField] private float _emissionCooldown; 		/// <summary>Emission's Cooldown Duration.</summary>
	private Rigidbody _rigidbody; 							/// <summary>Rigidbody's Component.</summary>
	private DisplacementAccumulator _accumulator; 			/// <summary>DisplacementAccumulator's Component.</summary>
	private Light _light; 									/// <summary>Light's Component.</summary>
	private Cooldown _cooldown; 							/// <summary>Light's Emission Cooldown.</summary>
	private Coroutine emission; 							/// <summary>Emission's Coroutine Reference.</summary>

#region Getters/Setters:
	/// <summary>Gets speed property.</summary>
	public Vector3 speed { get { return _speed; } }

	/// <summary>Gets emissionRadius property.</summary>
	public float emissionRadius { get { return _emissionRadius; } }

	/// <summary>Gets emissionDuration property.</summary>
	public float emissionDuration { get { return _emissionDuration; } }

	/// <summary>Gets suppressionDuration property.</summary>
	public float suppressionDuration { get { return _suppressionDuration; } }

	/// <summary>Gets maxPointWait property.</summary>
	public float maxPointWait { get { return _maxPointWait; } }

	/// <summary>Gets emissionCooldown property.</summary>
	public float emissionCooldown { get { return _emissionCooldown; } }

	/// <summary>Gets rigidbody Component.</summary>
	public Rigidbody rigidbody
	{ 
		get
		{
			if(_rigidbody == null) _rigidbody = GetComponent<Rigidbody>();
			return _rigidbody;
		}
	}

	/// <summary>Gets accumulator Component.</summary>
	public DisplacementAccumulator accumulator
	{ 
		get
		{
			if(_accumulator == null) _accumulator = GetComponent<DisplacementAccumulator>();
			return _accumulator;
		}
	}

	/// <summary>Gets light Component.</summary>
	public new Light light
	{ 
		get
		{
			if(_light == null) _light = GetComponent<Light>();
			return _light;
		}
	}

	/// <summary>Gets and Sets cooldown property.</summary>
	public Cooldown cooldown
	{
		get { return _cooldown; }
		set { _cooldown = value; }
	}
#endregion

	/// <summary>Resets Flame's instance to its default values.</summary>
	public void Reset()
	{
		rigidbody.isKinematic = true;
		rigidbody.useGravity = false;
	}

	/// <summary>Flame's instance initialization.</summary>
	private void Awake()
	{
		cooldown = new Cooldown(this, emissionCooldown, OnCooldownEnds);
	}

	/// <summary>Updates Flame's instance at each Physics Thread's frame.</summary>
	private void FixedUpdate()
	{
		accumulator.AddDisplacement(transform.forward * speed.z);
	}

	/// <summary>Moves the Flame on the XY's plane.</summary>
	/// <param name="_axes">Movement's Axes.</param>
	public void Move(Vector2 _axes)
	{
		accumulator.AddDisplacement(transform.rotation * new Vector3(
			_axes.x * speed.x,
			_axes.y * speed.y,
			0.0f)
		);
	}

	/// <summary>Emits Light.</summary>
	public void EmitLight()
	{
		if(emission == null)
		this.StartCoroutine(LightEmission(), ref emission);
	}

	/// <summary>Callback internally called after the emission's cooldown ends.</summary>
	private void OnCooldownEnds()
	{
		this.DispatchCoroutine(ref emission);
	}

	/// <summary>Light Emission and Suppression's Coroutine.</summary>
	private IEnumerator LightEmission()
	{
		SecondsDelayWait wait = new SecondsDelayWait(maxPointWait);
		float t = 0.0f;
		float inverseDuration = 1.0f / emissionDuration;

		light.range = 0.0f;

		while(t < 1.0f)
		{
			light.range = emissionRadius * t;
			t += (Time.deltaTime * inverseDuration);
			yield return null;
		}

		inverseDuration = 1.0f / suppressionDuration;
		t = 0.0f;
		light.range = emissionRadius;

		while(wait.MoveNext()) yield return null;

		while(t < 1.0f)
		{
			light.range = emissionRadius * (1.0f - t);
			t += (Time.deltaTime * inverseDuration);
			yield return null;
		}

		light.range = 0.0f;
		cooldown.Begin();
	}
}
}