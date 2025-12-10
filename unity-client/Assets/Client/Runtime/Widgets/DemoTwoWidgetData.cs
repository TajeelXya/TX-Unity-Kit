namespace Client.Runtime.Widgets
{
    public readonly struct DemoTwoWidgetData : IDemoTwoWidgetData
    {
        private readonly string _message;
        public DemoTwoWidgetData(string message)
        {
            _message = message;
        }

        public string Message => _message;
    }
}