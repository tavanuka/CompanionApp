using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Microsoft.JSInterop;

namespace CompanionApp.Shared;

public partial class MainLayout : IAsyncDisposable 
{
    private StandardLuminance baseLayerLuminance = StandardLuminance.LightMode;

    private ElementReference container;

    private bool darkMode;
    private IJSObjectReference? jsModule;
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        try
        {
            if (jsModule is not null) await jsModule.DisposeAsync();
        }
        catch (JSDisconnectedException)
        {
            // The JSRuntime side may routinely be gone already if the reason we're disposing is that
            // the client disconnected. This is not an error.
        }

        GC.SuppressFinalize(this);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            jsModule = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./Shared/MainLayout.razor.js");
            GlobalState.SetContainer(container);

            darkMode = await jsModule!.InvokeAsync<bool>("isDarkMode");
            baseLayerLuminance = darkMode ? StandardLuminance.DarkMode : StandardLuminance.LightMode;
            GlobalState.SetLuminance(baseLayerLuminance);
            StateHasChanged();
        }
    }

    public async void UpdateTheme()
    {
        baseLayerLuminance = darkMode ? StandardLuminance.DarkMode : StandardLuminance.LightMode;
        await BaseLayerLuminance.WithDefault(baseLayerLuminance.GetLuminanceValue());
        GlobalState.SetLuminance(baseLayerLuminance);
    }
}