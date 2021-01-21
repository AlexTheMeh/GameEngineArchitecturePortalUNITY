using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShoot : MonoBehaviour
{
    public float fireRate = .25f;
    public Transform gunEnd;

    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    private AudioSource gunAudio;
    private LineRenderer laserLine;
    private float nextFire;

    public Transform PortalBlue;
    public Transform PortalOrange;

    GameObject validWall;



    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        fpsCam = GetComponentInParent<Camera>();

        validWall = GameObject.FindWithTag("ValidWall");
    }

    
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            StartCoroutine(shotEffect());

            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            laserLine.SetPosition(0, gunEnd.position);

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit) && hit.transform.tag == "ValidWall")
            {
                //laserLine.SetPosition(1, hit.point);

                Quaternion hitObjectRotation = Quaternion.LookRotation(hit.normal);
                PortalBlue.transform.position = hit.point;
                PortalBlue.transform.rotation = hitObjectRotation;

                Instantiate(PortalBlue, hit.point, Quaternion.identity);
            }
            else
            { 
                laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward));
            }
        }

        if (Input.GetButtonDown("Fire2") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            StartCoroutine(shotEffect());

            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            laserLine.SetPosition(0, gunEnd.position);

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit) && hit.transform.tag == "ValidWall")
            {
                laserLine.SetPosition(1, hit.point);

                Instantiate(PortalOrange, hit.point, Quaternion.identity);
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward));
            }
        }

        IEnumerator shotEffect()
        {
            gunAudio.Play();

            laserLine.enabled = true;
            yield return shotDuration;
            laserLine.enabled = false;
        }
    }
}
