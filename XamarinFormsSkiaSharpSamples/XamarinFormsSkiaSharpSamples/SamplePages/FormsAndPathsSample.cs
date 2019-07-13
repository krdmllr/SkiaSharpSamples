using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace XamarinFormsSkiaSharpSamples.SamplePages
{
    public class FormsAndPathsSample : SKCanvasView
    {
        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            var strokeWidth = 20f;
            var orangePaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = Color.Orange.ToSKColor(),
                StrokeWidth = strokeWidth
            };
            var purplePaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = Color.Purple.ToSKColor(),
                StrokeWidth = strokeWidth
            };
            var redPaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = Color.Red.ToSKColor(),
                StrokeWidth = strokeWidth
            };
            var greenPaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = Color.Green.ToSKColor(),
                StrokeWidth = strokeWidth
            };

            var canvas = e.Surface.Canvas;

            var bounds = new SKRect(30, 30, e.Info.Width - 30, 400);

            

            // Draw rounded rect with paths
            float width = bounds.Width - strokeWidth;
            float height = bounds.Height - strokeWidth;
            var maxHeight = width / 2;

            // It looks weird when the form is as high as wide
            if (height > maxHeight)
                height = maxHeight;

            var outerSectionWidth = height / 2;
            var middleSectionWidth = width - outerSectionWidth * 2;

            using (var path = new SKPath())
            {
                path.MoveTo(bounds.Left + outerSectionWidth + strokeWidth / 2, bounds.Top + strokeWidth / 2);
                path.RLineTo(middleSectionWidth, 0);
                path.RArcTo(outerSectionWidth, outerSectionWidth, 0, SKPathArcSize.Large, SKPathDirection.Clockwise, 0, height);
                path.RLineTo(-middleSectionWidth, 0);
                path.RArcTo(-outerSectionWidth, -outerSectionWidth, 0, SKPathArcSize.Large, SKPathDirection.Clockwise, 0, -height); 
                canvas.DrawPath(path, redPaint);
            }


            // Rounded rect
            bounds.Offset(0, bounds.Height + 30); 
            var radius = bounds.Height / 2; 
            canvas.DrawRoundRect(bounds, radius, radius, greenPaint);

            // Circle
            bounds.Offset(0, bounds.Height + 30); 
            canvas.DrawCircle(bounds.Width/4, bounds.MidY, radius, orangePaint);

            // Rectangle  
            var rect = new SKRect(  bounds.Right -bounds.Height, bounds.Top, bounds.Right, bounds.Bottom); 
            canvas.DrawRect(rect, purplePaint);
        }
    }
}