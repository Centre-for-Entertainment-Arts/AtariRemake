using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public static int p1MinWidth = 5, p1MaxWidth = 11,
                     p1MinHeight = 1, p1MaxHeight = 20,
                     p2MinWidth = 13, p2MaxWidth = 19,
                     p2MinHeight = 20, p2MaxHeight = 0;
    public static Transform[,] Grid = new Transform[24, 20];

    public Color[] P1Colors;
    public Color[] P2Colors;

    public enum Player
    {
        P1,
        P2
    };
}
