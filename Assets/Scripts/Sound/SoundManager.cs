using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    /// <summary>
    /// ゲーム中でサウンドを出す処理は一つで良いのでシングルトンを仕様
    /// 仮にシーン間の移動があった場合は移動先シーンで作成されたサウンド管理オブジェクトは
    /// 削除するようにして常に単一のオブジェクトだけになるようにしている
    /// </summary>
    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public AudioSource audioSourceBGM;
    public AudioClip[] clipBGM;

    public AudioSource audioSourceSE;
    public AudioClip[] clipSE;

    enum SE_TYPE
    {
        MOVE_PIECE_SELECT,
        MOVE_PIECE_CANCEL,
        FOCUS_PIECE,
        FOCUS_CUBE,
        SE_TYPE_NUM,
    } 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //public void PlayBGM(string sceneName)
    //{

    //    audioSourceBGM.Stop();

    //    switch (sceneName)
    //    {
    //        case "Title":
    //            audioSourceBGM.clip = clipBGM[0];
    //            break;

    //        case "Town":
    //            audioSourceBGM.clip = clipBGM[1];
    //            break;
    //        case "Quest":
    //            audioSourceBGM.clip = clipBGM[2];
    //            break;
    //        case "Battle":
    //            audioSourceBGM.clip = clipBGM[3];
    //            break;
    //        default:
    //            audioSourceBGM.clip = clipBGM[0];
    //            break;
    //    }

    //    audioSourceBGM.Play();
    //}

    public void PlayPieceSelectSE()
    {
        audioSourceSE.PlayOneShot(clipSE[(int)SE_TYPE.MOVE_PIECE_SELECT]);
    }

    public void PlayPieceCancelSE()
    {
        audioSourceSE.PlayOneShot(clipSE[(int)SE_TYPE.MOVE_PIECE_CANCEL]);
    }

    public void PlayFocusPieceSE()
    {
        audioSourceSE.PlayOneShot(clipSE[(int)SE_TYPE.FOCUS_PIECE]);
    }

    public void PlayFocusCubeSE()
    {
        audioSourceBGM.PlayOneShot(clipSE[(int)SE_TYPE.FOCUS_CUBE]);
    }
}
