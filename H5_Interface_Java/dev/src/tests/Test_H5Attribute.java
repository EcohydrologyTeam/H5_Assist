package tests;

import static hec.h5.H5Constants.*;

import hec.h5.H5Attribute;
import hec.h5.H5Dataset;
import hec.h5.H5File;
import hec.h5.H5Group;
import org.junit.*;
import org.junit.rules.ExpectedException;
import org.junit.runners.MethodSorters;

import static org.junit.Assert.*;

@FixMethodOrder(MethodSorters.NAME_ASCENDING)
public class Test_H5Attribute
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
        deleteFile("Test_H5Attribute_create_attribute.h5");
        deleteFile("Test_H5Attribute_write_1D_array_double.h5");
        deleteFile("Test_H5Attribute_write_1D_array_float.h5");
        deleteFile("Test_H5Attribute_write_1D_array_int.h5");
        deleteFile("Test_H5Attribute_write_scalar_double.h5");
        deleteFile("Test_H5Attribute_write_scalar_float.h5");
        deleteFile("Test_H5Attribute_write_scalar_int.h5");
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

    /**
     * Test: Throw H5InterfaceException
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test(expected = H5InterfaceException.class)
    public void test_throw_H5InterfaceException() throws H5InterfaceException
    {
        H5Attribute.open(12345, "Test");
    }

    @Rule
    public ExpectedException exception = ExpectedException.none();

    /**
     * Test: Throw H5InterfaceException and check message
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
//    @Test
//    public void test_throw_H5InterfaceException_with_message() throws H5InterfaceException
//    {
//        exception.expect(H5InterfaceException.class);
//        exception.expectMessage("H5Attribute does not exist");
//        H5Attribute.open(12345, "Test");
//    }

    /**
     * Test: Throw H5InterfaceException
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_catch_H5InterfaceException() throws H5InterfaceException
    {
        try {
            H5Attribute.open(12345, "Test");
        }
        catch (H5InterfaceException e) {
            System.out.println(e.getMessage());
        }
    }

    /**
     * Test: Open and close attribute on a dataset
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_open_and_close_dataset_attribute() throws H5InterfaceException
    {
        String groupName = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections";
        String datasetName = "Flow";
        String attriName = "Maximum Value of Data Set";

        int fileID = H5File.open(HDF_INFILE, READ_ONLY);
        int groupID = H5Group.open(fileID, groupName);
        int datasetID = H5Dataset.open(groupID, datasetName);
        int attriID = H5Attribute.open(datasetID, attriName);

        H5Attribute.close(attriID);
        H5Dataset.close(datasetID);
        H5Group.close(groupID);
        H5File.close(fileID);
    }

    /**
     * Test: Open and close attribute on a group
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_open_and_close_group_attribute() throws H5InterfaceException
    {
        String groupName = "/Plan Data/Plan Parameters";
        String attriName = "HDF Compression";

        int fileID = H5File.open(HDF_INFILE, READ_ONLY);
        int groupID = H5Group.open(fileID, groupName);
        int attriID = H5Attribute.open(groupID, attriName);

        H5Attribute.close(attriID);
        H5Group.close(groupID);
        H5File.close(fileID);
    }

    /**
     * Test: Create attribute
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_create_attribute() throws H5InterfaceException
    {
        String outfileName = "Test_H5Attribute_create_attribute.h5";
        String outgroupName = "TestGroup";
        String outdatasetName = "TestDataset";
        long nvals = 10;

        int outfileID = H5File.create(outfileName, TRUNCATE);
        int outgroupID = H5Group.create(outfileID, outgroupName);
        int outdatasetID = H5Dataset.create_1D_dataset(outgroupID, outdatasetName,
                nvals, NATIVE_FLOAT, 4, true);

        // Create group attribute
        int outattriID1 = H5Attribute.create(outgroupID, "H5Group attribute",
                NATIVE_FLOAT, nvals);

        // Create dataset attribute
        int outattriID2 = H5Attribute.create(outdatasetID, "H5Dataset attribute",
                NATIVE_INT, nvals);

        H5Attribute.close(outattriID1);
        H5Attribute.close(outattriID2);
        H5Dataset.close(outdatasetID);
        H5Group.close(outgroupID);
        H5File.close(outfileID);
    }

    /**
     * Test: Read scalar attribute (float)
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_read_scalar_float() throws H5InterfaceException
    {
        String groupName = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections";
        String datasetName = "Flow";
        String attriName = "Maximum Value of Data Set";

        // Open file, group, dataset, and attribute
        int fileID = H5File.open(HDF_INFILE, READ_ONLY);
        int groupID = H5Group.open(fileID, groupName);
        int datasetID = H5Dataset.open(groupID, datasetName);
        int attriID = H5Attribute.open(datasetID, attriName);

        // Read data
        float attriValue = H5Attribute.read_scalar_float(attriID);
        assertEquals(810.03796f, attriValue, 0.0001);

        // Close resources
        H5Attribute.close(attriID);
        H5Dataset.close(datasetID);
        H5Group.close(groupID);
        H5File.close(fileID);
    }

    /**
     * Test: Read scalar attribute (double)
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_read_scalar_double() throws H5InterfaceException
    {
        String groupName = "/Todd";
        String datasetName = "Double_array_1d_new";
        String attriName = "MaxVal";

        // Open file, group, dataset, and attribute
        int fileID = H5File.open(HDF_INFILE, READ_ONLY);
        int groupID = H5Group.open(fileID, groupName);
        int datasetID = H5Dataset.open(groupID, datasetName);
        int attriID = H5Attribute.open(datasetID, attriName);

        // Read data
        double attriValue = H5Attribute.read_scalar_double(attriID);
        assertEquals(10.0, attriValue, 0.0001);

        // Close resources
        H5Attribute.close(attriID);
        H5Dataset.close(datasetID);
        H5Group.close(groupID);
        H5File.close(fileID);
    }

    /**
     * Test: Read scalar attribute (int)
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_read_scalar_int() throws H5InterfaceException
    {
        String groupName = "/Plan Data/Plan Parameters";
        String attriName = "HDF Compression";

        // Open file, group, dataset, and attribute
        int fileID = H5File.open(HDF_INFILE, READ_ONLY);
        int groupID = H5Group.open(fileID, groupName);
        int attriID = H5Attribute.open(groupID, attriName);

        // Read data
        int attriValue = H5Attribute.read_scalar_int(attriID);
        assertEquals(1, attriValue);

        // Close resources
        H5Attribute.close(attriID);
        H5Group.close(groupID);
        H5File.close(fileID);
    }

    /**
     * Test: Read scalar attribute (string)
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_read_scalar_string() throws H5InterfaceException
    {
        String groupName = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections";
        String datasetName = "Flow Lateral";
        String attriName = "Lateral Flow";

        // Open file, group, dataset, and attribute
        int fileID = H5File.open(HDF_INFILE, READ_ONLY);
        int groupID = H5Group.open(fileID, groupName);
        int datasetID = H5Dataset.open(groupID, datasetName);
        int attriID = H5Attribute.open(datasetID, attriName);

        // Read data
        String attriValue = H5Attribute.read_scalar_string(attriID);
        assertEquals("m3/s", attriValue);

        // Close resources
        H5Attribute.close(attriID);
        H5Dataset.close(datasetID);
        H5Group.close(groupID);
        H5File.close(fileID);
    }

    /**
     * Test: Read attribute array (float)
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_read_1D_array_float() throws H5InterfaceException
    {
        String groupName = "/Todd";
        String datasetName = "MyData";
        String attriName = "FloatAttribute";
        int nvals = 10;

        // Open file, group, dataset, and attribute
        int fileID = H5File.open(HDF_INFILE, READ_ONLY);
        int groupID = H5Group.open(fileID, groupName);
        int datasetID = H5Dataset.open(groupID, datasetName);
        int attriID = H5Attribute.open(datasetID, attriName);

        // Read data
        float[] attriValues = H5Attribute.read_1D_array_float(attriID, nvals);

        // Verify data
        float[] expectedValues = {1.1f, 2.2f, 3.3f, 4.4f, 5.5f, 6.6f, 7.7f, 8.8f, 9.9f, 10.10f};
        for (int i = 0; i < nvals; i++) {
            assertEquals(expectedValues[i], attriValues[i], 0.001);
        }

        // Close resources
        H5Attribute.close(attriID);
        H5Dataset.close(datasetID);
        H5Group.close(groupID);
        H5File.close(fileID);
    }

    /**
     * Test: Read attribute array (double)
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_read_1D_array_double() throws H5InterfaceException
    {
        String groupName = "/Todd";
        String datasetName = "MyData";
        String attriName = "DoubleAttribute";
        int nvals = 10;

        // Open file, group, dataset, and attribute
        int fileID = H5File.open(HDF_INFILE, READ_ONLY);
        int groupID = H5Group.open(fileID, groupName);
        int datasetID = H5Dataset.open(groupID, datasetName);
        int attriID = H5Attribute.open(datasetID, attriName);

        // Read data
        double[] attriValues = H5Attribute.read_1D_array_double(attriID, nvals);

        // Verify data
        double[] expectedValues = {1.1, 2.2, 3.3, 4.4, 5.5, 6.6, 7.7, 8.8, 9.9, 10.10};
        for (int i = 0; i < nvals; i++) {
            assertEquals(expectedValues[i], attriValues[i], 0.001);
        }

        // Close resources
        H5Attribute.close(attriID);
        H5Dataset.close(datasetID);
        H5Group.close(groupID);
        H5File.close(fileID);
    }

    /**
     * Test: Read attribute array (int)
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_read_1D_array_int() throws H5InterfaceException
    {
        String groupName = "/Todd";
        String datasetName = "MyData";
        String attriName = "IntAttribute";
        int nvals = 10;

        // Open file, group, dataset, and attribute
        int fileID = H5File.open(HDF_INFILE, READ_ONLY);
        int groupID = H5Group.open(fileID, groupName);
        int datasetID = H5Dataset.open(groupID, datasetName);
        int attriID = H5Attribute.open(datasetID, attriName);

        // Read data
        int[] attriValues = H5Attribute.read_1D_array_int(attriID, nvals);

        // Verify data
        int[] expectedValues = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
        for (int i = 0; i < nvals; i++) {
            assertEquals(expectedValues[i], attriValues[i], 0.001);
        }

        // Close resources
        H5Attribute.close(attriID);
        H5Dataset.close(datasetID);
        H5Group.close(groupID);
        H5File.close(fileID);
    }

    /**
     * Test: Read attribute array (string)
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_read_1D_array_string() throws H5InterfaceException
    {
        String groupName = "/Geometry/Cross Sections";
        String datasetName = "Lengths";
        String attriName = "Column";
        int nvals = 3;

        // Open file, group, dataset, and attribute
        int fileID = H5File.open(HDF_INFILE, READ_ONLY);
        int groupID = H5Group.open(fileID, groupName);
        int datasetID = H5Dataset.open(groupID, datasetName);
        int attriID = H5Attribute.open(datasetID, attriName);

        // Read data
        String[] attriValues = H5Attribute.read_1D_array_string(attriID, nvals);
        assertEquals("LengthLOB", attriValues[0]);
        assertEquals("LengthChan", attriValues[1]);
        assertEquals("LengthROB", attriValues[2]);

        // Close resources
        H5Attribute.close(attriID);
        H5Dataset.close(datasetID);
        H5Group.close(groupID);
        H5File.close(fileID);
    }

    /**
     * Test: Write scalar attribute (float)
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_write_scalar_float() throws H5InterfaceException
    {
        String outfileName = "Test_H5Attribute_write_scalar_float.h5";
        String groupName = "/Todd";
        String datasetName = "MyData";
        String attriName = "FloatAttribute";
        int nvals = 1;

        // Open file, group, dataset, and attribute
        int fileID = H5File.create(outfileName, TRUNCATE);
        int groupID = H5Group.create(fileID, groupName);
        int datasetID = H5Dataset.create_1D_dataset(groupID, datasetName, nvals,
                NATIVE_FLOAT, 4, true);
        int attriID = H5Attribute.create(datasetID, attriName, NATIVE_FLOAT, nvals);

        // Write data
        H5Attribute.write_scalar(attriID, 2.0f);

        // Verify data, reading result back from output file
        float attriValue = H5Attribute.read_scalar_float(attriID);
        assertEquals(2.0f, attriValue, 0.001);

        // Close resources
        H5Attribute.close(attriID);
        H5Dataset.close(datasetID);
        H5Group.close(groupID);
        H5File.close(fileID);
    }

    /**
     * Test: Write scalar attribute (double)
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_write_scalar_double() throws H5InterfaceException
    {
        String outfileName = "Test_H5Attribute_write_scalar_double.h5";
        String groupName = "/Todd";
        String datasetName = "MyData";
        String attriName = "DoubleAttribute";
        int nvals = 1;

        // Open file, group, dataset, and attribute
        int fileID = H5File.create(outfileName, TRUNCATE);
        int groupID = H5Group.create(fileID, groupName);
        int datasetID = H5Dataset.create_1D_dataset(groupID, datasetName, nvals,
                NATIVE_FLOAT, 4, true);
        int attriID = H5Attribute.create(datasetID, attriName, NATIVE_DOUBLE, nvals);

        // Write data
        H5Attribute.write_scalar(attriID, 2.0);

        // Verify data, reading result back from output file
        double attriValue = H5Attribute.read_scalar_double(attriID);
        assertEquals(2.0, attriValue, 0.001);

        // Close resources
        H5Attribute.close(attriID);
        H5Dataset.close(datasetID);
        H5Group.close(groupID);
        H5File.close(fileID);
    }

    /**
     * Test: Write scalar attribute (int)
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_write_scalar_int() throws H5InterfaceException
    {
        String outfileName = "Test_H5Attribute_write_scalar_int.h5";
        String groupName = "/Todd";
        String datasetName = "MyData";
        String attriName = "IntAttribute";
        int nvals = 1;

        // Open file, group, dataset, and attribute
        int fileID = H5File.create(outfileName, TRUNCATE);
        int groupID = H5Group.create(fileID, groupName);
        int datasetID = H5Dataset.create_1D_dataset(groupID, datasetName, nvals,
                NATIVE_INT, 4, true);
        int attriID = H5Attribute.create(datasetID, attriName, NATIVE_INT, nvals);

        // Write data
        H5Attribute.write_scalar(attriID, 2);

        // Verify data, reading result back from output file
        int attriValue = H5Attribute.read_scalar_int(attriID);
        assertEquals(2, attriValue);

        // Close resources
        H5Attribute.close(attriID);
        H5Dataset.close(datasetID);
        H5Group.close(groupID);
        H5File.close(fileID);
    }

    /**
     * Test: Write scalar attribute (string)
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_write_scalar_string() throws H5InterfaceException
    {
        String outfileName = "Test_H5Attribute_write_scalar_string.h5";
        String groupName = "/Todd";
        String datasetName = "MyData";
        String attriName = "StringAttribute";
        String attriValue = "Test";
        int nvals = 1;
        int str_len = attriValue.length();

        // Open file, group, dataset, and attribute
        int fileID = H5File.create(outfileName, TRUNCATE);
        int groupID = H5Group.create(fileID, groupName);
        int datasetID = H5Dataset.create_1D_dataset_string(groupID, datasetName,
                nvals, str_len, true);
        int attriID = H5Attribute.create_string(datasetID, attriName, nvals, str_len);

        // Write data
        H5Attribute.write_scalar(attriID, attriValue);

        // Verify data, reading result back from output file
        String expectedValue = H5Attribute.read_scalar_string(attriID);
        assertEquals(expectedValue, attriValue);

        // Close resources
        H5Attribute.close(attriID);
        H5Dataset.close(datasetID);
        H5Group.close(groupID);
        H5File.close(fileID);
    }

    /**
     * Test: Write 1D attribute array (float)
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_write_1D_array_float() throws H5InterfaceException
    {
        String outfileName = "Test_H5Attribute_write_1D_array_float.h5";
        String groupName = "/Todd";
        String datasetName = "MyData";
        String attriName = "FloatAttribute";
        int nvals = 10;

        // Open file, group, dataset, and attribute
        int fileID = H5File.create(outfileName, TRUNCATE);
        int groupID = H5Group.create(fileID, groupName);
        int datasetID = H5Dataset.create_1D_dataset(groupID, datasetName, nvals,
                NATIVE_FLOAT, 4, true);
        int attriID = H5Attribute.create(datasetID, attriName, NATIVE_FLOAT, nvals);

        // Write data
        float[] attriValues = new float[]{1.1f, 2.2f, 3.3f, 4.4f, 5.5f, 6.6f, 7.7f, 8.8f, 9.9f, 10.10f};
        H5Attribute.write_1D_array(attriID, attriValues);

        // Verify data, reading result back from output file
        float[] expectedValues = new float[]{1.1f, 2.2f, 3.3f, 4.4f, 5.5f, 6.6f, 7.7f, 8.8f, 9.9f, 10.10f};
        float[] actualValues = H5Attribute.read_1D_array_float(attriID, nvals);
        assertArrayEquals(expectedValues, actualValues, 0.001f);

        // Close resources
        H5Attribute.close(attriID);
        H5Dataset.close(datasetID);
        H5Group.close(groupID);
        H5File.close(fileID);
    }

    /**
     * Test: Write 1D attribute array (double)
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_write_1D_array_double() throws H5InterfaceException
    {
        String outfileName = "Test_H5Attribute_write_1D_array_double.h5";
        String groupName = "/Todd";
        String datasetName = "MyData";
        String attriName = "DoubleAttribute";
        int nvals = 10;

        // Open file, group, dataset, and attribute
        int fileID = H5File.create(outfileName, TRUNCATE);
        int groupID = H5Group.create(fileID, groupName);
        int datasetID = H5Dataset.create_1D_dataset(groupID, datasetName, nvals,
                NATIVE_FLOAT, 4, true);
        int attriID = H5Attribute.create(datasetID, attriName, NATIVE_DOUBLE, nvals);

        // Write data
        double[] attriValues = new double[]{1.1, 2.2, 3.3, 4.4, 5.5, 6.6, 7.7, 8.8, 9.9, 10.10};
        H5Attribute.write_1D_array(attriID, attriValues);

        // Verify data, reading result back from output file
        double[] expectedValues = new double[]{1.1f, 2.2f, 3.3f, 4.4f, 5.5f, 6.6f, 7.7f, 8.8f, 9.9f, 10.10f};
        double[] actualValues = H5Attribute.read_1D_array_double(attriID, nvals);
        assertArrayEquals(expectedValues, actualValues, 0.001);

        // Close resources
        H5Attribute.close(attriID);
        H5Dataset.close(datasetID);
        H5Group.close(groupID);
        H5File.close(fileID);
    }

    /**
     * Test: Write 1D attribute array (int)
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_write_1D_array_int() throws H5InterfaceException
    {
        String outfileName = "Test_H5Attribute_write_1D_array_int.h5";
        String groupName = "/Todd";
        String datasetName = "MyData";
        String attriName = "IntAttribute";
        int nvals = 10;

        // Open file, group, dataset, and attribute
        int fileID = H5File.create(outfileName, TRUNCATE);
        int groupID = H5Group.create(fileID, groupName);
        int datasetID = H5Dataset.create_1D_dataset(groupID, datasetName, nvals,
                NATIVE_INT, 4, true);
        int attriID = H5Attribute.create(datasetID, attriName, NATIVE_INT, nvals);

        // Write data
        int[] attriValues = new int[]{1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
        H5Attribute.write_1D_array(attriID, attriValues);

        // Verify data, reading result back from output file
        int[] expectedValues = new int[]{1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
        int[] actualValues = H5Attribute.read_1D_array_int(attriID, nvals);
        assertArrayEquals(expectedValues, actualValues);

        // Close resources
        H5Attribute.close(attriID);
        H5Dataset.close(datasetID);
        H5Group.close(groupID);
        H5File.close(fileID);
    }
}
