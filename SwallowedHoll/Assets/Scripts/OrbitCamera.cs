using UnityEngine;

[RequireComponent(typeof(Camera))]
public class OrbitCamera : MonoBehaviour {

	public GameObject player = default;
    MovingSphere sphere = default; 
	

	[SerializeField, Min(0f)]
	[Tooltip("This is how many degrees per second your camera rotates to match you")]
	float upAlignmentSpeed = 360f;

	Quaternion gravityAlignment = Quaternion.identity;

	Quaternion orbitRotation;

	[SerializeField]

	LayerMask obstructionMask = -1;

	Camera regularCamera;

	[SerializeField, Range(0f, 90f)]
	float alignSmoothRange = 45f;

	float lastManualRotationTime;

	[SerializeField, Min(0f)]
	[Tooltip("how long until the camera re-orients behind you")]
	float alignDelay = 5f;

	[SerializeField, Range(-89f, 89f)]
	[Tooltip("how far you can move the camera up or down around your target")]
	float minVerticalAngle = -30f, maxVerticalAngle = 60f;

	Vector2 orbitAngles = new Vector2(45f, 0f);

	[SerializeField, Range(1f, 360f)]
	[Tooltip("how fast the camera orbits your target")]
	float rotationSpeed = 90f;

    [SerializeField, Range(0f, 1f)]
	float focusCentering = 0.5f;

    [SerializeField, Min(0f)]
	float focusRadius = 1f;

    [SerializeField]
	Transform focus = default;
	Transform Prevfocus = default;

	[SerializeField, Range(1f, 20f)]
	[Tooltip("how close your camera stays to the target")]
	float distance = 5f;
    Vector3 focusPoint, previousFocusPoint;

	Vector3 CameraHalfExtends {
		get {
			Vector3 halfExtends;
			halfExtends.y =
				regularCamera.nearClipPlane *
				Mathf.Tan(0.5f * Mathf.Deg2Rad * regularCamera.fieldOfView);
			halfExtends.x = halfExtends.y * regularCamera.aspect;
			halfExtends.z = 0f;
			return halfExtends;
		}
	}
	bool AutomaticRotation () {
		if (Time.unscaledTime - lastManualRotationTime < alignDelay) {
			return false;
		}
		Vector3 alignedDelta = 
			Quaternion.Inverse(gravityAlignment) * 
			(focusPoint - previousFocusPoint);

		Vector2 movement = new Vector2(alignedDelta.x, alignedDelta.z);
		float movementDeltaSqr = movement.sqrMagnitude;
		if (movementDeltaSqr < 0.000001f) {
			return false;
		}

		float headingAngle = GetAngle(movement / Mathf.Sqrt(movementDeltaSqr));
		float deltaAbs = Mathf.Abs(Mathf.DeltaAngle(orbitAngles.y, headingAngle));
		float rotationChange = 
			rotationSpeed * Mathf.Min(Time.unscaledDeltaTime, movementDeltaSqr);
		if (deltaAbs < alignSmoothRange) {
			rotationChange *= deltaAbs / alignSmoothRange;
		}
		else if (180f - deltaAbs < alignSmoothRange) {
			rotationChange *= (180f - deltaAbs) / alignSmoothRange;
		}
		orbitAngles.y =
			Mathf.MoveTowardsAngle(orbitAngles.y, headingAngle, rotationChange);
		
		return true;
	}

	void ConstrainAngles () {
		orbitAngles.x =
			Mathf.Clamp(orbitAngles.x, minVerticalAngle, maxVerticalAngle);

		if (orbitAngles.y < 0f) {
			orbitAngles.y += 360f;
		}
		else if (orbitAngles.y >= 360f) {
			orbitAngles.y -= 360f;
		}
	}
	void OnValidate () {
		if (maxVerticalAngle < minVerticalAngle) {
			maxVerticalAngle = minVerticalAngle;
		}
	}

