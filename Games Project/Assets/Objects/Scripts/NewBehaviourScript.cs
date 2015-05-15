using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {
	public GameObject Buh;
	public GameObject Guh;
	public GameObject EBuh;
	public GameObject EGuh;
	public GameObject Player;
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Player.transform.position.z <= 30 && Player.transform.position.z > 0)
		{
			if (Player.transform.position.x <= 141 && Player.transform.position.x >= 111)
			{
				if (Buh.transform.position.x >= 106) 
				{
					Buh.transform.Translate (Vector3.left * Time.deltaTime);
					Guh.transform.Translate (Vector3.right * Time.deltaTime);
				}
			}
			else
			{
				if (Buh.transform.position.x < 119)
				{
					Buh.transform.Translate (Vector3.right * Time.deltaTime);
					Guh.transform.Translate (Vector3.left * Time.deltaTime);
				}
			}
			if (Player.transform.position.x <=32 && Player.transform.position.x >= 0)
			{
				if (EBuh.transform.position.x >= -6)
				{
					EBuh.transform.Translate (Vector3.left * Time.deltaTime);
					EGuh.transform.Translate (Vector3.right * Time.deltaTime);
				}
			}
			else
			{
				if (EBuh.transform.position.x < 8)
				{
					EBuh.transform.Translate (Vector3.right * Time.deltaTime);
					EGuh.transform.Translate (Vector3.left * Time.deltaTime);
				}
			}
		}
		else
		{
			if (Buh.transform.position.x < 119)
			{
				Buh.transform.Translate (Vector3.right * Time.deltaTime);
				Guh.transform.Translate (Vector3.left * Time.deltaTime);
			}
			if (EBuh.transform.position.x < 8)
			{
				EBuh.transform.Translate (Vector3.right * Time.deltaTime);
				EGuh.transform.Translate (Vector3.left * Time.deltaTime);
			}
		}
	}
}
