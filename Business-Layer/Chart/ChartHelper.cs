using Chart.Mvc.ComplexChart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;

namespace nmct.ssa.labo.webshop.Chart
{
    public class ChartHelper
    {
        public LineChart MakeLineChart<T>(IList<T> items) where T : IChartInterface
        {
            LineChart chart = new LineChart();
            chart.ComplexData.Labels.AddRange(items.Select(o => o.Label));
            chart.ComplexData.Datasets.AddRange(new List<ComplexDataset>() { new ComplexDataset() 
            { 
                Data = items.Select(i => i.Value).ToList<double>(),
                Label = "Label",
                FillColor = "rgba(60,81,170,1)",
                StrokeColor = "rgba(220,220,220,1)",
                PointColor = "rgba(160,173,229,1)",
                PointStrokeColor = "#fff",
                PointHighlightFill = "#fff",
                PointHighlightStroke = "rgba(220,220,220,1)",
            }});
            return chart;
        }
    }

    public interface IChartInterface
    {
        string Label { get; set; }
        double Value { get; set; }
    }
}