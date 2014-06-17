using UnityEngine;
using System.Collections;

public class StartGame : AGUIButton {
    private GUIText _guiText = null;
    private Camera_Manager _mainCameraManager = null;
    private bool _pause = true;
    private GameObject _menuCamera;
    private bool _newGame = true;
    private Quaternion _originRotation;

	// Use this for initialization
	void Start () {
        _guiText = gameObject.GetComponent<GUIText>();
        _mainCameraManager = Camera.main.GetComponent<Camera_Manager>();
        _menuCamera = GameObject.FindGameObjectWithTag("MenuCamera");
        _originRotation = _menuCamera.transform.localRotation;

        PauseGame();
        _guiText.text = "Start";
	}
	
	// Update is called once per frame
	void Update () {
        if (!_pause && Input.GetKeyDown("escape"))
        {
            PauseGame();
        }
	}

    public void PauseGame()
    {       
        _mainCameraManager.enabled = false;
        Time.timeScale = 0;
        _menuCamera.SetActive(true);
        _menuCamera.transform.rotation = _originRotation;
        _guiText.text = "Continue";
        _pause = true;
        GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<MusicPlayer>().PlayMainMenu();
        transform.parent.gameObject.GetComponent<ActivateGUI>().SetActivate(true);
        GameObject.FindGameObjectWithTag("Controls").GetComponent<ActivateGUI>().SetActivate(false);
    }

    protected override void Trigger()
    {
        if (_newGame)
        {
            GameObject menuCamera = GameObject.FindGameObjectWithTag("MenuCamera");
            menuCamera.transform.localRotation = new Quaternion(0, menuCamera.transform.localRotation.y,
                menuCamera.transform.localRotation.z, menuCamera.transform.localRotation.w);

            transform.parent.gameObject.GetComponent<ActivateGUI>().SetActivate(false);
            enabled = true;
            _guiText.enabled = true;
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Story"))
            {
                go.GetComponent<GUIText>().enabled = true;
            }
            _guiText.text = "Continue";
            _newGame = false;
        }
        else
        {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Story"))
            {
                go.GetComponent<GUIText>().enabled = false;
            }
            Time.timeScale = 1;
            _mainCameraManager.enabled = true;
            _menuCamera.SetActive(false);
            _pause = false;
            GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<MusicPlayer>().PlayBgm();
        }
    }
}
