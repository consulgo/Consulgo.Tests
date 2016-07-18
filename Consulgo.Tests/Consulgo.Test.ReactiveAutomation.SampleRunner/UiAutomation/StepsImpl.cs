using Consulgo.Test.ReactiveAutomation.SampleRunner.UiAutomation.Resolvers;
using Consulgo.Test.ReactiveBdd;
using System.Diagnostics;
using System.Windows.Automation;

namespace Consulgo.Test.ReactiveAutomation.SampleRunner.UiAutomation
{
    public class StepsImpl
    {
        private const string Name = "Mr. Ugly";
        private readonly string GreetingsForName = string.Format("Hello {0}!", Name);

        private readonly IPropertyResolver<UglyApplicationModel> _automationResolver;
        private readonly IPropertyResolver<UglyApplicationModel> _automationPropertyResolver;

        public StepsImpl()
        {
            _automationResolver = new AutomationPropertyResolver();
            _automationPropertyResolver = new AutomationPropertyChangedResolver(ValuePattern.ValueProperty);
        }

        public ICommand<UglyApplicationModel> GreetingsIsShown(ApplicationState<UglyApplicationModel> state)
        {
            return new RequireCommand<UglyApplicationModel>(
                (require) => require.Before(s => s.MainWindow.HelloWorldTab.HelloWorld, _automationResolver)
                                    .Before(s => s.MainWindow.HelloWorldTab.HelloWorld.Result, _automationResolver),
                () => Debug.Assert(string.Equals(state.Model.MainWindow.HelloWorldTab.HelloWorld.Result.Value, GreetingsForName, System.StringComparison.InvariantCulture)));
        }

        public ICommand<UglyApplicationModel> AcceptMyChoice(ApplicationState<UglyApplicationModel> state)
        {
            return new RequireCommand<UglyApplicationModel>(
                (require) => require.Before(s => s.MainWindow.HelloWorldTab.HelloWorld.AcceptButton, _automationResolver)
                                    .After(s => s.MainWindow.HelloWorldTab.HelloWorld.Result, _automationPropertyResolver),
                () => state.Model.MainWindow.HelloWorldTab.HelloWorld.AcceptButton.Invoke());
        }

        public ICommand<UglyApplicationModel> EnterName(ApplicationState<UglyApplicationModel> state)
        {
            return new RequireCommand<UglyApplicationModel>(
                (require) => require.Before(s => s.MainWindow.HelloWorldTab.HelloWorld, _automationResolver)
                                    .Before(s => s.MainWindow.HelloWorldTab.HelloWorld.Name, _automationResolver),
                () => state.Model.MainWindow.HelloWorldTab.HelloWorld.Name.Value = Name);
        }

        public ICommand<UglyApplicationModel> HelloWorldModeOpen(ApplicationState<UglyApplicationModel> state)
        {
            return new RequireCommand<UglyApplicationModel>(
                (require) => require.Before(s => s.MainWindow.HelloWorldTab, _automationResolver),
                () => state.Model.MainWindow.HelloWorldTab.Select());
        }

        public ICommand<UglyApplicationModel> NewUglyApplication(ApplicationState<UglyApplicationModel> state)
        {
            return new SimpleCommand<UglyApplicationModel>(
                () =>
                {
                    state.Model = new UglyApplicationModel();
                    state.Model.ApplicationProcess = ProcessHelper.StartUglyApplication();
                });
        }
    }
}
