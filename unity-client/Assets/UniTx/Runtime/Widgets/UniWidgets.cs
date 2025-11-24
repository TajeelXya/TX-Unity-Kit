using Cysharp.Threading.Tasks;
using System;
using System.Threading;

namespace UniTx.Runtime.Widgets
{
    public static class UniWidgets
    {
        private static IWidgetsManager _widgetsManager = null;

        internal static UniTask InitialiseAsync(IWidgetsManager widgetsManager, CancellationToken cToken = default)
        {
            _widgetsManager = widgetsManager ?? throw new ArgumentNullException(nameof(widgetsManager));
            return _widgetsManager.InitialiseAsync(cToken);
        }

        internal static UniTask ResetAsync(CancellationToken cToken = default) => _widgetsManager.ResetAsync(cToken);

        public static void Push<TWidgetType>()
            where TWidgetType : IWidget
            => _widgetsManager.PushAsync<TWidgetType>().Forget();

        public static void Push<TWidgetType>(IWidgetData widgetData)
            where TWidgetType : IWidget
            => _widgetsManager.PushAsync<TWidgetType>(widgetData).Forget();

        public static void PopWidgetsStack() => _widgetsManager.PopWidgetsStackAsync().Forget();

        public static IWidget Peek() => _widgetsManager.Peek();
    }
}