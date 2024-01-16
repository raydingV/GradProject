using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehindControl : MonoBehaviour
{
    private ObjectFader faderObject;

    [SerializeField] GameObject player;

    [SerializeField] GameManager gameManager;

    void Update()
    {
        if (player != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.tag == "Player" || hit.collider.gameObject.tag == "Boss")
                    {
                        //if (faderObject != null)
                        //{
                        //    faderObject.DoFade = false;
                        //}

                        if(gameManager != null)
                        {
                            gameManager.FadeObjects = false;
                        }
                    }
                    else
                    {
                        //faderObject = hit.collider.gameObject.GetComponent<ObjectFader>();

                        //if (faderObject != null)
                        //{
                        //    faderObject.DoFade = true;
                        //}

                        if(gameManager != null)
                        {
                            gameManager.FadeObjects = true;
                        }
                    }
                }
            }
        }
    }
}
