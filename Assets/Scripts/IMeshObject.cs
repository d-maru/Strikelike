using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IMeshObject
{
    /// <summary>
    /// �R�}��Ֆʂ̃I�u�W�F�N�g���猩���ڂɊւ���I�u�W�F�N�g���擾����
    /// </summary>
    /// <returns></returns>
    GameObject GetMeshGameObject();

    /// <summary>
    /// �R�}��Ֆʂ̖{�̃I�u�W�F�N�g���擾����
    /// </summary>
    /// <returns></returns>
    GameObject GetGameObject();
}