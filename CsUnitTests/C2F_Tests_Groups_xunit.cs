using Xunit;
using System;

namespace C2F_Tests
{
  [Trait("xunit: All", "")]
  [Trait("xunit: C2F All", "")]
  [Trait("xunit: C2F Groups", "")]
  public class C2F_Tests_Groups_xunit
  {
    int file_id = 0;
    int group_id = 0;
    bool is_ok = false;
    string infile = C2F_Interface.HDF_INFILE;

    public C2F_Tests_Groups_xunit()
    {
      C2F_Interface.h5_init();
    } 

    private void Dispose()
    {
      C2F_Interface.h5_terminate();
      is_ok = C2F_Interface.h5_close_group(ref group_id);
      is_ok = C2F_Interface.h5_close_file(ref file_id);
    }

    [Fact]
    public void C2F_Test_h5_group_exists()
    {
      int file_access_flag = C2F_Interface.ReadOnly;
      bool link_exists = false;
      string group_name = "Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series";

      is_ok = C2F_Interface.h5_open_file(infile, ref file_access_flag, ref file_id, infile.Length);
      Assert.True(is_ok);

      is_ok = C2F_Interface.h5_group_exists(ref file_id, group_name, ref link_exists, group_name.Length);
      Assert.True(is_ok);
    }

    [Fact]
    public void C2F_Test_h5_num_groups()
    {
      int file_access_flag = C2F_Interface.ReadOnly;
      int num_groups = -1;
      string infile = C2F_Interface.HDF_INFILE;
      string group_name = @"Results/Unsteady/Output/Output Blocks/Base Output";

      is_ok = C2F_Interface.h5_open_file(infile, ref file_access_flag, ref file_id, infile.Length);
      Assert.True(is_ok);

      is_ok = C2F_Interface.h5_num_groups(ref file_id, group_name, ref num_groups, group_name.Length);
      Assert.Equal(2, num_groups);
    }

    [Fact]
    public void C2F_Test_h5_open_and_close_group()
    {
      int file_access_flag = C2F_Interface.ReadOnly;
      bool link_exists = false;
      string infile = C2F_Interface.HDF_INFILE;
      string group_name = @"Results/Unsteady/Output/Output Blocks/Base Output";

      is_ok = C2F_Interface.h5_open_file(infile, ref file_access_flag, ref file_id, infile.Length);
      Assert.True(is_ok);

      is_ok = C2F_Interface.h5_group_exists(ref file_id, group_name, ref link_exists, group_name.Length);
      Assert.True(is_ok);

      is_ok = C2F_Interface.h5_open_group(ref file_id, group_name, ref group_id, group_name.Length);
      Assert.True(is_ok);

      is_ok = C2F_Interface.h5_close_group(ref group_id);
      Assert.True(is_ok);
    }

    [Fact]
    [Trait("xunit: C2F Write", "")]
    public void C2F_Test_h5_create_group()
    {
      string group_name = "C2F My New Group";
      int H5_access_flag = C2F_Interface.ReadWrite;
      bool link_exists = false;
      string infile = C2F_Interface.HDF_INFILE_COPY1; // using a copy of the original to avoid file writing conflicts

      is_ok = C2F_Interface.h5_open_file(infile, ref H5_access_flag, ref file_id, infile.Length);

      Console.WriteLine("Checking if group " + group_name + " exists...");
      is_ok = C2F_Interface.h5_group_exists(ref file_id, group_name, ref link_exists, group_name.Length);

      // If the group already exists, delete(unlink)
      if (link_exists) {
        Console.WriteLine("Group " + group_name + " already exists!");
        is_ok = C2F_Interface.h5_delete_group(ref file_id, group_name, group_name.Length);
      }
      else {
        Console.WriteLine("Group " + group_name + " doesn't exist yet.");
      }

      Console.WriteLine("Creating group " + group_name);
      is_ok = C2F_Interface.h5_create_group(ref file_id, group_name, ref group_id, group_name.Length);

      is_ok = C2F_Interface.h5_close_group(ref group_id);
      Assert.True(is_ok);
    }
  }
}
