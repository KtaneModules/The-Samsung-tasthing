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
	private static readonly string[] eastereggs = new string[] { "43556629", "82784464", "36725463", "69", "420", "666" };

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

	// Photomath
	public GameObject hideable;
	public TextMesh photomathmaintext;
	public TextMesh photomathstartingtext;
	public TextMesh[] photmathkeypad;
	public KMSelectable photomathstart;
	public KMSelectable photomathsubmit;
	public KMSelectable photomathclear;
	public KMSelectable[] photomathbuttons;
	public Renderer[] photomathcircles;
	public Color[] photomathcolors;
	private List<Color> photomathusedcolors = new List<Color>();
	private static readonly Vector3[] startingvaluepositions = new Vector3[] { new Vector3(.074f, .0121f, -.0505f), new Vector3(-.074f, .0121f, -.0505f), new Vector3(-.074f, .0121f, .0267f), new Vector3(.074f, .0121f, .0267f) };
	private int startingvalue;
	private int[] operations = new int[7];
	private int[] values = new int[7];
	private List<int> photomathentered = new List<int>();
	private int photomathsolution;
	private bool photomathdone;
	private List<string> mathsymbols = new List<string>() { "Σ", "ℝ", "≜", "!", "δ", "∞", "⋰", "∝", "∴", "¬" };
	private static readonly int[][] keypadgrids = new int[10][] {
		new int[10] { 7, 3, 4, 6, 2, 0, 9, 8, 5, 1 },
		new int[10] { 3, 7, 1, 4, 6, 9, 8, 2, 5, 0 },
		new int[10] { 6, 2, 1, 5, 8, 3, 7, 0, 4, 9 },
		new int[10] { 5, 4, 6, 9, 3, 0, 2, 8, 7, 1 },
		new int[10] { 3, 1, 7, 4, 0, 8, 2, 6, 9, 5 },
		new int[10] { 8, 2, 6, 0, 9, 4, 7, 5, 3, 1 },
		new int[10] { 9, 3, 5, 4, 2, 1, 7, 8, 6, 0 },
		new int[10] { 7, 9, 6, 3, 1, 5, 0, 2, 4, 8 },
		new int[10] { 0, 4, 8, 7, 5, 3, 1, 2, 9, 6 },
		new int[10] { 2, 5, 4, 8, 6, 3, 9, 0, 1, 7 }
 	};

	// Spotify
	public KMSelectable playbutton;
	private static readonly string[] adnames = new string[5] { "ad1", "ad2", "ad3", "ad4", "ad5" };
	private static readonly float[] adlengths = new float[5] { 13f, 18.5f, 15.25f, 14f, 17.5f };
	private static readonly string[] songnames = new string[9] { "rightround", "smoothcriminal", "hardwarestore", "beatit", "dangerzone", "tacky", "harderbetterfasterstronger", "drunkensailor", "megalovania" };
	private static readonly float[] songlengths = new float[9] { 7.5f, 7.5f, 30.5f, 8.5f, 5.5f, 6.5f, 5.5f, 8.5f, 8.5f };
	private static readonly string[] decoynames = new string[10] { "decoy1", "decoy2", "decoy3", "decoy4", "decoy5", "decoy6", "decoy7", "decoy8", "decoy9", "decoy10" };
	private static readonly float[] decoylengths = new float[10] { 7.5f, 8.5f, 6.5f, 8.5f, 9.5f, 6.5f, 9.5f, 6.5f, 8.5f, 8.5f, };
	private int decoyindex;
	private bool isplaying;

	// Google Arts & Culture
	private int artistindex;
	private static readonly int[][] artyears = new int[4][] {
		new int[8] { 1, 9, 4, 2, 1, 9, 9, 5, },
		new int[8] { 1, 8, 8, 1, 1, 9, 7, 3, },
		new int[8] { 1, 4, 5, 2, 1, 5, 1, 9, },
		new int[8] { 1, 8, 5, 3, 1, 8, 0, 0, }
	};
	private static readonly string[] artistnames = new string[5] { "Bob Ross", "Picasso", "Da Vinci", "Van Gogh", "Misc artist" };
	public Renderer painting;
	public TextMesh artisttext;
	public Texture[] bobross;
	public Texture[] picasso;
	public Texture[] davinci;
	public Texture[] vangogh;
	public Texture[] otherpaintings;
	private List<Texture[]> paintings = new List<Texture[]>();

	// Discord
	public GameObject call;
	public Renderer callpfp;
	public GameObject pfps;
	public GameObject greencircle;
	public Texture[] pfpimages;
	public Transform[] pfppositions;
	public KMSelectable[] pfpbuttons;
	public KMSelectable mutebutton;
	public Renderer[] pfprenders;
	public Renderer cyclingsymbol;
	public Texture[] symbol1;
	public Texture[] symbol2;
	public Texture[] symbol3;
	public Texture[] symbol4;
	public Texture[] symbol5;
	public Texture[] symbol6;
	public Texture[] symbol7;
	public Texture[] symbol8;
	private List<Texture[]> allsymbols = new List<Texture[]>();
	private User[] users = new User[6];
	private int discordstage;
	private int person1;
	private int person2;
	private int discordactivity;
	private int discordsymbol;
	private int discordcolor;
	private int[] extremes = new int[4];
	private bool cantleave;
	private bool speaking;
	private bool leavingbecausestrike;
	private bool endofdiscord;
	private bool cycling;
	private int currentsymbol;
	private int currentcolor;
	private int currentixuser;
	private int currentixbutton;
	private static readonly string[] discordnames = new string[10] { "TasThing", "Deaf", "Blananas", "Timwi", "Numdegased", "Zefod", "Espik", "Procyon", "eXish", "SillyPuppy" };
	private static readonly string[][] checknames = new string[4][] {
		new string[10] { "TasThing", "Blananas", "Numdegased", "Espik", "eXish", "SillyPuppy", "Deaf", "Timwi", "Zefod", "Procyon" },
		new string[10] { "Timwi", "eXish", "SillyPuppy", "Zefod", "Procyon", "Numdegased", "Deaf", "Espik", "Blananas", "TasThing" },
		new string[10] { "Deaf", "Procyon", "Espik", "Zefod", "Blananas", "TasThing", "Timwi", "eXish", "SillyPuppy", "Numdegased" },
		new string[10] { "Blananas", "TasThing", "Timwi", "Numdegased", "eXish", "Espik", "Procyon", "Zefod", "Deaf", "SillyPuppy" }
	};
	private static readonly string[] busyexcuses = new string[10] { "she's not really into you...", "he's being himself.", "he's busy modding.", "he's at the badminton club.", "numexcuse", "zefodexcuse", "espikexcuse", "procyonexcuse", "exishexcuse", "he's in the middle of something. (or someone?)"  };

	private int currentappindex;
	private int[] solution = new int[8];
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
	public TextMesh timetext;
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
		foreach (Renderer statusicon in miscstatus)
			statusicon.material.mainTexture = miscicons[statusnumbers[Array.IndexOf(miscstatus, statusicon)]];
        // <Selectables>
        homebutton.OnInteract += delegate () { PressHomeButton(); return false; };
		playbutton.OnInteract += delegate () { PressPlayButton(); return false; };
        foreach (KMSelectable appbutton in appbuttons)
            appbutton.OnInteract += delegate () { PressAppButton(appbutton); return false; };
        foreach (KMSelectable button in settingsbuttons)
            button.OnInteract += delegate () { PressSettingsButton(button); return false; };
		photomathclear.OnInteract += delegate () { PressPhotomathClearButton(); return false; };
		photomathsubmit.OnInteract += delegate () { PressPhotomathSubmitButton(); return false; };
		photomathstart.OnInteract += delegate () { StartCoroutine(PhotomathCycle()); return false;};
		foreach (KMSelectable button in photomathbuttons)
			button.OnInteract += delegate () { PressPhotomathButton(button); return false; };
		foreach (KMSelectable button in pfpbuttons)
			button.OnInteract += delegate () { PressPfpButton(button, Array.IndexOf(pfpbuttons, button)); return false; };
		mutebutton.OnInteract += delegate () { PressMuteButton(); return false; };
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
        for (int i = 0; i < 8; i++)
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
		Debug.LogFormat("[The Samsung #{0}] DUOLINGO:", moduleId);
		Debug.LogFormat("[The Samsung #{0}] The language present is {1}.", moduleId, languagenames[languageindex]);
		Debug.LogFormat("[The Samsung #{0}] The expression is {1} {2} {3}.", moduleId, englishnumbernames[duolingonumbers[0]], englishoperatornames[operatorindex], englishnumbernames[duolingonumbers[1]]);
		Debug.LogFormat("[The Samsung #{0}] The solution for Duolingo is {1}.", moduleId, solution[0]);
		// Authenticator
		string[] conditionnames = new string[10] { "a digital root of 8", "an even result when modulod by 3", "division by 7", "an odd result when modulod by 5", "a digital root of 3 or 4", "division by 6", "a digital root of 7", "division by 9", "a digital root of 5", "division by 3" };
		Debug.LogFormat("[The Samsung #{0}] GOOGLE AUTHENTICATOR:", moduleId);
		Debug.LogFormat("[The Samsung #{0}] Every number shown has {1}.", moduleId, conditionnames[solution[3]]);
		Debug.LogFormat("[The Samsung #{0}] Therefore, the solution for Google Authenticator is {1}.", moduleId, solution[3]);
		// Photomath
		mathsymbols.Shuffle();
		startingvalue = rnd.Range(1,10);
		photomathusedcolors = photomathcolors.ToList().Shuffle();
		for (int i = 0; i < 4; i++)
			photomathcircles[i].material.color = photomathusedcolors[i];
		for (int i = 0; i < 7; i++)
		{
			operations[i] = rnd.Range(0,4);
			values[i] = rnd.Range(1,10);
		}
		for (int i = 0; i < 7; i++)
		{
			var x = i == 0 ? startingvalue : photomathsolution;
			switch (operations[i])
			{
				case 0:
					photomathsolution += x;
					break;
				case 1:
					photomathsolution -= x;
					break;
				case 2:
					photomathsolution *= x;
					break;
				default:
					if (x == 0)
						x = 1;
					photomathsolution /= x;
					break;
			}
		}
		if (photomathsolution < 0)
			photomathsolution *= -1;
		photomathsolution += operations.Count(x => x == 0 || x == 2);
		for (int i = 0; i < 10; i++)
			photmathkeypad[i].text = mathsymbols[keypadgrids[bomb.GetSerialNumberNumbers().Last()][i]];
		string[] colornames = new string[4] { "blue", "purple", "green", "yellow" };
		Debug.LogFormat("[The Samsung #{0}] PHOTOMATH:", moduleId);
		Debug.LogFormat("[The Samsung #{0}] The circles on the bottom are colored {1}, {2}, {3}, and then {4}.", moduleId, colornames[Array.IndexOf(photomathcolors, photomathusedcolors[0])], colornames[Array.IndexOf(photomathcolors, photomathusedcolors[1])], colornames[Array.IndexOf(photomathcolors, photomathusedcolors[2])], colornames[Array.IndexOf(photomathcolors, photomathusedcolors[3])]);
		for (int i = 0; i < 10; i++)
		Debug.LogFormat("[The Samsung #{0}] {1} corresponds to {2}.", moduleId, mathsymbols[i], i);
		Debug.LogFormat("[The Samsung #{0}] The solution for Photomath is {1}.", moduleId, photomathsolution);
		// Spotify
		decoyindex = rnd.Range(0,10);
		string[] songnames = new string[] { "You Spin Me Right Round", "Smooth Criminal", "Hardware Store", "Beat It", "Danger Zone", "Tacky", "Harder, Better, Faster, Stronger", "Drunken Sailor", "Megalovania", "a song not mentioned" };
		Debug.LogFormat("[The Samsung #{0}] SPOTIFY:", moduleId);
		Debug.LogFormat("[The Samsung #{0}] The song being played is {1}, so the solution for Spotify is {2}.", moduleId, songnames[solution[5]], solution[5]);
		// Google Arts & Culture
		paintings = new List<Texture[]> { bobross, picasso, davinci, vangogh };
		artistindex = rnd.Range(0,5);
		int gacsolution;
		bool lying = rnd.Range(0,2) == 0;
		int paintingindex = rnd.Range(0,5);
		if (artistindex == 4)
		{
			gacsolution = bomb.GetSerialNumber()[5] - '0';
			painting.material.mainTexture = otherpaintings.PickRandom();
		}
		else
		{
			switch (positionnumbers[6])
			{
				case 0:
					gacsolution = artyears[artistindex][0];
					break;
				case 1:
					gacsolution = artyears[artistindex][1];
					break;
				case 2:
					gacsolution = artyears[artistindex][2];
					break;
				case 3:
					gacsolution = artyears[artistindex][3];
					break;
				case 5:
					gacsolution = artyears[artistindex][4];
					break;
				case 6:
					gacsolution = artyears[artistindex][5];
					break;
				case 7:
					gacsolution = artyears[artistindex][6];
					break;
				default:
					gacsolution = artyears[artistindex][7];
					break;
			}
			var n1 = new int[] { 0, 2, 4, 6, 8 };
			var n2 = new int[] { 1, 3, 5, 7, 9 };
			gacsolution += (!lying ? n1[paintingindex] : n2[paintingindex]);
			gacsolution %= 10;
			painting.material.mainTexture = paintings[artistindex][paintingindex];
		}
		artisttext.text = !lying ? artistnames[artistindex] : artistnames.Where(x => Array.IndexOf(artistnames, x) != artistindex).PickRandom();
		Debug.LogFormat("[The Samsung #{0}] GOOGLE ARTS & CULTURE:", moduleId);
		if (artistindex != 4)
			Debug.LogFormat("[The Samsung #{0}] The art is by {1}. It corresponds to the digit {2}, and the text is {3}.", moduleId, artistnames[artistindex], paintingindex, !lying ? "n't lying" : " lying");
		else
			Debug.LogFormat("[The Samsung #{0}] This art is by some other artist. Use the last digit of the serial number.", moduleId);
		Debug.LogFormat("[The Samsung #{0}] The solution for Google A&C is {1}.", moduleId, gacsolution);
		solution[6] = gacsolution;
		// Discord
		allsymbols = new List<Texture[]> { symbol1, symbol2, symbol3, symbol4, symbol5, symbol6, symbol7, symbol8 };
		cyclingsymbol.gameObject.SetActive(false);
		call.SetActive(false);
		greencircle.SetActive(false);
		discordtryagain:
		var usernumbers = Enumerable.Range(0,10).ToList().Shuffle();
		var discordnumbers = Enumerable.Range(0,16).ToList().Shuffle();
		var xfs = new float[4] { -.057f, -.0191f, .0188f, .0567f };
		var yfs = new float[4] { .0462f, .0083f, -.0296f, -.0675f };
		for (int i = 0; i < 6; i++)
		{
			users[i] = new User { id = i, positionnumber = discordnumbers[i], userid = usernumbers[i], username = discordnames[usernumbers[i]], x = 0f, z = 0f };
			pfppositions[i].localPosition = new Vector3(xfs[discordnumbers[i] % 4], .0123f, yfs[discordnumbers[i] / 4]);
			users[i].x = pfppositions[i].localPosition.x;
			users[i].z = pfppositions[i].localPosition.z;
			pfprenders[i].material.mainTexture = pfpimages[usernumbers[i]];
		}
		List<User>[] nonselves = new List<User>[6];
		for (int i = 0; i < 6; i++)
			nonselves[i] = users.Where(u => u != users[i]).ToList();
		extremes[0] = Array.IndexOf(users, users.Where(u => nonselves[Array.IndexOf(users, u)].Any(uu => uu.z != u.z)).OrderBy(u => u.z).Last());
		extremes[1] = Array.IndexOf(users, users.Where(u => nonselves[Array.IndexOf(users, u)].Any(uu => uu.x != u.x)).OrderBy(u => u.x).Last());
		extremes[2] = Array.IndexOf(users, users.Where(u => nonselves[Array.IndexOf(users, u)].Any(uu => uu.z != u.z)).OrderBy(u => u.z).First());
		extremes[3] = Array.IndexOf(users, users.Where(u => nonselves[Array.IndexOf(users, u)].Any(uu => uu.x != u.x)).OrderBy(u => u.x).First());
		for (int i = 0; i < 4; i++)
			if (nonselves[i].Any(u => ((i == 0 || i ==2) ? u.z : u.x)  == ((i == 0 || i == 2) ? users[extremes[i]].z : users[extremes[i]].x)))
				goto discordtryagain;
		Debug.LogFormat("[The Samsung #{0}] DISCORD:", moduleId);
		switch (Braille(users.Select(u => u.positionnumber).ToArray()))
		{
			case "A":
				person1 = extremes[0];
				Debug.LogFormat("[The Samsung #{0}] The 2x3 in the top-left spells out a Braille letter in set {1}.", moduleId, "A");
				break;
			case "B":
				person1 = extremes[1];
				Debug.LogFormat("[The Samsung #{0}] The 2x3 in the top-left spells out a Braille letter in set {1}.", moduleId, "B");
				break;
			case "C":
				person1 = extremes[2];
				Debug.LogFormat("[The Samsung #{0}] The 2x3 in the top-left spells out a Braille letter in set {1}.", moduleId, "C");
				break;
			case "D":
				person1 = extremes[3];
				Debug.LogFormat("[The Samsung #{0}] The 2x3 in the top-left spells out a Braille letter in set {1}.", moduleId, "D");
				break;
			case "E":
				person1 = bomb.GetSerialNumberLetters().Count(x => "AEIOU".Contains(x)) == 0 ? extremes[2] : extremes[1];
				Debug.LogFormat("[The Samsung #{0}] The 2x3 in the top-left does not spell out a Braille letter..", moduleId);
				break;
			default:
				person1 = bomb.GetPortPlates().Any(x => x.Length == 0) ? extremes[0] : extremes[3];
				Debug.LogFormat("[The Samsung #{0}] DISCORD:", moduleId);
				break;
		}
		string username2 = checknames[Array.IndexOf(extremes, person1)].Where(s => users.Select(u => u.username).ToArray().Contains(s) && !users.Where(u => u.username == s).Select(u => u.id).ToList().Contains(person1)).First();
		person2 = users.Where(u => u.username == username2).First().id;
		discordactivity = rnd.Range(0,10);
		discordcolor = rnd.Range(0,6);
		discordsymbol = rnd.Range(0,8);
		currentsymbol = rnd.Range(0,8);
		currentcolor = rnd.Range(0,6);
		string[] activitynames = new string[10] { "defusing", "playing Jackbox", "playing Tabletop Simulator", "reacting to a new mod", "complaining about sleep", "experting", "arguing", "talking about food", "being AFK", "something else" };
		string[] discordcolornames = new string[6] { "red", "orange", "yellow", "green", "blue", "purple" };
		string[] extremenames = new string[4] { "top-most", "right-most", "bottom-most", "left-most" };
		Debug.LogFormat("[The Samsung #{0}] The first user to call is {1}, because they are the {2} user.", moduleId, users[person1].username, extremenames[Array.IndexOf(extremes, extremes.Where(x => x == person1).First())]);
		Debug.LogFormat("[The Samsung #{0}] The second user to call is {1}.", moduleId, users[person2].username);
		Debug.LogFormat("[The Samsung #{0}] The activity is {1}, which corresponds to {2}.", moduleId, activitynames[discordactivity], discordactivity);
		Debug.LogFormat("[The Samsung #{0}] The correct symbol is symbol {1}.", moduleId, discordsymbol + 1);
		Debug.LogFormat("[The Samsung #{0}] The correct color is {1}.", moduleId, discordcolornames[discordcolor]);
		Debug.LogFormat("[The Samsung #{0}] The solution for Discord is {1}.", moduleId, solution[7]);
		// Solution
		string[] directionnames = new string[8] { "north-west", "north", "north-east", "east", "south-east", "south", "south-west", "west" };
		Debug.LogFormat("[The Samsung #{0}] SETTINGS:", moduleId);
		int startingoffset;
		var ser = bomb.GetSerialNumber();
		if (Char.IsLetter(ser[0]) && Char.IsLetter(ser[1]))
			startingoffset = 1;
		else if (Char.IsLetter(ser[0]) && !Char.IsLetter(ser[1]))
			startingoffset = 3;
		else if (!Char.IsLetter(ser[0]) && Char.IsLetter(ser[1]))
			startingoffset = 5;
		else
			startingoffset = 7;
		Debug.LogFormat("[The Samsung #{0}] The initial direction is, at first, {1}.", moduleId, directionnames[startingoffset]);
		if (bomb.GetPortCount(Port.Parallel) > 0)
			startingoffset--;
		else if (bomb.GetPortCount(Port.Serial) > 0)
			startingoffset++;
		Debug.LogFormat("[The Samsung #{0}] After modifications, the initial direction is {1}.", moduleId, directionnames[startingoffset]);
		//
		var solutionlist = new List<int>();
		var clockwiseorder = new int[] { 0, 1, 2, 5, 8, 7, 6, 3 };
		for (int i = 0; i < 8; i++)
			solutionlist.Add(solution[positionnumbers.IndexOf(clockwiseorder[(i + startingoffset) % clockwiseorder.Length])] );
		solutionstring = solutionlist.Join("");
		Debug.LogFormat("[The Samsung #{0}] The module's solution is {1}.", moduleId, solutionstring);
        StartCoroutine(DisableStuff());
        StartCoroutine(Authenticator());
    }

    private IEnumerator DisableStuff()
    {
        yield return null;
        homebutton.gameObject.SetActive(false);
		foreach (GameObject app in apps)
			app.SetActive(false);
		hideable.SetActive(false);
		photomathmaintext.text = "";
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
		if (cantleave)
			return;
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
		var adix = rnd.Range(0,5);
		Audio.PlaySoundAtTransform(adnames[adix], playbutton.transform);
		yield return new WaitForSeconds(adlengths[adix]);
		yield return new WaitForSeconds(.5f);
		if (solution[5] == 9)
		{
			Audio.PlaySoundAtTransform(decoynames[decoyindex], playbutton.transform);
			yield return new WaitForSeconds(decoylengths[decoyindex]);
		}
		else
		{
			Audio.PlaySoundAtTransform(songnames[solution[5]], playbutton.transform);
			yield return new WaitForSeconds(songlengths[solution[5]]);
		}
		isplaying = false;
	}

	void PressPhotomathClearButton()
	{
		Audio.PlaySoundAtTransform("keyClick", photomathclear.transform);
		photomathentered.Clear();
	}

	void PressPhotomathSubmitButton()
	{
		Audio.PlaySoundAtTransform("keyClick", photomathsubmit.transform);
		var photomathans = photomathentered.Join("");
		if (photomathans != photomathsolution.ToString())
		{
			Debug.LogFormat("[The Samsung #{0}] You submitted {1} on Photomath. That was incorrect. Strike!", moduleId, photomathans);
			StartCoroutine(Strike());
		}
		else
		{
			hideable.SetActive(false);
			photomathstart.gameObject.SetActive(false);
			photomathmaintext.text = mathsymbols[solution[4]];
			photomathmaintext.color = photomathcolors[2];
		}
	}

	void PressPhotomathButton(KMSelectable button)
	{
		Audio.PlaySoundAtTransform("keyClick", button.transform);
		photomathentered.Add(keypadgrids[bomb.GetSerialNumberNumbers().Last()][Array.IndexOf(photomathbuttons, button)]);
	}

	private IEnumerator PhotomathCycle()
	{
		Audio.PlaySoundAtTransform("keyClick", photomathstart.transform);
		var show = rnd.Range(0,7);
		photomathstartingtext.transform.localPosition = startingvaluepositions.PickRandom();
		photomathstart.gameObject.SetActive(false);
		hideable.SetActive(false);
		for (int i = 0; i < 7; i++)
		{
			photomathmaintext.text = mathsymbols[values[i]];
			photomathmaintext.color = photomathusedcolors[operations[i]];
			if (i == show)
				photomathstartingtext.text = mathsymbols[startingvalue];
			else
				photomathstartingtext.text = "";
			Audio.PlaySoundAtTransform("bass", photomathmaintext.transform);
			yield return new WaitForSeconds(.7f);
		}
		photomathmaintext.text = "";
		photomathstartingtext.text = "";
		photomathstart.gameObject.SetActive(true);
		hideable.SetActive(true);
	}

	void PressPfpButton(KMSelectable button, int ix)
	{
		callpfp.material.mainTexture = pfpimages[users[ix].userid];
		call.SetActive(true);
		pfps.SetActive(false);
		Audio.PlaySoundAtTransform("join", button.transform);
		StartCoroutine(DiscordVoice(users[ix].userid, ix));
	}

	private IEnumerator DiscordVoice(int ixuser, int ixbutton)
	{
		currentixuser = ixuser;
		currentixbutton = ixbutton;
		cantleave = true;
		speaking = true;
		greencircle.SetActive(true);
		yield return new WaitForSeconds(.75f);
		if (discordstage == 0)
		{
			if (ixbutton != person1)
			{
				Audio.PlaySoundAtTransform(Discord.busylinenames[ixuser], callpfp.transform);
				yield return new WaitForSeconds(10f); // TEMPORARY, to be set to length of sound clip in array.
				Debug.LogFormat("[The Samsung #{0}] You called {1}, but {2} Strike!", moduleId, discordnames[ixuser], busyexcuses[ixuser]);
				StartCoroutine(Strike());
				cantleave = false;
				speaking = false;
				leavingbecausestrike = true;
				PressLeaveButton();
			}
			else
			{
				Audio.PlaySoundAtTransform(Discord.activitylinenames[ixuser][discordactivity], callpfp.transform);
				yield return new WaitForSeconds(10f); // TEMPORARY, to be set to length of sound clip in array.
			}
		}
		else if (discordstage == 1)
		{
			Audio.PlaySoundAtTransform(Discord.symbollinenames[ixuser][discordsymbol], callpfp.transform);
			yield return new WaitForSeconds(10f); // TEMPORARY, to be set to length of sound clip in array.
			greencircle.SetActive(false);
			yield return new WaitForSeconds(.75f);
			greencircle.SetActive(true);
			Audio.PlaySoundAtTransform(Discord.colorlinenames[ixuser][discordcolor], callpfp.transform);
			yield return new WaitForSeconds(10f); // TEMPORARY, to be set to length of sound clip in array.
			speaking = false;
			cantleave = false;
			PressLeaveButton();
			discordstage++;
		}
		else if (discordstage == 2)
		{
			if (ixbutton != person2)
			{
				Audio.PlaySoundAtTransform(Discord.busylinenames[ixuser], callpfp.transform);
				yield return new WaitForSeconds(10f); // TEMPORARY, to be set to length of sound clip in array
				Debug.LogFormat("[The Samsung #{0}] You called {1}, but {2} Strike!", moduleId, discordnames[ixuser], busyexcuses[ixuser]);
				StartCoroutine(Strike());
				cantleave = false;
				speaking = false;
				leavingbecausestrike = true;
				PressLeaveButton();
			}
			else
			{
				cycling = true;
				StartCoroutine(SymbolCycle());
			}
		}
		else if (discordstage == 4)
		{
			Audio.PlaySoundAtTransform(Discord.digitlinenames[ixuser][solution[7]], callpfp.transform);
			yield return new WaitForSeconds(10f); // TEMPORARY, to be set to length of sound clip in array.
			cycling = false;
			endofdiscord = true;
			cantleave = false;
			PressLeaveButton();
		}
		speaking = false;
		greencircle.SetActive(false);
	}

	private IEnumerator SymbolCycle()
	{
		cantleave = true;
		cyclingsymbol.material.mainTexture = allsymbols[currentsymbol][currentcolor];
		cyclingsymbol.gameObject.SetActive(true);
		while (discordstage == 2 && cycling)
		{
			currentsymbol = (currentsymbol + 1) % 8;
			cyclingsymbol.material.mainTexture = allsymbols[currentsymbol][currentcolor];
			yield return new WaitForSeconds(1f);
		}
		yield return new WaitForSeconds(1.5f);
		while (cycling)
		{
			currentcolor = (currentcolor + 1) % 6;
			cyclingsymbol.material.mainTexture = allsymbols[currentsymbol][currentcolor];
			yield return new WaitForSeconds(1f);
		}
	}

	void PressLeaveButton()
	{
		if (cantleave)
			return;
		greencircle.SetActive(false);
		cantleave = false;
		call.SetActive(false);
		pfps.SetActive(true);
		Audio.PlaySoundAtTransform(leavingbecausestrike ? "leave" : "disconnect", callpfp.transform);
		leavingbecausestrike = false;
		if (endofdiscord)
			StartCoroutine(HideDiscord());
	}

	void PressMuteButton()
	{
		if (speaking)
			return;
		if (discordstage == 0)
		{
			if (!(((int) bomb.GetTime()) % 10 == discordactivity))
			{
				StartCoroutine(Strike());
			}
			else
			{
				discordstage++;
				StartCoroutine(DiscordVoice(currentixuser, currentixbutton));
			}
		}
		else if (discordstage == 2)
		{
			if (currentsymbol != discordsymbol)
				StartCoroutine(Strike());
			else
			{
				discordstage++;
				StartCoroutine(SymbolCycle());
			}
		}
		else if (discordstage == 3)
		{
			if (currentcolor != discordcolor)
				StartCoroutine(Strike());
			else
			{
				cantleave = false;
				cycling = false;
				discordstage++;
				StartCoroutine(DiscordVoice(currentixuser, currentixbutton));
			}
		}
	}

	private IEnumerator HideDiscord()
	{
		var unhiddenicons = Enumerable.Range(0,6).ToList().Shuffle();
		for (int i = 0; i < 6; i++)
		{
			pfprenders[unhiddenicons[i]].gameObject.SetActive(false);
			Audio.PlaySoundAtTransform("pop", pfprenders[unhiddenicons[i]].transform);
			yield return new WaitForSeconds(.1f);
		}
	}

    void PressSettingsButton(KMSelectable button)
    {
        if (enteringstage > 7)
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
        var enteredsolutionstring = enteredsolution.Join("");
        Debug.LogFormat("[The Samsung #{0}] You submitted {1}.", moduleId, enteredsolution);
        if (enteredsolutionstring == solutionstring)
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
		else if (eastereggs.Any(x => x == enteredsolutionstring))
		{
			var ix = Array.IndexOf(eastereggs, enteredsolutionstring);
			Debug.LogFormat("[The Samsung #{0}] You found easter egg {1}.", moduleId, ix + 1);
		}
		else
		{
            Debug.LogFormat("[The Samsung #{0}] That was incorrect. Strike!", moduleId);
			StartCoroutine(Strike());
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

	private class User
	{
		public int id;
		public int positionnumber;
		public int userid;
		public string username;
		public float x;
		public float z;
	}

	private string Braille(int[] g)
	{
		bool[] truthgrid = new bool[6];
		int[] checkgrid = new int[6] { 0, 4, 8, 1, 5, 9 };
		for (int i = 0; i < 6; i++)
			if (g.Any(ix => ix == checkgrid[i]))
				truthgrid[i]= true;
		if (BrailleArrays.brailleletters[0].Any(a => a.SequenceEqual(truthgrid)))
			return "A";
		else if (BrailleArrays.brailleletters[1].Any(a => a.SequenceEqual(truthgrid)))
			return "B";
		else if (BrailleArrays.brailleletters[2].Any(a => a.SequenceEqual(truthgrid)))
			return "C";
		else if (BrailleArrays.brailleletters[3].Any(a => a.SequenceEqual(truthgrid)))
			return "D";
		else if (BrailleArrays.brailleletters[4].Any(a => a.SequenceEqual(truthgrid)))
			return "E";
		else
			return "F";
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
		timetext.text = DateTime.Now.ToString("hh:mm");
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
		GetComponent<KMBombModule>().HandleStrike();
		var defaultcolor = solvedthingy.material.color;
		solvedthingy.material.color = strikecolor;
		solvedlight.enabled = true;
		solvedlight.color = strikecolor;
		yield return new WaitForSeconds(.5f);
		solvedlight.enabled = false;
		solvedlight.color = solvedcolor;
		solvedthingy.material.color = defaultcolor;
	}

	void ShowEverything()
	{

	}

	void HideEverything()
	{

	}

	private int mod(int x, int m)
	{
		if (x < 0)
			x *= -1;
		return x % m;
	}
}
