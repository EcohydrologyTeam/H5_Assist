using Xunit;
using C2F_Tests;

namespace F2F_Tests
{
  [Trait("xunit: All", "")]
  [Trait("xunit: F2F All", "")]
  [Trait("xunit: F2F Datasets", "")]
  public class F2F_Tests_Datasets_xunit
  {
    int file_id = 0;

    public F2F_Tests_Datasets_xunit()
    {
      C2F_Interface.h5_init();
    }

    private void Dispose()
    {
      C2F_Interface.h5_terminate();
      C2F_Utilities.H5CloseFile(file_id);
    }

    [Fact]
    public void F2F_Test_h5_dataset_exists_in_group_id()
    {
      bool is_ok = F2F_Interface.test_h5_dataset_exists_in_group_id(ref file_id);
      Assert.True(is_ok);
    }

    [Fact]
    public void F2F_Test_h5_dataset_exists_in_groupname()
    {
      bool is_ok = F2F_Interface.test_h5_dataset_exists_in_groupname(ref file_id);
      Assert.True(is_ok);
    }

    [Fact]
    public void F2F_Test_h5_open_and_close_dataset()
    {
      bool is_ok = F2F_Interface.test_h5_open_and_close_dataset(ref file_id);
      Assert.True(is_ok);
    }

    [Fact]
    public void F2F_Test_h5_get_dataset_dimensions()
    {
      bool is_ok = F2F_Interface.test_h5_get_dataset_dimensions(ref file_id);
      Assert.True(is_ok);
    }


    // ----- Read Tests -----

    [Fact]
    [Trait("xunit: F2F Read", "")]
    [Trait("xunit: F2F Array", "")]
    public void F2F_Test_h5_read_dataset_2d_array_r_by_row()
    {
      bool is_ok = F2F_Interface.test_h5_read_dataset_2d_array_r_by_row(ref file_id);

      Assert.True(is_ok);
    }

    [Fact]
    [Trait("xunit: F2F Read", "")]
    [Trait("xunit: F2F Array", "")]
    public void F2F_Test_h5_read_dataset_2d_array_i_by_row()
    {
      bool is_ok = F2F_Interface.test_h5_read_dataset_2d_array_i_by_row(ref file_id);
      Assert.True(is_ok);
    }

    [Fact]
    [Trait("xunit: F2F Read", "")]
    [Trait("xunit: F2F Array", "")]
    public void F2F_Test_h5_read_dataset_2d_array_r()
    {
      bool is_ok = F2F_Interface.test_h5_read_dataset_2d_array_r(ref file_id);
      Assert.True(is_ok);
    }

    [Fact]
    [Trait("xunit: F2F Read", "")]
    [Trait("xunit: F2F Array", "")]
    public void F2F_Test_h5_read_dataset_2d_array_i()
    {
      bool is_ok = F2F_Interface.test_h5_read_dataset_2d_array_i(ref file_id);
      Assert.True(is_ok);
    }

    [Fact]
    [Trait("xunit: F2F Read", "")]
    [Trait("xunit: F2F Array", "")]
    public void F2F_Test_h5_read_dataset_1d_array_c()
    {
      bool is_ok = F2F_Interface.test_h5_read_dataset_1d_array_c(ref file_id);
      Assert.True(is_ok);
    }


    // ----- Creation Tests -----

    [Fact]
    public void F2F_Test_h5_create_compressed_2d_datasets_chunked_in_time()
    {
      bool is_ok = F2F_Interface.test_h5_create_compressed_2d_datasets_chunked_in_time(ref file_id);
      Assert.True(is_ok);
    }

    [Fact]
    public void F2F_Test_h5_create_compressed_2d_datasets_chunked_in_space()
    {
      bool is_ok = F2F_Interface.test_h5_create_compressed_2d_datasets_chunked_in_space(ref file_id);
      Assert.True(is_ok);
    }


    // ----- Write Tests -----

    [Fact]
    [Trait("xunit: F2F Write", "")]
    [Trait("xunit: F2F Array", "")]
    public void F2F_Test_h5_write_dataset_2d_array_r()
    {
      bool is_ok = F2F_Interface.test_h5_write_dataset_2d_array_r(ref file_id);
      Assert.True(is_ok);
    }

