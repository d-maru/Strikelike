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
    public string PieceName { get; set; }
    public Status(int hp, int attack, string pieceName)
    { 
        Hp = hp;
        Attack = attack;
        PieceName = pieceName;
    }
}
public abstract class PieceBase : GameObjectBase
{
    public Status Status { get; set; }
    public Pieceside Side;
    /// <summary>
    /// 現在地(どのcubeの上にいるか)
    /// </summary>
    public CubeBase OnCube { get; set; }

    public void MoveTo(CubeBase cube)
    {
        OnCube.Piece = null;
        cube.Piece = this;
        OnCube = cube;
        transform.position = new Vector3(cube.transform.position.x, 0, cube.transform.position.z);
    }
    /// <summary>
    /// 自分が行けるマスのリストを返す抽象関数
    /// 引数 : なし
    /// 返り値 : マスのリスト
    /// </summary>
    /// <returns>自分が行けるマスのリスト</returns>
    public abstract HashSet<CubeBase> getCanMoveCubeSet();
}