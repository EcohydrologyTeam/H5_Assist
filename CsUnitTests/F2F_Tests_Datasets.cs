using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using C2F_Tests;

namespace F2F_Tests
{
  // ----- Dataset Tests -----

  [TestClass]
  public class F2F_Tests_Datasets
  {
    int file_id = 0;

    [ClassInitialize]
    public static void InitializeClass(TestContext testContext)
    {
      C2F_Interface.h5_init();
    }

    [ClassCleanup]
    public static void CleanupClass()
    {
      C2F_Interface.h5_terminate();
    }

    [TestInitialize]
    public void InitializeTest()
    {
      file_id = 0;
    }

    [TestCleanup]
    public void CleanupTest()
    {
      C2F_Utilities.H5CloseFile(file_id);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Datasets")]
    public void F2F_Test_h5_dataset_exists_in_group_id()
    {
      bool is_ok = F2F_Interface.test_h5_dataset_exists_in_group_id(ref file_id);
      Assert.IsTrue(is_ok);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Datasets")]
    public void F2F_Test_h5_dataset_exists_in_groupname()
    {
      bool is_ok = F2F_Interface.test_h5_dataset_exists_in_groupname(ref file_id);
      Assert.IsTrue(is_ok);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Datasets")]
    public void F2F_Test_h5_open_and_close_dataset()
    {
      bool is_ok = F2F_Interface.test_h5_open_and_close_dataset(ref file_id);
      Assert.IsTrue(is_ok);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Datasets")]
    public void F2F_Test_h5_get_dataset_dimensions()
    {
      bool is_ok = F2F_Interface.test_h5_get_dataset_dimensions(ref file_id);
      Assert.IsTrue(is_ok);
    }


    // ----- Read Tests -----

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Read"),
      TestCategory("F2F Arrays"),
      TestCategory("F2F Datasets")]
    public void F2F_Test_h5_read_dataset_2d_array_r_by_row()
    {
      bool is_ok = F2F_Interface.test_h5_read_dataset_2d_array_r_by_row(ref file_id);

      Assert.IsTrue(is_ok);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Read"),
      TestCategory("F2F Arrays"),
      TestCategory("F2F Datasets")]
    public void F2F_Test_h5_read_dataset_2d_array_i_by_row()
    {
      bool is_ok = F2F_Interface.test_h5_read_dataset_2d_array_i_by_row(ref file_id);
      Assert.IsTrue(is_ok);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Read"),
      TestCategory("F2F Arrays"),
      TestCategory("F2F Datasets")]
    public void F2F_Test_h5_read_dataset_2d_array_r()
    {
      bool is_ok = F2F_Interface.test_h5_read_dataset_2d_array_r(ref file_id);
      Assert.IsTrue(is_ok);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Read"),
      TestCategory("F2F Arrays"),
      TestCategory("F2F Datasets")]
    public void F2F_Test_h5_read_dataset_2d_array_i()
    {
      bool is_ok = F2F_Interface.test_h5_read_dataset_2d_array_i(ref file_id);
      Assert.IsTrue(is_ok);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Read"),
      TestCategory("F2F Arrays"),
      TestCategory("F2F Datasets")]
    public void F2F_Test_h5_read_dataset_1d_array_c()
    {
      bool is_ok = F2F_Interface.test_h5_read_dataset_1d_array_c(ref file_id);
      Assert.IsTrue(is_ok);
    }


    // ----- Creation Tests -----

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Write"),
      TestCategory("F2F Compressed")]
    public void F2F_Test_h5_create_compressed_2d_datasets_chunked_in_time()
    {
      bool is_ok = F2F_Interface.test_h5_create_compressed_2d_datasets_chunked_in_time(ref file_id);
      Assert.IsTrue(is_ok);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Write"),
      TestCategory("F2F Compressed")]
    public void F2F_Test_h5_create_compressed_2d_datasets_chunked_in_space()
    {
      bool is_ok = F2F_Interface.test_h5_create_compressed_2d_datasets_chunked_in_space(ref file_id);
      Assert.IsTrue(is_ok);
    }


    // ----- Write Tests -----

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Write"),
      TestCategory("F2F Arrays"),
      TestCategory("F2F Datasets")]
    public void F2F_Test_h5_write_dataset_2d_array_r()
    {
      bool is_ok = F2F_Interface.test_h5_write_dataset_2d_array_r(ref file_id);
      Assert.IsTrue(is_ok);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Write"),
      TestCategory("F2F Arrays"),
      TestCategory("F2F Datasets")]
    public void F2F_Test_h5_write_dataset_2d_array_r_by_row()
    {
      bool is_ok = F2F_Interface.test_h5_write_dataset_2d_array_r_by_row(ref file_id);
      Assert.IsTrue(is_ok);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Write"),
      TestCategory("F2F Arrays"),
      TestCategory("F2F Datasets")]
    public void F2F_Test_h5_write_dataset_2d_array_i()
    {
      bool is_ok = F2F_Interface.test_h5_write_dataset_2d_array_i(ref file_id);
      Assert.IsTrue(is_ok);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Write"),
      TestCategory("F2F Arrays"),
      TestCategory("F2F Datasets")]
    public void F2F_Test_h5_write_dataset_2d_array_i_by_row()
    {
      bool is_ok = F2F_Interface.test_h5_write_dataset_2d_array_i_by_row(ref file_id);
      Assert.IsTrue(is_ok);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Write"),
      TestCategory("F2F Arrays"),
      TestCategory("F2F Datasets")]
    public void F2F_Test_h5_write_dataset_1d_array_c()
    {
      bool is_ok = F2F_Interface.test_h5_write_dataset_1d_array_c(ref file_id);
      Assert.IsTrue(is_ok);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Read"),
      TestCategory("F2F Datasets")]
    public void _F2F_Test_compound_example_f95()
    {
      bool is_ok = F2F_Interface.test_compound_example_f95();
      Assert.IsTrue(is_ok);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Datasets")]
    public void _F2F_Test_compound_example_f2003()
    {
      bool is_ok = F2F_Interface.test_compound_example_f2003();
      Assert.IsTrue(is_ok);
    }


    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Read"),
      TestCategory("F2F Datasets")]
    public void _F2F_Test_h5_read_compound_integer()
    {
      int file_id = 0;
      bool is_ok = F2F_Interface.test_h5_read_compound_integer(ref file_id);
      Assert.IsTrue(is_ok);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Read"),
      TestCategory("F2F Datasets")]
    public void _F2F_Test_h5_read_compound_real()
    {
      int file_id = 0;
      bool is_ok = F2F_Interface.test_h5_read_compound_real(ref file_id);
      Assert.IsTrue(is_ok);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Read"),
      TestCategory("F2F Datasets")]
    public void _F2F_Test_h5_read_compound_double()
    {
      int file_id = 0;
      bool is_ok = F2F_Interface.test_h5_read_compound_double(ref file_id);
      Assert.IsTrue(is_ok);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Read"),
      TestCategory("F2F Datasets")]
    public void _F2F_Test_h5_read_compound_string()
    {
      int file_id = 0;
      bool is_ok = F2F_Interface.test_h5_read_compound_string(ref file_id);
      Assert.IsTrue(is_ok);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Read"),
      TestCategory("F2F Datasets")]
    public void _F2F_Test_h5_read_compound_field()
    {
      int file_id = 0;
      bool is_ok = F2F_Interface.test_h5_read_compound_field(ref file_id);
      Assert.IsTrue(is_ok);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F Read"),
      TestCategory("F2F All"),
      TestCategory("F2F Datasets")]
    public void _F2F_Test_h5_read_compound_dataset()
    {
      int file_id = 0;
      bool is_ok = F2F_Interface.test_h5_read_compound_dataset(ref file_id);
      Assert.IsTrue(is_ok);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Write"),
      TestCategory("F2F Datasets")]
    public void _F2F_Test_h5_write_compound_dataset()
    {
      int file_id = 0;
      bool is_ok = F2F_Interface.test_h5_write_compound_dataset(ref file_id);
      Assert.IsTrue(is_ok);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Write"),
      TestCategory("F2F Datasets")]
    public void _F2F_Test_h5_write_compound_dataset_with_more_columns()
    {
      int file_id = 0;
      bool is_ok = F2F_Interface.test_h5_write_compound_dataset_with_more_columns(ref file_id);
      Assert.IsTrue(is_ok);
    }

  }
}
