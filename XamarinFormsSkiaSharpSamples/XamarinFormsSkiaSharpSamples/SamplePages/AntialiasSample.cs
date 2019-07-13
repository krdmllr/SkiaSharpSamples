using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace XamarinFormsSkiaSharpSamples.SamplePages
{
    public class AntialiasSample : SKGLView
    {
        protected override void OnPaintSurface(SKPaintGLSurfaceEventArgs e)
        {
            var strokeWidth = 60;
            var aliasedPaint = new SKPaint
            {
                Color = Color.Red.ToSKColor(),
                Style = SKPaintStyle.Stroke,
                StrokeWidth = strokeWidth,
                IsAntialias = true
            };

            var defaultPaint = new SKPaint
            {
                Color = Color.Green.ToSKColor(),
                Style = SKPaintStyle.Stroke,
                StrokeWidth = strokeWidth
            };

            var smallerSize = CanvasSize.Width > CanvasSize.Height ? CanvasSize.Height : CanvasSize.Width;
            var padding = 20;
            smallerSize -= padding; // Calculate padding in

            var rect = new SKRect(padding, padding, smallerSize, smallerSize);

            using (var path = new SKPath())
            {
                path.ArcTo(rect, -90, 180, true);
                e.Surface.Canvas.DrawPath(path, defaultPaint);
            }

            rect.Offset(-smallerSize/2, 0);

            using (var path = new SKPath())
            {
                path.ArcTo(rect, -90, 180, true);
                e.Surface.Canvas.DrawPath(path, aliasedPaint);
            }
        }
    }
}