    [Fact]
    [Trait("xunit: F2F Write", "")]
    [Trait("xunit: F2F Array", "")]
    public void F2F_Test_h5_write_dataset_2d_array_r_by_row()
    {
      bool is_ok = F2F_Interface.test_h5_write_dataset_2d_array_r_by_row(ref file_id);
      Assert.True(is_ok);
    }

    [Fact]
    [Trait("xunit: F2F Write", "")]
    [Trait("xunit: F2F Array", "")]
    public void F2F_Test_h5_write_dataset_2d_array_i()
    {
      bool is_ok = F2F_Interface.test_h5_write_dataset_2d_array_i(ref file_id);
      Assert.True(is_ok);
    }

    [Fact]
    [Trait("xunit: F2F Write", "")]
    [Trait("xunit: F2F Array", "")]
    public void F2F_Test_h5_write_dataset_2d_array_i_by_row()
    {
      bool is_ok = F2F_Interface.test_h5_write_dataset_2d_array_i_by_row(ref file_id);
      Assert.True(is_ok);
    }

    [Fact]
    [Trait("xunit: F2F Write", "")]
    [Trait("xunit: F2F Array", "")]
    public void F2F_Test_h5_write_dataset_1d_array_c()
    {
      bool is_ok = F2F_Interface.test_h5_write_dataset_1d_array_c(ref file_id);
      Assert.True(is_ok);
    }

    [Fact]
    [Trait("xunit: F2F Compound", "")]
    public void _F2F_Test_compound_example_f95()
    {
      bool is_ok = F2F_Interface.test_compound_example_f95();
      Assert.True(is_ok);
    }

    [Fact]
    [Trait("xunit: F2F Compound", "")]
    public void _F2F_Test_compound_example_f2003()
    {
      bool is_ok = F2F_Interface.test_compound_example_f2003();
      Assert.True(is_ok);
    }


    [Fact]
    [Trait("xunit: F2F Compound", "")]
    public void _F2F_Test_h5_read_compound_integer()
    {
      int file_id = 0;
      bool is_ok = F2F_Interface.test_h5_read_compound_integer(ref file_id);
      Assert.True(is_ok);
    }

    [Fact]
    [Trait("xunit: F2F Compound", "")]
    public void _F2F_Test_h5_read_compound_real()
    {
      int file_id = 0;
      bool is_ok = F2F_Interface.test_h5_read_compound_real(ref file_id);
      Assert.True(is_ok);
    }

    [Fact]
    [Trait("xunit: F2F Compound", "")]
    public void _F2F_Test_h5_read_compound_double()
    {
      int file_id = 0;
      bool is_ok = F2F_Interface.test_h5_read_compound_double(ref file_id);
      Assert.True(is_ok);
    }

    [Fact]
    [Trait("xunit: F2F Compound", "")]
    public void _F2F_Test_h5_read_compound_string()
    {
      int file_id = 0;
      bool is_ok = F2F_Interface.test_h5_read_compound_string(ref file_id);
      Assert.True(is_ok);
    }

    [Fact]
    [Trait("xunit: F2F Compound", "")]
    public void _F2F_Test_h5_read_compound_field()
    {
      int file_id = 0;
      bool is_ok = F2F_Interface.test_h5_read_compound_field(ref file_id);
      Assert.True(is_ok);
    }

    [Fact]
    [Trait("xunit: F2F Compound", "")]
    public void _F2F_Test_h5_read_compound_dataset()
    {
      int file_id = 0;
      bool is_ok = F2F_Interface.test_h5_read_compound_dataset(ref file_id);
      Assert.True(is_ok);
    }

    [Fact]
    [Trait("xunit: F2F Compound", "")]
    public void _F2F_Test_h5_write_compound_dataset()
    {
      int file_id = 0;
      bool is_ok = F2F_Interface.test_h5_write_compound_dataset(ref file_id);
      Assert.True(is_ok);
    }

    [Fact]
    [Trait("xunit: F2F Compound", "")]
    public void _F2F_Test_h5_write_compound_dataset_with_more_columns()
    {
      int file_id = 0;
      bool is_ok = F2F_Interface.test_h5_write_compound_dataset_with_more_columns(ref file_id);
      Assert.True(is_ok);
    }

    [Fact]
    [Trait("aaa: Add row", "")]
    public void _F2F_Test_h5_add_row_2d_dataset_r()
    {
      int file_id = 0;
      bool is_ok = F2F_Interface.test_h5_add_row_2d_dataset_r(ref file_id);
      Assert.True(is_ok);
    }

  }
}
