using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusInfo : MonoBehaviour
{
    IMeshObject currentFocusObject;
   

    // StopWatch���`
    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
    

    // Start is called before the first frame update
    void Start()
    {
        currentFocusObject = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        IMeshObject justFocusObject = null;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
       
        //���̃t�H�[�J�X������
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // �t�H�[�J�X�ł���I�u�W�F�N�g��IMeshObject���p�������R���|�[�l���g�������Ă���͂��Ȃ̂�
            IMeshObject focusObjectBase = hit.collider.GetComponent<IMeshObject>();
            if (focusObjectBase != null)
            {
                justFocusObject = focusObjectBase;
            }
        }

        // �t�H�[�J�X���Ⴄ�Ȃ�X�V
        // �t�H�[�J�X���R�}�������ꍇ�A�ړ��\�G���A�̃A�j�������ɖ߂�
        if (justFocusObject != null && justFocusObject != currentFocusObject)
        {
            // �t�H�[�J�X����O�ꂽ�I�u�W�F�N�g�̐F�����ɖ߂�
            if (currentFocusObject != null)
            {
                currentFocusObject.ResetOriginColor();
                ResetBrightNessEnableMoveAreas(currentFocusObject);
            }

            // �V�����t�H�[�J�X�I�u�W�F�N�g�̐ݒ�
            currentFocusObject = justFocusObject;
           

            sw.Reset();
            sw.Start(); // �v���J�n


            //�t�H�[�J�X���ς�����̂�SE���Đ�
            if (justFocusObject.GetGameObject().GetComponent<PieceBase>())
            {
                SoundManager.Instance.PlayFocusPieceSE();
            }
            if (justFocusObject.GetGameObject().GetComponent<Cube>())
            {
                SoundManager.Instance.PlayFocusCubeSE();
            }
        }

        // �t�H�[�J�X���Ȃ��Ȃ����ꍇ�A�I�u�W�F�N�g�̐F�����ɖ߂�
        // �t�H�[�J�X���R�}�������ꍇ�A�ړ��\�G���A�̃A�j�������ɖ߂�
        else if (justFocusObject == null)
        {
            if (currentFocusObject != null)
            {
                currentFocusObject.ResetOriginColor();
                ResetBrightNessEnableMoveAreas(currentFocusObject);
            }
            currentFocusObject = justFocusObject;
        }
        // �t�H�[�J�X�A�j���̍X�V
        // �t�H�[�J�X���R�}�������ꍇ�A�ړ��\�G���A�̃A�j�����s��
        else if (currentFocusObject != null)
        {
            ChangeBrightNess(currentFocusObject,sw);

            // �R�}���t�H�[�J�X���Ă���Ȃ�ړ��\�͈͂𖾓x�A�j��������
            ChangeBrightNessEnableMoveAreas(currentFocusObject);
        }
    }

    /// <summary>
    /// �����I�u�W�F�N�g�̖��x�A�j�����s��
    /// </summary>
    /// <param name="currentFocusObject"></param>
    /// <param name="sw"></param>
    public static void ChangeBrightNess(IMeshObject meshObject, System.Diagnostics.Stopwatch sw)
    {
       
        if (meshObject == null)
        {
            return;
        }

        Color _color = meshObject.GetMeshGameObject().GetComponent<Renderer>().material.color;

        float brightness = Mathf.Sin((float)sw.Elapsed.TotalSeconds) / 2 + 0.5f;
        _color = SetBrightNess(_color, brightness + 2f);

        meshObject.GetMeshGameObject().GetComponent<Renderer>().material.color = _color;
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
    /// <param name="currentFocusObject"></param>
    void ChangeBrightNessEnableMoveAreas(IMeshObject currentFocusObject)
    {
        if (currentFocusObject.GetGameObject().GetComponent<Cube>())
        {
            return;
        }

        HashSet<CubeBase> enableMoveAreas = currentFocusObject.GetGameObject().GetComponent<PieceBase>().getCanMoveCubeSet();
        foreach (CubeBase enableMoveArea in enableMoveAreas)
        {
            ChangeBrightNess(enableMoveArea.GetComponent<IMeshObject>(), sw);
        }
    }

    /// <summary>
    /// �����I�u�W�F�N�g���R�}�̏ꍇ�A������͈͂̃L���[�u�̖��x�A�j�������Z�b�g����
    /// </summary>
    /// <param name="currentFocusObject"></param>
    void ResetBrightNessEnableMoveAreas(IMeshObject currentFocusObject)
    {
        if (currentFocusObject.GetGameObject().GetComponent<Cube>())
        {
            return;
        }

        HashSet<CubeBase> enableMoveAreas = currentFocusObject.GetGameObject().GetComponent<PieceBase>().getCanMoveCubeSet();
        foreach (CubeBase enableMoveArea in enableMoveAreas)
        {
            enableMoveArea.GetComponent<IMeshObject>().ResetOriginColor();
        }
    }

    /// <summary>
    /// ���}�E�X���t�H�[�J�X���Ă���I�u�W�F�N�g�̌����ڂɊւ���I�u�W�F�N�g���擾
    /// </summary>
    /// <returns></returns>
    public GameObject GetCurrentFocusMeshObject()
    {
        if(currentFocusObject == null)
        {
            return null;
        }
        return currentFocusObject.GetMeshGameObject();
    }

    /// <summary>
    /// ���}�E�X���t�H�[�J�X���Ă���I�u�W�F�N�g���擾
    /// </summary>
    /// <returns></returns>
    public GameObject GetCurrentFocusObject()
    {
        if (currentFocusObject == null)
        {
            return null;
        }
        return currentFocusObject.GetGameObject();
    }
}
