using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using rnd = UnityEngine.Random;
using System.Text.RegularExpressions;

public class theSamsung : MonoBehaviour
{
    public new KMAudio audio;
    public KMBombInfo bomb;
    public KMBombModule module;

    public GameObject[] apps;
    public KMSelectable homebutton;
    public KMSelectable[] appButtons;
    public KMSelectable resetButton;

    // Settings
    public KMSelectable[] settingsbuttons;
    public KMSelectable clearbutton;
    public KMSelectable submitbutton;
    public TextMesh answertext;
    public TextMesh strikeText;
    private static readonly string[] easterEggs = new string[] { "43556629", "82784464", "26725463", "69", "420", "666", "177013" };
    private static readonly float[] easterEggLengths = new float[] { 8.5f, 7.5f, 9.5f, 2.5f, 3.5f, 5.5f, 4.5f };
    private bool easterEgging;
    private bool strikeAnimating;

    // Duolingo
    private int languageIndex;
    private int[] duolingoNumbers = new int[2];
    private int operatorIndex;
    public TextMesh[] duolingotexts;
    public Renderer[] duolingotextenderers;
    public Font[] duolingofonts;
    public Material[] duolingofontmats;
    private static readonly string[] languageNames = new string[10] { "Spanish", "Italian", "Chinese", "French", "Afrikaans", "Swahili", "Japanese", "Korean", "Mongolian", "Thai" };
    private static readonly string[] englishNumberNames = new string[21] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen", "twenty" };
    private static readonly string[] englishOperatorNames = new string[4] { "plus", "minus", "times", "divided by" };

    // Google Maps
    public TextMesh[] gmapstext1;
    public TextMesh[] gmapstext2;
    private int country1;
    private int country2;
    private static readonly float[][] latCords = new float[10][] {
        new float[2] { 32.886970f, 40.353244f },
        new float[2] { 50.007742f, 64.623881f },
        new float[2] { 18.554616f, 25.459147f },
        new float[2] { 56.218924f, 65.425386f },
        new float[2] { 49.336579f, 52.972463f },
        new float[2] { -31.079284f, -20.739675f },
        new float[2] { 50.836475f, 53.035930f },
        new float[2] { 31.428669f, 39.707192f },
        new float[2] { -21.518496f, -5.633482f },
        new float[2] { -33.339706f, -29.531405f }
    };
    private static readonly float[][] lonCords = new float[10][] {
        new float[2] { -116.121098f, -81.843754f },
        new float[2] { -122.115239f, -97.417976f },
        new float[2] { -103.130860f, -98.455078f },
        new float[2] { 40.464846f, 128.988279f },
        new float[2] { 7.470706f, 11.610353f },
        new float[2] { 119.950928f, 144.386716f },
        new float[2] { -4.003420f, -0.259280f },
        new float[2] { 82.001950f, 113.642575f },
        new float[2] { -56.329102f, -40.535155f },
        new float[2] { 18.769041f, 24.117188f }
    };

    // Kindle
    private int quoteIndex;
    private int offset;
    private int startingWord;
    private static readonly string[][] kindleQuotes = new string[10][]
    {
        new string[] { "you", "have", "brains", "in", "your", "head", "you", "have", "feet", "in", "your", "shoes", "you", "can", "steer", "yourself", "any", "direction", "you", "choose" },
        new string[] { "there", "are", "darknesses", "in", "life", "and", "there", "are", "lights", "and", "you", "are", "one", "of", "the", "lights", "the", "light", "of", "all", "lights" },
        new string[] { "you", "never", "really", "understand", "a", "person", "until", "you", "consider", "things", "from", "his", "point", "of", "view", "until", "you", "climb", "inside", "of", "his", "skin", "and", "walk", "around", "in", "it" },
        new string[] { "bet", "i", "know", "something", "else", "you", "dont", "theres", "dew", "on", "the", "grass", "in", "this", "morning" },
        new string[] { "we", "lived", "in", "the", "blank", "white", "spaces", "at", "the", "edges", "of", "print", "it", "gave", "us", "more", "freedom" },
        new string[] { "weve", "all", "got", "both", "light", "and", "dark", "inside", "us", "what", "matters", "is", "the", "part", "we", "choose", "to", "act", "on", "thats", "who", "we", "really", "are" },
        new string[] { "do", "i", "love", "you", "my", "god", "if", "your", "love", "were", "a", "grain", "of", "sand", "mine", "would", "be", "a", "universe", "of", "beaches" },
        new string[] { "doublethink", "means", "the", "power", "of", "holding", "two", "contradictory", "beliefs", "in", "ones", "mind", "simultaneously", "and", "accepting", "both", "of", "them" },
        new string[] { "maybe", "everbody", "in", "the", "whole", "damn", "world", "is", "scared", "of", "each", "other" },
        new string[] { "there", "is", "an", "idea", "of", "a", "patrick", "bateman", "some", "kind", "of", "abstraction", "but", "there", "is", "no", "real", "me", "only", "an", "entity", "something", "illusory" }
    };
    public TextMesh[] kindleTexts;

    // Authentiator
    public TextMesh[] authenticatortexts;
    public Transform authenticatorbar;

