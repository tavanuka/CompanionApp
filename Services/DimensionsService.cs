namespace CompanionApp.Services;

public interface IDimensionsService
{
    double CalculateGap(double dimension, double marginLeft, double marginRight, int pointsCount);
}

public class DimensionsService : IDimensionsService
{
    public double CalculateGap(double dimension, double marginLeft, double marginRight, int pointsCount) =>
        pointsCount != 1
            ? (dimension - (marginLeft + marginRight)) / (pointsCount - 1)
            : (dimension - (marginLeft + marginRight)) / 2; // sets the point right in the middle 
}