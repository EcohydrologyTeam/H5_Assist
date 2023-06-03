using System;
using System.Runtime.InteropServices;
using System.Text;

namespace F2F_Tests
{
  public static class F2F_Interface
  {
    /* NOTES:
        1. The Fortran code in h5_interface.f90 uses Standard Call (STDCALL) convention. This
           is also the default convention for PINVOKE, although this is explicitly set in 
           the DLLIMPORT declarations below. An alternative calling convention is Cdecl, which
           is used by C/C++, associated with the C interoperability of Fortran 2003 using
           bind(C) and the ISO_C_BINDING module. However, this would make passing arrays of
           strings unnecessarily difficult. As the C interoperability features improve, we
           may consider switching to this in the future.
        2. Specify strings and StringBuilder objects
        3. Specify the lengths of each at the end of the command as non-reference int type

      HDF5 Data Types:
      ==============================================================================================
      HDF Size    Size      C# Equivalent   Description
      ----------------------------------------------------------------------------------------------
      hid_t:      4 bytes   uint32 or int   Manage references to nodes(ID), unsigned integer
      hsize_t:    8 bytes   long            Native multi-precision integer
      size_t:     8 bytes   ulong           C native unsigned integer
      ----------------------------------------------------------------------------------------------
    */

    // Fortran native types in HDF5
    public const int H5T_NATIVE_INTEGER   = 50331741; // Native integer type
    public const int H5T_NATIVE_REAL      = 50331742; // Single precision real type
    public const int H5T_NATIVE_DOUBLE    = 50331743; // Double precision real type
    public const int H5T_NATIVE_CHARACTER = 50331744; // Character
    public const int H5T_FORTRAN_S1       = 50331786; // Fortran string type

    // HDF5 File Access Flags
    public const int ReadOnly = 0;              // HDF5 constant: H5F_ACC_RDONLY_F
                                                //  Existing file is opened with read-only access. 
                                                //  If file does not exist, H5Fopen fails.
    public const int ReadWrite = 1;             // HDF5 constant: H5F_ACC_RDWR_F
                                                //  Existing file is opened with read-write access. 
                                                //  If file does not exist, H5Fopen fails.
    public const int Truncate = 2;              // HDF5 constant: H5F_ACC_TRUN_F
                                                //  File is truncated upon opening, i.e., if file 
                                                //  already exists, file is opened with read-write 
                                                //  access and new data overwrites existing data, 
                                                //  destroying all prior content. If file does not exist, 
                                                //  it is created and opened with read-write access.
    public const int Create_and_ReadWrite = 4;  // HDF5 constant: H5F_ACC_EXCL_F
                                                //  If file already exists, H5Fcreate fails. If file 
                                                //  does not exist, it is created and opened with 
                                                //  read-write access.

    // Fortran-to-Fortran Unit Testing Library
    const string unit_test = "FortranUnitTests.dll"; // Copied to run folder by post-build event



    // ----- Basic Tests -----

