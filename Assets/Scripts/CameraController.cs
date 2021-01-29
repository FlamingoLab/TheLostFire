using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voidless;

namespace Flamingo
{
[RequireComponent(typeof(ThirdPersonCamera))]
public class CameraController : MonoBehaviour
{
	private ThirdPersonCamera _camera; 	/// <summary>ThirdPersonCamera's Controller.</summary>

	/// <summary>Gets camera Component.</summary>
	public ThirdPersonCamera camera
	{ 
		get
		{
			if(_camera == null) _camera = GetComponent<ThirdPersonCamera>();
			return _camera;
		}
	}

	/// <summary>Updates CameraController's instance at each frame.</summary>
	private void Update()
	{
		//camera.orbitFollow	
	}
}
}