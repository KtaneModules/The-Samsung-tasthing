using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using rnd = UnityEngine.Random;

public class theSamsung : MonoBehaviour
{
    public KMAudio Audio;
    public KMBombInfo bomb;

    public GameObject[] apps;
    public KMSelectable homebutton;
    public KMSelectable[] appbuttons;
    public KMSelectable[] settingsbuttons;
    public KMSelectable clearbutton;
    public KMSelectable submitbutton;
    public TextMesh answertext;
    public TextMesh[] authenticatortexts;
    public Transform authenticatorbar;

    private int currentappindex;
    private string[] solution = new string[7];
    private string[] enteredsolution = new string[7];
    private string solutionstring = "";
    private int enteringstage;

    public GameObject icons;
    public GameObject statusLight;
    public Texture[] appbackgrounds;
    public Texture[] volumeicons;
    public Texture[] miscicons;
    public Texture[] baticons;
    public Renderer batstatus;
    public Renderer volumestatus;
    public Renderer[] miscstatus;
    public Renderer phonebackground;
    public Renderer phonescreen;
    public Renderer[] apprenders;
    public Texture[] apptextures;
    public Color[] casingColors;
    public Texture[] wallpapers;
    public Transform[] iconpositions;
    private Vector3[] apppositions = new Vector3[9] { new Vector3(-.05f, .0124f, .05f), new Vector3(0f, .0124f, .05f), new Vector3(.05f, .0124f, .05f), new Vector3(-.05f, .0124f, 0f), new Vector3( 0f, .0124f, 0f), new Vector3(.05f, .0124f, 0f), new Vector3(-.05f, .0124f, -.05f), new Vector3(0f, .0124f, -.05f), new Vector3(.05f, .0124f, -.05f) };
    private float timeremaining;
    public Renderer solvedthingy;
    public Color solvedcolor;
    public Light solvedlight;
    public Material[] ledcolors;
    public Renderer notificationlight;
    public Material notifoff;

    private Texture currentwallpaper;
    private float halfpoint;
    private bool flashing;
    static int moduleIdCounter = 1;
    private int moduleId;
    private bool moduleSolved;

    void Awake()
    {
      moduleId = moduleIdCounter++;
      statusLight.SetActive(false);
      phonebackground.material.color = casingColors.PickRandom();
      currentwallpaper = wallpapers.PickRandom();
      phonescreen.material.mainTexture = currentwallpaper;
      volumestatus.material.mainTexture = volumeicons.PickRandom();
      var statusnumbers = Enumerable.Range(0,5).ToList();
      statusnumbers.Shuffle();
      // <Selectables>
      foreach (Renderer statusicon in miscstatus)
        statusicon.material.mainTexture = miscicons[statusnumbers[Array.IndexOf(miscstatus, statusicon)]];
      homebutton.OnInteract += delegate () { PressHomeButton(); return false; };
      foreach (KMSelectable appbutton in appbuttons)
        appbutton.OnInteract += delegate () { PressAppButton(appbutton); return false; };
      foreach (KMSelectable button in settingsbuttons)
        button.OnInteract += delegate () { PressSettingsButton(button); return false; };
      clearbutton.OnInteract += delegate () { PressClearButton(); return false; };
      submitbutton.OnInteract += delegate () { PressSubmitButton(); return false; };
      // </Selectables>
      var positionnumbers = Enumerable.Range(0,9).ToList();
      positionnumbers.Shuffle();
      foreach (Transform iconposition in iconpositions)
        iconposition.localPosition = apppositions[positionnumbers[Array.IndexOf(iconpositions, iconposition)]];
      foreach (Renderer app in apprenders)
        app.material.mainTexture = apptextures[Array.IndexOf(apprenders, app)];
      solvedlight.gameObject.SetActive(false);
    }

    void Start()
    {
      halfpoint = bomb.GetTime() / 2f;
      foreach (GameObject app in apps)
        app.SetActive(false);
      for (int i = 0; i < 7; i++)
        solution[i] = rnd.Range(0,10).ToString();
      solutionstring = solution[0] + solution[1] + solution[2] + solution[3] + solution[4] + solution[5] + solution[6];
      Debug.LogFormat("[The Samsung #{0}] The solution is {1}.", moduleId, solutionstring);
      StartCoroutine(ShowHomeButton());
      StartCoroutine(Authenticator());
    }

    private IEnumerator ShowHomeButton()
    {
      yield return null;
      homebutton.gameObject.SetActive(false);
    }

