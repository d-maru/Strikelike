using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IPlayer
{
    public PieceBase piece;
    public bool pieceSlected = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Play()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Collider hitCollider = hit.collider;
                if (hitCollider.CompareTag("Piece"))
                {
                    pieceSlected = true;
                    piece = hitCollider.gameObject.GetComponent<PieceBase>();
                    //プレイヤーが動かすコマを選んだらSE再生
                    SoundManager.Instance.PlayPieceSelectSE();
                    /*
                    var choice = choiceUI.SelectButton();
                    if (choice)
                    {

                    }
                    else
                    {

                    }
                    */
                }
                else if (hitCollider.CompareTag("Cube") && pieceSlected)
                {
                    pieceSlected = false;
                    CubeBase cube = hitCollider.gameObject.GetComponent<CubeBase>();
                    if (piece.getCanMoveCubeSet().Contains(cube))
                    {
                        piece.MoveTo(cube);
                        return true;
                    }
                }
            }
        }
        return false;
    }
}
