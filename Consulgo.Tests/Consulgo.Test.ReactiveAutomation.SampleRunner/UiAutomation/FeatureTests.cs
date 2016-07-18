using Consulgo.Test.ReactiveBdd;
using System;

namespace Consulgo.Test.ReactiveAutomation.SampleRunner.UiAutomation
{
    public class FeatureTests : BaseFeatureTest
    {
        private StepsImpl _s = new StepsImpl();

        public void SmokeTest()
        {
            UglyApplicationModel model = null;
            Given<UglyApplicationModel>(_s.NewUglyApplication)
                .And(_s.HelloWorldModeOpen)
                .When(_s.EnterName)
                .And(_s.AcceptMyChoice)
                .Then(_s.GreetingsIsShown)
                .Start()
                .Subscribe(
                    m =>
                    {
                        Console.WriteLine("Test progress recieved");
                        model = m;
                    },
                    er => Console.WriteLine("Test error: " + er),
                    () =>
                    {
                        Console.WriteLine("Test completed successfully");

                        if (model != null && model.ApplicationProcess != null)
                        {
                            ProcessHelper.TryToKill(model.ApplicationProcess);
                        }
                    });
        }

    }
}
