using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cylinder : RuledSurface {

	public int resolution = 4;
	public int heightResolution = 2;
	public float height = 1;
	public float radius = 1;
	public float deltaRadius = 1;
	[Range(0,1)] public float topRotationOffset = 0;
	
	// Start is called before the first frame update
    void Start()
    {
		Surface surface = DefineSurface();
		SetMesh(surface);
		
    }

	Surface DefineSurface() {
		Surface surface = new Surface();
		surface.vertices = new Vector3[resolution * heightResolution + 2];
		surface.triangles = new int[resolution * heightResolution * 6 + resolution * 6];
		surface = LateralSurface(surface);
		surface = AddCircleSurfaces(surface);
		return surface;
	}

	Surface LateralSurface(Surface surface) {
		//Surface surface = new Surface();
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
						surface.DetermineFace(i * 6 * heightResolution + j * 6, i * heightResolution + j, heightResolution);
					else
						surface.DetermineFace(i * 6 * heightResolution + j * 6, i * heightResolution + j, i * heightResolution + j + 1, j +1, j);
			}
		}
		return surface;
	}

	Surface AddCircleSurfaces(Surface surface) {
		surface.vertices[resolution * heightResolution] = Vector3.zero;
		surface.vertices[resolution * heightResolution + 1] = Vector3.up * height;
		for (int i = 0; i < resolution; i++) {
			if (i == 0) {
				surface.DetermineTriangles(resolution * heightResolution * 6 + i * 3, heightResolution * i, resolution * heightResolution, heightResolution * (resolution - 1));
				surface.DetermineTriangles(resolution * heightResolution * 6 + resolution * 3 + i * 3, heightResolution * (resolution - 1) + heightResolution - 1, resolution * heightResolution + 1, heightResolution * i + heightResolution - 1);
			} else {
				surface.DetermineTriangles(resolution * heightResolution * 6 + i * 3, heightResolution * i, resolution * heightResolution, heightResolution * (i - 1));
				surface.DetermineTriangles(resolution * heightResolution * 6 + resolution * 3 + i * 3, heightResolution * (i - 1) + heightResolution - 1, resolution * heightResolution + 1, heightResolution * i + heightResolution - 1);
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
		if (resolution <= 0 || heightResolution <= 1)
			return;
		Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
		Gizmos.color = Color.green;
		Surface surface = DefineSurface();
		for (int i = 0; i < surface.triangles.Length; i += 3) {
			Gizmos.DrawLine(surface.vertices[surface.triangles[i]], surface.vertices[surface.triangles[i+1]]);
			Gizmos.DrawLine(surface.vertices[surface.triangles[i+1]], surface.vertices[surface.triangles[i+2]]);
			Gizmos.DrawLine(surface.vertices[surface.triangles[i+2]], surface.vertices[surface.triangles[i]]);
		}
	}

}

