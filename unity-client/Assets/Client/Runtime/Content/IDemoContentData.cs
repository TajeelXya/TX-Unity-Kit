using UniTx.Runtime.Content;

namespace Client.Runtime.Content
{
    public interface IDemoContentData : IData
    {
        string Name { get; }
        string[] Descriptions { get; }
    }
}