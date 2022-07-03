using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Pieceside
{
    Player,
    Opponent
}

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
    public Pieceside Side;

    /// <summary>
    /// コマの見た目に関する設定を行っているオブジェクトを取得
    /// </summary>
    /// <returns></returns>
    public GameObject GetMeshObject()
    {
        return transform.Find("polySurface1").gameObject;
    }
}