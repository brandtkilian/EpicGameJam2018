using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyShit : MonoBehaviour {

    AudioSource audioData;

    
    private bool isColliding = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        isColliding = false;
	}

    void OnTriggerEnter(Collider other)
    {
        if (isColliding) return;
        isColliding = true;
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
        Destroy(other.gameObject, 3f);
        StaticInfos.score -= StaticInfos.ScoreDecrease;
        StaticInfos.spawnsDestroyed++;
        StaticInfos.AnimateMichael = false;
        StaticInfos.Streak = 0;

    }

}
