using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralTileScript : TileGenerationScript
{
	
	GameObject player;
	float playerDis;

	Vector3 offset;
	Vector3 disRight;
	Vector3 disForward;

	void Awake() {
		offset = new Vector3(landscapeData.TileSize[0]-1, 0, landscapeData.TileSize[1]-1) / 2 * landscapeData.Resolution + Vector3.up*(landscapeData.Height + 0.2f);
		disRight = Vector3.right*(landscapeData.TileSize[0]-1)* landscapeData.Resolution;
		disForward = Vector3.forward*(landscapeData.TileSize[1]-1)* landscapeData.Resolution;
		player = GameObject.Find("Player");
	}

    // Update is called once per frame
    void Update() {
		if (player != null) {
			playerDis = Mathf.Abs((player.transform.position - (transform.position + offset)).magnitude);
			if (playerDis < landscapeData.MinDis) {
				GenerateTile(disRight);
				GenerateTile(disForward);
				GenerateTile(-disRight);
				GenerateTile(-disForward);
			} else if (playerDis > landscapeData.MaxDis) {
				GameObject.Destroy(gameObject);
			}
		} else {
			player = GameObject.Find("Player");
		}
    }

	void GenerateTile(Vector3 direction) {
		if (!Physics.Raycast(transform.position + offset + direction, Vector3.down, landscapeData.Height * 2+0.4f)) {
			GameObject newTile = GameObject.Instantiate(gameObject, transform.position + direction, transform.rotation);
			newTile.name = gameObject.name;
		}
	}

	public Vector3 GetCalculatedOffset() {
		return new Vector3(landscapeData.TileSize[0]-1, 0, landscapeData.TileSize[1]-1) / 2 * landscapeData.Resolution + Vector3.up*(landscapeData.Height + 0.2f);
	}

	public Vector3 GetCalcularedDisRight() {
		return Vector3.right*(landscapeData.TileSize[0]-1)* landscapeData.Resolution;
	}

	public Vector3 GetCalcularedDisForward() {
		return Vector3.forward*(landscapeData.TileSize[1]-1)* landscapeData.Resolution;
	}

	public GameObject GetPlayer() {
		return player;
	}

	public float GetMinDis() {
		return landscapeData.MinDis;
	}

	public float GetMaxDis() {
		return landscapeData.MaxDis;
	}
}
