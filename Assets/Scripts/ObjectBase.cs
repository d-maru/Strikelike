using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IObjectBase
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
}