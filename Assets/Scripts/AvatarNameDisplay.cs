using Photon.Pun;
using TMPro;

// MonoBehaviourPunCallbacksを継承して、photonViewプロパティを使えるようにする
public class AvatarNameDisplay : MonoBehaviourPunCallbacks
{
    void Start()
    {
        var nameLabel = GetComponent<TextMeshPro>();
        // プレイヤー名とプレイヤーIDを表示する
        nameLabel.text = $"{photonView.OwnerActorNr}_{photonView.Owner.NickName}"; //MonoBehaviourPunCallbacksを継承しているのでphotonViewプロパティが明示せず使える
    }
}