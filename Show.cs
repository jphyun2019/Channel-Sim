using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show : MonoBehaviour
{

    public string name;
    public float length;
    public Color color;



    public Show()
    {
        this.name = "unNamedShow";
        this.length = 1;
        this.color = Color.red;

    }
    public Show(string name, float length, Color color)
    {
        this.name = name;
        this.length = length;
        this.color = color;

    }

}
