using CompanionApp.Enums;

namespace CompanionApp.States;

public class CalculationSettingsStateContainer : StateObject
{
    public MeasurementType MeasurementType { get; private set; } = MeasurementType.Millimeter;
    public bool PreciseMeasurement { get; private set; }
    public bool PreciseMargin { get; private set; }
    public bool MarginSlider { get; private set; } = true;

    public void SetMeasurementType(MeasurementType measurementType)
    {
        MeasurementType = measurementType;
        NotifyStateChanged();
    }

    public void SetPreciseMeasurement(bool value)
    {
        PreciseMeasurement = value;
        NotifyStateChanged();
    }

    public void SetPreciseMargin(bool value)
    {
        PreciseMargin = value;
        NotifyStateChanged();
    }

    public void SetMarginSlider(bool value)
    {
        MarginSlider = value;
        NotifyStateChanged();
    }
}