    // Photomath
    public GameObject hideable;
    public TextMesh photomathmaintext;
    public TextMesh photomathstartingtext;
    public TextMesh photomathsolutiontext;
    public TextMesh[] photmathkeypad;
    public KMSelectable photomathstart;
    public KMSelectable photomathsubmit;
    public KMSelectable photomathclear;
    public KMSelectable[] photomathbuttons;
    public Renderer[] photomathcircles;
    public Color[] photomathcolors;
    private List<Color> photomathUsedColors = new List<Color>();
    #pragma warning disable 414
    private Coroutine mathCycle;
    #pragma warning restore 414
    private static readonly Vector3[] startingValuePositions = new Vector3[] { new Vector3(.074f, .0121f, -.0505f), new Vector3(-.074f, .0121f, -.0505f), new Vector3(-.074f, .0121f, .0267f), new Vector3(.074f, .0121f, .0267f) };
    private int startingValue;
    private bool photocycle;
    private int[] operations = new int[4];
    private int[] values = new int[4];
    private List<int> photomathEntered = new List<int>();
    private int photomathSolution;
    private List<string> mathSymbols = new List<string>() { "Σ", "ℝ", "≜", "!", "δ", "∞", "⋰", "∝", "∴", "¬" };
    private static readonly int[][] keypadGrids = new int[10][] {
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
    public GameObject copyright;
    private static readonly string[] adNames = new string[5] { "ad1", "ad2", "ad3", "ad4", "ad5" };
    private static readonly float[] adLengths = new float[5] { 13f, 18.5f, 15.25f, 14f, 17.5f };
    private static readonly string[] songNames = new string[9] { "rightround", "smoothcriminal", "hardwarestore", "beatit", "dangerzone", "tacky", "harderbetterfasterstronger", "drunkensailor", "megalovania" };
    private static readonly float[] songLengths = new float[9] { 7.5f, 7.5f, 30.5f, 8.5f, 5.5f, 6.5f, 5.5f, 8.5f, 8.5f };
    private static readonly string[] decoyNames = new string[10] { "decoy1", "decoy2", "decoy3", "decoy4", "decoy5", "decoy6", "decoy7", "decoy8", "decoy9", "decoy10" };
    private static readonly float[] decoyLengths = new float[10] { 7.5f, 8.5f, 6.5f, 8.5f, 9.5f, 6.5f, 9.5f, 6.5f, 8.5f, 8.5f, };
    private static readonly string[] ncSongNames = new string[9] { "heroestonight", "dontjuststandthere", "ignite", "blank", "runaway", "symbolism", "letsgo", "boombox", "journey" };
    private static readonly float[] ncSongLengths = new float[9] { 7.5f, 7.5f, 7f, 7f, 7.5f, 7.5f, 7.5f, 7.5f, 7.5f };
    private static readonly string[] ncDecoyNames = new string[10] { "ncdecoy1", "ncdecoy2", "ncdecoy3", "ncdecoy4", "ncdecoy5", "ncdecoy6", "ncdecoy7", "ncdecoy8", "ncdecoy9", "ncdecoy10" };
    private static readonly float[] ncDecoyLengths = new float[10] { 9.5f, 8f, 8f, 8.5f, 8f, 9f, 7f, 10f, 6f, 7.5f, };
    private int decoyIndex;
    private bool isPlaying;

    // Google Arts & Culture
    private int artistIndex;
    private static readonly int[][] artYears = new int[4][] {
        new int[8] { 1, 9, 4, 2, 1, 9, 9, 5, },
        new int[8] { 1, 8, 8, 1, 1, 9, 7, 3, },
        new int[8] { 1, 4, 5, 2, 1, 5, 1, 9, },
        new int[8] { 1, 8, 5, 3, 1, 8, 0, 0, }
    };
    private static readonly string[] artistNames = new string[5] { "Bob Ross", "Picasso", "Da Vinci", "Van Gogh", "Misc artist" };
    public Renderer painting;
    public TextMesh artisttext;
    public Texture[] bobross;
    public Texture[] picasso;
    public Texture[] davinci;
    public Texture[] vangogh;
    public Texture[] otherpaintings;
    private List<Texture[]> paintings = new List<Texture[]>();

    // Discord
    public AudioClip[] allSounds;
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
    public TextMesh twitchtext;
    #pragma warning disable 414
    private Coroutine cycle;
    private Coroutine voice;
    #pragma warning restore 414
    private List<Texture[]> allSymbols = new List<Texture[]>();
    private User[] users = new User[6];
    private int discordStage;
    private int person1;
    private int person2;
    private int discordActivity;
    private int discordSymbol;
    private int discordColor;
    private int[] extremes = new int[4];
    private bool cantLeave;
    private bool speaking;
    private bool leavingBecausestrike;
    private bool endOfDiscord;
    private bool cycling;
    private int currentSymbol;
    private int currentColor;
    private int currentIxUser;
    private int currentIxButton;
    private static readonly string[] discordNames = new string[10] { "TasThing", "Deaf", "Blananas", "Timwi", "Numdegased", "Nico Robin", "Espik", "Procyon", "eXish", "SillyPuppy" };
    private static readonly string[][] checkNames = new string[4][] {
        new string[10] { "TasThing", "Blananas", "Numdegased", "Espik", "eXish", "SillyPuppy", "Deaf", "Timwi", "Nico Robin", "Procyon" },
        new string[10] { "Timwi", "eXish", "SillyPuppy", "Nico Robin", "Procyon", "Numdegased", "Deaf", "Espik", "Blananas", "TasThing" },
        new string[10] { "Deaf", "Procyon", "Espik", "Nico Robin", "Blananas", "TasThing", "Timwi", "eXish", "SillyPuppy", "Numdegased" },
        new string[10] { "Blananas", "TasThing", "Timwi", "Numdegased", "eXish", "Espik", "Procyon", "Nico Robin", "Deaf", "SillyPuppy" }
    };
    private static readonly string[] busyExcuses = new string[10] { "she’s not really into you...", "he’s being himself.", "he’s busy modding.", "he’s at the badminton club.", "he’s watching a show.", "he can’t deal with you right now.", "he’s preoccupied.", "he’s not in the right headspace.", "he’ll be back to you later.", "he’s in the middle of something. (or someone?)" };

    private int currentAppIndex;
    private int[] solution = new int[8];
    private string solutionString = "";
    private List<string> enteredSolution = new List<string>();
    private int enteringStage;

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
    public Texture resetWallpaper;
    public Transform[] iconpositions;
    private static readonly Vector3[] appPositions = new Vector3[9] { new Vector3(-.05f, .0124f, .04f), new Vector3(0f, .0124f, .04f), new Vector3(.05f, .0124f, .04f), new Vector3(-.05f, .0124f, -.01f), new Vector3(0f, .0124f, -.01f), new Vector3(.05f, .0124f, -.01f), new Vector3(-.05f, .0124f, -.06f), new Vector3(0f, .0124f, -.06f), new Vector3(.05f, .0124f, -.06f) };
    private float timeRemaining;
    public Renderer solvedthingy;
    public Color solvedcolor;
    public Color strikecolor;
    public Light solvedlight;
    public Material[] ledcolors;
    public Renderer notificationlight;
    public Material notifoff;

    private SamsungSettings Settings = new SamsungSettings();
    private Texture currentWallpaper;
    private List<int> positionNumbers = new List<int>();
    private float halfPoint;
    private bool flashing;
    private bool generating;
    private static int moduleIdCounter = 1;
    private int moduleId;
    private bool moduleSolved;

    private static readonly string[] GETGAMEREDNIGGA = new string[] { "Forget Me Later", "The Samsung", "Dungeon 2nd Floor", "The Ultracube", "Echolocation", "Chinese Counting", "RGB Arithmetic", "Railway Cargo Loading", "Rainbow Arrows", "Intervals", "Module Rick" };
    private bool unlocked;
    public GameObject[] fuckinEverything;
    public Texture hoofduuoof;
    public GameObject theFunny;
    public TextMesh cockAndNuts;

    void Awake()
    {
        moduleId = moduleIdCounter++;
        ModConfig<SamsungSettings> modConfig = new ModConfig<SamsungSettings>("SamsungSettings");
        //Read from the settings file, or create one if one doesn't exist
        Settings = modConfig.Settings;
        //Update the settings file incase there was an error during read
        modConfig.Settings = Settings;
        Debug.LogFormat("[The Samsung #{0}] Copyright Filter: {1}", moduleId, Settings.noCopyright ? "On" : "Off");
        if (!Settings.noCopyright)
            copyright.SetActive(false);
        statusLight.SetActive(false);
        phonebackground.material.color = casingColors.PickRandom();
        currentWallpaper = wallpapers.PickRandom();
        phonescreen.material.mainTexture = currentWallpaper;
        volumestatus.material.mainTexture = volumeicons.PickRandom();
        var statusNumbers = Enumerable.Range(0, 5).ToList();
        statusNumbers.Shuffle();
        foreach (Renderer statusIcon in miscstatus)
            statusIcon.material.mainTexture = miscicons[statusNumbers[Array.IndexOf(miscstatus, statusIcon)]];
        // <Selectables>
        homebutton.OnInteract += delegate () { PressHomeButton(); return false; };
        resetButton.OnInteract += delegate () { PressResetButton(); return false; };
        playbutton.OnInteract += delegate () { PressPlayButton(); return false; };
        foreach (KMSelectable appButton in appButtons)
            appButton.OnInteract += delegate () { PressAppButton(appButton); return false; };
        foreach (KMSelectable button in settingsbuttons)
            button.OnInteract += delegate () { PressSettingsButton(button); return false; };
        photomathclear.OnInteract += delegate () { PressPhotomathClearButton(); return false; };
        photomathsubmit.OnInteract += delegate () { PressPhotomathSubmitButton(); return false; };
        photomathstart.OnInteract += delegate () { mathCycle = StartCoroutine(PhotomathCycle()); return false; };
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
        positionNumbers = Enumerable.Range(0, 9).Where(x => x != 4).ToList().Shuffle();
        for (int i = 0; i < 8; i++)
            iconpositions[i].localPosition = appPositions[positionNumbers[i]];
        iconpositions[8].localPosition = appPositions[4];
        solvedlight.enabled = false;
    }

    void Start()
    {
        float scalar = transform.lossyScale.x;
        solvedlight.range *= scalar;
        halfPoint = bomb.GetTime() / 2f;
        for (int i = 0; i < 8; i++)
            solution[i] = rnd.Range(0, 10);
        // Taskmaster
        theFunny.SetActive(false);
        if (bomb.GetSolvableModuleNames().All(x => GETGAMEREDNIGGA.Contains(x)) && !bomb.GetSolvableModuleNames().All(x => x == "The Samsung"))
        {
            if (!unlocked)
            {
                StartCoroutine(PenisMusic());
                return;
            }
        }
        // Duolingo
        languageIndex = rnd.Range(0, 10);
        duolingoNumbers[0] = rnd.Range(0, 21);
        operatorIndex = rnd.Range(0, 4);
        duolingoNumbers[1] = rnd.Range(1, 21);
        switch (operatorIndex)
        {
            case 0:
                solution[0] = mod(((duolingoNumbers[0] + duolingoNumbers[1]) + languageIndex + 1), 10);
                break;
            case 1:
                solution[0] = mod(((duolingoNumbers[0] - duolingoNumbers[1]) + languageIndex + 1), 10);
                break;
            case 2:
                solution[0] = mod(((duolingoNumbers[0] * duolingoNumbers[1]) + languageIndex + 1), 10);
                break;
            default:
                solution[0] = mod(((duolingoNumbers[0] / duolingoNumbers[1]) + languageIndex + 1), 10);
                break;
        }
        foreach (TextMesh dtext in duolingotexts)
        {
            var ix = Array.IndexOf(duolingotexts, dtext);
            switch (languageIndex)
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
                dtext.text = duolingo.numberWords[languageIndex][duolingoNumbers[0]];
            else if (ix == 1)
                dtext.text = duolingo.operatorWords[languageIndex][operatorIndex];
            else
                dtext.text = duolingo.numberWords[languageIndex][duolingoNumbers[1]];
        }
        Debug.LogFormat("[The Samsung #{0}] DUOLINGO:", moduleId);
        Debug.LogFormat("[The Samsung #{0}] The language present is {1}.", moduleId, languageNames[languageIndex]);
        Debug.LogFormat("[The Samsung #{0}] The expression is {1} {2} {3}.", moduleId, englishNumberNames[duolingoNumbers[0]], englishOperatorNames[operatorIndex], englishNumberNames[duolingoNumbers[1]]);
        Debug.LogFormat("[The Samsung #{0}] The solution for Duolingo is {1}.", moduleId, solution[0]);
        // Google Maps
        country1 = rnd.Range(0, 10);
        country2 = rnd.Range(0, 10);
        solution[1] = mod(country1 - country2, 10);
        gmapstext1[0].text = rnd.Range(latCords[country1][0], latCords[country1][1]).ToString();
        gmapstext1[1].text = rnd.Range(lonCords[country1][0], lonCords[country1][1]).ToString();
        gmapstext2[0].text = rnd.Range(latCords[country2][0], latCords[country2][1]).ToString();
        gmapstext2[1].text = rnd.Range(lonCords[country2][0], lonCords[country2][1]).ToString();
        Debug.LogFormat("[The Samsung #{0}] GOOGLE MAPS:", moduleId);
        string[] countryNames = new string[10] { "The United States", "Canada", "Mexico", "Russia", "Germany", "Australia", "The United Kingdom", "China", "Brazil", "South Africa" };
        Debug.LogFormat("[The Samsung #{0}] The first pair of coordinates is in {1}, and the second pair is in {2}.", moduleId, countryNames[country1], countryNames[country2]);
        Debug.LogFormat("[The Samsung #{0}] The solution for Google Maps is {1}.", moduleId, solution[1]);
        // Kindle
        quoteIndex = rnd.Range(0, 10);
        offset = rnd.Range(1, 14);
        startingWord = rnd.Range(0, kindleQuotes[quoteIndex].Length - 3);
        var wordSet = new Char[4][];
        Char[] alphabet = new Char[39] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm' };
        for (int i = 0; i < 4; i++)
        {
            wordSet[i] = kindleQuotes[quoteIndex][startingWord + i].ToCharArray();
            for (int j = 0; j < wordSet[i].Length; j++)
                wordSet[i][j] = alphabet[Array.IndexOf(alphabet, wordSet[i][j]) + offset];
            kindleTexts[i].text = new string(wordSet[i]);
        }
        solution[2] = mod(quoteIndex + offset, 10);
        Debug.LogFormat("[The Samsung #{0}] KINDLE:", moduleId);
        Debug.LogFormat("[The Samsung #{0}] The 4 words are taken from quote {1}, and the letters are shifted forwards by {2}.", moduleId, quoteIndex, offset);
        Debug.LogFormat("[The Samsung #{0}] The solution for Kindle is {1}.", moduleId, solution[2]);
        // Authenticator
        string[] conditionNames = new string[10] { "a digital root of 8", "a perfect square root", "division by 7", "an odd result when modulod by 5", "a digital root of 3 or 4", "division by 6", "a digital root of 7", "division by 9", "a digital root of 5", "division by 3" };
        Debug.LogFormat("[The Samsung #{0}] GOOGLE AUTHENTICATOR:", moduleId);
        Debug.LogFormat("[The Samsung #{0}] Every number shown has {1}.", moduleId, conditionNames[solution[3]]);
        Debug.LogFormat("[The Samsung #{0}] Therefore, the solution for Google Authenticator is {1}.", moduleId, solution[3]);
        // Photomath
        GeneratePhotomath();
        // Spotify
        decoyIndex = rnd.Range(0, 10);
        string[] songNames = new string[] { "You Spin Me Right Round", "Smooth Criminal", "Hardware Store", "Beat It", "Danger Zone", "Tacky", "Harder, Better, Faster, Stronger", "Drunken Sailor", "Megalovania", "a song not mentioned" };
        string[] ncSongNames = new string[] { "Heroes Tonight", "Don't Just Stand There", "Ignite", "Blank", "Runaway", "Symbolism", "Lets Go!", "Boombox 2012", "Journey", "a song not mentioned" };
        Debug.LogFormat("[The Samsung #{0}] SPOTIFY:", moduleId);
        Debug.LogFormat("[The Samsung #{0}] The song being played is {1}, so the solution for Spotify is {2}.", moduleId, Settings.noCopyright ? ncSongNames[solution[5]] : songNames[solution[5]], solution[5]);
        // Google Arts & Culture
        paintings = new List<Texture[]> { bobross, picasso, davinci, vangogh };
        artistIndex = rnd.Range(0, 5);
        int gacSolution;
        bool lying = rnd.Range(0, 2) == 0;
        int paintingIndex = rnd.Range(0, 5);
        if (artistIndex == 4)
        {
            gacSolution = bomb.GetSerialNumber()[5] - '0';
            painting.material.mainTexture = otherpaintings.PickRandom();
        }
        else
        {
            switch (positionNumbers[6])
            {
                case 0:
                    gacSolution = artYears[artistIndex][0];
                    break;
                case 1:
                    gacSolution = artYears[artistIndex][1];
                    break;
                case 2:
                    gacSolution = artYears[artistIndex][2];
                    break;
                case 3:
                    gacSolution = artYears[artistIndex][3];
                    break;
                case 5:
                    gacSolution = artYears[artistIndex][4];
                    break;
                case 6:
                    gacSolution = artYears[artistIndex][5];
                    break;
                case 7:
                    gacSolution = artYears[artistIndex][6];
                    break;
                default:
                    gacSolution = artYears[artistIndex][7];
                    break;
            }
            var n1 = new int[] { 0, 2, 4, 6, 8 };
            var n2 = new int[] { 1, 3, 5, 7, 9 };
            gacSolution += (!lying ? n1[paintingIndex] : n2[paintingIndex]);
            gacSolution %= 10;
            painting.material.mainTexture = paintings[artistIndex][paintingIndex];
        }
        artisttext.text = !lying ? artistNames[artistIndex] : artistNames.Where(x => Array.IndexOf(artistNames, x) != artistIndex).PickRandom();
        Debug.LogFormat("[The Samsung #{0}] GOOGLE ARTS & CULTURE:", moduleId);
        if (artistIndex != 4)
            Debug.LogFormat("[The Samsung #{0}] The art is by {1}. It corresponds to the digit {2}, and the text is{3}.", moduleId, artistNames[artistIndex], paintingIndex, !lying ? "n’t lying" : " lying");
        else
            Debug.LogFormat("[The Samsung #{0}] This art is by some other artist. Use the last digit of the serial number.", moduleId);
        Debug.LogFormat("[The Samsung #{0}] The solution for Google A&C is {1}.", moduleId, gacSolution);
        solution[6] = gacSolution;
        // Discord
        allSymbols = new List<Texture[]> { symbol1, symbol2, symbol3, symbol4, symbol5, symbol6, symbol7, symbol8 };
        cyclingsymbol.gameObject.SetActive(false);
        call.SetActive(false);
        greencircle.SetActive(false);
        GenerateDiscord();
        // Solution
        string[] directionNames = new string[8] { "north-west", "north", "north-east", "east", "south-east", "south", "south-west", "west" };
        Debug.LogFormat("[The Samsung #{0}] SETTINGS:", moduleId);
        int startingOffset;
        var ser = bomb.GetSerialNumber();
        if (Char.IsLetter(ser[0]) && Char.IsLetter(ser[1]))
            startingOffset = 1;
        else if (Char.IsLetter(ser[0]) && !Char.IsLetter(ser[1]))
            startingOffset = 3;
        else if (!Char.IsLetter(ser[0]) && Char.IsLetter(ser[1]))
            startingOffset = 5;
        else
            startingOffset = 7;
        Debug.LogFormat("[The Samsung #{0}] The initial direction is, at first, {1}.", moduleId, directionNames[startingOffset]);
        if (bomb.GetPortCount(Port.Parallel) > 0)
            startingOffset--;
        else if (bomb.GetPortCount(Port.Serial) > 0)
            startingOffset++;
        startingOffset %= 8;
        Debug.LogFormat("[The Samsung #{0}] After modifications, the initial direction is {1}.", moduleId, directionNames[startingOffset]);
        //
        var solutionList = new List<int>();
        var clockwiseOrder = new int[] { 0, 1, 2, 5, 8, 7, 6, 3 };
        for (int i = 0; i < 8; i++)
            solutionList.Add(solution[positionNumbers.IndexOf(clockwiseOrder[(i + startingOffset) % clockwiseOrder.Length])]);
        solutionString = solutionList.Join("");
        Debug.LogFormat("[The Samsung #{0}] The module’s solution is {1}.", moduleId, solutionString);
        StartCoroutine(DisableStuff());
        StartCoroutine(Authenticator());
    }

    IEnumerator DisableStuff()
    {
        yield return null;
        homebutton.gameObject.SetActive(false);
        foreach (GameObject app in apps)
            app.SetActive(false);
        hideable.SetActive(false);
        photomathmaintext.text = "";
        photomathsolutiontext.text = "";
    }

    void PressResetButton()
    {
        audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, resetButton.transform);
        resetButton.AddInteractionPunch(.5f);
        switch (currentAppIndex)
        {
            case 7:
                if (!apps[7].activeSelf)
                    return;
                Debug.LogFormat("[The Samsung #{0}] Discord reset.", moduleId);
                discordStage = 0;
                cantLeave = false;
                if (cycle != null)
                {
                    StopCoroutine(cycle);
                    cycle = null;
                }
                if (voice != null)
                {
                    StopCoroutine(voice);
                    voice = null;
                }
                GenerateDiscord();
                break;
            case 4:
                if (!apps[4].activeSelf)
                    return;
                Debug.LogFormat("[The Samsung #{0}] Photomath reset.", moduleId);
                if (mathCycle != null)
                {
                    StopCoroutine(mathCycle);
                    mathCycle = null;
                    photocycle = false;
                }
                hideable.SetActive(false);
                photomathstart.gameObject.SetActive(true);
                photomathmaintext.text = "";
                photomathstartingtext.text = "";
                photomathsolutiontext.text = "";
                GeneratePhotomath();
                break;
            default:
                break;
        }
    }

