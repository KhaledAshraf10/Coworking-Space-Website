using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ChartExample.Models.Chart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Workspace.Models;

/*using Select.Pdf;
*/
namespace Workspace.Pages.Charts
{
    public class ChartsModel : PageModel
    {

        private readonly DB _db;

        public ChartJs Chart { get; set; }
        public string MonthlyCustomersChartJson { get; set; }
        
        public string TodayChartJson { get; set; }
        public string MonthChartJson { get; set; }
        public string AllTimeChartJson { get; set; }
        public string TodayOccupancyChartJson { get; set; }
        public string YesterdayOccupancyChartJson { get; set; }
        public string AllTimeOccupancyChartJson { get; set; }
        public string EventRevAttendChartJson { get; set; }

        public string ThisMonthTotalMoneyString { get; set; }
        public string AllTimeTotalMoneyString { get; set; }
        public string TodayTotalMoneyString { get; set; }
        public string EventTotalMoneyString { get; set; }
        public string EventTotalAttendantsString { get; set; }
        public string TodayNewCustomersString { get; set; }
        public string WeeklyNewCustomersString { get; set; }

        public string MonthlyNewCustomersString { get; set; }

        public ChartsModel(DB db)
        {
            _db = db;
        }

        private string[] MyCustomColorArray = new string[]
        {
            "#00A99D", // Dark turquoise
            "#8BC34A", // Light green
            "#4A148C", // Deep purple
            "#1A237E", // Dark blue
            "#1976D2", // Bright blue
            "#004D40", // Dark green
            "#00796B", // Teal
            "#689F38", // Olive green
            "#FFA000", // Amber
            "#455A64", // Dark grayish blue
            "#616161", // Gray
            "#D7D5C5", // Light gray
           "#BDB9A8" // Beige
        };

        private string[] GenerateCustomColorArray(int count)
        {
            var random = new Random();
            var shuffledColors = MyCustomColorArray.OrderBy(c => random.Next()).ToList();
            var colors = shuffledColors.Take(count).ToList();
            return colors.ToArray();
        }




        public void OnGet()
        {
           
            var TodaysalesData = _db.GetSalesForTodayByRoomType();
            var TodaytotalSales = TodaysalesData.Values.Sum();
            var TodaypercentageValues = TodaysalesData.Values.Select(value => (int)Math.Round((decimal)value / (decimal)TodaytotalSales * 100)).ToArray();
            var TodaychartData = new
            {
                type = "pie",
                data = new
                {
                    labels = TodaysalesData.Keys.Zip(TodaypercentageValues, (k, v) => k + " (" + v + "%)").ToArray(),
                    datasets = new[]
                    {
                new
                {
                    label = "Room Sales Today",
                    data = TodaysalesData.Values.ToArray(),
                    backgroundColor = GenerateCustomColorArray(TodaysalesData.Count),
                    borderColor = "rgba(0, 0, 0, 0)",
                    borderWidth = 1
                }
            }
                },
                options = new
                {
                    responsive = true,
                    maintainAspectRatio = false,
                }
            };

            TodayChartJson = JsonConvert.SerializeObject(TodaychartData, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            });

            TodayTotalMoneyString = string.Format("{0:n2} L.E.", TodaytotalSales);


            var salesData = _db.GetSalesByRoomType();
            var totalSales = salesData.Values.Sum();
            var percentageValues = salesData.Values.Select(value => (int)Math.Round((decimal)value / (decimal)totalSales * 100)).ToArray();
            var chartData = new
            {
                type = "pie",
                data = new
                {
                    labels = salesData.Keys.Zip(percentageValues, (k, v) => k + " (" + v + "%)").ToArray(),
                    datasets = new[]
                    {
                new
                {
                    label = "Room Sales This Month",
                    data = salesData.Values.ToArray(),
                    backgroundColor = GenerateCustomColorArray(salesData.Count),
                    borderColor = "rgba(0, 0, 0, 0)",
                    borderWidth = 1
                }
            }
                },
                options = new
                {
                    responsive = true,
                    maintainAspectRatio = false,
                }
            };

            MonthChartJson = JsonConvert.SerializeObject(chartData, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            });

            ThisMonthTotalMoneyString = string.Format("{0:n2} L.E.", totalSales);

            //
            //
        
            var allTimeSalesData = _db.GetAllTimeSalesByRoomType();
            var allTimeTotalSales = allTimeSalesData.Values.Sum();
            var allTimePercentageValues = allTimeSalesData.Values.Select(value => (int)Math.Round((decimal)value / (decimal)allTimeTotalSales * 100)).ToArray();
            var allTimeChartData = new
            {
                type = "pie",
                data = new
                {
                    labels = allTimeSalesData.Keys.Zip(allTimePercentageValues, (k, v) => k + " (" + v + "%)").ToArray(),
                    datasets = new[]
                    {
                new
                {
                    label = "Room Sales All Time",
                    data = allTimeSalesData.Values.ToArray(),
                    backgroundColor = GenerateCustomColorArray(allTimeSalesData.Count),
                    borderColor = "rgba(0, 0, 0, 0)",
                    borderWidth = 1
                }
            }
                },
                options = new
                {
                    responsive = true,
                    maintainAspectRatio = false,
                }
            };

