#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class TransformMacros : Editor {
	[MenuItem("Tools/Snap Selection To Ground &#v")]
	public static void SnapToGround() {
		foreach (Transform t in Selection.transforms) {
			RaycastHit rayhit;
			if (Physics.Raycast(t.position, Vector3.down, out rayhit)) {
				t.position = rayhit.point;
			}
		}
	}

}

#endif