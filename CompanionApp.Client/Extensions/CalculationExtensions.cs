namespace CompanionApp.Client.Extensions;

public static class CalculationExtensions
{
    /// <summary>
    /// Shifts the decimal point of an integer. 
    /// </summary>
    /// <param name="value">The value to shift.</param>
    /// <param name="count">Amount of decimal places to shift by. Negative number shifts the number to the left.</param>
    /// <returns>Shifted value as a float.</returns>
    public static float ShiftDecimalPoint(this int value, int count) => value / MathF.Pow(10, count);
    public static float ShiftDecimalPoint(this float value, int count) => value / MathF.Pow(10, count);
    public static decimal ShiftDecimalPoint(this decimal value, int count) => value / (decimal)MathF.Pow(10, count);
    public static float ShiftDecimalPoint(this double value, int count) => (float)value / MathF.Pow(10, count);
}