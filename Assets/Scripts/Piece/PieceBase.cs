using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Status
{
    public int hp;
    public int attack;
   
    public Status(int _hp,int _attack)
    {
        hp = _hp;
        attack = _attack; 
    }
}
public class PieceBase : MonoBehaviour
{
    public Status status { get; set; }
}
