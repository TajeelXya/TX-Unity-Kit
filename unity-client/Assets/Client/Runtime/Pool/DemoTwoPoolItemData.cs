namespace Client.Runtime.Pool
{
    public readonly struct DemoTwoPoolItemData : IDemoTwoPoolItemData
    {
        private readonly string _message;

        public DemoTwoPoolItemData(string message)
        {
            _message = message;
        }

        public string Message => _message;
    }
}