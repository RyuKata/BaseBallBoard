using UnityEngine;
using System.Collections;

public class Pitcher : MonoBehaviour {
		public GameObject ball;
		Vector3 speed = new Vector3 (20, 0, 20);
	
		public void Throw(){
				float speedVel = Random.Range (15f, 22f);
				speed = new Vector3 (speedVel, 0, speedVel);
				GameObject throwBall = Instantiate (ball, transform.position, transform.rotation) as GameObject;
				throwBall.rigidbody.velocity = speed; 
		}
}
