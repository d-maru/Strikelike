using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogKnight : PieceBase
{
    /// <summary>
    /// 継承したコンストラクタ
    /// </summary>
    /// <param name="status">ステータス構造体</param>
    /// <param name="player">PlayerかOpponentか</param>
    /// <param name="onCube">どのcubeの上にいるか</param>
    public DogKnight(Status status, Pieceside player, CubeBase onCube) : base(status, player, onCube)
    {
    }

    /// <summary>
    /// 移動可能距離
    /// </summary>
    public static int MoveDistance { get; } = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 移動距離分、全方角を再帰的に探索し行ける範囲のリストを生成するメソッド
    /// </summary>
    /// <param name="cubeList">再帰的に格納していく行けるキューブのリスト</param>
    /// <param name="currentCube">現在対象として見てるキューブ</param>
    /// <param name="moveDistance">残りの移動距離</param>
    /// <returns>行けるマスのリストに今の対象を追加したもの</returns>
    public List<CubeBase> RecursiveGetCubes(List<CubeBase> cubeList, CubeBase currentCube,int moveDistance)
    {
        // リスト未登録であれば現在の対象をリスト追加
        if ( ! cubeList.Contains(currentCube))
            cubeList.Add(currentCube);

        // 移動距離がまだ残っているのなら移動距離を1歩減らして再帰的に探索
        if (moveDistance-- > 0)
        {
            // 全方角を走査。対象のマスに移動可能であれば再帰的に探索
            foreach (Direction direction in Direction.GetValues(typeof(Direction)))
                if (currentCube.CanMove(direction))
                {
                    cubeList = RecursiveGetCubes(cubeList, currentCube.AdjacentCubes[direction], moveDistance);
                }
        }
        return cubeList;
    }

    /// <summary>
    /// 自分が行けるマスのリストを返す関数の実現
    /// リストを用意し、移動距離分 全方角 再帰的に探索する関数を呼び出す
    /// </summary>
    /// <returns>自分が行けるマスのリスト</returns>
    public override List<CubeBase> getCanMoveCubeList()
    {
        List<CubeBase> cubeList = new List<CubeBase>();

        return RecursiveGetCubes(cubeList, OnCube, MoveDistance);
    }
}
