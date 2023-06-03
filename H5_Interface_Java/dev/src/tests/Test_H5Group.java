package tests;

import static hec.h5.H5Constants.*;

import hec.h5.H5File;
import hec.h5.H5Group;
import ncsa.hdf.hdf5lib.HDF5Constants;

import static org.junit.Assert.*;

import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.FixMethodOrder;
import org.junit.Test;
import org.junit.runners.MethodSorters;


@FixMethodOrder(MethodSorters.NAME_ASCENDING)
public class Test_H5Group
{
    @BeforeClass
    public static void setUpBeforeClass() throws Exception
    {
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
     * Test: Open and close group
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_Group_open_and_close() throws H5InterfaceException
    {
        String fileName = "LMNRRAS.p03.hdf";
        String groupName = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections";

        int fileID = H5File.open(fileName, HDF5Constants.H5F_ACC_RDONLY);
        int groupID = H5Group.open(fileID, groupName);

        assertTrue(groupID > -1);

        H5Group.close(groupID);
    }

    /**
     * Test: Create group
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_Group_create() throws H5InterfaceException
    {
        String fileName = "test_Group_create.h5";
        String groupName = "Test";
        int fileID = H5File.create(fileName, HDF5Constants.H5F_ACC_TRUNC);
        int groupID = H5Group.create(fileID, groupName);
        H5Group.close(groupID);
    }

}
