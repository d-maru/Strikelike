using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MeshObjectManager
{
    /// <summary>
    /// �e�I�u�W�F�N�g�̃��[�g�I�u�W�F�N�g
    /// ��ԏ�̃q�G�����L�[�ɂ���Q�[���I�u�W�F�N�g
    /// </summary>
    GameObject ingameObject;

    /// <summary>
    /// ingameObject�̉��ɂ��錩���ڂɊւ���I�u�W�F�N�g
    /// </summary>
    GameObject meshObject;

    /// <summary>
    /// �I�u�W�F�N�g�̌��̐F
    /// </summary>
    Color originColor;

    /// <summary>
    /// �����ڂɊւ���I�u�W�F�N�g�͈�x�����ݒ肵�A
    /// �ݒ肳�ꂽ���Ƃ��o���Ă����悤�ɂ���
    /// </summary>
    bool isSetMeshObject;

    public MeshObjectManager(GameObject ingameObject)
    {
        this.ingameObject = ingameObject;
        isSetMeshObject = false;

        // �Ƃ肠�������ɐݒ肵�Ă���
        originColor = Color.white;
    }


    /// <summary>
    /// �R�}��Ֆʂ̃I�u�W�F�N�g���猩���ڂɊւ���I�u�W�F�N�g���擾����
    /// </summary>
    /// <returns></returns>
    public GameObject GetMeshGameObject()
    {
        if (!isSetMeshObject)
        {
            Assert.IsTrue(false, "�����ڂɊւ���I�u�W�F�N�g�����ݒ�̂��߁A�I�u�W�F�N�g�������ɐݒ肷�邱��");
            return null;
        }
        return meshObject;
    }

    /// <summary>
    /// �R�}��Ֆʂ̃I�u�W�F�N�g���猩���ڂ�ݒ肷��
    /// ��x�ȍ~�ĂԂ��Ƃ͋��e���Ȃ�
    /// </summary>
    /// <returns></returns>
    public void SetMeshGameObject(GameObject meshObject)
    {
        if (isSetMeshObject)
        {
            Assert.IsTrue(false, "���Ɍ����ڂ̃I�u�W�F�N�g�͒�`����Ă��܂�");
            return;
        }
        this.meshObject = meshObject;
        originColor = meshObject.GetComponent<Renderer>().material.color;
        isSetMeshObject = true;
    }

    /// <summary>
    /// �R�}��Ֆʂ̖{�̃I�u�W�F�N�g���擾����
    /// </summary>
    /// <returns></returns>
    public GameObject GetGameObject()
    {
        return ingameObject;
    }

    /// <summary>
    /// �t�H�[�J�X�ɂ���Č����ڂ��ς��O�̂��Ƃ��Ƃ̃J���[���擾����
    /// �e�I�u�W�F�N�g��start�Ń��[�J���ϐ��ɕۑ����Ă����Ă�����Ăяo���`�ɂȂ�
    /// ���N���X����Ă�肽�����ǖʓ|�Ȃ̂�
    /// </summary>
    /// <returns></returns>
    public Color GetOriginColor()
    {

        if (!isSetMeshObject)
        {
            Assert.IsTrue(false, "�����ڂɊւ���I�u�W�F�N�g�����ݒ�̂��߁A�I�u�W�F�N�g�������ɐݒ肷�邱��");
            return Color.white;
        }

        return originColor;
    }

    /// <summary>
    /// �I�u�W�F�N�g�����̐F�ɖ߂�
    /// </summary>
    public void ResetOriginColor()
    {

        if (!isSetMeshObject)
        {
            Assert.IsTrue(false, "�����ڂɊւ���I�u�W�F�N�g�����ݒ�̂��߁A�I�u�W�F�N�g�������ɐݒ肷�邱��");
            return;
        }

        GetMeshGameObject().GetComponent<Renderer>().material.color = originColor;
    }
}
