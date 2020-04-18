public static class Discord
{
    public static readonly string[][] activityLineNames = new string[10][]
    {
        new string[10] { "tasdefuse", "tasjackbox", "tastabletop", "tasreaction", "tassleep", "tasexpert", "tasargument", "tasfood", "tasbrb", "tasmisc" },
        new string[10] { "deafdefuse", "deafjackbox", "deaftabletop", "deafreaction", "deafsleep", "deafexpert", "deafargument", "deaffood", "deafbrb", "deafmisc" },
        new string[10] { "blandefuse", "blanjackbox", "blantabletop", "blanreaction", "blansleep", "blanexpert", "blanargument", "blanfood", "blanbrb", "blanmisc" },
        new string[10] { "timwidefuse", "timwijackbox", "timwitabletop", "timwireaction", "timwisleep", "timwiexpert", "timwiargument", "timwifood", "timwibrb", "timwimisc" },
        new string[10] { "numdefuse", "numjackbox", "numtabletop", "numreaction", "numsleep", "numexpert", "numargument", "numfood", "numbrb", "nummisc" },
        new string[10] { "nicodefuse", "nicojackbox", "nicotabletop", "nicoreaction", "nicosleep", "nicoexpert", "nicoargument", "nicofood", "nicobrb", "nicomisc" },
        new string[10] { "espikdefuse", "espikjackbox", "espiktabletop", "espikreaction", "espiksleep", "espikexpert", "espikargument", "espikfood", "espikbrb", "espikmisc" },
        new string[10] { "procyondefuse", "procyonjackbox", "procyontabletop", "procyonreaction", "procyonsleep", "procyonexpert", "procyonargument", "procyonfood", "procyonbrb", "procyonmisc" },
        new string[10] { "exishdefuse", "exishjackbox", "exishtabletop", "exishreaction", "exishsleep", "exishexpert", "exishargument", "exishfood", "exishbrb", "exishmisc" },
        new string[10] { "sillydefuse", "sillyjackbox", "sillytabletop", "sillyreaction", "sillysleep", "sillyexpert", "sillyargument", "sillyfood", "sillybrb", "sillymisc" }
    };
    public static readonly float[][] activityLengths = new float[10][]
    {
        new float[10] { 5.5f, 4.5f, 4.5f, 3.5f, 5.5f, 7.5f, 5.5f, 3.5f, 2.5f, 3.5f },
        new float[10] { 4.5f, 5.5f, 2.5f, 3.5f, 3.5f, 4.5f, 6.5f, 2.5f, 3.5f, 8.5f },
        new float[10] { 6.5f, 4.5f, 4.5f, 4.5f, 3.5f, 8.5f, 5.5f, 5.5f, 7.5f, 12.5f },
        new float[10] { 6.5f, 2.5f, 1.5f, 3.5f, 5.5f, 3.5f, 1.5f, 2.5f, 1.5f, 2.5f },
        new float[10] { 2.5f, 3.5f, 1.5f, 3.5f, 2.5f, 1.5f, 2.5f, 2.5f, 2.5f, 16.5f },
        new float[10] { 10.5f, 16.5f, 10.5f, 7.5f, 7.5f, 13.5f, 3.5f, 11.5f, 3.5f, 9.5f },
        new float[10] { 2.5f, 2.5f, 1.5f, 1.5f, 1.5f, 2.5f, 2.5f, 1.5f, 1.5f, 2.5f },
        new float[10] { 1.5f, 3.5f, 2.5f, 4.5f, 4.5f, 8.5f, 4.5f, 4.5f, 4.5f, 3.5f },
        new float[10] { 3.5f, 3.5f, 4.5f, 4.5f, 3.5f, 2.5f, 3.5f, 2.5f, 3.5f, 5.5f },
        new float[10] { 2.5f, 3.5f, 2.5f, 2.5f, 3.5f, 1.5f, 1.5f, 2.5f, 1.5f, 2.5f },
    };

    public static readonly string[][] digitLineNames = new string[10][]
    {
        new string[10] { "tas0", "tas1", "tas2", "tas3", "tas4", "tas5", "tas6", "tas7", "tas8", "tas9" },
        new string[10] { "deaf0", "deaf1", "deaf2", "deaf3", "deaf4", "deaf5", "deaf6", "deaf7", "deaf8", "deaf9" },
        new string[10] { "blan0", "blan1", "blan2", "blan3", "blan4", "blan5", "blan6", "blan7", "blan8", "blan9" },
        new string[10] { "timwi0", "timwi1", "timwi2", "timwi3", "timwi4", "timwi5", "timwi6", "timwi7", "timwi8", "timwi9" },
        new string[10] { "num0", "num1", "num2", "num3", "num4", "num5", "num6", "num7", "num8", "num9" },
        new string[10] { "nico0", "nico1", "nico2", "nico3", "nico4", "nico5", "nico6", "nico7", "nico8", "nico9" },
        new string[10] { "espik0", "espik1", "espik2", "espik3", "espik4", "espik5", "espik6", "espik7", "espik8", "espik9" },
        new string[10] { "procyon0", "procyon1", "procyon2", "procyon3", "procyon4", "procyon5", "procyon6", "procyon7", "procyon8", "procyon9" },
        new string[10] { "exish0", "exish1", "exish2", "exish3", "exish4", "exish5", "exish6", "exish7", "exish8", "exish9" },
        new string[10] { "silly0", "silly1", "silly2", "silly3", "silly4", "silly5", "silly6", "silly7", "silly8", "silly9" }
    };
    public static readonly float[][] digitLengths = new float[10][]
    {
        new float[10] { 3.5f, 2.5f, 3.5f, 3.5f, 1.5f, 2.5f, 4f, 2.5f, 5.5f, 5.5f },
        new float[10] { 2.5f, 1.5f, 3.5f, 3.5f, 2.5f, 2.5f, 2.5f, 5.5f, 2.5f, 7.5f },
        new float[10] { 3.5f, 4.5f, 3.5f, 4.5f, 3.5f, 4.5f, 3.5f, 5.5f, 2.5f, 5.5f },
        new float[10] { 3.5f, 1.5f, 3.5f, 2.5f, 1.5f, 3.5f, 4.5f, 5.5f, 3.5f, 4.5f },
        new float[10] { 2.5f, 2.5f, 1.5f, 2.5f, 3.5f, 5.5f, 3.5f, 9.5f, 4.5f, 3.5f },
        new float[10] { 9.5f, 8.5f, 10.5f, 10.5f, 8.5f, 6.5f, 8.5f, 9.5f, 13.5f, 5.5f },
        new float[10] { 2.5f, 1.5f, 3.5f, 1.5f, 2.5f, 3.5f, 3.5f, 1.5f, 1.5f, 2.5f },
        new float[10] { 2.5f, 2.5f, 1.5f, 1.5f, 1.5f, 2.5f, 3.5f, 3.5f, 1.5f, 2.5f },
        new float[10] { 2.5f, 1.5f, 2.5f, 2.5f, 3.5f, 2.5f, 3.5f, 3.5f, 2.5f, 3.5f },
        new float[10] { 2.5f, 2.5f, 1.5f, 2.5f, 1.5f, 3.5f, 2.5f, 1.5f, 2.5f, 2.5f },
    };

    public static readonly string[][] colorLineNames = new string[10][]
    {
        new string[6] { "tasred", "tasorange", "tasyellow", "tasgreen", "tasblue", "taspurple" },
        new string[6] { "deafred", "deaforange", "deafyellow", "deafgreen", "deafblue", "deafpurple" },
        new string[6] { "blanred", "blanorange", "blanyellow", "blangreen", "blanblue", "blanpurple" },
        new string[6] { "timwired", "timwiorange", "timwiyellow", "timwigreen", "timwiblue", "timwipurple" },
        new string[6] { "numred", "numorange", "numyellow", "numgreen", "numblue", "numpurple" },
        new string[6] { "nicored", "nicoorange", "nicoyellow", "nicogreen", "nicoblue", "nicopurple" },
        new string[6] { "espikred", "espikorange", "espikyellow", "espikgreen", "espikblue", "espikpurple" },
        new string[6] { "procyonred", "procyonorange", "procyonyellow", "procyongreen", "procyonblue", "procyonpurple" },
        new string[6] { "exishred", "exishorange", "exishyellow", "exishgreen", "exishblue", "exishpurple" },
        new string[6] { "sillyred", "sillyorange", "sillyyellow", "sillygreen", "sillyblue", "sillypurple" }
    };
    public static readonly float[][] colorLengths = new float[10][]
    {
        new float[6] { 3.5f, 5.5f, 3.5f, 7.5f, 5.5f, 4.5f },
        new float[6] { 1.5f, 2.5f, 3.9f, 4.5f, 3.5f, 4.5f },
        new float[6] { 3.5f, 3.5f, 3.5f, 4.5f, 4.5f, 3.5f },
        new float[6] { 2.5f, 1.75f, 3.5f, 3.5f, 2.5f, 4.5f },
        new float[6] { 2.5f, 2.5f, 1.5f, 1.5f, 1.5f, 2.5f },
        new float[6] { 3.5f, 4.5f, 5.5f, 3.5f, 3.5f, 5.5f },
        new float[6] { 2.5f, 1.5f, 2.5f, 2.5f, 1.5f, 3.5f },
        new float[6] { 1.5f, 1.5f, 2.5f, 1.5f, 1.5f, 1.5f },
        new float[6] { 2.5f, 2.5f, 3.5f, 2.5f, 2.5f, 1.5f },
        new float[6] { 1.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f },
    };

    public static readonly string[][] symbolLineNames = new string[10][]
    {
        new string[8] { "tassymbol1", "tassymbol2", "tassymbol3", "tassymbol4", "tassymbol5", "tassymbol6", "tassymbol7", "tassymbol8" },
        new string[8] { "deafsymbol1", "deafsymbol2", "deafsymbol3", "deafsymbol4", "deafsymbol5", "deafsymbol6", "deafsymbol7", "deafsymbol8" },
        new string[8] { "blansymbol1", "blansymbol2", "blansymbol3", "blansymbol4", "blansymbol5", "blansymbol6", "blansymbol7", "blansymbol8" },
        new string[8] { "timwisymbol1", "timwisymbol2", "timwisymbol3", "timwisymbol4", "timwisymbol5", "timwisymbol6", "timwisymbol7", "timwisymbol8" },
        new string[8] { "numsymbol1", "numsymbol2", "numsymbol3", "numsymbol4", "numsymbol5", "numsymbol6", "numsymbol7", "numsymbol8" },
        new string[8] { "nicosymbol1", "nicosymbol2", "nicosymbol3", "nicosymbol4", "nicosymbol5", "nicosymbol6", "nicosymbol7", "nicosymbol8" },
        new string[8] { "espiksymbol1", "espiksymbol2", "espiksymbol3", "espiksymbol4", "espiksymbol5", "espiksymbol6", "espiksymbol7", "espiksymbol8" },
        new string[8] { "procyonsymbol1", "procyonsymbol2", "procyonsymbol3", "procyonsymbol4", "procyonsymbol5", "procyonsymbol6", "procyonsymbol7", "procyonsymbol8" },
        new string[8] { "exishsymbol1", "exishsymbol2", "exishsymbol3", "exishsymbol4", "exishsymbol5", "exishsymbol6", "exishsymbol7", "exishsymbol8" },
        new string[8] { "sillysymbol1", "sillysymbol2", "sillysymbol3", "sillysymbol4", "sillysymbol5", "sillysymbol6", "sillysymbol7", "sillysymbol8" }
    };
    public static readonly float[][] symbolLengths = new float[10][]
    {
        new float[8] { 4.5f, 3.5f, 3.5f, 3.5f, 3.5f, 4.5f, 4.5f, 3.5f },
        new float[8] { 6.5f, 2.5f, 3.5f, 2.5f, 7.5f, 4.5f, 3.5f, 4.5f },
        new float[8] { 5.5f, 3.5f, 3.5f, 6.5f, 5.5f, 7.5f, 6.5f, 8.5f },
        new float[8] { 3.5f, 2.5f, 3.5f, 1f, 3.5f, 3.5f, 3.5f, 2.5f },
        new float[8] { 2.5f, 1.5f, 2.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f },
        new float[8] { 15.5f, 11.5f, 10.5f, 15.5f, 12.5f, 10.5f, 11.5f, 11.5f },
        new float[8] { 2.5f, 2.5f, 2.5f, 2.5f, 3.5f, 3.5f, 2.5f, 2.5f },
        new float[8] { 1.5f, 1.5f, 2.5f, 1f, 1f, 2.5f, 1.5f, 1.5f },
        new float[8] { 5.5f, 4.5f, 1.5f, 2.5f, 2.5f, 2.5f, 3.5f, 2.5f },
        new float[8] { 3.5f, 3.5f, 2.5f, 2.5f, 4.5f, 5.5f, 4.5f, 1.5f },
    };

    public static readonly string[] busyLineNames = new string[10] { "tasbusy", "deafbusy", "blanbusy", "timwibusy", "numbusy", "nicobusy", "espikbusy", "procyonbusy", "exishbusy", "sillybusy" };
    public static readonly float[] busyLengths = new float[10] { 3.5f, 1f, 5.5f, 2f, 3.5f, 5.5f, 1.5f, 3.5f, 1.5f, 1.9f };
}
