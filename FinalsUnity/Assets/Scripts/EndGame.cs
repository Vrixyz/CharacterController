using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        bool end = true;

        foreach (SetHeart heart in gameObject.GetComponentsInChildren<SetHeart>())
        {
            if (!heart.IsDone())
            {
                end = false;
            }
        }

        if (end)
        {
            StartGame start = GameObject.FindGameObjectWithTag("PauseManager").GetComponent<StartGame>();

            start.PauseGame();

            GameObject menuCamera = GameObject.FindGameObjectWithTag("MenuCamera");
            menuCamera.transform.localRotation = new Quaternion(0, menuCamera.transform.localRotation.y,
                menuCamera.transform.localRotation.z, menuCamera.transform.localRotation.w);

            ActivateGUI mainMenu = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<ActivateGUI>();

            mainMenu.SetActivate(false);

            ActivateGUI endMenu = GameObject.FindGameObjectWithTag("EndMenu").GetComponent<ActivateGUI>();

            endMenu.SetActivate(true);
            enabled = false;
            GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<MusicPlayer>().PlayEndMenu();
        }
	}
}
