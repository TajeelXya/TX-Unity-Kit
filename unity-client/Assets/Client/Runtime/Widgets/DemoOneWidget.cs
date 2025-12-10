using UniTx.Runtime;
using UniTx.Runtime.Widgets;
using UnityEngine;

namespace Client.Runtime.Widgets
{
    public sealed class DemoOneWidget : MonoBehaviour, IWidget
    {
        public GameObject GameObject => gameObject;

        public Transform Transform => transform;

        public void Initialise()
        {
            UniStatics.LogInfo("Initialise", this, Color.ivory);
        }

        public void Reset()
        {
            UniStatics.LogInfo("Reset", this, Color.ivory);
        }
    }
}
