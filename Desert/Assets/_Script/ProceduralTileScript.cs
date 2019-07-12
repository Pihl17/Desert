using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralTileScript : TileGenerationScript
{

	float minDis = 50;
	float maxDis = 55;

	//public GameObject pre;
	GameObject player;
	float playerDis;

	Vector3 offset;
	Vector3 disRight;
	Vector3 disForward;

	void Awake() {
		offset = new Vector3(size[0]-1, 0, size[1]-1) / 2 * resolution + Vector3.up*(height+0.2f);
		disRight = Vector3.right*(size[0]-1)*resolution;
		disForward = Vector3.forward*(size[1]-1)*resolution;
		player = GameObject.Find("Player");
	}

    // Update is called once per frame
    void Update() {
		if (player == null) {
			player = GameObject.Find("Player");
		}
		if (player != null) {
			playerDis = Mathf.Abs((player.transform.position - (transform.position + offset)).magnitude);
			if (playerDis < minDis) {
				GenerateTile(disRight);
				GenerateTile(disForward);
				GenerateTile(-disRight);
				GenerateTile(-disForward);
			} else if (playerDis > maxDis) {
				GameObject.Destroy(gameObject);
			}
		}
    }

	void GenerateTile(Vector3 direction) {
		if (!Physics.Raycast(transform.position + offset + direction, Vector3.down, height*2+0.4f)) {
			GameObject newTile = GameObject.Instantiate(gameObject, transform.position + direction, transform.rotation);
			newTile.name = gameObject.name;
		}
	}

	public Vector3 GetCalculatedOffset() {
		return new Vector3(size[0]-1, 0, size[1]-1) / 2 * resolution + Vector3.up*(height+0.2f);
	}

	public Vector3 GetCalcularedDisRight() {
		return Vector3.right*(size[0]-1)*resolution;
	}

	public Vector3 GetCalcularedDisForward() {
		return Vector3.forward*(size[1]-1)*resolution;
	}

	public GameObject GetPlayer() {
		return player;
	}

	public float GetMinDis() {
		return minDis;
	}

	public float GetMaxDis() {
		return maxDis;
	}
}
