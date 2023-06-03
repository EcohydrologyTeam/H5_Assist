using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C2F_Tests
{
  [TestClass]
  public class C2F_Tests_Attributes
  {
    int infile_id = 0;
    int outfile_id = 0;
    int ingroup_id = 0;
    int outgroup_id = 0;
    int indataset_id = 0;
    int outdataset_id = 0;
    int dataset1_id = 0;
    int dataset2_id = 0;
    int dataset3_id = 0;
    int attri_id = 0;
    int inattri_id = 0;
    int outattri_id = 0;
    bool is_ok = false;
    string infile = "";
    string outfile = "";

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
      infile_id = 0;
      outfile_id = 0;
      ingroup_id = 0;
      outgroup_id = 0;
      indataset_id = 0;
      outdataset_id = 0;
      dataset1_id = 0;
      dataset2_id = 0;
      dataset3_id = 0;
      attri_id = 0;
      inattri_id = 0;
      outattri_id = 0;
      is_ok = false;
      infile = C2F_Interface.HDF_INFILE;
      outfile = "";
    }

    [TestCleanup]
    public void CleanupTest()
    {
      C2F_Utilities.H5CloseAttribute(attri_id);
      C2F_Utilities.H5CloseAttribute(inattri_id);
      C2F_Utilities.H5CloseAttribute(outattri_id);
      C2F_Utilities.H5CloseDataset(indataset_id);
      C2F_Utilities.H5CloseDataset(outdataset_id);
      C2F_Utilities.H5CloseDataset(dataset1_id);
      C2F_Utilities.H5CloseDataset(dataset2_id);
      C2F_Utilities.H5CloseDataset(dataset3_id);
      C2F_Utilities.H5CloseGroup(ingroup_id);
      C2F_Utilities.H5CloseGroup(outgroup_id);
      C2F_Utilities.H5CloseFile(infile_id);
      C2F_Utilities.H5CloseFile(outfile_id);
    }

    // Open/Close Attributes

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Read"),
      TestCategory("C2F Scalars"),
      TestCategory("C2F Attributes")]
    public void C2F_Test_h5_open_and_close_dataset_attribute()
    {
      long nvals = 300000;
      var float_hdf_array = new float[nvals];
      string group_name = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections";
      string dataset_name = "Flow";
      string attri_name = "Maximum Value of Data Set";
      int file_access_flag = C2F_Interface.ReadOnly;

      is_ok = C2F_Interface.h5_open_file(infile, ref file_access_flag, ref infile_id, infile.Length);

      is_ok = C2F_Interface.h5_open_group(ref infile_id, group_name, ref ingroup_id, group_name.Length);

      is_ok = C2F_Interface.h5_open_dataset(ref ingroup_id, dataset_name, ref indataset_id, dataset_name.Length);

      is_ok = C2F_Interface.h5_open_attribute(ref indataset_id, attri_name, ref attri_id, attri_name.Length);

      is_ok = C2F_Interface.h5_close_attribute(ref attri_id);
      Assert.IsTrue(is_ok);

      // Files, groups, datasets, and attributes will be closed by TestCleanup()
    }

    public void C2F_Test_h5_open_and_close_group_attribute()
    {
      long nvals = 300000;
      var float_hdf_array = new float[nvals];
      string group_name = "/Plan Data/Plan Parameters";
      string attri_name = "HDF Compression";
      int file_access_flag = C2F_Interface.ReadOnly;

      C2F_Interface.h5_open_file(infile, ref file_access_flag, ref infile_id, infile.Length);

      C2F_Interface.h5_open_group(ref infile_id, group_name, ref ingroup_id, group_name.Length);

      is_ok = C2F_Interface.h5_open_attribute(ref ingroup_id, attri_name, ref attri_id, attri_name.Length);
      Assert.IsTrue(is_ok);

      is_ok = C2F_Interface.h5_close_attribute(ref attri_id);
      Assert.IsTrue(is_ok);

      // Files, groups, datasets, and attributes will be closed by TestCleanup()
    }

    // Read Attributes

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Read"),
      TestCategory("C2F Scalars"),
      TestCategory("C2F Attributes")]
    public void C2F_Test_h5_read_attri_scalar_r()
    {
      string group_name;
      string dataset_name;
      string attri_name;
      float attri_value = -9999;
      long nvals = 300000;
      var float_hdf_array = new float[nvals];
      int file_access_flag = C2F_Interface.ReadOnly;

      group_name = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections";
      dataset_name = "Flow";
      attri_name = "Maximum Value of Data Set";

      C2F_Interface.h5_open_file(infile, ref file_access_flag, ref infile_id, infile.Length);

      C2F_Interface.h5_open_group(ref infile_id, group_name, ref ingroup_id, group_name.Length);

      C2F_Interface.h5_open_dataset(ref ingroup_id, dataset_name, ref indataset_id, dataset_name.Length);

      C2F_Interface.h5_open_attribute(ref indataset_id, attri_name, ref attri_id, attri_name.Length);

      is_ok = C2F_Interface.h5_read_attri_scalar_r(ref attri_id, ref attri_value);
      Assert.IsTrue(is_ok);

      Console.WriteLine("Condition = " + is_ok);
      Console.WriteLine("Attribute Name = " + attri_name);
      Console.WriteLine("Attribute Value = " + attri_value);

      Assert.AreEqual(810.03796, Math.Round(attri_value, 5));

      // Files, groups, datasets, and attributes will be closed by TestCleanup()
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Read"),
      TestCategory("C2F Scalars"),
      TestCategory("C2F Attributes")]
    public void C2F_Test_h5_read_attri_scalar_i()
    {
      string group_name;
      string attri_name;
      int attri_value = -9999;
      long nvals = 300000;
      var float_hdf_array = new float[nvals];
      int file_access_flag = C2F_Interface.ReadOnly;

      group_name = "/Plan Data/Plan Parameters";
      attri_name = "HDF Compression";

      C2F_Interface.h5_open_file(infile, ref file_access_flag, ref infile_id, infile.Length);

      C2F_Interface.h5_open_group(ref infile_id, group_name, ref ingroup_id, group_name.Length);

      C2F_Interface.h5_open_attribute(ref ingroup_id, attri_name, ref attri_id, attri_name.Length);

      is_ok = C2F_Interface.h5_read_attri_scalar_i(ref attri_id, ref attri_value);
      Assert.IsTrue(is_ok);

      Console.WriteLine("Condition = " + is_ok);
      Console.WriteLine("Attribute Name = " + attri_name);
      Console.WriteLine("Attribute Value = " + attri_value);

      Assert.AreEqual(1, attri_value);

      // Files, groups, datasets, and attributes will be closed by TestCleanup()
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Read"),
      TestCategory("C2F Arrays"),
      TestCategory("C2F Attributes")]
    public void C2F_Test_h5_read_attri_1d_array_r()
    {
      long nvals = 10;
      string group_name = "/Todd";
      string dataset_name = "MyData";
      string attri_name = "FloatAttribute";
      var hdf_array = new float[nvals];
      int file_access_flag = C2F_Interface.ReadOnly;

      C2F_Interface.h5_open_file(infile, ref file_access_flag, ref infile_id, infile.Length);

      is_ok = C2F_Interface.h5_open_group(ref infile_id, group_name, ref ingroup_id, group_name.Length);

      is_ok = C2F_Interface.h5_open_dataset(ref ingroup_id, dataset_name, ref indataset_id, dataset_name.Length);

      is_ok = C2F_Interface.h5_open_attribute(ref indataset_id, attri_name, ref attri_id, attri_name.Length);

      is_ok = C2F_Interface.h5_read_attri_1d_array_r(ref attri_id, hdf_array, ref nvals);

      Console.WriteLine("Condition = " + is_ok);
      Assert.IsTrue(is_ok);
      Assert.AreEqual(Math.Round(3.3, 1), Math.Round(hdf_array[2], 1));

      // Files, groups, datasets, and attributes will be closed by TestCleanup()
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Read"),
      TestCategory("C2F Arrays"),
      TestCategory("C2F Attributes")]
    public void C2F_Test_h5_read_attri_1d_array_i()
    {
      long nvals = 10;
      string group_name = "/Todd";
      string dataset_name = "MyData";
      string attri_name = "IntAttribute";
      var hdf_array = new int[nvals];
      int file_access_flag = C2F_Interface.ReadOnly;

      is_ok = C2F_Interface.h5_open_file(infile, ref file_access_flag, ref infile_id, infile.Length);
      is_ok = C2F_Interface.h5_open_group(ref infile_id, group_name, ref ingroup_id, group_name.Length);
      is_ok = C2F_Interface.h5_open_dataset(ref ingroup_id, dataset_name, ref indataset_id, dataset_name.Length);
      is_ok = C2F_Interface.h5_open_attribute(ref indataset_id, attri_name, ref attri_id, attri_name.Length);

      is_ok = C2F_Interface.h5_read_attri_1d_array_i(ref attri_id, hdf_array, ref nvals);

      Console.WriteLine("Condition = " + is_ok);
      Assert.IsTrue(is_ok);
      Assert.AreEqual(3, hdf_array[2]);

      // Files, groups, datasets, and attributes will be closed by TestCleanup()
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Read"),
      TestCategory("C2F Scalars"),
      TestCategory("C2F Attributes")]
    public void C2F_Test_h5_read_attri_scalar_c()
    {
      int str_len = 16;
      var SB_attri_value = new StringBuilder(str_len);
      string attri_value;
      long nvals = 300000;
      var float_hdf_array = new float[nvals];
      string group_name = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections";
      string dataset_name = "Flow Lateral";
      string attri_name = "Lateral Flow";
      int file_access_flag = C2F_Interface.ReadOnly;

      C2F_Interface.h5_open_file(infile, ref file_access_flag, ref infile_id, infile.Length);
      C2F_Interface.h5_open_group(ref infile_id, group_name, ref ingroup_id, group_name.Length);
      C2F_Interface.h5_open_dataset(ref ingroup_id, dataset_name, ref indataset_id, dataset_name.Length);
      C2F_Interface.h5_open_attribute(ref indataset_id, attri_name, ref attri_id, attri_name.Length);

      is_ok = C2F_Interface.h5_read_attri_scalar_c(ref attri_id, SB_attri_value, SB_attri_value.Length);

      attri_value = SB_attri_value.ToString(0, str_len);

      Console.WriteLine("Condition = " + is_ok);
      Console.WriteLine("Attribute Name = " + attri_name);
      Console.WriteLine("Attribute Value = " + attri_value);

      Assert.IsTrue(is_ok);
      Assert.AreEqual("m3/s", attri_value.Trim());

      // Files, groups, datasets, and attributes will be closed by TestCleanup()
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Read"),
      TestCategory("C2F Arrays"),
      TestCategory("C2F Attributes")]
    public void C2F_Test_h5_read_attri_1d_array_c()
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
      long nvals = 3;
      int str_len = 10;
      // Make object large enough to hold a Fortran array of strings:
      // Character(len=str_len), dimension(nvals)
      // Character*str_len(nvals)
      StringBuilder SBvalues = new StringBuilder((int)nvals * str_len); // Note: casting nvals to int limits the size of array supported
      string group_name = "/Geometry/Cross Sections";
      string dataset_name = "Lengths";
      string attri_name = "Column";
      int file_access_flag = C2F_Interface.ReadOnly;

      is_ok = C2F_Interface.h5_open_file(infile, ref file_access_flag, ref infile_id, infile.Length);
      is_ok = C2F_Interface.h5_open_group(ref infile_id, group_name, ref ingroup_id, group_name.Length);
      is_ok = C2F_Interface.h5_open_dataset(ref ingroup_id, dataset_name, ref indataset_id, dataset_name.Length);
      is_ok = C2F_Interface.h5_open_attribute(ref indataset_id, attri_name, ref attri_id, attri_name.Length);
      is_ok = C2F_Interface.h5_read_attri_1d_array_c(ref attri_id, SBvalues, ref str_len, ref nvals);

      Console.WriteLine("Condition = " + is_ok);
      Assert.IsTrue(is_ok);

      string[] values = C2F_Utilities.ToStringArray(SBvalues, nvals, str_len);

      for (int i = 0; i < nvals; i++) {
        Console.WriteLine("values[" + i + "] = " + values[i]);
      }

      // Files, groups, datasets, and attributes will be closed by TestCleanup()
    }

    // Write Attributes

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Write"),
      TestCategory("C2F Scalars"),
      TestCategory("C2F Attributes")]
    public void C2F_Test_h5_write_attri_scalar_r()
    {
      float outattri_value = 42.2f;
      long nvals_dataset = 10;
      long nvals_attri = 1;
      var float_hdf_array = new float[nvals_dataset];
      int file_access_flag = C2F_Interface.ReadOnly;
      string outgroup_name = "/AttributesTest";
      string outdataset_name = "Flow";
      string outattri_name = "RealScalar";
      int data_type = C2F_Interface.H5T_NATIVE_REAL;
      int kind_nbytes = 4;
      outfile = "C2F_h5_write_attri_scalar_r.h5";
      int rank = 1;

      // Create output file
      file_access_flag = C2F_Interface.Truncate;
      is_ok = C2F_Interface.h5_create_file(outfile, ref file_access_flag, ref outfile_id, outfile.Length);
      Assert.IsTrue(is_ok);

      // Create output group
      is_ok = C2F_Interface.h5_create_group(ref outfile_id, outgroup_name, ref outgroup_id, outgroup_name.Length);
      Assert.IsTrue(is_ok);

      // Create output dataset
      is_ok = C2F_Interface.h5_create_compressed_1d_dataset(ref outgroup_id, outdataset_name, ref nvals_dataset, ref data_type, ref kind_nbytes, ref outdataset_id, outdataset_name.Length);
      Assert.IsTrue(is_ok);

      C2F_Interface.h5_create_attribute(ref outdataset_id, outattri_name, ref rank, ref data_type, ref nvals_attri, ref outattri_id, outattri_name.Length);
      Assert.IsTrue(is_ok);

      is_ok = C2F_Interface.h5_write_attri_scalar_r(ref outattri_id, ref outattri_value);
      Assert.IsTrue(is_ok);

      C2F_Interface.h5_close_attribute(ref outattri_id);
      C2F_Interface.h5_close_dataset(ref outdataset_id);
      C2F_Interface.h5_close_group(ref outgroup_id);
      C2F_Interface.h5_close_file(ref outfile_id);

      // Files, groups, datasets, and attributes will be closed by TestCleanup()
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Write"),
      TestCategory("C2F Scalars"),
      TestCategory("C2F Attributes")]
    public void C2F_Test_h5_write_attri_scalar_i()
    {
      int outattri_value = 42;
      long nvals_dataset = 10;
      long nvals_attri = 1;
      var float_hdf_array = new float[nvals_dataset];
      int file_access_flag = C2F_Interface.ReadOnly;
      string outgroup_name = "/AttributesTest";
      string outdataset_name = "Flow";
      string outattri_name = "IntegerScalar";
      int data_type = C2F_Interface.H5T_NATIVE_INTEGER;
      int kind_nbytes = 4;
      outfile = "C2F_h5_write_attri_scalar_i.h5";
      int rank = 1;

      // Create output file
      file_access_flag = C2F_Interface.Truncate;
      is_ok = C2F_Interface.h5_create_file(outfile, ref file_access_flag, ref outfile_id, outfile.Length);
      Assert.IsTrue(is_ok);

      // Create output group
      is_ok = C2F_Interface.h5_create_group(ref outfile_id, outgroup_name, ref outgroup_id, outgroup_name.Length);
      Assert.IsTrue(is_ok);

      // Create output dataset
      is_ok = C2F_Interface.h5_create_compressed_1d_dataset(ref outgroup_id, outdataset_name, ref nvals_dataset, ref data_type, ref kind_nbytes, ref outdataset_id, outdataset_name.Length);
      Assert.IsTrue(is_ok);

      C2F_Interface.h5_create_attribute(ref outdataset_id, outattri_name, ref rank, ref data_type, ref nvals_attri, ref outattri_id, outattri_name.Length);
      Assert.IsTrue(is_ok);

      is_ok = C2F_Interface.h5_write_attri_scalar_i(ref outattri_id, ref outattri_value);
      Assert.IsTrue(is_ok);

      C2F_Interface.h5_close_attribute(ref outattri_id);
      C2F_Interface.h5_close_dataset(ref outdataset_id);
      C2F_Interface.h5_close_group(ref outgroup_id);
      C2F_Interface.h5_close_file(ref outfile_id);

      // Files, groups, datasets, and attributes will be closed by TestCleanup()
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Write"),
      TestCategory("C2F Scalars"),
      TestCategory("C2F Attributes")]
    public void C2F_Test_h5_write_attri_scalar_c()
    {
      string outattri_value = "42 is the answer";
      long nvals_dataset = 10;
      long nvals_attri = 1;
      var float_hdf_array = new float[nvals_dataset];
      int file_access_flag = C2F_Interface.ReadOnly;
      string outgroup_name = "/AttributesTest";
      string outdataset_name = "Flow";
      string outattri_name = "StringScalar";
      int data_type; // defined below by h5_get_string_datatype()
      int kind_nbytes = 4;
      outfile = "C2F_h5_write_attri_scalar_c.h5";
      int rank = 1;
      int str_len = 512;

      // Create output file
      file_access_flag = C2F_Interface.Truncate;
      is_ok = C2F_Interface.h5_create_file(outfile, ref file_access_flag, ref outfile_id, outfile.Length);
      Assert.IsTrue(is_ok);

      // Create output group
      is_ok = C2F_Interface.h5_create_group(ref outfile_id, outgroup_name, ref outgroup_id, outgroup_name.Length);
      Assert.IsTrue(is_ok);

      // Create output dataset
      data_type = C2F_Interface.H5T_NATIVE_REAL; // data type for data set only
      is_ok = C2F_Interface.h5_create_compressed_1d_dataset(ref outgroup_id, outdataset_name, ref nvals_dataset, ref data_type, ref kind_nbytes, ref outdataset_id, outdataset_name.Length);
      Assert.IsTrue(is_ok);

      // Get the string data type, given the specified string length, str_len
      is_ok = C2F_Interface.h5_get_string_datatype(ref str_len, ref data_type);

      // Create scalar string attribute
      C2F_Interface.h5_create_attribute(ref outdataset_id, outattri_name, ref rank, ref data_type, ref nvals_attri, ref outattri_id, outattri_name.Length);
      Assert.IsTrue(is_ok);

      // Write attribute
      is_ok = C2F_Interface.h5_write_attri_scalar_c(ref outattri_id, outattri_value, ref str_len);
      Assert.IsTrue(is_ok);

      C2F_Interface.h5_close_attribute(ref outattri_id);
      C2F_Interface.h5_close_dataset(ref outdataset_id);
      C2F_Interface.h5_close_group(ref outgroup_id);
      C2F_Interface.h5_close_file(ref outfile_id);

      // Files, groups, datasets, and attributes will be closed by TestCleanup()
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Write"),
      TestCategory("C2F Arrays"),
      TestCategory("C2F Attributes")]
    public void C2F_Test_h5_write_attri_1d_array_r()
    {
      long nvals = 10;
      string ingroup_name = "/Todd";
      string indataset_name = "MyData";
      string inattri_name = "FloatAttribute";
      string outfile = "C2F_h5_write_attri_1d_array_r.h5";
      string outgroup_name = "/Todd";
      string outdataset_name = "MyData";
      string outattri_name = "FloatAttribute";
      var hdf_array = new float[nvals];
      int file_access_flag = C2F_Interface.ReadOnly;
      int kind_nbytes = 4;
      int data_type = C2F_Interface.H5T_NATIVE_REAL;
      int rank = 1;

      C2F_Interface.h5_open_file(infile, ref file_access_flag, ref infile_id, infile.Length);
      is_ok = C2F_Interface.h5_open_group(ref infile_id, ingroup_name, ref ingroup_id, ingroup_name.Length);
      is_ok = C2F_Interface.h5_open_dataset(ref ingroup_id, indataset_name, ref indataset_id, indataset_name.Length);
      is_ok = C2F_Interface.h5_open_attribute(ref indataset_id, inattri_name, ref inattri_id, inattri_name.Length);
      is_ok = C2F_Interface.h5_read_attri_1d_array_r(ref inattri_id, hdf_array, ref nvals);

      C2F_Interface.h5_create_file(outfile, ref file_access_flag, ref outfile_id, outfile.Length);
      is_ok = C2F_Interface.h5_create_group(ref outfile_id, outgroup_name, ref outgroup_id, outgroup_name.Length);
      is_ok = C2F_Interface.h5_create_compressed_1d_dataset(ref outgroup_id, outdataset_name, ref nvals, ref data_type, ref kind_nbytes, ref outdataset_id, outdataset_name.Length);
      is_ok = C2F_Interface.h5_create_attribute(ref outdataset_id, outattri_name, ref rank, ref data_type, ref nvals, ref outattri_id, outattri_name.Length);
      is_ok = C2F_Interface.h5_write_attri_1d_array_r(ref outattri_id, hdf_array, ref nvals);

      Console.WriteLine("Condition = " + is_ok);
      Assert.IsTrue(is_ok);

      // Files, groups, datasets, and attributes will be closed by TestCleanup()
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Write"),
      TestCategory("C2F Arrays"),
      TestCategory("C2F Attributes")]
    public void C2F_Test_h5_write_attri_1d_array_i()
    {
      long nvals = 10;
      string ingroup_name = "/Todd";
      string indataset_name = "MyData";
      string inattri_name = "IntAttribute";
      string outfile = "C2F_h5_write_attri_1d_array_i.h5";
      string outgroup_name = "/Todd";
      string outdataset_name = "MyData";
      string outattri_name = "IntAttribute";
      var hdf_array = new int[nvals];
      int file_access_flag = C2F_Interface.ReadOnly;
      int kind_nbytes = 4;
      int data_type = C2F_Interface.H5T_NATIVE_INTEGER;
      int rank = 1;

      C2F_Interface.h5_open_file(infile, ref file_access_flag, ref infile_id, infile.Length);
      is_ok = C2F_Interface.h5_open_group(ref infile_id, ingroup_name, ref ingroup_id, ingroup_name.Length);
      is_ok = C2F_Interface.h5_open_dataset(ref ingroup_id, indataset_name, ref indataset_id, indataset_name.Length);
      is_ok = C2F_Interface.h5_open_attribute(ref indataset_id, inattri_name, ref inattri_id, inattri_name.Length);
      is_ok = C2F_Interface.h5_read_attri_1d_array_i(ref inattri_id, hdf_array, ref nvals);

      C2F_Interface.h5_create_file(outfile, ref file_access_flag, ref outfile_id, outfile.Length);
      is_ok = C2F_Interface.h5_create_group(ref outfile_id, outgroup_name, ref outgroup_id, outgroup_name.Length);
      is_ok = C2F_Interface.h5_create_compressed_1d_dataset(ref outgroup_id, outdataset_name, ref nvals, ref data_type, ref kind_nbytes, ref outdataset_id, outdataset_name.Length);
      is_ok = C2F_Interface.h5_create_attribute(ref outdataset_id, outattri_name, ref rank, ref data_type, ref nvals, ref outattri_id, outattri_name.Length);
      is_ok = C2F_Interface.h5_write_attri_1d_array_i(ref outattri_id, hdf_array, ref nvals);

      Console.WriteLine("Condition = " + is_ok);
      Assert.IsTrue(is_ok);

      // Files, groups, datasets, and attributes will be closed by TestCleanup()
    }

    [TestMethod]
    [TestCategory("All"),
      TestCategory("C2F All"),
      TestCategory("C2F Write"),
      TestCategory("C2F Arrays"),
      TestCategory("C2F Attributes")]
    public void C2F_Test_h5_write_attri_1d_array_c()
    {
      long nvals = 3;
      int str_len = 10;
      string ingroup_name = "/Geometry/Cross Sections";
      string indataset_name = "Lengths";
      string inattri_name = "Column";
      string outfile = "C2F_h5_write_attri_1d_array_c.h5";
      string outgroup_name = "/Todd";
      string outdataset_name = "MyData";
      string outattri_name = "StringAttribute";
      StringBuilder SBvalues = new StringBuilder((int)nvals * str_len); // Note: casting nvals to int limits the size of array supported
      int file_access_flag = C2F_Interface.ReadOnly;
      int kind_nbytes = 4;
      int data_type = C2F_Interface.H5T_FORTRAN_S1;
      int rank = 1;

      C2F_Interface.h5_open_file(infile, ref file_access_flag, ref infile_id, infile.Length);
      is_ok = C2F_Interface.h5_open_group(ref infile_id, ingroup_name, ref ingroup_id, ingroup_name.Length);
      is_ok = C2F_Interface.h5_open_dataset(ref ingroup_id, indataset_name, ref indataset_id, indataset_name.Length);
      is_ok = C2F_Interface.h5_open_attribute(ref indataset_id, inattri_name, ref inattri_id, inattri_name.Length);
      is_ok = C2F_Interface.h5_read_attri_1d_array_c(ref inattri_id, SBvalues, ref str_len, ref nvals);

      C2F_Interface.h5_create_file(outfile, ref file_access_flag, ref outfile_id, outfile.Length);
      is_ok = C2F_Interface.h5_create_group(ref outfile_id, outgroup_name, ref outgroup_id, outgroup_name.Length);
      is_ok = C2F_Interface.h5_create_compressed_1d_dataset(ref outgroup_id, outdataset_name, ref nvals, ref data_type, ref kind_nbytes, ref outdataset_id, outdataset_name.Length);
      is_ok = C2F_Interface.h5_create_attribute_c(ref outdataset_id, outattri_name, ref rank, ref str_len, ref nvals, ref outattri_id, outattri_name.Length);
      is_ok = C2F_Interface.h5_write_attri_1d_array_c(ref outattri_id, SBvalues, ref str_len, ref nvals);

      Console.WriteLine("Condition = " + is_ok);
      Assert.IsTrue(is_ok);

      // Files, groups, datasets, and attributes will be closed by TestCleanup()
    }

  }
}
