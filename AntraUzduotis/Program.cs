using AntraUzduotis.Spline;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace AntraUzduotis
{
    class Program
    {
        static void Main(string[] args)
        {
            TestSplineA();
            TestSplineB();
        }
        private static float Function(double x)
        {
            var answer = (-4 * (x * x) - 6 * x) / (x * x + 4 * x + 5);
            return (float)answer;
        }

        private static void TestSplineB()
        {
            float[] x = new float[] { 0f, 1f, 2f, 3f, 4f, 5f };
            float[] y = new float[] { 209f, 0f, 209f, 209f, 209f, 209f };
            Random rand = new Random(1);


            Console.WriteLine("x: {0}", ArrayUtil.ToString(x));
            Console.WriteLine("y: {0}", ArrayUtil.ToString(y));

            // Create the upsampled X values to interpolate
            int n = 60;
            float[] xs = new float[n];
            float stepSize = (x[x.Length - 1] - x[0]) / (n - 1);

            for (int i = 0; i < n; i++)
            {
                xs[i] = x[0] + i * stepSize;
            }

            // Solve
            CubicSpline spline = new CubicSpline();
            float[] ys = spline.FitAndEval(x, y, xs);

            Console.WriteLine("xs: {0}", ArrayUtil.ToString(xs));
            Console.WriteLine("ys: {0}", ArrayUtil.ToString(ys));

            // Plot
            string path = @"spline-B.png";
            PlotSplineSolution("Kubinis splainas - B", x, y, xs, ys, path);
        }
        private static void TestSplineA()
        {
            float[] x = new float[10];
            float[] y = new float[10];
            Random rand = new Random(1);
            float Interval = 0.3f;
            float a = -2f;

            for (int i = 0; i < 10; i++)
            {
                x[i] = a;
                y[i] = Function(a);
                a += Interval;
            }

            Console.WriteLine("x: {0}", ArrayUtil.ToString(x));
            Console.WriteLine("y: {0}", ArrayUtil.ToString(y));

            // Create the upsampled X values to interpolate
            int n = 60;
            float[] xs = new float[n];
            float stepSize = (x[x.Length - 1] - x[0]) / (n - 1);

            for (int i = 0; i < n; i++)
            {
                xs[i] = x[0] + i * stepSize;
            }

            // Solve
            CubicSpline spline = new CubicSpline();
            float[] ys = spline.FitAndEval(x, y, xs);

            Console.WriteLine("xs: {0}", ArrayUtil.ToString(xs));
            Console.WriteLine("ys: {0}", ArrayUtil.ToString(ys));

            // Plot
            string path = @"spline-A.png";
            PlotSplineSolution("Kubinis splainas - A", x, y, xs, ys, path);
        }

        #region PlotSplineSolution

        private static void PlotSplineSolution(string title, float[] x, float[] y, float[] xs, float[] ys, string path)
        {
            List<DataPoint> points = new List<DataPoint>();

            for (int i = 0; i < x.Length; i++)
            {
                points.Add(new DataPoint(x[i], y[i]));
            }

            var chart = new Chart();
            chart.Size = new Size(600, 400);
            chart.Titles.Add(title);
            string legendName = "Legend";
            chart.Legends.Add(new Legend(legendName));

            ChartArea ca = new ChartArea("DefaultChartArea");
            ca.AxisX.Title = "X";
            ca.AxisY.Title = "Y";
            chart.ChartAreas.Add(ca);

            Series s1 = CreateSeries(chart, "Spline", CreateDataPoints(xs, ys), Color.Blue, MarkerStyle.None);
            Series s2 = CreateSeries(chart, "Original", CreateDataPoints(x, y), Color.Green, MarkerStyle.Diamond);

            chart.Series.Add(s1);
            chart.Series.Add(s2);

            ca.RecalculateAxesScale();
            ca.AxisX.Minimum = Math.Round(ca.AxisX.Minimum);
            ca.AxisX.Maximum = Math.Round(ca.AxisX.Maximum);
            int nIntervals = (x.Length - 1);
            nIntervals = Math.Max(4, nIntervals);
            ca.AxisX.Interval = (ca.AxisX.Maximum - ca.AxisX.Minimum) / nIntervals;

            // Save
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (FileStream fs = new FileStream(path, FileMode.CreateNew))
            {
                chart.SaveImage(fs, ChartImageFormat.Png);
            }
        }

        private static List<DataPoint> CreateDataPoints(float[] x, float[] y)
        {
            Debug.Assert(x.Length == y.Length);
            List<DataPoint> points = new List<DataPoint>();

            for (int i = 0; i < x.Length; i++)
            {
                points.Add(new DataPoint(x[i], y[i]));
            }

            return points;
        }

        private static Series CreateSeries(Chart chart, string seriesName, IEnumerable<DataPoint> points, Color color, MarkerStyle markerStyle = MarkerStyle.None)
        {
            var s = new Series()
            {
                XValueType = ChartValueType.Double,
                YValueType = ChartValueType.Double,
                Legend = chart.Legends[0].Name,
                IsVisibleInLegend = true,
                ChartType = SeriesChartType.Line,
                Name = seriesName,
                ChartArea = chart.ChartAreas[0].Name,
                MarkerStyle = markerStyle,
                Color = color,
                MarkerSize = 8
            };

            foreach (var p in points)
            {
                s.Points.Add(p);
            }

            return s;
        }

        #endregion
    }
}