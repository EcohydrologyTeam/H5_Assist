using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C2F_Tests
{
  [TestClass]
  public class C2F_Tests_Files
  {
    //----- File Tests

    int file_id = 0;

    //----- Group Tests -----

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
      TestCategory("C2F All"),
      TestCategory("C2F Files")]
    public void C2F_Test_h5_open_close_file()
    {
      int file_id = 0;
      int file_access_flag = C2F_Interface.ReadOnly;
      bool is_ok;
      string infile = C2F_Interface.HDF_INFILE;

      is_ok = C2F_Interface.h5_open_file(infile, ref file_access_flag, ref file_id, infile.Length);
      Assert.IsTrue(is_ok);

      is_ok = C2F_Interface.h5_close_file(ref file_id);
      Assert.IsTrue(is_ok);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Files"),
      TestCategory("C2F Write")]
    public void C2F_Test_h5_create_file()
    {
      bool is_ok;
      int file_id = 0;
      string filename = "C2F_h5_create_file.h5";
      int H5_access_flag = C2F_Interface.Truncate;

      is_ok = C2F_Interface.h5_create_file(filename, ref H5_access_flag, ref file_id, filename.Length);
      Assert.IsTrue(is_ok);
    }

  }
}
