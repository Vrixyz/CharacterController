using UnityEngine;
using System.Collections;

public class StoryText : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GUIText guiText = gameObject.GetComponent<GUIText>();

        guiText.text = "Arzial, newly born demon realizes after one week of destruction that her path is wrong.\n" +
            "She disobey Lucifer’s orders to kill all the people of a small village,\n" +
            "the one where all her family still humans, lived.\n" +
            "While seeking redemption, she crosses the path of an angel, Sinius.\n" +
            "Sinius tells her the way to become a better being: the Gate of Trials.\n" +
            "He brings her to the Gate, but Arzial will have to do the rest.\n";
        //+ "She needs to bring 3 pure hearts to it in order to open it and redeem herself.";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
