package tests;

import static hec.h5.H5Constants.*;

import hec.h5.H5Dataset;
import hec.h5.H5File;
import hec.h5.H5Group;

import static org.junit.Assert.*;

import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.FixMethodOrder;
import org.junit.Test;
import org.junit.runners.MethodSorters;


/**
 *
 */
@FixMethodOrder(MethodSorters.NAME_ASCENDING)
public class Test_H5Dataset
{
    private static String HDF_INFILE = "LMNRRAS.p03.hdf";

    private static boolean deleteFile(String filename)
    {
        java.io.File file = new java.io.File(filename);
        return file.delete();
    }

    @BeforeClass
    public static void setUpBeforeClass() throws Exception
    {
        deleteFile("Test_create_1D_dataset.h5");
        deleteFile("Test_create_1D_dataset_string.h5");
        deleteFile("Test_create_2D_dataset_float.h5");
        deleteFile("Test_write_1D_array_float_whole_dataset.h5");
        deleteFile("Test_write_1D_array_int_whole_dataset.h5");
        deleteFile("Test_write_1D_array_string.h5");
        deleteFile("Test_write_2D_array_double_chunked_in_time.h5");
        deleteFile("Test_write_2D_array_double_not_chunked.h5");
        deleteFile("Test_write_2D_array_float_by_row.h5");
        deleteFile("Test_write_2D_array_float_whole_dataset.h5");
        deleteFile("Test_write_2D_array_int.h5");
    }

    @AfterClass
    public static void tearDownAfterClass() throws Exception
    {
    }

    @Before
    public void setUp() throws Exception
    {
    }

    @After
    public void tearDown() throws Exception
    {
    }

//    /**
//     * Test: Dimensions constructor, given rank (int)
//     */
//    @Test
//    public void test_Dimensions_int_constructor()
//    {
//        int rank = 5;
//        H5Dataset.Dimensions dims = new H5Dataset.Dimensions(rank);
//        assertEquals(rank, dims.currentDims.length);
//        assertEquals(rank, dims.maxDims.length);
//    }
//
//    /**
//     * Test: Dimensions constructor, given dimension arrays
//     */
//    @Test
//    public void test_Dimensions_array_constructor()
//    {
//        int rank = 5;
//        long[] currentDims = new long[rank];
//        long[] maxDims = new long[rank];
//        H5Dataset.Dimensions dims = new H5Dataset.Dimensions(currentDims, maxDims);
//        assertEquals(currentDims.length, dims.currentDims.length);
//        assertEquals(maxDims.length, dims.maxDims.length);
//    }
//
//    /**
//     * Test: Get dimensions of dataset
//     *
//     * @throws H5InterfaceException HDF5 interface exception
//     */
//    @Test
//    public void test_getDimensions() throws H5InterfaceException
//    {
//        String groupName = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections";
//        String datasetName = "Flow";
//
//        int fileID = H5File.open(HDF_INFILE, READ_ONLY);
//        int groupID = H5Group.open(fileID, groupName);
//        int datasetID = H5Dataset.open(groupID, datasetName);
//        H5Dataset.Dimensions dims = H5Dataset.getDimensions(datasetID, 2);
//        assertEquals(2, dims.currentDims.length);
//        assertEquals(2, dims.maxDims.length);
//        System.out.println(dims);
//    }


