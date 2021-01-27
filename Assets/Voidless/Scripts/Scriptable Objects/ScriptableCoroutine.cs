using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voidless
{
public abstract class ScriptableCoroutine<T> : ScriptableObject
{
	/// <summary>Callback invoked when drawing Gizmos.</summary>
	public virtual void OnDrawGizmos() {/*...*/}

	/// <summary>Coroutine's IEnumerator.</summary>
	/// <param name="obj">Object of type T's argument.</param>
	public abstract IEnumerator Routine(T obj);

	/// <summary>Finishes the Routine.</summary>
	/// <param name="obj">Object of type T's argument.</param>
	public abstract void FinishRoutine(T obj);
}
}