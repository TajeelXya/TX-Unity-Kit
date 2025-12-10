using UnityEngine;

namespace UniTx.Runtime.Widgets
{
    /// <summary>
    /// Base class for all UI widgets.
    /// </summary>
    public abstract class UniWidgetBase : MonoBehaviour, IWidget
    {
        public GameObject GameObject => gameObject;

        public Transform Transform => transform;

        public abstract void Initialise();

        public abstract void Reset();
    }

    /// <summary>
    /// Base class for UI widgets that receive typed widget data.
    /// </summary>
    /// <typeparam name="TWidgetData">The type of widget data this widget uses.</typeparam>
    public abstract class UniWidgetBase<TWidgetData> : UniWidgetBase, IWidgetDataReceiver
    {
        public TWidgetData Data { get; private set; }

        public void SetData(IWidgetData data) => Data = (TWidgetData)data;
    }
}