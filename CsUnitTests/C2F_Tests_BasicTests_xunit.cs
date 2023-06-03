using Xunit;
using System;

namespace C2F_Tests
{
  [Trait("xunit: All", "")]
  [Trait("xunit: C2F All", "")]
  [Trait("xunit: C2F Basic Tests", "")]
  public class C2F_Tests_BasicTests_xunit
  {

    [Fact]
    public void C2F_Test_add_one()
    {
      int x = 5;
      int y = C2F_Interface.add_one(ref x);
      Assert.Equal(6, y);
    }

    [Fact]
    public void C2F_Test_pass_2d_float_array_two_way()
    {
      long nrows = 5;
      long ncols = 7;
      var myarray = new float[nrows, ncols];
      C2F_Interface.pass_2d_float_array_two_way(myarray, ref nrows, ref ncols);
      for (int i = 0; i < nrows; i++)
      {
        for (int j = 0; j < ncols; j++)
        {
          Console.WriteLine(i + " " + j + ": " + myarray[i, j]);
        }
      }
    }

    [Fact]
    public void C2F_Test_pass_2d_int_array_two_way()
    {
      long nrows = 5;
      long ncols = 7;
      var myarray = new int[nrows, ncols];
      C2F_Interface.pass_2d_int_array_two_way(myarray, ref nrows, ref ncols);
      for (int i = 0; i < nrows; i++)
      {
        for (int j = 0; j < ncols; j++)
        {
          Console.WriteLine(i + " " + j + ": " + myarray[i, j]);
        }
      }
    }

    [Fact]
    public void C2F_Test_h5_init_and_terminate()
    {
      bool is_ok;
      is_ok = C2F_Interface.h5_init();
      Assert.True(is_ok);
      is_ok = C2F_Interface.h5_terminate();
      Assert.True(is_ok);
    }
  }
}
