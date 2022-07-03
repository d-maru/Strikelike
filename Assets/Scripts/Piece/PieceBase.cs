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
	public int Hp { get; set; }
    public int Attack { get; set; }
    public Status(int hp,int attack, string pieceName)
    { 
        Hp = hp;
        Attack = attack;
        this.pieceName = pieceName;
    }
}
public abstract class PieceBase : MonoBehaviour
{
    public Status Status { get; set; }
    public Pieceside Side;
    /// <summary>
    /// 現在地(どのcubeの上にいるか)
    /// </summary>
    public CubeBase OnCube { get; set; }

    /// <summary>
    /// 自分が行けるマスのリストを返す抽象関数
    /// 引数 : なし
    /// 返り値 : マスのリスト
    /// </summary>
    /// <returns>自分が行けるマスのリスト</returns>
    public abstract HashSet<CubeBase> getCanMoveCubeSet();

    

    /// <summary>
    /// コマの見た目に関する設定を行っているオブジェクトを取得
    /// </summary>
    /// <returns></returns>
    public GameObject GetMeshObject()
    {
        return transform.Find("polySurface1").gameObject;
    }
}