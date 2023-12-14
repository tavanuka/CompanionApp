using CompanionApp.Models;

namespace CompanionApp.States;

public class CalculationStateContainer : StateObject
{

    public bool ShowResultCard { get; private set; }
    
    public int NumberOfHoles { get; private set; } = 1;

    public double Dimension { get; private set; }

    public int Margin { get; private set; }

    public int MarginLeft { get; private set; }

    public int MarginRight { get; private set; }

    public int Offset { get; private set; }

    public decimal DistanceBetweenHoles { get; private set; }

    public decimal RoundedDistanceBetweenHoles { get; private set; }

    public bool LinkMargins { get; private set; } = true;

    public double SpacingFactor { get; private set; }

    public double SpacingDisplay { get; private set; }

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

    public void SetDimension(double value)
    {
        Dimension = value;
        NotifyStateChanged();
    }

    public void SetMargin(int value)
    {
        Margin = value;
        MarginLeft = value;
        NotifyStateChanged();
    }

    public void SetMarginLeft(int value)
    {
        MarginLeft = value;
        if (LinkMargins)
            MarginRight = value;
        NotifyStateChanged();
    }

    public void SetMarginRight(int value)
    {
        MarginRight = value;
        NotifyStateChanged();
    }

    public void SetOffset(int value)
    {
        Offset = value;
        NotifyStateChanged();
    }

    public void SetDistanceBetweenHoles(decimal value)
    {
        DistanceBetweenHoles = value;
        NotifyStateChanged();
    }

    public void SetRoundedDistanceBetweenHoles(decimal value)
    {
        RoundedDistanceBetweenHoles = value;
        NotifyStateChanged();
    }

    public void SetLinkMargins(bool value)
    {
        LinkMargins = value;
        NotifyStateChanged();
    }

    public void SetSpacingFactor(double value)
    {
        SpacingFactor = value;
        NotifyStateChanged();
    }

    public void SetSpacingDisplay(double value)
    {
        SpacingDisplay = value;
        NotifyStateChanged();
    }

    public void SetHoleDistances(IEnumerable<Hole> value)
    {
        Holes = value.AsQueryable();
        NotifyStateChanged();
    }
}