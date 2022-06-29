using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Status
{
    public int hp { get; set; }
    public int attack { get; set; }
   
    public Status(int hp,int attack)
    {
        this.hp = hp;
        this.attack = attack; 
    }
}
public class PieceBase : MonoBehaviour
{
    public Status status { get; set; }
}

public enum Pieceside
{
    player,
    opponent 
}