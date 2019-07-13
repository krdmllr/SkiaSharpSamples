using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFormsSkiaSharpSamples.SamplePages;

namespace XamarinFormsSkiaSharpSamples
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage
    {
        private const string Coordinates = "Coordinate system";
        private const string Antialias = "AntialiasSample";
        private const string Blend = "Blend";
        private const string FormsAndPathsSample = "Forms and paths";
        private const string SvgPath = "SVG Path";

        /// <summary>
        /// All selectable pages.
        /// </summary>
        public List<string> Pages { get; set; } = new List<string>()
        {
            Coordinates,
            Antialias,
            Blend,
            SvgPath,
            FormsAndPathsSample
        };

        public MenuPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            SetPage(Coordinates);
        }

        private void SetPage(string name)
        {
            var masterDetail = (MasterDetailPage)Parent;
            switch (name)
            {
                case Coordinates:
                    masterDetail.Detail = new ContentPage
                    {
                        BackgroundColor = Color.Gray,
                        Content = new CoordinateSystemSample()
                    };
                    break;
                case Blend:
                    masterDetail.Detail = new ContentPage
                    {
                        Content = new BlendSample()
                    };
                    break;
                case Antialias:
                    masterDetail.Detail = new ContentPage
                    {
                        Content = new AntialiasSample()
                    };
                    break;
                case SvgPath:
                    masterDetail.Detail = new ContentPage
                    {
                        Content = new SvgPathSample()
                    };
                    break;
                case FormsAndPathsSample:
                    masterDetail.Detail = new ContentPage
                    {
                        Content = new FormsAndPathsSample()
                    };
                    break;
            }
        }

        private void PageSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                SetPage((string)e.SelectedItem);
                ((ListView)sender).SelectedItem = null;
            }

        }
    }
}