using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Throwable : MonoBehaviour {

	public float throwSpeed;
	public Vector3 startPos;

	void Start() {
		gameObject.AddListener (EventTriggerType.PointerDown, Hold);
		gameObject.AddListener (EventTriggerType.PointerUp, Release);

		startPos = gameObject.transform.position;
	}

	public void Hold() {
		// get pointer Transform
		Transform pointerTransform = GvrPointerInputModule.Pointer.PointerTransform;

		// set GameObject parent to pointer
		transform.SetParent (pointerTransform, false);

		// pos in view
		transform.localPosition = new Vector3(0,0,3);

		// enable kinematic
		GetComponent<Rigidbody>().isKinematic = true;
	}

	public void Release() {
		// remove parent
		transform.SetParent(null, true);

		// get rigidbody 
		Rigidbody rigidbody = GetComponent<Rigidbody>();

		// reset velocity
		rigidbody.velocity = Vector3.forward * throwSpeed;

		// disable kinematic
		rigidbody.isKinematic = false;
	}

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "Floor" || col.gameObject.tag == "Customer") {
			transform.position = startPos;
			GetComponent<Rigidbody> ().velocity = Vector3.zero;
		}
	}

}

public static class EventExtensions {

	public static void AddListener(this GameObject gameObject,
		EventTriggerType eventTriggerType,
		UnityAction action) {
		// get the EventTrigger component; if it doesn't exist, create one and add it
		EventTrigger eventTrigger = gameObject.GetComponent<EventTrigger>()
			?? gameObject.AddComponent<EventTrigger>();

		// check to see if the entry already exists
		EventTrigger.Entry entry;
		entry = eventTrigger.triggers.Find(e => e.eventID == eventTriggerType);

		if (entry == null) {
			// if it does not, create and add it
			entry = new EventTrigger.Entry {eventID = eventTriggerType};

			// add the entry to the triggers list
			eventTrigger.triggers.Add(entry);
		}

		// add the callback listener
		entry.callback.AddListener(_ => action());
	}

}