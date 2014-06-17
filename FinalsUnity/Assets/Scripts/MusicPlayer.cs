using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
    public AudioClip bgm = null;
    public AudioClip mainMenu = null;
    public AudioClip endMenu = null;

    private AudioSource _source = null;

	// Use this for initialization
	void Start () {
        _source = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayMainMenu()
    {
        if (_source != null)
        {
            _source.clip = mainMenu;
            _source.Play();
        }
    }

    public void PlayEndMenu()
    {
        if (_source != null)
        {
            _source.clip = endMenu;
            _source.Play();
        }
    }

    public void PlayBgm()
    {
        if (_source != null)
        {
            _source.clip = bgm;
            _source.Play();
        }
    }
}
