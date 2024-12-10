using TheSequenceSystem;

namespace TheSequenceMAUI
{
    public partial class MainPage : ContentPage
    {
        Sequence sequence = new();

        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = sequence;

            
        }

        private void btnStart_Clicked(object sender, EventArgs e)
        {
            sequence.StartGame();
        }

        private void RoundStartBtn_Clicked(object sender, EventArgs e)
        {
            sequence.RoundStart();
        }
    }

}
