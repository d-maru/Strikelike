using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FocusInfoPanelManager : MonoBehaviour
{

    public FocusInfo FocusInfo;
    public GameObject FocusInfoPanel; 

    public TextMeshProUGUI HPDisplay;
    public TextMeshProUGUI AttackDisplay;
    public TextMeshProUGUI PieceNameDisplay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDetailInfoPanel();
    }

    // ���t�H�[�J�X����Ă���I�u�W�F�N�g���R�}�I�u�W�F�N�g�Ȃ�A���p�l����\�����āA�ڍ׏����o��
    public void UpdateDetailInfoPanel()
    {
        GameObject currentFocusObject = FocusInfo.GetCurrentFocusObject();

        if (currentFocusObject == null)
        {
            FocusInfoPanel.SetActive(false);
            return;
        }

        if (currentFocusObject.GetComponent<PieceBase>())
        {
            PieceBase piece = currentFocusObject.GetComponent<PieceBase>();
            if (piece != null)
            {
                FocusInfoPanel.SetActive(true);
                UpdatePieceDetailInfo(piece.status);
            }
            else
            {
                FocusInfoPanel.SetActive(false);
            }

        }
        else if (currentFocusObject.CompareTag("Cube"))
        {
            FocusInfoPanel.SetActive(false);
        }
    }

    public void UpdatePieceDetailInfo(Status status)
    {
        UpdateAttack(status);
        UpdateHp(status);
        UpdatePiceName(status);
    }

   void UpdateHp(Status status)
    {
        HPDisplay.text = string.Format("HP:{0}/{1}", status.hp, status.hp);
    }
    void UpdateAttack(Status status)
    {
        AttackDisplay.text = string.Format("Atk:{0}", status.attack);
    }
    void UpdatePiceName(Status status)
    {
        PieceNameDisplay.text = string.Format("Name:{0}", status.pieceName);
    }
}
