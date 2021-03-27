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
		mesh.RecalculateBounds();
		GetComponent<MeshFilter>().sharedMesh = mesh;
		GetComponent<MeshCollider>().sharedMesh = mesh;
	}


	public struct Surface {
		public Vector3[] vertices;
		public int[] triangles;

		public void DetermineTriangles(int i, int t1, int t2, int t3) {
			triangles[i] = t1;
			triangles[i + 1] = t2;
			triangles[i + 2] = t3;
		}

		public void DetermineFace(int i, int botLeft, int topLeft, int topRight, int botRight) {
			/*triangles[i] = botLeft;
			triangles[i + 1] = topLeft;
			triangles[i + 2] = botRight;
			triangles[i + 3] = topLeft;
			triangles[i + 4] = topRight;
			triangles[i + 5] = botRight;*/
			DetermineTriangles(i, botLeft, topLeft, botRight);
			DetermineTriangles(i+3, topLeft, topRight, botRight);
		}

		public void DetermineFace(int i, int botLeft, int rowSize) {
			DetermineFace(i, botLeft, botLeft + 1, botLeft + 1 + rowSize, botLeft + rowSize);
		}

	}

}
