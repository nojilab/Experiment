using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    //[SerializeField] Camera target;
    public GameObject text_object = null;
    public GameObject dist_object = null;
    public GameObject point = null;
    BulletGenerator bulletGeneratorScript;
    public Text explanation;
    public int hit;
    [SerializeField] private int limit;
    public int close;
    Text distance;
    public int scene = 0;

    // Start is called before the first frame update
    void Start()
    {
        //XRDevice.DisableAutoXRCameraTracking(target, false);
        GameObject generator = GameObject.Find("BulletLauncher");
        bulletGeneratorScript = generator.GetComponent<BulletGenerator>();
        limit = bulletGeneratorScript.limit;
        hit = 0;
        close = 0;
        explanation = text_object.GetComponent<Text>();
        //distance = dist_object.GetComponent<Text>();
        if (scene == 0)
        {
            explanation.text = ("準備ができたらスタートします\n実験中はなるべく赤い点のあたりを\n見るようにしてください");
        }
     }


    void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            hit = 1;
        }
        switch(scene)
        {
            case 0:
                if (Input.GetKeyDown(KeyCode.Y))
                {
                    scene = 1;
                    Debug.Log("y");
                }
                break;
            case 1:
                hit = 0;
                explanation.text = ("");
                if (bulletGeneratorScript.num == limit+1)
                {
                    scene = 3;
                }else
                {
                }
                break;
            case 2:
                explanation.text = ("のけぞりそうになった:右コントローラーのボタン \nならなかった:左コントローラーのボタン");
                if (OVRInput.GetDown(OVRInput.RawButton.A) || OVRInput.GetDown(OVRInput.RawButton.B))
                {
                    close = 1;
                    scene = 1;
                }else if (OVRInput.GetDown(OVRInput.RawButton.Y) || OVRInput.GetDown(OVRInput.RawButton.X))
                {
                    close = 0;
                    scene = 1;
                }
                break;
            case 3:
                explanation.text = ("１セット終了です．お疲れさまでした．");
                break;
            default:
                Debug.Log(scene);
                break;
        }
    }
}
