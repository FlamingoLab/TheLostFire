using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voidless;

public class TEST_ButtonMashing : MonoBehaviour
{
	[SerializeField] private KeyCode keyCode; 			/// <summary>KeyCode to Press.</summary>
	[SerializeField] private int inputID; 				/// <summary>Input ID to Enter.</summary>
	[SerializeField] private float acceleration; 		/// <summary>Acceleration's Rate.</summary>
	[SerializeField] private float decceleration; 		/// <summary>Decceleration's Rate.</summary>
	[SerializeField] private float minLimit; 			/// <summary>Minimum's Limit.</summary>
	[SerializeField] private float maxLimit; 			/// <summary>Maximum's Limit.</summary>
	private float progress; 							/// <summary>Current's Progress.</summary>
	private IEnumerator<float> InputMashingSequence; 	/// <summary>Input Mashing Sequence's Iterator.</summary>

#region UnityMethods:
	/// <summary>TEST_ButtonMashing's instance initialization.</summary>
	private void Awake()
	{
		
	}

	/// <summary>TEST_ButtonMashing's starting actions before 1st Update frame.</summary>
	private void Start ()
	{
		//InputMashingSequence = VCoroutines.InputMashingSequence(keyCode, acceleration, decceleration, minLimit, maxLimit, null, null);
		InputMashingSequence = VCoroutines.InputMashingSequence(inputID, acceleration, decceleration, minLimit, maxLimit, null, null);
	}
	
	/// <summary>TEST_ButtonMashing's tick at each frame.</summary>
	private void Update ()
	{
		if(InputMashingSequence.MoveNext())
		{
			progress = InputMashingSequence.Current;
		}
	}
#endregion
}