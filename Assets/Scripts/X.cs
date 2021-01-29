using UnityEngine;
using UnityEditor;

public class X : MonoBehaviour
{
	void Start()
	{
		Material mat = Resources.Load("Resources/unity_builtin_extra") as Material;

		EditorUtility.SetDirty(mat);
		AssetDatabase.SaveAssets();
	}
}