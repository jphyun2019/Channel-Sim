using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{
    List<Show> fullList;
    float defaultSize;

    public static String GetTimestamp(DateTime value)
    {
        return value.ToString("ssffff");
    }

    void Start()
    {
        //Show a = new Show("a", 9, new Color(2, 3, 4));
        //Show b = new Show("b", 8, new Color(2, 3, 4));
        //Show c = new Show("c", 7, new Color(2, 3, 4));
        //Show d = new Show("d", 6, new Color(2, 3, 4));
        //Show e = new Show("e", 5, new Color(2, 3, 4));
        //Show f = new Show("f", 4, new Color(2, 3, 4));
        //Show g = new Show("g", 3, new Color(2, 3, 4));
        //Show h = new Show("h", 2, new Color(2, 3, 4));
        //Show i = new Show("i", 1, new Color(2, 3, 4));
        //defaultSize = 10;

        //fullList = new List<Show>()
        //{a, b, c, d, e, f, g, h, i };


        float startTime = DateTime.Now.Millisecond/1000f + DateTime.Now.Second;

        defaultSize = 15;
        fullList = new List<Show>();
        for(int i = 0; i < 80; i++)
        {
            fullList.Add(new Show(i.ToString(), UnityEngine.Random.Range(3f, 10.0f), Color.red));
        }


        float midtime1 = DateTime.Now.Millisecond / 1000f + DateTime.Now.Second;
        fullList = sortShows(fullList, 0, fullList.Count - 1);
        float midtime2 = DateTime.Now.Millisecond / 1000f + DateTime.Now.Second;
        List<Channel> greedyResult = Greedy();

        foreach(Channel greedy in greedyResult)
        {
            greedy.displayChannel();
        }

        float endTime = DateTime.Now.Millisecond/1000f + DateTime.Now.Second;

        Debug.Log(endTime - startTime + " seconds");
        Debug.Log(midtime1 - startTime + " seconds");
        Debug.Log(midtime2 - midtime1 + "seconds");
        Debug.Log(endTime - midtime2 + "seconds");
    }

    public List<Show> sortShows(List<Show> list, int leftIndex, int rightIndex)
    {
        int i = leftIndex;
        int j = rightIndex;
        Show pivot = list[leftIndex];
        while (i <= j)
        {
            while (list[i].length > pivot.length)
            {
                i++;
            }

            while (list[j].length < pivot.length)
            {
                j--;
            }
            if (i <= j)
            {
                Show temp = list[i];
                list[i] = list[j];
                list[j] = temp;
                i++;
                j--;
            }
        }

        if (leftIndex < j)
            sortShows(list, leftIndex, j);
        if (i < rightIndex)
            sortShows(list, i, rightIndex);
        return list;
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
