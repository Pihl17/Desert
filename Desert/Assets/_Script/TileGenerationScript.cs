using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class TileGenerationScript : MonoBehaviour
{
    
	protected int[] size = new int[] {10, 8};
	protected float resolution = 0.5f;
	protected float height = 1f;
	float normalRes = 0.01f;

	float sinFreq = 0.18f;
	float cosFreq = 0.2f;

	Vector3[] vertices;
	int[] triangles;
	Vector3[] normals;
	Vector3[] fracVertice = new Vector3[2];

	// Start is called before the first frame update
    void Start()
    {
		vertices = new Vector3[size[0]*size[1]];
		normals = new Vector3[vertices.Length];
		for (int i = 0; i < size[0]; i++) {
			for (int j = 0; j < size[1]; j++) {
				vertices[j * size[0] + i] = new Vector3(i, 0, j) *resolution;
				vertices[j * size[0] + i] += Vector3.up * VerticeHeight(vertices[j * size[0] + i].x, vertices[j * size[0] + i].z);

				fracVertice[0] = new Vector3(i, 0, j) *resolution + Vector3.right*normalRes + Vector3.up * VerticeHeight(vertices[j * size[0] + i].x+normalRes, vertices[j * size[0] + i].z);
				fracVertice[1] = new Vector3(i, 0, j) *resolution + Vector3.forward*normalRes + Vector3.up * VerticeHeight(vertices[j * size[0] + i].x, vertices[j * size[0] + i].z+normalRes);
				normals[j * size[0] + i] = Vector3.Cross(fracVertice[1] - vertices[j * size[0] + i], fracVertice[0] - vertices[j * size[0] + i]);
			}
		}

		int triangleAmount = (size[0]-1)*(size[1]-1)*2;
		triangles = new int[triangleAmount*3];
		for (int i = 0; i < size[0]-1; i++) {
			for (int j = 0; j < size[1]-1; j++) {
				int c = j*(size[0]) + i;
				int p = (j * (size[0]-1) + i) * 6;
				triangles[p] = c;
				triangles[p+1] = c+size[0];
				triangles[p+2] = c+1;
				triangles[p+3] = c+size[0];
				triangles[p+4] = c+size[0]+1;
				triangles[p+5] = c+1;
			}
		}

		Mesh mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.normals = normals;
		GetComponent<MeshFilter>().sharedMesh = mesh;
		GetComponent<MeshCollider>().sharedMesh = mesh;
    }

	float VerticeHeight(float right, float forward) {
		return (Mathf.Sin((transform.position.x + right) * sinFreq) + Mathf.Cos((transform.position.z + forward) * cosFreq)) * height/2;
	}

	public float GetHeight() {
		return height;
	}

}
