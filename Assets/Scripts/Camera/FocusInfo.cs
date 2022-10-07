using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusInfo : MonoBehaviour
{
    MeshObjectManager currentFocusMeshObjectManager;

    // StopWatchを定義
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
       
        //今のフォーカスを検索
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // フォーカスできるオブジェクトはGameObjectBaseを継承したコンポーネントを持っているはずなので
            GameObjectBase focusObjectBase = hit.collider.GetComponent<GameObjectBase>();
            if (focusObjectBase != null)
            {
                justFocusMeshObjectManager = focusObjectBase.GetMeshGameManager();
            }
        }

        // フォーカスが違うなら更新
        // フォーカスがコマだった場合、移動可能エリアのアニメも元に戻す
        if (justFocusMeshObjectManager != null && justFocusMeshObjectManager != currentFocusMeshObjectManager)
        {
            // フォーカスから外れたオブジェクトの色を元に戻す
            if (currentFocusMeshObjectManager != null)
            {
                currentFocusMeshObjectManager.ResetOriginColor();
                ResetBrightNessEnableMoveAreas(currentFocusMeshObjectManager);
            }

            // 新しいフォーカスオブジェクトの設定
            currentFocusMeshObjectManager = justFocusMeshObjectManager;
           

            sw.Reset();
            sw.Start(); // 計測開始


            //フォーカスが変わったのでSEを再生
            if (justFocusMeshObjectManager.GetGameObject().GetComponent<PieceBase>())
            {
                SoundManager.Instance.PlayFocusPieceSE();
            }
            if (justFocusMeshObjectManager.GetGameObject().GetComponent<Cube>())
            {
                SoundManager.Instance.PlayFocusCubeSE();
            }
        }

        // フォーカスがなくなった場合、オブジェクトの色を元に戻す
        // フォーカスがコマだった場合、移動可能エリアのアニメも元に戻す
        else if (justFocusMeshObjectManager == null)
        {
            if (currentFocusMeshObjectManager != null)
            {
                currentFocusMeshObjectManager.ResetOriginColor();
                ResetBrightNessEnableMoveAreas(currentFocusMeshObjectManager);
            }
            currentFocusMeshObjectManager = justFocusMeshObjectManager;
        }
        // フォーカスアニメの更新
        // フォーカスがコマだった場合、移動可能エリアのアニメも行う
        else if (currentFocusMeshObjectManager != null)
        {
            ChangeBrightNess(currentFocusMeshObjectManager,sw);
            
            // コマをフォーカスしているなら移動可能範囲を明度アニメさせる
            ChangeBrightNessEnableMoveAreas(currentFocusMeshObjectManager);
        }
    }

    /// <summary>
    /// 引数オブジェクトの明度アニメを行う
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
        HashSet<CubeBase> enableAttackAreas = currentFocusMeshObjectManager.GetGameObject().GetComponent<PieceBase>().getCanAttackCubeSet();
        foreach(CubeBase enableAttackArea in enableAttackAreas)
        {
            ChangeBrightNess(enableAttackArea.GetMeshGameManager(), sw);
        }
    }

    /// <summary>
    /// 引数オブジェクトがコマの場合、動ける範囲のキューブの明度アニメをリセットする
    /// </summary>
    /// <param name="currentFocusMeshObjectManager"></param>
    void ResetBrightNessEnableMoveAreas(MeshObjectManager currentFocusMeshObjectManager)
    {
        if (currentFocusMeshObjectManager.GetGameObject().GetComponent<Cube>())
        {
            return;
        }

        HashSet<CubeBase> enableMoveAreas = currentFocusMeshObjectManager.GetGameObject().GetComponent<PieceBase>().getCanMoveCubeSet();
        HashSet<CubeBase> enableAttackAreas = currentFocusMeshObjectManager.GetGameObject().GetComponent<PieceBase>().getCanAttackCubeSet();
        foreach (CubeBase enableMoveArea in enableMoveAreas)
        {
            enableMoveArea.GetMeshGameManager().ResetOriginColor();
        }
        foreach (CubeBase enableAttackArea in enableAttackAreas)
        {
            enableAttackArea.GetMeshGameManager().ResetOriginColor();
        }
    }

    /// <summary>
    /// 今マウスがフォーカスしているオブジェクトの見た目に関するオブジェクトを取得
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
    /// 今マウスがフォーカスしているオブジェクトを取得
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
