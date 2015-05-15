using UnityEngine;
using System.Collections;

public class NewBehaviourScript1 : MonoBehaviour {
	int goob = 0;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseDown()
	{
		if (goob == 0)
		{
			this.transform.Translate(-3, 0, 3);
			this.transform.Rotate(0, -90, 0);
			goob = 1;
		}
		else  
		{
			this.transform.Rotate(0, 90, 0);
			this.transform.Translate(3, 0, -3);
			goob = 0;
		}
	}
}
