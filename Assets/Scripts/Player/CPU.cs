using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CPU : MonoBehaviour, IPlayer
{
    public GameObject[] pieces;
    public GameObject picPiece;
    private Pieceside pieceside;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// コマの見た目に関する設定を行っているオブジェクトを取得
    /// </summary>
    /// <returns></returns>
    public GameObject GetMeshObject()
    {
        return transform.Find("polySurface1").gameObject;
    }

    public bool Play()
    {
        pieces = GameObject.FindGameObjectsWithTag("Piece");
        Debug.Log(pieceside);
        var opponentPieces = new List<PieceBase>();
        foreach (var sidePiece in pieces)
        {
            var pieceBase = sidePiece.GetComponent<PieceBase>();
            pieceside = pieceBase.Side;
            if (pieceside == Pieceside.Opponent)
            {
                opponentPieces.Add(pieceBase);
            }
        }

        int pieceNumber = Random.Range(0, opponentPieces.Count);
        var opponentPiece = opponentPieces[pieceNumber];
        var cubes = opponentPiece.getCanMoveCubeSet().ToList();
        var cube = cubes[Random.Range(0, cubes.Count)];
        opponentPiece.transform.position = new Vector3(cube.transform.position.x, 0, cube.transform.position.z);
        return true;
    }
}
