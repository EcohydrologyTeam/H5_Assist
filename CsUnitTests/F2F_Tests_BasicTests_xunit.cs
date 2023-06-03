using Xunit;

namespace F2F_Tests
{
  [Trait("xunit: All", "")]
  [Trait("xunit: F2F All", "")]
  [Trait("xunit: F2F Basic Tests", "")]
  public class F2F_Tests_BasicTests_xunit
  {
    [Fact]
    public void F2F_Test_add_two()
    {
      int x = 5;
      int y = F2F_Interface.test_add_two(ref x);
      Assert.Equal(7, y);
    }

    [Fact]
    public void F2F_Test_add_one()
    {
      int x = 5;
      int y = F2F_Interface.test_add_one(ref x);
      Assert.Equal(6, y);
    }

    [Fact]
    public void F2F_Test_pass_2d_float_array_two_way()
    {
      bool is_ok = F2F_Interface.test_pass_2d_float_array_two_way();
      Assert.True(is_ok);
    }

    [Fact]
    public void F2F_Test_pass_2d_int_array_two_way()
    {
      bool is_ok = F2F_Interface.test_pass_2d_int_array_two_way();
      Assert.True(is_ok);
    }

    [Fact]
    public void F2F_Test_h5_init_and_terminate()
    {
      bool is_ok = F2F_Interface.test_h5_init_and_terminate();
      Assert.True(is_ok);
    }

  }
}
