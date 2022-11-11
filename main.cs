using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{
    List<Show> fullList;
    float defaultSize;

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
        defaultSize = 10;

        fullList = new List<Show>()
        {a, b, c, d, e, f, g, h, i };

        List<Channel> greedyResult = Greedy();

        foreach(Channel greedy in greedyResult)
        {
            greedy.displayChannel();
        }
        




    }


    private List<Channel> Greedy()
    {
        int channelId = 0;
        List<Channel> channelList = new List<Channel>();
        bool placed;
        for (int i = 0; i < fullList.Count; i++)
        {
            placed = false;
            if(channelList.Count != 0)
            {
                for (int j = 0; j < channelList.Count; j++)
                {
                    if (fullList[i].length <= channelList[j].size - channelList[j].fill)
                    {
                        channelList[j].showList.Add(fullList[i]);
                        placed = true;
                        channelList[j].fill += fullList[i].length;
                        break;
                    }
                }
            }
            if (!placed)
            {
                channelList.Add(new Channel(new List<Show>() { fullList[i] }, channelId, defaultSize, fullList[i].length));
                channelId++;

                
            }

        }




        return channelList;
        
    }











}
