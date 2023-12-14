using CompanionApp.Client.Models;

namespace CompanionApp.Client.Services;

public interface IHoleCalculationService
{
    /// <summary>
    ///     Calculates the distance between the given number of holes for equal distribution across a length. If no margin is
    ///     given, it distributes the the holes equally across the given length.
    /// </summary>
    /// <param name="length">The length of the axis.</param>
    /// <param name="holeCount">Number of holes on the axis.</param>
    /// <param name="marginLeft">Optional. Distance between start and the first hole.</param>
    /// <param name="marginRight">Optional. Distance between the end and the last hole.</param>
    /// <returns>The distance needed between the holes.</returns>
    decimal CalculateDistance(double length, int holeCount, double marginLeft = 0D, double marginRight = 0D);

    /// <summary>
    ///     Calculates the amount that should be subtracted from the length to equally distribute points without it resulting
    ///     in a floating point number.
    /// </summary>
    /// <param name="length">The length of the axis.</param>
    /// <param name="holeCount">Number of holes on the axis.</param>
    /// <param name="marginLeft">Distance between start and the first hole.</param>
    /// <param name="marginRight">Distance between the end and the last hole.</param>
    /// <returns>The offset to subtract from length.</returns>
    int CalculateOffset(double length, int holeCount, double marginLeft = 0D, double marginRight = 0D);

    /// <summary>
    ///     Generates a list of holes that describes where should they be placed along the given length.
    /// </summary>
    /// <param name="roundedDistance">Rounded distance between given number of holes.</param>
    /// <param name="distance">Distance between given number of holes.</param>
    /// <param name="holeCount">Number of holes on the axis.</param>
    /// <param name="offset">Optional. Starting offset to be added between start and the first hole.</param>
    /// <param name="marginLeft">Optional. Distance between start and the first hole.</param>
    /// <returns>Returns a list of type <see cref="Hole" /></returns>
    IEnumerable<Hole> GenerateHoles(decimal roundedDistance, decimal distance, int holeCount, int offset = 0,
        int marginLeft = 0);
}

public class HoleCalculationService : IHoleCalculationService
{
    public int CalculateOffset(double length, int holeCount, double marginLeft = 0D, double marginRight = 0D)
    {
        // Validate number of points
        if (holeCount < 1)
            return 0;

        var offset = 0;
        decimal gap;

        do
        {
            gap = marginLeft == 0 && marginRight == 0
                ? CalculateDistance(length - offset, holeCount)
                : CalculateDistance(length - offset, holeCount, marginLeft, marginRight);

            if (gap % 1 != 0) offset++;
        } while (gap % 1 != 0);

        return offset;
    }


    public decimal CalculateDistance(double length, int holeCount, double marginLeft = 0,
        double marginRight = 0)
    {
        // Validate number of points
        if (holeCount < 1)
            return 0;

        // Check for margins
        if (marginLeft == 0 && marginRight == 0)
            return holeCount switch
            {
                1 => (decimal)(length / 2), // Sets the point in the middle
                _ => (decimal)(length / (holeCount + 1))
            };

        return holeCount switch
        {
            1 => (decimal)((length - (marginLeft + marginRight)) / 2), // Also sets it in the middle
            _ => (decimal)((length - (marginLeft + marginRight)) / (holeCount - 1))
        };
    }

    public IEnumerable<Hole> GenerateHoles(decimal roundedDistance, decimal distance, int holeCount, int offset = 0,
        int marginLeft = 0)
    {
        var listOfHoles = new List<Hole>();
        decimal holeDistance;
        decimal roundedHoleDistance;

        if (marginLeft is not 0)
        {
            holeDistance = marginLeft;
            roundedHoleDistance = marginLeft + offset;
        }
        else
        {
            holeDistance = distance;
            roundedHoleDistance = roundedDistance + offset;
        }

        for (var i = 1; i <= holeCount; i++)
        {
            listOfHoles.Add(new Hole(i, holeDistance, roundedHoleDistance));
            holeDistance += distance;
            roundedHoleDistance += roundedDistance;
        }

        return listOfHoles;
    }
}