    [DllImport(unit_test, EntryPoint = "test_add_two", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern int test_add_two(ref int arg);

    [DllImport(unit_test, EntryPoint = "test_add_one", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern int test_add_one(ref int arg);

    [DllImport(unit_test, EntryPoint = "test_pass_2d_float_array_two_way", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_pass_2d_float_array_two_way();

    [DllImport(unit_test, EntryPoint = "test_pass_2d_int_array_two_way", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_pass_2d_int_array_two_way();


    // ----- Interface Tests -----

    [DllImport(unit_test, EntryPoint = "test_h5_init_and_terminate", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_init_and_terminate();


    // ----- File Tests -----

    [DllImport(unit_test, EntryPoint = "test_h5_open_close_file", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_open_close_file();

    [DllImport(unit_test, EntryPoint = "test_h5_create_file", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_create_file();



    // ----- Group Tests -----

    [DllImport(unit_test, EntryPoint = "test_h5_group_exists", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_group_exists(ref int file_id);

    [DllImport(unit_test, EntryPoint = "test_h5_num_groups", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_num_groups(ref int num_groups, ref int file_id);

    [DllImport(unit_test, EntryPoint = "test_h5_open_and_close_group", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_open_and_close_group(ref int file_id, ref int group_id);

    [DllImport(unit_test, EntryPoint = "test_h5_create_group", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_create_group(ref int file_id, ref int group_id);


    // ----- Dataset Tests -----

    [DllImport(unit_test, EntryPoint = "test_h5_dataset_exists_in_group_id", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_dataset_exists_in_group_id(ref int file_id);

    [DllImport(unit_test, EntryPoint = "test_h5_dataset_exists_in_groupname", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_dataset_exists_in_groupname(ref int file_id);

    [DllImport(unit_test, EntryPoint = "test_h5_open_and_close_dataset", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_open_and_close_dataset(ref int file_id);

    [DllImport(unit_test, EntryPoint = "test_h5_get_dataset_dimensions", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_get_dataset_dimensions(ref int file_id);

    [DllImport(unit_test, EntryPoint = "test_h5_read_dataset_2d_array_r", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_read_dataset_2d_array_r(ref int file_id);

    [DllImport(unit_test, EntryPoint = "test_h5_read_dataset_2d_array_r_by_row", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_read_dataset_2d_array_r_by_row(ref int file_id);

    [DllImport(unit_test, EntryPoint = "test_h5_read_dataset_2d_array_i", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_read_dataset_2d_array_i(ref int file_id);

    [DllImport(unit_test, EntryPoint = "test_h5_read_dataset_2d_array_i_by_row", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_read_dataset_2d_array_i_by_row(ref int file_id);


    [DllImport(unit_test, EntryPoint = "test_h5_read_dataset_1d_array_c", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_read_dataset_1d_array_c(ref int file_id);


    [DllImport(unit_test, EntryPoint = "test_h5_create_compressed_2d_datasets_chunked_in_time", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_create_compressed_2d_datasets_chunked_in_time(ref int file_id);

    [DllImport(unit_test, EntryPoint = "test_h5_create_compressed_2d_datasets_chunked_in_space", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_create_compressed_2d_datasets_chunked_in_space(ref int file_id);

    [DllImport(unit_test, EntryPoint = "test_h5_write_dataset_2d_array_r", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_write_dataset_2d_array_r(ref int file_id);

    [DllImport(unit_test, EntryPoint = "test_h5_write_dataset_2d_array_i", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_write_dataset_2d_array_i(ref int file_id);

    [DllImport(unit_test, EntryPoint = "test_h5_write_dataset_2d_array_r_by_row", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_write_dataset_2d_array_r_by_row(ref int file_id);

    [DllImport(unit_test, EntryPoint = "test_h5_write_dataset_2d_array_i_by_row", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_write_dataset_2d_array_i_by_row(ref int file_id);

    [DllImport(unit_test, EntryPoint = "test_h5_write_dataset_1d_array_c", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_write_dataset_1d_array_c(ref int file_id);


    // ----- Attribute Tests -----

    [DllImport(unit_test, EntryPoint = "test_h5_open_and_close_dataset_attribute", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_open_and_close_dataset_attribute(ref int file_id, ref int group_id, ref int dataset_id, ref int attri_id);

    [DllImport(unit_test, EntryPoint = "test_h5_open_and_close_group_attribute", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_open_and_close_group_attribute(ref int file_id, ref int group_id, ref int attri_id);

    [DllImport(unit_test, EntryPoint = "test_h5_read_attri_scalar_r", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_read_attri_scalar_r(ref int file_id, ref int group_id, ref int dataset_id, ref int attri_id);

    [DllImport(unit_test, EntryPoint = "test_h5_read_attri_scalar_i", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_read_attri_scalar_i(ref int file_id, ref int group_id, ref int dataset_id, ref int attri_id);

    [DllImport(unit_test, EntryPoint = "test_h5_read_attri_scalar_c", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_read_attri_scalar_c(ref int file_id, ref int group_id, ref int dataset_id, ref int attri_id);

    [DllImport(unit_test, EntryPoint = "test_h5_read_attri_1d_array_r", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_read_attri_1d_array_r(ref int file_id, ref int group_id, ref int dataset_id, ref int attri_id);

    [DllImport(unit_test, EntryPoint = "test_h5_read_attri_1d_array_i", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_read_attri_1d_array_i(ref int file_id, ref int group_id, ref int dataset_id, ref int attri_id);

    [DllImport(unit_test, EntryPoint = "test_h5_read_attri_1d_array_c", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_read_attri_1d_array_c(ref int file_id, ref int group_id, ref int dataset_id, ref int attri_id);

    [DllImport(unit_test, EntryPoint = "test_h5_write_attri_scalar_r", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_write_attri_scalar_r(ref int file_id, ref int group_id, ref int dataset_id, ref int attri_id);

    [DllImport(unit_test, EntryPoint = "test_compound_example_f95", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_compound_example_f95();

    [DllImport(unit_test, EntryPoint = "test_compound_example_f2003", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_compound_example_f2003();

    [DllImport(unit_test, EntryPoint = "test_h5_read_compound_integer", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_read_compound_integer(ref int file_id);

    [DllImport(unit_test, EntryPoint = "test_h5_read_compound_real", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_read_compound_real(ref int file_id);

    [DllImport(unit_test, EntryPoint = "test_h5_read_compound_double", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_read_compound_double(ref int file_id);

    [DllImport(unit_test, EntryPoint = "test_h5_read_compound_string", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_read_compound_string(ref int file_id);

    [DllImport(unit_test, EntryPoint = "test_h5_read_compound_field", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_read_compound_field(ref int file_id);

    [DllImport(unit_test, EntryPoint = "test_h5_read_compound_dataset", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_read_compound_dataset(ref int file_id);

    [DllImport(unit_test, EntryPoint = "test_h5_write_compound_dataset", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_write_compound_dataset(ref int file_id);

    [DllImport(unit_test, EntryPoint = "test_h5_write_compound_dataset_with_more_columns", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_write_compound_dataset_with_more_columns(ref int file_id);

    [DllImport(unit_test, EntryPoint = "test_h5_add_row_2d_dataset_r", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool test_h5_add_row_2d_dataset_r(ref int file_id);
  }
}
