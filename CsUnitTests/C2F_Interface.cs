using System;
using System.Runtime.InteropServices;
using System.Text;

namespace C2F_Tests
{
  public static class C2F_Interface
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

    // HDF5 Fortran Interface Library
    const string h5_interface = "h5_assist.dll"; // Copied to run folder by post-build event

    // Set input HDF5 file to use for all tests
    // These files are copied to the local folder by a pre-build event
    public const string HDF_INFILE = "LMNRRAS.p03.hdf";
    public const string HDF_INFILE_COPY1 = "LMNRRAS.p03.copy1.hdf";


    // ----- Basic Tests -----

    [DllImport(h5_interface, EntryPoint = "add_one", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern int add_one(ref int arg);

    [DllImport(h5_interface, EntryPoint = "return_string", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool return_string([Out] char[] mystring, ref int str_len);

    [DllImport(h5_interface, EntryPoint = "return_string_cdecl", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool return_string_cdecl([In, Out] char[,] mystring, ref int str_len);

    [DllImport(h5_interface, EntryPoint = "receive_string_array_cdecl", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool receive_string_array_cdecl([In, Out] char[,] char_array, ref int str_len, ref long nvals);

    [DllImport(h5_interface, EntryPoint = "send_string_array_cdecl", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool send_string_array_cdecl([In, Out] char[,] char_array, ref int str_len, ref long nvals);

    [DllImport(h5_interface, EntryPoint = "return_string_array", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool return_string_array(StringBuilder mystring, ref int str_len, ref long nvals);

    [DllImport(h5_interface, EntryPoint = "pass_2d_float_array_two_way", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool pass_2d_float_array_two_way([In, Out] float[,] myarray, ref long nrows_hdf, ref long ncols_hdf);

    [DllImport(h5_interface, EntryPoint = "pass_2d_int_array_two_way", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool pass_2d_int_array_two_way([In, Out] int[,] myarray, ref long nrows_hdf, ref long ncols_hdf);


    // ----- Utility/Helper Functions -----

    [DllImport(h5_interface, EntryPoint = "h5_get_string_datatype", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_get_string_datatype(ref int str_len, ref int data_type);

    [DllImport(h5_interface, EntryPoint = "h5_get_real_datatype", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_get_real_datatype(ref int data_type);

    [DllImport(h5_interface, EntryPoint = "h5_get_integer_datatype", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_get_integer_datatype(ref int data_type);


    // ----- HDF5 Interface -----

    [DllImport(h5_interface, EntryPoint = "h5_init", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_init();

    [DllImport(h5_interface, EntryPoint = "h5_terminate", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_terminate();


    // ----- HDF5 Files -----

    [DllImport(h5_interface, EntryPoint = "h5_open_file", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_open_file(string infile, ref int file_access_flag, ref int file_id, int len_infile);

    [DllImport(h5_interface, EntryPoint = "h5_close_file", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_close_file(ref int file_id);

    [DllImport(h5_interface, EntryPoint = "h5_create_file", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_create_file(string filename, ref int H5_access_flag, ref int file_id, int len_filename);


    // ----- HDF5 Groups -----

    [DllImport(h5_interface, EntryPoint = "h5_open_group", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_open_group(ref int loc_id, string group_name, ref int group_id, int len_group_name);

    [DllImport(h5_interface, EntryPoint = "h5_close_group", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_close_group(ref int group_id);

    [DllImport(h5_interface, EntryPoint = "h5_group_exists", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_group_exists(ref int loc_id, string group_name, ref bool link_exists, int len_group_name);

    [DllImport(h5_interface, EntryPoint = "h5_num_groups", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_num_groups(ref int file_id, string group_name, ref int num_groups, int len_group_name);

    [DllImport(h5_interface, EntryPoint = "h5_delete_group", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_delete_group(ref int file_id, string group_name, int len_group_name);

    [DllImport(h5_interface, EntryPoint = "h5_create_group", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_create_group(ref int file_id, string group_name, ref int group_id, int len_group_name);


    // ----- HDF5 Datasets -----

    [DllImport(h5_interface, EntryPoint = "h5_open_dataset", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_open_dataset(ref int loc_id, string dataset_name, ref int dataset_id, int len_dataset_name);

    [DllImport(h5_interface, EntryPoint = "h5_close_dataset", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_close_dataset(ref int dataset_id);

    [DllImport(h5_interface, EntryPoint = "h5_dataset_exists_in_groupname", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_dataset_exists_in_groupname(ref int file_id, string group_name, string dataset_name, ref bool link_exists, int len_group_name, int len_dataset_name);

    [DllImport(h5_interface, EntryPoint = "h5_dataset_exists_in_group_id", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_dataset_exists_in_group_id(ref int group_id, string dataset_name, ref bool link_exists, int len_dataset_name);

    [DllImport(h5_interface, EntryPoint = "h5_get_dataset_dimensions", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_get_dataset_dimensions(ref int dataset_id, long[] data_dims, long[] max_dims, ref int rank);

    //[DllImport(h5_interface, EntryPoint = "h5_get_dataset_dimensions", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    //public static unsafe extern bool h5_get_dataset_dimensions(ref int dataset_id, int* data_dims_ptr , int* max_dims_ptr, ref int rank);


    [DllImport(h5_interface, EntryPoint = "h5_read_dataset_2d_array_r", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_read_dataset_2d_array_r(ref int dataset_id, ref long startrow, ref long startcol, ref long nrows_hdf, ref long ncols_hdf, float[,] hdf_array);

    [DllImport(h5_interface, EntryPoint = "h5_read_dataset_2d_array_i", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_read_dataset_2d_array_i(ref int dataset_id, ref long startrow, ref long startcol, ref long nrows_hdf, ref long ncols_hdf, int[,] hdf_array);


    [DllImport(h5_interface, EntryPoint = "h5_read_dataset_1d_array_r", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_read_dataset_1d_array_r(ref int dataset_id, ref long startrow, ref long nvals, [In, Out] float[] hdf_array);

    [DllImport(h5_interface, EntryPoint = "h5_read_dataset_1d_array_d", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_read_dataset_1d_array_d(ref int dataset_id, ref long startrow, ref long nvals, [In, Out] double[] hdf_array);

    [DllImport(h5_interface, EntryPoint = "h5_read_dataset_1d_array_i", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_read_dataset_1d_array_i(ref int dataset_id, ref long startrow, ref long nvals, [In, Out] int[] hdf_array);

    [DllImport(h5_interface, EntryPoint = "h5_read_dataset_1d_array_c", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_read_dataset_1d_array_c(ref int dataset_id, ref long startrow, ref long nvals, ref int str_len, [In, Out] StringBuilder hdf_array);

    [DllImport(h5_interface, EntryPoint = "h5_create_2d_dataset", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_create_2d_dataset(ref int loc_id, string dataset_name, ref long nrows_hdf, ref long ncols_hdf, ref int data_type, ref bool chunk_in_time, ref int kind_nbytes, ref bool compressed, ref int dataset_id, int len_dataset_name);

    [DllImport(h5_interface, EntryPoint = "h5_create_compressed_1d_dataset", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_create_compressed_1d_dataset(ref int loc_id, string dataset_name, ref long nvals, ref int data_type, ref int kind_nbytes, ref int dataset_id, int len_dataset_name);

    [DllImport(h5_interface, EntryPoint = "h5_create_1d_dataset", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_create_1d_dataset(ref int loc_id, string dataset_name, ref long nvals, ref int data_type, ref int kind_nbytes, ref bool compressed, ref int dataset_id, int len_dataset_name);

    [DllImport(h5_interface, EntryPoint = "h5_create_1d_dataset_c", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_create_1d_dataset_c(ref int loc_id, string dataset_name, ref long nvals, ref int str_len, ref bool compressed, ref int dataset_id, int len_dataset_name);

    [DllImport(h5_interface, EntryPoint = "h5_write_dataset_2d_array_r", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_write_dataset_2d_array_r(ref int dataset_id, ref long startrow, ref long startcol, ref long nrows_hdf, ref long ncols_hdf, float[,] hdf_array);

    [DllImport(h5_interface, EntryPoint = "h5_write_dataset_2d_array_i", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_write_dataset_2d_array_i(ref int dataset_id, ref long startrow, ref long startcol, ref long nrows_hdf, ref long ncols_hdf, int[,] hdf_array);

    [DllImport(h5_interface, EntryPoint = "h5_write_dataset_1d_array_c", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_write_dataset_1d_array_c(ref int dataset_id, ref long startrow, ref long nvals, ref int str_len, [In, Out] StringBuilder hdf_array);

    [DllImport(h5_interface, EntryPoint = "h5_read_dataset_1d_array_oneval_r", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_read_dataset_1d_array_oneval_r(ref int dataset_id, [In, Out] float[] hdf_array, ref long index);

    [DllImport(h5_interface, EntryPoint = "h5_read_dataset_1d_array_oneval_d", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_read_dataset_1d_array_oneval_d(ref int dataset_id, [In, Out] float[] hdf_array, ref long index);

    [DllImport(h5_interface, EntryPoint = "h5_write_dataset_1d_array_oneval_c", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_write_dataset_1d_array_oneval_c(ref int dataset_id, ref long index, string instring, ref int str_len);

    [DllImport(h5_interface, EntryPoint = "h5_write_dataset_1d_array_r", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_write_dataset_1d_array_r(ref int dataset_id, ref long startrow, ref long nvals, [In, Out] float[] hdf_array);

    [DllImport(h5_interface, EntryPoint = "h5_write_dataset_1d_array_i", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_write_dataset_1d_array_i(ref int dataset_id, ref long startrow, ref long nvals, [In, Out] int[] hdf_array);


    // ----- HDF5 Attributes -----

    [DllImport(h5_interface, EntryPoint = "h5_open_attribute", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_open_attribute(ref int obj_id, string attri_name, ref int attri_id, int len_attri_name);

    [DllImport(h5_interface, EntryPoint = "h5_close_attribute", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_close_attribute(ref int attri_id);

    [DllImport(h5_interface, EntryPoint = "h5_create_attribute", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_create_attribute(ref int loc_id, string attri_name, ref int rank, ref int data_type, ref long nvals, ref int attri_id, int len_attri_name);

    [DllImport(h5_interface, EntryPoint = "h5_create_attribute_c", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_create_attribute_c(ref int loc_id, string attri_name, ref int rank, ref int str_len, ref long nvals, ref int attri_id, int len_attri_name);

    [DllImport(h5_interface, EntryPoint = "h5_get_attribute_datatype", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_get_attribute_datatype(ref int attri_id, ref int data_type);

    [DllImport(h5_interface, EntryPoint = "h5_read_attri_scalar_r", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_read_attri_scalar_r(ref int attri_id, ref float value);

    [DllImport(h5_interface, EntryPoint = "h5_read_attri_scalar_i", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_read_attri_scalar_i(ref int attri_id, ref int value);

    [DllImport(h5_interface, EntryPoint = "h5_read_attri_scalar_c", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_read_attri_scalar_c(ref int attri_id, [In, Out] StringBuilder SBvalue, int len_SBvalue);

    [DllImport(h5_interface, EntryPoint = "h5_read_attri_1d_array_r", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_read_attri_1d_array_r(ref int attri_id, [In, Out] float[] hdf_array, ref long nvals);

    [DllImport(h5_interface, EntryPoint = "h5_read_attri_1d_array_i", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_read_attri_1d_array_i(ref int attri_id, [In, Out] int[] hdf_array, ref long nvals);

    [DllImport(h5_interface, EntryPoint = "h5_read_attri_1d_array_c", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_read_attri_1d_array_c(ref int attri_id, [In, Out] StringBuilder hdf_array, ref int str_len, ref long nvals);

    [DllImport(h5_interface, EntryPoint = "h5_write_attri_scalar_r", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_write_attri_scalar_r(ref int attri_id, ref float value);

    [DllImport(h5_interface, EntryPoint = "h5_write_attri_scalar_i", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_write_attri_scalar_i(ref int attri_id, ref int value);

    [DllImport(h5_interface, EntryPoint = "h5_write_attri_scalar_c", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_write_attri_scalar_c(ref int attri_id, string value, ref int len_value);

    [DllImport(h5_interface, EntryPoint = "h5_write_attri_1d_array_r", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_write_attri_1d_array_r(ref int attri_id, [In, Out] float[] hdf_array, ref long nvals);

    [DllImport(h5_interface, EntryPoint = "h5_write_attri_1d_array_i", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_write_attri_1d_array_i(ref int attri_id, [In, Out] int[] hdf_array, ref long nvals);

    [DllImport(h5_interface, EntryPoint = "h5_write_attri_1d_array_c", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool h5_write_attri_1d_array_c(ref int attri_id, StringBuilder hdf_array, ref int str_len, ref long nvals);
  }

}
