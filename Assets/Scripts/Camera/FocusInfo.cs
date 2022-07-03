using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusInfo : MonoBehaviour
{
    GameObject currentFocusObject;
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

        GameObject justFocusObject =null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Piece"))
            {

                if (hit.collider.gameObject.GetComponent<PieceBase>())
                {
                    PieceBase pieceContoroller = hit.collider.gameObject.GetComponent<PieceBase>();
                    justFocusObject = pieceContoroller.GetMeshObject();
                }
            }
            else if (hit.collider.CompareTag("Cube"))
            {
                justFocusObject = hit.collider.gameObject;
            }
        }

        if(justFocusObject !=null && justFocusObject != currentFocusObject)
        {
            // �t�H�[�J�X����O�ꂽ�I�u�W�F�N�g�̐F�����ɖ߂�
            if (currentFocusObject)
            {
                currentFocusObject.GetComponent<Renderer>().material.color = preFocusColor;
            }

            // �V�����t�H�[�J�X�I�u�W�F�N�g�̐ݒ�ƐF���o���Ă���
            currentFocusObject = justFocusObject;
            preFocusColor = currentFocusObject.GetComponent<Renderer>().material.color;

            sw.Reset();
            sw.Start(); // �v���J�n

        }
        else if(justFocusObject == null)
        {
            // �t�H�[�J�X����O�ꂽ�I�u�W�F�N�g�̐F�����ɖ߂�
            if (currentFocusObject)
            {
                currentFocusObject.GetComponent<Renderer>().material.color = preFocusColor;
            }

            currentFocusObject = justFocusObject;
        }
        else if(currentFocusObject !=null && currentFocusObject)
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

        Color _color = currentFocusObject.GetComponent<Renderer>().material.color;

        float brightness = Mathf.Sin((float)sw.Elapsed.TotalSeconds) / 2 + 0.5f;
        _color = SetBrightNess(_color, brightness + 2f);
        currentFocusObject.GetComponent<Renderer>().material.color = _color;
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
}