    void GenerateDiscord()
    {
        generating = true;
        notificationlight.material = ledcolors[1];
        pfps.SetActive(true);
        cyclingsymbol.gameObject.SetActive(false);
        call.SetActive(false);
        greencircle.SetActive(false);
        discordtryagain:
        var userNumbers = Enumerable.Range(0, 10).ToList().Shuffle();
        var discordNumbers = Enumerable.Range(0, 16).ToList().Shuffle();
        var xfs = new float[4] { -.057f, -.0191f, .0188f, .0567f };
        var yfs = new float[4] { .0462f, .0083f, -.0296f, -.0675f };
        for (int i = 0; i < 6; i++)
        {
            users[i] = new User { id = i, positionNumber = discordNumbers[i], userId = userNumbers[i], userName = discordNames[userNumbers[i]], x = 0f, z = 0f };
            pfppositions[i].localPosition = new Vector3(xfs[discordNumbers[i] % 4], .0123f, yfs[discordNumbers[i] / 4]);
            users[i].x = pfppositions[i].localPosition.x;
            users[i].z = pfppositions[i].localPosition.z;
            pfprenders[i].material.mainTexture = pfpimages[userNumbers[i]];
        }
        var nonselves = new List<User>[6];
        for (int i = 0; i < 6; i++)
            nonselves[i] = users.Where(u => u != users[i]).ToList();
        extremes[0] = Array.IndexOf(users, users.Where(u => nonselves[Array.IndexOf(users, u)].Any(uu => uu.z != u.z)).OrderBy(u => u.z).Last());
        extremes[1] = Array.IndexOf(users, users.Where(u => nonselves[Array.IndexOf(users, u)].Any(uu => uu.x != u.x)).OrderBy(u => u.x).Last());
        extremes[2] = Array.IndexOf(users, users.Where(u => nonselves[Array.IndexOf(users, u)].Any(uu => uu.z != u.z)).OrderBy(u => u.z).First());
        extremes[3] = Array.IndexOf(users, users.Where(u => nonselves[Array.IndexOf(users, u)].Any(uu => uu.x != u.x)).OrderBy(u => u.x).First());
        for (int i = 0; i < 4; i++)
            if (nonselves[i].Any(u => ((i == 0 || i == 2) ? u.z : u.x) == ((i == 0 || i == 2) ? users[extremes[i]].z : users[extremes[i]].x)))
                goto discordtryagain;
        Debug.LogFormat("[The Samsung #{0}] DISCORD:", moduleId);
        switch (Braille(users.Select(u => u.positionNumber).ToArray()))
        {
            case "A":
                person1 = bomb.GetModuleNames().Count() % 2 == 0 ? extremes[0] : extremes[2];
                Debug.LogFormat("[The Samsung #{0}] The 2×3 in the top-left spells out a Braille letter in set A.", moduleId);
                break;
            case "B":
                person1 = (bomb.IsIndicatorOn("MSA") || bomb.IsIndicatorOn("NSA")) ? extremes[3] : extremes[1];
                Debug.LogFormat("[The Samsung #{0}] The 2×3 in the top-left spells out a Braille letter in set B.", moduleId);
                break;
            case "C":
                person1 = bomb.GetSerialNumberLetters().Any(x => "CORA".Contains(x)) ? extremes[2] : extremes[3];
                Debug.LogFormat("[The Samsung #{0}] The 2×3 in the top-left spells out a Braille letter in set C.", moduleId);
                break;
            case "D":
                person1 = (bomb.GetBatteryCount() + bomb.GetBatteryHolderCount()) % 2 == 0 ? extremes[0] : extremes[1];
                Debug.LogFormat("[The Samsung #{0}] The 2×3 in the top-left spells out a Braille letter in set D.", moduleId);
                break;
            case "E":
                person1 = bomb.GetSerialNumberLetters().Count(x => "AEIOU".Contains(x)) == 0 ? extremes[2] : extremes[1];
                Debug.LogFormat("[The Samsung #{0}] The 2×3 in the top-left spells out a Braille letter in set E.", moduleId);
                break;
            default:
                person1 = bomb.GetPortPlates().Any(x => x.Length == 0) ? extremes[0] : extremes[3];
                Debug.LogFormat("[The Samsung #{0}] The 2×3 in the top-left does not spell out a Braille letter.", moduleId);
                break;
        }
        var userName2 = checkNames[Array.IndexOf(extremes, person1)].First(s => users.Any(u => u.userName == s && u.id != person1));
        person2 = users.First(u => u.userName == userName2).id;
        discordActivity = rnd.Range(0, 10);
        discordColor = rnd.Range(0, 6);
        discordSymbol = rnd.Range(0, 8);
        currentSymbol = rnd.Range(0, 8);
        currentColor = rnd.Range(0, 6);
        var activityNames = new string[10] { "defusing", "playing Jackbox", "playing Tabletop Simulator", "reacting to a new mod", "complaining about sleep", "experting", "arguing", "talking about food", "being AFK", "something else" };
        var discordColornames = new string[6] { "red", "orange", "yellow", "green", "blue", "purple" };
        var extremeNames = new string[4] { "top-most", "right-most", "bottom-most", "left-most" };
        Debug.LogFormat("[The Samsung #{0}] The first user to call is {1}, because they are the {2} user.", moduleId, users[person1].userName, extremeNames[Array.IndexOf(extremes, extremes.Where(x => x == person1).First())]);
        Debug.LogFormat("[The Samsung #{0}] The second user to call is {1}.", moduleId, users[person2].userName);
        Debug.LogFormat("[The Samsung #{0}] The activity is {1}, which corresponds to {2}.", moduleId, activityNames[discordActivity], discordActivity);
        Debug.LogFormat("[The Samsung #{0}] The correct symbol is symbol {1}.", moduleId, discordSymbol + 1);
        Debug.LogFormat("[The Samsung #{0}] The correct color is {1}.", moduleId, discordColornames[discordColor]);
        Debug.LogFormat("[The Samsung #{0}] The solution for Discord is {1}.", moduleId, solution[7]);
        generating = false;
        notificationlight.material = notifoff;
    }

