﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AWS.Lambda.PowerTools.Metrics
{
    public class MetricsContext : IDisposable
    {
        private RootNode _rootNode;
        internal bool IsSerializable => !(GetMetrics().Count == 0 && _rootNode.AWS.CustomMetadata.Count == 0);

        public MetricsContext()
        {
            _rootNode = new RootNode();
        }

        public MetricsContext(List<DimensionSet> dimensions) : this()
        {
            foreach (var dimension in dimensions)
            {
                AddDimension(dimension);
            }
        }

        public List<MetricDefinition> GetMetrics()
        {
            return _rootNode.AWS.GetMetrics();
        }

        public void ClearMetrics()
        {
            _rootNode.AWS.ClearMetrics();
        }

        internal void ClearNonDefaultDimensions()
        {
            _rootNode.AWS.ClearNonDefaultDimensions();
        }

        public void AddMetric(string key, double value, MetricUnit unit)
        {
            _rootNode.AWS.AddMetric(key, value, unit);
        }

        public void SetNamespace(string metricNamespace)
        {
            _rootNode.AWS.SetNamespace(metricNamespace);
        }

        internal string GetNamespace()
        {
            return _rootNode.AWS.GetNamespace();
        }

        public void AddDimension(DimensionSet dimension)
        {
            _rootNode.AWS.AddDimensionSet(dimension);
        }

        public void AddDimension(string key, string value)
        {
            _rootNode.AWS.AddDimensionSet(new DimensionSet(key, value));
        }

        public void SetDefaultDimensions(List<DimensionSet> defaultDimensions){
            _rootNode.AWS.SetDefaultDimensions(defaultDimensions);
        }

        public void SetDimensions(params DimensionSet[] dimensions)
        {
            _rootNode.AWS.SetDimensions(dimensions.ToList());
        }

        public void AddMetadata(string key, dynamic value)
        {
            _rootNode.AWS.AddMetadata(key, value);
        }

        public string Serialize()
        {
            return _rootNode.Serialize();
        }

        public void Dispose()
        {
            _rootNode = null;
        }
    }
}