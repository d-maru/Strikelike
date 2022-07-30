using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MeshObjectManager
{
    /// <summary>
    /// 各オブジェクトのルートオブジェクト
    /// 一番上のヒエラルキーにあるゲームオブジェクト
    /// </summary>
    GameObject ingameObject;

    /// <summary>
    /// ingameObjectの下にある見た目に関するオブジェクト
    /// </summary>
    GameObject meshObject;

    /// <summary>
    /// オブジェクトの元の色
    /// </summary>
    Color originColor;

    /// <summary>
    /// 見た目に関するオブジェクトは一度だけ設定し、
    /// 設定されたことを覚えておくようにする
    /// </summary>
    bool isSetMeshObject;

    public MeshObjectManager(GameObject ingameObject)
    {
        this.ingameObject = ingameObject;
        isSetMeshObject = false;

        // とりあえず白に設定しておく
        originColor = Color.white;
    }


    /// <summary>
    /// コマや盤面のオブジェクトから見た目に関するオブジェクトを取得する
    /// </summary>
    /// <returns></returns>
    public GameObject GetMeshGameObject()
    {
        if (!isSetMeshObject)
        {
            Assert.IsTrue(false, "見た目に関するオブジェクトが未設定のため、オブジェクト生成時に設定すること");
            return null;
        }
        return meshObject;
    }

    /// <summary>
    /// コマや盤面のオブジェクトから見た目を設定する
    /// 二度以降呼ぶことは許容しない
    /// </summary>
    /// <returns></returns>
    public void SetMeshGameObject(GameObject meshObject)
    {
        if (isSetMeshObject)
        {
            Assert.IsTrue(false, "既に見た目のオブジェクトは定義されています");
            return;
        }
        this.meshObject = meshObject;
        originColor = meshObject.GetComponent<Renderer>().material.color;
        isSetMeshObject = true;
    }

    /// <summary>
    /// コマや盤面の本体オブジェクトを取得する
    /// </summary>
    /// <returns></returns>
    public GameObject GetGameObject()
    {
        return ingameObject;
    }

    /// <summary>
    /// フォーカスによって見た目が変わる前のもともとのカラーを取得する
    /// 各オブジェクトのstartでローカル変数に保存しておいてそれを呼び出す形になる
    /// 基底クラス作ってやりたいけど面倒なので
    /// </summary>
    /// <returns></returns>
    public Color GetOriginColor()
    {

        if (!isSetMeshObject)
        {
            Assert.IsTrue(false, "見た目に関するオブジェクトが未設定のため、オブジェクト生成時に設定すること");
            return Color.white;
        }

        return originColor;
    }

    /// <summary>
    /// オブジェクトを元の色に戻す
    /// </summary>
    public void ResetOriginColor()
    {

        if (!isSetMeshObject)
        {
            Assert.IsTrue(false, "見た目に関するオブジェクトが未設定のため、オブジェクト生成時に設定すること");
            return;
        }

        GetMeshGameObject().GetComponent<Renderer>().material.color = originColor;
    }
}
