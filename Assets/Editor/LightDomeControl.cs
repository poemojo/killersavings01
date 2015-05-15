using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class LightDomeControl : MonoBehaviour
{
   [SerializeField]
   protected Color lightColor = Color.white;
   
   [SerializeField]
   [Range(0.0f, 8.0f)]
   protected float Gintensity = 1.0f;

   [SerializeField]
   protected Texture cookie;

   public Texture Cookie
   {
      get { return cookie; }
      set { cookie = value; }
   }


   public void Start()
   {
      Light lt = gameObject.GetComponent<Light>();

      
   }
}
