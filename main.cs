using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{
    List<Show> fullList;
    List<Channel> channelList;

    void Start()
    {
        Show a = new Show("a", 9, new Color(2, 3, 4));
        Show b = new Show("b", 8, new Color(2, 3, 4));
        Show c = new Show("c", 7, new Color(2, 3, 4));
        Show d = new Show("d", 6, new Color(2, 3, 4));
        Show e = new Show("e", 5, new Color(2, 3, 4));
        Show f = new Show("f", 4, new Color(2, 3, 4));
        Show g = new Show("g", 3, new Color(2, 3, 4));
        Show h = new Show("h", 2, new Color(2, 3, 4));
        Show i = new Show("i", 1, new Color(2, 3, 4));
        Channel x = new Channel("x", 10);
        channelList = new List<Channel>()
        {x};
        fullList = new List<Show>()
        {a, b, c, d, e, f, g, h, i };

    }

    void Update()
    {
        Debug.Log(channelList[0].showList);
    }
}