    void PressAppButton(KMSelectable button)
    {
      currentappindex = Array.IndexOf(appbuttons, button);
      Audio.PlaySoundAtTransform("keyClick", button.transform);
      icons.SetActive(false);
      homebutton.gameObject.SetActive(true);
      phonescreen.material.mainTexture = appbackgrounds[currentappindex];
      apps[currentappindex].SetActive(true);
    }

    void PressHomeButton()
    {
      Audio.PlaySoundAtTransform("keyClick", homebutton.transform);
      icons.SetActive(true);
      homebutton.gameObject.SetActive(false);
      phonescreen.material.mainTexture = currentwallpaper;
      apps[currentappindex].SetActive(false);
    }

    void PressSettingsButton(KMSelectable button)
    {
      if (enteringstage > 6)
        return;
      Audio.PlaySoundAtTransform("keyClick", button.transform);
      var numbers = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
      enteredsolution[enteringstage] = numbers[Array.IndexOf(settingsbuttons, button)];
      answertext.text += enteredsolution[enteringstage];
      enteringstage++;
    }

    void PressClearButton()
    {
      Audio.PlaySoundAtTransform("keyClick", clearbutton.transform);
      enteringstage = 0;
      answertext.text = "";
    }

    void PressSubmitButton()
    {
      Audio.PlaySoundAtTransform("keyClick", submitbutton.transform);
      var enteredsolutionstring = enteredsolution[0] + enteredsolution[1] + enteredsolution[2] + enteredsolution[3] + enteredsolution[4] + enteredsolution[5] + enteredsolution[6];
      Debug.LogFormat("[The Samsung #{0}] You submitted {1}.", moduleId, enteredsolution);
      if (enteredsolutionstring != solutionstring)
      {
        Debug.LogFormat("[The Samsung #{0}] That was incorrect. Strike!", moduleId);
        GetComponent<KMBombModule>().HandleStrike();
      }
      else
      {
        Debug.LogFormat("[The Samsung #{0}] That was correct. Module solved!", moduleId);
        apps[8].SetActive(false);
        homebutton.gameObject.SetActive(false);
        phonescreen.material.mainTexture = wallpapers.PickRandom();
        GetComponent<KMBombModule>().HandlePass();
        solvedthingy.material.color = solvedcolor;
        StopAllCoroutines();
        solvedlight.gameObject.SetActive(true);
      }
    }

    private IEnumerator Authenticator()
    {
      var elapsed = 0f;
      var duration = 6f;
      restart:
      elapsed = 0f;
      authenticatorbar.localScale = new Vector3(.15f, .001f, .01f);
      foreach (TextMesh auth in authenticatortexts)
        auth.text = Enumerable.Range(100000,900000).Where(x => AuthCheck(x)).PickRandom().ToString();
      while (elapsed < duration)
      {
        authenticatorbar.localScale = new Vector3(Mathf.Lerp(.15f, 0f, elapsed / duration), 0.001f, 0.01f);
        yield return null;
        elapsed += Time.deltaTime;
      }
      goto restart;
    }

    private bool AuthCheck(int x)
    {
      if (solution[3] == "0")
        return (((x - 1) % 9 ) + 1) == 8;
      else if (solution[3] == "1")
        return (x % 3) % 2 == 0;
      else if (solution[3] == "2")
        return x % 7 == 0;
      else if (solution[3] == "3")
        return (x % 5) % 2 != 0;
      else if (solution[3] == "4")
        return ((((x - 1) % 9 ) + 1) == 3) || ((((x - 1) % 9 ) + 1) == 4);
      else if (solution[3] == "5")
        return x % 6 == 0;
      else if (solution[3] == "6")
        return (((x - 1) % 9 ) + 1) == 7;
      else if (solution[3] == "7")
        return x % 9 == 0;
      else if (solution[3] == "8")
        return (((x - 1) % 9 ) + 1) == 5;
      else
        return x % 3 == 0;
    }

    void Update()
    {
      timeremaining = bomb.GetTime();
      if (timeremaining > halfpoint || timeremaining == halfpoint)
        batstatus.material.mainTexture = baticons[0];
      else
        batstatus.material.mainTexture = baticons[1];
      if (!flashing && rnd.Range(0,10000) == 0)
          StartCoroutine(FlashLed());
    }

    private IEnumerator FlashLed()
    {
      flashing = true;
      notificationlight.material = ledcolors.PickRandom();
      yield return new WaitForSeconds(1f);
      notificationlight.material = notifoff;
      flashing = false;
    }
}
