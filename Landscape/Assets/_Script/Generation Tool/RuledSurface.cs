using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class RuledSurface : MonoBehaviour
{

	protected void SetMesh(Surface surface) {
		Mesh mesh = new Mesh();
		mesh.vertices = surface.vertices;
		mesh.triangles = surface.triangles;
		mesh.RecalculateNormals();
		mesh.RecalculateTangents();
		GetComponent<MeshFilter>().sharedMesh = mesh;
		GetComponent<MeshCollider>().sharedMesh = mesh;
	}


	public struct Surface {
		public Vector3[] vertices;
		public int[] triangles;

		public void DetermineTriangles(int i, int botLeft, int topLeft, int topRight, int botRight) {
			triangles[i] = botLeft;
			triangles[i + 1] = topLeft;
			triangles[i + 2] = botRight;
			triangles[i + 3] = topLeft;
			triangles[i + 4] = topRight;
			triangles[i + 5] = botRight;
		}

		public void DetermineTriangles(int i, int botLeft, int rowSize) {
			DetermineTriangles(i, botLeft, botLeft + 1, botLeft + 1 + rowSize, botLeft + rowSize);
		}

	}

}
