using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CubeBaseを継承したクラス(未実装)
/// </summary>
public class Cube : CubeBase
{
    Color originColor;

    // Start is called before the first frame update
    void Start()
    {
        originColor = GetMeshGameObject().GetComponent<Renderer>().material.color;
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

    public override Color GetOriginColor()
    {
        return originColor;
    }

    /// <summary>
    /// オブジェクトを元の色に戻す
    /// </summary>
    public override void ResetOriginColor()
    {
        GetMeshGameObject().GetComponent<Renderer>().material.color = originColor;
    }
}
