using UniTx.Runtime;
using UniTx.Runtime.Widgets;
using UnityEngine;

namespace Client.Runtime.Widgets
{
    public sealed class DemoOneWidget : UniWidgetBase
    {
        public override void Initialise()
        {
            UniStatics.LogInfo("Initialise", this, Color.ivory);
        }

        public override void Reset()
        {
            UniStatics.LogInfo("Reset", this, Color.ivory);
        }
    }
}
