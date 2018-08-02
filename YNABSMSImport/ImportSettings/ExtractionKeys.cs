using System;
using System.Collections.Generic;

public sealed class ExtractionKeys
{
    //TODO public static readonly ExtractionKeys Account = new ExtractionKeys(3, "Account", "(?<account>.*?)");

    public static readonly ExtractionKeys Amount = new ExtractionKeys(0, "Amount", "(?<amount>.*?)");
    public static readonly ExtractionKeys Payee = new ExtractionKeys(1, "Payee", "(?<payee>.*?)");
    public static readonly ExtractionKeys RandomText = new ExtractionKeys(2, "RandomText", "(.*?)");

    public readonly string RegExPattern;

    public static List<ExtractionKeys> GetAll()
    {
        var list = new List<ExtractionKeys>
        {
            Amount,
            Payee,
            RandomText
        };
        return list;
    }

    public override string ToString()
    {
        return _name;
    }

    private readonly string _name;
    private readonly int _value;

    private ExtractionKeys(int value, string name, string regExPattern)
    {
        _name = name;
        _value = value;
        RegExPattern = regExPattern;
    }
}