using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IPlayer
{
    public PieceBase piece;
    public bool pieceSelected = false;
    public bool moveSelected = false;
    public GameObject choice;
    private Pieceside pieceside;
    // Start is called before the first frame update
    void Start()
    {

        choice.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool Play()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Collider hitCollider = hit.collider;
                if (hitCollider.CompareTag("Piece"))
                {
                    pieceSelected = true;
                    piece = hitCollider.gameObject.GetComponent<PieceBase>();
                    pieceside = piece.Side;
                    if (pieceside == Pieceside.Player)
                    {
                        //プレイヤーが動かすコマを選んだらSE再生
                        SoundManager.Instance.PlayPieceSelectSE();
                        choice.SetActive(true);
                        var selectButton = choice.GetComponent<ButtonUI>();
                        selectButton.SelectButton(piece);
                    }
                }
                else if (hitCollider.CompareTag("Cube") && pieceSelected && moveSelected)
                {
                    pieceSelected = false;
                    CubeBase cube = hitCollider.gameObject.GetComponent<CubeBase>();
                    if (piece.getCanMoveCubeSet().Contains(cube))
                    {
                        piece.MoveTo(cube);

                        moveSelected = false;

                        return true;
                    }
                }
            }
        }
        return false;

    }

    public void OnClickAttack()
    {
        moveSelected = false;
        choice.SetActive(false);
    }

    public void OnClickMove()
    {
        moveSelected = true;
        choice.SetActive(false);
    }
}
