using CompanionApp.Client.Components;
using CompanionApp.Client.Services;
using CompanionApp.Client.States;
using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;

namespace CompanionApp.Client.Pages;

public partial class Calculator
{
    [Inject] public IDialogService DialogService { get; set; } = default!;
    [Inject] public CalculationSettingsStateContainer Settings { get; set; } = default!;
    [Inject] public CalculationStateContainer State { get; set; } = default!;
    [Inject] public IHoleCalculationService Service { get; set; } = default!;

    public void Dispose()
    {
        Settings.OnStateChange -= StateHasChanged;
    }

    protected override void OnInitialized()
    {
        Settings.OnStateChange += StateHasChanged;
        base.OnInitialized();
    }

    private void Calculate()
    {
        var offset = Service.CalculateOffset(State.Dimension,
            State.NumberOfHoles,
            Settings.PreciseMargin ? State.MarginLeft : State.Margin,
            Settings.PreciseMargin ? State.MarginRight : State.Margin);
        State.SetOffset(offset);

        var roundedDistance = Service.CalculateDistance(
            State.Dimension - offset, State.NumberOfHoles,
            Settings.PreciseMargin ? State.MarginLeft : State.Margin,
            Settings.PreciseMargin ? State.MarginRight : State.Margin);
        State.SetRoundedDistanceBetweenHoles(roundedDistance);

        var distance = Service.CalculateDistance(State.Dimension,
            State.NumberOfHoles, Settings.PreciseMargin ? State.MarginLeft : State.Margin,
            Settings.PreciseMargin ? State.MarginRight : State.Margin);
        State.SetDistanceBetweenHoles(distance);

        var holes = Service.GenerateHoles(State.RoundedDistanceBetweenHoles, State.DistanceBetweenHoles,
            State.NumberOfHoles, State.Offset, Settings.PreciseMargin ? State.MarginLeft : State.Margin);
        State.SetHoleDistances(holes);

        State.SetShowResultCard(true);
    }

    private async Task OpenCalculationSettings()
    {
        DialogParameters parameters = new()
        {
            Title = "Calculation settings",
            PrimaryAction = "Ok",
            SecondaryAction = null,
            Alignment = HorizontalAlignment.Right,
            ShowDismiss = true
        };
        var dialog = await DialogService.ShowPanelAsync<CalculationSettings>(parameters);
        _ = await dialog.Result;
    }
}