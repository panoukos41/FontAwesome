using Microsoft.AspNetCore.Components;

namespace P41.FontAwesome;

public abstract class FontAwesomeIconBase : ComponentBase
{
    [Parameter(CaptureUnmatchedValues = true)]
    public IEnumerable<KeyValuePair<string, object?>>? AdditionalAttributes { get; set; }
}
