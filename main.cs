using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{
    List<Show> fullList;
    List<Channel> channelList;

    void Start()
    {
        Show a = new Show("a", 5, new Color(2, 3, 4));
        Show b = new Show("a", 5, new Color(2, 3, 4));
        Show c = new Show("a", 5, new Color(2, 3, 4));
        Channel c = new Channel();
        channelList = new List<Channel>()
        {c};

    }

    void Update()
    {
        Debug.Log(channelList[0].showList);
    }
}
