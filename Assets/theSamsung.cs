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

	// Settings
	public KMSelectable[] settingsbuttons;
	public KMSelectable clearbutton;
	public KMSelectable submitbutton;
	public TextMesh answertext;

	// Duolingo
	private int languageindex;
	private int[] duolingonumbers = new int[2];
	private int operatorindex;
	public TextMesh[] duolingotexts;
	public Renderer[] duolingotextenderers;
	public Font[] duolingofonts;
	public Material[] duolingofontmats;
	private static readonly string[] languagenames = new string[10] { "Spanish", "Italian", "Chinese", "French", "Afrikaans", "Swahili", "Japanese", "Korean", "Mongolian", "Khmer" };
	private static readonly string[] englishnumbernames = new string[21] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen", "twenty" };
	private static readonly string[] englishoperatornames = new string[4] { "plus", "minus", "times", "divided by" };

	// Authentiator
	public TextMesh[] authenticatortexts;
	public Transform authenticatorbar;

	// Spotify
	public KMSelectable playbutton;
	private bool isplaying;

	// Google Arts & Culture
	private static readonly int[][] artyears = new int[4][] {
		new int[8] { 1, 9, 4, 2, 1, 9, 9, 5, },
		new int[8] { 1, 8, 8, 1, 1, 9, 7, 3, },
		new int[8] { 1, 4, 5, 2, 1, 5, 1, 9, },
		new int[8] { 1, 8, 5, 3, 1, 8, 0, 0, }
	};
	private int artistindex;

	private int currentappindex;
	private int[] solution = new int[7];
	private string solutionstring = "";
	private List<string> enteredsolution = new List<string>();
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
    private Vector3[] apppositions = new Vector3[9] { new Vector3(-.05f, .0124f, .05f), new Vector3(0f, .0124f, .05f), new Vector3(.05f, .0124f, .05f), new Vector3(-.05f, .0124f, 0f), new Vector3(0f, .0124f, 0f), new Vector3(.05f, .0124f, 0f), new Vector3(-.05f, .0124f, -.05f), new Vector3(0f, .0124f, -.05f), new Vector3(.05f, .0124f, -.05f) };
    private float timeremaining;
    public Renderer solvedthingy;
    public Color solvedcolor;
	public Color strikecolor;
    public Light solvedlight;
    public Material[] ledcolors;
    public Renderer notificationlight;
    public Material notifoff;

    private Texture currentwallpaper;
	private List<int> positionnumbers = new List<int>();
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
        var statusnumbers = Enumerable.Range(0, 5).ToList();
        statusnumbers.Shuffle();
        // <Selectables>
        foreach (Renderer statusicon in miscstatus)
            statusicon.material.mainTexture = miscicons[statusnumbers[Array.IndexOf(miscstatus, statusicon)]];
        homebutton.OnInteract += delegate () { PressHomeButton(); return false; };
		playbutton.OnInteract += delegate () { PressPlayButton(); return false; };
        foreach (KMSelectable appbutton in appbuttons)
            appbutton.OnInteract += delegate () { PressAppButton(appbutton); return false; };
        foreach (KMSelectable button in settingsbuttons)
            button.OnInteract += delegate () { PressSettingsButton(button); return false; };
        clearbutton.OnInteract += delegate () { PressClearButton(); return false; };
        submitbutton.OnInteract += delegate () { PressSubmitButton(); return false; };
        // </Selectables>
		foreach (Renderer app in apprenders)
			app.material.mainTexture = apptextures[Array.IndexOf(apprenders, app)];
		positionnumbers = Enumerable.Range(0,9).Where(x => x != 4).ToList().Shuffle();
		for (int i = 0; i < 8; i++)
	  		iconpositions[i].localPosition = apppositions[positionnumbers[i]];
		iconpositions[8].localPosition = apppositions[4];
        solvedlight.enabled = false;
    }

    void Start()
    {
        float scalar = transform.lossyScale.x;
        solvedlight.range *= scalar;
        halfpoint = bomb.GetTime() / 2f;
        for (int i = 0; i < 7; i++)
            solution[i] = rnd.Range(0,10);
		// Duolingo
		languageindex = rnd.Range(0,10);
		duolingonumbers[0] = rnd.Range(0,21);
		operatorindex = rnd.Range(0,4);
		duolingonumbers[1] = rnd.Range(1,21);
		switch (operatorindex)
		{
			case 0:
				solution[0] = mod(((duolingonumbers[0] + duolingonumbers[1]) + languageindex + 1), 10);
				break;
			case 1:
				solution[0] = mod(((duolingonumbers[0] - duolingonumbers[1]) + languageindex + 1), 10);
				break;
			case 2:
				solution[0] = mod(((duolingonumbers[0] * duolingonumbers[1]) + languageindex + 1), 10);
				break;
			default:
				solution[0] = mod(((duolingonumbers[0] / duolingonumbers[1]) + languageindex + 1), 10);
				break;
		}
		foreach (TextMesh dtext in duolingotexts)
		{
			var ix = Array.IndexOf(duolingotexts, dtext);
			switch (languageindex)
			{
				case 6:
					dtext.font = duolingofonts[0];
					duolingotextenderers[ix].material = duolingofontmats[0];
					break;
				case 9:
					dtext.font = duolingofonts[0];
					duolingotextenderers[ix].material = duolingofontmats[0];
					break;
				case 8:
					dtext.font = duolingofonts[2];
					duolingotextenderers[ix].material = duolingofontmats[2];
					break;
				default:
					dtext.font = duolingofonts[1];
					duolingotextenderers[ix].material = duolingofontmats[1];
					break;
			}
			if (ix == 0)
				dtext.text = duolingo.numberwords[languageindex][duolingonumbers[0]];
			else if (ix == 1)
				dtext.text = duolingo.operatorwords[languageindex][operatorindex];
			else
				dtext.text = duolingo.numberwords[languageindex][duolingonumbers[1]];
		}
		// Google Arts & Culture
		artistindex = rnd.Range(0,4);
		switch (positionnumbers.IndexOf(6))
		{
			case 0:
				solution[6] = artyears[artistindex][0];
				break;
			case 1:
				solution[6] = artyears[artistindex][1];
				break;
			case 2:
				solution[6] = artyears[artistindex][2];
				break;
			case 3:
				solution[6] = artyears[artistindex][3];
				break;
			case 5:
				solution[6] = artyears[artistindex][4];
				break;
			case 6:
				solution[6] = artyears[artistindex][5];
				break;
			case 7:
				solution[6] = artyears[artistindex][6];
				break;
			default:
				solution[6] = artyears[artistindex][7];
				break;
		}
		// Solution
		var solutionlist = new List<int>();
		var nondiscordnumbers = positionnumbers.Where(x => );
		for (int i = 0; i < 7; i++)
			solutionlist.Add(solution[positionnumbers[i]]);
		int startingdirection;
		var ser = bomb.GetSerialNumber();
		if (Char.IsLetter(ser[0]) && Char.IsLetter(ser[1]))
			startingdirection = 0;
		else if (Char.IsLetter(ser[0]) && !Char.IsLetter(ser[1]))
			startingdirection = 2;
		else if (!Char.IsLetter(ser[0]) && Char.IsLetter(ser[1]))
			startingdirection = 4;
		else
			startingdirection = 6;
		if (bomb.GetPortCount(Port.Parallel) > 0)
			startingdirection--;
		else if (bomb.GetPortCount(Port.Serial) > 0)
			startingdirection++;
		if (startingdirection == 8 || startingdirection == -1)
			startingdirection = (startingdirection == 8 ? 0 : 7);
		var splice = solutionlist.Take(startingdirection).ToList();
		solutionlist = solutionlist.Skip(startingdirection + 1).ToList();
		solutionlist.AddRange(splice);
		//solutionstring = solution[0].ToString() + solution[1].ToString() + solution[2].ToString() + solution[3].ToString() + solution[4].ToString() + solution[5].ToString() + solution[6].ToString();
        Debug.LogFormat("[The Samsung #{0}] The solution is {1}.", moduleId, solutionstring);
        StartCoroutine(DisableStuff());
        StartCoroutine(Authenticator());
    }

    private IEnumerator DisableStuff()
    {
        yield return null;
        homebutton.gameObject.SetActive(false);
		foreach (GameObject app in apps)
			app.SetActive(false);
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

	void PressPlayButton()
	{
		if (isplaying)
			return;
		StartCoroutine(Spotify());
	}

	private IEnumerator Spotify()
	{
		isplaying = true;
		yield return new WaitForSeconds(.1f);
		isplaying = false;
	}

    void PressSettingsButton(KMSelectable button)
    {
        if (enteringstage > 6)
            return;
        Audio.PlaySoundAtTransform("keyClick", button.transform);
        var numbers = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
        enteredsolution.Add(numbers[Array.IndexOf(settingsbuttons, button)]);
        answertext.text += enteredsolution[enteringstage];
        enteringstage++;
    }

    void PressClearButton()
    {
        Audio.PlaySoundAtTransform("keyClick", clearbutton.transform);
        enteringstage = 0;
        enteredsolution.Clear();
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
			StartCoroutine(Strike());
        }
        else
        {
            Debug.LogFormat("[The Samsung #{0}] That was correct. Module solved!", moduleId);
            apps[8].SetActive(false);
            homebutton.gameObject.SetActive(false);
            phonescreen.material.mainTexture = wallpapers.Where(w => w != currentwallpaper).PickRandom();
            GetComponent<KMBombModule>().HandlePass();
            solvedthingy.material.color = solvedcolor;
            StopAllCoroutines();
            solvedlight.enabled = true;
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
            auth.text = Enumerable.Range(100000, 900000).Where(x => AuthCheck(x)).PickRandom().ToString();
        while (elapsed < duration)
        {
            authenticatorbar.localScale = new Vector3(Mathf.Lerp(.15f, 0f, elapsed / duration), .001f, .01f);
            yield return null;
            elapsed += Time.deltaTime;
        }
        goto restart;
    }

    private bool AuthCheck(int x)
    {
        if (solution[3] == 0)
            return (((x - 1) % 9) + 1) == 8;
        else if (solution[3] == 1)
            return (x % 3) % 2 == 0;
        else if (solution[3] == 2)
            return x % 7 == 0;
        else if (solution[3] == 3)
            return (x % 5) % 2 != 0;
        else if (solution[3] == 4)
            return ((((x - 1) % 9) + 1) == 3) || ((((x - 1) % 9) + 1) == 4);
        else if (solution[3] == 5)
            return x % 6 == 0;
        else if (solution[3] == 6)
            return (((x - 1) % 9) + 1) == 7;
        else if (solution[3] == 7)
            return x % 9 == 0;
        else if (solution[3] == 8)
            return (((x - 1) % 9) + 1) == 5;
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
        if (!flashing && rnd.Range(0, 10000) == 0)
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

	private IEnumerator Strike()
	{
		var defaultcolor = solvedthingy.material.color;
		solvedthingy.material.color = strikecolor;
		solvedlight.enabled = true;
		solvedlight.color = strikecolor;
		yield return new WaitForSeconds(.5f);
		solvedlight.enabled = false;
		solvedlight.color = solvedcolor;
		solvedthingy.material.color = defaultcolor;
	}

	private int mod(int x, int m)
	{
		return (x % m + m) % m;
	}
}
