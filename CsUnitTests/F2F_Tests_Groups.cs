using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using C2F_Tests;

namespace F2F_Tests
{
  [TestClass]
  public class F2F_Tests_Groups
  {
    // ----- Group Tests -----

    int file_id = 0;
    int group_id = 0;

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
    }

    [TestCleanup]
    public void CleanupTest()
    {
      C2F_Utilities.H5CloseGroup(group_id);
      C2F_Utilities.H5CloseFile(file_id);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Groups")]
    public void F2F_Test_h5_group_exists()
    {
      bool is_ok = F2F_Interface.test_h5_group_exists(ref file_id);
      Assert.IsTrue(is_ok);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Groups")]
    public void F2F_Test_h5_num_groups()
    {
      int num_groups = 0;
      bool is_ok = F2F_Interface.test_h5_num_groups(ref num_groups, ref file_id);
      Assert.AreEqual(2, num_groups);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Groups")]
    public void F2F_Test_h5_open_and_close_group()
    {
      bool is_ok = F2F_Interface.test_h5_open_and_close_group(ref file_id, ref group_id);
      Assert.IsTrue(is_ok);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("F2F All"),
      TestCategory("F2F Groups"),
      TestCategory("F2F Write")]
    public void F2F_Test_h5_create_group()
    {
      bool is_ok = F2F_Interface.test_h5_create_group(ref file_id, ref group_id);
      Assert.IsTrue(is_ok);
    }

  }
}
