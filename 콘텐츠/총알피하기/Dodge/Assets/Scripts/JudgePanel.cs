using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgePanel : MonoBehaviour
{
    public GameObject[] judgepanel;
    public static int judgeindex;
    public float horizontalInput=0;
    public GameObject player1;
    public float verticalInput=0;
    public float moveForce=0;
    public static Rigidbody rigidbody;
    private static JudgePanel _panel;
    Transform trans;
    PlayerAgent player;
    public static JudgePanel Instance { get { return Panel; }  }
    public GameObject panel;
    public static JudgePanel Panel { get => _panel; set => _panel = value; }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        trans = gameObject.transform;
    }

    // Update is called once per frame
    public void SetParameter(float h, float v, float m)
    {
        h = horizontalInput;
        v = verticalInput;
        m = moveForce;
    }
    public  void FixedUpdate()
    {
         player= GameObject.Find("plane").GetComponent<PlayerAgent>();
        if (player.dead || player.dead2)
        {
            //panel.transform.localPosition = new Vector3(1.4f, 1.3f, 0);
            //gameObject.transform.localPosition = trans.localPosition;
            //rigidbody.position = trans.localPosition;
        }
        else { }
        //rigidbody.AddForce( horizontalInput *  moveForce * 4.0f,  verticalInput *  moveForce * 4.0f, 0f);
    }
    
    public void OnTriggerExit(Collider other)
    {
        char[] c = gameObject.name.ToCharArray();
        judgeindex = int.Parse(c[5].ToString()) - 1;
        
        switch (judgeindex)
        {
            case 0:
                player.isjudge[0] = false;
                break;
            case 1:
                player.isjudge[1] = false;
                break;
            case 2:
                player.isjudge[2] = false;
                break;
            case 3:
                player.isjudge[3] = false;
                break;
            case 4:
                player.isjudge[4] = false;

                break;
            case 5:
                player.isjudge[5] = false;

                break;
            case 6:
                player.isjudge[6] = false;

                break;
            case 7:
                player.isjudge[7] = false;

                break;
        }
    }
    public  void OnTriggerEnter(Collider other)
    {
        char[] c = gameObject.name.ToCharArray();
        judgeindex = int.Parse(c[5].ToString()) - 1;
        string bulletindex = "dead";
        switch (judgeindex)
        {
            case 0:
                player.isjudge[0] = true;
                break;
            case 1:
                player.isjudge[1] = true;
                break;
            case 2:
                player.isjudge[2] = true;
                break;
            case 3:
                player.isjudge[3] = true; 
                break;
            case 4:
                player.isjudge[4] = true;

                break;
            case 5:
                player.isjudge[5] = true;

                break;
            case 6:
                player.isjudge[6] = true;

                break;
            case 7:
                player.isjudge[7] = true;

                break;
            default:
                break;
            
        }
    }
}
