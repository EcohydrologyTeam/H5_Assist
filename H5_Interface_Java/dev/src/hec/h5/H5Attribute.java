package hec.h5;

import static hec.h5.H5Constants.*;

import ncsa.hdf.hdf5lib.H5;
import ncsa.hdf.hdf5lib.exceptions.HDF5Exception;
import ncsa.hdf.hdf5lib.exceptions.HDF5LibraryException;

/*
 * HDF5 interface library: Attributes
 * This library provides wrapper methods for HDF5 storage and
 * retrieval of HDF5 data for the U.S. Army Corps of Engineers
 * Hydrologic Engineering Center (HEC) and Engineer Research
 * and Development Center Environmental Laboratory (ERDC-EL). 
 * 
 * @author Todd Steissberg
 * @since August 2017
 */
public class H5Attribute
{

    /**
     * Get attribute data type
     *
     * @param attriID H5Attribute identifier
     * @return H5Attribute data type
     * @throws H5InterfaceException HDF5 interface exception
     */
    public static int getDataType(int attriID) throws H5InterfaceException
    {
        try {
            return H5.H5Aget_type(attriID);
        }
        catch (HDF5LibraryException e) {
            throw new H5InterfaceException("Could not get attribute data type", e);
        }
    }

    /**
     * Open attribute
     *
     * @param locID     Location identifier (group or dataset)
     * @param attriName H5Attribute name
     * @return H5Attribute identifier
     * @throws H5InterfaceException HDF5 interface exception
     */
    public static int open(int locID, String attriName) throws H5InterfaceException
    {
        int attriID = -1;
        boolean exists;

        try {
            exists = H5.H5Aexists(locID, attriName);
            if (exists) {
                attriID = H5.H5Aopen(locID, attriName, PDEFAULT);
            }
        }
        catch (NullPointerException e) {
            throw new H5InterfaceException("H5Attribute does not exist", e);
        }
        catch (Exception e) {
            throw new H5InterfaceException("H5Attribute does not exist", e);
        }

        return attriID;
    }

    /**
     * Close attribute
     *
     * @param attriID H5Attribute identifier
     * @throws H5InterfaceException HDF5 interface exception
     */
    public static void close(int attriID) throws H5InterfaceException
    {
        try {
            H5.H5Aclose(attriID);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not close attribute", e);
        }
    }

    /**
     * Create attribute (float, double, int)
     *
     * @param locID     Location identifier
     * @param attriName H5Attribute name
     * @param dataType  Data type identifier
     * @param nvals     Number of values in attribute array
     * @return H5Attribute identifier
     * @throws H5InterfaceException HDF5 interface exception
     */
    public static int create(int locID, String attriName, int dataType, long nvals) throws H5InterfaceException
    {
        int attriID;

        try {
            final int rank = 1;
            long[] dataDims = new long[]{nvals};
            long[] maxDims = new long[]{nvals};
            int dataspaceID = H5.H5Screate_simple(rank, dataDims, maxDims);
            attriID = H5.H5Acreate(locID, attriName, dataType, dataspaceID, PDEFAULT, PDEFAULT);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not create attribute", e);
        }

        return attriID;
    }

    /**
     * Create attribute (string)
     *
     * @param locID     Location identifier
     * @param attriName H5Attribute name
     * @param nvals     Number of values in attribute array
     * @param str_len   H5Attribute string length (length of Fortran string)
     * @return H5Attribute identifier
     * @throws H5InterfaceException HDF5 interface exception
     */
    public static int create_string(int locID, String attriName, long nvals, int str_len) throws H5InterfaceException
    {
        int attriID;

        try {
            int rank = 1;
            int filetypeID = H5.H5Tcopy(FORTRAN_STRING);
            long[] dims = new long[]{1};
            H5.H5Tset_size(filetypeID, str_len);
            H5.H5Tset_size(filetypeID, str_len + 1);
            int dataspaceID = H5.H5Screate_simple(rank, dims, null);
            attriID = H5.H5Acreate(locID, attriName, filetypeID, dataspaceID, PDEFAULT, PDEFAULT);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not create string attribute", e);
        }

        return attriID;
    }

    /**
     * Read scalar attribute (float)
     *
     * @param attriID H5Attribute identifier
     * @return H5Attribute value (float)
     * @throws H5InterfaceException HDF5 interface exception
     */
    public static float read_scalar_float(int attriID) throws H5InterfaceException
    {
        int dataType = getDataType(attriID);
        float[] attriValues = new float[1];
        try {
            H5.H5Aread(attriID, dataType, attriValues);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not read data from attribute", e);
        }
        return attriValues[0];
    }

