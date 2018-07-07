using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaloEffect : MonoBehaviour {

    public Behaviour Halo;
    public float HaloTimeMs = 100;

    private Renderer r;
    // Use this for initialization
    void Start () {
        Halo.enabled = false;
        r = GetComponent<Renderer>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TriggerHalloEffect()
    {
        StartCoroutine(HaloLight());
    }

    public void TriggerBlink()
    {
        StartCoroutine(Blink());
    }

    IEnumerator HaloLight()
    {
        Halo.enabled = true;
        yield return new WaitForSeconds(HaloTimeMs / 1000);
        Halo.enabled = false;
    }

    IEnumerator Blink()
    {
        r.enabled = false;
        yield return new WaitForSeconds(0.2f);
        r.enabled = true;
    }
}
