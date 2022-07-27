using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMeshObject
{
    /// <summary>
    /// コマや盤面のオブジェクトから見た目に関するオブジェクトを取得する
    /// </summary>
    /// <returns></returns>
    GameObject GetMeshGameObject();

    /// <summary>
    /// コマや盤面の本体オブジェクトを取得する
    /// </summary>
    /// <returns></returns>
    GameObject GetGameObject();

    /// <summary>
    /// フォーカスによって見た目が変わる前のもともとのカラーを取得する
    /// 各オブジェクトのstartでローカル変数に保存しておいてそれを呼び出す形になる
    /// 基底クラス作ってやりたいけど面倒なので
    /// </summary>
    /// <returns></returns>
    Color GetOriginColor();

    /// <summary>
    /// オブジェクトを元の色に戻す
    /// </summary>
    void ResetOriginColor();
}