    /**
     * Test: H5Dataset exists in group, given group name
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_datasetExistsInGroupName() throws H5InterfaceException
    {
        String groupName = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections";
        String datasetName = "Flow";

        int fileID = H5File.open(HDF_INFILE, READ_ONLY);
        boolean exists = H5Dataset.datasetExistsInGroupName(fileID, groupName, datasetName);
        assertTrue(exists);
    }

    /**
     * Test: H5Dataset exists in group, given group identifier
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_datasetExistsInGroupID() throws H5InterfaceException
    {
        String groupName = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections";
        String datasetName = "Flow";

        int fileID = H5File.open(HDF_INFILE, READ_ONLY);
        int groupID = H5Group.open(fileID, groupName);
        boolean exists = H5Dataset.datasetExistsInGroupID(groupID, datasetName);
        assertTrue(exists);
    }

    /**
     * Test: Create 1D dataset (float, double, and int)
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_create_1D_dataset() throws H5InterfaceException
    {
        String outFileName = "Test_create_1D_dataset.h5";
        long nvals = 10;

        // Create output file and group
        int outfile_id = H5File.create(outFileName, TRUNCATE);
        int outgroup_id = H5Group.create(outfile_id, "TestGroup");

        // Create 2D integer dataset: float (kind = 4)
        int dset1 = H5Dataset.create_1D_dataset(outgroup_id, "Compressed Float", nvals, NATIVE_FLOAT, 4, true);
        int dset2 = H5Dataset.create_1D_dataset(outgroup_id, "Uncompressed Float", nvals, NATIVE_FLOAT, 4, false);

        // Create 2D integer dataset: double (kind = 8)
        int dset3 = H5Dataset.create_1D_dataset(outgroup_id, "Compressed Double", nvals, NATIVE_DOUBLE, 8, true);
        int dset4 = H5Dataset.create_1D_dataset(outgroup_id, "Uncompressed Double", nvals, NATIVE_DOUBLE, 8, false);

        // Create 2D integer dataset: int (kind = 4)
        int dset5 = H5Dataset.create_1D_dataset(outgroup_id, "Compressed Integer", nvals, NATIVE_INT, 4, true);
        int dset6 = H5Dataset.create_1D_dataset(outgroup_id, "Uncompressed Integer", nvals, NATIVE_INT, 4, false);

        H5Dataset.close(dset1);
        H5Dataset.close(dset2);
        H5Dataset.close(dset3);
        H5Dataset.close(dset4);
        H5Dataset.close(dset5);
        H5Dataset.close(dset6);
        H5Group.close(outgroup_id);
        H5File.close(outfile_id);
    }

    /**
     * Test: Create 2D datasets
     * <p>
     * Testing all combinations of int, double, float, compressed
     * uncompressed, chunked in time, and chunked in space
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_create_2D_datasets() throws H5InterfaceException
    {
        String outFileName = "Test_create_2D_dataset_float.h5";
        long nrows_hdf = 2953;
        long ncols_hdf = 334;

        // Create output file
        int outfile_id = H5File.create(outFileName, TRUNCATE);

        // Create group
        int outgroup_id = H5Group.create(outfile_id, "TestGroup");

        // Create 2D float dataset (kind = 4)
        int dset1 = H5Dataset.create_2D_dataset(outgroup_id, "Uncompressed Float", nrows_hdf, ncols_hdf, NATIVE_FLOAT, true, 4, false);
        int dset2 = H5Dataset.create_2D_dataset(outgroup_id, "Compressed Float - Chunked in Time", nrows_hdf, ncols_hdf, NATIVE_FLOAT, true, 4, true);
        int dset3 = H5Dataset.create_2D_dataset(outgroup_id, "Compressed Float - Chunked in Space", nrows_hdf, ncols_hdf, NATIVE_FLOAT, false, 4, true);

        // Create 2D double dataset (kind = 8)
        int dset4 = H5Dataset.create_2D_dataset(outgroup_id, "Uncompressed Double", nrows_hdf, ncols_hdf, NATIVE_DOUBLE, true, 8, false);
        int dset5 = H5Dataset.create_2D_dataset(outgroup_id, "Compressed Double - Chunked in Time", nrows_hdf, ncols_hdf, NATIVE_DOUBLE, true, 8, true);
        int dset6 = H5Dataset.create_2D_dataset(outgroup_id, "Compressed Double - Chunked in Space", nrows_hdf, ncols_hdf, NATIVE_DOUBLE, false, 8, true);

        // Create 2D int dataset (kind = 4)
        int dset7 = H5Dataset.create_2D_dataset(outgroup_id, "Uncompressed Integer", nrows_hdf, ncols_hdf, NATIVE_INT, true, 4, false);
        int dset8 = H5Dataset.create_2D_dataset(outgroup_id, "Compressed Integer - Chunked in Time", nrows_hdf, ncols_hdf, NATIVE_INT, true, 4, true);
        int dset9 = H5Dataset.create_2D_dataset(outgroup_id, "Compressed Integer - Chunked in Space", nrows_hdf, ncols_hdf, NATIVE_INT, false, 4, true);

        H5Dataset.close(dset1);
        H5Dataset.close(dset2);
        H5Dataset.close(dset3);
        H5Dataset.close(dset4);
        H5Dataset.close(dset5);
        H5Dataset.close(dset6);
        H5Dataset.close(dset7);
        H5Dataset.close(dset8);
        H5Dataset.close(dset9);
        H5Group.close(outgroup_id);
        H5File.close(outfile_id);
    }

    /**
     * Test: Create 1D string dataset
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_create_1D_dataset_string() throws H5InterfaceException
    {
        String outFileName = "Test_create_1D_dataset_string.h5";
        long nvals = 2953;
        int str_len = 16;

        // Create output file
        int outfile_id = H5File.create(outFileName, TRUNCATE);

        // Create group
        int outgroup_id = H5Group.create(outfile_id, "TestGroup");

        // Create 2D integer dataset
        int dset1 = H5Dataset.create_1D_dataset_string(outgroup_id, "Compressed String", nvals, str_len, true);
        int dset2 = H5Dataset.create_1D_dataset_string(outgroup_id, "Uncompressed String", nvals, str_len, false);

        // Close resources
        H5Dataset.close(dset1);
        H5Dataset.close(dset2);
        H5Group.close(outgroup_id);
        H5File.close(outfile_id);
    }

    /**
     * Test: Read 2D float array - read whole dataset at once
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_read_2D_array_double_whole_dataset() throws H5InterfaceException
    {
        String groupName = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections";
        String datasetName = "Flow";
        int startrow = 0;
        int startcol = 0;
        int nrows_hdf = 2953;
        int ncols_hdf = 334;

        int fileID = H5File.open(HDF_INFILE, READ_ONLY);
        int groupID = H5Group.open(fileID, groupName);
        int datasetID = H5Dataset.open(groupID, datasetName);
        double[][] hdfArray = H5Dataset.read_2D_array_double(datasetID, startrow, startcol, nrows_hdf, ncols_hdf);
        System.out.println(hdfArray[0][0]);
    }

    /**
     * Test: Read 2D float array - one row at a time
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_read_2D_array_float_by_row() throws H5InterfaceException
    {
        String fileName = HDF_INFILE;
        String groupName = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections";
        String datasetName = "Flow";
        int nrows_hdf = 2953;
        int ncols_hdf = 334;
        int startrow = 0;
        int startcol = 0;

        int fileID = H5File.open(fileName, READ_ONLY);
        int groupID = H5Group.open(fileID, groupName);
        int datasetID = H5Dataset.open(groupID, datasetName);

        float[][] hdfArray = new float[nrows_hdf][ncols_hdf];
        float[][] rowVector;
        int nrows = 1;
        for (int i = startrow; i < nrows_hdf; i++) {
            rowVector = H5Dataset.read_2D_array_float(datasetID, i, startcol, nrows, ncols_hdf);
            hdfArray[i] = rowVector[0];
        }
        System.out.println(hdfArray[0][0]);
    }

    /**
     * Test: Read 2D double array - read whole dataset at once
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_read_2D_array_float_whole_dataset() throws H5InterfaceException
    {
        String groupName = "/Todd";
        String datasetName = "Double_array_2d";
        int startrow = 0;
        int startcol = 0;
        int nrows_hdf = 30;
        int ncols_hdf = 20;

        int fileID = H5File.open(HDF_INFILE, READ_ONLY);
        int groupID = H5Group.open(fileID, groupName);
        int datasetID = H5Dataset.open(groupID, datasetName);
        float[][] hdfArray = H5Dataset.read_2D_array_float(datasetID, startrow, startcol, nrows_hdf, ncols_hdf);
        System.out.println(hdfArray[0][0]);
    }

    /**
     * Test: Read 2D int array - read whole dataset at once
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_read_2D_array_int_whole_dataset() throws H5InterfaceException
    {
        String groupName = "/Geometry/Cross Sections";
        String datasetName = "Polyline Info";
        int startrow = 0;
        int startcol = 0;
        int nrows_hdf = 334;
        int ncols_hdf = 4;

        int fileID = H5File.open(HDF_INFILE, READ_ONLY);
        int groupID = H5Group.open(fileID, groupName);
        int datasetID = H5Dataset.open(groupID, datasetName);

        int[][] hdfArray = H5Dataset.read_2D_array_int(datasetID, startrow, startcol, nrows_hdf, ncols_hdf);

        System.out.println(hdfArray[0][0]);
    }

    /**
     * Test: Read 1D float array
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_read_1D_array_float() throws H5InterfaceException
    {
        String groupName = "/Todd";
        String datasetName = "Float_array_1d";
        long startrow = 0;
        long nvals = 10;

        int fileID = H5File.open(HDF_INFILE, READ_ONLY);
        int groupID = H5Group.open(fileID, groupName);
        int datasetID = H5Dataset.open(groupID, datasetName);

        float[] hdfArray = H5Dataset.read_1D_array_float(datasetID, startrow, nvals);

        float[] expectedValues = new float[]{2.2f, 4.4f, 6.6f, 8.8f, 10.10f, 12.12f,
                14.14f, 16.16f, 18.18f, 20.20f};

        assertArrayEquals(expectedValues, hdfArray, 0.001f);
    }

    /**
     * Test: Read 1D double array
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_read_1D_array_double() throws H5InterfaceException
    {
        String groupName = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series";
        String datasetName = "Time";
        long startrow = 0;
        long nvals = 2953;

        int fileID = H5File.open(HDF_INFILE, READ_ONLY);
        int groupID = H5Group.open(fileID, groupName);
        int datasetID = H5Dataset.open(groupID, datasetName);
        double[] hdfArray = H5Dataset.read_1D_array_double(datasetID, startrow, nvals);

        // Test the first several values
        double[] expectedValues = new double[]{-0.0, 0.041666666666666664, 0.08333333333333333,
                0.125, 0.16666666666666666, 0.20833333333333334};
        for (int i = 0; i < expectedValues.length; i++) {
            assertEquals(expectedValues[i], hdfArray[i], 0.001);
        }
    }

    /**
     * Test: Read 1D int array
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_read_1D_array_int() throws H5InterfaceException
    {
        String groupName = "/Todd";
        String datasetName = "Integer_array_1d";
        long startrow = 0;
        long nvals = 10;

        int fileID = H5File.open(HDF_INFILE, READ_ONLY);
        int groupID = H5Group.open(fileID, groupName);
        int datasetID = H5Dataset.open(groupID, datasetName);

        int[] hdfArray = H5Dataset.read_1D_array_int(datasetID, startrow, nvals);

        int[] expectedValues = {2, 4, 6, 8, 10, 12, 14, 16, 18, 20};
        assertArrayEquals(expectedValues, hdfArray);
    }

    /**
     * Test: Read 1D string array
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_read_1D_array_string() throws H5InterfaceException
    {
        String groupName = "/Geometry/Cross Sections";
        String datasetName = "Node Descriptions";
        long startrow = 0;
        long nvals = 334;
        int str_len = 512;

        int fileID = H5File.open(HDF_INFILE, READ_ONLY);
        int groupID = H5Group.open(fileID, groupName);
        int datasetID = H5Dataset.open(groupID, datasetName);

        String[] hdfArray = H5Dataset.read_1D_array_string(datasetID, startrow, nvals, str_len);

        System.out.println(hdfArray[0]);
    }

    /**
     * Test: Write 1D float array - write whole dataset at once
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_write_1D_array_float_a_whole_dataset() throws H5InterfaceException
    {
        String ingroupName = "Todd";
        String indatasetName = "Float_array_1d";
        String outfileName = "Test_write_1D_array_float_whole_dataset.h5";
        String outgroupName = "/Todd";
        String outdatasetName = "Float_array_1d";
        long startrow = 0;
        long nvals = 10;

        int infileID = H5File.open(HDF_INFILE, READ_ONLY);
        int ingroupID = H5Group.open(infileID, ingroupName);
        int indatasetID = H5Dataset.open(ingroupID, indatasetName);

        float[] hdfArray = H5Dataset.read_1D_array_float(indatasetID, startrow, nvals);

        int outfileID = H5File.create(outfileName, TRUNCATE);
        int outgroupID = H5Group.create(outfileID, outgroupName);
        int outdatasetID = H5Dataset.create_1D_dataset(outgroupID, outdatasetName, nvals, NATIVE_FLOAT, 4, true);

        H5Dataset.write_1D_array_float(outdatasetID, startrow, nvals, hdfArray);

        // Close and release resources
        H5Dataset.close(indatasetID);
        H5Dataset.close(outdatasetID);
        H5Group.close(ingroupID);
        H5Group.close(outgroupID);
        H5File.close(infileID);
        H5File.close(outfileID);
    }

    /**
     * Test: Write 1D int array - write whole dataset at once
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_write_1D_array_int_a_whole_dataset() throws H5InterfaceException
    {
        String ingroupName = "Todd";
        String indatasetName = "Integer_array_1d";
        String outfileName = "Test_write_1D_array_int_whole_dataset.h5";
        String outgroupName = "/Todd";
        String outdatasetName = "Integer_array_1d";
        long startrow = 0;
        long nvals = 10;

        int dataTypeID = NATIVE_INT;

        int infileID = H5File.open(HDF_INFILE, READ_ONLY);
        int ingroupID = H5Group.open(infileID, ingroupName);
        int indatasetID = H5Dataset.open(ingroupID, indatasetName);
        int[] hdfArray = H5Dataset.read_1D_array_int(indatasetID, startrow, nvals);
        int outfileID = H5File.create(outfileName, TRUNCATE);
        int outgroupID = H5Group.create(outfileID, outgroupName);
        int outdatasetID = H5Dataset.create_1D_dataset(outgroupID, outdatasetName, nvals, dataTypeID, 4, true);
        H5Dataset.write_1D_array_int(outdatasetID, startrow, nvals, hdfArray);

        // Close and release resources
        H5Dataset.close(indatasetID);
        H5Dataset.close(outdatasetID);
        H5Group.close(ingroupID);
        H5Group.close(outgroupID);
        H5File.close(infileID);
        H5File.close(outfileID);
    }

    /**
     * Test: Write 2D float array - write whole dataset at once
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_write_2D_array_float_a_whole_dataset() throws H5InterfaceException
    {
        String ingroupName = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections";
        String indatasetName = "Flow";
        String outfileName = "Test_write_2D_array_float_whole_dataset.h5";
        String outgroupName = "/Results";
        String outdatasetName = "Flow";
        int startrow = 0;
        int startcol = 0;
        int nrows_hdf = 2953;
        int ncols_hdf = 334;

        int infileID = H5File.open(HDF_INFILE, READ_ONLY);
        int ingroupID = H5Group.open(infileID, ingroupName);
        int indatasetID = H5Dataset.open(ingroupID, indatasetName);
        float[][] hdfArray = H5Dataset.read_2D_array_float(indatasetID, startrow, startcol, nrows_hdf, ncols_hdf);
        int outfileID = H5File.create(outfileName, TRUNCATE);
        int outgroupID = H5Group.create(outfileID, outgroupName);
        int outdatasetID = H5Dataset.create_2D_dataset(outgroupID, outdatasetName, nrows_hdf, ncols_hdf,
                NATIVE_FLOAT, true, 4, true);
        H5Dataset.write_2D_array_float(outdatasetID, startrow, startcol, nrows_hdf, ncols_hdf, hdfArray);

        // Close and release resources
        H5Dataset.close(indatasetID);
        H5Dataset.close(outdatasetID);
        H5Group.close(ingroupID);
        H5Group.close(outgroupID);
        H5File.close(infileID);
        H5File.close(outfileID);
    }

    /**
     * Test: Write 2D float array - one row at a time
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_write_2D_array_float_by_row() throws H5InterfaceException
    {
        String ingroupName = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections";
        String indatasetName = "Flow";
        String outfileName = "Test_write_2D_array_float_by_row.h5";
        String outgroupName = "/Results";
        String outdatasetName = "Flow";
        int nrows_hdf = 2953;
        int ncols_hdf = 334;

        // Read dataset one row at a time
        int infileID = H5File.open(HDF_INFILE, READ_ONLY);
        int ingroupID = H5Group.open(infileID, ingroupName);
        int indatasetID = H5Dataset.open(ingroupID, indatasetName);
        float[][] hdfArray = new float[nrows_hdf][ncols_hdf];
        float[][] rowVector = new float[1][ncols_hdf];
        int nrows_vector = 1;
        for (int row = 0; row < nrows_hdf; row++) {
            rowVector = H5Dataset.read_2D_array_float(indatasetID, row, 0, nrows_vector, ncols_hdf);
            hdfArray[row] = rowVector[0];
        }

        // Write array to output file, one row at a time
        int outfileID = H5File.create(outfileName, TRUNCATE);
        int outgroupID = H5Group.create(outfileID, outgroupName);
        int outdatasetID = H5Dataset.create_2D_dataset(outgroupID, outdatasetName, nrows_hdf, ncols_hdf,
                NATIVE_FLOAT, true, 4, true);
        for (int row = 0; row < nrows_hdf; row++) {
            rowVector[0] = hdfArray[row];
            H5Dataset.write_2D_array_float(outdatasetID, row, 0, nrows_vector, ncols_hdf, rowVector);
        }

        // Close and release resources
        H5Dataset.close(indatasetID);
        H5Dataset.close(outdatasetID);
        H5Group.close(ingroupID);
        H5Group.close(outgroupID);
        H5File.close(infileID);
        H5File.close(outfileID);
    }

    /**
     * Test: Write 2D double array - chunked in time and compressed
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_write_2D_array_double_chunked_in_time() throws H5InterfaceException
    {
        String ingroupName = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections";
        String indatasetName = "Flow";
        String outfileName = "Test_write_2D_array_double_chunked_in_time.h5";
        String outgroupName = "/Results";
        String outdatasetName = "FlowDouble";
        int startrow = 0;
        int startcol = 0;
        int nrows_hdf = 2953;
        int ncols_hdf = 334;

        int infileID = H5File.open(HDF_INFILE, READ_ONLY);
        int ingroupID = H5Group.open(infileID, ingroupName);
        int indatasetID = H5Dataset.open(ingroupID, indatasetName);

        // IMPORTANT: Read float dataset into a float array
        float[][] hdfArray_float = H5Dataset.read_2D_array_float(indatasetID, startrow, startcol, nrows_hdf, ncols_hdf);

        // Copy float array to double array for output
        double[][] hdfArray_double = new double[nrows_hdf][ncols_hdf];
        for (int i = 0; i < nrows_hdf; i++) {
            for (int j = 0; j < ncols_hdf; j++) {
                hdfArray_double[i][j] = hdfArray_float[i][j];
            }
        }

        int outfileID = H5File.create(outfileName, TRUNCATE);
        int outgroupID = H5Group.create(outfileID, outgroupName);
        int outdatasetID = H5Dataset.create_2D_dataset(outgroupID, outdatasetName, nrows_hdf, ncols_hdf,
                NATIVE_DOUBLE, true, 8, true);
        H5Dataset.write_2D_array_double(outdatasetID, startrow, startcol, nrows_hdf, ncols_hdf, hdfArray_double);

        // Close and release resources
        H5Dataset.close(indatasetID);
        H5Dataset.close(outdatasetID);
        H5Group.close(ingroupID);
        H5Group.close(outgroupID);
        H5File.close(infileID);
        H5File.close(outfileID);
    }

    /**
     * Test: Write 2D double array - not chunked or compressed
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_write_2D_array_double_not_chunked() throws H5InterfaceException
    {
        String ingroupName = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections";
        String indatasetName = "Flow";
        String outfileName = "Test_write_2D_array_double_not_chunked.h5";
        String outgroupName = "/Results";
        String outdatasetName = "FlowDouble";
        int startrow = 0;
        int startcol = 0;
        int nrows_hdf = 2953;
        int ncols_hdf = 334;

        int infileID = H5File.open(HDF_INFILE, READ_ONLY);
        int ingroupID = H5Group.open(infileID, ingroupName);
        int indatasetID = H5Dataset.open(ingroupID, indatasetName);

        // IMPORTANT: Read float dataset into a float array
        float[][] hdfArray_float = H5Dataset.read_2D_array_float(indatasetID, startrow, startcol, nrows_hdf, ncols_hdf);

        // Copy float array to double array for output
        double[][] hdfArray_double = new double[nrows_hdf][ncols_hdf];
        for (int i = 0; i < nrows_hdf; i++) {
            for (int j = 0; j < ncols_hdf; j++) {
                hdfArray_double[i][j] = hdfArray_float[i][j];
            }
        }

        int outfileID = H5File.create(outfileName, TRUNCATE);
        int outgroupID = H5Group.create(outfileID, outgroupName);

        // Note: chunk_in_time will have no effect, since isCompressed will be set to false
        int outdatasetID = H5Dataset.create_2D_dataset(outgroupID, outdatasetName, nrows_hdf, ncols_hdf,
                NATIVE_DOUBLE, true, 8, false);

        H5Dataset.write_2D_array_double(outdatasetID, startrow, startcol, nrows_hdf, ncols_hdf, hdfArray_double);

        // Close and release resources
        H5Dataset.close(indatasetID);
        H5Dataset.close(outdatasetID);
        H5Group.close(ingroupID);
        H5Group.close(outgroupID);
        H5File.close(infileID);
        H5File.close(outfileID);
    }

    /**
     * Test: Write 2D int array
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_write_2D_array_int() throws H5InterfaceException
    {
        String ingroupName = "/Geometry/Cross Sections";
        String indatasetName = "Polyline Info";
        String outfileName = "Test_write_2D_array_int.h5";
        String outgroupName = "/Geometry";
        String outdatasetName = "Polyline Info";

        int infileID = H5File.open(HDF_INFILE, READ_ONLY);
        int ingroupID = H5Group.open(infileID, ingroupName);
        int indatasetID = H5Dataset.open(ingroupID, indatasetName);
        int startrow = 0;
        int startcol = 0;
        int nrows_hdf = 334;
        int ncols_hdf = 4;
        int[][] hdfArray = H5Dataset.read_2D_array_int(indatasetID, startrow, startcol, nrows_hdf, ncols_hdf);
        int outfileID = H5File.create(outfileName, TRUNCATE);
        int outgroupID = H5Group.create(outfileID, outgroupName);
        int outdatasetID = H5Dataset.create_2D_dataset(outgroupID, outdatasetName, nrows_hdf, ncols_hdf,
                NATIVE_INT, true, 4, true);
        H5Dataset.write_2D_array_int(outdatasetID, startrow, startcol, nrows_hdf, ncols_hdf, hdfArray);

        // Close and release resources
        H5Dataset.close(indatasetID);
        H5Dataset.close(outdatasetID);
        H5Group.close(ingroupID);
        H5Group.close(outgroupID);
        H5File.close(infileID);
        H5File.close(outfileID);
    }

    /**
     * Test: Write 1D string array
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_write_1D_array_string() throws H5InterfaceException
    {
        String ingroupName = "/Geometry/Cross Sections";
        String indatasetName = "Node Descriptions";
        String outfileName = "Test_write_1D_array_string.h5";
        String outgroupName = "/Geometry";
        String outdatasetName = "Polyline Info";

        int infileID = H5File.open(HDF_INFILE, READ_ONLY);
        int ingroupID = H5Group.open(infileID, ingroupName);
        int indatasetID = H5Dataset.open(ingroupID, indatasetName);
        int startidx = 0;
        int nvals = 334;
        int str_len = 16;

        String[] hdfArray = H5Dataset.read_1D_array_string(indatasetID, startidx, nvals, str_len);
        int outfileID = H5File.create(outfileName, TRUNCATE);
        int outgroupID = H5Group.create(outfileID, outgroupName);
        int outdatasetID = H5Dataset.create_1D_dataset_string(outgroupID, outdatasetName, nvals, str_len, true);

        H5Dataset.write_1D_array_string(outdatasetID, startidx, nvals, str_len, hdfArray);

        // Close and release resources
        H5Dataset.close(indatasetID);
        H5Dataset.close(outdatasetID);
        H5Group.close(ingroupID);
        H5Group.close(outgroupID);
        H5File.close(infileID);
        H5File.close(outfileID);
    }
}
