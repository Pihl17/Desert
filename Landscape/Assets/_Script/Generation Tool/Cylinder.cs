using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cylinder : RuledSurface {

	[Min(3)] public int resolution = 4;
	[Min(2)] public int heightResolution = 2;
	[Min(0)] public float height = 1;
	[Min(0)] public float radius = 1;
	[Min(0)] public float deltaRadius = 1;
	[Range(0,1)] public float topRotationOffset = 0;
	
	// Start is called before the first frame update
    void Start()
    {
		Surface surface = DefineSurface();
		SetMesh(surface);
		
    }

	Surface DefineSurface() {
		Surface surface = LateralSurface();
		surface.Add(CircleSurface(true));
		surface.Add(CircleSurface().InverseNormals());
		return surface;
	}

	Surface LateralSurface() {
		Surface surface = new Surface();
		surface.vertices = new Vector3[resolution * heightResolution];
		surface.triangles = new int[resolution * heightResolution * 6];

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

	Surface CircleSurface(bool topPart = false) {
		Surface surface = new Surface();
		surface.vertices = new Vector3[resolution + 1];
		surface.triangles = new int[resolution * 3];

		surface.vertices[0] = topPart ? Vector3.up * height : Vector3.zero;
		for (int i = 0; i < resolution; i++) {
			surface.vertices[i + 1] = topPart ? DirectrixD((float)i / resolution) : DirectrixC((float)i / resolution);

			if (i == 0)
				surface.DetermineTriangles(i, resolution, 0, i + 1);
			else
				surface.DetermineTriangles(i*3, i, 0, i + 1);
		}

		return surface;
	}

	protected override Vector3 DirectrixC(float u) {
		return new Vector3(Mathf.Cos(u * 2 * Mathf.PI), 0, Mathf.Sin(u * 2 * Mathf.PI)) * radius;
	}

	protected override Vector3 DirectrixD(float u) {
		return DirectrixC(u + topRotationOffset) * deltaRadius + Vector3.up * height;
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

