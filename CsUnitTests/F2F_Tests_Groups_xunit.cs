using Xunit;
using C2F_Tests;

namespace F2F_Tests
{
  [Trait("xunit: All", "")]
  [Trait("xunit: F2F All", "")]
  [Trait("xunit: F2F Groups", "")]
  public class F2F_Tests_Groups_xunit
  {
    // ----- Group Tests -----

    int file_id = 0;
    int group_id = 0;

    public F2F_Tests_Groups_xunit()
    {
      C2F_Interface.h5_init();
    }

    private void Dispose()
    {
      C2F_Interface.h5_terminate();
      C2F_Utilities.H5CloseGroup(group_id);
      C2F_Utilities.H5CloseFile(file_id);
    }

    [Fact]
    public void F2F_Test_h5_group_exists()
    {
      bool is_ok = F2F_Interface.test_h5_group_exists(ref file_id);
      Assert.True(is_ok);
    }

    [Fact]
    public void F2F_Test_h5_num_groups()
    {
      int num_groups = 0;
      bool is_ok = F2F_Interface.test_h5_num_groups(ref num_groups, ref file_id);
      Assert.Equal(2, num_groups);
    }

    [Fact]
    public void F2F_Test_h5_open_and_close_group()
    {
      bool is_ok = F2F_Interface.test_h5_open_and_close_group(ref file_id, ref group_id);
      Assert.True(is_ok);
    }

    [Fact]
    public void F2F_Test_h5_create_group()
    {
      bool is_ok = F2F_Interface.test_h5_create_group(ref file_id, ref group_id);
      Assert.True(is_ok);
    }

  }
}
