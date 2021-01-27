using System.Collections;
using System.Text;
using UnityEngine;

namespace Voidless
{
public class SecondsDelayWait : VYieldInstruction
{
	private float _waitDuration; 	/// <summary>Wait Delay's Duration.</summary>
	private float _currentWait; 	/// <summary>Current Wait's Time.</summary>
	private float _timeScale; 		/// <summary>Time's Scale.</summary>

	/// <summary>Gets and Sets waitDuration property.</summary>
	public float waitDuration
	{
		get { return _waitDuration; }
		set { _waitDuration = value; }
	}

	/// <summary>Gets and Sets currentWait property.</summary>
	public float currentWait
	{
		get { return _currentWait; }
		protected set { _currentWait = value; }
	}

	/// <summary>Gets and Sets timeScale property.</summary>
	public float timeScale
	{
		get { return _timeScale; }
		set { _timeScale = value; }
	}

	/// <summary>Gets progress property.</summary>
	public float progress { get { return currentWait / waitDuration; } }

	/// <summary>SecondsDelayWait's constructor.</summary>
	/// <param name="_waitDuration">Wait's Duration.</param>
	public SecondsDelayWait(float _waitDuration, float _timeScale = 1.0f)
	{
		waitDuration = _waitDuration;
		currentWait = 0.0f;
		timeScale = _timeScale;
	}

	/// <summary>Advances the enumerator to the next element of the collection.</summary>
	public override bool MoveNext()
	{
		return MoveNext(1.0f);
	}

	/// <summary>Iterates the current time.</summary>
	/// <param name="_timeScale">New Time Scale.</param>
	/// <returns>True if the iterator can keep moving, false otherwise.</returns>
	public bool MoveNext(float _timeScale)
	{
		timeScale = _timeScale;
		currentWait += Time.deltaTime * timeScale;

		return (currentWait <= (waitDuration + Mathf.Epsilon));
	}

	/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
	public override void Reset()
	{
		currentWait = 0.0f;
	}

	/// <summary>Changes duration and resets the Yield Instruction.</summary>
	/// <param name="_waitDuration">New Wait Duration.</param>
	public void ChangeDurationAndReset(float _waitDuration)
	{
		Reset();
		waitDuration = _waitDuration;
	}

	/// <returns>String representing this YieldInstruction.</returns>
	public override string ToString()
	{
		StringBuilder builder = new StringBuilder();

		builder.Append("{ Current Time: ");
		builder.Append(currentWait.ToString());
		builder.Append(", Wait Duration: ");
		builder.Append(waitDuration.ToString());
		builder.Append(" }");

		return builder.ToString();
	}
}
}