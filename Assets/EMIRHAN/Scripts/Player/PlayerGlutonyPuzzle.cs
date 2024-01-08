using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerGlutonyPuzzle : MonoBehaviour
{
    [SerializeField] Vector3 CheckPoint;
    [SerializeField] Vector3 FinishPoint;

    PlayerManager playerManager;

    public GameObject clickedObject;

    public bool tileInput = true;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    private void FixedUpdate()
    {
        if (clickedObject != null)
        {
            PlayerTransform();
        }
    }

    public void PlayerDead()
    {
        clickedObject = null;
        StartCoroutine(GetCheckPoint());
    }

    public void FinishPuzzle()
    {
        clickedObject = null;
        StartCoroutine(GetFinishPoint());
    }

    private IEnumerator GetCheckPoint()
    {
        transform.position = new Vector3(CheckPoint.x, CheckPoint.y, CheckPoint.z);
        tileInput = true;
        yield return new WaitForSeconds(0.01f);
    }
    private IEnumerator GetFinishPoint()
    {
        clickedObject = null;
        transform.position = new Vector3(FinishPoint.x, FinishPoint.y, FinishPoint.z);
        tileInput = true;
        playerManager.InputEnable = true;
        yield return new WaitForSeconds(0.01f);
    }

    public void PlayerTransform()
    {
        transform.position = Vector3.MoveTowards(transform.position, clickedObject.transform.position, 5f * Time.deltaTime);

        Vector3 moveDirection = (clickedObject.transform.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10 * Time.deltaTime);

        if (Vector3.Distance(clickedObject.transform.position, transform.position) < 0.9f)
        {
            tileInput = true;
            clickedObject = null;
            Debug.Log("TRUE");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GluttonyPuzzleStart")
        {
            PlayerDead();
        }
    }
}
