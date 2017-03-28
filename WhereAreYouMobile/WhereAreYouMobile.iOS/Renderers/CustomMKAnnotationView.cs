using MapKit;
using WhereAreYouMobile.iOS.Renderers;
using WhereAreYouMobile.ViewModels.Map;
using WhereAreYouMobile.Views.Map;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace WhereAreYouMobile.iOS.Renderers
{
    public class CustomMKAnnotationView : MKAnnotationView
    {
        public string Id { get; set; }

        public string Url { get; set; }

        public CustomMKAnnotationView(IMKAnnotation annotation, string id)
            : base(annotation, id)
        {
        }
    }
}