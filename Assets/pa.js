#pragma strict
public class pa extends MonoBehaviour
{
	var Boss: GameObject;
	function Start () {

	}

	function Update () {
	//Boss.transform.Translate(Vector3.forward * Time.deltaTime);
	Boss.transform.localScale += new Vector3(0.01,0,0);
	}
}