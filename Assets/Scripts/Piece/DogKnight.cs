using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogKnight : PieceBase
{
    /// <summary>
    /// 移動可能距離
    /// </summary>
    public static int DefaultMoveDistance { get; } = 1;

    // Start is called before the first frame update
    void Start()
    {
        // 見た目に関するオブジェクトが何であるかは一番はじめに設定しておく必要がある
        GetMeshGameManager().SetMeshGameObject(transform.Find("polySurface1").gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 移動距離分、全方角を再帰的に探索し行ける範囲のリストを生成するメソッド
    /// </summary>
    /// <param name="cubeSet">再帰的に格納していく行けるキューブのリスト</param>
    /// <param name="currentCube">現在対象として見てるキューブ</param>
    /// <param name="moveDistance">残りの移動距離</param>
    /// <returns>行けるマスのリストに今の対象を追加したもの</returns>
    public HashSet<CubeBase> RecursiveGetCubes(HashSet<CubeBase> cubeSet, CubeBase currentCube,int moveDistance)
    {

        // 移動距離がまだ残っているのなら移動距離を1歩減らして再帰的に探索
        if (--moveDistance >= 0)
        {
            // 全方角を走査。対象のマスに移動可能であれば再帰的に探索
            foreach (Direction direction in Direction.GetValues(typeof(Direction)))
                if (currentCube.CanMove(direction))
                {
                    // 対象をリスト追加
                    cubeSet.Add(currentCube.AdjacentCubes[direction]);

                    cubeSet = RecursiveGetCubes(cubeSet, currentCube.AdjacentCubes[direction], moveDistance);
                }
        }
        return cubeSet;
    }

    /// <summary>
    /// 自分が行けるマスの集合を返す。
    /// </summary>
    /// <returns>自分が行けるマスの集合。現在立っている場所も含む。</returns>
    public override HashSet<CubeBase> getCanMoveCubeSet()
    {
        var cubeSet = new HashSet<CubeBase>();

        return RecursiveGetCubes(cubeSet, OnCube, DefaultMoveDistance);
    }
}
