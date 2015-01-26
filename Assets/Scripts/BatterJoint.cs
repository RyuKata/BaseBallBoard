using UnityEngine;
using System.Collections;

public class BatterJoint : MonoBehaviour {
		Quaternion defaultRot;
		bool swing = false;
	// Use this for initialization
	void Start () {
				defaultRot = transform.rotation;
	}
	// Update is called once per frame
	void Update () {
				if (Input.GetMouseButton (0) && !swing) {
						//swing = true;
						transform.Rotate (0, -400f * Time.deltaTime, 0);
				} else {
						transform.rotation = defaultRot;
				}
				/*while (swing) {
						transform.Rotate (0, -300f * Time.deltaTime, 0);
					if (transform.rotation.y < defaultRot.y -360) {
								swing = false;
								transform.rotation = defaultRot;
								break;
						}
				}*/
	}
}
