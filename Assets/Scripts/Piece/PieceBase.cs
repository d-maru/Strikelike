using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Pieceside
{
    Player,
    Opponent
}

public struct Status
{
    public int Hp { get; set; }
    public int Attack { get; set; }

    public Status(int hp,int attack, int moveDistance)
    {
        Hp = hp;
        Attack = attack;
    }
}
public abstract class PieceBase : MonoBehaviour
{
    public Status Status { get; set; }
    public Pieceside Side;
    /// <summary>
    /// ���ݒn(�ǂ�cube�̏�ɂ��邩)
    /// </summary>
    public CubeBase OnCube { get; set; }

    /// <summary>
    /// �������s����}�X�̃��X�g��Ԃ����ۊ֐�
    /// ���� : �Ȃ�
    /// �Ԃ�l : �}�X�̃��X�g
    /// </summary>
    /// <returns>�������s����}�X�̃��X�g</returns>
    public abstract HashSet<CubeBase> getCanMoveCubeSet();

    

    /// <summary>
    /// �R�}�̌����ڂɊւ���ݒ���s���Ă���I�u�W�F�N�g���擾
    /// </summary>
    /// <returns></returns>
    public GameObject GetMeshObject()
    {
        return transform.Find("polySurface1").gameObject;
    }
}