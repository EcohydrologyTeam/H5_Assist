package hec.h5;

import static hec.h5.H5Constants.*;

import ncsa.hdf.hdf5lib.H5;

/*
 * HDF5 interface library: Groups
 * This library provides wrapper methods for HDF5 storage and
 * retrieval of HDF5 data for the U.S. Army Corps of Engineers
 * Hydrologic Engineering Center (HEC) and Engineer Research
 * and Development Center Environmental Laboratory (ERDC-EL). 
 * 
 * @author Todd Steissberg
 * @since August 2017
 */
public class H5Group
{
    /**
     * @param locID     Location identifier (file or group)
     * @param groupName H5Group name
     * @return H5Group identifier (int)
     * @throws H5InterfaceException HDF5 interface exception
     */
    public static int open(int locID, String groupName) throws H5InterfaceException
    {
        int gapl_id = PDEFAULT; // H5Group access property list identifier

        try {
            return H5.H5Gopen(locID, groupName, gapl_id);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not open group", e);
        }
    }

    /**
     * Close group
     *
     * @param groupID H5Group identifier
     * @throws H5InterfaceException HDF5 interface exception
     */
    public static void close(int groupID) throws H5InterfaceException
    {
        try {
            H5.H5Gclose(groupID);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not open group", e);
        }
    }

    /**
     * Create a group in an existing HDF5 file or group
     *
     * @param locID     H5File or group identifier, where group is expected to be located
     * @param groupName H5Group name
     * @return H5Group identifier (int)
     * @throws H5InterfaceException HDF5 interface exception
     */
    public static int create(int locID, String groupName) throws H5InterfaceException
    {
        int groupID;

        try {
            groupID = H5.H5Gcreate(locID, groupName, PDEFAULT, PDEFAULT, PDEFAULT);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not open group", e);
        }
        return groupID;
    }
}
