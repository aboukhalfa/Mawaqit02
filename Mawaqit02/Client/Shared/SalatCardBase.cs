using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mawaqit02.Client.Shared
{
    public class SalatCardBase: ComponentBase
    {
        [Parameter]
        public RenderFragment Content { get; set; }

        [Parameter]
        public string Title { get; set; } = "Hello";

        [Parameter]
        public string Value { get; set; } = "World";

        [Parameter]
        public string BackgroundColor { get; set; } = "#2244AA";

        [Parameter]
        public string TextColor { get; set; } = "#FFFFFF";

        [Parameter]
        public string TitleFontSize { get; set; } = "6";

        [Parameter]
        public string TitleValueSize { get; set; } = "3";
    }
}
