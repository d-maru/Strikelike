using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour
{
    Transform buttonPiece;
    RectTransform parentUI;
    [SerializeField] Camera pieceCamera;
    [SerializeField] RectTransform moveButtonUI;
    [SerializeField] RectTransform attackButtonUI;
    [SerializeField] Transform CommandUI;

    // Start is called before the first frame update
    void Start()
    {
        parentUI = moveButtonUI.parent.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectButton(PieceBase piece)
    {
        // 選択したコマの仮のボタンUIレイアウトを取得し、その場所にボタンUIを表示させるようにする
        var statusUi = piece.StatusCanvas.GetComponent<StatusUI>();
        var placeHolder = statusUi.ButtonPlaceHolder;
        buttonPiece = placeHolder.GetComponent<Transform>();

        // 仮ボタンUIレイアウトの座標をスクリーン座標型に変換
        var position = RectTransformUtility.WorldToScreenPoint(Camera.main, buttonPiece.transform.position);

        // ボタンUIの親オブジェクトの場所を更新する
        CommandUI.transform.position = position;
    }
}