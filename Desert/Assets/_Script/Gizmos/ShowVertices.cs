using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TileGenerationScript))]
public class ShowVertices : MonoBehaviour
{
	void OnDrawGizmos() {
		Gizmos.DrawSphere(transform.position, 0.5f);
		if (GetComponent<MeshFilter>() != null) {
			
		}
	}
}