    /**
     * Read scalar attribute (double)
     *
     * @param attriID H5Attribute identifier
     * @return H5Attribute value (double)
     * @throws H5InterfaceException HDF5 interface exception
     */
    public static double read_scalar_double(int attriID) throws H5InterfaceException
    {
        int dataType = getDataType(attriID);
        double[] attriValues = new double[1];
        try {
            H5.H5Aread(attriID, dataType, attriValues);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not read data from attribute", e);
        }
        return attriValues[0];
    }

    /**
     * Read scalar attribute (int)
     *
     * @param attriID H5Attribute identifier
     * @return H5Attribute value (int)
     * @throws H5InterfaceException HDF5 interface exception
     */
    public static int read_scalar_int(int attriID) throws H5InterfaceException
    {
        int dataType = getDataType(attriID);
        int[] attriValues = new int[1];
        try {
            H5.H5Aread(attriID, dataType, attriValues);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not read data from attribute", e);
        }
        return attriValues[0];
    }

    /**
     * Read scalar attribute (string)
     *
     * @param attriID H5Attribute identifier
     * @return H5Attribute value (string)
     * @throws H5InterfaceException HDF5 interface exception
     */
    public static String read_scalar_string(int attriID) throws H5InterfaceException
    {
        String attriVal;

        try {
            // Get data type and its size
            int fileTypeID = H5.H5Aget_type(attriID);
            int str_len = H5.H5Tget_size(fileTypeID);
            str_len += 1; // Make room for null terminator

            // Allocate space for data
            byte[] dset_data = new byte[str_len];

            // Create memory data type
            int memtypeID = H5.H5Tcopy(C_STRING);
            H5.H5Tset_size(memtypeID, str_len);

            // Read data
            H5.H5Aread(attriID, memtypeID, dset_data);
            byte[] tempbuf = new byte[str_len];
            for (int j = 0; j < str_len; j++) {
                tempbuf[j] = dset_data[j];
            }
            StringBuffer str_data = new StringBuffer(new String(tempbuf).trim());
            attriVal = str_data.toString();
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not read data from attribute", e);
        }

        return attriVal;
    }

    /**
     * Read attribute array (float)
     *
     * @param attriID H5Attribute identifier
     * @param nvals   Number of values in attribute array
     * @return H5Attribute value array (float[])
     * @throws H5InterfaceException HDF5 interface exception
     */
    public static float[] read_1D_array_float(int attriID, int nvals) throws H5InterfaceException
    {
        int dataType = getDataType(attriID);
        float[] attriValues = new float[nvals];

        try {
            H5.H5Aread(attriID, dataType, attriValues);
        }
        catch (HDF5Exception e) {
            throw new H5InterfaceException("Could not read data from attribute", e);
        }

        return attriValues;
    }

    /**
     * Read attribute array (double)
     *
     * @param attriID H5Attribute identifier
     * @param nvals   Number of values in attribute array
     * @return H5Attribute value array (double[])
     * @throws H5InterfaceException HDF5 interface exception
     */
    public static double[] read_1D_array_double(int attriID, int nvals) throws H5InterfaceException
    {
        int dataType = getDataType(attriID);
        double[] attriValues = new double[nvals];

        try {
            H5.H5Aread(attriID, dataType, attriValues);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not read data from attribute", e);
        }

        return attriValues;
    }

    /**
     * Read attribute array (float, double, int)
     *
     * @param attriID H5Attribute identifier
     * @param nvals   Number of values in attribute array
     * @return H5Attribute value array (int[])
     * @throws H5InterfaceException HDF5 interface exception
     */
    public static int[] read_1D_array_int(int attriID, int nvals) throws H5InterfaceException
    {
        int dataType = getDataType(attriID);
        int[] attriValues = new int[nvals];

        try {
            H5.H5Aread(attriID, dataType, attriValues);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not read data from attribute", e);
        }

        return attriValues;
    }

