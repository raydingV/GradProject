using UnityEngine;

public class StonePuzzle : MonoBehaviour
{
    [SerializeField] EnteranceLevelManager levelManager;
    [SerializeField] GameObject _flyObject;
    [SerializeField] GemManager _gemObject;
    [SerializeField] int tagHolder;

    GameObject placedGemObject;

    bool checkOnce = false;
    bool placedGem = false;
    bool callOnce = false;

    Rigidbody rb;

    Vector3 startTransform;

    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<EnteranceLevelManager>();
        startTransform = _flyObject.transform.position;
    }

    private void FixedUpdate()
    {
        flyToObject();

        if(placedGemObject != null && _gemObject != null)
        {
            GemTransform();
        }
    }

    void GemTransform()
    {
        if(_gemObject.took == false)
        {
            placedGemObject.transform.position = _flyObject.transform.position;
            placedGemObject.transform.rotation = _flyObject.transform.rotation;
        }
    }

    void flyToObject()
    {
        float yOffset = Mathf.Sin(Time.time * 1) * 0.3f;
        _flyObject.transform.position = startTransform + new Vector3(0, yOffset, 0);
    }

    void controlTag(int tag)
    {
        if(tag == tagHolder && checkOnce == false)
        {
            levelManager.puzzleControl[tag] = true;
            levelManager.checkPuzzle();
            checkOnce = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && callOnce == false)
        {
            callOnce = true;
            levelManager.BossNameOnScreen(gameObject.name);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "HoldObject" && placedGem == false)
        {
            if(other.gameObject.TryGetComponent(out GemManager gem))
            {
                _gemObject = gem;
            }

            //rb = other.GetComponent<Rigidbody>();
            //rb.constraints = RigidbodyConstraints.FreezeAll;

            controlTag(_gemObject.ObjectTag);

            placedGemObject = other.gameObject;

            placedGem = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Untagged")
        {
            rb = null;
            _gemObject = null;
            levelManager.puzzleControl[tagHolder] = false;
            checkOnce = false;
            placedGem = false;
            placedGemObject = null;
        }

        if (other.tag == "Player")
        {
            levelManager.BossNameOffScreen();
            callOnce = false;
        }
    }
}
