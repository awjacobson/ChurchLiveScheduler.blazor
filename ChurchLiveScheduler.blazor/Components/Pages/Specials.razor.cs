using ChurchLiveScheduler.blazor.ViewModels;
using ChurchLiveScheduler.sdk;
using ChurchLiveScheduler.sdk.Models;
using Microsoft.JSInterop;

namespace ChurchLiveScheduler.blazor.Components.Pages;

public partial class Specials
{
    private readonly IChurchLiveSchedulerClient _churchLiveSchedulerClient;
    private readonly IJSRuntime _jsRuntime;
    public IReadOnlyList<Special>? SpecialsList { get; set; }
    public Special SelectedSpecial { get; set; } = new();
    public bool IsAdd => SelectedSpecial?.Id is null;
    public string OffCanvasTitle => IsAdd ? "Create Special" : "Edit Special";

    public Specials(IChurchLiveSchedulerClient churchLiveSchedulerClient, IJSRuntime jsRuntime)
    {
        _churchLiveSchedulerClient = churchLiveSchedulerClient;
        _jsRuntime = jsRuntime;
    }

    protected override async Task OnInitializedAsync()
    {
        SpecialsList = await GetSpecialsAsync();
        await base.OnInitializedAsync();
    }

    private async Task EditSpecialAsync(Special special)
    {
        SelectedSpecial = special;

        await _jsRuntime.InvokeVoidAsync("eval", "new bootstrap.Offcanvas(document.getElementById('editCanvas')).show()");
    }

    private async Task SaveSpecialAsync()
    {
        if (IsAdd)
        {
            await CreateSpecialAsync(SelectedSpecial);
        }
        else
        {
            await UpdateSpecialAsync(SelectedSpecial);
        }
        SpecialsList = await GetSpecialsAsync();
        await _jsRuntime.InvokeVoidAsync("eval", "bootstrap.Offcanvas.getInstance(document.getElementById('editCanvas')).hide()");
    }

    private async Task CreateSpecialAsync(Special special)
    {
        await _churchLiveSchedulerClient.CreateSpecialAsync(new CreateSpecialRequest
        {
            Name = special!.Name ?? string.Empty,
            DateTime = special.Datetime ?? DateTime.Now
        });
    }

    private async Task UpdateSpecialAsync(Special special)
    {
        await _churchLiveSchedulerClient.UpdateSpecialAsync(special.Id!.Value, new UpdateSpecialRequest
        {
            Name = special.Name ?? string.Empty,
            DateTime = special.Datetime ?? DateTime.Now
        });
    }

    private async Task<IReadOnlyList<Special>> GetSpecialsAsync()
    {
        var specialDtos = await _churchLiveSchedulerClient.GetSpecialsAsync() ?? [];

        return specialDtos.Select(dto => new Special
        {
            Id = dto.Id,
            Name = dto.Name,
            Datetime = dto.DateTime
        }).ToList();
    }
}
