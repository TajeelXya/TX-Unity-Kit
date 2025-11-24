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

        /// <summary>
        /// Pushes a widget of the given type without data.
        /// </summary>
        public static void Push<TWidgetType>()
            where TWidgetType : IWidget
            => _widgetsManager.PushAsync<TWidgetType>().Forget();

        /// <summary>
        /// Pushes a widget of the given type with data.
        /// </summary>
        public static void Push<TWidgetType>(IWidgetData widgetData)
            where TWidgetType : IWidget
            => _widgetsManager.PushAsync<TWidgetType>(widgetData).Forget();

        /// <summary>
        /// Pops the widget from the stack.
        /// </summary>
        public static void PopWidgetsStack() => _widgetsManager.PopWidgetsStackAsync().Forget();

        /// <summary>
        /// Returns the widget currently at the top of the stack without removing it.
        /// Returns null if the widgets stack is empty.
        /// </summary>
        public static IWidget Peek() => _widgetsManager.Peek();
    }
}