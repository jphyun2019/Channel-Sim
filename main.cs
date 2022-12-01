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
        string finalfinal = "";
        for(int run = 11; run<=20; run++)
        {
            float deviation = 0.2f + (0.8f * (run / 20f));
            float topBound = 8;
            float bottomBound = 2;

            int cycles = 20;



            string final = "";


            float ide = bottomBound;
            for (int change = 0; change <= cycles; change++)
            {
                ide = bottomBound + ((topBound - bottomBound) * ((float)change / (float)cycles));

                float difference = 10 - (Mathf.Abs(ide - 5f));

                float min = ide - (difference * deviation);
                float max = ide + (difference * deviation);

                int testCount = 1500;


                List<float> scoreboard = new List<float>() { 0, 0, 0, 0, 0 };
                float testStartTime = DateTime.Now.Millisecond / 1000f + DateTime.Now.Second;

                for (int test = 0; test < testCount; test++)
                {
                    defaultSize = 10;
                    fullList = new List<Show>();
                    for (int i = 0; i < 20; i++)
                    {
                        fullList.Add(new Show(i.ToString(), UnityEngine.Random.Range(min, max), Color.red));
                    }

                    fullList = sortShows(fullList, 0, fullList.Count - 1);


                    float greedyStartTime = DateTime.Now.Millisecond / 1000f + DateTime.Now.Second;
                    List<Channel> greedyResult = Greedy(fullList);
                    float greedyEndTime = DateTime.Now.Millisecond / 1000f + DateTime.Now.Second;



                    float greedyV2StartTime = DateTime.Now.Millisecond / 1000f + DateTime.Now.Second;
                    List<Channel> greedyV2Result = GreedyV2();
                    float greedyV2EndTime = DateTime.Now.Millisecond / 1000f + DateTime.Now.Second;


                    float snakeStartTime = DateTime.Now.Millisecond / 1000f + DateTime.Now.Second;
                    List<Channel> snakeResult = Snake(fullList);
                    float snake2EndTime = DateTime.Now.Millisecond / 1000f + DateTime.Now.Second;


                    float reverseStartTime = DateTime.Now.Millisecond / 1000f + DateTime.Now.Second;
                    List<Channel> reverseResult = ReverseFill(fullList);
                    float reverseEndTime = DateTime.Now.Millisecond / 1000f + DateTime.Now.Second;


                    float reverseV2StartTime = DateTime.Now.Millisecond / 1000f + DateTime.Now.Second;
                    List<Channel> reverseV2Result = ReverseFillV2(fullList);
                    float reverseV2EndTime = DateTime.Now.Millisecond / 1000f + DateTime.Now.Second;


                    // Debug.Log("Greedy: "+ greedyResult.Count + " channels in " + Math.Round(greedyEndTime - greedyStartTime, 5) + " seconds  GreedyV2: " + greedyV2Result.Count + " channels in " + Math.Round((greedyV2EndTime - greedyV2StartTime), 5) + " seconds    Snake: " + snakeResult.Count + " channels in " + Math.Round((snake2EndTime - snakeStartTime), 5) + " seconds   Reverse Fill: " + reverseResult.Count + " channels in " + Math.Round((reverseEndTime - reverseStartTime), 5) + " seconds   Reverse Fill V2: " + reverseV2Result.Count + " channels in " + Math.Round((reverseV2EndTime - reverseV2StartTime), 5) + " seconds");


                    List<List<Channel>> allSorts = new List<List<Channel>>() { greedyResult, greedyV2Result, snakeResult, reverseResult, reverseV2Result };

                    //int bestChannel = -1;
                    //float bestRemainder = -1;

                    //int counter = 0;




                    float best;
                    int place = 4;

                    List<List<Channel>> placed = new List<List<Channel>>() { };
                    while (placed.Count < 5)
                    {

                        List<int> temp = new List<int>() { };
                        best = -1f;

                        int counter = 0;
                        foreach (List<Channel> sort in allSorts)
                        {
                            if (!placed.Contains(sort))
                            {

                                if (best < 0 || (sort.Count - 1f + (sort[sort.Count - 1].fill / (float)defaultSize)) < best)
                                {

                                    temp.Clear();
                                    temp.Add(counter);
                                    best = sort.Count - 1f + (sort[sort.Count - 1].fill / (float)defaultSize);

                                }
                                else if ((sort.Count - 1f + (sort[sort.Count - 1].fill / (float)defaultSize)) == best)
                                {
                                    temp.Add(counter);
                                }
                            }
                            counter++;
                        }
                        foreach (int s in temp)
                        {
                            placed.Add(allSorts[s]);

                            scoreboard[s] += place;

                        }
                        place--;

                    }
                }

                for (int i = 0; i < scoreboard.Count; i++)
                {
                    scoreboard[i] = scoreboard[i] / testCount;
                }

                float testEndTime = DateTime.Now.Millisecond / 1000f + DateTime.Now.Second;

                //final += ("Greedy: " + scoreboard[0] + "    GreedyV2: " + scoreboard[1] + "    Snake: " + scoreboard[2] + "    Reverse Fill: " + scoreboard[3] + "    ReverseV2 Fill: " + scoreboard[4] + "\n");
                final += (Math.Round(ide, 4) + "," + Math.Round(deviation, 4) + "," + scoreboard[0] + "," + scoreboard[1] + "," + scoreboard[2] + "," + scoreboard[3] + "," + scoreboard[4] + "\n");

            }

            finalfinal += final;
        }

        Debug.Log(finalfinal);

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



    private List<Channel> ReverseFillV2(List<Show> inputList)
    {
        List<Channel> channelList = new List<Channel>();
        int channelId = 0;
        List<Show> tempInputList = new List<Show>();

        foreach(Show s in inputList)
        {
            tempInputList.Add(s);
        }

        int doings = 0;
        while (tempInputList.Count > 0)
        {
            Channel temp = new Channel(new List<Show>() { tempInputList[0] }, channelId, defaultSize, tempInputList[0].length);

            if(tempInputList.Count != 0)
            {
                int counter = tempInputList.Count - 1;
                float difference = temp.size - temp.fill;
                while (difference > tempInputList[counter].length)
                {
                    doings++;
                    temp.showList.Add(tempInputList[counter]);
                    temp.fill += tempInputList[counter].length;

                    if (counter > 1)
                    {
                        counter--;
                    }
                    else
                    {
                        break;
                    }
                    difference = temp.size - temp.fill;
                }

                //temp.displayChannel();
                temp.showList.Reverse();

                int range = temp.showList.Count - 2;
                if(range > 0)
                {
                    for (int i = range; i > 0; i--)
                    {
                        difference = temp.size - temp.fill;

                        int differenceCounter = tempInputList.Count - temp.showList.Count;

                        if (differenceCounter > 0)
                        {
                            while (tempInputList[differenceCounter].length - temp.showList[i].length < difference)
                            {
                                differenceCounter--;

                                if (differenceCounter == 0)
                                {
                                    break;
                                }

                            }
                        }

                        if (differenceCounter != (tempInputList.Count - temp.showList.Count))

                        {
                            //Debug.Log(temp.showList[i].length + " -> " + tempInputList[differenceCounter + 1].length);
                            temp.fill -= temp.showList[i].length;
                            temp.showList.RemoveAt(i);
                            temp.showList.Add(tempInputList[differenceCounter + 1]);
                            temp.fill += tempInputList[differenceCounter + 1].length;
                            tempInputList.RemoveAt(differenceCounter + 1);

                        }
                    }
                }
                

                //temp.displayChannel();
                foreach (Show s in temp.showList)
                {
                    tempInputList.Remove(s);
                }

            }

            channelList.Add(temp);
            channelId++;
        }

        return channelList;
    }


    private List<Channel> ReverseFill(List<Show> inputList)
    {
        List<Channel> channelList = new List<Channel>();
        int channelId = 0;
        List<Show> tempInputList = new List<Show>();

        foreach (Show s in inputList)
        {
            tempInputList.Add(s);
        }

        int doings = 0;
        while (tempInputList.Count > 0)
        {
            Channel temp = new Channel(new List<Show>() { tempInputList[0] }, channelId, defaultSize, tempInputList[0].length);

            if (tempInputList.Count != 0)
            {
                int counter = tempInputList.Count - 1;
                float difference = temp.size - temp.fill;
                while (difference > tempInputList[counter].length)
                {
                    doings++;
                    temp.showList.Add(tempInputList[counter]);
                    temp.fill += tempInputList[counter].length;

                    if (counter > 1)
                    {
                        counter--;
                    }
                    else
                    {
                        break;
                    }
                    difference = temp.size - temp.fill;
                }

                foreach (Show s in temp.showList)
                {
                    tempInputList.Remove(s);
                }
            }

            channelList.Add(temp);
            channelId++;
        }

        return channelList;
    }


    private List<Channel> Snake(List<Show> inputList)
    {
        List<Channel> channelList = new List<Channel>();
        int channelId = 0;
        List<Show> tempInputList = new List<Show>();

        foreach (Show s in inputList)
        {
            tempInputList.Add(s);
        }


        while (tempInputList.Count > 0)
        {
            Channel temp = new Channel(new List<Show>() {tempInputList[0] }, channelId, defaultSize, tempInputList[0].length);
            tempInputList.RemoveAt(0);
            if(tempInputList.Count!= 0)
            {
                if ((temp.size - temp.fill) > tempInputList[tempInputList.Count-1].length)
                {
                    temp.showList.Add(tempInputList[tempInputList.Count - 1]);
                    temp.fill += tempInputList[tempInputList.Count - 1].length;
                    tempInputList.RemoveAt(tempInputList.Count - 1);
                }
            }

            channelList.Add(temp);
            channelId++;

        }
        return channelList;
    }










}
