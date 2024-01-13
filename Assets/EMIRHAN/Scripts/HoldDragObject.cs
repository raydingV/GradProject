using UnityEngine;

public class HoldDragObject : MonoBehaviour
{
    [SerializeField] public int ObjectTag;
    [SerializeField] PlayerManager player;
    [SerializeField] EnteranceLevelManager levelManager;

    public bool took = false;

    Rigidbody rb;

    private void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<EnteranceLevelManager>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HoldObject();
        RelaseObject();
    }

    void HoldObject()
    {
        if (Input.GetKeyDown(KeyCode.E) && player != null && levelManager.puzzleDone == false && levelManager.tookGem == false)
        {
            transform.position = player.holdObject.transform.position;
            gameObject.tag = "Untagged";
            //rb.constraints = RigidbodyConstraints.FreezeAll;
            gameObject.transform.parent = player.transform;
            levelManager.tookGem = true;
            took = true;
        }
    }

    void RelaseObject()
    {
        if (Input.GetKeyDown(KeyCode.G) && player != null && took == true)
        {
            gameObject.tag = "HoldObject";
            rb.freezeRotation = false;
            //rb.constraints = RigidbodyConstraints.None;
            gameObject.transform.parent = null;
            levelManager.tookGem = false;
            took = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player = other.GetComponent<PlayerManager>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            player = null;
        }
    }
}
