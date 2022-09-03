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

    // 今フォーカスされているオブジェクトがコマオブジェクトなら、情報パネルを表示して、詳細情報を出す
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
                UpdatePieceDetailInfo(piece.Status);
            }
            else
            {
                FocusInfoPanel.SetActive(false);
            }

        }
        else if (currentFocusObject.GetComponent<Cube>())
        {
            FocusInfoPanel.SetActive(false);
        }
    }

    void UpdatePieceDetailInfo(Status status)
    {
        UpdateAttack(status);
        UpdateHp(status);
        UpdatePiceName(status);
    }

   void UpdateHp(Status status)
    {
        HPDisplay.text = string.Format("HP:{0}/{1}", status.Hp, status.Hp);
    }
    void UpdateAttack(Status status)
    {
        AttackDisplay.text = string.Format("Atk:{0}", status.Attack);
    }
    void UpdatePiceName(Status status)
    {
        PieceNameDisplay.text = string.Format("Name:{0}", status.PieceName);
    }
}
