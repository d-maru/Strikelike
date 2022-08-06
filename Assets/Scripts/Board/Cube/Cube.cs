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
        // 見た目に関するオブジェクトが何であるかは一番はじめに設定しておく必要がある
        GetMeshGameManager().SetMeshGameObject(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
