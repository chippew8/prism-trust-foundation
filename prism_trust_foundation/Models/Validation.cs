using System.ComponentModel.DataAnnotations;

public class AllowedExtension : ValidationAttribute
{
    private readonly string[] _extensions;

    public AllowedExtension(string[] extensions)
    {
        _extensions = extensions;
    }
}

public class IsNRIC : ValidationAttribute
{

    public IsNRIC()
    {
    }

    public override bool IsValid(object value)
    {
        var NRIC = value as string;
        string[] letters = new string[] { "F", "G", "T", "S" };
        int[] weights = new int[] { 2, 7, 6, 5, 4, 3, 2 };
        string check1 = "JZIHGFEDCBA";
        string check2 = "XWUTRQPNMLK";

        if (NRIC == null || NRIC.Length != 9)
        {
            return false;
        }
        char f = NRIC[0];
        if (f != 'S' && f != 'T' && f != 'F' && f != 'G')
        {
            return false;
        }

        char l = NRIC[8];
        int sum = 0;

        for (var i = 1; i < 8; i++)
        {
            sum += int.Parse(NRIC[i].ToString()) * weights[i - 1];
        }
        if (f == 'T' || f == 'G')
        {
            sum += 4;
        }
        int remainder = sum % 11;
        char letter = 'a';
        if (f == 'S' || f == 'T')
        {
            letter = check1[remainder];
        }
        else if (f == 'F' || f == 'G')
        {
            letter = check2[remainder];
        }

        return letter == l;
    }
}