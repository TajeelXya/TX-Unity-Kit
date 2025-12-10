using System;
using System.Threading;
using Client.Runtime.Widgets;
using Cysharp.Threading.Tasks;
using UniTx.Runtime.Bootstrap;
using UniTx.Runtime.Widgets;

namespace Client.Runtime
{
    public sealed class WidgetStep : LoadingStepBase
    {
        public async override UniTask InitialiseAsync(CancellationToken cToken = default)
        {
            await UniWidgets.PushAsync<DemoOneWidget>(cToken);
            await UniTask.Delay(TimeSpan.FromSeconds(5f), cancellationToken: cToken);
            await UniWidgets.PushAsync<DemoTwoWidget>(new DemoTwoWidgetData("Hello from UniTx."), cToken);
            await UniTask.Delay(TimeSpan.FromSeconds(5f), cancellationToken: cToken);
            await UniWidgets.PopWidgetsStackAsync(cToken);
            await UniTask.Delay(TimeSpan.FromSeconds(5f), cancellationToken: cToken);
            await UniWidgets.PopWidgetsStackAsync(cToken);
        }
    }

}
