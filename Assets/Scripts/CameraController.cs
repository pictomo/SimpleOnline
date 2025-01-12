using UnityEngine;

public class CameraController : MonoBehaviour
{


    [SerializeField] Transform target; // 追従対象のオブジェクト
    [SerializeField] float distance = 10.0f; // ターゲットからの距離
    [SerializeField] float height = 5.0f; // ターゲットの上方の高さ

    public GameObject objectObject;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (objectObject)
        {
            // transform.position = new Vector3(objectObject.transform.position.x, objectObject.transform.position.y, objectObject.transform.position.z - 10);

            target = objectObject.transform;

            // ターゲットの後ろと上方に位置するオフセットを計算
            Vector3 offset = -target.forward * distance + target.up * height;

            // カメラの位置を設定：ターゲット位置 + 計算されたオフセット
            transform.position = target.position + offset;

            // カメラをターゲットの方向に向ける
            transform.LookAt(target);
        }
    }
}
