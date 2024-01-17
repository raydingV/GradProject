using UnityEngine;

public class GemManager : MonoBehaviour
{
    [SerializeField] public int ObjectTag;
    [SerializeField] GemData _gemData;
    [SerializeField] PlayerManager player;
    [SerializeField] EnteranceLevelManager levelManager;

    [SerializeField] AudioClip tookSound;
    [SerializeField] AudioClip putSound;

    public bool took = false;

    string Riddle;

    Rigidbody rb;

    private void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<EnteranceLevelManager>();
        rb = GetComponent<Rigidbody>();
        
        if(_gemData != null )
        {
            Riddle = _gemData.Riddles[ObjectTag];
        }
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
            transform.rotation = player.holdObject.transform.rotation;
            gameObject.tag = "Untagged";
            //rb.constraints = RigidbodyConstraints.FreezeAll;
            levelManager.audioSource.PlayOneShot(tookSound);
            levelManager.RiddleOnScreen(Riddle);
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
            levelManager.audioSource.PlayOneShot(putSound);
            levelManager.RiddleOffScreen();
            gameObject.transform.parent = null;
            levelManager.tookGem = false;
            took = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.gameObject.TryGetComponent(out PlayerManager _player))
        {
            player = _player;
        }

        Debug.Log(other.gameObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            player = null;
        }
    }
}
