using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mawaqit02.Client.Shared
{
    public class FiveCardsBase: ComponentBase
    {
        [Parameter]
        public IEnumerable<(string Title, string Value, bool IsSelected)> Values { get; set; }

        [Parameter]
        public string BackgroundColor { get; set; } = "#1b8046";

        [Parameter]
        public string TextColor { get; set; } = "#FFFFFF";

        [Parameter]
        public string SelectedTextColor { get; set; } = "#FFDDFF";

        [Parameter]
        public string SelectedBackgroundColor { get; set; } = "#275eb0";

        protected string GetBoxClass((string Title, string Value, bool IsSelected) v)
        {
            if (!v.IsSelected)
                return "sbox";

            return "sbox-sel-border-top sbox-sel-color";
        }

        protected override Task OnInitializedAsync()
        {
            if (Values == null)
                Values = new List<(string Title, string Value, bool IsSelected)>();
            return base.OnInitializedAsync();
        }
    }
}
