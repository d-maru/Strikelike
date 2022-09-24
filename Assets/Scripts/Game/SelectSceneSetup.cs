using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SelectSceneSetup : MonoBehaviour
{
    // 初期配置をファイルから読み込む場合のファイル名
    public string defaultCubeMapFile = "defaultCubeMap.csv";

    // ファイルから読み込まない/読み込めない場合のマップサイズ
    public int mapSize = 5;

    // キューブ1つあたりの大きさ
    public float cubeSize = 1.5f;

    // キューブ配置の基点位置
    public Vector3 defaultPosition = new (-3.526728f, 7.115623f, 2.859495f);

    // キューブの種類数
    public int cubePrefabTypeCount = 2;
    public GameObject prefab_Cube;
    public GameObject prefab_WaterCube;


    // 親となるボードオブジェクト
    public GameObject gameBoard;

    /// <summary>
    /// キューブの配置のリストを生成する
    /// ファイルを読み込んでマッピングを2次元リストで返却する
    /// </summary>
    /// <returns>ファイルから読み込んだキューブのマッピングを記載したint型2次元リスト</returns>
    public List<List<int>> MakeCubeList()
    {
        // 初期配置のキューブリスト
        List<List<int>> cubeLists = new();

        try
        {
            // ファイル読み込み
            if (File.Exists(Application.dataPath + "/" + defaultCubeMapFile))
            {
                using StreamReader sr = new(Application.dataPath + "/" + defaultCubeMapFile, System.Text.Encoding.UTF8);

                // 末尾まで繰り返す
                while (!sr.EndOfStream)
                {
                    // 1行ずつ読み込んでカンマ区切りでリストに格納する
                    List<string> stringList = new(sr.ReadLine().Split(','));
                    cubeLists.Add(stringList.ConvertAll(x => int.Parse(x)));
                }
            }
            else
            {
                // ファイルが無ければ全部-1
                List<int> cubeListOneRow = new(System.Linq.Enumerable.Repeat<int>(-1, mapSize));
                for (int index = 0; index < mapSize; index++)
                {
                    cubeLists.Add(cubeListOneRow);
                }
            }
        }
        catch
        {
            // リストを一回空に
            cubeLists.Clear();

            // ファイル読み込みや型変換に失敗したら全部-1
            List<int> cubeListOneRow = new(System.Linq.Enumerable.Repeat<int>(-1, mapSize));
            for (int index = 0; index < mapSize; index++)
            {
                cubeLists.Add(cubeListOneRow);
            }
        }
        return cubeLists;
    }

    private void Awake()
    {
        // ボードの下をお掃除
        foreach (Transform n in gameBoard.transform)
        {
            GameObject.Destroy(n.gameObject);
        }

        // ファイルから読み込んでリスト作成
        List<List<int>> cubeLists = MakeCubeList();

        // キューブ連結用のキューブリスト
        List<CubeBase> CubeBaseList = new();

        // 各行にループ
        int rowIndex = 0;
        foreach (List<int> cubeList in cubeLists)
        {
            // 格列にループ
            int colIndex = 0;
            foreach (int cubeType in cubeList)
            {
                // 配置するキューブ
                GameObject cube=null;

                // 値が正しくなければランダム
                int cubeTypeValid = cubeType;
                if (cubeType < 0 || cubeType > cubePrefabTypeCount)
                {
                    cubeTypeValid = Random.Range(0, cubePrefabTypeCount + 1);
                }
                switch (cubeTypeValid)
                {
                    // 1の時はCube
                    case 1:
                        cube = Instantiate(prefab_Cube, new Vector3(defaultPosition.x + rowIndex * cubeSize, defaultPosition.y-8, defaultPosition.z + colIndex * cubeSize), Quaternion.identity, gameBoard.transform);
                        break;
                    // 2の時はWaterCube
                    case 2:
                        cube = Instantiate(prefab_WaterCube, new Vector3(defaultPosition.x + rowIndex * cubeSize, defaultPosition.y-8, defaultPosition.z + colIndex * cubeSize), Quaternion.identity , gameBoard.transform);
                        break;
                    // 0の時は空
                    case 0:
                        break;
                }

                // 名前を設定
                if(cube is not null)
                    cube.name = "Cube_" + rowIndex + "_" + colIndex;

                // 隣接キューブ設定
                CubeBase cubeBase = cube?.GetComponent<CubeBase>();
                if(cubeBase is not null){

                    // 隣接キューブの初期値としてnullを設定
                    cubeBase.AdjacentCubes = new() { { Direction.North, null }, { Direction.East, null }, { Direction.West, null }, { Direction.South, null } };
                    
                    if (rowIndex > 0)
                    {
                        CubeBase linkCube = CubeBaseList[(rowIndex - 1) * mapSize + colIndex]?.GetComponent<CubeBase>();
                        if(linkCube is not null)
                        {
                            cubeBase.AdjacentCubes[Direction.North] = linkCube;
                            linkCube.AdjacentCubes[Direction.South] = cubeBase;
                        }
                    }
                    if (colIndex > 0)
                    {
                        CubeBase linkCube = CubeBaseList[rowIndex * mapSize + colIndex - 1]?.GetComponent<CubeBase>();
                        if (linkCube is not null)
                        {
                            cubeBase.AdjacentCubes[Direction.West] = linkCube;
                            linkCube.AdjacentCubes[Direction.East] = cubeBase;
                        }
                    }
                }
                CubeBaseList.Add(cubeBase);
                colIndex++;
            }
            rowIndex++;
        }

    }
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
