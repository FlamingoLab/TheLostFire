using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voidless
{
public class SteeringVehicle2D : MonoBehaviour
{
	[SerializeField] private float _maxSpeed; 	/// <summary>Vehicle's Maximum Speed.</summary>
	[SerializeField] private float _maxForce; 	/// <summary>Vehicle's Maximum Steering Force.</summary>
	[SerializeField] private float _mass; 		/// <summary>Vehicle's Mass.</summary>
#if UNITY_EDITOR
	[SerializeField] private Color color; 		/// <summary>Gizmos' Color.</summary>
#endif
	private Vector2 velocity; 					/// <summary>Vehicle's Velocity.</summary>

	/// <summary>Gets and Sets maxSpeed property.</summary>
	public float maxSpeed
	{
		get { return _maxSpeed; }
		set { _maxSpeed = value; }
	}

	/// <summary>Gets and Sets maxForce property.</summary>
	public float maxForce
	{
		get { return _maxForce; }
		set { _maxForce = value; }
	}

	/// <summary>Gets and Sets mass property.</summary>
	public float mass
	{
		get { return _mass; }
		set { _mass = value; }
	}

	/// <summary>Resets SteeringVehicle2D's instance to its default values.</summary>
	private void Reset()
	{
		mass = 1.0f;
#if UNITY_EDITOR
		color = Color.red;
#endif
	}

#if UNITY_EDITOR
	/// <summary>Draws Gizmos on Editor mode.</summary>
	private void OnDrawGizmos()
	{
		Gizmos.color = color;
		Gizmos.DrawRay(transform.position, velocity);
	}
#endif

	/// <returns>Vehicle's Velocity.</returns>
	public Vector2 GetVelocity()
	{
		return velocity;
	}

#region LocalFunctions:
	/// <summary>Gets Seek Steering Force.</summary>
	/// <param name="t">Target's position.</param>
	/// <returns>Seek Steering Force towards target.</returns>
	public Vector2 GetSeekForce(Vector2 t)
	{
		return GetSeekForce(transform.position, t, ref velocity, maxSpeed, maxForce, mass);
	}

	/// <summary>Gets Arrival Weight between vehicle and target.</summary>
	/// <param name="p">Vehicle's Position.</param>
	/// <param name="t">Target's Position.</param>
	/// <param name="rMin">Minimum's Radius.</param>
	/// <param name="rMax">Maximum's Radius.</param>
	/// <returns>Normalized Arrival Weight.</returns>
	public float GetArrivalWeight(Vector2 t, float rMin, float rMax)
	{
		return GetArrivalWeight(transform.position, t, rMin, rMax);
	}
#endregion

#region StaticFunctions:
	/// <summary>Gets Seek Steering Force.</summary>
	/// <param name="p">Vehicle's Position.</param>
	/// <param name="t">Target's position.</param>
	/// <param name="v">Velocity's reference.</param>
	/// <param name="s">Vehicle's Maximum Speed.</param>
	/// <param name="f">Vehicle's Maximum Steering Force.</param>
	/// <param name="m">Vehicle's Mass [1.0 by default].</param>
	/// <returns>Seek Steering Force towards target.</returns>
	public static Vector2 GetSeekForce(Vector2 p, Vector2 t, ref Vector2 v, float s, float f, float m = 1.0f)
	{
		Vector2 d = t - p;
		d = d.normalized * s;

		Vector2 steering = d - v;
		steering = steering.normalized * f;

		if(m != 1.0f) steering /= m;

		v = v + steering;
		v = v.normalized * s;

		return v;
	}

	/// <summary>Gets Arrival Weight between vehicle and target.</summary>
	/// <param name="p">Vehicle's Position.</param>
	/// <param name="t">Target's Position.</param>
	/// <param name="rMin">Minimum's Radius.</param>
	/// <param name="rMax">Maximum's Radius.</param>
	/// <returns>Normalized Arrival Weight.</returns>
	public static float GetArrivalWeight(Vector2 p, Vector2 t, float rMin, float rMax)
	{
		if(rMin == rMax) rMin = 0.0f;
		
		float r = rMax - rMin;
		Vector2 d = t - p;

		r *= r;
		
		if(rMin > 0.0f) d = (t + (d.normalized * rMin)) - p;

		return (Mathf.Min(d.sqrMagnitude, r) / r);
	}
#endregion

}
}