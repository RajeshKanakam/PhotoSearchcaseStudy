using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PhotoSearch.UnitTests
{
    [TestClass]
    public class MainWindowTests
    {
        [TestMethod]
        public void TestMainWindowObject()
        {
            Exception checkExeception =  null;
            try
            {
                var mainWindow = new MainWindow();
                Assert.IsNotNull(mainWindow);
            }
            catch (Exception exception)
            {
                checkExeception = exception;
            }
            Assert.IsNull(checkExeception);
        }

        [TestMethod]
        public void TestMainWindowVmObject()
        {
            Exception checkExeception = null;
            try
            {
                var mainWindow = new MainWindow();
                Assert.IsNotNull(mainWindow);
                Assert.IsNotNull(mainWindow.PhotoSearchViewModel);
            }
            catch (Exception exception)
            {
                checkExeception = exception;
            }
            Assert.IsNull(checkExeception);
        }
    }
}
