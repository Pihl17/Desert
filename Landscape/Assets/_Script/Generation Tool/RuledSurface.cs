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
			DetermineTriangles(i, botLeft, topLeft, botRight);
			DetermineTriangles(i+3, topLeft, topRight, botRight);
		}

		public void DetermineFace(int i, int botLeft, int rowSize) {
			DetermineFace(i, botLeft, botLeft + 1, botLeft + 1 + rowSize, botLeft + rowSize);
		}

		public Surface InverseNormals() {
			int temp;
			for (int i = 0; i < triangles.Length; i += 3) {
				temp = triangles[i];
				triangles[i] = triangles[i + 2];
				triangles[i + 2] = temp;
			}
			return this;
		}

		public Surface Add(Surface other) {
			Vector3[] tempVertices = new Vector3[vertices.Length + other.vertices.Length];
			int[] tempTriangles = new int[triangles.Length + other.triangles.Length];
			for (int i = 0; i < vertices.Length; i++)
				tempVertices[i] = vertices[i];
			for (int i = 0; i < triangles.Length; i++)
				tempTriangles[i] = triangles[i];

			for (int i = 0; i < other.vertices.Length; i++)
				tempVertices[i + vertices.Length] = other.vertices[i];
			for (int i = 0; i < other.triangles.Length; i++)
				tempTriangles[i + triangles.Length] = other.triangles[i] + vertices.Length;

			vertices = tempVertices;
			triangles = tempTriangles;
			return this;
		}

	}

}
