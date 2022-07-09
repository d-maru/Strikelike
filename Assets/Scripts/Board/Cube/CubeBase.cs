using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 方角
/// </summary>
public enum Direction
{
    North,
    East,
    South,
    West
}

/// <summary>
/// cubeの抽象クラス
/// 各マスで継承してね！
/// </summary>
public abstract class CubeBase : MonoBehaviour, IObjectBase
{
    /// <summary>
    /// 現在地(2次元的に何行何列目か)
    /// </summary>
    public int[] Place { get; set; }

    /// <summary>
    /// 上に乗っているコマオブジェクト
    /// </summary>
    public PieceBase Piece { get; set; }

    /// <summary>
    /// 隣接マス
    /// </summary>
    public Dictionary<Direction, CubeBase> AdjacentCubes { get; set; }

    /// <summary>
    /// 自分から対象方向に進めるか
    /// </summary>
    /// <param name="direction">移動方向</param>
    /// <returns>進めるならtrue/進めないならfalse</returns>
    public bool CanMove(Direction direction)
    {
        // 隣接キューブが存在 かつ その隣接キューブ上にコマが無いなら移動可能
        if(AdjacentCubes[direction] is not null)
            if (AdjacentCubes[direction].Piece is null)
                return true;
        return false;
    }

    /// <summary>
    /// キューブの見た目に関する設定を行っているオブジェクトを取得する抽象関数
    /// </summary>
    /// <returns></returns>
    public abstract GameObject GetMeshGameObject();

    /// <summary>
    /// キューブを取得する抽象関数
    /// </summary>
    /// <returns></returns>
    public abstract GameObject GetGameObject();
}
