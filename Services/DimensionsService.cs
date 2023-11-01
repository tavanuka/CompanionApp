namespace CompanionApp.Services;

public interface IDimensionsService
{
    double CalculateGap(int dimension, int marginLeft, int marginRight, int pointsCount);
}

public class DimensionsService : IDimensionsService
{
    public double CalculateGap(int dimension, int marginLeft, int marginRight, int pointsCount) =>
        pointsCount != 1
            ? (double)(dimension - (marginLeft + marginRight)) / (pointsCount - 1)
            : (double)(dimension - (marginLeft + marginRight)) / 2; // sets the point right in the middle 
}