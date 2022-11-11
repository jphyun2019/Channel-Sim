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
    public Channel(List<Show> showList, string name, float size)
    {
        this.showList = showList;
        this.name = name;
        this.size = size;
    }

}