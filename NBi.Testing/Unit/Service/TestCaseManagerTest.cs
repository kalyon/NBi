﻿using System;
using System.Data;
using System.Linq;
using NBi.Service;
using NUnit.Framework;

namespace NBi.Testing.Unit.Service
{
    [TestFixture]
    public class TestCaseManagerTest
    {
        [Test]
        public void Filter_Equal_CorrectNewContent()
        {
            var manager = new TestCaseManager();
            //Setup content;
            manager.Content.Columns.Add(new DataColumn("columnName"));
            manager.Variables.Add("columnName");
            var matchingRow = manager.Content.NewRow();
            matchingRow[0]="matching";
            var nonMatchingRow = manager.Content.NewRow();
            nonMatchingRow[0]="xyz";
            manager.Content.Rows.Add(matchingRow);
            manager.Content.Rows.Add(nonMatchingRow);
            manager.Content.AcceptChanges();

            //Setup filter
            manager.Filter("columnName", Operator.Equal, false, "matching");

            Assert.That(manager.Content.Rows, Has.Count.EqualTo(1));
            Assert.That(manager.Content.Rows[0][0], Is.EqualTo("matching"));
        }

        [Test]
        public void Filter_NotEqual_CorrectNewContent()
        {
            var manager = new TestCaseManager();
            //Setup content;
            manager.Content.Columns.Add(new DataColumn("columnName"));
            manager.Variables.Add("columnName");
            var matchingRow = manager.Content.NewRow();
            matchingRow[0] = "matching";
            var nonMatchingRow = manager.Content.NewRow();
            nonMatchingRow[0] = "xyz";
            manager.Content.Rows.Add(matchingRow);
            manager.Content.Rows.Add(nonMatchingRow);
            manager.Content.AcceptChanges();

            //Setup filter
            manager.Filter("columnName", Operator.Equal, true, "matching");

            Assert.That(manager.Content.Rows, Has.Count.EqualTo(1));
            Assert.That(manager.Content.Rows[0][0], Is.Not.EqualTo("matching"));
        }

        [Test]
        public void Filter_LikeStart_CorrectNewContent()
        {
            var manager = new TestCaseManager();
            //Setup content;
            manager.Content.Columns.Add(new DataColumn("columnName"));
            manager.Variables.Add("columnName");
            var matchingRow = manager.Content.NewRow();
            matchingRow[0] = "matching";
            var nonMatchingRow = manager.Content.NewRow();
            nonMatchingRow[0] = "xyz";
            manager.Content.Rows.Add(matchingRow);
            manager.Content.Rows.Add(nonMatchingRow);
            manager.Content.AcceptChanges();

            //Setup filter
            manager.Filter("columnName", Operator.Like, false, "match%");

            Assert.That(manager.Content.Rows, Has.Count.EqualTo(1));
            Assert.That(manager.Content.Rows[0][0], Is.EqualTo("matching"));
        }

        [Test]
        public void Filter_LikeEnd_CorrectNewContent()
        {
            var manager = new TestCaseManager();
            //Setup content;
            manager.Content.Columns.Add(new DataColumn("columnName"));
            manager.Variables.Add("columnName");
            var matchingRow = manager.Content.NewRow();
            matchingRow[0] = "matching";
            var nonMatchingRow = manager.Content.NewRow();
            nonMatchingRow[0] = "xyz";
            manager.Content.Rows.Add(matchingRow);
            manager.Content.Rows.Add(nonMatchingRow);
            manager.Content.AcceptChanges();

            //Setup filter
            manager.Filter("columnName", Operator.Like, false, "%ing");

            Assert.That(manager.Content.Rows, Has.Count.EqualTo(1));
            Assert.That(manager.Content.Rows[0][0], Is.EqualTo("matching"));
        }

