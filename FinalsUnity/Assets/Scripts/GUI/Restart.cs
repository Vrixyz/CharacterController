using UnityEngine;
using System.Collections;

public class Restart : AGUIButton
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void Trigger()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}