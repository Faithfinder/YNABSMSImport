using System;
using System.Collections.Generic;

public sealed class ExtractionKeys
{
    //TODO public static readonly ExtractionKeys Account = new ExtractionKeys(3, "Account", "(?<account>.*?)");

    public static readonly ExtractionKeys Amount = new ExtractionKeys(0, "Amount", "(?<amount>.*?)");
    public static readonly ExtractionKeys Payee = new ExtractionKeys(1, "Payee", "(?<payee>.*?)");
    public static readonly ExtractionKeys RandomText = new ExtractionKeys(2, "RandomText", "(.*?)");

    public readonly string regExPattern;

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
        return name;
    }

    private readonly string name;
    private readonly int value;

    private ExtractionKeys(int value, string name, string regExPattern)
    {
        this.name = name;
        this.value = value;
        this.regExPattern = regExPattern;
    }
}