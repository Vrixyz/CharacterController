using UnityEngine;
using System.Collections;

public class ActivateGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetActivate(bool active)
    {
        foreach (GUIText text in gameObject.GetComponentsInChildren<GUIText>()) {
            if (text != gameObject.GetComponent<GUIText>())
                text.enabled = active;
        }
        foreach (AGUIButton button in gameObject.GetComponentsInChildren<AGUIButton>())
        {
            if (button != gameObject.GetComponent<AGUIButton>())
                button.enabled = active;
        }
    }
}
