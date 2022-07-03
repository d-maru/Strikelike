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

    public Status(int hp,int attack, int moveDistance)
    {
        Hp = hp;
        Attack = attack;
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
    /// コンストラクタ
    /// </summary>
    /// <param name="status">ステータス構造体</param>
    /// <param name="side">PlayerかOpponentか</param>
    /// <param name="onCube">どのcubeの上にいるか</param>
    protected PieceBase(Status status, Pieceside side, CubeBase onCube)
    {
        Status = status;
        Side = side;
        OnCube = onCube;
    }


    /// <summary>
    /// 自分が行けるマスのリストを返す抽象関数
    /// 引数 : なし
    /// 返り値 : マスのリスト
    /// </summary>
    /// <returns>自分が行けるマスのリスト</returns>
    public abstract List<CubeBase> getCanMoveCubeList();

    

    /// <summary>
    /// コマの見た目に関する設定を行っているオブジェクトを取得
    /// </summary>
    /// <returns></returns>
    public GameObject GetMeshObject()
    {
        return transform.Find("polySurface1").gameObject;
    }
}