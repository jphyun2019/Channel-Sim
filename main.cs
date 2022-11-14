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

        for(int test = 0; test < 10; test++)
        {

            defaultSize = 12;
            fullList = new List<Show>();
            for (int i = 0; i < 100; i++)
            {
                fullList.Add(new Show(i.ToString(), (Mathf.Round(UnityEngine.Random.Range(1f, 10.0f))), Color.red));
            }

            float greedyStartTime = DateTime.Now.Millisecond / 1000f + DateTime.Now.Second;
            List<Channel> greedyResult = Greedy(fullList);
            float greedyEndTime = DateTime.Now.Millisecond / 1000f + DateTime.Now.Second;



            float greedyV2StartTime = DateTime.Now.Millisecond / 1000f + DateTime.Now.Second;
            List<Channel> greedyV2Result = GreedyV2();
            float greedyV2EndTime = DateTime.Now.Millisecond / 1000f + DateTime.Now.Second;



            Debug.Log("Greedy: "+ greedyResult.Count + " channels in " + (greedyEndTime - greedyStartTime) + " seconds \nGreedyV2: " + greedyV2Result.Count + " channels in " + (greedyV2EndTime - greedyV2StartTime) + " seconds");






        }
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

    private List<Channel> Greedy(List<Show> inputList)
    {
        int channelId = 0;
        List<Channel> channelList = new List<Channel>();
        bool placed;
        for (int i = 0; i < inputList.Count; i++)
        {
            placed = false;
            if(channelList.Count != 0)
            {
                for (int j = 0; j < channelList.Count; j++)
                {
                    if (inputList[i].length <= channelList[j].size - channelList[j].fill)
                    {
                        channelList[j].showList.Add(inputList[i]);
                        placed = true;
                        channelList[j].fill += inputList[i].length;
                        break;
                    }
                }
            }
            if (!placed)
            {
                channelList.Add(new Channel(new List<Show>() { inputList[i] }, channelId, defaultSize, inputList[i].length));
                channelId++;
            }

        }
        return channelList;
    }

    private List<Channel> GreedyV2()
    {
        List<Channel> channelList = new List<Channel>();
        List<Show> showlist = fullList;
        int bestChannel=-1;
        float bestRemainder=-1;
        for(int i = 0; i < fullList.Count; i++)
        {
            List<Channel> temp = Greedy(showlist);

            if (temp.Count < bestChannel || bestChannel == -1)
            {
                bestChannel = temp.Count;
                bestRemainder = temp[temp.Count - 1].fill;
                channelList = temp;

            }
            else if(temp.Count == bestChannel)
            {
                if(temp[temp.Count-1].fill < bestRemainder)
                {
                    bestRemainder = temp[temp.Count - 1].fill;
                    channelList = temp;
                }
            }
            showlist.Add(showlist[0]);
            showlist.RemoveAt(0);
        }
        return channelList;
    }



    private List<Channel> ReverseFill(List<Show> inputList)
    {
        List<Channel> channelList = new List<Channel>();




        return channelList;
    }














}
