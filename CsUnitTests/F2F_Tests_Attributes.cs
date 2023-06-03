using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using C2F_Tests;

namespace F2F_Tests
{
  [TestClass]
  public class F2F_Tests_Attributes
  {
    // ----- Attribute Tests

    int file_id = 0;
    int group_id = 0;
    int dataset_id = 0;
    int attri_id = 0;

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
      group_id = 0;
      dataset_id = 0;
      attri_id = 0;
    }

    [TestCleanup]
    public void CleanupTest()
    {
      C2F_Utilities.H5CloseAttribute(attri_id);
      C2F_Utilities.H5CloseDataset(dataset_id);
      C2F_Utilities.H5CloseGroup(group_id);
      C2F_Utilities.H5CloseFile(file_id);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Read"),
      TestCategory("F2F Scalars"),
      TestCategory("F2F Attributes")]
    public void F2F_Test_h5_open_and_close_attribute()
    {
      bool is_ok = F2F_Interface.test_h5_open_and_close_dataset_attribute(ref file_id, ref group_id, ref dataset_id, ref attri_id);
      Assert.IsTrue(is_ok);
    }

    public void F2F_Test_h5_open_and_close_group_attribute()
    {
      bool is_ok = F2F_Interface.test_h5_open_and_close_group_attribute(ref file_id, ref group_id, ref attri_id);
      Assert.IsTrue(is_ok);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Read"),
      TestCategory("F2F Scalars"),
      TestCategory("F2F Attributes")]
    public void F2F_Test_h5_read_attri_scalar_r()
    {
      bool is_ok = F2F_Interface.test_h5_read_attri_scalar_r(ref file_id, ref group_id, ref dataset_id, ref attri_id);
      Assert.IsTrue(is_ok);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Read"),
      TestCategory("F2F Scalars"),
      TestCategory("F2F Attributes")]
    public void F2F_Test_h5_read_attri_scalar_i()
    {
      bool is_ok = F2F_Interface.test_h5_read_attri_scalar_i(ref file_id, ref group_id, ref dataset_id, ref attri_id);
      Assert.IsTrue(is_ok);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Read"),
      TestCategory("F2F Scalars"),
      TestCategory("F2F Attributes")]
    public void F2F_Test_h5_read_attri_scalar_c()
    {
      bool is_ok = F2F_Interface.test_h5_read_attri_scalar_c(ref file_id, ref group_id, ref dataset_id, ref attri_id);
      Assert.IsTrue(is_ok);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Read"),
      TestCategory("F2F Arrays"),
      TestCategory("F2F Attributes")]
    public void F2F_Test_h5_read_attri_1d_array_r()
    {
      bool is_ok = F2F_Interface.test_h5_read_attri_1d_array_r(ref file_id, ref group_id, ref dataset_id, ref attri_id);
      Assert.IsTrue(is_ok);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Read"),
      TestCategory("F2F Arrays"),
      TestCategory("F2F Attributes")]
    public void F2F_Test_h5_read_attri_1d_array_i()
    {
      bool is_ok = F2F_Interface.test_h5_read_attri_1d_array_i(ref file_id, ref group_id, ref dataset_id, ref attri_id);
      Assert.IsTrue(is_ok);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Read"),
      TestCategory("F2F Arrays"),
      TestCategory("F2F Attributes")]
    public void F2F_Test_h5_read_attri_1d_array_c()
    {
      bool is_ok = F2F_Interface.test_h5_read_attri_1d_array_c(ref file_id, ref group_id, ref dataset_id, ref attri_id);
      Assert.IsTrue(is_ok);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Write"),
      TestCategory("F2F Scalars"),
      TestCategory("F2F Attributes")]
    public void F2F_Test_h5_write_attri_scalar_r()
    {
      bool is_ok = F2F_Interface.test_h5_write_attri_scalar_r(ref file_id, ref group_id, ref dataset_id, ref attri_id);
      Assert.IsTrue(is_ok);
    }

  }
}
