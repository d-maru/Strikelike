using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusInfo : MonoBehaviour
{
    IMeshObject currentFocusObject;
   

    // StopWatchを定義
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
       
        //今のフォーカスを検索
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // フォーカスできるオブジェクトはIMeshObjectを継承したコンポーネントを持っているはずなので
            IMeshObject focusObjectBase = hit.collider.GetComponent<IMeshObject>();
            if (focusObjectBase != null)
            {
                justFocusObject = focusObjectBase;
            }
        }

        // フォーカスが違うなら更新
        // フォーカスがコマだった場合、移動可能エリアのアニメも元に戻す
        if (justFocusObject != null && justFocusObject != currentFocusObject)
        {
            // フォーカスから外れたオブジェクトの色を元に戻す
            if (currentFocusObject != null)
            {
                currentFocusObject.ResetOriginColor();
                ResetBrightNessEnableMoveAreas(currentFocusObject);
            }

            // 新しいフォーカスオブジェクトの設定
            currentFocusObject = justFocusObject;
           

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

        // フォーカスがなくなった場合、オブジェクトの色を元に戻す
        // フォーカスがコマだった場合、移動可能エリアのアニメも元に戻す
        else if (justFocusObject == null)
        {
            if (currentFocusObject != null)
            {
                currentFocusObject.ResetOriginColor();
                ResetBrightNessEnableMoveAreas(currentFocusObject);
            }
            currentFocusObject = justFocusObject;
        }
        // フォーカスアニメの更新
        // フォーカスがコマだった場合、移動可能エリアのアニメも行う
        else if (currentFocusObject != null)
        {
            ChangeBrightNess(currentFocusObject,sw);

            // コマをフォーカスしているなら移動可能範囲を明度アニメさせる
            ChangeBrightNessEnableMoveAreas(currentFocusObject);
        }
    }

    /// <summary>
    /// 引数オブジェクトの明度アニメを行う
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
    /// 明度変更を行う
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
    /// 引数オブジェクトがコマの場合、動ける範囲のキューブを明度アニメさせる
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
    /// 引数オブジェクトがコマの場合、動ける範囲のキューブの明度アニメをリセットする
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
    /// 今マウスがフォーカスしているオブジェクトの見た目に関するオブジェクトを取得
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
    /// 今マウスがフォーカスしているオブジェクトを取得
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
