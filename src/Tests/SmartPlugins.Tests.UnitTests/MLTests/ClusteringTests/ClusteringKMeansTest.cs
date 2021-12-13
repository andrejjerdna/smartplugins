using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartPlugins.Common.Abstractions.ML;
using SmartPlugins.Common.ML;
using SmartPlugins.Common.ML.Attributes;
using System.Collections.Generic;

namespace SmartPlugins.Tests.UnitTests.MLTests.ClusteringTests
{
    [TestClass]
    public class ClusteringKMeansTest : BaseTest
    {
        [TestMethod("Clustering Kmeans with 3 clusters")]
        public void KMeansTest()
        {
            var testList = new List<KmeamsTestData>
            {
                new KmeamsTestData{TestProperty1="", TestProperty2=0f},
                new KmeamsTestData{TestProperty1="", TestProperty2=0f},
                new KmeamsTestData{TestProperty1="", TestProperty2=0f},
                new KmeamsTestData{TestProperty1="", TestProperty2=0f},

                new KmeamsTestData{TestProperty1="", TestProperty2=100f},
                new KmeamsTestData{TestProperty1="", TestProperty2=1222f},
                new KmeamsTestData{TestProperty1="", TestProperty2=3423f},
                new KmeamsTestData{TestProperty1="", TestProperty2=44f},

                new KmeamsTestData{TestProperty1="Test1", TestProperty2=340f},
                new KmeamsTestData{TestProperty1="Test2", TestProperty2=234f},
                new KmeamsTestData{TestProperty1="Test3", TestProperty2=3245f},
                new KmeamsTestData{TestProperty1="Test4", TestProperty2=32423f},
            };

            var kmeams = ContainerConfigure.GetRequiredService<IClusteringKMeans<KmeamsTestData>>();

            kmeams.MLTraining(testList, 3);

            var predict1 = kmeams.MLPredictor(testList[0]).Category;
            var predict2 = kmeams.MLPredictor(testList[5]).Category;
            var predict3 = kmeams.MLPredictor(testList[11]).Category;
            //Test logic
            Assert.IsTrue((predict1 + predict2 + predict3) == 6);
        }
    }

    class KmeamsTestData
    {
        [ClusterClassificationStatus(true, typeof(string), false, NormalizeType.None)]
        public string TestProperty1 { get; set; }

        [ClusterClassificationStatus(true, typeof(float), true, NormalizeType.NormalizeMeanVariance)]
        public float TestProperty2 { get; set; }
    }
}
