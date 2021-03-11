using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class TileGenerationScript : MonoBehaviour
{

	public LandscapeData landscapeData;

	// Start is called before the first frame update
    void Start()
    {
		CreateMesh();
	}

	void CreateMesh() {
		Mesh mesh = new Mesh();
		mesh.vertices = DefineVertices();
		mesh.triangles = DefineTriangles();
		mesh.normals = CalculateNormals(mesh.vertices);
		GetComponent<MeshFilter>().sharedMesh = mesh;
		GetComponent<MeshCollider>().sharedMesh = mesh;
	}

	Vector3[] DefineVertices() {
		Vector3[] vertices = new Vector3[landscapeData.TileSize[0] * landscapeData.TileSize[1]];
		
		for (int i = 0; i < landscapeData.TileSize[0]; i++) {
			for (int j = 0; j < landscapeData.TileSize[1]; j++) {
				vertices[j * landscapeData.TileSize[0] + i] = new Vector3(i, 0, j) * landscapeData.Resolution;
				vertices[j * landscapeData.TileSize[0] + i] += Vector3.up * VerticeHeight(vertices[j * landscapeData.TileSize[0] + i].x, vertices[j * landscapeData.TileSize[0] + i].z);
			}
		}
		return vertices;
	}

	Vector3[] CalculateNormals(Vector3[] vertices) {
		Vector3[] normals = new Vector3[vertices.Length];
		for (int i = 0; i < landscapeData.TileSize[0]; i++) {
			for (int j = 0; j < landscapeData.TileSize[1]; j++) {
				normals[j * landscapeData.TileSize[0] + i] = CalculateNormal(i, j, vertices);
			}
		}
		return normals;
	}

	Vector3 CalculateNormal(int x, int z, Vector3[] vertices) {
		Vector3[] fracVertice = new Vector3[2];

		fracVertice[0] = new Vector3(x, 0, z) * landscapeData.Resolution + Vector3.right * landscapeData.NormalRes + Vector3.up * VerticeHeight(vertices[z * landscapeData.TileSize[0] + x].x + landscapeData.NormalRes, vertices[z * landscapeData.TileSize[0] + x].z);
		fracVertice[1] = new Vector3(x, 0, z) * landscapeData.Resolution + Vector3.forward * landscapeData.NormalRes + Vector3.up * VerticeHeight(vertices[z * landscapeData.TileSize[0] + x].x, vertices[z * landscapeData.TileSize[0] + x].z + landscapeData.NormalRes);
		 return Vector3.Cross(fracVertice[1] - vertices[z * landscapeData.TileSize[0] + x], fracVertice[0] - vertices[z * landscapeData.TileSize[0] + x]);
	}

	int[] DefineTriangles() {
		int triangleAmount = (landscapeData.TileSize[0] - 1) * (landscapeData.TileSize[1] - 1) * 2;
		int[] triangles = new int[triangleAmount * 3];
		for (int i = 0; i < landscapeData.TileSize[0] - 1; i++) {
			for (int j = 0; j < landscapeData.TileSize[1] - 1; j++) {
				int c = j * (landscapeData.TileSize[0]) + i;
				int p = (j * (landscapeData.TileSize[0] - 1) + i) * 6;
				triangles[p] = c;
				triangles[p + 1] = c + landscapeData.TileSize[0];
				triangles[p + 2] = c + 1;
				triangles[p + 3] = c + landscapeData.TileSize[0];
				triangles[p + 4] = c + landscapeData.TileSize[0] + 1;
				triangles[p + 5] = c + 1;
			}
		}
		return triangles;
	}


	float VerticeHeight(float right, float forward) {
		return (Mathf.Sin((transform.position.x + right) * landscapeData.SinFreq) + Mathf.Cos((transform.position.z + forward) * landscapeData.CosFreq)) * landscapeData.Height/2;
	}

	public float GetHeight() {
		return landscapeData.Height;
	}

}