using Chart.Mvc.ComplexChart;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using nmct.ssa.labo.webshop.businesslayer.Services;
using nmct.ssa.labo.webshop.businesslayer.Services.Interfaces;
using nmct.ssa.labo.webshop.models;
using nmct.ssa.labo.webshop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nmct.ssa.labo.webshop.Chart;
using System.Web.Helpers;

namespace nmct.ssa.labo.webshop.Controllers
{
    [Authorize]
    public class ChartController : Controller
    {
        private IOrderService orderService = null;
        private IUserService userService = null;

        public ChartController(IOrderService orderService, IUserService userService)
        {
            this.orderService = orderService;
            this.userService = userService;
        }

        [HttpGet]
        public ActionResult Index(int mode = 0)
        {
            List<OrderChart> orders = orderService.GetAllJoinedOrders(mode);
            
            TempVM vm = new TempVM() { Chart = MakeChart(orders) };
            return View(vm);
        }

        public FileContentResult GetChartView()
        {
            System.Web.Helpers.Chart chart = GetChart();
            byte[] bytes = chart.GetBytes("png");
            return File(bytes, "image/png");
        }

        public System.Web.Helpers.Chart GetChart()
        {
            ApplicationUser user = userService.GetAllUserValues(User.Identity.Name);
            List<Order> orders = orderService.GetOrdersFromUser(user.Id);
            var xvalues = (from o in orders select o.Orders.Count).ToList();
            var yvalues = (from o in orders select o.Timestamp).ToList();
            System.Web.Helpers.Chart chart = new System.Web.Helpers.Chart(width: 1000, height: 500, theme: ChartTheme.Blue)
                .AddTitle("Title")
                .AddLegend("Legend", "Legend")
                .AddSeries(name: "name", chartType: "Bar", axisLabel: "label", xValue: yvalues, yValues: xvalues);
            return chart;
        }

        [HttpPost]
        public RedirectToRouteResult Update(double selectedMode)
        {
            return RedirectToAction("Index", new { mode = selectedMode });
        }

        private LineChart MakeChart(List<OrderChart> orders)
        {
            LineChart chart = new LineChart();
            chart.ComplexData.Labels.AddRange(orders.Select(o => o.Label));
            chart.ComplexData.Datasets.AddRange(new List<ComplexDataset>() { new ComplexDataset() 
            { 
                Data = orders.Select(o => o.Count).ToList<double>(),
                Label = "Orders",
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
}