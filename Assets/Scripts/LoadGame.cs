﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StaticInfos.score = 20000;
        SceneManager.LoadScene("Game");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
