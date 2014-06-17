using UnityEngine;
using System.Collections;

public class ShowControls : AGUIButton {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    protected override void Trigger()
    {
        foreach (GUIText text in gameObject.GetComponentsInChildren<GUIText>())
        {
            text.enabled = true;
        }
    }
}
