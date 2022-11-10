using System;
using static System.Console;
using System.Globalization;
class GreenvilleRevenue
{
    static void Main()
    {
        const int Fee = 25;
        const int MIN_CONTESTANTS = 0;
        const int MAX_CONTESTANTS = 30;
        int num;
        int revenue;
        Contestant[] contestants = new Contestant[MAX_CONTESTANTS];
        num = getContestantNumber(MIN_CONTESTANTS, MAX_CONTESTANTS);
        revenue = num * Fee;
        WriteLine("Revenue expected this year is {0}", revenue.ToString("C", CultureInfo.GetCultureInfo("en-US")));
        getContestantData(num, contestants, revenue);
        getLists(num, contestants);
    }

    private static int getContestantNumber(int min, int max)
    {
        string entryString;
        int num = max + 1;
        Write("Enter number of contestants >> ");
        entryString = ReadLine();
        while (num < min || num > max)
        {
            if (!int.TryParse(entryString, out num))
            {
                WriteLine("Format invalid");
                num = max + 1;
                Write("Enter number of contestants >> ");
                entryString = ReadLine();
            }
            else
            {
                if (num < min || num > max)
                {
                    WriteLine("Number must be between {0} and {1}", min, max);
                    num = max + 1;
                    Write("Enter number of contestants >> ");
                    entryString = ReadLine();
                }
            }
        }
        return num;
    }
    private static void getContestantData(int num, Contestant[] contestants, int revenue)
    {
        const int ADULTAGE = 17;
        const int TEENAGE = 12;
        int x = 0;
        int age;
        string name;
        char talent;
        while (x < num)
        {
            Write("Enter contestant name >> ");
            name = ReadLine();
            WriteLine("Talent codes are:");
            for (int y = 0; y < Contestant.talentCodes.Length; ++y)
                WriteLine("  {0}   {1}", Contestant.talentCodes[y], Contestant.talentStrings[y]);
            Write("       Enter talent code >> ");
            char.TryParse(ReadLine(), out talent);
            Write("Enter contestant's age>> ");
            int.TryParse(ReadLine(), out age);
            if (age > ADULTAGE)
                contestants[x] = new AdultContestant();
            else
                if (age > TEENAGE)
                contestants[x] = new TeenContestant();
            else 
            contestants[x] = new ChildContestant();
            contestants[x].Name = name;
            contestants[x].TalentCode = talent;
            revenue += contestants[x].Fee;
            ++x;
        }
    }
    private static void getLists(int num, Contestant[] contestants)
    {
        int x;
        char QUIT = 'Z';
        char option;
        bool isValid;
        int pos = 0;
        bool found;
        WriteLine("\nThe types of talent are:");
        for (x = 0; x < Contestant.talentStrings.Length; ++x)
            WriteLine("{0, -6}{1, -20}", Contestant.talentCodes[x], Contestant.talentStrings[x]);
        Write("\nEnter a talent type or {0} to quit >> ", QUIT);
        isValid = false;
        while (!isValid)
        {
            if (!char.TryParse(ReadLine(), out option))
            {
                isValid = false;
                WriteLine("Invalid format - entry must be a single character");
                Write("\nEnter a talent type or {0} to quit >> ", QUIT);
            }
            else
            {
                if (option == QUIT)
                    isValid = true;
                else
                {
                    for (int z = 0; z < Contestant.talentCodes.Length; ++z)
                    {
                        if (option == Contestant.talentCodes[z])
                        {
                            isValid = true;
                            pos = z;
                        }
                    }
                    if (!isValid)
                    {
                        WriteLine("{0} is not a valid code", option);
                        Write("\nEnter a talent type or {0} to quit >> ", QUIT);
                    }
                    else
                    {
                        WriteLine("\nContestants with talent {0} are:", Contestant.talentStrings[pos]);
                        found = false;
                        for (x = 0; x < num; ++x)
                        {
                            if (contestants[x].TalentCode == option)
                            {
                                WriteLine(contestants[x].Name);
                                found = true;
                            }
                        }
                        if (!found)
                            WriteLine("No contestants had talent {0}", Contestant.talentStrings[pos]);
                        isValid = false;
                        Write("\nEnter a talent type or {0} to quit >> ", QUIT);
                    }
                }
            }
        }
    }
}
class Contestant
{
    public static char[] talentCodes = { 'S', 'D', 'M', 'O' };
    public static string[] talentStrings = {"Singing", "Dancing",
           "Musical instrument", "Other"};
    public string Name { get; set; }
    private char talentCode;
    private string talent;
    public int Fee { get; set; }
    public char TalentCode
    {
        get
        {
            return talentCode;
        }
        set
        {
            int pos = talentCodes.Length;
            for (int x = 0; x < talentCodes.Length; ++x)
                if (value == talentCodes[x])
                    pos = x;
            if (pos == talentCodes.Length)
            {
                talentCode = 'I';
                talent = "Invalid";
            }
            else
            {
                talentCode = value;
                talent = talentStrings[pos];
            }
        }
    }
    public string Talent
    {
        get
        {
            return talent;
        }
    }
}
class ChildContestant : Contestant
{
    public int ChildFee = 15;
    public ChildContestant()
    {
        Fee = ChildFee;
    }
    public override string ToString()
    {
        return ("Child Contestant" + Name + TalentCode + "" + " Fee" + Fee.ToString("C", CultureInfo.GetCultureInfo("en-US")));
    }
}
class TeenContestant : Contestant
{
    public int TeenFee = 20;
    public TeenContestant()
    {
        Fee = TeenFee;
    }
    public override string ToString()
    {
        return ("Teen Contestant" + Name + TalentCode + "Fee " + Fee.ToString("C", CultureInfo.GetCultureInfo("en-US")));
    }
}
class AdultContestant : Contestant
{
    public int AdultFee = 30;
    public AdultContestant()
    {
        Fee = AdultFee;
    }
    public override string ToString()
    {
        return ("Adult Contestant" + Name + TalentCode + "" + "Fee" + Fee.ToString("C", CultureInfo.GetCultureInfo("en-US")));
    }
}



