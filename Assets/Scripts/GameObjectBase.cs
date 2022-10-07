using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// オブジェクトの型
/// </summary>
public enum ObjectTypeDefine
{
    Piece,
    Cube,
    ObjectType_Num,
    ObjectType_Invalid
}

/// <summary>
/// すべてのオブジェクトの基本となるクラス
/// </summary>
public class GameObjectBase : MonoBehaviour
{
    MeshObjectManager MeshObjectManager;

    protected virtual void Awake()
    {
        this.MeshObjectManager = new MeshObjectManager(transform.gameObject);

        // セットされているコンポーネントの種類からオブジェクトタイプを設定
        if (GetGameObject().GetComponent<PieceBase>())
        {
            ObjectType = ObjectTypeDefine.Piece;
        }
        else if (GetGameObject().GetComponent<Cube>())
        {
            ObjectType = ObjectTypeDefine.Cube;
        }
        else
        {
            ObjectType = ObjectTypeDefine.ObjectType_Invalid;
            Assert.IsTrue(false, "無効なデータに設定されています");
        }
       
    }

    /// <summary>
    /// このオブジェクトのタイプのアクセッサ
    /// </summary>
    public ObjectTypeDefine ObjectType { get; set; }

    public bool IsPiece()
    {
        return ObjectType == ObjectTypeDefine.Piece;
    }

    public bool IsCube()
    {
        return ObjectType == ObjectTypeDefine.Cube;
    }


    /// <summary>
    /// コマや盤面のオブジェクトから見た目に関するオブジェクトを取得する
    /// </summary>
    /// <returns></returns>
    public MeshObjectManager GetMeshGameManager()
    {
        return this.MeshObjectManager;
    }

    /// <summary>
    /// コマや盤面の本体オブジェクトを取得する
    /// </summary>
    /// <returns></returns>
    public virtual GameObject GetGameObject()
    {
        return transform.gameObject;
    }
}
