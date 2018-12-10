using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class is purely used so that other scripts can be derived from it.
//It does not need to be placed on a GameObject

public class Interactable : MonoBehaviour {

	//Is the object being focused
	bool isFocus = false;

	Transform player;

	//boolean to tack if the objects has interected
	bool hasInteracted = false;

	//virtual methods can be called from within the base class but it can be over-ridden in any class that uses it
	//Therefore this method can be over-ridden by classes that call it e.g. ItemPickup
	public virtual void Interact()
	{
		Debug.Log ("Interacting with: " + transform.name);
	}
	

	void Update()
	{
		if (isFocus && !hasInteracted) 
		{			
			//If the interact key is pressed
			if (Input.GetKeyDown (KeyCode.I)) 
			{
                // Calls the Interact method
				Interact ();
                //This line here could be an issue with continuing dialogue. This may need to be moved to be within specific items Interact functions
				hasInteracted = true;
			}
		}
	}

	//method to detect the focus. takes in a Transform called playerTransform
	public void OnFocused(Transform playerTransform)
	{
		isFocus = true;
		player = playerTransform;
		hasInteracted = false;
	}

	//method to detect when focus ends
	public void OnDefocused()
	{
		isFocus = false;
		player = null;
		hasInteracted = false;
	}
}