    void GeneratePhotomath()
    {
        mathSymbols.Shuffle();
        startingValue = rnd.Range(1, 10);
        photomathUsedColors = photomathcolors.ToList().Shuffle();
        for (int i = 0; i < 4; i++)
            photomathcircles[i].material.color = photomathUsedColors[i];
        for (int i = 0; i < 4; i++)
        {
            operations[i] = rnd.Range(0, 4);
            values[i] = rnd.Range(1, 10);
        }
        string[] operationNames = new string[4] { "plus", "minus", "times", "divided by" };
        Debug.LogFormat("[The Samsung #{0}] PHOTOMATH:", moduleId);
        for (int i = 0; i < 4; i++)
        {
            var firstValue = (i == 0) ? startingValue : photomathSolution;
            var secondValue = values[i];
            switch (operations[i])
            {
                case 0:
                    photomathSolution = firstValue + secondValue;
                    break;
                case 1:
                    photomathSolution = firstValue - secondValue;
                    break;
                case 2:
                    photomathSolution = firstValue * secondValue;
                    break;
                default:
                    photomathSolution = firstValue / secondValue;
                    break;
            }
            Debug.LogFormat("[The Samsung #{0}] {1} {2} {3} is {4}.", moduleId, firstValue, operationNames[operations[i]], secondValue, photomathSolution);
        }
        if (photomathSolution < 0)
            photomathSolution *= -1;
        photomathSolution %= 1000;
        int[] currentKeypadGrid = keypadGrids[bomb.GetSerialNumberNumbers().Last()];
        for (int i = 0; i < 10; i++)
            photmathkeypad[i].text = mathSymbols[currentKeypadGrid[i]];
        string[] colornames = new string[4] { "blue", "purple", "green", "yellow" };
        Debug.LogFormat("[The Samsung #{0}] The circles on the bottom are colored {1}, {2}, {3}, and then {4}.", moduleId, colornames[Array.IndexOf(photomathcolors, photomathUsedColors[0])], colornames[Array.IndexOf(photomathcolors, photomathUsedColors[1])], colornames[Array.IndexOf(photomathcolors, photomathUsedColors[2])], colornames[Array.IndexOf(photomathcolors, photomathUsedColors[3])]);
        for (int i = 0; i < 10; i++)
            Debug.LogFormat("[The Samsung #{0}] {1} corresponds to {2}.", moduleId, mathSymbols[i], i);
        Debug.LogFormat("[The Samsung #{0}] The number to enter is {1}.", moduleId, photomathSolution);
        Debug.LogFormat("[The Samsung #{0}] The solution for Photomath is {1}.", moduleId, solution[4]);
    }

    void PressAppButton(KMSelectable button)
    {
        button.AddInteractionPunch(.1f);
        currentAppIndex = Array.IndexOf(appButtons, button);
        audio.PlaySoundAtTransform("keyClick", button.transform);
        icons.SetActive(false);
        homebutton.gameObject.SetActive(true);
        phonescreen.material.mainTexture = appbackgrounds[currentAppIndex];
        apps[currentAppIndex].SetActive(true);
    }

    void PressHomeButton()
    {
        homebutton.AddInteractionPunch(.1f);
        if (cantLeave || easterEgging || strikeAnimating || generating)
            return;
        audio.PlaySoundAtTransform("keyClick", homebutton.transform);
        icons.SetActive(true);
        homebutton.gameObject.SetActive(false);
        phonescreen.material.mainTexture = currentWallpaper;
        apps[currentAppIndex].SetActive(false);
    }

    void PressPlayButton()
    {
        playbutton.AddInteractionPunch(.1f);
        if (isPlaying)
            return;
        StartCoroutine(Spotify());
    }

