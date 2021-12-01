using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
//this script allows you to drag and drop game obejcts in the editor and select a a specific method to be called in one way or another. has functionality to work on volumes as well. 
public class InteractableObject : MonoBehaviour {
	
	[SerializeField]
	public bool isVolume;


	[SerializeField]
	UnityEvent onFirstEnter = default, onLastExit = default;
	List<Collider> colliders = new List<Collider>();

	void Awake () {
		enabled = false;
	}
	void OnDisable () {
	#if UNITY_EDITOR
		if (enabled && gameObject.activeInHierarchy) {
			return;
		}
	#endif
		if (colliders.Count > 0) {
			colliders.Clear();
			onLastExit.Invoke();
		}
	}

	void FixedUpdate () {
		if(isVolume){
			for (int i = 0; i < colliders.Count; i++) {
				Collider collider = colliders[i];
				if (!collider || !collider.gameObject.activeInHierarchy) {
					colliders.RemoveAt(i--);
					if (colliders.Count == 0) {
						onLastExit.Invoke();
						enabled = false;
					}
				}
			}
		}
	}

	public void Press(){
		onFirstEnter.Invoke();
	}
	public void Release(){
		onLastExit.Invoke();
	}

    void OnTriggerEnter (Collider other) {
		if(isVolume){
			if (colliders.Count == 0) {
				onFirstEnter.Invoke();
				enabled = true;
			}
			colliders.Add(other);
		}

	}

	void OnTriggerExit (Collider other) {
		if(isVolume){
			if (colliders.Remove(other) && colliders.Count == 0) {
				onLastExit.Invoke();
				enabled = false;
			}
		}
	}
}