        [Test]
        public void Filter_LikeContain_CorrectNewContent()
        {
            var manager = new TestCaseManager();
            //Setup content;
            manager.Content.Columns.Add(new DataColumn("columnName"));
            manager.Variables.Add("columnName");
            var matchingRow = manager.Content.NewRow();
            matchingRow[0] = "matching";
            var nonMatchingRow = manager.Content.NewRow();
            nonMatchingRow[0] = "xyz";
            manager.Content.Rows.Add(matchingRow);
            manager.Content.Rows.Add(nonMatchingRow);
            manager.Content.AcceptChanges();

            //Setup filter
            manager.Filter("columnName", Operator.Like, false, "%atch%");

            Assert.That(manager.Content.Rows, Has.Count.EqualTo(1));
            Assert.That(manager.Content.Rows[0][0], Is.EqualTo("matching"));
        }
        
        [Test]
        public void Filter_LikeContainBounded_CorrectNewContent()
        {
            var manager = new TestCaseManager();
            //Setup content;
            manager.Content.Columns.Add(new DataColumn("columnName"));
            manager.Variables.Add("columnName");
            var matchingRow = manager.Content.NewRow();
            matchingRow[0] = "matching";
            var nonMatchingRow = manager.Content.NewRow();
            nonMatchingRow[0] = "xyz";
            manager.Content.Rows.Add(matchingRow);
            manager.Content.Rows.Add(nonMatchingRow);
            manager.Content.AcceptChanges();

            //Setup filter
            manager.Filter("columnName", Operator.Like, false, "%matching%");

            Assert.That(manager.Content.Rows, Has.Count.EqualTo(1));
            Assert.That(manager.Content.Rows[0][0], Is.EqualTo("matching"));
        }

        [Test]
        public void Filter_LikeContainComplex_CorrectNewContent()
        {
            var manager = new TestCaseManager();
            //Setup content;
            manager.Content.Columns.Add(new DataColumn("columnName"));
            manager.Variables.Add("columnName");
            var matchingRow = manager.Content.NewRow();
            matchingRow[0] = "matching";
            var nonMatchingRow = manager.Content.NewRow();
            nonMatchingRow[0] = "xyz";
            manager.Content.Rows.Add(matchingRow);
            manager.Content.Rows.Add(nonMatchingRow);
            manager.Content.AcceptChanges();

            //Setup filter
            manager.Filter("columnName", Operator.Like, false, "ma%h%ng%");

            Assert.That(manager.Content.Rows, Has.Count.EqualTo(1));
            Assert.That(manager.Content.Rows[0][0], Is.EqualTo("matching"));
        }

        [Test]
        public void Filter_EqualWithoutMatch_EmptyContent()
        {
            var manager = new TestCaseManager();
            //Setup content;
            manager.Content.Columns.Add(new DataColumn("columnName"));
            manager.Variables.Add("columnName");
            var nonMatchingRow1 = manager.Content.NewRow();
            nonMatchingRow1[0] = "abc";
            var nonMatchingRow2 = manager.Content.NewRow();
            nonMatchingRow2[0] = "xyz";
            manager.Content.Rows.Add(nonMatchingRow1);
            manager.Content.Rows.Add(nonMatchingRow2);
            manager.Content.AcceptChanges();

            //Setup filter
            manager.Filter("columnName", Operator.Equal, false, "matching");

            Assert.That(manager.Content.Rows, Has.Count.EqualTo(0));
        }

        [Test]
        public void Filter_Distinct_CorrectNewContent()
        {
            var manager = new TestCaseManager();
            //Setup content;
            manager.Content.Columns.Add(new DataColumn("columnName"));
            manager.Variables.Add("columnName");
            var firstRow = manager.Content.NewRow();
            firstRow[0] = "alpha";
            var secondRow = manager.Content.NewRow();
            secondRow[0] = "beta";
            var duplicatedRow = manager.Content.NewRow();
            duplicatedRow[0] = "alpha";
            manager.Content.Rows.Add(firstRow);
            manager.Content.Rows.Add(secondRow);
            manager.Content.Rows.Add(duplicatedRow);
            manager.Content.AcceptChanges();

            //Setup filter
            manager.FilterDistinct();

            Assert.That(manager.Content.Rows, Has.Count.EqualTo(2));
            Assert.That(manager.Content.Rows[0][0], Is.EqualTo("alpha"));
            Assert.That(manager.Content.Rows[1][0], Is.EqualTo("beta"));
        }

