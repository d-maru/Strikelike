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
        // �I�������R�}�̉��̃{�^��UI���C�A�E�g���擾���A���̏ꏊ�Ƀ{�^��UI��\��������悤�ɂ���
        var statusUi = piece.StatusCanvas.GetComponent<StatusUI>();
        var placeHolder = statusUi.ButtonPlaceHolder;
        buttonPiece = placeHolder.GetComponent<Transform>();

        // ���{�^��UI���C�A�E�g�̍��W���X�N���[�����W�^�ɕϊ�
        var position = RectTransformUtility.WorldToScreenPoint(Camera.main, buttonPiece.transform.position);

        // �{�^��UI�̐e�I�u�W�F�N�g�̏ꏊ���X�V����
        CommandUI.transform.position = position;
    }
}