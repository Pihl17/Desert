using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProceduralTileScript))]
public class ShowPlayerTileDistance : MonoBehaviour
{
	public bool distanceIndication;

	ProceduralTileScript script;

	void OnDrawGizmos() {
		if (script == null) {
			script = GetComponent<ProceduralTileScript>();
		} else {
			if (script.GetPlayer() != null) {
				if (distanceIndication && script.GetMinDis() < Mathf.Abs((script.GetPlayer().transform.position - (transform.position + script.GetCalculatedOffset())).magnitude)) {
					Gizmos.color = new Color(1,0,0);
				} else
					Gizmos.color = new Color(1,1,0);
				Gizmos.DrawLine(transform.position + script.GetCalculatedOffset(), script.GetPlayer().transform.position);
			}
		}
	}
}
