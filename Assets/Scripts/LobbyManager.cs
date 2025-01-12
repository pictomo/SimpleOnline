using Photon.Pun;
using UnityEngine;
using TMPro;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject nameField;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnEnterName()
    {
        // 入力フィールドのテキストを取得する
        string inputText = nameField.GetComponent<TMP_InputField>().text;
        // プレイヤー自身の名前を"Player"に設定する
        PhotonNetwork.NickName = inputText;
        // シーンを"Main"に遷移する
        PhotonNetwork.LoadLevel("Main");
    }
}
