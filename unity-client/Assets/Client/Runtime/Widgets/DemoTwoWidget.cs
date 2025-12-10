using UnityEngine;
using UniTx.Runtime;
using UniTx.Runtime.IoC;
using UniTx.Runtime.Widgets;

namespace Client.Runtime.Widgets
{
    public sealed class DemoTwoWidget : MonoBehaviour, IWidget<IDemoTwoWidgetData>, IInjectable
    {
        public IDemoTwoWidgetData Data { get; private set; }

        public GameObject GameObject => gameObject;

        public Transform Transform => transform;

        public void Inject(IResolver resolver)
        {
            UniStatics.LogInfo("Inject", this, Color.khaki);
        }

        public void Initialise()
        {
            UniStatics.LogInfo($"Initialise -> Message: {Data.Message}", this, Color.khaki);
        }

        public void Reset()
        {
            UniStatics.LogInfo("Reset", this, Color.khaki);
        }

        public void SetData(IWidgetData widgetData) => Data = (IDemoTwoWidgetData)widgetData;
    }
}