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
    [SerializeField] Vector3 worldOffset;
    [SerializeField] Vector2 buttonOffset;
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
        var statusUi = piece.StatusCanvas.GetComponent<StatusUI>();
        var placeHolder = statusUi.ButtonPlaceHolder;
        buttonPiece = placeHolder.GetComponent<Transform>();
        var pieceWorldPosition = buttonPiece.position;
        var pieceScreenPosition = pieceCamera.WorldToScreenPoint(pieceWorldPosition);
        RectTransformUtility.ScreenPointToLocalPointInRectangle
            (parentUI, pieceScreenPosition, null, out var uiLocalPosition);
        moveButtonUI.position = buttonPiece.position;
        attackButtonUI.position = buttonPiece.position;
    }
}