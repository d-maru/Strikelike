using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusInfo : MonoBehaviour
{
    IMeshObject currentFocusObject;
    Color preFocusColor;

    // StopWatch���`
    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
    

    // Start is called before the first frame update
    void Start()
    {
        currentFocusObject = null;
        preFocusColor = new Color32(0, 0, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        IMeshObject justFocusObject = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            //�t�H�[�J�X�ł���I�u�W�F�N�g��IMeshObject���p�������R���|�[�l���g�������Ă���͂��Ȃ̂�
            IMeshObject focusObjectBase = hit.collider.GetComponent<IMeshObject>();
            if (focusObjectBase != null)
            {
                justFocusObject = focusObjectBase;
            }
        }

        if (justFocusObject != null && justFocusObject != currentFocusObject)
        {
            // �t�H�[�J�X����O�ꂽ�I�u�W�F�N�g�̐F�����ɖ߂�
            if (currentFocusObject != null)
            {
                currentFocusObject.GetMeshGameObject().GetComponent<Renderer>().material.color = preFocusColor;
            }

            // �V�����t�H�[�J�X�I�u�W�F�N�g�̐ݒ�ƐF���o���Ă���
            currentFocusObject = justFocusObject;
            preFocusColor = currentFocusObject.GetMeshGameObject().GetComponent<Renderer>().material.color;

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

        else if (justFocusObject == null)
        {
            // �t�H�[�J�X����O�ꂽ�I�u�W�F�N�g�̐F�����ɖ߂�

            if (currentFocusObject != null)
            {

                currentFocusObject.GetMeshGameObject().GetComponent<Renderer>().material.color = preFocusColor;
            }


            currentFocusObject = justFocusObject;
        }

        else if (currentFocusObject != null)
        {
            ChangeBrightNess();
        }
    }

    //�t�H�[�J�X�ΏۃI�u�W�F�N�g�̖��x�A�j�����s��
    public void ChangeBrightNess()
    {
       
        if (currentFocusObject == null)
        {
            return;
        }

       
        Color _color = currentFocusObject.GetMeshGameObject().GetComponent<Renderer>().material.color;

        float brightness = Mathf.Sin((float)sw.Elapsed.TotalSeconds) / 2 + 0.5f;
        _color = SetBrightNess(_color, brightness + 2f);
       
        currentFocusObject.GetMeshGameObject().GetComponent<Renderer>().material.color = _color;
        //Debug.Log(brightness);
    }

    //���x�ύX���s��
    public static Color SetBrightNess(Color baseColor, float brightness)
    {
        float hue = 0;
        float saturation = 0;
        float value = 0;
        Color.RGBToHSV(baseColor, out hue, out saturation, out value);
        Color outColor = Color.HSVToRGB(hue, saturation, brightness);
        return outColor;
    }

    public GameObject GetCurrentFocusMeshObject()
    {
        if(currentFocusObject == null)
        {
            return null;
        }
        return currentFocusObject.GetMeshGameObject();
    }

    public GameObject GetCurrentFocusObject()
    {
        if (currentFocusObject == null)
        {
            return null;
        }
        return currentFocusObject.GetGameObject();
    }
}
