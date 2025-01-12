using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class Scene : MonoBehaviourPunCallbacks
{
    [SerializeField] int maxPlayerPerRoom = 2;

    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject rankingTextObj;

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster()
    {
        // // "Room"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
        // PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayerPerRoom });
    }

    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        if (playerCount >= maxPlayerPerRoom)
        {
            StartGame();
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        if (playerCount >= maxPlayerPerRoom)
        {
            StartGame();
        }
    }

    void StartGame()
    {

        // ランダムな座標に自身のアバター（ネットワークオブジェクト）を生成する
        // var position = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        var position = new Vector3(0, 0, 0);

        GameObject player = PhotonNetwork.Instantiate("Avatar", position, Quaternion.identity);
        mainCamera.GetComponent<CameraController>().objectObject = player;
        PlayerController.rankingText = rankingTextObj.GetComponent<TMP_Text>();
    }
}