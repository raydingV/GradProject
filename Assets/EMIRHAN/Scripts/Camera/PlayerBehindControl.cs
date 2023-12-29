using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehindControl : MonoBehaviour
{
    private ObjectFader faderObject;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

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
                    if (hit.collider.gameObject == player)
                    {
                        if (faderObject != null)
                        {
                            faderObject.DoFade = false;
                        }
                    }
                    else
                    {
                        faderObject = hit.collider.gameObject.GetComponent<ObjectFader>();

                        if (faderObject != null)
                        {
                            faderObject.DoFade = true;
                        }
                    }
                }
            }
        }
    }
}
