﻿using System.Data;
using System.Data.SqlClient;
using Moq;
using NBi.Core.Query;
using NBi.Core.ResultSet;
using NUnit.Framework;

namespace NBi.Testing.Unit.NUnit
{
    [TestFixture]
    public class EqualToConstraintTest
    {
        [Test]
        public void Matches_AnyResultSets_EngineCalledOnceResultSetBuilderTwice()
        {
            var rs = new ResultSet("a;b;c");
            var cmd = new SqlCommand();

            var rsbMock = new Mock<ResultSetBuilder>();
            rsbMock.Setup(engine => engine.Build(It.IsAny<object>()))
                .Returns(rs);
            var rsb = rsbMock.Object;

            var rscMock = new Mock<IResultSetComparer>();
            rscMock.Setup(engine => engine.Compare(It.IsAny<ResultSet>(), It.IsAny<ResultSet>()))
                .Returns(0);
            var rsc = rscMock.Object;

            var equalToConstraint = new NBi.NUnit.EqualToConstraint(rs) {Engine = rsc, ResultSetBuilder=rsb };
            equalToConstraint.ResultSetBuilder = rsb;

            //Method under test
            equalToConstraint.Matches(cmd);

            //Test conclusion            
            //Test conclusion            
            rscMock.Verify(engine => engine.Compare(It.IsAny<ResultSet>(), It.IsAny<ResultSet>()), Times.Once());
            rsbMock.Verify(engine => engine.Build(It.IsAny<object>()), Times.Exactly(2));
        }

        [Test]
        public void Matches_IDbCommandAsActualAndPathAsExpectation_ResulSetBuildersCreateResultSetsUsingActualAndExpectationProvided()
        {
            var rsActual = new ResultSet("a;b;c");
            var rsExpect = new ResultSet("x;y;z");
            var cmd = new SqlCommand();

            var rsbMock = new Mock<IResultSetBuilder>();
            rsbMock.Setup(engine => engine.Build(cmd))
                .Returns(rsActual);
            rsbMock.Setup(engine => engine.Build("my path for expectation"))
                .Returns(rsExpect);
            var rsb = rsbMock.Object;

            var equalToConstraint = new NBi.NUnit.EqualToConstraint("my path for expectation") {ResultSetBuilder = rsb };
            equalToConstraint.ResultSetBuilder = rsb;

            //Method under test
            equalToConstraint.Matches(cmd);

            //Test conclusion            
            //Test conclusion            
            rsbMock.Verify(engine => engine.Build(cmd), Times.Once());
            rsbMock.Verify(engine => engine.Build("my path for expectation"), Times.Once());
        }
        

        [Test]
        public void Matches_AnyIDbCommandAsActualAndAnyPathAsExpectation_EngineCompareTheTwoResultSetsPreviouslyCreated()
        {
            var rsActual = new ResultSet("a;b;c");
            var rsExpect = new ResultSet("x;y;z");
            var cmd = new SqlCommand();

            var rsbStub = new Mock<IResultSetBuilder>();
            rsbStub.Setup(engine => engine.Build(It.IsAny<IDbCommand>()))
                .Returns(rsActual);
            rsbStub.Setup(engine => engine.Build(It.IsAny<string>()))
                .Returns(rsExpect);
            var rsbFake = rsbStub.Object;

            var rscMock = new Mock<IResultSetComparer>();
            rscMock.Setup(engine => engine.Compare(rsActual, rsExpect))
                .Returns(1);
            var rsc = rscMock.Object;

            var equalToConstraint = new NBi.NUnit.EqualToConstraint("my path for expectation") {ResultSetBuilder = rsbFake, Engine = rsc };

            //Method under test
            equalToConstraint.Matches(cmd);

            //Test conclusion            
            //Test conclusion            
            rscMock.Verify(engine => engine.Compare(rsActual, rsExpect), Times.Once());
        }
        [Test]
        public void Matches_TwoIdenticalResultSets_ReturnTrue()
        {
            var rs = new ResultSet("a;b;c");

            var cmd = new SqlCommand();

            var rsbMock = new Mock<IResultSetBuilder>();
            rsbMock.Setup(engine => engine.Build(It.IsAny<IDbCommand>()))
                .Returns(rs);
            rsbMock.Setup(engine => engine.Build(rs))
                .Returns(rs);
            var rsb = rsbMock.Object;

            var equalToConstraint = new NBi.NUnit.EqualToConstraint(rs) { ResultSetBuilder = rsb };

            //Method under test
            var res = equalToConstraint.Matches(cmd);

            //Test conclusion            
            rsbMock.Verify(engine => engine.Build(rs), Times.Once());
            Assert.That(res, Is.True);
        }

        [Test]
        public void Matches_TwoDifferentResultSets_ReturnFalse()
        {
            var rsActual = new ResultSet("a;b;c");
            var rsExpect = new ResultSet("a;X;c");

            var cmd = new SqlCommand();

            var rsbMock = new Mock<IResultSetBuilder>();
            rsbMock.Setup(engine => engine.Build(It.IsAny<IDbCommand>()))
                .Returns(rsActual);
            rsbMock.Setup(engine => engine.Build(rsExpect))
                .Returns(rsExpect);
            var rsb = rsbMock.Object;

            var equalToConstraint = new NBi.NUnit.EqualToConstraint(rsExpect) {ResultSetBuilder = rsb };

            //Method under test
            var res = equalToConstraint.Matches(cmd);

            //Test conclusion            
            rsbMock.Verify(engine => engine.Build(rsExpect), Times.Once());
            Assert.That(res, Is.False);
        }

        

       
    }
}
