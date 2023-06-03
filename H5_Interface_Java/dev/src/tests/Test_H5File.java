package tests;

import static hec.h5.H5Constants.*;

import hec.h5.H5File;

import static org.junit.Assert.*;

import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.FixMethodOrder;
import org.junit.Test;
import org.junit.runners.MethodSorters;


@FixMethodOrder(MethodSorters.NAME_ASCENDING)
public class Test_H5File
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
     * Test opening and closing HDF5 file
     *
     * @throws H5InterfaceException HDF5 interface exception
     */
    @Test
    public void test_H5File_open_and_close() throws H5InterfaceException
    {
        String fileName = "LMNRRAS.p03.hdf";
        int fileID = H5File.open(fileName, READ_ONLY);

        assertTrue(fileID > -1);

        H5File.close(fileID);
    }

}
