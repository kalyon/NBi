﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBi.Core.Report
{
    public class DatabaseRequest : IQueryRequest
    {
        private readonly string source;
        private readonly string reportPath;
        private readonly string reportName;
        private readonly string dataSetName;

        public string Source
        {
            get
            {
                return source;
            }
        }

        public string ReportPath
        {
            get
            {
                return reportPath;
            }
        }

        public string ReportName
        {
            get
            {
                return reportName;
            }
        }

        public string DataSetName
        {
            get
            {
                return dataSetName;
            }
        }

        public DatabaseRequest(string connectionString, string reportPath, string reportName, string dataSetName)
        {
            this.source = connectionString;
            this.reportPath = reportPath;
            this.reportName = reportName;
            this.dataSetName = dataSetName;
        }
    }
}
