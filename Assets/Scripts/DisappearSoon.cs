using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearSoon : MonoBehaviour {

    public float timeToDisappear = 2f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, timeToDisappear);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
