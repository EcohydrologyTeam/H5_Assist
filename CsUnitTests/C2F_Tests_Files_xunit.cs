using Xunit;

namespace C2F_Tests
{
  [Trait("xunit: All", "")]
  [Trait("xunit: C2F All", "")]
  [Trait("xunit: C2F Files", "")]
  public class C2F_Tests_Files_xunit
  {
    int file_id = 0;

    public C2F_Tests_Files_xunit()
    {
      C2F_Interface.h5_init();
    }

    private void Dispose()
    {
      C2F_Interface.h5_terminate();
      C2F_Utilities.H5CloseFile(file_id);
    }

    [Fact]
    public void C2F_Test_h5_open_close_file()
    {
      int file_id = 0;
      int file_access_flag = C2F_Interface.ReadOnly;
      bool is_ok;
      string infile = C2F_Interface.HDF_INFILE;

      is_ok = C2F_Interface.h5_open_file(infile, ref file_access_flag, ref file_id, infile.Length);
      Assert.True(is_ok);

      is_ok = C2F_Interface.h5_close_file(ref file_id);
      Assert.True(is_ok);
    }

    [Fact]
    public void C2F_Test_h5_create_file()
    {
      bool is_ok;
      int file_id = 0;
      string filename = "C2F_h5_create_file.h5";
      int H5_access_flag = C2F_Interface.Truncate;

      is_ok = C2F_Interface.h5_create_file(filename, ref H5_access_flag, ref file_id, filename.Length);
      Assert.True(is_ok);
    }
  }
}
