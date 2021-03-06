using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusInfo : MonoBehaviour
{
    IMeshObject currentFocusObject;
    Color preFocusColor;

    // StopWatchを定義
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
            //フォーカスできるオブジェクトはIMeshObjectを継承したコンポーネントを持っているはずなので
            IMeshObject focusObjectBase = hit.collider.GetComponent<IMeshObject>();
            if (focusObjectBase != null)
            {
                justFocusObject = focusObjectBase;
            }
        }

        if (justFocusObject != null && justFocusObject != currentFocusObject)
        {
            // フォーカスから外れたオブジェクトの色を元に戻す
            if (currentFocusObject != null)
            {
                currentFocusObject.GetMeshGameObject().GetComponent<Renderer>().material.color = preFocusColor;
            }

            // 新しいフォーカスオブジェクトの設定と色を覚えておく
            currentFocusObject = justFocusObject;
            preFocusColor = currentFocusObject.GetMeshGameObject().GetComponent<Renderer>().material.color;

            sw.Reset();
            sw.Start(); // 計測開始


            //フォーカスが変わったのでSEを再生
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
            // フォーカスから外れたオブジェクトの色を元に戻す

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

    //フォーカス対象オブジェクトの明度アニメを行う
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
