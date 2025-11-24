namespace UniTx.Runtime.IoC
{
    public interface IInjectable
    {
        void Inject(IResolver resolver);
    }
}