using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zeje.T4_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zeje.T4_.Tests
{
    [TestClass()]
    public class UtilsTests
    {
        [TestMethod()]
        public void CamelNameTest()
        {
            string result = Utils.CamelName(null);
            Assert.AreEqual("", result);
            result = Utils.CamelName("_");
            Assert.AreEqual("_", result);
            result = Utils.CamelName("a");
            Assert.AreEqual("A", result);
            result = Utils.CamelName("a", Utils.CamelType.小驼峰法);
            Assert.AreEqual("a", result);
            result = Utils.CamelName("_HELLO_WORLD");
            Assert.AreEqual("HelloWorld", result);
            result = Utils.CamelName("_HELLO_WORLD", Utils.CamelType.小驼峰法);
            Assert.AreEqual("helloWorld", result);
            result = Utils.CamelName("_hello_world");
            Assert.AreEqual("HelloWorld", result);
            result = Utils.CamelName("_hello_world", Utils.CamelType.小驼峰法);
            Assert.AreEqual("helloWorld", result);
            result = Utils.CamelName("__HELLO__wORLD__");
            Assert.AreEqual("HelloWorld", result);
            result = Utils.CamelName("__HELLO__wORLD__", Utils.CamelType.小驼峰法);
            Assert.AreEqual("helloWorld", result);


            result = Utils.CamelName("HELLO_WORLD");
            Assert.AreEqual("HelloWorld", result);
            result = Utils.CamelName("HELLO_WORLD", Utils.CamelType.小驼峰法);
            Assert.AreEqual("helloWorld", result);
            result = Utils.CamelName("hello_world");
            Assert.AreEqual("HelloWorld", result);
            result = Utils.CamelName("hello_world", Utils.CamelType.小驼峰法);
            Assert.AreEqual("helloWorld", result);

            result = Utils.CamelName("HELLO_wORLD");
            Assert.AreEqual("HelloWorld", result);
            result = Utils.CamelName("HELLO_wORLD", Utils.CamelType.小驼峰法);
            Assert.AreEqual("helloWorld", result);
            result = Utils.CamelName("hello_World");
            Assert.AreEqual("HelloWorld", result);
            result = Utils.CamelName("hello_World", Utils.CamelType.小驼峰法);
            Assert.AreEqual("helloWorld", result);
        }

        [TestMethod()]
        public void UnderScoreNameTest()
        {
            string result = Utils.UnderScoreName("HelloWorld", Utils.UnderScoreType.全部大写);
            Assert.AreEqual("HELLO_WORLD", result);
            result = Utils.UnderScoreName("helloWorld", Utils.UnderScoreType.全部大写);
            Assert.AreEqual("HELLO_WORLD", result);
            result = Utils.UnderScoreName("_HelloWorld", Utils.UnderScoreType.全部大写);
            Assert.AreEqual("_HELLO_WORLD", result);

            result = Utils.UnderScoreName("HelloWorld");
            Assert.AreEqual("hello_world", result);
            result = Utils.UnderScoreName("helloWorld");
            Assert.AreEqual("hello_world", result);
            result = Utils.UnderScoreName("_HelloWorld");
            Assert.AreEqual("_hello_world", result);
        }
    }
}