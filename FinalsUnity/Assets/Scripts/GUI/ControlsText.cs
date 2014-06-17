using UnityEngine;
using System.Collections;

public class ControlsText : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GUIText guiText = gameObject.GetComponent<GUIText>();

        guiText.text = "WASD to move\nSpacebar to jump\nMouse to look around\nescape to pause";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
