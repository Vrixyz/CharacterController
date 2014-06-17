using UnityEngine;
using System.Collections;

public abstract class AGUIButton : MonoBehaviour {
    public int defaultSize = 50;
    public int focusSize = 55;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseEnter()
    {
        GUIText guiText = gameObject.GetComponent<GUIText>();

        guiText.fontSize = focusSize;
    }

    void OnMouseExit()
    {
        GUIText guiText = gameObject.GetComponent<GUIText>();

        guiText.fontSize = defaultSize;
    }

    void OnMouseUp()
    {
        Trigger();
    }

    protected abstract void Trigger();
}
