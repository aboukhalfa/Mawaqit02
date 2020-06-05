using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mawaqit02.Client.Shared;

namespace Mawaqit02.Client.Components
{
    public class FiveCardsBase: ComponentBase
    {
        [Parameter]
        public IEnumerable<(string Title, DateTime Value, bool IsSelected)> Values { get; set; }

        //[Parameter]
        //public RenderFragment SelectedValueHeader { get; set; }

        [Parameter]
        public string BackgroundColor { get; set; } = "#1b8046";

        [Parameter]
        public string TextColor { get; set; } = "#FFFFFF";

        [Parameter]
        public string SelectedTextColor { get; set; } = "#FFFFFF";

        [Parameter]
        public string SelectedBackgroundColor { get; set; } = "#275eb0";

        [Parameter]
        public string DateTextColor { get; set; } = "#FFFFFF";

        [Parameter]
        public string DateBackgroundColor { get; set; } = "#943d1b";

        [Parameter]
        public string TimeTextColor { get; set; } = "#FFFFFF";

        [Parameter]
        public string TimeBackgroundColor { get; set; } = "#6b1565";

        [Parameter]
        public bool IsSelectionDescriptionVisible { get; set; } = true;


        protected string GetBoxClass((string Title, string Value, bool IsSelected) v)
        {
            if (!v.IsSelected)
                return "sbox";

            return "sbox-sel-border-top sbox-sel-color";
        }

        protected string GetVerticalMargin(bool isSelected)
        {
            if (isSelected)
                return "-3px";

            return "0px";
        }

        public string GetBorderColor(bool isSelected, bool isTop = false)
        {
            var color = isSelected ? SelectedBackgroundColor : BackgroundColor;

            if (isTop && isSelected)
                return color;

            return GetBorderColor(color);
        }

        public string GetBorderColor(string color)
        {
            var (r, g, b) = Util.ToRGB(color);
            return Util.FromRGB(((byte)(r * 2 / 3), (byte)(g * 2 / 3), (byte)(b * 2 / 3)));
        }

        protected override Task OnInitializedAsync()
        {
            if (Values == null)
                Values = new List<(string Title, DateTime Value, bool IsSelected)>();
            return base.OnInitializedAsync();
        }
    }
}
