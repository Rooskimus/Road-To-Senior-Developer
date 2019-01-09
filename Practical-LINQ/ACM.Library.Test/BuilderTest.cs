using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ACM.Library.Test
{
    [TestClass]
    public class BuilderTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void BuildIntegerSequenceTest()
        {
            //Arrange
            Builder listBuilder = new Builder();

            //Act

            var result = listBuilder.BuildIntegerSequence();

            //Analyze
            foreach (var item in result)
            {
                TestContext.WriteLine(item.ToString());
            }

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void BuildRepeatedIntegerSequenceTest()
        {
            //Arrange
            Builder listBuilder = new Builder();

            //Act

            var result = listBuilder.BuildRepeatedIntegerSequence();

            //Analyze
            foreach (var item in result)
            {
                TestContext.WriteLine(item.ToString());
            }

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void BuildStringSequenceTest()
        {
            //Arrange
            Builder listBuilder = new Builder();

            //Act

            var result = listBuilder.BuildStringSequence();

            //Analyze
            foreach (var item in result)
            {
                TestContext.WriteLine(item);
            }

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void BuildRandomStringSequenceTest()
        {
            //Arrange
            Builder listBuilder = new Builder();

            //Act

            var result = listBuilder.BuildRandomStringSequence();

            //Analyze
            foreach (var item in result)
            {
                TestContext.WriteLine(item);
            }

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void BuildRepeatedtringSequenceTest()
        {
            //Arrange
            Builder listBuilder = new Builder();

            //Act

            var result = listBuilder.BuildRepeatedStringSequence();

            //Analyze
            foreach (var item in result)
            {
                TestContext.WriteLine(item);
            }

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CompareSequenceTest()
        {
            //Arrange
            Builder listBuilder = new Builder();

            //Act

            var result = listBuilder.CompareSequences();

            //Analyze
            foreach (var item in result)
            {
                TestContext.WriteLine(item.ToString());
            }

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CompareSequenceExceptTest()
        {
            //Arrange
            Builder listBuilder = new Builder();

            //Act

            var result = listBuilder.CompareSequencesExcept();

            //Analyze
            foreach (var item in result)
            {
                TestContext.WriteLine(item.ToString());
            }

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CompareSequenceConcatTest()
        {
            //Arrange
            Builder listBuilder = new Builder();

            //Act

            var result = listBuilder.CompareSequencesConcat();

            //Analyze
            foreach (var item in result)
            {
                TestContext.WriteLine(item.ToString());
            }

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
