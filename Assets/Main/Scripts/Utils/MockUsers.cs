public static class MockUsers
{
    private static readonly string[] Array = {
"595.080.896-84", "412.289.303-97", "785.797.738-73", "331.154.515-63", "114.795.138-29", "489.223.292-02",  "188.671.817-43", "225.117.078-30", "568.803.932-81", "220.239.365-57", "400.270.483-13", "870.983.116-89", "994.694.622-83", "799.150.507-71", "721.793.641-40", "801.413.139-04", "638.404.703-23", "091.050.995-90", "107.127.967-28", "812.543.271-98", "231.939.154-91", "734.568.731-09"
    };

    private static int CurrentIndex { get; set; } = 0;

    public static string GetCurrent()
    {
        return Array[CurrentIndex];
    }

    public static string GetNext()
    {
        CurrentIndex++;
        CurrentIndex = CurrentIndex % Array.Length;
        return Array[CurrentIndex];
    }
}