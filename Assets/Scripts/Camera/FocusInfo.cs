using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusInfo : MonoBehaviour
{
    GameObject currentFocusMeshObject;
    Color preFocusColor;

    // StopWatchを定義
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
            // フォーカスから外れたオブジェクトの色を元に戻す
            if (currentFocusMeshObject)
            {
                currentFocusMeshObject.GetComponent<Renderer>().material.color = preFocusColor;
            }

            // 新しいフォーカスオブジェクトの設定と色を覚えておく
            currentFocusMeshObject = justFocusMeshObject;
            preFocusColor = currentFocusMeshObject.GetComponent<Renderer>().material.color;

            sw.Reset();
            sw.Start(); // 計測開始

        }
      
        else if(justFocusMeshObject == null)
        {
            // フォーカスから外れたオブジェクトの色を元に戻す
            
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

    //フォーカス対象オブジェクトの明度アニメを行う
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

    //明度変更を行う
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
            // キューブはメッシュオブジェクト＝実際のゲームオブジェクト
            return currentFocusMeshObject;
        }

        else
        {
            // 犬コマはメッシュオブジェクトの親オブジェクトが実際のゲームオブジェクトなので
            return currentFocusMeshObject.transform.parent.gameObject;
        }
    }
}
