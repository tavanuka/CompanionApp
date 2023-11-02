namespace CompanionApp.Services;

public interface IDimensionsService
{
    /// <summary>
    /// Calculates the gap between the given number of points for equal distribution across a length in millimeters.
    /// It considers both margins. If the number of points equals one, it calculates the middle.
    /// </summary>
    /// <param name="dimension">The length of the item in millimeters.</param>
    /// <param name="marginLeft">Left offset of where the hole should be drilled in millimeters.</param>
    /// <param name="marginRight">right offset of where the hole should be drilled in millimeters.</param>
    /// <param name="pointsCount">Number of holes to drill.</param>
    /// <returns>The amount of space that should be given between points in millimeters.</returns>
    int CalculateGapInMillimeters(int dimension, int marginLeft, int marginRight, int pointsCount);
}

public class DimensionsService : IDimensionsService
{
    public int CalculateGapInMillimeters(int dimension, int marginLeft, int marginRight, int pointsCount) =>
        pointsCount != 1
            ? (dimension - (marginLeft + marginRight)) / (pointsCount - 1)
            : (dimension - (marginLeft + marginRight)) / 2; // sets the point right in the middle 
}