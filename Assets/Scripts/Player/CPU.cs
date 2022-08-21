using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CPU : MonoBehaviour, IPlayer
{
    public GameObject[] pieces;
    public GameObject picPiece;
    private Pieceside pieceside;
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
        var cubes = opponentPiece.getCanMoveCubeSet();
        var candidates = from c in cubes
                         where c != opponentPiece.OnCube
                         select c;
        var destination = candidates.ElementAt(Random.Range(0, candidates.Count()));
        opponentPiece.MoveTo(destination);
        return true;
    }
}
