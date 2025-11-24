using UnityEngine;

namespace UniTx.Runtime.Widgets
{
    public abstract class UniWidgetBase : MonoBehaviour, IWidget
    {
        public GameObject GameObject => gameObject;

        public Transform Transform => transform;

        public abstract void Initialise();

        public abstract void Reset();
    }

    public abstract class UniWidgetBase<TWidgetData> : UniWidgetBase, IWidgetDataReceiver
    {
        public TWidgetData WidgetData { get; private set; }

        public void SetData(IWidgetData widgetData) => WidgetData = (TWidgetData)widgetData;
    }
}