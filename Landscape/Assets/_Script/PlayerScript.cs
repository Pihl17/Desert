using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

	float speed = 10;
	float rotSpeed = 90;

    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
			Move(1,0);
		if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
			Move(0,1);
		if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
			Move(-1,0);
		if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
			Move(0,-1);
		if (Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.Q))
			Rotate(1);
		if (Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.E))
			Rotate(-1);
    }

	void Move(float forward, float right) {
		transform.Translate((Vector3.forward * forward + Vector3.right * right) * speed * Time.deltaTime);
	}

	void Rotate(float right) {
		transform.Rotate(Vector3.up * right * rotSpeed * Time.deltaTime);
	}
}
