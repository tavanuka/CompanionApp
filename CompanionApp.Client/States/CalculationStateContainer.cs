using CompanionApp.Client.Models;

namespace CompanionApp.Client.States;

public class CalculationStateContainer : StateObject
{
    public bool ShowResultCard { get; private set; }

    public int NumberOfHoles { get; private set; } = 1;

    public double Length { get; private set; }

    public int Margin { get; private set; }

    public double MarginLeft { get; private set; }

    public double MarginRight { get; private set; }

    public int Offset { get; private set; }

    public decimal Interval { get; private set; }

    public decimal RoundedInterval { get; private set; }

    public bool LinkMargins { get; private set; } = true;

    public double IntervalFactor { get; private set; }

    public double DisplayInterval { get; private set; }

    public IQueryable<Hole>? Holes { get; private set; }

    public void SetShowResultCard(bool value)
    {
        ShowResultCard = value;
        NotifyStateChanged();
    }

    public void SetNumberOfHoles(int value)
    {
        NumberOfHoles = value;
        NotifyStateChanged();
    }

    public void SetLength(double value)
    {
        Length = value;
        NotifyStateChanged();
    }

    public void SetMargin(int value)
    {
        Margin = value;
        MarginLeft = value;
        NotifyStateChanged();
    }

    public void SetMarginLeft(double value)
    {
        MarginLeft = value;
        if (LinkMargins)
            MarginRight = value;
        NotifyStateChanged();
    }

    public void SetMarginRight(double value)
    {
        MarginRight = value;
        NotifyStateChanged();
    }

    public void SetOffset(int value)
    {
        Offset = value;
        NotifyStateChanged();
    }

    public void SetInterval(decimal value)
    {
        Interval = value;
        NotifyStateChanged();
    }

    public void SetRoundedInterval(decimal value)
    {
        RoundedInterval = value;
        NotifyStateChanged();
    }

    public void SetLinkMargins(bool value)
    {
        LinkMargins = value;
        NotifyStateChanged();
    }

    public void SetIntervalFactor(double value)
    {
        IntervalFactor = value;
        NotifyStateChanged();
    }

    public void SetDisplayInterval(double value)
    {
        DisplayInterval = value;
        NotifyStateChanged();
    }

    public void SetHoles(IEnumerable<Hole> value)
    {
        Holes = value.AsQueryable();
        NotifyStateChanged();
    }
}