        [Test]
        public void Filter_Distinct_EmptyContent()
        {
            var manager = new TestCaseManager();
            //Setup content;
            manager.Content.Columns.Add(new DataColumn("columnName"));
            manager.Variables.Add("columnName");
            manager.Content.AcceptChanges();

            //Setup filter
            manager.FilterDistinct();

            Assert.That(manager.Content.Rows, Has.Count.EqualTo(0));
        }

        [Test]
        public void Filter_EqualMultipleValues_CorrectNewContent()
        {
            var manager = new TestCaseManager();
            //Setup content;
            manager.Content.Columns.Add(new DataColumn("columnName"));
            manager.Variables.Add("columnName");
            var matchingRow1 = manager.Content.NewRow();
            matchingRow1[0] = "matching 1";
            var matchingRow2 = manager.Content.NewRow();
            matchingRow2[0] = "matching 2";
            var nonMatchingRow = manager.Content.NewRow();
            nonMatchingRow[0] = "xyz";
            manager.Content.Rows.Add(matchingRow1);
            manager.Content.Rows.Add(matchingRow2);
            manager.Content.Rows.Add(nonMatchingRow);
            manager.Content.AcceptChanges();

            //Setup filter
            manager.Filter("columnName", Operator.Equal, false, new [] {"matching 1", "matching 2"});

            Assert.That(manager.Content.Rows, Has.Count.EqualTo(2));
            Assert.That(manager.Content.Rows[0][0], Is.EqualTo("matching 1"));
            Assert.That(manager.Content.Rows[1][0], Is.EqualTo("matching 2"));
        }

        [Test]
        public void Filter_NotEqualMultipleValues_CorrectNewContent()
        {
            var manager = new TestCaseManager();
            //Setup content;
            manager.Content.Columns.Add(new DataColumn("columnName"));
            manager.Variables.Add("columnName");
            var matchingRow1 = manager.Content.NewRow();
            matchingRow1[0] = "matching 1";
            var matchingRow2 = manager.Content.NewRow();
            matchingRow2[0] = "matching 2";
            var nonMatchingRow = manager.Content.NewRow();
            nonMatchingRow[0] = "xyz";
            manager.Content.Rows.Add(matchingRow1);
            manager.Content.Rows.Add(matchingRow2);
            manager.Content.Rows.Add(nonMatchingRow);
            manager.Content.AcceptChanges();

            //Setup filter
            manager.Filter("columnName", Operator.Equal, true, new[] { "matching 1", "matching 2" });

            Assert.That(manager.Content.Rows, Has.Count.EqualTo(1));
            Assert.That(manager.Content.Rows[0][0], Is.EqualTo("xyz"));
        }

        public void Filter_LikeMultipleValues_CorrectNewContent()
        {
            var manager = new TestCaseManager();
            //Setup content;
            manager.Content.Columns.Add(new DataColumn("columnName"));
            manager.Variables.Add("columnName");
            var matchingRow1 = manager.Content.NewRow();
            matchingRow1[0] = "matching 1";
            var matchingRow2 = manager.Content.NewRow();
            matchingRow2[0] = "matching 2";
            var nonMatchingRow = manager.Content.NewRow();
            nonMatchingRow[0] = "xyz";
            manager.Content.Rows.Add(matchingRow1);
            manager.Content.Rows.Add(matchingRow2);
            manager.Content.Rows.Add(nonMatchingRow);
            manager.Content.AcceptChanges();

            //Setup filter
            manager.Filter("columnName", Operator.Like, false, new[] { "%1", "%2" });

            Assert.That(manager.Content.Rows, Has.Count.EqualTo(2));
            Assert.That(manager.Content.Rows[0][0], Is.EqualTo("matching 1"));
            Assert.That(manager.Content.Rows[1][0], Is.EqualTo("matching 2"));
        }
    }
}
