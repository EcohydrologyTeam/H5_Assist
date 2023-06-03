using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace F2F_Tests
{
  //----- Basic Tests -----

  [TestClass]
  public class F2F_Tests_BasicTests
  {
    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F BasicTests")]
    public void F2F_Test_add_two()
    {
      int x = 5;
      int y = F2F_Interface.test_add_two(ref x);
      Assert.AreEqual(7, y);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F BasicTests")]
    public void F2F_Test_add_one()
    {
      int x = 5;
      int y = F2F_Interface.test_add_one(ref x);
      Assert.AreEqual(6, y);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Arrays"),
      TestCategory("F2F BasicTests")]
    public void F2F_Test_pass_2d_float_array_two_way()
    {
      bool is_ok = F2F_Interface.test_pass_2d_float_array_two_way();
      Assert.IsTrue(is_ok);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Arrays"),
      TestCategory("F2F BasicTests")]
    public void F2F_Test_pass_2d_int_array_two_way()
    {
      bool is_ok = F2F_Interface.test_pass_2d_int_array_two_way();
      Assert.IsTrue(is_ok);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F BasicTests")]
    public void F2F_Test_h5_init_and_terminate()
    {
      bool is_ok = F2F_Interface.test_h5_init_and_terminate();
      Assert.IsTrue(is_ok);
    }

  }
}
