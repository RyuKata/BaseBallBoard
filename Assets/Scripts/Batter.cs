using UnityEngine;
using System.Collections;

public class Batter : MonoBehaviour {
		float power = 250;

		void OnCollisionEnter(Collision collision) {
				Vector3 vel = rigidbody.velocity.normalized * power;
				collision.rigidbody.AddForce(vel);
		}
}
