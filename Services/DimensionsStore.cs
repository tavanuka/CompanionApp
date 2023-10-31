namespace CompanionApp.Services;

public class DimensionsStore
{
    private readonly IDimensionsService _dimensionsService;

    public double Dimension { get; set; }
    public double MarginLeft { get; set; }
    public double MarginRight { get; set; }
    public int PointsCount { get; set; }
    public double Gap => _dimensionsService.CalculateGap(Dimension, MarginLeft, MarginRight, PointsCount);

    public DimensionsStore(IDimensionsService dimensionsService)
    {
        _dimensionsService = dimensionsService;
    }
}