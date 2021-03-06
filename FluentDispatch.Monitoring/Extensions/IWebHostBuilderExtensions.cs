﻿using System;
using App.Metrics;
using App.Metrics.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace FluentDispatch.Monitoring.Extensions
{
    public static class IWebHostBuilderExtensions
    {
        public static IWebHostBuilder UseMonitoring(this IWebHostBuilder builder, bool enabled)
        {
            builder
                .ConfigureMetricsWithDefaults(bld =>
                {
                    bld.Configuration.Configure(
                        options =>
                        {
                            options.Enabled = enabled;
                            options.ReportingEnabled = true;
                        });
                    if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("INFLUXDB")))
                    {
                        bld.Report.ToInfluxDb(options =>
                        {
                            options.InfluxDb.BaseUri = new Uri(Environment.GetEnvironmentVariable("INFLUXDB"));
                            options.InfluxDb.Database = "fluentdispatch";
                            options.FlushInterval = TimeSpan.FromSeconds(5);
                            options.InfluxDb.CreateDataBaseIfNotExists = true;
                            options.HttpPolicy.Timeout = TimeSpan.FromSeconds(10);
                        });
                    }
                });

            builder.UseMetricsWebTracking();
            builder.UseMetrics<MetricsStartup>();
            return builder;
        }
    }
}