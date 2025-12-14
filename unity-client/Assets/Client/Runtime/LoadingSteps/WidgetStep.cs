using Client.Runtime.Widgets;
using Cysharp.Threading.Tasks;
using System.Threading;
using UniTx.Runtime.Bootstrap;
using UniTx.Runtime.Widgets;

namespace Client.Runtime
{
    public sealed class WidgetStep : LoadingStepBase
    {
        public async override UniTask InitialiseAsync(CancellationToken cToken = default)
        {
            await UniWidgets.PushAsync<DemoOneWidget>(cToken);
            await UniWidgets.PushAsync<DemoTwoWidget>(new DemoTwoWidgetData("Hello from UniTx."), cToken);
            await UniWidgets.PopWidgetsStackAsync(cToken);
            await UniWidgets.PopWidgetsStackAsync(cToken);
        }
    }
}
