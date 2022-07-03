using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusInfo : MonoBehaviour
{
    GameObject currentFocusMeshObject;
    Color preFocusColor;

    // StopWatch���`
    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
    

    // Start is called before the first frame update
    void Start()
    {
        currentFocusMeshObject = null;
        preFocusColor = new Color32(0, 0, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameObject justFocusMeshObject =null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Piece"))
            {

                if (hit.collider.gameObject.GetComponent<PieceBase>())
                {
                    PieceBase pieceContoroller = hit.collider.gameObject.GetComponent<PieceBase>();
                    justFocusMeshObject = pieceContoroller.GetMeshObject();
                }
            }
            else if (hit.collider.CompareTag("Cube"))
            {

                justFocusMeshObject = hit.collider.gameObject;
            }
        }

        if(justFocusMeshObject !=null && justFocusMeshObject != currentFocusMeshObject)
        {
            // �t�H�[�J�X����O�ꂽ�I�u�W�F�N�g�̐F�����ɖ߂�
            if (currentFocusMeshObject)
            {
                currentFocusMeshObject.GetComponent<Renderer>().material.color = preFocusColor;
            }

            // �V�����t�H�[�J�X�I�u�W�F�N�g�̐ݒ�ƐF���o���Ă���
            currentFocusMeshObject = justFocusMeshObject;
            preFocusColor = currentFocusMeshObject.GetComponent<Renderer>().material.color;

            sw.Reset();
            sw.Start(); // �v���J�n

        }
      
        else if(justFocusMeshObject == null)
        {
            // �t�H�[�J�X����O�ꂽ�I�u�W�F�N�g�̐F�����ɖ߂�
            
            if (currentFocusMeshObject)
            {
                
                currentFocusMeshObject.GetComponent<Renderer>().material.color = preFocusColor;
            }

            
            currentFocusMeshObject = justFocusMeshObject;
        }
        
        else if(currentFocusMeshObject !=null && currentFocusMeshObject)
        {
            ChangeBrightNess();
        }
    }

    //�t�H�[�J�X�ΏۃI�u�W�F�N�g�̖��x�A�j�����s��
    public void ChangeBrightNess()
    {
       
        if (currentFocusMeshObject == null)
        {
            return;
        }

       
        Color _color = currentFocusMeshObject.GetComponent<Renderer>().material.color;

        float brightness = Mathf.Sin((float)sw.Elapsed.TotalSeconds) / 2 + 0.5f;
        _color = SetBrightNess(_color, brightness + 2f);
       
        currentFocusMeshObject.GetComponent<Renderer>().material.color = _color;
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
        return currentFocusMeshObject;
    }

    public GameObject GetCurrentFocusObject()
    {
        if (currentFocusMeshObject == null)
        {
            return null;
        }

        if (currentFocusMeshObject.CompareTag("Cube"))
        {
            // �L���[�u�̓��b�V���I�u�W�F�N�g�����ۂ̃Q�[���I�u�W�F�N�g
            return currentFocusMeshObject;
        }

        else
        {
            // ���R�}�̓��b�V���I�u�W�F�N�g�̐e�I�u�W�F�N�g�����ۂ̃Q�[���I�u�W�F�N�g�Ȃ̂�
            return currentFocusMeshObject.transform.parent.gameObject;
        }
    }
}
