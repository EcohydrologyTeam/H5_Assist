using Xunit;

namespace F2F_Tests
{
  [Trait("xunit: All", "")]
  [Trait("xunit: F2F All", "")]
  [Trait("xunit: F2F Files", "")]
  public class F2F_Tests_Files_xunit
  {
    // ----- File Tests

    [Fact]
    public void F2F_Test_h5_open_close_file()
    {
      bool is_ok = F2F_Interface.test_h5_open_close_file();
      Assert.True(is_ok);
    }

    [Fact]
    public void F2F_Test_h5_create_file()
    {

      bool is_ok = F2F_Interface.test_h5_create_file();
      Assert.True(is_ok);
    }
  }
}
