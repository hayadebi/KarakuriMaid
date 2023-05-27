using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool enablecamera = true;
    //　キャラクターのTransform
    [SerializeField]
    private Transform charaLookAtPosition;
    //　カメラの移動スピード
    [SerializeField]
    private float cameraMoveSpeed = 2f;
    //　カメラの回転スピード
    //　カメラのキャラクターからの相対値を指定
    [SerializeField]
    private Vector3 basePos = new Vector3(0f, 0f, 2f);
    // 障害物とするレイヤー
    [SerializeField]
    private LayerMask obstacleLayer;
    [SerializeField]
    private Vector3 defaultPos;
    [SerializeField]
    private bool hittrg = false;
    private bool stoptrg = false;
    private float stoptime = 0;
    GameObject P;
    GameObject cpos;
    //BoxCollider bc;
    private void Awake()
    {
        cpos = GameObject.Find("cmpos");
        //bc = this.GetComponent<BoxCollider>();
        //Vector3 bs = this.transform.position;
        //bs.x = 1;
       // bs.y = 1;
        //bs.z = 1;
        //bc.size = bs;
       // bs.x = 0;
       // bs.y = 0;
       // bs.z = 0;
        //bc.center = bs;
        P = GameObject.Find("Player");
        charaLookAtPosition = P.GetComponent<Transform>();
        
    }

    void LateUpdate()
    {
        if (GManager.instance.walktrg == true && P != null && GManager.instance.Triggers[23] != 1 && enablecamera == true)
        {
            if (stoptrg == true)
            {
                stoptime += Time.deltaTime;
                if (stoptime > 2f)
                {
                    stoptime = 0;
                    stoptrg = false;
                }
            }
            //　通常のカメラ位置を計算
            defaultPos = P.transform.position + (P.transform.right * 2 + P.transform.up * 4.5f + P.transform.forward * -8);
            //var inputhight = P.transform.position;// + P.transform.up * 2;
            //　カメラの位置をキャラクターの後ろ側に移動させる
            if (hittrg == true )
            {
                transform.position = Vector3.Lerp(transform.position, cpos.transform.position, cameraMoveSpeed * Time.deltaTime);
            }
            else if (hittrg == false && stoptrg == false)
            {
                transform.position = Vector3.Lerp(transform.position, defaultPos, cameraMoveSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, cpos.transform.position, cameraMoveSpeed * Time.deltaTime);
            }

            RaycastHit hit;
            //　キャラクターとカメラの間に障害物があったら障害物の位置にカメラを移動させる
            if (Physics.Linecast(P.transform.position, transform.position, out hit, obstacleLayer) )
            {
                if (hittrg == false )
                {
                    stoptrg = true;
                    hittrg = true;
                }
            }
            else 
            {
                if (hittrg == true && stoptrg == false)
                {
                    
                    hittrg = false;
                }
            }
            //　レイを視覚的に確認
            //Debug.DrawLine(charaLookAtPosition.position, transform.position, Color.red, 0f, false);

            //　スピードを考慮しない場合はLookAtで出来る
            //transform.LookAt(this.transform.position);
            //　スピードを考慮する場合
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(charaLookAtPosition.position - transform.position), cameraRotateSpeed * Time.deltaTime);
        }
        else
        {

        }
    }

}