    void Awake () {
		Prevfocus = focus;
		sphere = player.GetComponent<MovingSphere>();
		regularCamera = GetComponent<Camera>();
		focusPoint = focus.position;
		transform.localRotation = orbitRotation = Quaternion.Euler(orbitAngles);
	}

	bool ManualRotation(){
		Vector2 input = new Vector2(
			Input.GetAxis("Vertical Camera"),
			Input.GetAxis("Horizontal Camera")
		);
		const float e = 0.001f;
		if (input.x < -e || input.x > e || input.y < -e || input.y > e) {
			orbitAngles += rotationSpeed * Time.unscaledDeltaTime * input;
			lastManualRotationTime = Time.unscaledTime;
			return true;
		}
		return false;
	}

	void LateUpdate () {

		//NEED TO DISABLE AUTO ROTATE

		//if (sphere.isAiming){

		//	sphere.parent.transform.GetChild(1).GetChild(10).gameObject.SetActive(true);
		//}
		//if (!sphere.isAiming){
		//	sphere.parent.transform.GetChild(1).GetChild(10).gameObject.SetActive(false);
			//Debug.Log("aGGGH");
		//}

		UpdateGravityAlignment();
		UpdateFocusPoint();
		if(ManualRotation() || AutomaticRotation()){
			ConstrainAngles();
			orbitRotation = Quaternion.Euler(orbitAngles);
		}

		Quaternion lookRotation = gravityAlignment * orbitRotation;

		Vector3 lookDirection = lookRotation * Vector3.forward;
		Vector3 lookPosition = focusPoint - lookDirection * distance;
		Vector3 rectOffset = lookDirection * regularCamera.nearClipPlane;
		Vector3 rectPosition = lookPosition + rectOffset;
		Vector3 castFrom = focus.position;
		Vector3 castLine = rectPosition - castFrom;
		float castDistance = castLine.magnitude;
		Vector3 castDirection = castLine / castDistance;

		if (Physics.BoxCast(
			castFrom, CameraHalfExtends, castDirection, out RaycastHit hit, lookRotation, castDistance, obstructionMask,
			QueryTriggerInteraction.Ignore
		)) {
			rectPosition = castFrom + castDirection * hit.distance;
			lookPosition = rectPosition - rectOffset;
		}
		transform.SetPositionAndRotation(lookPosition, lookRotation);
		
	}
	void UpdateGravityAlignment(){
		Vector3 fromUp = gravityAlignment * Vector3.up;
		Vector3 toUp = CustomGravity.GetUpAxis(focusPoint);
		float dot = Mathf.Clamp(Vector3.Dot(fromUp, toUp), -1f, 1f);
		float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
		float maxAngle = upAlignmentSpeed * Time.deltaTime;

		Quaternion newAlignment =
			Quaternion.FromToRotation(fromUp, toUp) * gravityAlignment;
		if (angle <= maxAngle) {
			gravityAlignment = newAlignment;
		}
		else {
			gravityAlignment = Quaternion.SlerpUnclamped(
				gravityAlignment, newAlignment, maxAngle / angle
			);
		}
	}

    void UpdateFocusPoint () {
		previousFocusPoint = focusPoint;
		Vector3 targetPoint = focus.position;
		if (focusRadius > 0f) {
			float distance = Vector3.Distance(targetPoint, focusPoint);
            float t = 1f;
			if (distance > 0.01f && focusCentering > 0f) {
				t = Mathf.Pow(1f - focusCentering, Time.unscaledDeltaTime);
			}
			if (distance > focusRadius) {
                t = Mathf.Min(t, focusRadius / distance);
			}
            focusPoint = Vector3.Lerp(targetPoint, focusPoint, t);
		}
		else {
			focusPoint = targetPoint;
		}
    }

	static float GetAngle (Vector2 direction) {
		float angle = Mathf.Acos(direction.y) * Mathf.Rad2Deg;
		return direction.x < 0f ? 360f - angle : angle;
	}
}