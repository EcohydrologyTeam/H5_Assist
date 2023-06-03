using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace F2F_Tests
{
  [TestClass]
  public class F2F_Tests_Files
  {
    // ----- File Tests

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Files")]
    public void F2F_Test_h5_open_close_file()
    {
      bool is_ok = F2F_Interface.test_h5_open_close_file();
      Assert.IsTrue(is_ok);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Files"),
      TestCategory("F2F Write")]
    public void F2F_Test_h5_create_file()
    {

      bool is_ok = F2F_Interface.test_h5_create_file();
      Assert.IsTrue(is_ok);
    }

  }
}
