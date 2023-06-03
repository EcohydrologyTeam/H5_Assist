package hec.h5;

import static hec.h5.H5Constants.*;

import ncsa.hdf.hdf5lib.H5;

import static ncsa.hdf.hdf5lib.HDF5Constants.*;

/*
 * HDF5 interface library: Files
 * This library provides wrapper methods for HDF5 storage and
 * retrieval of HDF5 data for the U.S. Army Corps of Engineers
 * Hydrologic Engineering Center (HEC) and Engineer Research
 * and Development Center Environmental Laboratory (ERDC-EL). 
 * 
 * @author Todd Steissberg
 * @since August 2017
 */
public class H5File
{
    /**
     * Open HDF5 file
     *
     * @param fileName   Name of HDF5 file open
     * @param fileAccess H5File access identifier:
     *                   HDF5Constants.H5F_ACC_RDONLY;
     *                   HDF5Constants.H5F_ACC_RDWR;
     *                   HDF5Constants.H5F_ACC_TRUNC;
     *                   HDF5Constants.H5F_ACC_DEFAULT;
     *                   HDF5Constants.H5F_ACC_EXCL;
     * @return H5File identifier
     * @throws H5InterfaceException HDF5 interface exception
     */
    public static int open(String fileName, int fileAccess) throws H5InterfaceException
    {
        try {
            return H5.H5Fopen(fileName, fileAccess, H5P_DEFAULT);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not open file", e);
        }
    }

    /**
     * Close HDF5 file
     *
     * @param fileID H5File identifier
     * @throws H5InterfaceException HDF5 interface exception
     */
    public static void close(int fileID) throws H5InterfaceException
    {
        try {
            H5.H5Fclose(fileID);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not open file", e);
        }
    }

    /**
     * Create HDF5 file
     *
     * @param fileName       Name of HDF5 file to create
     * @param fileAccessType H5File access type identifier
     * @return H5File identifier
     * @throws H5InterfaceException HDF5 interface exception
     */
    public static int create(String fileName, int fileAccessType) throws H5InterfaceException
    {
        int fileID;

        try {
            fileID = H5.H5Fcreate(fileName, fileAccessType, H5P_DEFAULT, H5P_DEFAULT);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not create file", e);
        }

        return fileID;
    }

}
