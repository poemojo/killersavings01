using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Logo : MonoBehaviour
{
   [SerializeField]
   protected Image _self = null;
 

   // Use this for initialization
   void Start()
   {
      if (_self == null)
      {
         _self = gameObject.GetComponent<Image>();
      }
      _self.material.SetFloat("_Blend", 1.0f);
   }

   // Update is called once per frame
   void Update()
   {

      if (IntroScript.AllDone && (Time.time > IntroScript.AllDoneTime + 1.375f))
      {
         float value = _self.material.GetFloat("_Blend");
         value = Mathf.Lerp(value, 0.0f, Time.deltaTime);
         _self.material.SetFloat("_Blend", value);
      }


   }


}
