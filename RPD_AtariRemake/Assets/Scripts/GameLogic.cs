using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public static int p1MinWidth = 13, p1MaxWidth = 19,  //5.11
                     p1EndPos = 3, p1StartPos = 22,   //1.20
                     p2MinWidth = 5, p2MaxWidth = 11,  //13.19
                     p2EndPos = 22, p2StartPos = 3; //20.0
    public static Transform[,] Grid = new Transform[25, 25];

    public Color[] P1Colors;
    public Color[] P2Colors;

    public enum Player
    {
        P1,
        P2
    };
}
