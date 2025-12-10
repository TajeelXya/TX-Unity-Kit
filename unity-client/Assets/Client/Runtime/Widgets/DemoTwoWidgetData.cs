using UniTx.Runtime.Widgets;

namespace Client.Runtime.Widgets
{
    public readonly struct DemoTwoWidgetData : IWidgetData
    {
        public readonly string Message;

        public DemoTwoWidgetData(string message)
        {
            Message = message;
        }
    }
}