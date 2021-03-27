using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cylinder : RuledSurface {

	public uint resolution = 4;
	public uint heightResolution = 2;
	public float height = 1;
	public float radius = 1;
	public float deltaRadius = 1;
	public float topRotationOffset = 0;
	
	// Start is called before the first frame update
    void Start()
    {
		Surface surface = LateralShape();
		SetMesh(surface);
		
    }

	Surface LateralShape() {
		Surface surface = new Surface();
		surface.vertices = new Vector3[resolution * heightResolution];
		surface.triangles = new int[resolution * heightResolution * 6];
		/*for (int i = 0; i < resolution; i++) {
			surface.vertices[i*2] = DirectrixC((float)i/resolution);
			surface.vertices[i*2 + 1] = DirectrixD((float)i/resolution);
			

			if (i != resolution-1) {
				surface.triangles[i * 6] = i * 2;
				surface.triangles[i * 6 + 1] = i * 2 + 1;
				surface.triangles[i * 6 + 2] = i * 2 + 2;
				surface.triangles[i * 6 + 3] = i * 2 + 1;
				surface.triangles[i * 6 + 4] = i * 2 + 3;
				surface.triangles[i * 6 + 5] = i * 2 + 2;
			}
		}*/
		for (int i = 0; i < resolution; i++) {
			for (int j = 0; j < heightResolution; j++) {
				surface.vertices[i * heightResolution + j] = DirectrixC((float)i / resolution) + GeneratorLine((float)i / resolution, (float)j / (heightResolution - 1));


				if (j != heightResolution - 1)
					if (i != resolution - 1)
						surface.DetermineTriangles(i * 6 * (int)heightResolution + j * 6, i * (int)heightResolution + j, (int)heightResolution);
					else
						surface.DetermineTriangles(i * 6 * (int)heightResolution + j * 6, i * (int)heightResolution + j, i * (int)heightResolution + j + 1, j +1, j);
			}
		}
		return surface;
	}

	Vector3 DirectrixC(float u) {
		return new Vector3(Mathf.Cos(u * 2 * Mathf.PI), 0, Mathf.Sin(u * 2 * Mathf.PI)) * radius;
	}

	Vector3 DirectrixD(float u) {
		return DirectrixC(u + topRotationOffset) * deltaRadius + Vector3.up * height;
	}

	Vector3 GeneratorLine(float u) {
		return DirectrixD(u) - DirectrixC(u);
	}

	Vector3 GeneratorLine(float u, float v) {
		return GeneratorLine(u) * v;
	}

	private void OnDrawGizmosSelected() {
		Gizmos.matrix = Matrix4x4.Translate(transform.position);
		Gizmos.color = Color.green;
		Surface surface = LateralShape();
		for (int i = 0; i < surface.triangles.Length; i += 3) {
			Gizmos.DrawLine(surface.vertices[surface.triangles[i]], surface.vertices[surface.triangles[i+1]]);
			Gizmos.DrawLine(surface.vertices[surface.triangles[i+1]], surface.vertices[surface.triangles[i+2]]);
			Gizmos.DrawLine(surface.vertices[surface.triangles[i+2]], surface.vertices[surface.triangles[i]]);
		}
	}

}