            AllTimeChartJson = JsonConvert.SerializeObject(allTimeChartData, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            });

            AllTimeTotalMoneyString = string.Format("{0:n2} L.E.", allTimeTotalSales);

            //
            //

            var MonthlyCustomersData = _db.GetNewCustomersLast30Days();
            var totalNewCustomers = MonthlyCustomersData.Values.Sum();
            var MonthlyCustomerschartData = new
            {
                type = "bar",
                data = new
                {
                    labels = MonthlyCustomersData.Keys.Select(d => DateTime.Parse(d.ToString()).ToString("M/d")).ToArray(),
                    datasets = new[]
                    {
            new
            {
                label = "New Customers That Day",
                data = MonthlyCustomersData.Values.ToArray(),
                backgroundColor = GenerateCustomColorArray(MonthlyCustomersData.Count),
                borderColor = "rgba(0, 0, 0, 0)",
                borderWidth = 1
            }
        }
                },
                options = new
                {
                    responsive = true,
                    maintainAspectRatio = false
                }
            };

            MonthlyCustomersChartJson = JsonConvert.SerializeObject(MonthlyCustomerschartData, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            var currentDate = DateTime.Now;
            var Today = currentDate;
            var lastWeek = currentDate.AddDays(-7);

            var newCustomersLastDay = MonthlyCustomersData.Where(x => DateTime.Parse(x.Key).Date == Today.Date).Sum(x => x.Value);
            TodayNewCustomersString = string.Format("Today: {0:n0} ", newCustomersLastDay);

            var newCustomersLastWeek = MonthlyCustomersData.Where(x => DateTime.TryParse(x.Key, out DateTime date) && date.Date >= lastWeek.Date && date.Date <= currentDate.Date).Sum(x => x.Value);
            WeeklyNewCustomersString = string.Format("Week: {0:n0} ", newCustomersLastWeek);

            MonthlyNewCustomersString = string.Format("Month: {0:n0} ", totalNewCustomers);

            var TodayoccupancyData = _db.GetOccupancyByRoomTypeToday();
            var TodayoccupancyValues = TodayoccupancyData.Values.Select(value => value).ToArray();
            var TodayoccupancyLabels = TodayoccupancyData.Keys.ToArray();

            var TodayoccupancyChartData = new
            {
                type = "bar",
                data = new
                {
                    labels = TodayoccupancyLabels,
                    datasets = new[]
                    {
                        new
                        {
                            label = "Average Occupancy",
                            data = TodayoccupancyValues,
                            backgroundColor = GenerateCustomColorArray(TodayoccupancyData.Count),
                            borderColor = "rgba(0, 0, 0, 0)",
                            borderWidth = 1
                        }
                    }
                },
                options = new
                {
                    responsive = true,
                    maintainAspectRatio = false,
                    scales = new
                    {
                        yAxes = new[]
                        {
                            new
                            {
                                ticks = new
                                {
                                    beginAtZero = true,
                                    callback = @"function(value, index, values) {
                                        return value + '%';
                                    }"
                                },
                                scaleLabel = new
                                {
                                    display = true,
                                    labelString = "Occupancy Rate"
                                }
                            }
                        }
                    },
                    legend = new
                    {
                        display = false,
                        labels = new
                        {
                            fontColor = "rgba(0,0,0,0)"
                        }
                    }
                }
            };

            TodayOccupancyChartJson = JsonConvert.SerializeObject(TodayoccupancyChartData, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            });

            //
            //

            var YesterdayoccupancyData = _db.GetOccupancyByRoomTypeYesterday();
            var YesterdayoccupancyValues = YesterdayoccupancyData.Values.Select(value => value).ToArray();
            var YesterdayoccupancyLabels = YesterdayoccupancyData.Keys.ToArray();

            var YesterdayoccupancyChartData = new
            {
                type = "bar",
                data = new
                {
                    labels = YesterdayoccupancyLabels,
                    datasets = new[]
                    {
                        new
                        {
                            label = "Average Occupancy",
                            data = YesterdayoccupancyValues,
                            backgroundColor = GenerateCustomColorArray(YesterdayoccupancyData.Count),
                            borderColor = "rgba(0, 0, 0, 0)",
                            borderWidth = 1
                        }
                    }
                },
                options = new
                {
                    responsive = true,
                    maintainAspectRatio = false,
                    scales = new
                    {
                        yAxes = new[]
                        {
                            new
                            {
                                ticks = new
                                {
                                    beginAtZero = true,
                                    callback = @"function(value, index, values) {
                                        return value + '%';
                                    }"
                                },
                                scaleLabel = new
                                {
                                    display = true,
                                    labelString = "Occupancy Rate"
                                }
                            }
                        }
                    },
                    legend = new
                    {
                        display = false,
                        labels = new
                        {
                            fontColor = "rgba(0,0,0,0)"
                        }
                    }
                }
            };

            YesterdayOccupancyChartJson = JsonConvert.SerializeObject(YesterdayoccupancyChartData, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            });

            //
            //

            var occupancyData = _db.GetOccupancyByRoomType();
            var occupancyValues = occupancyData.Values.Select(dict => dict.Values.Last()).ToArray();
            var occupancyLabels = occupancyData.Keys.ToArray();
            var occupancyChartData = new
            {
                type = "bar",
                data = new
                {
                    labels = occupancyLabels,
                    datasets = new[]
                    {
                        new
                        {
                            label = "Average Occupancy",
                            data = occupancyValues,
                            backgroundColor = GenerateCustomColorArray(occupancyData.Count),
                            borderColor = "rgba(0, 0, 0, 0)",
                            borderWidth = 1
                        }
                    }
                },
                options = new
                {
                    responsive = true,
                    maintainAspectRatio = false,
                    scales = new
                    {
                        yAxes = new[]
                        {
                            new
                            {
                                ticks = new
                                {
                                    beginAtZero = true,
                                    callback = @"function(value, index, values) {
                                        return value + '%';
                                    }"
                                },
                                scaleLabel = new
                                {
                                    display = true,
                                    labelString = "Occupancy Rate"
                                }
                            }
                        }
                    },
                    legend = new
                    {
                        display = false,
                        labels = new
                        {
                            fontColor = "rgba(0,0,0,0)"
                        }
                    }
                }
            };

            AllTimeOccupancyChartJson = JsonConvert.SerializeObject(occupancyChartData, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            });

            //
            //

            var EventRevAttendData = _db.GetEventsData();
            var EventRevAttendValues = EventRevAttendData.Values.Select(dict => {
                double totalAttendees = 0;
                double totalCost = 0;
                foreach (var dateData in dict)
                {
                    double cost = dateData.Value[0];
                    double numAttendees = dateData.Value[1];
                    totalAttendees += numAttendees;
                    totalCost += cost;
                }
                double occupancyRate = totalAttendees;
                return new { OccupancyRate = occupancyRate, Cost = totalCost };
            }).ToArray();


            var EventRevAttendLabels = EventRevAttendData.Keys.ToArray();
            var EventRevAttendChartData = new
            {
                type = "bar",
                data = new
                {
                    labels = EventRevAttendLabels,
                    datasets = new[]
        {
            new
            {
                label = "Number of Attendants",
                data = EventRevAttendValues.Select(x => x.OccupancyRate).ToArray(),
                backgroundColor = GenerateCustomColorArray(EventRevAttendData.Count),
                borderColor = "rgba(0, 0, 0, 0)",
                borderWidth = 1
            },
            new
            {
                label = "Cost",
                data = EventRevAttendValues.Select(x => x.Cost).ToArray(),
                backgroundColor = GenerateCustomColorArray(EventRevAttendData.Count),
                borderColor = "rgba(0, 0, 0, 0)",
                borderWidth = 1
            }
        }
                },
                options = new
                {
                    responsive = true,
                    maintainAspectRatio = false,
                    scales = new
                    {
                        yAxes = new object[]
{
    new
    {
        ticks = new
        {
            beginAtZero = true,
            callback = @"function(value, index, values) {
                return value + '%';
            }"
        },
        scaleLabel = new
        {
            display = true,
            labelString = "Occupancy Rate"
        }
    },
    new
    {
        ticks = new
        {
            beginAtZero = true,
            callback = @"function(value, index, values) {
                return '$' + value;
            }"
        },
        position = "right",
        scaleLabel = new
        {
            display = true,
            labelString = "Cost"
        }
    }
}
                    },
                    legend = new
                    {
                        display = true,
                        labels = new
                        {
                            fontColor = "rgba(0,0,0,1)"
                        }
                    }
                }
            };

            EventRevAttendChartJson = JsonConvert.SerializeObject(EventRevAttendChartData, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            });

            EventTotalMoneyString = string.Format("Total Event Money: {0:n0} L.E.", EventRevAttendValues.Select(x => x.Cost).Sum()) ;
            EventTotalAttendantsString = string.Format("Total Attendants: {0:n0}", EventRevAttendValues.Sum(x => x.OccupancyRate));
        }

       /* public void pdfConverter()
        {
            SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
            SelectPdf.PdfDocument doc = converter.ConvertUrl("http://selectpdf.com");
            doc.Save("test.pdf");
            doc.Close();
        }*/

    }
}