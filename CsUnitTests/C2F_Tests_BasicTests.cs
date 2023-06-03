using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C2F_Tests
{
  [TestClass]
  public class C2F_Tests_BasicTests
  {
    //----- Basic Tests -----

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F BasicTests")]
    //[Priority(1)]
    public void C2F_Test_add_one()
    {
      int x = 5;
      int y = C2F_Interface.add_one(ref x);
      Assert.AreEqual(6, y);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Arrays"),
      TestCategory("C2F BasicTests")]
    public void C2F_Test_pass_2d_float_array_two_way()
    {
      long nrows = 5;
      long ncols = 7;
      var myarray = new float[nrows, ncols];
      C2F_Interface.pass_2d_float_array_two_way(myarray, ref nrows, ref ncols);
      for (int i = 0; i < nrows; i++) {
        for (int j = 0; j < ncols; j++) {
          Console.WriteLine(i + " " + j + ": " + myarray[i, j]);
        }
      }
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Arrays"),
      TestCategory("C2F BasicTests")]
    public void C2F_Test_pass_2d_int_array_two_way()
    {
      long nrows = 5;
      long ncols = 7;
      var myarray = new int[nrows, ncols];
      C2F_Interface.pass_2d_int_array_two_way(myarray, ref nrows, ref ncols);
      for (int i = 0; i < nrows; i++) {
        for (int j = 0; j < ncols; j++) {
          Console.WriteLine(i + " " + j + ": " + myarray[i, j]);
        }
      }
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F BasicTests")]
    //[Priority(2)]
    public void C2F_Test_h5_init_and_terminate()
    {
      bool is_ok;
      is_ok = C2F_Interface.h5_init();
      Assert.IsTrue(is_ok);
      is_ok = C2F_Interface.h5_terminate();
      Assert.IsTrue(is_ok);
    }
  }
}
