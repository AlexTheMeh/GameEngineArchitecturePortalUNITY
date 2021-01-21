using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPortal : MonoBehaviour
{
	public GameObject BluePortal;
	public GameObject OrangePortal;
	
	private AudioSource gunAudio;

	GameObject validWall;
	GameObject mainCamera;

	void Start()
	{
		mainCamera = GameObject.FindWithTag("MainCamera");
		validWall = GameObject.FindWithTag("ValidWall");
		gunAudio = GetComponent<AudioSource>();
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			throwPortal(BluePortal);
		}

		if (Input.GetMouseButtonDown(1))
		{
			throwPortal(OrangePortal);
		}
	}

	void throwPortal(GameObject portal)
	{
		int x = Screen.width / 2;
		int y = Screen.height / 2;

		Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit) && hit.transform.tag == "ValidWall")
		{
			Quaternion hitObjectRotation = Quaternion.LookRotation(hit.normal);
			portal.transform.position = hit.point;
			portal.transform.rotation = hitObjectRotation;
		}
	}
}
