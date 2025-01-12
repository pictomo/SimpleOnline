using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using TMPro;
using UnityEngine;

// MonoBehaviourPunCallbacksを継承して、photonViewプロパティを使えるようにする
public class PlayerController : MonoBehaviourPunCallbacks
{
    Rigidbody rb;
    float speed = 100f;
    float maxSpeed = 10f;

    public static TMP_Text rankingText;
    static List<string> ranking = new List<string>();

    void Start()
    {
        Debug.Log(PhotonNetwork.LocalPlayer);
        Debug.Log(PhotonNetwork.PlayerList.Length);
        Debug.Log(PhotonNetwork.PlayerListOthers.Length);

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            if (transform.position.y < -10f)
            {
                transform.position = new Vector3(0, 0, 0);
            }

            if (Input.GetKey(KeyCode.Q))
            {
                rb.AddTorque(-transform.up * Time.deltaTime * 100f, ForceMode.VelocityChange);
            }
            if (Input.GetKey(KeyCode.E))
            {
                rb.AddTorque(transform.up * Time.deltaTime * 100f, ForceMode.VelocityChange);
            }
        }
    }

    void FixedUpdate()
    {
        // 自身が生成したオブジェクトだけに移動処理を行う
        if (photonView.IsMine)
        {
            // Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            // // transform.Translate(6f * Time.deltaTime * input.normalized);
            // rb.velocity = new Vector3(input.normalized.x * speed, rb.velocity.y, input.normalized.z * speed);

            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            // ローカル座標に基づく方向ベクトルを生成
            Vector3 movement = transform.right * moveHorizontal + transform.forward * moveVertical;

            // 速度を設定
            // rb.velocity = new Vector3(movement.normalized.x * speed, rb.velocity.y, movement.normalized.z * speed);
            // if (Vector3.Dot(movement, rb.velocity) < 3f) // 条件として不適切
            // if (Vector3.Dot(movement.normalized, rb.velocity) < 3f) // 条件として不適切
            {
                rb.AddForce(new Vector3(movement.x, 0, movement.z) * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
            }
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed); // 速度制限
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (photonView.IsMine)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (collision.contacts.Any(e => e.normal.y > 0.5f))
                {
                    rb.AddForce(Vector3.up * 10f, ForceMode.Impulse);
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            // photonView.RPC(nameof(Goal), RpcTarget.All, DateTime.Now.ToString());
            photonView.RPC(nameof(Goal), RpcTarget.AllViaServer, photonView.Owner.NickName);
        }
    }

    [PunRPC]
    void Goal(string playerName)
    {
        // if (!ranking.Contains(info.Sender.NickName))
        if (!ranking.Contains(playerName))
        {
            // ranking.Add(info.Sender.NickName);
            ranking.Add(playerName);
            if (rankingText)
            {
                rankingText.text = "";
                int rank = 1;
                foreach (var playerNameElem in ranking)
                {
                    rankingText.text += rank + ". " + playerNameElem + "\n";
                    rank++;
                }
            }
        }
    }

    // // 同期による順位の不整合を許さない場合
    // [PunRPC]
    // void Goal(string goalTimeString)
    // {
    //     DateTime goalTime = DateTime.Parse(goalTimeString);
    //     Debug.Log(goalTime.ToString());

    //     // 関数外で定義された配列(もしくはArray?)に、goalTimeが小さい順になるようにプレイヤー名とgoalTimeの情報を代入する処理

    //     if (rankingText)
    //     {
    //         rankingText.text = goalTime.ToString(); // この代わりに、配列に従ってランキングを表示する処理
    //     }
    // }
}