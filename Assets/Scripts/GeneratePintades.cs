using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePintades : MonoBehaviour {

    public GameObject Left;
    public GameObject Right;
    public GameObject Up;
    public GameObject Down;

    public GameObject ILeft;
    public GameObject IRight;
    public GameObject IUp;
    public GameObject IDown;

    public float SpeedVariationPercents = 0.5f;
    public float StartDelay = 2000f;
    public float MsDelay = 300f;
    public float DelayBeforeText = 10000f;
    public float DelayAfterText = 10000f;
    public float YPosPintades = 10f;
    public float YPosIngurgitator = -3.6f;
    public float FallSpeed = 1f;
    public float ProbaWordPercent = 15;


    private float MinPosX = 0;
    private float MaxPosX = 0;
    private float elapsed = 0;
    private List<string> words;
    private string word;
    private int letterCount = 0;
    private List<KeyValuePair<GameObject, int>> spawnMapPosX;
    private List<KeyValuePair<GameObject, int>> cunniLinguMapPosX;
    private List<GameObject> ingurgitators;
    private List<GameObject> textIngurgitators;
    private bool isArrowGenerated = true;
    private KeyCode[] keys = Enum.GetValues(typeof(KeyCode)) as KeyCode[];
    private List<KeyCode> currentKeys;
    private bool started = false;
    private int spawned = 0;


    private UnityEngine.Object[] lettersTexture;


    // Use this for initialization
    void Start ()
    {
        lettersTexture = Resources.LoadAll("Letters", typeof(Texture2D));
        ingurgitators = new List<GameObject>();
        textIngurgitators = new List<GameObject>();
        currentKeys = new List<KeyCode>();

        spawnMapPosX = new List<KeyValuePair<GameObject, int>>();
        spawnMapPosX.Add(new KeyValuePair<GameObject, int>(Right, 3));
        spawnMapPosX.Add(new KeyValuePair<GameObject, int>(Down, 1));
        spawnMapPosX.Add(new KeyValuePair<GameObject, int>(Up, -1));
        spawnMapPosX.Add(new KeyValuePair<GameObject, int>(Left, -3));

        cunniLinguMapPosX = new List<KeyValuePair<GameObject, int>>();
        cunniLinguMapPosX.Add(new KeyValuePair<GameObject, int>(IRight, 3));
        cunniLinguMapPosX.Add(new KeyValuePair<GameObject, int>(IDown, 1));
        cunniLinguMapPosX.Add(new KeyValuePair<GameObject, int>(IUp, -1));
        cunniLinguMapPosX.Add(new KeyValuePair<GameObject, int>(ILeft, -3));

        foreach(var ingu in cunniLinguMapPosX)
        {
            var obj = Instantiate(ingu.Key, new Vector3(ingu.Value, YPosIngurgitator, -1), Quaternion.identity);
            obj.transform.rotation = Quaternion.Euler(0, 0, 180);
            ingurgitators.Add(obj);
        }

        words = new List<string>();
        words.Add("yellow");
        words.Add("blue");
        StaticInfos.totalSpawns = StaticInfos.score / StaticInfos.ScoreDecrease;
        StaticInfos.spawnsDestroyed = 0;
    }
	
	// Update is called once per frame
	void Update () {
        elapsed += Time.deltaTime * 1000;
        if (elapsed > StartDelay && !started)
        {
            started = true;
        }
        if (!started) return;

        if (isArrowGenerated)
        {
            if (elapsed > MsDelay)
            {
                elapsed = 0;
                SpawnRandomPintade();
                if (UnityEngine.Random.Range(0, 100) < ProbaWordPercent)
                {
                    word = words[UnityEngine.Random.Range(0, words.Count)];
                    if(word.Length < StaticInfos.totalSpawns - spawned)
                        isArrowGenerated = false;
                }
            }
        }
        else
        {
            if(elapsed > DelayBeforeText && textIngurgitators.Count == 0)
            {
                foreach (var ingu in ingurgitators)
                    ingu.transform.position = new Vector3(ingu.transform.position.x, ingu.transform.position.y, 10);
                GenerateTextIngurgitator();
            }

            if (elapsed > DelayBeforeText && letterCount < word.Length)
            {
                if (elapsed > DelayBeforeText + (letterCount + 1) * MsDelay)
                {
                    SpawnLitteralPintade();
                }
            }
            else
            {
                if (elapsed > DelayBeforeText + (letterCount + 1) * MsDelay + DelayAfterText)
                {
                    ResetGameState();
                }
            }

        }

    }

    private void ResetGameState()
    {
        foreach (var qq in textIngurgitators)
            Destroy(qq);
        textIngurgitators.Clear();
        foreach (var zizi in ingurgitators)
            zizi.transform.position = new Vector3(zizi.transform.position.x, zizi.transform.position.y, -1);
        isArrowGenerated = true;
        elapsed = 0;
        letterCount = 0;
        currentKeys.Clear();
    }

    private void GenerateTextIngurgitator()
    {
        MinPosX = -(word.Length  - 1);
        MaxPosX = -MinPosX;
        float posX = MinPosX;
        float step = (MaxPosX - MinPosX) / (word.Length - 1);
        foreach (char c in word)
        {
            Texture2D t = findTexture(c);
            GameObject turlututu = Instantiate(IUp, new Vector3(posX, YPosIngurgitator, -1), Quaternion.Euler(0, 0, 180)) as GameObject;
            turlututu.GetComponent<Renderer>().material.mainTexture = t;
            var kuku = turlututu.GetComponent(typeof(KeyKeyrbutace)) as KeyKeyrbutace;
            keys = Enum.GetValues(typeof(KeyCode)) as KeyCode[];
            foreach (KeyCode k in keys)
            {
                if (k.ToString() == c.ToString().ToUpper())
                {
                    kuku.key = k;
                    currentKeys.Add(k);
                }
            }
            textIngurgitators.Add(turlututu);
            posX += step;
        }
    }

    void SpawnRandomPintade()
    {
        var toSpawn = spawnMapPosX[UnityEngine.Random.Range(0, spawnMapPosX.Count)];
        var obj = Instantiate(toSpawn.Key, new Vector3(toSpawn.Value, YPosPintades, -1), Quaternion.Euler(0, 0, 180));
        SetVelocity(obj, true);
        spawned++;
    }

    private void SetVelocity(GameObject obj, bool variate)
    {
        var vel = -FallSpeed * (variate ? UnityEngine.Random.Range(1, 1 + SpeedVariationPercents) : 1);
        var rb = obj.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, vel, 0);
    }

    void SpawnLitteralPintade()
    {
        float step = (MaxPosX - MinPosX) / (word.Length - 1);
        float posX = MinPosX + step * letterCount;
        var zbouebe = Instantiate(Up, new Vector3(posX, YPosPintades, -1), Quaternion.Euler(0, 0, 180));
        SetVelocity(zbouebe, false);

        zbouebe.GetComponent<Renderer>().material.mainTexture = findTexture(currentKeys[letterCount].ToString().ToLower().ToCharArray()[0]);
        var wligliglick = zbouebe.GetComponent<ConsumeWhenUserDoRightShit>() as ConsumeWhenUserDoRightShit;
        wligliglick.key = currentKeys[letterCount];
        letterCount++;
        spawned++;
    }

    Texture2D findTexture(char c)
    {
        foreach(var o in lettersTexture)
        {
            if (o.name.ToLower() == c.ToString().ToLower())
                return o as Texture2D;
        }
        return lettersTexture[0] as Texture2D;
    }


}
