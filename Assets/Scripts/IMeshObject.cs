using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMeshObject
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

    /// <summary>
    /// �t�H�[�J�X�ɂ���Č����ڂ��ς��O�̂��Ƃ��Ƃ̃J���[���擾����
    /// �e�I�u�W�F�N�g��start�Ń��[�J���ϐ��ɕۑ����Ă����Ă�����Ăяo���`�ɂȂ�
    /// ���N���X����Ă�肽�����ǖʓ|�Ȃ̂�
    /// </summary>
    /// <returns></returns>
    Color GetOriginColor();

    /// <summary>
    /// �I�u�W�F�N�g�����̐F�ɖ߂�
    /// </summary>
    void ResetOriginColor();
}