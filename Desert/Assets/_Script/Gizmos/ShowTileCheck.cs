using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProceduralTileScript))]
public class ShowTileCheck : MonoBehaviour
{

	ProceduralTileScript script;
	Vector3 originPoint;

	void OnDrawGizmos() {
		if (script == null) {
			script = GetComponent<ProceduralTileScript>();
		} else {
			originPoint = transform.position + script.GetCalculatedOffset();
			Gizmos.color = new Color(0,0,1);
			Gizmos.DrawSphere(originPoint, 0.1f);
			Gizmos.DrawLine(originPoint + script.GetCalcularedDisRight(), 	originPoint + script.GetCalcularedDisRight() + Vector3.down*(script.GetHeight()*2+0.4f));
			Gizmos.DrawLine(originPoint + script.GetCalcularedDisForward(), 	originPoint + script.GetCalcularedDisForward() + Vector3.down*(script.GetHeight()*2+0.4f));
			Gizmos.DrawLine(originPoint - script.GetCalcularedDisRight(), 	originPoint - script.GetCalcularedDisRight() + Vector3.down*(script.GetHeight()*2+0.4f));
			Gizmos.DrawLine(originPoint - script.GetCalcularedDisForward(), 	originPoint - script.GetCalcularedDisForward() + Vector3.down*(script.GetHeight()*2+0.4f));
		}
	}
}
