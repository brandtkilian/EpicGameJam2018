using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jacksonne
{

    public static Object[] jacksonnesamer;

    static Jacksonne()
    {
        jacksonnesamer = Resources.LoadAll("Jackson");
    }

    public static AudioClip GetRandomJackson()
    {   
        return jacksonnesamer[Random.Range(0, jacksonnesamer.Length)] as AudioClip;
    }


}
