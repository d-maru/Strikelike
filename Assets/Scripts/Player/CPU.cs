using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPU : MonoBehaviour, IPlayer
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// �R�}�̌����ڂɊւ���ݒ���s���Ă���I�u�W�F�N�g���擾
    /// </summary>
    /// <returns></returns>
    public GameObject GetMeshObject()
    {
        return transform.Find("polySurface1").gameObject;
    }

    public void Play()
    {

    }
}
