using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CubeBaseを継承したクラス(未実装)
/// </summary>
public class Cube : CubeBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 盤面の見た目に関するオブジェクトを取得
    /// </summary>
    /// <returns></returns>
    public override GameObject GetMeshGameObject()
    {
        return transform.gameObject;
    }

    /// <summary>
    /// キューブを取得する関数
    /// </summary>
    /// <returns></returns>
    public override GameObject GetGameObject()
    {
        return transform.gameObject;
    }
}
