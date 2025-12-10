using UniTx.Runtime;
using UniTx.Runtime.IoC;
using UniTx.Runtime.Widgets;
using UnityEngine;

namespace Client.Runtime.Widgets
{
    public sealed class DemoTwoWidget : UniWidgetBase<IDemoTwoWidgetData>, IInjectable
    {
        public void Inject(IResolver resolver)
        {
            UniStatics.LogInfo("Inject", this, Color.khaki);
        }

        public override void Initialise()
        {
            UniStatics.LogInfo($"Initialise -> Message: {Data.Message}", this, Color.khaki);
        }

        public override void Reset()
        {
            UniStatics.LogInfo("Reset", this, Color.khaki);
        }
    }
}