    /**
     * Read attribute array (string)
     *
     * @param attriID H5Attribute identifier
     * @param nvals   Number of values in array
     * @return H5Attribute array (string[])
     * @throws H5InterfaceException HDF5 interface exception
     */
    public static String[] read_1D_array_string(int attriID, int nvals) throws H5InterfaceException
    {
        String[] attriVals = new String[nvals];
        try {

            // Get data type and its size
            final int rank = 1;
            int fileTypeID = H5.H5Aget_type(attriID);
            int str_len = H5.H5Tget_size(fileTypeID);
            str_len += 1; // Make room for null terminator
            int dataspaceID = H5.H5Aget_space(attriID);
            long[] dims = new long[rank];
            long[] maxDims = new long[rank];

            // Get attribute array dimensions
            H5.H5Sget_simple_extent_dims(dataspaceID, dims, maxDims);

            // Allocate space for data
            byte[][] dset_data = new byte[(int) dims[0]][str_len];
            StringBuffer[] str_data = new StringBuffer[(int) dims[0]];

            // Create memory data type
            int memtypeID = H5.H5Tcopy(C_STRING);
            H5.H5Tset_size(memtypeID, str_len);

            // Read data
            H5.H5Aread(attriID, memtypeID, dset_data);
            byte[] tempbuf = new byte[str_len];
            for (int i = 0; i < (int) dims[0]; i++) {
                for (int j = 0; j < str_len; j++) {
                    tempbuf[j] = dset_data[i][j];
                }
                str_data[i] = new StringBuffer(new String(tempbuf).trim());
            }

            for (int i = 0; i < nvals; i++) {
                attriVals[i] = str_data[i].toString();
            }
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not read data from attribute", e);
        }

        return attriVals;
    }

    /**
     * Write scalar attribute (float)
     *
     * @param attriID    H5Attribute identifier
     * @param attriValue H5Attribute value
     * @throws H5InterfaceException HDF5 interface exception
     */
    public static void write_scalar(int attriID, float attriValue) throws H5InterfaceException
    {
        int dataType = getDataType(attriID);
        float[] attriValues = new float[1];
        attriValues[0] = attriValue;
        try {
            H5.H5Awrite(attriID, dataType, attriValues);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not write data to attribute", e);
        }
    }

    /**
     * Write scalar attribute (double)
     *
     * @param attriID    H5Attribute identifier
     * @param attriValue H5Attribute value
     * @throws H5InterfaceException HDF5 interface exception
     */
    public static void write_scalar(int attriID, double attriValue) throws H5InterfaceException
    {
        int dataType = getDataType(attriID);
        double[] attriValues = new double[1];
        attriValues[0] = attriValue;
        try {
            H5.H5Awrite(attriID, dataType, attriValues);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not write data to attribute", e);
        }
    }

    /**
     * Write scalar attribute (int)
     *
     * @param attriID    H5Attribute identifier
     * @param attriValue H5Attribute value
     * @throws H5InterfaceException HDF5 interface exception
     */
    public static void write_scalar(int attriID, int attriValue) throws H5InterfaceException
    {
        int dataType = getDataType(attriID);
        int[] attriValues = new int[1];
        attriValues[0] = attriValue;
        try {
            H5.H5Awrite(attriID, dataType, attriValues);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not write data to attribute", e);
        }
    }

    /**
     * Write scalar attribute (string)
     *
     * @param attriID  H5Attribute identifier
     * @param attriVal H5Attribute value
     * @throws H5InterfaceException HDF5 interface exception
     */
    public static void write_scalar(int attriID, String attriVal) throws H5InterfaceException
    {
        int str_len = attriVal.length();

        try {
            int filetypeID = H5.H5Tcopy(FORTRAN_STRING);
            H5.H5Tset_size(filetypeID, str_len - 1);

            byte[] dset_data = new byte[str_len];
            StringBuffer str_data = new StringBuffer(attriVal);

            int memtypeID = H5.H5Tcopy(C_STRING);
            //int memtypeID = H5.H5Tcopy(H5T_FORTRAN_S1);
            H5.H5Tset_size(memtypeID, str_len);

            for (int i = 0; i < str_len; i++) {
                if (i < str_data.length())
                    dset_data[i] = (byte) str_data.charAt(i);
                else
                    dset_data[i] = 0;
            }
            H5.H5Awrite(attriID, memtypeID, dset_data);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not write data to attribute", e);
        }
    }

    /**
     * Write 1D attribute array (float, double, int)
     *
     * @param attriID     H5Attribute identifier
     * @param attriValues H5Attribute array
     * @param <T>         Generic primitive type (float, double, int)
     * @throws H5InterfaceException HDF5 interface exception
     */
    public static <T> void write_1D_array(int attriID, T attriValues) throws H5InterfaceException
    {
        int dataType = getDataType(attriID);
        try {
            H5.H5Awrite(attriID, dataType, attriValues);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not write data to attribute", e);
        }
    }

}