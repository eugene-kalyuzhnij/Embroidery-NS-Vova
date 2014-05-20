using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using NSEmbroidery.Core;
using System.Drawing;
using System.Threading;
using NSEmbroidery.Core.Decorators;
using System.Threading.Tasks;
using System.Collections.Concurrent;


namespace NSEmbroidery.TimeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //Test_ConvertBitmapToCanvas();

            //Test_ConvertCanvasToBitmap();

            //Test_Generate();

            //Test_GridDecorator();

            //Test_CellsDecorator();

            //Test_SymbolsDecorator();
            

            //PartitionerExample();
        }




        static void PartitionerExample()
        {
            Stopwatch sw = null;

            long sum = 0;
            long SUMTOP = 10000000;

            // Try sequential for
            sw = Stopwatch.StartNew();
            for (long i = 0; i < SUMTOP; i++) sum += i;
            sw.Stop();
            Console.WriteLine("sequential for result = {0}, time = {1} ms", sum, sw.ElapsedMilliseconds);

            // Try parallel for -- this is slow! 
            sum = 0; 
            sw = Stopwatch.StartNew();
            Parallel.For(0L, SUMTOP, (item) => Interlocked.Add(ref sum, item)); 
            sw.Stop(); 
            Console.WriteLine("parallel for  result = {0}, time = {1} ms", sum, sw.ElapsedMilliseconds);

            // Try parallel for with locals
            sum = 0;
            sw = Stopwatch.StartNew();
            Parallel.For(0L, SUMTOP, () => 0L, (item, state, prevLocal) => prevLocal + item, local => Interlocked.Add(ref sum, local));
            sw.Stop();
            Console.WriteLine("parallel for w/locals result = {0}, time = {1} ms", sum, sw.ElapsedMilliseconds);

            // Try range partitioner
            sum = 0;
            sw = Stopwatch.StartNew();
            Parallel.ForEach(Partitioner.Create(0L, SUMTOP), (range) =>
            {
                long local = 0;
                for (long j = range.Item1; j < range.Item2; j++) local += j;
                Interlocked.Add(ref sum, local);
            });
            sw.Stop();
            Console.WriteLine("range partitioner result = {0}, time = {1} ms", sum, sw.ElapsedMilliseconds);
        }


        #region Time spent

        static void Test_ConvertBitmapToCanvas()
        {
            CanvasConverter converter = new CanvasConverter();

            Bitmap image = new Bitmap(1900, 1200);

            for (int y = 0; y < image.Height; y++)
                for (int x = 0; x < image.Width; x++)
                    image.SetPixel(x, y, Color.Black);

            long total = 0;
            for (int i = 0; i < 10; i++)
            {
                Stopwatch watch = new Stopwatch();

                watch.Start();
                Canvas canvas = converter.ConvertBitmapToCanvas(image);
                watch.Stop();

                total += watch.ElapsedMilliseconds;
            }

            Console.WriteLine("ConvertBitmapToCanvas. Average time (10 calls): " + (total / 10L).ToString());
        }

        static void Test_ConvertCanvasToBitmap()
        {
            CanvasConverter converter = new CanvasConverter();

            Canvas canvas = new Canvas(1400, 1400);

            for (int y = 0; y < canvas.Height; y++)
                for (int x = 0; x < canvas.Width; x++)
                    canvas.SetColor(x, y, Color.Black);

            long total = 0;
            for (int i = 0; i < 5; i++)
            {
                Stopwatch watch = new Stopwatch();

                watch.Start();
                Bitmap image = converter.ConvertCanvasToBitmap(canvas);
                watch.Stop();

                total += watch.ElapsedMilliseconds;
            }

            Console.WriteLine("ConvertCanvasToBitmap. Average time (5 calls): " + (total / 5L).ToString());
        }

        static void Test_Generate()
        {


            Canvas inputCanvas = new Canvas(new Resolution(1200, 1000));

            #region FillingFillingExpectedCanvas
            for (int y = 0; y < 1000; y++)
                for (int x = 0; x < 1200; x++)
                {
                        inputCanvas.SetColor(x, y, Color.Red);
                }
            #endregion


            Settings settings = new Settings();
            settings.CellsCount = 100;
            settings.Coefficient = 2;
            settings.Palette = new Palette(new Color[] { Color.Red, Color.Green, Color.Blue });

            PatternMapGenerator mapGenerator = new PatternMapGenerator();

            long total = 0;
            for (int i = 0; i < 10; i++)
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                Canvas actual = mapGenerator.Generate(inputCanvas, settings);
                watch.Stop();

                total += watch.ElapsedMilliseconds;
            }
            Console.WriteLine("Generate. Average time (10 calls): " + (total / 10L).ToString());

        }

        static void Test_GridDecorator()
        {
            Settings settings = new Settings();
            settings.CellsCount = 300;
            settings.Coefficient = 2;
            settings.Palette = new Palette(new Color[] { Color.Red, Color.Green, Color.Blue });
            settings.GridType = GridType.SolidLine;


            Canvas actual = new Canvas(new Resolution(1200, 900));

            #region Filling actual
            for (int y = 0; y < 900; y++)
                for (int x = 0; x < 1200; x++)
                    actual.SetColor(x, y, Color.Green);
            #endregion

            PatternMapGenerator generator = new PatternMapGenerator();
            Canvas pattern = generator.Generate(actual, settings);

            GridDecorator decoratorGrid = new GridDecorator();

            long total = 0;
            for (int i = 0; i < 10; i++)
            {
                Stopwatch watch = new Stopwatch();

                watch.Start();
                decoratorGrid.Decorate(actual, pattern, settings);
                watch.Stop();

                total += watch.ElapsedMilliseconds;
            }


            Console.WriteLine("GridDecorator. Average time (10 calls): " + (total / 10L).ToString());

        }

        static void Test_CellsDecorator()
        {
            Settings settings = new Settings();
            settings.CellsCount = 300;
            settings.Coefficient = 4;
            settings.Palette = new Palette(new Color[] { Color.Red});
            settings.GridType = GridType.SolidLine;


            Canvas actual = new Canvas(new Resolution(1900, 2000));

            #region Filling actual
            for (int y = 0; y < 1400; y++)
                for (int x = 0; x < 1400; x++)
                    actual.SetColor(x, y, Color.Green);
            #endregion

            PatternMapGenerator generator = new PatternMapGenerator();
            Canvas pattern = generator.Generate(actual, settings);

            Resolution resolution = new Resolution(pattern.Width * settings.Coefficient, pattern.Height * settings.Coefficient);
            Canvas input = new Canvas(resolution);

            CellsDecorator decorator = new CellsDecorator();


            long total = 0;

            for (int i = 0; i < 20; i++)
            {
                Stopwatch watch = new Stopwatch();

                watch.Start();
                decorator.Decorate(input, pattern, settings);
                watch.Stop();

                total += watch.ElapsedMilliseconds;
            }

            Console.WriteLine("CellsDecorator. Average time (20 calls): " + (total / 20L).ToString());
        }

        static void Test_SymbolsDecorator()
        {
            Settings settings = new Settings()
            {
                CellsCount = 300,
                GridType = GridType.None,
                DecoratorsComposition = new DecoratorsComposition(),
                Coefficient = 4,
                Palette = new Palette(new Color[] { Color.White }),
                Symbols = new char[] { '1' },
                SymbolColor = Color.Black
            };




            Canvas image = new Canvas(1400, 1400);
            for (int y = 0; y < image.Height; y++)
                for (int x = 0; x < image.Width; x++)
                    image.SetColor(x, y, Color.White);

            PatternMapGenerator generator = new PatternMapGenerator();
            Canvas pattern = generator.Generate(image, settings);

            SymbolsDecorator decorator = new SymbolsDecorator();

            Resolution resolution = new Resolution(pattern.Width * settings.Coefficient, pattern.Height * settings.Coefficient);
            Canvas result = new Canvas(resolution);

            long total = 0;
            for (int i = 0; i < 10; i++)
            {
                Stopwatch watch = new Stopwatch();

                watch.Start();
                decorator.Decorate(result, pattern, settings);
                watch.Stop();

                total += watch.ElapsedMilliseconds;
            }


            Console.WriteLine("SymbolDecorator. Average time (10 calls): " + (total / 10L).ToString());

        }

        #endregion

    }
}


