using UnityEngine;
using UnityEngine.UI;

public class FlushController : MonoBehaviour
{
    // Start is called before the first frame update
    public Image img;
    void Start()
    {
        img.color = Color.clear;
    }

    // Update is called once per frame
    void Update()
    {
        this.img.color = Color.Lerp(this.img.color, Color.clear, Time.deltaTime);
    }

    public void Damage(){
        Debug.Log("damage");
        this.img.color = new Color(0.5f, 0f, 0f, 0.5f);
    }
}
