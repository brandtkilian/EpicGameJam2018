using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadKeyTiming : MonoBehaviour {


    private bool isColliding = false;
    private KeyCode key;
    private HaloEffect Effects;

	// Use this for initialization
	void Start () {
        key = GetComponent<KeyKeyrbutace>().key;
        Effects = GetComponent<HaloEffect>();
	}

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKeyDown(key)) && !isColliding)
        {
            StaticInfos.score -= StaticInfos.ScoreDecrease;
            Effects.TriggerBlink();
            // TODO Play fail sound
        }
    }

    void OnTriggerStay(Collider other)
    {
        isColliding = true;
    }

    void OnTriggerEnter(Collider other)
    {
        isColliding = true;
    }

    void OnTriggerExit(Collider other)
    {
        isColliding = false;
    }
}