    IEnumerator Spotify()
    {
        isPlaying = true;
        var adIx = rnd.Range(0, 5);
        audio.PlaySoundAtTransform(adNames[adIx], playbutton.transform);
        yield return new WaitForSeconds(adLengths[adIx] + .5f);
        if (solution[5] == 9)
        {
            if (Settings.noCopyright)
            {
                audio.PlaySoundAtTransform(ncDecoyNames[decoyIndex], playbutton.transform);
                yield return new WaitForSeconds(ncDecoyLengths[decoyIndex]);
            }
            else
            {
                audio.PlaySoundAtTransform(decoyNames[decoyIndex], playbutton.transform);
                yield return new WaitForSeconds(decoyLengths[decoyIndex]);
            }
        }
        else
        {
            if (Settings.noCopyright)
            {
                audio.PlaySoundAtTransform(ncSongNames[solution[5]], playbutton.transform);
                yield return new WaitForSeconds(ncSongLengths[solution[5]]);
            }
            else
            {
                audio.PlaySoundAtTransform(songNames[solution[5]], playbutton.transform);
                yield return new WaitForSeconds(songLengths[solution[5]]);
            }
        }
        isPlaying = false;
    }

    void PressPhotomathClearButton()
    {
        photomathclear.AddInteractionPunch(.1f);
        audio.PlaySoundAtTransform("keyClick", photomathclear.transform);
        photomathsolutiontext.text = "";
        photomathEntered.Clear();
    }

    void PressPhotomathSubmitButton()
    {
        photomathsubmit.AddInteractionPunch(.1f);
        audio.PlaySoundAtTransform("keyClick", photomathsubmit.transform);
        var photomashAns = photomathEntered.Join("");
        if (photomashAns != photomathSolution.ToString())
        {
            Debug.LogFormat("[The Samsung #{0}] You submitted {1} on Photomath. That was incorrect. Strike!", moduleId, photomashAns);
            photomathEntered.Clear();
            StartCoroutine(Strike());
        }
        else
        {
            hideable.SetActive(false);
            photomathstart.gameObject.SetActive(false);
            photomathsolutiontext.text = "";
            photomathmaintext.text = mathSymbols[solution[4]];
            photomathmaintext.color = photomathcolors[2];
        }
    }

    void PressPhotomathButton(KMSelectable button)
    {
        button.AddInteractionPunch(.1f);
        audio.PlaySoundAtTransform("keyClick", button.transform);
        if (photomathsolutiontext.text.Length >= 3)
            return;
        var ix = Array.IndexOf(photomathbuttons, button);
        photomathEntered.Add(keypadGrids[bomb.GetSerialNumberNumbers().Last()][ix]);
        photomathsolutiontext.text += button.GetComponentInChildren<TextMesh>().text;
    }

    IEnumerator PhotomathCycle()
    {
        photocycle = true;
        audio.PlaySoundAtTransform("keyClick", photomathstart.transform);
        photomathsolutiontext.text = "";
        photomathEntered.Clear();
        var show = rnd.Range(0, 4);
        photomathstartingtext.transform.localPosition = startingValuePositions.PickRandom();
        photomathstart.gameObject.SetActive(false);
        hideable.SetActive(false);
        for (int i = 0; i < 4; i++)
        {
            photomathmaintext.text = mathSymbols[values[i]];
            photomathmaintext.color = photomathUsedColors[operations[i]];
            if (i == show)
                photomathstartingtext.text = mathSymbols[startingValue];
            else
                photomathstartingtext.text = "";
            audio.PlaySoundAtTransform("bass", photomathmaintext.transform);
            yield return new WaitForSeconds(.7f);
        }
        photomathmaintext.text = "";
        photomathstartingtext.text = "";
        photomathstart.gameObject.SetActive(true);
        hideable.SetActive(true);
        photocycle = false;
    }

    void PressPfpButton(KMSelectable button, int ix)
    {
        if (generating)
            return;
        button.AddInteractionPunch(.1f);
        callpfp.material.mainTexture = pfpimages[users[ix].userId];
        call.SetActive(true);
        pfps.SetActive(false);
        audio.PlaySoundAtTransform("join", button.transform);
        voice = StartCoroutine(DiscordVoice(users[ix].userId, ix));
    }

    IEnumerator DiscordVoice(int ixuser, int ixbutton)
    {
        currentIxUser = ixuser;
        currentIxButton = ixbutton;
        var tempNames = new string[] { "tas", "deaf", "blan", "timwi", "num", "nico", "espik", "procyon", "exish", "silly" };
        cantLeave = true;
        speaking = true;
        greencircle.SetActive(true);
        twitchtext.text = "";
        yield return new WaitForSeconds(.75f);
        if (discordStage == 0)
        {
            if (ixbutton != person1)
            {
                var soundName = tempNames[ixuser] + "busy";
                audio.PlaySoundAtTransform(soundName, callpfp.transform);
                yield return new WaitForSeconds(allSounds.First(x => x.name == soundName).length + .5f);
                Debug.LogFormat("[The Samsung #{0}] You called {1}, but {2} Strike!", moduleId, discordNames[ixuser], busyExcuses[ixuser]);
                StartCoroutine(Strike());
                cantLeave = false;
                speaking = false;
                leavingBecausestrike = true;
                PressLeaveButton();
            }
            else
            {
                var tempActivities = new string[] { "defuse", "jackbox", "tabletop", "reaction", "sleep", "expert", "argument", "food", "brb", "misc" };
                var soundName = tempNames[ixuser] + tempActivities[discordActivity];
                audio.PlaySoundAtTransform(soundName, callpfp.transform);
                yield return new WaitForSeconds(allSounds.First(x => x.name == soundName).length + .5f);
            }
        }
        else if (discordStage == 1)
        {
            var soundName = tempNames[ixuser] + "symbol" + (discordSymbol + 1);
            audio.PlaySoundAtTransform(soundName, callpfp.transform);
            yield return new WaitForSeconds(allSounds.First(x => x.name == soundName).length + .5f);
            greencircle.SetActive(false);
            yield return new WaitForSeconds(.75f);
            var tempColors = new string[6] { "red", "orange", "yellow", "green", "blue", "purple" };
            soundName = tempNames[ixuser] + tempColors[discordColor];
            greencircle.SetActive(true);
            audio.PlaySoundAtTransform(soundName, callpfp.transform);
            yield return new WaitForSeconds(allSounds.First(x => x.name == soundName).length + .5f);
            speaking = false;
            cantLeave = false;
            PressLeaveButton();
            discordStage++;
        }
        else if (discordStage == 2)
        {
            if (ixbutton != person2)
            {
                var soundName = tempNames[ixuser] + "busy";
                audio.PlaySoundAtTransform(soundName, callpfp.transform);
                yield return new WaitForSeconds(allSounds.First(x => x.name == soundName).length + .5f);
                Debug.LogFormat("[The Samsung #{0}] You called {1}, but {2} Strike!", moduleId, discordNames[ixuser], busyExcuses[ixuser]);
                StartCoroutine(Strike());
                cantLeave = false;
                speaking = false;
                leavingBecausestrike = true;
                PressLeaveButton();
            }
            else
            {
                cycling = true;
                if (TwitchPlaysActive)
                    twitchtext.text = "0";
                cycle = StartCoroutine(SymbolCycle());
            }
        }
        else if (discordStage == 4)
        {
            var soundName = tempNames[ixuser] + solution[7];
            audio.PlaySoundAtTransform(soundName, callpfp.transform);
            yield return new WaitForSeconds(allSounds.First(x => x.name == soundName).length + .5f);
            cycling = false;
            endOfDiscord = true;
            cantLeave = false;
            PressLeaveButton();
        }
        speaking = false;
        greencircle.SetActive(false);
    }

