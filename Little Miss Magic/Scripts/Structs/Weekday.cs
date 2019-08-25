using System;

[System.Serializable]
public struct Weekday
{
    public Weekday(Days day)
    {
        this.day = day;
    }

    public Weekday(int day)
    {
        this.day = (Days)day;
    }

    public enum Days
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    Days day;

    public override string ToString()
    {
        switch (day)
        {
            case Days.Monday:
                return "Monday";

            case Days.Tuesday:
                return "Tuesday";

            case Days.Wednesday:
                return "Wednesday";

            case Days.Thursday:
                return "Thursday";

            case Days.Friday:
                return "Friday";

            case Days.Saturday:
                return "Saturday";

            case Days.Sunday:
                return "Sunday";
        }

        return string.Empty;
    }

    #region 3rd party access to days

    public Days Monday()
    {
        return Days.Monday;
    }

    public Days Tuesday()
    {
        return Days.Tuesday;
    }

    public Days Wednesday()
    {
        return Days.Wednesday;
    }

    public Days Thursday()
    {
        return Days.Thursday;
    }

    public Days Friday()
    {
        return Days.Friday;
    }

    public Days Saturday()
    {
        return Days.Saturday;
    }

    public Days Sunday()
    {
        return Days.Sunday;
    }

    #endregion


}
