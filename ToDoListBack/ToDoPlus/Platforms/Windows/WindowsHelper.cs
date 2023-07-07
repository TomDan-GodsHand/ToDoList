using Microsoft.Maui.Platform;
using Microsoft.UI.Composition;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using PInvoke;
using WinRT;
using WinRT.Interop;
using static PInvoke.User32;

namespace ToDoPlus.Platforms.Windows
{
# nullable disable
    public static class WindowsHelper
    {
        private static Microsoft.UI.Xaml.Window GetActiveNativeWindow() =>
        (Microsoft.UI.Xaml.Window)Microsoft.Maui.Controls.Application.Current.Windows.FirstOrDefault()?.Handler?.PlatformView;

        public static void SetColorForWindows(Color backgroundColor, Color foregroundColor)
        {
            var res = Microsoft.UI.Xaml.Application.Current.Resources;
            res["WindowCaptionBackground"] = backgroundColor.ToWindowsColor();
            res["WindowCaptionBackgroundDisabled"] = backgroundColor.ToWindowsColor();
            res["WindowCaptionForeground"] = foregroundColor.ToWindowsColor();
            res["WindowCaptionForegroundDisabled"] = foregroundColor.ToWindowsColor();

            TriggertTitleBarRepaint();
        }

        private static bool TriggertTitleBarRepaint()
        {
#if WINDOWS
            var nativeWindow = GetActiveNativeWindow();
            if (nativeWindow is null)
            {
                return default;
            }

            var hWnd = WindowNative.GetWindowHandle(nativeWindow);
            var activeWindow = User32.GetActiveWindow();
            if (hWnd == activeWindow)
            {
                User32.PostMessage(hWnd, WindowMessage.WM_ACTIVATE, new IntPtr((int)0x00), IntPtr.Zero);
                User32.PostMessage(hWnd, WindowMessage.WM_ACTIVATE, new IntPtr((int)0x01), IntPtr.Zero);
            }
            else
            {
                User32.PostMessage(hWnd, WindowMessage.WM_ACTIVATE, new IntPtr((int)0x01), IntPtr.Zero);
                User32.PostMessage(hWnd, WindowMessage.WM_ACTIVATE, new IntPtr((int)0x00), IntPtr.Zero);
            }

#endif
            return true;
        }

        public static void TryMicaOrAcrylic(this Microsoft.UI.Xaml.Window window)
        {
            var dispatcherQueueHelper = new WindowsSystemDispatcherQueueHelper(); // in Platforms.Windows folder
            dispatcherQueueHelper.EnsureWindowsSystemDispatcherQueueController();

            // Hooking up the policy object
            var configurationSource = new SystemBackdropConfiguration();
            configurationSource.IsInputActive = true;

            switch (((FrameworkElement)window.Content).ActualTheme)
            {
                case ElementTheme.Dark:
                    configurationSource.Theme = SystemBackdropTheme.Dark;
                    break;

                case ElementTheme.Light:
                    configurationSource.Theme = SystemBackdropTheme.Light;
                    break;

                case ElementTheme.Default:
                    configurationSource.Theme = SystemBackdropTheme.Default;
                    break;
            }

            // Let's try Mica first
            if (MicaController.IsSupported())
            {
                var micaController = new MicaController();
                micaController.AddSystemBackdropTarget(window.As<ICompositionSupportsSystemBackdrop>());
                micaController.SetSystemBackdropConfiguration(configurationSource);

                window.Activated += (object sender, WindowActivatedEventArgs args) =>
                {
                    if (args.WindowActivationState is WindowActivationState.CodeActivated or WindowActivationState.PointerActivated)
                    {
                        // Handle situation where a window is activated and placed on top of other
                        // active windows.
                        if (micaController == null)
                        {
                            micaController = new MicaController();
                            micaController.AddSystemBackdropTarget(window.As<ICompositionSupportsSystemBackdrop>());
                            micaController.SetSystemBackdropConfiguration(configurationSource);
                        }

                        if (configurationSource != null)
                            configurationSource.IsInputActive = args.WindowActivationState != WindowActivationState.Deactivated;
                    }
                };

                window.Closed += (object sender, WindowEventArgs args) =>
                {
                    if (micaController != null)
                    {
                        micaController.Dispose();
                        micaController = null;
                    }

                    configurationSource = null;
                };
            }
            // If no Mica, maybe we can use Acrylic instead
            else if (DesktopAcrylicController.IsSupported())
            {
                var acrylicController = new DesktopAcrylicController();
                acrylicController.AddSystemBackdropTarget(window.As<ICompositionSupportsSystemBackdrop>());
                acrylicController.SetSystemBackdropConfiguration(configurationSource);

                window.Activated += (object sender, WindowActivatedEventArgs args) =>
                {
                    if (args.WindowActivationState is WindowActivationState.CodeActivated or WindowActivationState.PointerActivated)
                    {
                        // Handle situation where a window is activated and placed on top of other
                        // active windows.
                        if (acrylicController == null)
                        {
                            acrylicController = new DesktopAcrylicController();
                            acrylicController.AddSystemBackdropTarget(window.As<ICompositionSupportsSystemBackdrop>());
                            acrylicController.SetSystemBackdropConfiguration(configurationSource);
                        }
                    }

                    if (configurationSource != null)
                        configurationSource.IsInputActive = args.WindowActivationState != WindowActivationState.Deactivated;
                };

                window.Closed += (object sender, WindowEventArgs args) =>
                {
                    if (acrylicController != null)
                    {
                        acrylicController.Dispose();
                        acrylicController = null;
                    }

                    configurationSource = null;
                };
            }
        }
    }
}