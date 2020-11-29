﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomMid3Manager : MonoBehaviour
{
    private List<int> selectedNumbers = new List<int>();
    public struct Mode
    {
        public int a;
        public int b;
        public string name;

        public Mode(int a, int b, string name)
        {
            this.a = a;
            this.b = b;
            this.name = name;
        }
    }
    private Mode[] modes =
    {
        new Mode(0,0, "Normal row, left-right"),new Mode(0,4,"Normal raw, right-left"),new Mode(4,0,  "Inverse row, left-right"),
        new Mode(4,4, "Inverse row, right-left"),new Mode(0,0, "Normal column, top-down"),new Mode(0,4,  "Normal column, down-top"),
        new Mode(4,0, "Inverse column, top-down"),new Mode(4,4,"Inverse column, down-top")
    };
    
    private int[,] matrix = new int[5, 5] {
                                            {0,1,2,3,4},
                                            {5,6,7,8,9},
                                            {10,11,12,13,14},
                                            {15,16,17,18,19},
                                            {20,21,22,23,24}
                                            };

    private void SelectNumbers()
    {
        int remaining = 5;
        int chosen;
        List<int> numbers = new List<int>();
        

        int countA = 5;
        int modeID = Random.Range(0,8);
        Mode mode = modes[modeID];
        int a = mode.a;
        int b = mode.b;
        while (countA != 0)
        {
            int countB = 5;
            while (countB != 0)
            {
                //se mode riga
                if (modeID <= 3) numbers.Add(matrix[a,b]);
                //se mode colonna
                else numbers.Add(matrix[b,a]);
                b = UpdateAandB(1, modeID % 4, a, b);
                countB--;
            }

            a = UpdateAandB(0, modeID % 4, a, b);
            b = mode.b;
            countA--;
        }
        
        
        int start = 0;
        while (remaining != 0)
        {
            chosen = Random.Range(start, 26 - remaining);
            selectedNumbers.Add(numbers[chosen]);
            start = chosen + 1;
            remaining--;
        }
    }

    private int UpdateAandB(int choice, int mode, int a, int b)
    {
        int x = a;
        int y = b;
        switch (mode)
        {
            case 0:
                x++;
                y++;
                break;
            case 1:
                x++;
                y--;
                break;
            case 2:
                x--;
                y++;
                break;
            case 3:
                x--;
                y--;
                break;
            default:
                x++;
                y++;
                break;
        }

        if (choice == 0) return x;
        return y;
    }
    
    private void Start()
    {
        SelectNumbers();
    }
}