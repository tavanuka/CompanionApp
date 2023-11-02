namespace CompanionApp;

public static class Int32Extensions
{
    /// <summary>
    /// Shifts the decimal point of an integer. 
    /// </summary>
    /// <param name="value">The value to shift.</param>
    /// <param name="count">Amount of decimal places to shift by. Negative number shifts the number to the left.</param>
    /// <returns>Shifted value as a float.</returns>
    public static float ShiftDecimalPoint(this int value, int count) => value / MathF.Pow(10, count);
}