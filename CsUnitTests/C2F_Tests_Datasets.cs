using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace C2F_Tests
{
  [TestClass]
  public class C2F_Tests_Datasets
  {
    // ----- Dataset Tests -----

    int file_id = 0;
    int infile_id = 0;
    int outfile_id = 0;
    int group_id = 0;
    int ingroup_id = 0;
    int outgroup_id = 0;
    int dataset_id = 0;
    int indataset_id = 0;
    int outdataset_id = 0;
    int dataset1_id = 0;
    int dataset2_id = 0;
    int dataset3_id = 0;
    bool is_ok = false;
    string infile = "";

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
      infile_id = 0;
      outfile_id = 0;
      group_id = 0;
      ingroup_id = 0;
      outgroup_id = 0;
      dataset_id = 0;
      indataset_id = 0;
      outdataset_id = 0;
      dataset1_id = 0;
      dataset2_id = 0;
      dataset3_id = 0;
      is_ok = false;
      infile = C2F_Interface.HDF_INFILE;
    }

    [TestCleanup]
    public void CleanupTest()
    {
      C2F_Utilities.H5CloseDataset(dataset_id);
      C2F_Utilities.H5CloseDataset(indataset_id);
      C2F_Utilities.H5CloseDataset(outdataset_id);
      C2F_Utilities.H5CloseDataset(dataset1_id);
      C2F_Utilities.H5CloseDataset(dataset2_id);
      C2F_Utilities.H5CloseDataset(dataset3_id);

      C2F_Utilities.H5CloseGroup(group_id);
      C2F_Utilities.H5CloseGroup(ingroup_id);
      C2F_Utilities.H5CloseGroup(outgroup_id);

      C2F_Utilities.H5CloseFile(file_id);
      C2F_Utilities.H5CloseFile(infile_id);
      C2F_Utilities.H5CloseFile(outfile_id);
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Datasets")]
    public void C2F_Test_h5_dataset_exists_in_group()
    {
      int file_access_flag = C2F_Interface.ReadOnly;
      bool link_exists = false;
      string group_name = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections";
      string dataset_name = "Flow";

      is_ok = C2F_Interface.h5_open_file(infile, ref file_access_flag, ref file_id, infile.Length);
      Assert.IsTrue(is_ok);

      is_ok = C2F_Interface.h5_open_group(ref file_id, group_name, ref group_id, group_name.Length);
      Assert.IsTrue(is_ok);

      is_ok = C2F_Interface.h5_dataset_exists_in_group_id(ref group_id, dataset_name, ref link_exists, dataset_name.Length);
      Assert.IsTrue(is_ok);

      is_ok = C2F_Interface.h5_close_group(ref group_id);
      Assert.IsTrue(is_ok);

      // TestCleanup will close datasets, groups, and files
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Datasets")]
    public void C2F_Test_h5_dataset_exists_in_groupname()
    {
      int file_access_flag = C2F_Interface.ReadOnly;
      bool link_exists = false;
      string group_name = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections";
      string dataset_name = "Flow";

      is_ok = C2F_Interface.h5_open_file(infile, ref file_access_flag, ref file_id, infile.Length);
      Assert.IsTrue(is_ok);

      is_ok = C2F_Interface.h5_dataset_exists_in_groupname(ref file_id, group_name, dataset_name, ref link_exists, group_name.Length, dataset_name.Length);
      Assert.IsTrue(is_ok);

      // TestCleanup will close datasets, groups, and files
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Datasets")]
    public void C2F_Test_h5_open_and_close_dataset()
    {
      int file_access_flag = C2F_Interface.ReadOnly;
      bool link_exists = false;
      string group_name = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections";
      string dataset_name = "Flow";

      is_ok = C2F_Interface.h5_open_file(infile, ref file_access_flag, ref file_id, infile.Length);
      Assert.IsTrue(is_ok);

      is_ok = C2F_Interface.h5_group_exists(ref file_id, group_name, ref link_exists, group_name.Length);
      Assert.IsTrue(is_ok);

      is_ok = C2F_Interface.h5_open_group(ref file_id, group_name, ref group_id, group_name.Length);
      Assert.IsTrue(is_ok);

      is_ok = C2F_Interface.h5_dataset_exists_in_group_id(ref group_id, dataset_name, ref link_exists, dataset_name.Length);
      Assert.IsTrue(is_ok);

      is_ok = C2F_Interface.h5_open_dataset(ref group_id, dataset_name, ref dataset_id, dataset_name.Length);
      Assert.IsTrue(is_ok);

      // TestCleanup will close datasets, groups, and files
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Datasets")]
    //public unsafe void C2F_Test_h5_get_dataset_dimensions()
    public void C2F_Test_h5_get_dataset_dimensions()
    {
      int file_access_flag = C2F_Interface.ReadOnly;
      bool link_exists = false;
      int rank = 2;
      var data_dims = new long[rank];
      var max_dims = new long[rank];
      long nrows_hdf = 2953;
      long ncols_hdf = 334;
      string group_name = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections";
      string dataset_name = "Flow";

      data_dims[0] = -1;
      data_dims[1] = -1;
      max_dims[0] = -1;
      max_dims[1] = -1;

      is_ok = C2F_Interface.h5_open_file(infile, ref file_access_flag, ref file_id, infile.Length);
      Assert.IsTrue(is_ok);

      is_ok = C2F_Interface.h5_group_exists(ref file_id, group_name, ref link_exists, group_name.Length);
      Assert.IsTrue(is_ok);

      is_ok = C2F_Interface.h5_open_group(ref file_id, group_name, ref group_id, group_name.Length);
      Assert.IsTrue(is_ok);

      is_ok = C2F_Interface.h5_dataset_exists_in_group_id(ref group_id, dataset_name, ref link_exists, dataset_name.Length);
      Assert.IsTrue(is_ok);

      is_ok = C2F_Interface.h5_open_dataset(ref group_id, dataset_name, ref dataset_id, dataset_name.Length);
      Assert.IsTrue(is_ok);

      //fixed (int* data_dims_ptr = data_dims, max_dims_ptr = max_dims)
      //{
      //    is_ok = C2F_H5_Interface.h5_get_dataset_dimensions(ref dataset_id, data_dims_ptr, max_dims_ptr, ref rank);
      //}

      is_ok = C2F_Interface.h5_get_dataset_dimensions(ref dataset_id, data_dims, max_dims, ref rank);
      Assert.IsTrue(is_ok);

      Assert.AreEqual(ncols_hdf, data_dims[0]);
      Assert.AreEqual(nrows_hdf, data_dims[1]);

      // TestCleanup will close datasets, groups, and files
    }


    // ----- Read Tests -----

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Read"),
      TestCategory("C2F Arrays"),
      TestCategory("C2F Datasets")]
    public void C2F_Test_h5_read_dataset_2d_array_r()
    {
      // Test reading entire 2D dataset
      string group_name = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections";
      string dataset_name = "Flow";
      long startrow = 0;
      long startcol = 0;
      long nrows_hdf = 2953;
      long ncols_hdf = 334;

      float[,] hdf_array = new float[nrows_hdf, ncols_hdf];

      C2F_Utilities.H5OpenFileAndDataset(infile, group_name, dataset_name, C2F_Interface.ReadOnly, ref file_id, ref group_id, ref dataset_id);

      is_ok = C2F_Interface.h5_read_dataset_2d_array_r(ref dataset_id, ref startrow, ref startcol, ref nrows_hdf, ref ncols_hdf, hdf_array);
      Console.WriteLine("Result = " + is_ok);
      Assert.IsTrue(is_ok);

      // TestCleanup will close datasets, groups, and files
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Read"),
      TestCategory("C2F Arrays"),
      TestCategory("C2F Datasets")]
    public void C2F_Test_h5_read_dataset_2d_array_r_BY_ROW()
    {
      // Test reading one row of a 2D dataset
      string group_name = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections";
      string dataset_name = "Flow";
      long startrow = 5;
      long startcol = 0;
      long nrows_vector = 1;
      long ncols_vector = 334;
      long index;

      float[,] hdf_array = new float[nrows_vector, ncols_vector];

      C2F_Utilities.H5OpenFileAndDataset(infile, group_name, dataset_name, C2F_Interface.ReadOnly, ref file_id, ref group_id, ref dataset_id);

      index = startrow;
      is_ok = C2F_Interface.h5_read_dataset_2d_array_r(ref dataset_id, ref startrow, ref startcol, ref nrows_vector, ref ncols_vector, hdf_array);
      Console.WriteLine("Result = " + is_ok);
      Assert.IsTrue(is_ok);

      // TestCleanup will close datasets, groups, and files
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Read"),
      TestCategory("C2F Arrays"),
      TestCategory("C2F Datasets")]
    public void C2F_Test_h5_read_dataset_2d_array_i()
    {
      string group_name = "/Geometry/Cross Sections";
      string dataset_name = "Polyline Info";
      long startrow = 0;
      long startcol = 0;
      long nrows_hdf = 334;
      long ncols_hdf = 4;
      int[,] hdf_array;
      int file_access_flag = C2F_Interface.ReadOnly;

      hdf_array = new int[nrows_hdf, ncols_hdf];

      C2F_Interface.h5_open_file(infile, ref file_access_flag, ref file_id, infile.Length);
      C2F_Interface.h5_open_group(ref file_id, group_name, ref group_id, group_name.Length);
      C2F_Interface.h5_open_dataset(ref group_id, dataset_name, ref dataset_id, dataset_name.Length);

      is_ok = C2F_Interface.h5_read_dataset_2d_array_i(ref dataset_id, ref startrow, ref startcol, ref nrows_hdf, ref ncols_hdf, hdf_array);
      Console.WriteLine("Result = " + is_ok);
      Assert.IsTrue(is_ok);

      // TestCleanup will close datasets, groups, and files
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Read"),
      TestCategory("C2F Arrays"),
      TestCategory("C2F Datasets")]
    public void C2F_Test_h5_read_dataset_2d_array_i_BY_ROW()
    {
      string group_name = "/Geometry/Cross Sections";
      string dataset_name = "Polyline Info";
      long index = 2; // Row number
      long startcol = 0;
      long ncols_hdf = 4;
      long nrows_vector = 1;
      long ncols_vector = ncols_hdf;
      int[,] hdf_array = new int[nrows_vector, ncols_vector];

      C2F_Utilities.H5OpenFileAndDataset(infile, group_name, dataset_name, C2F_Interface.ReadOnly, ref file_id, ref group_id, ref dataset_id);

      is_ok = C2F_Interface.h5_read_dataset_2d_array_i(ref dataset_id, ref index, ref startcol, ref nrows_vector, ref ncols_vector, hdf_array);
      Console.WriteLine("Result = " + is_ok);
      Assert.IsTrue(is_ok);

      // The test routine should have extracted row 2 from the data set
      // Checking row 2, column 1 of the dataset
      Assert.AreEqual(4, hdf_array[0, 1]);

      // TestCleanup will close datasets, groups, and files
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Read"),
      TestCategory("C2F Arrays"),
      TestCategory("C2F Datasets")]
    public void C2F_Test_h5_read_dataset_1d_array_r()
    {
      string group_name = "/Todd";
      string dataset_name = "Float_array_1d";
      long startrow = 0;
      long nvals = 10;
      float[] hdf_array = new float[nvals]; // Note this one is DOUBLE PRECISION

      is_ok = C2F_Utilities.H5OpenFileAndDataset(infile, group_name, dataset_name, C2F_Interface.ReadOnly, ref file_id, ref group_id, ref dataset_id);
      Assert.IsTrue(is_ok);

      is_ok = C2F_Interface.h5_read_dataset_1d_array_r(ref dataset_id, ref startrow, ref nvals, hdf_array);
      Console.WriteLine("Result = " + is_ok);
      Assert.IsTrue(is_ok);

      // TestCleanup will close datasets, groups, and files
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Read"),
      TestCategory("C2F Arrays"),
      TestCategory("C2F Datasets")]
    public void C2F_Test_h5_read_dataset_1d_array_r_by_val()
    {
      string group_name = "/Todd";
      string dataset_name = "Float_array_1d";
      long oneval = 1;
      long nvals = 10;
      long startrow;
      float[] hdf_array = new float[oneval]; // Note this one is DOUBLE PRECISION

      is_ok = C2F_Utilities.H5OpenFileAndDataset(infile, group_name, dataset_name, C2F_Interface.ReadOnly, ref file_id, ref group_id, ref dataset_id);
      Assert.IsTrue(is_ok);

      for (startrow = 0; startrow < nvals; startrow++) {
        is_ok = C2F_Interface.h5_read_dataset_1d_array_r(ref dataset_id, ref startrow, ref oneval, hdf_array);
        Console.WriteLine("Result = " + is_ok);
        Assert.IsTrue(is_ok);
      }

      // TestCleanup will close datasets, groups, and files
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Read"),
      TestCategory("C2F Arrays"),
      TestCategory("C2F Datasets")]
    public void C2F_Test_h5_read_dataset_1d_array_d()
    {
      string group_name = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series";
      string dataset_name = "Time";
      long startrow = 0;
      long nvals = 2953;
      double[] hdf_array = new double[nvals]; // Note this one is DOUBLE PRECISION

      is_ok = C2F_Utilities.H5OpenFileAndDataset(infile, group_name, dataset_name, C2F_Interface.ReadOnly, ref file_id, ref group_id, ref dataset_id);
      Assert.IsTrue(is_ok);

      is_ok = C2F_Interface.h5_read_dataset_1d_array_d(ref dataset_id, ref startrow, ref nvals, hdf_array);
      Console.WriteLine("Result = " + is_ok);
      Assert.IsTrue(is_ok);

      // TestCleanup will close datasets, groups, and files
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Read"),
      TestCategory("C2F Arrays"),
      TestCategory("C2F Datasets")]
    public void C2F_Test_h5_read_dataset_1d_array_d_by_val()
    {
      string group_name = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series";
      string dataset_name = "Time";
      long startrow = 0;
      long nvals = 2953;
      long oneval = 1;
      double[] hdf_array = new double[oneval]; // Note this one is DOUBLE PRECISION

      is_ok = C2F_Utilities.H5OpenFileAndDataset(infile, group_name, dataset_name, C2F_Interface.ReadOnly, ref file_id, ref group_id, ref dataset_id);
      Assert.IsTrue(is_ok);

      for (startrow = 0; startrow < nvals; startrow++) {
        is_ok = C2F_Interface.h5_read_dataset_1d_array_d(ref dataset_id, ref startrow, ref oneval, hdf_array);
        Console.WriteLine("Result = " + is_ok);
        Assert.IsTrue(is_ok);
      }

      // TestCleanup will close datasets, groups, and files
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Read"),
      TestCategory("C2F Arrays"),
      TestCategory("C2F Datasets")]
    public void C2F_Test_h5_read_dataset_1d_array_i()
    {
      string group_name = "/Todd";
      string dataset_name = "Integer_array_1d";
      long startrow = 0;
      long nvals = 10;
      int[] hdf_array = new int[nvals];

      is_ok = C2F_Utilities.H5OpenFileAndDataset(infile, group_name, dataset_name, C2F_Interface.ReadOnly, ref file_id, ref group_id, ref dataset_id);
      Assert.IsTrue(is_ok);

      is_ok = C2F_Interface.h5_read_dataset_1d_array_i(ref dataset_id, ref startrow, ref nvals, hdf_array);
      Console.WriteLine("Result = " + is_ok);
      Assert.IsTrue(is_ok);

      // TestCleanup will close datasets, groups, and files
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Read"),
      TestCategory("C2F Arrays"),
      TestCategory("C2F Datasets")]
    public void C2F_Test_h5_read_dataset_1d_array_i_by_val()
    {
      string group_name = "/Todd";
      string dataset_name = "Integer_array_1d";
      long nvals = 10;
      long startrow = 0;
      long oneval = 1;
      int[] hdf_array = new int[oneval];

      is_ok = C2F_Utilities.H5OpenFileAndDataset(infile, group_name, dataset_name, C2F_Interface.ReadOnly, ref file_id, ref group_id, ref dataset_id);
      Assert.IsTrue(is_ok);

      for (startrow = 0; startrow < nvals; startrow++) {
        is_ok = C2F_Interface.h5_read_dataset_1d_array_i(ref dataset_id, ref startrow, ref oneval, hdf_array);
        Console.WriteLine("Result = " + is_ok);
        Assert.IsTrue(is_ok);
      }

      // TestCleanup will close datasets, groups, and files
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Read"),
      TestCategory("C2F Arrays"),
      TestCategory("C2F Datasets")]
    public void C2F_Test_h5_read_dataset_1d_array_c()
    {
      // Pass Fortran array to a StringBuilder object. Parse the string to
      // a string[] array using ToStringArray() in the H5Utilities class.
      //
      // Note: Reading arrays of strings from Fortran was a nightmare.
      // I tried byte[][], char[][], StringBuilder[][], and many other
      // things. Using the C interoperability of Fortran2003, it appears to be
      // possible, but requires considerable complexity on the Fortran side.
      // Each Fortran routine would need special routines to handle C, Fortran, 
      // and .Net (not sure about Java). The other Fortran functions in the library 
      // would likely need to enforce C interoperability as well. This could then
      // limit use of the Fortran library by other Fortran routines. The StringBuilder
      // method used here is amazingly simple, and it works.
      // Todd Steissberg, July 1, 2017
      string group_name = "/Geometry/Cross Sections";
      string dataset_name = "Node Descriptions";
      long nvals = 334;
      int str_len = 512;
      long startrow = 0;
      // Make object large enough to hold a Fortran array of strings:
      // Character(len=str_len), dimension(nvals)
      // Character*str_len(nvals)
      StringBuilder SBvalues = new StringBuilder((int)nvals * str_len); // Note: casting to int limits the size of array supported
      int file_access_flag = C2F_Interface.ReadOnly;

      is_ok = C2F_Interface.h5_open_file(infile, ref file_access_flag, ref file_id, infile.Length);

      is_ok = C2F_Interface.h5_open_group(ref file_id, group_name, ref group_id, group_name.Length);

      is_ok = C2F_Interface.h5_open_dataset(ref group_id, dataset_name, ref dataset_id, dataset_name.Length);

      is_ok = C2F_Interface.h5_read_dataset_1d_array_c(ref dataset_id, ref startrow, ref nvals, ref str_len, SBvalues);

      Console.WriteLine("Condition = " + is_ok);
      Assert.IsTrue(is_ok);

      string[] values = C2F_Utilities.ToStringArray(SBvalues, nvals, str_len);

      for (int i = 0; i < nvals; i++) {
        Console.WriteLine("values[" + i + "] = " + values[i]);
      }

      // TestCleanup will close datasets, groups, and files
    }

    // ----- Creation Tests -----

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Datasets"),
      TestCategory("C2F Write"),
      TestCategory("C2F Compressed")]
    public void C2F_Test_h5_create_compressed_2d_datasets_chunked_in_time()
    {
      long ncols_hdf = 334;  // Number of HDF5 columns (dim1)
      long nrows_hdf = 2953; // Number of HDF5 rows (dim2)
      string filename = "C2F_h5_create_compressed_2d_datasets_chunked_in_time.h5";
      string group_name = "I feel compressed";
      string dataset_name1 = "Look Charley, we're FLOATing";
      string dataset_name2 = "One is the lonliest INTEGER";
      string dataset_name3 = "We've had a STRING of successes.";
      var data_dims = new int[2];
      bool chunk_in_time = true; // Chunk in time
      int kind_nbytes = 4;
      int H5_access_flag = C2F_Interface.Truncate;
      int data_type1;
      int data_type2;
      int data_type3 = -99; // defined below by h5_get_string_datatype()
      int str_len = 128;    // String length for string dataset
      bool compressed = true;

      is_ok = C2F_Interface.h5_init();

      is_ok = C2F_Interface.h5_create_file(filename, ref H5_access_flag, ref file_id, filename.Length);
      Console.WriteLine("Create file: " + is_ok);

      is_ok = C2F_Interface.h5_create_group(ref file_id, group_name, ref group_id, group_name.Length);
      Console.WriteLine("Create group: " + is_ok);

      // Create integer dataset
      data_type1 = C2F_Interface.H5T_NATIVE_INTEGER;
      is_ok = C2F_Interface.h5_create_2d_dataset(ref group_id, dataset_name1, ref nrows_hdf, ref ncols_hdf, ref data_type1, ref chunk_in_time, ref kind_nbytes, ref compressed, ref dataset1_id, dataset_name1.Length);
      Console.WriteLine("Create dataset: " + is_ok);

      // Create float/real dataset
      data_type2 = C2F_Interface.H5T_NATIVE_REAL;
      is_ok = C2F_Interface.h5_create_2d_dataset(ref group_id, dataset_name2, ref nrows_hdf, ref ncols_hdf, ref data_type2, ref chunk_in_time, ref kind_nbytes, ref compressed, ref dataset2_id, dataset_name2.Length);
      Console.WriteLine("Create dataset: " + is_ok);

      // *** Get string data type
      is_ok = C2F_Interface.h5_get_string_datatype(ref str_len, ref data_type3);
      Assert.IsTrue(is_ok);

      // Create string dataset
      is_ok = C2F_Interface.h5_create_2d_dataset(ref group_id, dataset_name3, ref nrows_hdf, ref ncols_hdf, ref data_type3, ref chunk_in_time, ref kind_nbytes, ref compressed, ref dataset3_id, dataset_name3.Length);
      Console.WriteLine("Create dataset: " + is_ok);

      // TestCleanup will close datasets, groups, and files
    }

    [TestMethod]
    [TestCategory("All"),
       TestCategory("C2F All"),
       TestCategory("C2F Datasets"),
       TestCategory("C2F Write"),
       TestCategory("C2F Compressed")]
    public void C2F_Test_h5_create_compressed_2d_datasets_chunked_in_space()
    {
      long ncols_hdf = 334;  // Number of HDF5 columns (dim1)
      long nrows_hdf = 2953; // Number of HDF5 rows (dim2)
      string filename = "C2F_h5_create_compressed_2d_datasets_chunked_in_space.h5";
      string group_name = "I feel compressed";
      string dataset_name1 = "Look Charley, we're FLOATing";
      string dataset_name2 = "One is the lonliest INTEGER";
      string dataset_name3 = "We've had a STRING of successes.";
      var data_dims = new int[2];
      bool chunk_in_time = false; // Chunk in space
      int kind_nbytes = 4;
      int H5_access_flag = C2F_Interface.Truncate;
      int data_type1;
      int data_type2;
      //int data_type3       = C2F_Interface.H5T_FORTRAN_S1;
      int data_type3 = -99; // defined below by h5_get_string_datatype(), using H5T_NATIVE_CHARACTER and str_len
      int str_len = 128;    // String length for string dataset
      bool compressed = true;

      is_ok = C2F_Interface.h5_init();

      is_ok = C2F_Interface.h5_create_file(filename, ref H5_access_flag, ref file_id, filename.Length);
      Console.WriteLine("Create file: " + is_ok);

      is_ok = C2F_Interface.h5_create_group(ref file_id, group_name, ref group_id, group_name.Length);
      Console.WriteLine("Create group: " + is_ok);

      // Create integer dataset
      data_type1 = C2F_Interface.H5T_NATIVE_INTEGER;
      is_ok = C2F_Interface.h5_create_2d_dataset(ref group_id, dataset_name1, ref nrows_hdf, ref ncols_hdf, ref data_type1, ref chunk_in_time, ref kind_nbytes, ref compressed, ref dataset1_id, dataset_name1.Length);
      Console.WriteLine("Create dataset: " + is_ok);

      // Create float/real dataset
      data_type2 = C2F_Interface.H5T_NATIVE_REAL;
      is_ok = C2F_Interface.h5_create_2d_dataset(ref group_id, dataset_name2, ref nrows_hdf, ref ncols_hdf, ref data_type2, ref chunk_in_time, ref kind_nbytes, ref compressed, ref dataset2_id, dataset_name2.Length);
      Console.WriteLine("Create dataset: " + is_ok);

      // *** Get string data type
      is_ok = C2F_Interface.h5_get_string_datatype(ref str_len, ref data_type3);
      Assert.IsTrue(is_ok);

      // Create string dataset
      is_ok = C2F_Interface.h5_create_2d_dataset(ref group_id, dataset_name3, ref nrows_hdf, ref ncols_hdf, ref data_type3, ref chunk_in_time, ref kind_nbytes, ref compressed, ref dataset3_id, dataset_name3.Length);
      Console.WriteLine("Create dataset: " + is_ok);

      // TestCleanup will close datasets, groups, and files
      is_ok = C2F_Interface.h5_close_dataset(ref dataset1_id);
      is_ok = C2F_Interface.h5_close_dataset(ref dataset2_id);
      is_ok = C2F_Interface.h5_close_dataset(ref dataset3_id);
      is_ok = C2F_Interface.h5_close_group(ref group_id);
      is_ok = C2F_Interface.h5_close_file(ref file_id);
    }


    // ----- Write Tests -----

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Write"),
      TestCategory("C2F Arrays"),
      TestCategory("C2F Datasets")]
    public void C2F_Test_h5_write_dataset_2d_array_r()
    {
      string outfile = "C2F_h5_write_dataset_2d_array_r.h5";
      string ingroup_name = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections";
      string outgroup_name = "/Results";
      string indataset_name = "Flow";
      string outdataset_name = "Flow";
      int data_type = C2F_Interface.H5T_NATIVE_REAL;
      long nrows_hdf = 2953; // # Fortran columns, # HDF rows
      long ncols_hdf = 334;  // # Fortran rows, # HDF columns
      float[,] hdf_array = new float[nrows_hdf, ncols_hdf];
      float[,] hdf_array_in = new float[nrows_hdf, ncols_hdf];
      float[,] hdf_array_out = new float[nrows_hdf, ncols_hdf];
      long index = 0; // Row number (zero-based indexing)
      bool is_ok = false;
      bool chunk_in_time = true; // Chunk in time
      int kind_nbytes = 4;
      int H5_access_flag = C2F_Interface.Truncate;
      long startcol = 0;
      bool compressed = true;

      infile_id = 0;
      outfile_id = 0;
      ingroup_id = 0;
      outgroup_id = 0;
      indataset_id = 0;
      outdataset_id = 0;

      // Open HDF input file and dataset
      C2F_Utilities.H5OpenFileAndDataset(infile, ingroup_name, indataset_name, C2F_Interface.ReadOnly, ref infile_id, ref ingroup_id, ref indataset_id);

      // Create file, group, and dataset
      is_ok = C2F_Interface.h5_create_file(outfile, ref H5_access_flag, ref outfile_id, outfile.Length);
      Assert.IsTrue(is_ok);
      is_ok = C2F_Interface.h5_create_group(ref outfile_id, outgroup_name, ref outgroup_id, outgroup_name.Length);
      Assert.IsTrue(is_ok);
      is_ok = C2F_Interface.h5_create_2d_dataset(ref outgroup_id, outdataset_name, ref nrows_hdf, ref ncols_hdf, ref data_type, ref chunk_in_time, ref kind_nbytes, ref compressed, ref outdataset_id, outdataset_name.Length);
      Assert.IsTrue(is_ok);

      // Read dataset from input file by row and write to output file by row
      is_ok = C2F_Interface.h5_read_dataset_2d_array_r(ref indataset_id, ref index, ref startcol, ref nrows_hdf, ref ncols_hdf, hdf_array);
      Assert.IsTrue(is_ok);
      is_ok = C2F_Interface.h5_write_dataset_2d_array_r(ref outdataset_id, ref index, ref startcol, ref nrows_hdf, ref ncols_hdf, hdf_array);
      Assert.IsTrue(is_ok);

      // Close both files
      C2F_Utilities.H5CloseDatasetAndFile(infile_id, ingroup_id, indataset_id);
      C2F_Utilities.H5CloseDatasetAndFile(outfile_id, outgroup_id, outdataset_id);

      // Reopen both files
      // Note: The output file was in write/truncate mode. Now it will be opened read-only mode.
      C2F_Utilities.H5OpenFileAndDataset(infile, ingroup_name, indataset_name, C2F_Interface.ReadOnly, ref infile_id, ref ingroup_id, ref indataset_id);
      C2F_Utilities.H5OpenFileAndDataset(outfile, outgroup_name, outdataset_name, C2F_Interface.ReadOnly, ref outfile_id, ref outgroup_id, ref outdataset_id);

      // TestCleanup will close datasets, groups, and files
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Write"),
      TestCategory("C2F Arrays"),
      TestCategory("C2F Datasets")]
    public void C2F_Test_h5_write_dataset_2d_array_r_BY_ROW()
    {
      string outfile = "C2F_h5_write_dataset_2d_array_r_BY_ROW.h5";
      string ingroup_name = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections";
      string outgroup_name = "/Results";
      string indataset_name = "Flow";
      string outdataset_name = "Flow";
      int data_type = C2F_Interface.H5T_NATIVE_REAL;
      long nrows_hdf = 2953; // # Fortran columns, # HDF rows
      long ncols_hdf = 334;  // # Fortran rows, # HDF columns
      long nrows_vector = 1;
      long ncols_vector = ncols_hdf;
      float[,] hdf_array = new float[nrows_vector, ncols_vector];
      float[,] hdf_array_in = new float[nrows_vector, ncols_vector];
      float[,] hdf_array_out = new float[nrows_vector, ncols_vector];
      long index = 0; // Row number (zero-based indexing)
      bool is_ok = false;
      bool chunk_in_time = true; // Chunk in time
      int kind_nbytes = 4;
      int H5_access_flag = C2F_Interface.Truncate;
      long startcol = 0;
      bool compressed = true;

      infile_id = 0;
      outfile_id = 0;
      ingroup_id = 0;
      outgroup_id = 0;
      indataset_id = 0;
      outdataset_id = 0;

      // Open HDF input file and dataset
      C2F_Utilities.H5OpenFileAndDataset(infile, ingroup_name, indataset_name, C2F_Interface.ReadOnly, ref infile_id, ref ingroup_id, ref indataset_id);

      // Create file, group, and dataset
      is_ok = C2F_Interface.h5_create_file(outfile, ref H5_access_flag, ref outfile_id, outfile.Length);
      Assert.IsTrue(is_ok);
      is_ok = C2F_Interface.h5_create_group(ref outfile_id, outgroup_name, ref outgroup_id, outgroup_name.Length);
      Assert.IsTrue(is_ok);
      is_ok = C2F_Interface.h5_create_2d_dataset(ref outgroup_id, outdataset_name, ref nrows_hdf, ref ncols_hdf, ref data_type, ref chunk_in_time, ref kind_nbytes, ref compressed, ref outdataset_id, outdataset_name.Length);
      Assert.IsTrue(is_ok);

      // Read dataset from input file by row and write to output file by row
      for (index = 0; index < nrows_hdf; index++) {
        is_ok = C2F_Interface.h5_read_dataset_2d_array_r(ref indataset_id, ref index, ref startcol, ref nrows_vector, ref ncols_vector, hdf_array);
        Assert.IsTrue(is_ok);
        is_ok = C2F_Interface.h5_write_dataset_2d_array_r(ref outdataset_id, ref index, ref startcol, ref nrows_vector, ref ncols_hdf, hdf_array);
        Assert.IsTrue(is_ok);
      }

      // Close both files
      C2F_Utilities.H5CloseDatasetAndFile(infile_id, ingroup_id, indataset_id);
      C2F_Utilities.H5CloseDatasetAndFile(outfile_id, outgroup_id, outdataset_id);

      // Reopen both files
      // Note: The output file was in write/truncate mode. Now it will be opened read-only mode.
      C2F_Utilities.H5OpenFileAndDataset(infile, ingroup_name, indataset_name, C2F_Interface.ReadOnly, ref infile_id, ref ingroup_id, ref indataset_id);
      C2F_Utilities.H5OpenFileAndDataset(outfile, outgroup_name, outdataset_name, C2F_Interface.ReadOnly, ref outfile_id, ref outgroup_id, ref outdataset_id);

      // Now verify the output data written to file - one row at a time
      for (index = 0; index < nrows_hdf; index++) {
        is_ok = C2F_Interface.h5_read_dataset_2d_array_r(ref indataset_id, ref index, ref startcol, ref nrows_vector, ref ncols_vector, hdf_array);
        is_ok = C2F_Interface.h5_read_dataset_2d_array_r(ref outdataset_id, ref index, ref startcol, ref nrows_vector, ref ncols_vector, hdf_array);
        for (int j = 0; j < ncols_hdf; j++) {
          Assert.AreEqual(hdf_array_in[0, j], hdf_array_out[0, j]);
        }
      }

      // TestCleanup will close datasets, groups, and files
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Write"),
      TestCategory("C2F Arrays"),
      TestCategory("C2F Datasets")]
    public void C2F_Test_h5_write_dataset_2d_array_i()
    {
      string outfile = "C2F_h5_write_dataset_2d_array_i.h5";
      string ingroup_name = "/Geometry/Cross Sections";
      string outgroup_name = "/Geometry";
      string indataset_name = "Polyline Info";
      string outdataset_name = "Polyline Info";
      int data_type = C2F_Interface.H5T_NATIVE_INTEGER;
      long nrows_hdf = 334; // # Fortran columns, # HDF rows
      long ncols_hdf = 4;  // # Fortran rows, # HDF columns
      int[,] hdf_array = new int[nrows_hdf, ncols_hdf];
      int[,] hdf_array_in = new int[nrows_hdf, ncols_hdf];
      int[,] hdf_array_out = new int[nrows_hdf, ncols_hdf];
      long index = 0; // Row number (zero-based indexing)
      bool is_ok = false;
      bool chunk_in_time = true; // Chunk in time
      int kind_nbytes = 4;
      int H5_access_flag = C2F_Interface.Truncate;
      long startcol = 0;
      bool compressed = true;

      infile_id = 0;
      outfile_id = 0;
      ingroup_id = 0;
      outgroup_id = 0;
      indataset_id = 0;
      outdataset_id = 0;

      // Open HDF input file and dataset
      C2F_Utilities.H5OpenFileAndDataset(infile, ingroup_name, indataset_name, C2F_Interface.ReadOnly, ref infile_id, ref ingroup_id, ref indataset_id);

      // Create file, group, and dataset
      is_ok = C2F_Interface.h5_create_file(outfile, ref H5_access_flag, ref outfile_id, outfile.Length);
      Assert.IsTrue(is_ok);
      is_ok = C2F_Interface.h5_create_group(ref outfile_id, outgroup_name, ref outgroup_id, outgroup_name.Length);
      Assert.IsTrue(is_ok);
      is_ok = C2F_Interface.h5_create_2d_dataset(ref outgroup_id, outdataset_name, ref nrows_hdf, ref ncols_hdf, ref data_type, ref chunk_in_time, ref kind_nbytes, ref compressed, ref outdataset_id, outdataset_name.Length);
      Assert.IsTrue(is_ok);

      // Read dataset from input file by row and write to output file by row
      is_ok = C2F_Interface.h5_read_dataset_2d_array_i(ref indataset_id, ref index, ref startcol, ref nrows_hdf, ref ncols_hdf, hdf_array);
      Assert.IsTrue(is_ok);
      is_ok = C2F_Interface.h5_write_dataset_2d_array_i(ref outdataset_id, ref index, ref startcol, ref nrows_hdf, ref ncols_hdf, hdf_array);
      Assert.IsTrue(is_ok);

      // Close both files
      C2F_Utilities.H5CloseDatasetAndFile(infile_id, ingroup_id, indataset_id);
      C2F_Utilities.H5CloseDatasetAndFile(outfile_id, outgroup_id, outdataset_id);

      // Reopen both files
      // Note: The output file was in write/truncate mode. Now it will be opened read-only mode.
      C2F_Utilities.H5OpenFileAndDataset(infile, ingroup_name, indataset_name, C2F_Interface.ReadOnly, ref infile_id, ref ingroup_id, ref indataset_id);
      C2F_Utilities.H5OpenFileAndDataset(outfile, outgroup_name, outdataset_name, C2F_Interface.ReadOnly, ref outfile_id, ref outgroup_id, ref outdataset_id);

      // TestCleanup will close datasets, groups, and files
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Write"),
      TestCategory("C2F Arrays"),
      TestCategory("C2F Datasets")]
    public void C2F_Test_h5_write_dataset_2d_array_i_BY_ROW()
    {
      string outfile = "C2F_h5_write_dataset_2d_array_i_BY_ROW.h5";
      string ingroup_name = "/Geometry/Cross Sections";
      string outgroup_name = "/Geometry";
      string indataset_name = "Polyline Info";
      string outdataset_name = "Polyline Info";
      int data_type = C2F_Interface.H5T_NATIVE_INTEGER;
      long nrows_hdf = 334; // # Fortran columns, # HDF rows
      long ncols_hdf = 4;  // # Fortran rows, # HDF columns
      long nrows_vector = 1;
      long ncols_vector = ncols_hdf;
      int[,] hdf_array = new int[nrows_vector, ncols_vector];
      int[,] hdf_array_in = new int[nrows_vector, ncols_vector];
      int[,] hdf_array_out = new int[nrows_vector, ncols_vector];
      long index = 0; // Row number (zero-based indexing)
      bool is_ok = false;
      bool chunk_in_time = true; // Chunk in time
      int kind_nbytes = 4;
      int H5_access_flag = C2F_Interface.Truncate;
      long startcol = 0;
      bool compressed = true;

      infile_id = 0;
      outfile_id = 0;
      ingroup_id = 0;
      outgroup_id = 0;
      indataset_id = 0;
      outdataset_id = 0;

      // Open HDF input file and dataset
      C2F_Utilities.H5OpenFileAndDataset(infile, ingroup_name, indataset_name, C2F_Interface.ReadOnly, ref infile_id, ref ingroup_id, ref indataset_id);

      // Create file, group, and dataset
      is_ok = C2F_Interface.h5_create_file(outfile, ref H5_access_flag, ref outfile_id, outfile.Length);
      Assert.IsTrue(is_ok);
      is_ok = C2F_Interface.h5_create_group(ref outfile_id, outgroup_name, ref outgroup_id, outgroup_name.Length);
      Assert.IsTrue(is_ok);
      is_ok = C2F_Interface.h5_create_2d_dataset(ref outgroup_id, outdataset_name, ref nrows_hdf, ref ncols_hdf, ref data_type, ref chunk_in_time, ref kind_nbytes, ref compressed, ref outdataset_id, outdataset_name.Length);
      Assert.IsTrue(is_ok);

      // Read dataset from input file by row and write to output file by row
      for (index = 0; index < nrows_hdf; index++) {
        is_ok = C2F_Interface.h5_read_dataset_2d_array_i(ref indataset_id, ref index, ref startcol, ref nrows_vector, ref ncols_vector, hdf_array);
        Assert.IsTrue(is_ok);
        is_ok = C2F_Interface.h5_write_dataset_2d_array_i(ref outdataset_id, ref index, ref startcol, ref nrows_vector, ref ncols_hdf, hdf_array);
        Assert.IsTrue(is_ok);
      }

      // Close both files
      C2F_Utilities.H5CloseDatasetAndFile(infile_id, ingroup_id, indataset_id);
      C2F_Utilities.H5CloseDatasetAndFile(outfile_id, outgroup_id, outdataset_id);

      // Reopen both files
      // Note: The output file was in write/truncate mode. Now it will be opened read-only mode.
      C2F_Utilities.H5OpenFileAndDataset(infile, ingroup_name, indataset_name, C2F_Interface.ReadOnly, ref infile_id, ref ingroup_id, ref indataset_id);
      C2F_Utilities.H5OpenFileAndDataset(outfile, outgroup_name, outdataset_name, C2F_Interface.ReadOnly, ref outfile_id, ref outgroup_id, ref outdataset_id);

      // Now verify the output data written to file - one row at a time
      for (index = 0; index < nrows_hdf; index++) {
        is_ok = C2F_Interface.h5_read_dataset_2d_array_i(ref indataset_id, ref index, ref startcol, ref nrows_vector, ref ncols_vector, hdf_array);
        is_ok = C2F_Interface.h5_read_dataset_2d_array_i(ref outdataset_id, ref index, ref startcol, ref nrows_vector, ref ncols_vector, hdf_array);
        for (int j = 0; j < ncols_hdf; j++) {
          Assert.AreEqual(hdf_array_in[0, j], hdf_array_out[0, j]);
        }
      }

      // TestCleanup will close datasets, groups, and files
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Write"),
      TestCategory("C2F Arrays"),
      TestCategory("C2F Datasets")]
    public void C2F_Test_h5_write_dataset_1d_array_c()
    {
      string outfile = "C2F_h5_write_dataset_1d_array_c.h5";
      string ingroup_name = "/Geometry/Cross Sections";
      string indataset_name = "Node Descriptions";
      string outgroup_name = "/Geometry";
      string outdataset_name = "Node Descriptions";
      //int data_type = C2F_Interface.H5T_FORTRAN_S1;
      int data_type = -99; // Defined with call to h5_get_string_datatype() below
      long nvals = 334;
      int str_len = 512;
      long startrow = 0;

      infile_id = 0;
      outfile_id = 0;
      ingroup_id = 0;
      outgroup_id = 0;
      indataset_id = 0;
      outdataset_id = 0;

      // Make object large enough to hold a Fortran array of strings:
      // Character(len=str_len), dimension(nvals)
      // Character*str_len(nvals)
      StringBuilder SBvalues = new StringBuilder((int)nvals * str_len); // Note: casting nvals to int limits the size of array supported
      int file_access_flag = C2F_Interface.ReadOnly;

      // Read data

      file_access_flag = C2F_Interface.ReadOnly;
      is_ok = C2F_Interface.h5_open_file(infile, ref file_access_flag, ref infile_id, infile.Length);
      Assert.IsTrue(is_ok);

      is_ok = C2F_Interface.h5_open_group(ref infile_id, ingroup_name, ref ingroup_id, ingroup_name.Length);
      Assert.IsTrue(is_ok);

      is_ok = C2F_Interface.h5_open_dataset(ref ingroup_id, indataset_name, ref indataset_id, indataset_name.Length);
      Assert.IsTrue(is_ok);

      is_ok = C2F_Interface.h5_read_dataset_1d_array_c(ref indataset_id, ref startrow, ref nvals, ref str_len, SBvalues);
      Assert.IsTrue(is_ok);

      string[] values = C2F_Utilities.ToStringArray(SBvalues, nvals, str_len);

      // Write data

      // Create output file
      file_access_flag = C2F_Interface.Truncate;
      is_ok = C2F_Interface.h5_create_file(outfile, ref file_access_flag, ref outfile_id, outfile.Length);
      Assert.IsTrue(is_ok);

      // Create output group
      is_ok = C2F_Interface.h5_create_group(ref outfile_id, outgroup_name, ref outgroup_id, outgroup_name.Length);
      Assert.IsTrue(is_ok);

      // Get string data type
      is_ok = C2F_Interface.h5_get_string_datatype(ref str_len, ref data_type);
      Assert.IsTrue(is_ok);

      // Create output dataset
      bool compressed = true;
      is_ok = C2F_Interface.h5_create_1d_dataset_c(ref outgroup_id, outdataset_name, ref nvals, ref str_len, ref compressed, ref outdataset_id, outdataset_name.Length);
      Assert.IsTrue(is_ok);

      // Write dataset
      is_ok = C2F_Interface.h5_write_dataset_1d_array_c(ref outdataset_id, ref startrow, ref nvals, ref str_len, SBvalues);
      Assert.IsTrue(is_ok);

      is_ok = C2F_Interface.h5_close_dataset(ref outdataset_id);
      is_ok = C2F_Interface.h5_close_group(ref outgroup_id);
      is_ok = C2F_Interface.h5_close_file(ref outfile_id);

      // TestCleanup will close datasets, groups, and files
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Write"),
      TestCategory("C2F Arrays"),
      TestCategory("C2F Datasets")]
    public void C2F_Test_h5_write_dataset_1d_array_oneval_c()
    {
      string outfile = "C2F_h5_write_dataset_1d_array_oneval_c.h5";
      string ingroup_name = "/Geometry/Cross Sections";
      string indataset_name = "Node Descriptions";
      string outgroup_name = "/Geometry";
      string outdataset_name = "Node Descriptions";
      //int data_type = C2F_Interface.H5T_FORTRAN_S1;
      int data_type = -99; // Defined with call to h5_get_string_datatype() below
      long nvals = 334;
      int str_len = 512;
      //int kind_nbytes = 4;
      long startrow = 0;
      bool compressed = true;

      infile_id = 0;
      outfile_id = 0;
      ingroup_id = 0;
      outgroup_id = 0;
      indataset_id = 0;
      outdataset_id = 0;

      // Make object large enough to hold a Fortran array of strings:
      // Character(len=str_len), dimension(nvals)
      // Character*str_len(nvals)
      StringBuilder SBvalues = new StringBuilder((int)nvals * str_len); // Note: casting nvals to int limits the size of string arrays supported
      int file_access_flag = C2F_Interface.ReadOnly;

      // Read data

      file_access_flag = C2F_Interface.ReadOnly;
      is_ok = C2F_Interface.h5_open_file(infile, ref file_access_flag, ref infile_id, infile.Length);
      Assert.IsTrue(is_ok);

      is_ok = C2F_Interface.h5_open_group(ref infile_id, ingroup_name, ref ingroup_id, ingroup_name.Length);
      Assert.IsTrue(is_ok);

      is_ok = C2F_Interface.h5_open_dataset(ref ingroup_id, indataset_name, ref indataset_id, indataset_name.Length);
      Assert.IsTrue(is_ok);

      is_ok = C2F_Interface.h5_read_dataset_1d_array_c(ref indataset_id, ref startrow, ref nvals, ref str_len, SBvalues);
      Assert.IsTrue(is_ok);

      string[] values = C2F_Utilities.ToStringArray(SBvalues, nvals, str_len);

      // Write data

      // Create output file
      file_access_flag = C2F_Interface.Truncate;
      is_ok = C2F_Interface.h5_create_file(outfile, ref file_access_flag, ref outfile_id, outfile.Length);
      Assert.IsTrue(is_ok);

      // Create output group
      is_ok = C2F_Interface.h5_create_group(ref outfile_id, outgroup_name, ref outgroup_id, outgroup_name.Length);
      Assert.IsTrue(is_ok);

      // Get string data type
      is_ok = C2F_Interface.h5_get_string_datatype(ref str_len, ref data_type);
      Assert.IsTrue(is_ok);

      // Create output dataset
      //is_ok = C2F_Interface.h5_create_compressed_1d_dataset(ref outgroup_id, outdataset_name, ref nvals, ref data_type, ref kind_nbytes, ref outdataset_id, outdataset_name.Length);
      is_ok = C2F_Interface.h5_create_1d_dataset_c(ref outgroup_id, outdataset_name, ref nvals, ref str_len, ref compressed, ref outdataset_id, outdataset_name.Length);
      Assert.IsTrue(is_ok);

      // Write dataset
      for (long index = 0; index < nvals; index++) {
        StringBuilder sb = new StringBuilder(values[index]);
        string[] one_value_array = C2F_Utilities.ToStringArray(sb, 1, str_len);
        string outstring = one_value_array[0];
        is_ok = C2F_Interface.h5_write_dataset_1d_array_oneval_c(ref outdataset_id, ref index, outstring, ref str_len);
      }
      Assert.IsTrue(is_ok);

      is_ok = C2F_Interface.h5_close_dataset(ref outdataset_id);
      is_ok = C2F_Interface.h5_close_group(ref outgroup_id);
      is_ok = C2F_Interface.h5_close_file(ref outfile_id);

      // TestCleanup will close datasets, groups, and files
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Write"),
      TestCategory("C2F Arrays"),
      TestCategory("C2F Datasets")]
    public void C2F_Test_h5_write_dataset_1d_array_r()
    {
      string outfile = "C2F_h5_write_dataset_1d_array_r.h5";
      string ingroup_name = "/Todd";
      string outgroup_name = "/Todd";
      string indataset_name = "Float_array_1d";
      string outdataset_name = "Float_array_1d";
      int data_type = C2F_Interface.H5T_NATIVE_REAL;
      bool is_ok = false;
      int kind_nbytes = 4;
      int H5_access_flag = C2F_Interface.Truncate;
      long startrow = 0;
      long nvals = 10;
      float[] hdf_array = new float[nvals]; // Note this one is DOUBLE PRECISION

      infile_id = 0;
      outfile_id = 0;
      ingroup_id = 0;
      outgroup_id = 0;
      indataset_id = 0;
      outdataset_id = 0;

      // Open HDF input file and dataset
      C2F_Utilities.H5OpenFileAndDataset(infile, ingroup_name, indataset_name, C2F_Interface.ReadOnly, ref infile_id, ref ingroup_id, ref indataset_id);

      // Create file, group, and dataset
      is_ok = C2F_Interface.h5_create_file(outfile, ref H5_access_flag, ref outfile_id, outfile.Length);
      Assert.IsTrue(is_ok);
      is_ok = C2F_Interface.h5_create_group(ref outfile_id, outgroup_name, ref outgroup_id, outgroup_name.Length);
      Assert.IsTrue(is_ok);
      is_ok = C2F_Interface.h5_create_compressed_1d_dataset(ref outgroup_id, outdataset_name, ref nvals, ref data_type, ref kind_nbytes, ref outdataset_id, outdataset_name.Length);
      Assert.IsTrue(is_ok);

      // Read dataset from input file by row and write to output file by row
      is_ok = C2F_Interface.h5_read_dataset_1d_array_r(ref indataset_id, ref startrow, ref nvals, hdf_array);
      Assert.IsTrue(is_ok);
      is_ok = C2F_Interface.h5_write_dataset_1d_array_r(ref outdataset_id, ref startrow, ref nvals, hdf_array);
      Assert.IsTrue(is_ok);

      // Close both files
      C2F_Utilities.H5CloseDatasetAndFile(infile_id, ingroup_id, indataset_id);
      C2F_Utilities.H5CloseDatasetAndFile(outfile_id, outgroup_id, outdataset_id);

      // Reopen both files
      // Note: The output file was in write/truncate mode. Now it will be opened read-only mode.
      C2F_Utilities.H5OpenFileAndDataset(infile, ingroup_name, indataset_name, C2F_Interface.ReadOnly, ref infile_id, ref ingroup_id, ref indataset_id);
      C2F_Utilities.H5OpenFileAndDataset(outfile, outgroup_name, outdataset_name, C2F_Interface.ReadOnly, ref outfile_id, ref outgroup_id, ref outdataset_id);

      // TestCleanup will close datasets, groups, and files
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Write"),
      TestCategory("C2F Arrays"),
      TestCategory("C2F Datasets")]
    public void C2F_Test_h5_write_dataset_1d_array_i()
    {
      string outfile = "C2F_h5_write_dataset_1d_array_i.h5";
      string ingroup_name = "/Todd";
      string outgroup_name = "/Todd";
      string indataset_name = "Integer_array_1d";
      string outdataset_name = "Integer_array_1d";
      int data_type = C2F_Interface.H5T_NATIVE_INTEGER;
      bool is_ok = false;
      int kind_nbytes = 4;
      int H5_access_flag = C2F_Interface.Truncate;
      long startrow = 0;
      long nvals = 10;
      int[] hdf_array = new int[nvals]; // Note this one is DOUBLE PRECISION

      infile_id = 0;
      outfile_id = 0;
      ingroup_id = 0;
      outgroup_id = 0;
      indataset_id = 0;
      outdataset_id = 0;

      // Open HDF input file and dataset
      C2F_Utilities.H5OpenFileAndDataset(infile, ingroup_name, indataset_name, C2F_Interface.ReadOnly, ref infile_id, ref ingroup_id, ref indataset_id);

      // Create file, group, and dataset
      is_ok = C2F_Interface.h5_create_file(outfile, ref H5_access_flag, ref outfile_id, outfile.Length);
      Assert.IsTrue(is_ok);
      is_ok = C2F_Interface.h5_create_group(ref outfile_id, outgroup_name, ref outgroup_id, outgroup_name.Length);
      Assert.IsTrue(is_ok);
      is_ok = C2F_Interface.h5_create_compressed_1d_dataset(ref outgroup_id, outdataset_name, ref nvals, ref data_type, ref kind_nbytes, ref outdataset_id, outdataset_name.Length);
      Assert.IsTrue(is_ok);

      // Read dataset from input file by row and write to output file by row
      is_ok = C2F_Interface.h5_read_dataset_1d_array_i(ref indataset_id, ref startrow, ref nvals, hdf_array);
      Assert.IsTrue(is_ok);
      is_ok = C2F_Interface.h5_write_dataset_1d_array_i(ref outdataset_id, ref startrow, ref nvals, hdf_array);
      Assert.IsTrue(is_ok);

      // Close both files
      C2F_Utilities.H5CloseDatasetAndFile(infile_id, ingroup_id, indataset_id);
      C2F_Utilities.H5CloseDatasetAndFile(outfile_id, outgroup_id, outdataset_id);

      // Reopen both files
      // Note: The output file was in write/truncate mode. Now it will be opened read-only mode.
      C2F_Utilities.H5OpenFileAndDataset(infile, ingroup_name, indataset_name, C2F_Interface.ReadOnly, ref infile_id, ref ingroup_id, ref indataset_id);
      C2F_Utilities.H5OpenFileAndDataset(outfile, outgroup_name, outdataset_name, C2F_Interface.ReadOnly, ref outfile_id, ref outgroup_id, ref outdataset_id);

      // TestCleanup will close datasets, groups, and files
    }

  }
}
