using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusInfo : MonoBehaviour
{
    MeshObjectManager currentFocusMeshObjectManager;
   

    // StopWatch���`
    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
    

    // Start is called before the first frame update
    void Start()
    {
        currentFocusMeshObjectManager = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MeshObjectManager justFocusMeshObjectManager = null;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
       
        //���̃t�H�[�J�X������
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // �t�H�[�J�X�ł���I�u�W�F�N�g��GameObjectBase���p�������R���|�[�l���g�������Ă���͂��Ȃ̂�
            GameObjectBase focusObjectBase = hit.collider.GetComponent<GameObjectBase>();
            if (focusObjectBase != null)
            {
                justFocusMeshObjectManager = focusObjectBase.GetMeshGameManager();
            }
        }

        // �t�H�[�J�X���Ⴄ�Ȃ�X�V
        // �t�H�[�J�X���R�}�������ꍇ�A�ړ��\�G���A�̃A�j�������ɖ߂�
        if (justFocusMeshObjectManager != null && justFocusMeshObjectManager != currentFocusMeshObjectManager)
        {
            // �t�H�[�J�X����O�ꂽ�I�u�W�F�N�g�̐F�����ɖ߂�
            if (currentFocusMeshObjectManager != null)
            {
                currentFocusMeshObjectManager.ResetOriginColor();
                ResetBrightNessEnableMoveAreas(currentFocusMeshObjectManager);
            }

            // �V�����t�H�[�J�X�I�u�W�F�N�g�̐ݒ�
            currentFocusMeshObjectManager = justFocusMeshObjectManager;
           

            sw.Reset();
            sw.Start(); // �v���J�n


            //�t�H�[�J�X���ς�����̂�SE���Đ�
            if (justFocusMeshObjectManager.GetGameObject().GetComponent<PieceBase>())
            {
                SoundManager.Instance.PlayFocusPieceSE();
            }
            if (justFocusMeshObjectManager.GetGameObject().GetComponent<Cube>())
            {
                SoundManager.Instance.PlayFocusCubeSE();
            }
        }

        // �t�H�[�J�X���Ȃ��Ȃ����ꍇ�A�I�u�W�F�N�g�̐F�����ɖ߂�
        // �t�H�[�J�X���R�}�������ꍇ�A�ړ��\�G���A�̃A�j�������ɖ߂�
        else if (justFocusMeshObjectManager == null)
        {
            if (currentFocusMeshObjectManager != null)
            {
                currentFocusMeshObjectManager.ResetOriginColor();
                ResetBrightNessEnableMoveAreas(currentFocusMeshObjectManager);
            }
            currentFocusMeshObjectManager = justFocusMeshObjectManager;
        }
        // �t�H�[�J�X�A�j���̍X�V
        // �t�H�[�J�X���R�}�������ꍇ�A�ړ��\�G���A�̃A�j�����s��
        else if (currentFocusMeshObjectManager != null)
        {
            ChangeBrightNess(currentFocusMeshObjectManager,sw);

            // �R�}���t�H�[�J�X���Ă���Ȃ�ړ��\�͈͂𖾓x�A�j��������
            ChangeBrightNessEnableMoveAreas(currentFocusMeshObjectManager);
        }
    }

    /// <summary>
    /// �����I�u�W�F�N�g�̖��x�A�j�����s��
    /// </summary>
    /// <param name="currentFocusObject"></param>
    /// <param name="sw"></param>
    public static void ChangeBrightNess(MeshObjectManager MeshObjectManager, System.Diagnostics.Stopwatch sw)
    {
       
        if (MeshObjectManager == null)
        {
            return;
        }

        Color _color = MeshObjectManager.GetMeshGameObject().GetComponent<Renderer>().material.color;

        float brightness = Mathf.Sin((float)sw.Elapsed.TotalSeconds) / 2 + 0.5f;
        _color = SetBrightNess(_color, brightness + 2f);

        MeshObjectManager.GetMeshGameObject().GetComponent<Renderer>().material.color = _color;
    }

    /// <summary>
    /// ���x�ύX���s��
    /// </summary>
    static Color SetBrightNess(Color baseColor, float brightness)
    {
        float hue = 0;
        float saturation = 0;
        float value = 0;
        Color.RGBToHSV(baseColor, out hue, out saturation, out value);
        Color outColor = Color.HSVToRGB(hue, saturation, brightness);
        return outColor;
    }

    /// <summary>
    /// �����I�u�W�F�N�g���R�}�̏ꍇ�A������͈͂̃L���[�u�𖾓x�A�j��������
    /// </summary>
    /// <param name="currentFocusMeshObjectManager"></param>
    void ChangeBrightNessEnableMoveAreas(MeshObjectManager currentFocusMeshObjectManager)
    {
        if (currentFocusMeshObjectManager.GetGameObject().GetComponent<Cube>())
        {
            return;
        }

        HashSet<CubeBase> enableMoveAreas = currentFocusMeshObjectManager.GetGameObject().GetComponent<PieceBase>().getCanMoveCubeSet();
        foreach (CubeBase enableMoveArea in enableMoveAreas)
        {
            ChangeBrightNess(enableMoveArea.GetMeshGameManager(), sw);
        }
    }

    /// <summary>
    /// �����I�u�W�F�N�g���R�}�̏ꍇ�A������͈͂̃L���[�u�̖��x�A�j�������Z�b�g����
    /// </summary>
    /// <param name="currentFocusMeshObjectManager"></param>
    void ResetBrightNessEnableMoveAreas(MeshObjectManager currentFocusMeshObjectManager)
    {
        if (currentFocusMeshObjectManager.GetGameObject().GetComponent<Cube>())
        {
            return;
        }

        HashSet<CubeBase> enableMoveAreas = currentFocusMeshObjectManager.GetGameObject().GetComponent<PieceBase>().getCanMoveCubeSet();
        foreach (CubeBase enableMoveArea in enableMoveAreas)
        {
            enableMoveArea.GetMeshGameManager().ResetOriginColor();
        }
    }

    /// <summary>
    /// ���}�E�X���t�H�[�J�X���Ă���I�u�W�F�N�g�̌����ڂɊւ���I�u�W�F�N�g���擾
    /// </summary>
    /// <returns></returns>
    public GameObject GetCurrentFocusMeshObject()
    {
        if(currentFocusMeshObjectManager == null)
        {
            return null;
        }
        return currentFocusMeshObjectManager.GetMeshGameObject();
    }

    /// <summary>
    /// ���}�E�X���t�H�[�J�X���Ă���I�u�W�F�N�g���擾
    /// </summary>
    /// <returns></returns>
    public GameObject GetCurrentFocusObject()
    {
        if (currentFocusMeshObjectManager == null)
        {
            return null;
        }
        return currentFocusMeshObjectManager.GetGameObject();
    }
}
