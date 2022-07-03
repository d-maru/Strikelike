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
    public int hp { get; set; }
    public int attack { get; set; }
    public string pieceName { get; set; }
   
    public Status(int hp,int attack, string pieceName)
    {
        this.hp = hp;
        this.attack = attack; 
        this.pieceName = pieceName;
    }
}
public class PieceBase : MonoBehaviour
{
    public Status status { get; set; }
    public Pieceside Side;

    /// <summary>
    /// �R�}�̌����ڂɊւ���ݒ���s���Ă���I�u�W�F�N�g���擾
    /// </summary>
    /// <returns></returns>
    public GameObject GetMeshObject()
    {
        return transform.Find("polySurface1").gameObject;
    }
}