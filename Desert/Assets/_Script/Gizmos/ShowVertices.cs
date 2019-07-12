using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TileGenerationScript))]
public class ShowVertices : MonoBehaviour
{
	void OnDrawGizmos() {
		//Gizmos.DrawSphere(transform.position, 0.5f);
		if (GetComponent<MeshFilter>() != null) {
			Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
			if (mesh != null) {
				foreach (Vector3 vertice in mesh.vertices) {
					Gizmos.DrawSphere(transform.position + vertice, 0.05f);
				}
				for (int i = 0; i < mesh.normals.Length; i++) {
					Gizmos.DrawLine(transform.position + mesh.vertices[i], transform.position + mesh.vertices[i] + mesh.normals[i].normalized*0.2f);
				}
			}
		}
	}
}
