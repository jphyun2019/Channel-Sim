using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Channel{

    public List<Show> showList;
    public string name;
    public float size;
    

    public Channel()
    {
        showList = new List<Show>();
        name = "unNamedChannel";
        size = 0;
    }
    public Channel(string name, float size)
    {
        this.showList = new List<Show>();
        this.name = name;
        this.size = size;
    }

}