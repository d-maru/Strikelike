using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceContoroller : MonoBehaviour
{
    // 移動可能な距離を設定

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    // Transform.positionで移動する
    // 将来的にはTranslateで移動する

    /// <summary>
    /// コマの見た目に関する設定を行っているオブジェクトを取得
    /// </summary>
    /// <returns></returns>
    public GameObject GetMeshObject()
    {
        return transform.Find("polySurface1").gameObject;
    }
}
