using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace XamarinFormsSkiaSharpSamples.SamplePages
{
    public class BlendSample : SKCanvasView
    {
        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            this.IgnorePixelScaling = false;

            var canvas = e.Surface.Canvas;
            var paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = Color.Black.ToSKColor(),
                IsAntialias = true
            };
            var blendPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = Color.Transparent.ToSKColor(),
                IsAntialias = true,
                BlendMode = SKBlendMode.Src
            };

            using (new SKAutoCanvasRestore(canvas))
            {  
                using (var outerPath = SKPath.ParseSvgPathData("M32.875 5h-17.75a3.303 3.303 0 0 0-2.816 1.648L3.422 22.34a3.477 3.477 0 0 0 0 3.32l8.887 15.692c.562 1 1.68 1.648 2.816 1.648h17.75a3.303 3.303 0 0 0 2.816-1.648l8.887-15.692a3.477 3.477 0 0 0 0-3.32L35.691 6.648A3.303 3.303 0 0 0 32.875 5z"))
                using (var innerPath = SKPath.ParseSvgPathData(
                    "M32.613 34h-3.05a.407.407 0 0 1-.352-.219L24.047 24.2c-.027-.05-.035-.11-.047-.16-.012.05-.02.11-.047.16l-5.18 9.582a.396.396 0 0 1-.335.219h-3.051c-.274 0-.485-.367-.34-.621L20.094 24l-5.047-9.398c-.125-.227.027-.551.265-.602h3.126c.132 0 .265.09.335.207l5.18 9.586c.027.05.035.098.047.156.012-.058.02-.105.047-.156l5.164-9.586a.406.406 0 0 1 .352-.207h3.05c.266 0 .465.355.34.602L27.906 24l5.047 9.379c.145.254-.066.621-.34.621z"))
                {
                    var xamagonBounds = outerPath.Bounds;
                    var smallerCanvasSide = Math.Min(CanvasSize.Width, CanvasSize.Height);
                    smallerCanvasSide *= 0.9f; // Add a bit padding
                    var largerXamagonSide = Math.Max(xamagonBounds.Height, xamagonBounds.Width);
                    var scale = smallerCanvasSide / largerXamagonSide;

                    // Negate the offset from the SVG path
                    outerPath.Offset(-xamagonBounds.Left, -xamagonBounds.Top);
                    innerPath.Offset(-xamagonBounds.Left, -xamagonBounds.Top);

                    // Center the drawing 
                    canvas.Translate((CanvasSize.Width - smallerCanvasSide) / 2, (CanvasSize.Height - smallerCanvasSide) / 2);
                    // Scale the path up
                    canvas.Scale(scale);

                    // Draw the outer form
                    canvas.DrawPath(outerPath, paint);
                    // Remove the inner form from the drawing
                    canvas.DrawPath(innerPath, blendPaint);
                }
            }

            // Background
            var rect = new SKRect(0, 0, e.Info.Width, e.Info.Height); 
            var backgroundPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Shader = SKShader.CreateLinearGradient(new SKPoint(0, 0),
                    new SKPoint(e.Info.Width, e.Info.Height),
                    new[] { SKColors.Red, SKColors.Blue },
                    new float[] { 0, 1 },
                    SKShaderTileMode.Repeat),
                BlendMode = SKBlendMode.SrcOut
            };  
            canvas.DrawRect(rect, backgroundPaint);
        }
    }
}