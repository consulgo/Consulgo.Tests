using Consulgo.Test.ReactiveAutomation.Controls;
using Consulgo.Test.ReactiveAutomation.SampleRunner.UiAutomation.Models;
using Consulgo.Test.ReactiveBdd;
using System.Diagnostics;
using Ugly.Yaua.AutomationIds;

namespace Consulgo.Test.ReactiveAutomation.SampleRunner.UiAutomation
{
    public class UglyApplicationModel : IModel
    {
        public Process ApplicationProcess { get; set; }
        public MainWindowModel MainWindow { get; set; }

        public UglyApplicationModel()
        {
            MainWindow = AutomationControlDescription
                .ById(MainWindowIds.MainWindow)
                .ChildOf(DesktopWindowControl.NewInstance)
                .As<MainWindowModel>();
        }
    }
}
