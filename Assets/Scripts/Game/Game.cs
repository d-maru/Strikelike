using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Player User;
    public BoardManager board;
    private bool isPlayerTurn = true;

    // Start is called before the first frame update
    void Start()
    {
        string[][] CubeName = new string[4][]
        {
            new string[4]{ "Cube (7)","Water Cube (7)","Cube (8)","Water Cube (8)" },
            new string[4]{ "Water Cube (5)","Cube (5)","Water Cube (6)","Cube (6)" },
            new string[4]{ "Cube (3)","Water Cube (3)","Cube (4)","Water Cube (4)" },
            new string[4]{ "Water Cube (1)","Cube (1)","Water Cube (2)","Cube (2)" }
        };
        for(int Row = 0; Row < 4; Row++)
            for (int Column = 0; Column < 4; Column++)
            {
                CubeBase CubeObject = GameObject.Find(CubeName[Row][Column]).GetComponent<CubeBase>();
                CubeObject.Place = new int[2] { Row, Column };
                CubeObject.AdjacentCubes = new() { { Direction.North, null }, { Direction.East, null }, { Direction.West, null }, { Direction.South, null } };
                if (Row > 0)
                {
                    CubeBase linkCube = GameObject.Find(CubeName[Row - 1][Column]).GetComponent<CubeBase>();
                    CubeObject.AdjacentCubes[Direction.North] = linkCube;
                    linkCube.AdjacentCubes[Direction.South] = CubeObject;
                }
                if (Column > 0)
                {
                    CubeBase linkCube = GameObject.Find(CubeName[Row][Column - 1]).GetComponent<CubeBase>();
                    CubeObject.AdjacentCubes[Direction.West] = linkCube;
                    linkCube.AdjacentCubes[Direction.East] = CubeObject;
                }
            }
        GameObject.Find("DogPolyart (1)").GetComponent<PieceBase>().OnCube = GameObject.Find("Cube (4)").GetComponent<CubeBase>();
        GameObject.Find("DogPolyart (2)").GetComponent<PieceBase>().OnCube = GameObject.Find("Water Cube (3)").GetComponent<CubeBase>();
        GameObject.Find("OpponenntDog (1)").GetComponent<PieceBase>().OnCube = GameObject.Find("Water Cube (8)").GetComponent<CubeBase>();
        GameObject.Find("OpponenntDog (2)").GetComponent<PieceBase>().OnCube = GameObject.Find("Cube (7)").GetComponent<CubeBase>();

        GameObject.Find("Cube (4)").GetComponent<CubeBase>().Piece = GameObject.Find("DogPolyart (1)").GetComponent<PieceBase>();
        GameObject.Find("Water Cube (3)").GetComponent<CubeBase>().Piece = GameObject.Find("DogPolyart (2)").GetComponent<PieceBase>();
        GameObject.Find("Water Cube (8)").GetComponent<CubeBase>().Piece = GameObject.Find("OpponenntDog (1)").GetComponent<PieceBase>();
        GameObject.Find("Cube (7)").GetComponent<CubeBase>().Piece = GameObject.Find("OpponenntDog (2)").GetComponent<PieceBase>();
    }

    // Update is called once per frame
    void Update()
    {
        User.Play();
    }
}
