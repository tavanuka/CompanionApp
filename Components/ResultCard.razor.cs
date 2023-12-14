using CompanionApp.States;
using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Microsoft.Fast.Components.FluentUI.DesignTokens;

namespace CompanionApp.Components;

public partial class ResultCard
{
    private FluentCard fluentCard = default!;

    [Inject] public NeutralBaseColor NeutralBaseColor { get; set; } = default!;
    [Inject] public CalculationSettingsStateContainer Settings { get; set; } = default!;
    [Inject] public CalculationStateContainer State { get; set; } = default!;
    [Inject] public NavigationManager Navigation { get; set; } = default!;

    public void Dispose()
    {
        State.OnStateChange -= StateHasChanged;
        Settings.OnStateChange -= StateHasChanged;
    }

    protected override void OnInitialized()
    {
        State.OnStateChange += StateHasChanged;
        Settings.OnStateChange += StateHasChanged;
        base.OnInitialized();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender) await NeutralBaseColor.SetValueFor(fluentCard.Element, "#caba8c".ToSwatch());
    }

    private void ShowTable()
    {
        Navigation.NavigateTo("/result");
    }
}