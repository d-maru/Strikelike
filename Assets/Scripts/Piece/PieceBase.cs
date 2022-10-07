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
    public int currentHp;
    /// <summary>
    /// 現在地(どのcubeの上にいるか)
    /// </summary>
    public CubeBase OnCube { get; set; }
    [field: SerializeField] 
    public GameObject StatusCanvas { get; set; }

    /// <summary>
    /// コマをcubeの上に移動する
    /// </summary>
    /// <param name="cube">移動先のマス</param>
    public void MoveTo(CubeBase cube)
    {
        OnCube.Piece = null;
        cube.Piece = this;
        OnCube = cube;
        transform.position = new Vector3(cube.transform.position.x, 0, cube.transform.position.z);
    }

    /// <summary>
    /// 自分が行けるマスのリストを返す抽象関数
    /// </summary>
    /// <returns>自分が行けるマスの集合</returns>
    public abstract HashSet<CubeBase> getCanMoveCubeSet();

    public void AttackTo(PieceBase targetPiece)
    {
        var targetPieceSide = targetPiece.Side;
        if (targetPieceSide == Pieceside.Opponent)
        {
            int opponentHp = targetPiece.currentHp;
            int attack = Status.Attack;
            Debug.Log(currentHp);
            opponentHp -= attack;
            targetPiece.currentHp = opponentHp;
            Debug.Log(targetPiece.currentHp);
        }
    }
    public abstract HashSet<CubeBase> getCanAttackCubeSet();
}