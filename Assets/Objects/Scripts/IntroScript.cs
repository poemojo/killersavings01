using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntroScript : MonoBehaviour 
{
   [SerializeField]
   private string[] lines = new string[4];

   [SerializeField]
   private Text txt_dialog = null;

   [SerializeField]
   private Button btn_continue = null;

   [SerializeField]
   private Image _self = null;

   private int index = 0;

   private bool fadeIn = false, fadeOut = false;

   private bool nextScene = false;

   private bool allDone = false;

   public static bool AllDone = false;
   public static float AllDoneTime = 0.0f;
	// Use this for initialization
	void Start() 
   {
      if (txt_dialog == null)
         txt_dialog = gameObject.GetComponentInChildren<Text>();
      if (_self == null)
         _self = gameObject.GetComponent<Image>();

      txt_dialog.canvasRenderer.SetAlpha(0.0f);
      _self.canvasRenderer.SetAlpha(0.0f);

      txt_dialog.text = lines[index];

      Invoke("InitialFadeInP", 9.5f);

      btn_continue.interactable = false;
	}
	
	// Update is called once per frame
	void Update() 
   {
      if (fadeIn)
      {
         fadeIn = false;
         if (txt_dialog.canvasRenderer.GetAlpha() == 1.0f)
         {
            fadeOut = true;
            StartCoroutine(TextFadeOut(0.75f));
         }

         StartCoroutine(TextFadeIn(0.75f));
      }

      else if (allDone)
      {
         StartCoroutine(TextFadeOut(0.75f));
         StartCoroutine(PanelFadeOut(0.875f));
         allDone = false;
      }
	}

   public IEnumerator TextFadeIn(float alphaTime)
   {
      if (fadeOut)
      {
         yield return new WaitForSeconds(1.125f);
         fadeOut = false;
      }
      txt_dialog.text = lines[index];
      txt_dialog.CrossFadeAlpha(1.0f, alphaTime, false);
      yield return null;
   }

   public IEnumerator TextFadeOut(float alphaTime)
   {
      txt_dialog.CrossFadeAlpha(0.0f, alphaTime, false);
      yield return null;
   }

   public IEnumerator PanelFadeIn(float alphaTime)
   {
      _self.CrossFadeAlpha(1.0f, alphaTime, false);
      yield return new WaitForSeconds(alphaTime + 0.625f);
   }

   public IEnumerator PanelFadeOut(float alphaTime)
   {
      _self.CrossFadeAlpha(0.0f, alphaTime, false);
      yield return new WaitForSeconds(alphaTime + 0.625f);
   }

   public void ContinuePress()
   {


      if (index < 3)
      {
         index++;
         fadeIn = true;
      }
      else if (nextScene)
      {
         Application.LoadLevel(Application.loadedLevel + 1);
      }
      else
      {
         allDone = true;
         AllDone = true;
         AllDoneTime = Time.time;
         nextScene = true;
      }
 }
   public void InitialFadeInP()
   {
      StartCoroutine(PanelFadeIn(0.9f));
      Invoke("InitialFadeInT", 1.0f);
      
   }

   public void InitialFadeInT()
   {
      StartCoroutine(TextFadeIn(0.75f));
      btn_continue.interactable = true;
   }
}
