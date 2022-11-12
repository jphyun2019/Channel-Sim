using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Channel
{

    public List<Show> showList;
    public int id;
    public float size;
    public float fill;
    

    public Channel()
    {
        showList = new List<Show>();
        id = 0;
        size = 0;
    }
    public Channel(int id, float size)
    {
        this.showList = new List<Show>();
        this.id = id;
        this.size = size;
    }
    public Channel(List<Show> showlist, int id, float size, float fill)
    {
        this.showList = showlist;
        this.id = id;
        this.size = size;
        this.fill = fill;
    }
    public void displayChannel()
    {
        string summary = "Channel: " + id + " Fill: " + fill + "    ";
        foreach(Show s in showList)
        {
            summary += s.length + ", ";
        }
        Debug.Log(summary);
    }



}