using CompanionApp.Client.Enums;
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
    /// <param name="offset">The direction of the offset to calculate.</param>
    /// <returns>The offset to subtract from length.</returns>
    int CalculateOffset(double length, int holeCount, double marginLeft = 0D, double marginRight = 0D,
        Offset offset = Offset.Negative);

    /// <summary>
    ///     Generates a list of holes that describes where should they be placed along the given length.
    /// </summary>
    /// <param name="roundedInterval">Rounded distance between given number of holes.</param>
    /// <param name="interval">Distance between given number of holes.</param>
    /// <param name="holeCount">Number of holes on the axis.</param>
    /// <param name="offset">Optional. Starting offset to be added between start and the first hole.</param>
    /// <param name="marginLeft">Optional. Distance between start and the first hole.</param>
    /// <returns>Returns a list of type <see cref="Hole" /></returns>
    IEnumerable<Hole> GenerateHoles(decimal roundedInterval, decimal interval, int holeCount, int offset = 0,
        double marginLeft = 0D);
}

public class HoleCalculationService : IHoleCalculationService
{
    public int CalculateOffset(double length, int holeCount, double marginLeft = 0D,
        double marginRight = 0D, Offset offset = Offset.Negative)
    {
        // Validate number of points
        if (holeCount < 1)
            return 0;

        decimal interval;
        var intervalList = new List<decimal>();
        
        for (var i = 0; i < holeCount; i++)
        {
            interval = offset switch
            {
                Offset.Positive => marginLeft == 0 && marginRight == 0
                    ? CalculateDistance(length + i, holeCount)
                    : CalculateDistance(length + i, holeCount, marginLeft, marginRight),
                Offset.Negative => marginLeft == 0 && marginRight == 0
                    ? CalculateDistance(length - i, holeCount)
                    : CalculateDistance(length - i, holeCount, marginLeft, marginRight),
                _ => throw new ArgumentOutOfRangeException(nameof(offset), offset, null)
            };

            interval = Math.Round(interval, 1);
            intervalList.Add(interval);
        }

        // Determines which rounded number is of modulo 1 and returns the index of this number as offset.  
        return intervalList.IndexOf(intervalList.FirstOrDefault(x => x % 1 == 0));
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

    public IEnumerable<Hole> GenerateHoles(decimal roundedInterval, decimal interval, int holeCount, int offset = 0,
        double marginLeft = 0D)
    {
        var holeList = new List<Hole>();
        decimal holeInterval;
        decimal roundedHoleInterval;

        if (marginLeft is not 0)
        {
            holeInterval = (decimal)marginLeft;
            roundedHoleInterval = (decimal)(marginLeft + offset);
        }
        else
        {
            holeInterval = interval;
            roundedHoleInterval = roundedInterval + offset;
        }

        for (var i = 1; i <= holeCount; i++)
        {
            holeList.Add(new Hole(i, holeInterval, roundedHoleInterval));
            holeInterval += interval;
            roundedHoleInterval += roundedInterval;
        }

        return holeList;
    }
}