using System.ComponentModel.DataAnnotations;

namespace ChurchLiveScheduler.blazor.ViewModels;

public sealed class Special
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Date is required")]
    public DateTime? Datetime { get; set; }
}
