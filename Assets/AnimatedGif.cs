using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedGif : MonoBehaviour {


    private Object[] frames;
    private Texture2D[] casted;

    public GameObject Gif;
    private List<GameObject> Duplicates;

    private int framesPerSecond = 10;
    private Renderer r;
    private int lastStreak = 0;
    // Use this for initialization
    void Start () {
        Duplicates = new List<GameObject>();
		frames = Resources.LoadAll("JacksonGif", typeof(Texture2D));
        casted = new Texture2D[frames.Length];
        for (int i = 0; i < frames.Length; i++)
            casted[i] = frames[i] as Texture2D;

        r = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if(StaticInfos.AnimateMichael)
        {
            int index = (int)(Time.time * framesPerSecond) % casted.Length;
            r.material.mainTexture = casted[index];

            if(StaticInfos.Streak > 10 && !StaticInfos.JacksonDuplicate)
            {
                Duplicates.Add(Instantiate(Gif, new Vector3(7, -1, -2), Quaternion.Euler(0,0,180)));
                StaticInfos.JacksonDuplicate = true;
                lastStreak = StaticInfos.Streak;
            }
            else
            {
                if (StaticInfos.Streak < 10)
                {
                    foreach(var ob in Duplicates)
                        Destroy(ob);
                    StaticInfos.JacksonDuplicate = false;
                }
            }
        }
        
    }
}
