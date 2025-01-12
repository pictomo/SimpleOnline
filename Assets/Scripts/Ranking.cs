using Photon.Pun;
using TMPro;
using UnityEngine;

public class Ranking : MonoBehaviourPunCallbacks, IPunObservable
{
    TMP_Text text;

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // if (stream.IsWriting)
        // {
        //     // Transformの値をストリームに書き込んで送信する
        //     stream.SendNext(text.text);
        // }
        // else
        // {
        //     // 受信したストリームを読み込んでTransformの値を更新する
        //     text.text = (string)stream.ReceiveNext();
        // }
    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [PunRPC]
    void Goal(Time goalTime)
    {
        Debug.Log(goalTime);
    }
}
