using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowEndScreen : MonoBehaviour {


    public Text ScoreText;
    public GameObject EndScreen;

    private Renderer panelR;
    private bool ended = false;
    public AudioClip EndClip;
    public AudioSource ASource;

	// Use this for initialization
	void Start () {
        panelR = EndScreen.GetComponent<Renderer>();
        panelR.enabled = false;
        ScoreText.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (StaticInfos.spawnsDestroyed == StaticInfos.totalSpawns && !ended)
            EndGame();
    }


    void EndGame()
    {
        ended = true;
        ScoreText.text = "Score: " + StaticInfos.score;
        Time.timeScale = 0;
        panelR.enabled = true;
        ScoreText.enabled = true;
        ASource.Stop();
        ASource.volume = 100;
        ASource.clip = EndClip;
        ASource.Play();
    }
}
