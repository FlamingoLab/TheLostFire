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
[RequireComponent(typeof(SphereCollider))]
public class Flame : MonoBehaviour
{
	[SerializeField] private LayerMask _obstacleMask; 		/// <summary>Obstacle's LayerMask.</summary>
	[SerializeField] private Vector3 _speed; 				/// <summary>Flame's Speed on its respective 3 axes.</summary>
	[Space(5f)]
	[Header("Light Emission's Attributes:")]
	[SerializeField] private float _lightOscillationSpeed; 	/// <summary>Light Oscillation's Speed.</summary>
	[SerializeField] private float _emissionRadius; 		/// <summary>Emission's Radius.</summary>
	[SerializeField] private float _emissionDuration; 		/// <summary>Emission's Duration.</summary>
	[SerializeField] private float _suppressionDuration; 	/// <summary>Light's Suppression Duration.</summary>
	[SerializeField] private float _maxPointWait; 			/// <summary>Wait duration when the emission reaches its highest point.</summary>
	[SerializeField] private float _emissionCooldown; 		/// <summary>Emission's Cooldown Duration.</summary>
	[SerializeField] private Light _light; 					/// <summary>Light's Component.</summary>
	private Rigidbody _rigidbody; 							/// <summary>Rigidbody's Component.</summary>
	private DisplacementAccumulator _accumulator; 			/// <summary>DisplacementAccumulator's Component.</summary>
	private SphereCollider _sphereCollider; 				/// <summary>SphereCollider's Component.</summary>
	private Cooldown _cooldown; 							/// <summary>Light's Emission Cooldown.</summary>
	private Coroutine emission; 							/// <summary>Emission's Coroutine Reference.</summary>

#region Getters/Setters:
	/// <summary>Gets obstacleMask property.</summary>
	public LayerMask obstacleMask { get { return _obstacleMask; } }

	/// <summary>Gets speed property.</summary>
	public Vector3 speed { get { return _speed; } }

	/// <summary>Gets lightOscillationSpeed property.</summary>
	public float lightOscillationSpeed { get { return _lightOscillationSpeed; } }

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

	/// <summary>Gets sphereCollider Component.</summary>
	public SphereCollider sphereCollider
	{ 
		get
		{
			if(_sphereCollider == null) _sphereCollider = GetComponent<SphereCollider>();
			return _sphereCollider;
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

	/// <summary>Updates Flame's instance at each frame.</summary>
	private void Update()
	{
		OscillateLightRange();
		LimitOnPitRadius();
	}

	/// <summary>Updates Flame's instance at each Physics Thread's frame.</summary>
	private void FixedUpdate()
	{
		accumulator.AddDisplacement(transform.up * speed.y);
	}

	/// <summary>Event triggered when this Collider enters another Collider trigger.</summary>
	/// <param name="col">The other Collider involved in this Event.</param>
	private void OnTriggerEnter(Collider col)
	{
		GameObject obj = col.gameObject;
		int mask = 1 << obj.layer;
		
		if((obstacleMask | mask) == obstacleMask)
		{
			Destroy(gameObject);
		}
	}

	/// <summary>Limits Flame of Pit's Radius.</summary>
	private void LimitOnPitRadius()
	{
		Vector3 center = Vector3.zero.WithY(transform.position.y);
		Vector3 direction = transform.position - center;
		float limits = Game.data.limitRadius - sphereCollider.radius;

		if(direction.sqrMagnitude >= (limits * limits)) transform.position = center + (direction.normalized * limits);
	}

	/// <summary>Moves the Flame on the XY's plane.</summary>
	/// <param name="_axes">Movement's Axes.</param>
	public void Move(Vector2 _axes)
	{
		if(!enabled) return;

		Transform camera = ThirdPersonCamera.Instance.transform;
		Vector3 movement = (camera.right * _axes.x * speed.x) + (camera.up * _axes.y * speed.z);
		//movement = transform.rotation * new Vector3(_axes.x * speed.x, 0.0f, _axes.y * speed.z);
		accumulator.AddDisplacement(movement);
	}

	/// <summary>Emits Light.</summary>
	public void EmitLight()
	{
		if(!enabled) return;

		if(emission == null)
		this.StartCoroutine(LightEmission(), ref emission);
	}

	/// <summary>Oscillates Light's Range.</summary>
	public void OscillateLightRange()
	{
		light.range = Mathf.Sin(Time.time * lightOscillationSpeed) * emissionRadius;
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