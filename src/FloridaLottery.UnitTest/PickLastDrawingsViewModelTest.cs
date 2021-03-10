using FloridaLottery.ViewModels;
using Xunit;

namespace FloridaLottery.UnitTest
{
    public class PickLastDrawingsViewModelTest
    {
        static PickLastDrawingsViewModel ViewModel;

        public PickLastDrawingsViewModelTest()
        {
            ViewModel = new PickLastDrawingsViewModel()
            {
                NumberSequence = 1234
            };
        }

        [Fact]
        public void HaveTitle()
        {
            Assert.NotNull(ViewModel.Title);
            Assert.Equal("Lottery App", ViewModel.Title);
        }

        [Fact]
        public void CreateForecast()
        {
            ViewModel.CreateForecastCommand.Execute(null);

            Assert.Equal(ViewModel.DrawingNumbers, ViewModel.LastDrawings.Count);

        }
    }
}
