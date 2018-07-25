using System;

public sealed class ExtractionKeys
{
    public static readonly ExtractionKeys Account = new ExtractionKeys(2, "Account");
    public static readonly ExtractionKeys Amount = new ExtractionKeys(0, "Amount");
    public static readonly ExtractionKeys Payee = new ExtractionKeys(1, "Payee");
    public static readonly ExtractionKeys RandomText = new ExtractionKeys(2, "RandomText");

    public override String ToString()
    {
        return name;
    }

    private readonly String name;
    private readonly int value;

    private ExtractionKeys(int value, String name)
    {
        this.name = name;
        this.value = value;
    }
}