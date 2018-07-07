using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeWhenUserDoRightShit : MonoBehaviour {

    public KeyCode key;
    private AudioSource s;
    // Use this for initialization
    void Start () {
        s = gameObject.GetComponent<AudioSource>() as AudioSource;
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "ingurgitator")
        {
            var keyToPress = other.gameObject.GetComponent(typeof(KeyKeyrbutace)) as KeyKeyrbutace;

            if (keyToPress.key == key)
            {
                if (Input.GetKeyDown(key) || Input.GetKeyUp(key))
                {
                    s.PlayOneShot(Jacksonne.GetRandomJackson());
                    gameObject.transform.position = new Vector3(0, 100, 0);
                    Destroy(gameObject, 3f);
                    StaticInfos.spawnsDestroyed++;
                    (other.gameObject.GetComponent<HaloEffect>() as HaloEffect).TriggerHalloEffect();
                    StaticInfos.AnimateMichael = true;
                    StaticInfos.Streak++;
                }
            }
        }
        
        
    }
}
