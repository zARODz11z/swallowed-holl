using UnityEngine;

public class AccelerationZone : MonoBehaviour {
	public bool Gate = false;

	[SerializeField]
	public float acceleration = 10f, speed = 10f;
	void OnTriggerEnter (Collider other) {
		Rigidbody body = other.attachedRigidbody;
		if (body && other.gameObject.layer != 14) {
			Accelerate(body);
		}
	}
	void OnTriggerStay (Collider other) {
		Rigidbody body = other.attachedRigidbody;
		if (body && other.gameObject.layer != 14) {
			Accelerate(body);
		}
	}

	public void gateSwap(){
		if(Gate){
			Gate = false;
		}
		else{
			Gate = true;
		}
	}

	void Accelerate(Rigidbody body) {
		if(Gate){
			Vector3 velocity = transform.InverseTransformDirection(body.velocity);
			if (velocity.y >= speed) {
				return;
			}
			if (body.TryGetComponent(out FPSMovingSphere sphere)) {
				sphere.PreventSnapToGround();
			}

			if (acceleration > 0f) {
				velocity.y = Mathf.MoveTowards(
					velocity.y, speed, acceleration * Time.deltaTime
				);
			}
			else {
				velocity.y = speed;
			}
			body.velocity = transform.TransformDirection(velocity);
		}
	}
}
