package hec.h5;

import ncsa.hdf.hdf5lib.HDF5Constants;

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
public class H5Constants
{
    // HDF5 file access flags

    // Existing file is opened with read-only access.
    // If file does not exist, H5Fopen fails.
    public static final int READ_ONLY = HDF5Constants.H5F_ACC_RDONLY;

    // Existing file is opened with read-write access.
    // If file does not exist, H5Fopen fails.
    public static final int READ_WRITE = HDF5Constants.H5F_ACC_RDWR;

    // H5File is truncated upon opening, i.e., if file
    // already exists, file is opened with read-write
    // access and new data overwrites existing data,
    // destroying all prior content. If file does not exist,
    // it is created and opened with read-write access.
    public static final int TRUNCATE = HDF5Constants.H5F_ACC_TRUNC;

    // If file already exists, H5Fcreate fails. If file
    // does not exist, it is created and opened with
    // read-write access.
    public static final int CREATE_AND_READ_WRITE = HDF5Constants.H5F_ACC_EXCL;

    // HDF5 Constants

    // Native int, long, float, and double
    public static final int NATIVE_INT = HDF5Constants.H5T_NATIVE_INT;
    public static final int NATIVE_LONG = HDF5Constants.H5T_NATIVE_LONG;
    public static final int NATIVE_FLOAT = HDF5Constants.H5T_NATIVE_FLOAT;
    public static final int NATIVE_DOUBLE = HDF5Constants.H5T_NATIVE_DOUBLE;

    // Fortran-style string
    public static final int FORTRAN_STRING = HDF5Constants.H5T_FORTRAN_S1;

    // C-style string
    public static final int C_STRING = HDF5Constants.H5T_C_S1;

    // Default property
    public static final int PDEFAULT = HDF5Constants.H5P_DEFAULT;

    // Create dataset
    public static final int CREATE_DATASET = HDF5Constants.H5P_DATASET_CREATE;

    // Select dataset
    public static final int SELECT_SET = HDF5Constants.H5S_SELECT_SET;

    // Specifies unlimited array dimensions
    public static final int UNLIMITED_DIMS = HDF5Constants.H5S_UNLIMITED;

    // The file dataset's dataspace is used for the memory space
    public static final int ALL = HDF5Constants.H5S_ALL;

    /**
     * h5interface Exception Class
     */
    public static class H5InterfaceException extends Exception
    {
        public H5InterfaceException(String message)
        {
            super(message);
        }

        public H5InterfaceException(String message, Throwable exception)
        {
            super(message, exception);
        }

        public H5InterfaceException(Throwable exception)
        {
            super(exception);
        }
    }

}