    IEnumerator SymbolCycle()
    {
        cantLeave = true;
        cyclingsymbol.material.mainTexture = allSymbols[currentSymbol][currentColor];
        cyclingsymbol.gameObject.SetActive(true);
        while (discordStage == 2 && cycling)
        {
            currentSymbol = (currentSymbol + 1) % 8;
            cyclingsymbol.material.mainTexture = allSymbols[currentSymbol][currentColor];
            if (TwitchPlaysActive)
                twitchtext.text = ((int.Parse(twitchtext.text) + 1) % 8) + "";
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(1f);
        twitchtext.text = "";
        while (discordStage == 3 && cycling)
        {
            currentColor = (currentColor + 1) % 6;
            cyclingsymbol.material.mainTexture = allSymbols[currentSymbol][currentColor];
            yield return new WaitForSeconds(1f);
        }
    }

    void PressLeaveButton()
    {
        if (cantLeave)
            return;
        greencircle.SetActive(false);
        cantLeave = false;
        call.SetActive(false);
        pfps.SetActive(true);
        audio.PlaySoundAtTransform(leavingBecausestrike ? "leave" : "disconnect", callpfp.transform);
        leavingBecausestrike = false;
        if (endOfDiscord)
            StartCoroutine(HideDiscord());
    }

    void PressMuteButton()
    {
        mutebutton.AddInteractionPunch(.1f);
        if (speaking || generating)
            return;
        if (discordStage == 0)
        {
            if (!(((int)bomb.GetTime()) % 10 == discordActivity))
            {
                StartCoroutine(Strike());
                Debug.LogFormat("[The Samsung #{0}] You pressed the mute button at the wrong time. Strike!", moduleId);
            }
            else
            {
                discordStage++;
                voice = StartCoroutine(DiscordVoice(currentIxUser, currentIxButton));
            }
        }
        else if (discordStage == 2)
        {
            if (currentSymbol != discordSymbol)
            {
                StartCoroutine(Strike());
                Debug.LogFormat("[The Samsung #{0}] You submitted the wrong symbol. Strike!", moduleId);
            }
            else
                discordStage++;
        }
        else if (discordStage == 3)
        {
            if (currentColor != discordColor)
            {
                StartCoroutine(Strike());
                Debug.LogFormat("[The Samsung #{0}] You submitted the wrong color. Strike!", moduleId);
            }
            else
            {
                cantLeave = false;
                cycling = false;
                discordStage++;
                voice = StartCoroutine(DiscordVoice(currentIxUser, currentIxButton));
            }
        }
    }

    IEnumerator HideDiscord()
    {
        StopCoroutine(SymbolCycle());
        cycle = null;
        var unhiddenicons = Enumerable.Range(0, 6).ToList().Shuffle();
        for (int i = 0; i < 6; i++)
        {
            pfprenders[unhiddenicons[i]].gameObject.SetActive(false);
            audio.PlaySoundAtTransform("pop", pfprenders[unhiddenicons[i]].transform);
            yield return new WaitForSeconds(.1f);
        }
    }

    void PressSettingsButton(KMSelectable button)
    {
        button.AddInteractionPunch(.1f);
        if (enteringStage > 7 || easterEgging || strikeAnimating)
            return;
        audio.PlaySoundAtTransform("keyClick", button.transform);
        var numbers = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
        enteredSolution.Add(numbers[Array.IndexOf(settingsbuttons, button)]);
        answertext.text += enteredSolution[enteringStage];
        enteringStage++;
    }

    void PressClearButton()
    {
        clearbutton.AddInteractionPunch(.1f);
        if (easterEgging || strikeAnimating)
            return;
        audio.PlaySoundAtTransform("keyClick", clearbutton.transform);
        enteringStage = 0;
        enteredSolution.Clear();
        answertext.text = "";
    }

    void PressSubmitButton()
    {
        submitbutton.AddInteractionPunch(.1f);
        if (easterEgging || strikeAnimating)
            return;
        audio.PlaySoundAtTransform("keyClick", submitbutton.transform);
        var enteredSolutionString = enteredSolution.Join("");
        Debug.LogFormat("[The Samsung #{0}] You submitted {1}.", moduleId, enteredSolutionString);
        if (enteredSolutionString == solutionString)
        {
            Debug.LogFormat("[The Samsung #{0}] That was correct. Module solved!", moduleId);
            apps[8].SetActive(false);
            homebutton.gameObject.SetActive(false);
            phonescreen.material.mainTexture = resetWallpaper;
            module.HandlePass();
            audio.PlaySoundAtTransform("solve", module.transform);
            solvedthingy.material.color = solvedcolor;
            StopAllCoroutines();
            solvedlight.enabled = true;
        }
        else if (easterEggs.Any(x => x == enteredSolutionString))
        {
            var ix = Array.IndexOf(easterEggs, enteredSolutionString);
            StartCoroutine(EasterEgg(enteredSolutionString, ix));
        }
        else
        {
            Debug.LogFormat("[The Samsung #{0}] That was incorrect. Strike!", moduleId);
            StartCoroutine(Strike());
            StartCoroutine(StrikeCycle(enteredSolutionString));
        }
    }

    IEnumerator StrikeCycle(string enteredSolutionString)
    {
        strikeAnimating = true;
        for (int i = 0; i < enteredSolutionString.Length; i++)
        {
            if (solutionString[i] != enteredSolutionString[i])
            {
                strikeText.text = (i + 1).ToString();
                yield return new WaitForSeconds(1.25f);
                strikeText.text = "";
                yield return new WaitForSeconds(.5f);
            }
        }
        strikeAnimating = false;
    }

    IEnumerator EasterEgg(string name, int ix)
    {
        easterEgging = true;
        audio.PlaySoundAtTransform(name, homebutton.transform);
        yield return new WaitForSeconds(easterEggLengths[ix]);
        easterEgging = false;
        enteredSolution.Clear();
        answertext.text = "";
        enteringStage = 0;
    }

    IEnumerator Authenticator()
    {
        var elapsed = 0f;
        var duration = 6f;
        var validNumbers = Enumerable.Range(100000, 900000).Where(x => AuthCheck(x)).Select(x => x.ToString()).ToArray();
        restart:
        elapsed = 0f;
        authenticatorbar.localScale = new Vector3(.15f, .001f, .01f);
        foreach (TextMesh auth in authenticatortexts)
            auth.text = validNumbers.PickRandom();
        while (elapsed < duration)
        {
            authenticatorbar.localScale = new Vector3(Mathf.Lerp(.15f, 0f, elapsed / duration), .001f, .01f);
            yield return null;
            elapsed += Time.deltaTime;
        }
        goto restart;
    }

    bool AuthCheck(int x)
    {
        if (solution[3] == 0)
            return dr(x) == 8;
        else if (solution[3] == 1)
        {
            double result = Math.Sqrt(x);
            return result % 1.0 == 0.0;
        }
        else if (solution[3] == 2)
            return x % 7 == 0;
        else if (solution[3] == 3)
            return (x % 5) % 2 != 0;
        else if (solution[3] == 4)
            return (dr(x) == 3) || (dr(x) == 4);
        else if (solution[3] == 5)
            return x % 6 == 0;
        else if (solution[3] == 6)
            return dr(x) == 7;
        else if (solution[3] == 7)
            return x % 9 == 0;
        else if (solution[3] == 8)
            return dr(x) == 5;
        else
        {
            double result = CubeRoot(x);
            return result % 1.0 == 0.0;
        }
    }

    static double CubeRoot(int x)
    {
        double number, result;
        number = Convert.ToDouble(x);
        result = Math.Pow(number, (double) 1 / 3);
        return result;
    }

    class User
    {
        public int id;
        public int positionNumber;
        public int userId;
        public string userName;
        public float x;
        public float z;
    }

    static string Braille(int[] g)
    {
        bool[] truthGrid = new bool[6];
        int[] checkGrid = new int[6] { 0, 4, 8, 1, 5, 9 };
        for (int i = 0; i < 6; i++)
            if (g.Any(ix => ix == checkGrid[i]))
                truthGrid[i] = true;
        if (BrailleArrays.brailleLetters[0].Any(a => a.SequenceEqual(truthGrid)))
            return "A";
        else if (BrailleArrays.brailleLetters[1].Any(a => a.SequenceEqual(truthGrid)))
            return "B";
        else if (BrailleArrays.brailleLetters[2].Any(a => a.SequenceEqual(truthGrid)))
            return "C";
        else if (BrailleArrays.brailleLetters[3].Any(a => a.SequenceEqual(truthGrid)))
            return "D";
        else if (BrailleArrays.brailleLetters[4].Any(a => a.SequenceEqual(truthGrid)))
            return "E";
        else
            return "F";
    }

    void Update()
    {
        timeRemaining = bomb.GetTime();
        if (timeRemaining > halfPoint || timeRemaining == halfPoint)
            batstatus.material.mainTexture = baticons[0];
        else
            batstatus.material.mainTexture = baticons[1];
        if (!flashing && !generating && rnd.Range(0, 10000) == 0)
            StartCoroutine(FlashLed());
        timetext.text = DateTime.Now.ToString("hh:mm");
    }

    IEnumerator FlashLed()
    {
        flashing = true;
        notificationlight.material = ledcolors.PickRandom();
        yield return new WaitForSeconds(1f);
        if (!generating)
            notificationlight.material = notifoff;
        flashing = false;
    }

    IEnumerator Strike()
    {
        module.HandleStrike();
        var defaultColor = solvedthingy.material.color;
        solvedthingy.material.color = strikecolor;
        solvedlight.enabled = true;
        solvedlight.color = strikecolor;
        yield return new WaitForSeconds(.5f);
        solvedlight.enabled = false;
        solvedlight.color = solvedcolor;
        solvedthingy.material.color = defaultColor;
    }

    static int mod(int x, int m)
    {
        if (x < 0)
            x *= -1;
        return x % m;
    }

    static int dr(int x)
    {
        return ((x - 1) % 9) + 1;
    }

    // Twitch Plays
    #pragma warning disable 0649
    #pragma warning disable 414
    bool TwitchPlaysActive;

    private readonly string TwitchHelpMessage = @"!{0} open <duo/maps/kindle/auth/photo/spotify/arts/discord/settings> [Opens the specified app] | !{0} play [Presses the play button if Spotify is open] | !{0} start [Presses the start button if Photomath is open] | !{0} mathsub <digits> [Presses the specified buttons 'digits' in reading order (0-9) and submits the input to Photomath if Photomath is open] | !{0} call <user> [Calls the specified user 'user' if Discord is open] | !{0} mute <#> [Presses the mute button when the last digit of the bomb's timer is '#' if Discord is open] | !{0} symbol <#> [Presses the mute button when the specified symbol '#' is shown if Discord is open] | !{0} color <col> [Presses the mute button when the specified color 'col' is shown if Discord is open] | !{0} home [Goes back to the home screen] | !{0} submit <digits> [Submits the pin 'digits'] | !{0} regenerate [Presses the notification LED]";
    #pragma warning restore 414
    #pragma warning restore 0649
    IEnumerator ProcessTwitchCommand(string command)
    {
        if (Regex.IsMatch(command, @"^\s*home\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            if (!homebutton.gameObject.activeSelf)
            {
                yield return "sendtochaterror I am already on the home screen!";
            }
            else if (easterEgging || cantLeave || photocycle || strikeAnimating)
            {
                yield return "sendtochaterror I cannot go to the home screen right now!";
            }
            else
            {
                homebutton.OnInteract();
            }
            yield break;
        }
        if (Regex.IsMatch(command, @"^\s*regenerate\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            cantLeave = false;
            resetButton.OnInteract();
            yield break;
        }
        if (Regex.IsMatch(command, @"^\s*play\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            if (!apps[5].activeSelf)
            {
                yield return "sendtochaterror I cannot press the play button because Spotify is not open!";
                yield break;
            }
            if (isPlaying)
            {
                yield return "sendtochaterror I cannot press the play button because a snippet of a song is already playing!";
                yield break;
            }
            playbutton.OnInteract();
            yield break;
        }
        if (Regex.IsMatch(command, @"^\s*start\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            if (!apps[4].activeSelf)
            {
                yield return "sendtochaterror I cannot press the start button because Photomath is not open!";
                yield break;
            }
            if (photocycle)
            {
                yield return "sendtochaterror I cannot press the start button because Photomath is already cycling!";
                yield break;
            }
            photomathstart.OnInteract();
            yield break;
        }
        string[] parameters = command.Split(' ');
        if (Regex.IsMatch(parameters[0], @"^\s*submit\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            if (parameters.Length > 2)
            {
                yield return "sendtochaterror Too many parameters!";
            }
            else if (parameters.Length == 2)
            {
                int temp = 0;
                if (int.TryParse(parameters[1], out temp))
                {
                    if (temp < 0 || temp > 99999999)
                    {
                        yield return "sendtochaterror The specified code '" + parameters[1] + "' is not in range 0-99999999!";
                        yield break;
                    }
                    if (easterEgging || cantLeave || photocycle || strikeAnimating)
                    {
                        yield return "sendtochaterror I cannot submit the code right now!";
                        yield break;
                    }
                    if (homebutton.gameObject.activeSelf && !apps[8].activeSelf)
                    {
                        homebutton.OnInteract();
                        yield return new WaitForSeconds(0.1f);
                    }
                    if (!apps[8].activeSelf)
                    {
                        appButtons[8].OnInteract();
                        yield return new WaitForSeconds(0.1f);
                    }
                    if (enteringStage > 0)
                    {
                        clearbutton.OnInteract();
                        yield return new WaitForSeconds(0.1f);
                    }
                    for (int i = 0; i < parameters[1].Length; i++)
                    {
                        if (parameters[1].ElementAt(i).Equals('0'))
                            settingsbuttons[9].OnInteract();
                        else
                            settingsbuttons[int.Parse("" + parameters[1].ElementAt(i)) - 1].OnInteract();
                        yield return new WaitForSeconds(0.1f);
                    }
                    submitbutton.OnInteract();
                }
                else
                {
                    yield return "sendtochaterror The specified code '" + parameters[1] + "' is invalid!";
                }
            }
            else if (parameters.Length == 1)
            {
                yield return "sendtochaterror Please specify the code to submit!";
            }
            yield break;
        }
        if (Regex.IsMatch(parameters[0], @"^\s*mathsub\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            if (parameters.Length > 2)
            {
                yield return "sendtochaterror Too many parameters!";
            }
            else if (parameters.Length == 2)
            {
                int temp = 0;
                if (int.TryParse(parameters[1], out temp))
                {
                    if (temp < 0 || temp > 999)
                    {
                        yield return "sendtochaterror The specified buttons to press '" + parameters[1] + "' to submit as an answer for Photomath are not in range 0-999!";
                        yield break;
                    }
                    if (easterEgging || cantLeave || photocycle || strikeAnimating)
                    {
                        yield return "sendtochaterror I cannot press the specified buttons to submit as an answer to Photomath right now!";
                        yield break;
                    }
                    if (!apps[4].activeSelf)
                    {
                        yield return "sendtochaterror I cannot press the specified buttons to submit an answer to Photomath because Photomath is not open!";
                        yield break;
                    }
                    if (photomathsolutiontext.text.Length > 0)
                    {
                        photomathclear.OnInteract();
                        yield return new WaitForSeconds(0.1f);
                    }
                    for (int i = 0; i < parameters[1].Length; i++)
                    {
                        photomathbuttons[int.Parse("" + parameters[1].ElementAt(i))].OnInteract();
                        yield return new WaitForSeconds(0.1f);
                    }
                    photomathsubmit.OnInteract();
                }
                else
                {
                    yield return "sendtochaterror The specified buttons '" + parameters[1] + "' to press for Photomath are invalid!";
                }
            }
            else if (parameters.Length == 1)
            {
                yield return "sendtochaterror Please specify the buttons to press to submit as an answer to Photomath!";
            }
            yield break;
        }
        if (Regex.IsMatch(parameters[0], @"^\s*mute\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            if (parameters.Length > 2)
            {
                yield return "sendtochaterror Too many parameters!";
            }
            else if (parameters.Length == 2)
            {
                int temp = 0;
                if (int.TryParse(parameters[1], out temp))
                {
                    if (temp < 0 || temp > 9)
                    {
                        yield return "sendtochaterror The time to press the mute button '" + parameters[1] + "' for Discord is not in range 0-9!";
                        yield break;
                    }
                    if (easterEgging || !call.activeSelf || photocycle || strikeAnimating)
                    {
                        yield return "sendtochaterror I cannot press the mute button right now!";
                        yield break;
                    }
                    if (!apps[7].activeSelf)
                    {
                        yield return "sendtochaterror I cannot press the mute button because Discord is not open!";
                        yield break;
                    }
                    while ((int)bomb.GetTime() % 60 % 10 != temp) { yield return "trycancel Halted pressing the mute button due to a request to cancel!"; yield return new WaitForSeconds(0.1f); }
                    mutebutton.OnInteract();
                }
                else
                {
                    yield return "sendtochaterror The time to press the mute button '" + parameters[1] + "' for Discord is invalid!";
                }
            }
            else if (parameters.Length == 1)
            {
                yield return "sendtochaterror Please specify the time to press the mute button!";
            }
            yield break;
        }
        if (Regex.IsMatch(parameters[0], @"^\s*symbol\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            if (parameters.Length > 2)
            {
                yield return "sendtochaterror Too many parameters!";
            }
            else if (parameters.Length == 2)
            {
                int temp = 0;
                if (int.TryParse(parameters[1], out temp))
                {
                    if (temp < 0 || temp > 7)
                    {
                        yield return "sendtochaterror The symbol to press the mute button on '" + parameters[1] + "' for Discord is not in range 0-7!";
                        yield break;
                    }
                    if (easterEgging || !call.activeSelf || !cycling || strikeAnimating)
                    {
                        yield return "sendtochaterror I cannot press the mute button right now!";
                        yield break;
                    }
                    if (!apps[7].activeSelf)
                    {
                        yield return "sendtochaterror I cannot press the mute button because Discord is not open!";
                        yield break;
                    }
                    while (twitchtext.text != ("" + temp)) { yield return "trycancel Halted pressing the mute button due to a request to cancel!"; yield return new WaitForSeconds(0.1f); }
                    mutebutton.OnInteract();
                }
                else
                {
                    yield return "sendtochaterror The symbol to press the mute button on '" + parameters[1] + "' for Discord is invalid!";
                }
            }
            else if (parameters.Length == 1)
            {
                yield return "sendtochaterror Please specify the symbol to press the mute button on!";
            }
            yield break;
        }
        if (Regex.IsMatch(parameters[0], @"^\s*color\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            string[] colors = { "red", "orange", "yellow", "green", "blue", "purple" };
            if (parameters.Length > 2)
            {
                yield return "sendtochaterror Too many parameters!";
            }
            else if (parameters.Length == 2)
            {
                if (colors.Contains(parameters[1].ToLower()))
                {
                    if (easterEgging || !call.activeSelf || !cycling || strikeAnimating)
                    {
                        yield return "sendtochaterror I cannot press the mute button right now!";
                        yield break;
                    }
                    if (!apps[7].activeSelf)
                    {
                        yield return "sendtochaterror I cannot press the mute button because Discord is not open!";
                        yield break;
                    }
                    while (currentColor != Array.IndexOf(colors, parameters[1].ToLower())) { yield return "trycancel Halted pressing the mute button due to a request to cancel!"; yield return new WaitForSeconds(0.1f); }
                    mutebutton.OnInteract();
                }
                else
                {
                    yield return "sendtochaterror The color to press the mute button on '" + parameters[1] + "' for Discord is invalid!";
                }
            }
            else if (parameters.Length == 1)
            {
                yield return "sendtochaterror Please specify the color to press the mute button on!";
            }
            yield break;
        }
        if (Regex.IsMatch(parameters[0], @"^\s*call\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            string temp1 = "";
            if (parameters.Length == 1)
            {
                yield return "sendtochaterror Please specify the user you would like to call!";
                yield break;
            }
            if (parameters.Length > 2)
            {
                string param = "";
                for (int i = 1; i < parameters.Length; i++)
                {
                    param += parameters[i];
                }
                temp1 = parameters[1];
                parameters[1] = param;
            }
            string[] tempnames = discordNames.ToArray();
            for (int i = 0; i < tempnames.Length; i++)
            {
                tempnames[i] = tempnames[i].Replace(" ", "").ToLower();
            }
            if (!tempnames.Contains(parameters[1].ToLower()))
            {
                string prm = "";
                for (int i = 1; i < parameters.Length; i++)
                {
                    if (i == (parameters.Length - 1))
                    {
                        prm += parameters[i];
                    }
                    else if (i == 1)
                    {
                        prm += temp1 + " ";
                    }
                    else
                    {
                        prm += parameters[i] + " ";
                    }
                }
                yield return "sendtochaterror The specified user to call '" + prm + "' is invalid!";
            }
            else
            {
                if (easterEgging || cantLeave || photocycle || strikeAnimating)
                {
                    yield return "sendtochaterror I cannot call the specified user right now!";
                    yield break;
                }
                if (!apps[7].activeSelf)
                {
                    yield return "sendtochaterror I cannot call the specified user because Discord is not open!";
                    yield break;
                }
                bool done = false;
                for (int i = 0; i < pfpbuttons.Length; i++)
                {
                    if (pfprenders[i].material.mainTexture == pfpimages[Array.IndexOf(tempnames, parameters[1].ToLower())])
                    {
                        done = true;
                        pfpbuttons[i].OnInteract();
                    }
                }
                if (!done)
                {
                    yield return "sendtochaterror The specified user is not available right now!";
                }
            }
            yield break;
        }
        if (Regex.IsMatch(parameters[0], @"^\s*open\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            string temp1 = "";
            if (parameters.Length == 1)
            {
                yield return "sendtochaterror Please specify the application to open!";
                yield break;
            }
            else if (parameters.Length > 2)
            {
                string param = "";
                for (int i = 1; i < parameters.Length; i++)
                {
                    param += parameters[i];
                }
                temp1 = parameters[1];
                parameters[1] = param;
            }
            if (easterEgging || cantLeave || photocycle || strikeAnimating)
            {
                yield return "sendtochaterror I cannot open an application right now!";
                yield break;
            }
            if (Regex.IsMatch(parameters[1], @"^\s*duo\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant) || Regex.IsMatch(parameters[1], @"^\s*duolingo\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
            {
                if (!apps[0].activeSelf)
                {
                    if (homebutton.gameObject.activeSelf)
                    {
                        homebutton.OnInteract();
                        yield return new WaitForSeconds(0.1f);
                    }
                    appButtons[0].OnInteract();
                }
                else
                    yield return "sendtochaterror The application 'Duolingo' is already open!";
            }
            else if (Regex.IsMatch(parameters[1], @"^\s*maps\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant) || Regex.IsMatch(parameters[1], @"^\s*googlemaps\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
            {
                if (!apps[1].activeSelf)
                {
                    if (homebutton.gameObject.activeSelf)
                    {
                        homebutton.OnInteract();
                        yield return new WaitForSeconds(0.1f);
                    }
                    appButtons[1].OnInteract();
                }
                else
                    yield return "sendtochaterror The application 'Google Maps' is already open!";
            }
            else if (Regex.IsMatch(parameters[1], @"^\s*kindle\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
            {
                if (!apps[2].activeSelf)
                {
                    if (homebutton.gameObject.activeSelf)
                    {
                        homebutton.OnInteract();
                        yield return new WaitForSeconds(0.1f);
                    }
                    appButtons[2].OnInteract();
                }
                else
                    yield return "sendtochaterror The application 'Kindle' is already open!";
            }
            else if (Regex.IsMatch(parameters[1], @"^\s*auth\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant) || Regex.IsMatch(parameters[1], @"^\s*googleauthenticator\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
            {
                if (!apps[3].activeSelf)
                {
                    if (homebutton.gameObject.activeSelf)
                    {
                        homebutton.OnInteract();
                        yield return new WaitForSeconds(0.1f);
                    }
                    appButtons[3].OnInteract();
                }
                else
                    yield return "sendtochaterror The application 'Google Authenticator' is already open!";
            }
            else if (Regex.IsMatch(parameters[1], @"^\s*photo\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant) || Regex.IsMatch(parameters[1], @"^\s*photomath\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
            {
                if (!apps[4].activeSelf)
                {
                    if (homebutton.gameObject.activeSelf)
                    {
                        homebutton.OnInteract();
                        yield return new WaitForSeconds(0.1f);
                    }
                    appButtons[4].OnInteract();
                }
                else
                    yield return "sendtochaterror The application 'Photomath' is already open!";
            }
            else if (Regex.IsMatch(parameters[1], @"^\s*spotify\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
            {
                if (!apps[5].activeSelf)
                {
                    if (homebutton.gameObject.activeSelf)
                    {
                        homebutton.OnInteract();
                        yield return new WaitForSeconds(0.1f);
                    }
                    appButtons[5].OnInteract();
                }
                else
                    yield return "sendtochaterror The application 'Spotify' is already open!";
            }
            else if (Regex.IsMatch(parameters[1], @"^\s*arts\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant) || Regex.IsMatch(parameters[1], @"^\s*culture\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant) || Regex.IsMatch(parameters[1], @"^\s*a&c\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant) || Regex.IsMatch(parameters[1], @"^\s*googlearts&culture\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
            {
                if (!apps[6].activeSelf)
                {
                    if (homebutton.gameObject.activeSelf)
                    {
                        homebutton.OnInteract();
                        yield return new WaitForSeconds(0.1f);
                    }
                    appButtons[6].OnInteract();
                }
                else
                    yield return "sendtochaterror The application 'Google Arts & Culture' is already open!";
            }
            else if (Regex.IsMatch(parameters[1], @"^\s*discord\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
            {
                if (!apps[7].activeSelf)
                {
                    if (homebutton.gameObject.activeSelf)
                    {
                        homebutton.OnInteract();
                        yield return new WaitForSeconds(0.1f);
                    }
                    appButtons[7].OnInteract();
                }
                else
                    yield return "sendtochaterror The application 'Discord' is already open!";
            }
            else if (Regex.IsMatch(parameters[1], @"^\s*settings\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
            {
                if (!apps[8].activeSelf)
                {
                    if (homebutton.gameObject.activeSelf)
                    {
                        homebutton.OnInteract();
                        yield return new WaitForSeconds(0.1f);
                    }
                    appButtons[8].OnInteract();
                }
                else
                    yield return "sendtochaterror The application 'Settings' is already open!";
            }
            else
            {
                string prm = "";
                for (int i = 1; i < parameters.Length; i++)
                {
                    if (i == (parameters.Length - 1))
                    {
                        prm += parameters[i];
                    }
                    else if (i == 1)
                    {
                        prm += temp1 + " ";
                    }
                    else
                    {
                        prm += parameters[i] + " ";
                    }
                }
                yield return "sendtochaterror The specified application to open '" + prm + "' is invalid!";
            }
            yield break;
        }
    }

    IEnumerator TwitchHandleForcedSolve()
    {
        if (call.activeSelf && discordStage == 0)
        {
            while ((int)bomb.GetTime() % 60 % 10 != discordActivity) { yield return true; yield return new WaitForSeconds(0.1f); }
            mutebutton.OnInteract();
        }
        if (call.activeSelf && cycling)
        {
            while (currentSymbol != discordSymbol) { yield return true; yield return new WaitForSeconds(0.1f); }
            mutebutton.OnInteract();
            while (currentColor != discordColor) { yield return true; yield return new WaitForSeconds(0.1f); }
            mutebutton.OnInteract();
        }
        while (easterEgging || cantLeave || photocycle || strikeAnimating) { yield return true; yield return new WaitForSeconds(0.1f); }
        yield return ProcessTwitchCommand("submit " + solutionString);
    }

    class SamsungSettings
    {
        public bool noCopyright = false;
    }

    #pragma warning disable 414
    static Dictionary<string, object>[] TweaksEditorSettings = new Dictionary<string, object>[]
    {
        new Dictionary<string, object>
        {
            { "Filename", "SamsungSettings.json" },
            { "Name", "Samsung Settings" },
            { "Listing", new List<Dictionary<string, object>>{
                new Dictionary<string, object>
                {
                    { "Key", "noCopyright" },
                    { "Text", "If enabled all songs played in Spotify will be non-copyright (use non-copyright manual variant to solve Spotify with this setting)." }
                },
            } }
        }
    };
    #pragma warning restore 414

    IEnumerator PenisMusic()
    {
        foreach (GameObject x in fuckinEverything)
            x.SetActive(false);
        theFunny.SetActive(true);
        phonescreen.material.mainTexture = hoofduuoof;
        yield return null;
        for (int i = 0; i < 36000; i++)
        {
            cockAndNuts.text = (36000 - i).ToString();
            yield return new WaitForSeconds(1f);
        }
        phonescreen.material.mainTexture = currentWallpaper;
        fuckinEverything[0].SetActive(true);
        fuckinEverything[1].SetActive(true);
        unlocked = true;
        Start();
    }
}
