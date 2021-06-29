using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public static int width = 20, height = 23;
    public static Transform[,] grid = new Transform[width, height];

    public Color[] P1Colors;
    public Color[] P2Colors;

    public enum Player
    {
        P1,
        P2
    };
}
