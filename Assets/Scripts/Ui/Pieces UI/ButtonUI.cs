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
    [SerializeField] GameObject UIPiece;
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
        var button = GameObject.Find("MoveButton");
        var choiceUI = button.GetComponent<ButtonUI>();
        buttonPiece = UIPiece.GetComponent<Transform>();
        var pieceWorldPosition = buttonPiece.position + worldOffset;
        var pieceScreenPosition = pieceCamera.WorldToScreenPoint(pieceWorldPosition);
        RectTransformUtility.ScreenPointToLocalPointInRectangle
            (parentUI, pieceScreenPosition, null, out var uiLocalPosition);
        moveButtonUI.position = uiLocalPosition;
        attackButtonUI.position = uiLocalPosition + buttonOffset;
    }

    public bool SelectButton()
    {


        /*
        GameObject moveButton = GameObject.Find("Move");
        GameObject attackButton = GameObject.Find("Attack");
        
        return false;
        */
        return true;
    }
}