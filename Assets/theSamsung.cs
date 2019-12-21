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

    public KMSelectable homebutton;
    public KMSelectable[] appbuttons;

    private int currentappindex;

    public GameObject icons;
    public GameObject statusLight;
    public Texture[] volumeicons;
    public Texture[] miscicons;
    public Texture[] baticons;
    public Renderer batstatus;
    public Renderer volumestatus;
    public Renderer[] miscstatus;
    public Renderer phoneBackground;
    public Renderer phoneScreen;
    public Renderer[] apprenders;
    public Texture[] apptextures;
    public Color[] casingColors;
    public Texture[] wallpapers;
    public Transform[] iconpositions;
    private Vector3[] apppositions = new Vector3[9] { new Vector3(-.05f, .0124f, .05f), new Vector3(0f, .0124f, .05f), new Vector3(.05f, .0124f, .05f), new Vector3(-.05f, .0124f, 0f), new Vector3( 0f, .0124f, 0f), new Vector3(.05f, .0124f, 0f), new Vector3(-.05f, .0124f, -.05f), new Vector3(0f, .0124f, -.05f), new Vector3(.05f, .0124f, -.05f) };
    private float timeremaining;
    public Renderer solvedthingy;
    public Color solvedcolor;

    private float halfpoint;
    static int moduleIdCounter = 1;
    private int moduleId;
    private bool moduleSolved;

    void Awake()
    {
      moduleId = moduleIdCounter++;
      statusLight.SetActive(false);
      phoneBackground.material.color = casingColors.PickRandom();
      phoneScreen.material.mainTexture = wallpapers.PickRandom();
      volumestatus.material.mainTexture = volumeicons.PickRandom();
      var statusnumbers = Enumerable.Range(0,5).ToList();
      statusnumbers.Shuffle();
      foreach (Renderer statusicon in miscstatus)
        statusicon.material.mainTexture = miscicons[statusnumbers[Array.IndexOf(miscstatus, statusicon)]];
      homebutton.OnInteract += delegate () { PressHomeButton(); return false; };
      foreach (KMSelectable appbutton in appbuttons)
        appbutton.OnInteract += delegate () { PressAppButton(appbutton); return false; };
      var positionnumbers = Enumerable.Range(0,9).ToList();
      positionnumbers.Shuffle();
      foreach (Transform iconposition in iconpositions)
        iconposition.localPosition = apppositions[positionnumbers[Array.IndexOf(iconpositions, iconposition)]];
      foreach (Renderer app in apprenders)
        app.material.mainTexture = apptextures[Array.IndexOf(apprenders, app)];
    }

    void Start()
    {
      halfpoint = bomb.GetTime() / 2f;
      //homebutton.gameObject.SetActive(false);
    }

    void PressAppButton(KMSelectable button)
    {
      var ix = Array.IndexOf(appbuttons, button);
      Audio.PlaySoundAtTransform("keyClick", button.transform);
      icons.SetActive(false);
      homebutton.gameObject.SetActive(true);
    }

    void PressHomeButton()
    {
      Audio.PlaySoundAtTransform("keyClick", homebutton.transform);
      icons.SetActive(true);
      homebutton.gameObject.SetActive(false);
    }

    void Update()
    {
      timeremaining = bomb.GetTime();
      if (timeremaining > halfpoint || timeremaining == halfpoint)
        batstatus.material.mainTexture = baticons[0];
      else
        batstatus.material.mainTexture = baticons[1];
    }
}
