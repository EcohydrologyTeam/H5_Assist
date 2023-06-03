using Xunit;
using C2F_Tests;

namespace F2F_Tests
{
  [Trait("xunit: All", "")]
  [Trait("xunit: F2F All", "")]
  [Trait("xunit: F2F Attributes", "")]
  public class F2F_Tests_Attributes_xunit
  {
    // ----- Attribute Tests

    int file_id = 0;
    int group_id = 0;
    int dataset_id = 0;
    int attri_id = 0;

    public F2F_Tests_Attributes_xunit()
    {
      C2F_Interface.h5_init();
    }

    private void Dispose()
    {
      C2F_Interface.h5_terminate();
      C2F_Utilities.H5CloseAttribute(attri_id);
      C2F_Utilities.H5CloseDataset(dataset_id);
      C2F_Utilities.H5CloseGroup(group_id);
      C2F_Utilities.H5CloseFile(file_id);
    }

    [Fact]
    public void F2F_Test_h5_open_and_close_attribute()
    {
      bool is_ok = F2F_Interface.test_h5_open_and_close_dataset_attribute(ref file_id, ref group_id, ref dataset_id, ref attri_id);
      Assert.True(is_ok);
    }

    [Fact]
    public void F2F_Test_h5_open_and_close_group_attribute()
    {
      bool is_ok = F2F_Interface.test_h5_open_and_close_group_attribute(ref file_id, ref group_id, ref attri_id);
      Assert.True(is_ok);
    }

    [Fact]
    [Trait("xunit: F2F Read", "")]
    [Trait("xunit: F2F Scalar", "")]
    public void F2F_Test_h5_read_attri_scalar_r()
    {
      bool is_ok = F2F_Interface.test_h5_read_attri_scalar_r(ref file_id, ref group_id, ref dataset_id, ref attri_id);
      Assert.True(is_ok);
    }

    [Fact]
    [Trait("xunit: F2F Read", "")]
    [Trait("xunit: F2F Scalar", "")]
    public void F2F_Test_h5_read_attri_scalar_i()
    {
      bool is_ok = F2F_Interface.test_h5_read_attri_scalar_i(ref file_id, ref group_id, ref dataset_id, ref attri_id);
      Assert.True(is_ok);
    }

    [Fact]
    [Trait("xunit: F2F Read", "")]
    [Trait("xunit: F2F Scalar", "")]
    public void F2F_Test_h5_read_attri_scalar_c()
    {
      bool is_ok = F2F_Interface.test_h5_read_attri_scalar_c(ref file_id, ref group_id, ref dataset_id, ref attri_id);
      Assert.True(is_ok);
    }

    [Fact]
    [Trait("xunit: F2F Read", "")]
    [Trait("xunit: F2F Array", "")]
    public void F2F_Test_h5_read_attri_1d_array_r()
    {
      bool is_ok = F2F_Interface.test_h5_read_attri_1d_array_r(ref file_id, ref group_id, ref dataset_id, ref attri_id);
      Assert.True(is_ok);
    }

    [Fact]
    [Trait("xunit: F2F Read", "")]
    [Trait("xunit: F2F Array", "")]
    public void F2F_Test_h5_read_attri_1d_array_i()
    {
      bool is_ok = F2F_Interface.test_h5_read_attri_1d_array_i(ref file_id, ref group_id, ref dataset_id, ref attri_id);
      Assert.True(is_ok);
    }

    [Fact]
    [Trait("xunit: F2F Read", "")]
    [Trait("xunit: F2F Array", "")]
    public void F2F_Test_h5_read_attri_1d_array_c()
    {
      bool is_ok = F2F_Interface.test_h5_read_attri_1d_array_c(ref file_id, ref group_id, ref dataset_id, ref attri_id);
      Assert.True(is_ok);
    }

    [Fact]
    [Trait("xunit: F2F Write", "")]
    [Trait("xunit: F2F Scalar", "")]
    public void F2F_Test_h5_write_attri_scalar_r()
    {
      bool is_ok = F2F_Interface.test_h5_write_attri_scalar_r(ref file_id, ref group_id, ref dataset_id, ref attri_id);
      Assert.True(is_ok);
    }

  }
}
