using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// �I�u�W�F�N�g�̌^
/// </summary>
public enum ObjectTypeDefine
{
    Piece,
    Cube,
    ObjectType_Num,
    ObjectType_Invalid
}

/// <summary>
/// ���ׂẴI�u�W�F�N�g�̊�{�ƂȂ�N���X
/// </summary>
public class GameObjectBase : MonoBehaviour
{
    MeshObjectManager MeshObjectManager;

    protected virtual void Awake()
    {
        this.MeshObjectManager = new MeshObjectManager(transform.gameObject);

        // �Z�b�g����Ă���R���|�[�l���g�̎�ނ���I�u�W�F�N�g�^�C�v��ݒ�
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
            Assert.IsTrue(false, "�����ȃf�[�^�ɐݒ肳��Ă��܂�");
        }
       
    }

    /// <summary>
    /// ���̃I�u�W�F�N�g�̃^�C�v�̃A�N�Z�b�T
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
    /// �R�}��Ֆʂ̃I�u�W�F�N�g���猩���ڂɊւ���I�u�W�F�N�g���擾����
    /// </summary>
    /// <returns></returns>
    public MeshObjectManager GetMeshGameManager()
    {
        return this.MeshObjectManager;
    }

    /// <summary>
    /// �R�}��Ֆʂ̖{�̃I�u�W�F�N�g���擾����
    /// </summary>
    /// <returns></returns>
    public virtual GameObject GetGameObject()
    {
        return transform.gameObject;
    }
}
