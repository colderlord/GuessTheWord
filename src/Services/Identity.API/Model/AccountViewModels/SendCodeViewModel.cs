using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Identity.API.Model.AccountViewModels
{
    public record SendCodeViewModel
    {
        public string SelectedProvider { get; init; }

        public ICollection<SelectListItem> Providers { get; init; }

        public string ReturnUrl { get; init; }

        public bool RememberMe { get; init; }
    }
}
