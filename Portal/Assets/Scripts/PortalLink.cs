using UnityEngine;
using System.Collections;

public class PortalLink : MonoBehaviour
{
    public GameObject otherPortal;
    GameObject player;
    Collider playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        //name print(player.transform.position);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("something hit the portal");

        if (other == playerCollider)
        {
            print("player touch");

            player.transform.rotation = otherPortal.transform.rotation;
            player.transform.position = otherPortal.transform.position + otherPortal.transform.forward * 1;
        }
    }
}
