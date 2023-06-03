package hec.h5;

import static hec.h5.H5Constants.*;

import ncsa.hdf.hdf5lib.H5;


/**
 * HDF5 interface library: Datasets
 * This library provides wrapper methods for HDF5 storage and
 * retrieval of HDF5 data for the U.S. Army Corps of Engineers
 * Hydrologic Engineering Center (HEC) and Engineer Research
 * and Development Center Environmental Laboratory (ERDC-EL).
 *
 * @author Todd Steissberg
 * @since August 2017
 */
public class H5Dataset
{
    private static final long serialVersionUID = 1L;

    /**
     * Chunking and compression parameters
     */
    private static final int COMPRESSION_LEVEL = 1;     // Set compression level for ZLIB deflate. Level 1
    private static final int NBYTES_PER_CHUNK = 1024 * 1024; // Use 1 MB chunk size
    private static final int NUM_CHUNKS_IN_SPACE = 1;   // Number of chunks along an HDF5 row (spatial dimension, cross-sections)
    private static final int NUM_CHUNKS_IN_TIME = 1;    // Number of chunks along an HDF5 column (time dimension)
    private static final int MIN_VALUES_IN_CHUNK = 100; // Set minimum HDF chunk size (number of values?)

    /**
     * Container for current and maximum dimensions of a dataset
     */
    static class Dimensions
    {
        long[] currentDims;
        long[] maxDims;

        Dimensions(int rank)
        {
            this.currentDims = new long[rank];
            this.maxDims = new long[rank];
        }

        Dimensions(long[] currentDims, long[] maxDims)
        {
            this.currentDims = currentDims;
            this.maxDims = maxDims;
        }

        @Override
        public String toString()
        {
            String str1 = "Current dimensions: [";
            String str2 = "Maximum dimensions: [";
            for (int i = 0; i < currentDims.length; i++) {
                str1 += currentDims[i] + ", ";
                str2 += maxDims[i] + ", ";
            }

            str1 = str1.substring(0, str1.length() - 2) + "]";
            str2 = str2.substring(0, str2.length() - 2) + "]";

            return str1 + "\n" + str2;
        }
    }

    /**
     * Check if dataset exists in a group, specified by group name
     *
     * @param locID       H5File or group identifier, where dataset is expected to be located
     * @param groupName   H5Group name
     * @param datasetName H5Dataset name
     * @return Existence of dataset (true or false)
     * @throws H5InterfaceException HDF5 dataset exception
     */
    public static boolean datasetExistsInGroupName(int locID, String groupName, String datasetName)
            throws H5InterfaceException
    {
        boolean isOK;

        try {
            // Check if group exists in location
            isOK = H5.H5Lexists(locID, groupName, PDEFAULT);
            if (isOK) {
                // Check if dataset exists in group
                int groupID = H5.H5Gopen(locID, groupName, READ_ONLY);
                isOK = H5.H5Lexists(groupID, datasetName, PDEFAULT);
            }
        }
        catch (Exception e) {
            throw new H5InterfaceException("Error checking if dataset exists", e);
        }

        return isOK;
    }

    /**
     * Check if a dataset exists in an open group, specified by group ID
     *
     * @param groupID     H5Group identifier, where dataset is expected to be located
     * @param datasetName H5Dataset name
     * @return Existence of dataset (true or false)
     * @throws H5InterfaceException HDF5 dataset exception
     */
    public static boolean datasetExistsInGroupID(int groupID, String datasetName) throws H5InterfaceException
    {
        try {
            return H5.H5Lexists(groupID, datasetName, PDEFAULT);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Error checking if dataset exists", e);
        }
    }

    /**
     * Get current and maximum dataset dimensions
     *
     * @param datasetID H5Dataset identifier
     * @param rank      H5Dataset rank
     * @return Object containing current and maximum dataset dimensions
     * @throws H5InterfaceException HDF5 dataset exception
     */
    public static Dimensions getDimensions(int datasetID, int rank) throws H5InterfaceException
    {
        Dimensions dims = new Dimensions(rank);

        try {
            int dataspaceID = H5.H5Dget_space(datasetID);
            H5.H5Sget_simple_extent_dims(dataspaceID, dims.currentDims, dims.maxDims);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not get dataset dimensions", e);
        }

        return dims;
    }

    /**
     * Set the dataset extents
     *
     * @param datasetID H5Dataset identifier
     * @param dataDims  Data dimensions
     * @throws H5InterfaceException HDF5 dataset exception
     * @notes This method will first check to make sure the dataset is extensible
     */
    private static void setExtents(int datasetID, long[] dataDims, int rank) throws H5InterfaceException
    {
        boolean extensible = false;

        try {
            Dimensions datasetDims = getDimensions(datasetID, rank);
            for (int i = 0; i < rank; i++) {
                if (datasetDims.maxDims[i] == -1) {
                    extensible = true;
                    datasetDims.currentDims[i] = Math.max(datasetDims.currentDims[i], dataDims[i]);
                }
            }

            if (extensible) {
                H5.H5Dset_extent(datasetID, datasetDims.currentDims);
            }
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not set dataset extents", e);
        }
    }

    /**
     * The Hyperspace class is a container for
     * returning the memory space and data space identifiers
     * and the dimensions of the subset of a dataset.
     */
    static class Hyperspace
    {
        int memspaceID;
        int dataspaceID;
        long[] subsetDims;
    }

    /**
     * Open dataset
     *
     * @param groupID     H5Group identifier
     * @param datasetName H5Dataset name
     * @return H5Dataset identifier (int)
     * @throws H5InterfaceException HDF5 dataset exception
     */
    public static int open(int groupID, String datasetName) throws H5InterfaceException
    {
        try {
            return H5.H5Dopen(groupID, datasetName, PDEFAULT);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not open dataset", e);
        }
    }

    /**
     * Close dataset
     *
     * @param datasetID H5Dataset identifier
     * @throws H5InterfaceException HDF5 dataset exception
     */
    public static void close(int datasetID) throws H5InterfaceException
    {
        try {
            H5.H5Dclose(datasetID);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not close dataset", e);
        }
    }


    /**
     * Helper function: get 2D hyperslab and memory space
     *
     * @param datasetID H5Dataset identifier
     * @param startrow  Index of first row of subset
     * @param startcol  Index of first column of subset
     * @param nrows_hdf Number of rows in subset
     * @param ncols_hdf Number of columns in subset
     * @throws H5InterfaceException HDF5 dataset exception
     */
    private static Hyperspace getHyperspace2D(int datasetID, long startrow,
                                              long startcol, long nrows_hdf, long ncols_hdf) throws H5InterfaceException
    {
        int rank = 2;
        long[] datasetDims = new long[rank];
        long[] maxDims = new long[rank];
        long[] start, stride, count, block;
        Hyperspace hyperspace = new Hyperspace();
        int dataspaceID;
        int memspaceID;

        try {
            // Get data space from dataset
            dataspaceID = H5.H5Dget_space(datasetID);

            // Get data space dimensions
            H5.H5Sget_simple_extent_dims(dataspaceID, datasetDims, maxDims);

            // If subset extends beyond the dataset boundaries, trim the excess
            nrows_hdf = Math.min(nrows_hdf, datasetDims[0]);
            ncols_hdf = Math.min(ncols_hdf, datasetDims[1]);

            // Set subset dimensions and offset (location of first point of subset in the dataset)
            start = new long[]{startrow, startcol}; // offset
            stride = new long[]{1, 1};
            count = new long[]{nrows_hdf, ncols_hdf}; // subset dimensions
            block = new long[]{1, 1};
        }
        catch (Exception e) {
            throw new H5InterfaceException("Data space error", e);
        }

        try {
            // Create simple memory space
            memspaceID = H5.H5Screate_simple(rank, count, maxDims);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Error creating memory space", e);
        }

        try {
            // Select hyperslab
            H5.H5Sselect_hyperslab(dataspaceID, SELECT_SET, start, stride, count, block);

            hyperspace.memspaceID = memspaceID;
            hyperspace.dataspaceID = dataspaceID;
            hyperspace.subsetDims = count;
        }
        catch (Exception e) {
            throw new H5InterfaceException("Error selecting hyperslab", e);
        }

        return hyperspace;
    }

    /**
     * Helper function: get 1D hyperslab and memory space
     *
     * @param datasetID  H5Dataset identifier
     * @param startindex Index of first row of subset
     * @param nvals      Number of values in 1D subset
     * @throws H5InterfaceException HDF5 dataset exception
     */
    private static Hyperspace getHyperspace1D(int datasetID, long startindex, long nvals) throws H5InterfaceException
    {
        int rank = 1;
        int dataspaceID, memspaceID;
        Hyperspace hyperspace = new Hyperspace();
        long[] datasetDims, maxDims;
        long[] start, stride, count, block;

        try {
            // Get data space from dataset
            dataspaceID = H5.H5Dget_space(datasetID);

            // Get data space dimensions
            datasetDims = new long[rank];
            maxDims = new long[rank];
            H5.H5Sget_simple_extent_dims(dataspaceID, datasetDims, maxDims);

            // Set subset dimensions and offset (location of first point of subset in the dataset)
            // If subset extends beyond the dataset boundaries, trim the excess
            start = new long[]{startindex}; // offset
            stride = new long[]{1};
            count = new long[rank]; // subset dimensions
            count[0] = Math.min(nvals, datasetDims[0]);
            block = new long[]{1};
        }
        catch (Exception e) {
            throw new H5InterfaceException("Data space error", e);
        }

        try {
            // Create simple memory space
            memspaceID = H5.H5Screate_simple(rank, count, maxDims);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Error creating memory space", e);
        }
        try {
            // Select hyperslab
            H5.H5Sselect_hyperslab(dataspaceID, SELECT_SET, start, stride, count, block);

            hyperspace.memspaceID = memspaceID;
            hyperspace.dataspaceID = dataspaceID;
            hyperspace.subsetDims = count;
        }
        catch (Exception e) {
            throw new H5InterfaceException("Error selecting hyperslab", e);
        }

        return hyperspace;
    }

    /**
     * Read 2D array (float)
     * <p>
     * This method can read an entire dataset or a subset,
     * including row and column vectors.
     * </p>
     * <p>
     * Example Settings:
     * <ul>
     * <li>Entire dataset, 100 rows x 50 columns:<br>
     * startrow = 0, startcol = 0, nrows_hdf = 100, ncols_hdf = 50</li>
     * <li>Subset of dataset at starting at (row, col) = (5,7), 30 rows x 20 columns:<br>
     * startrow = 5, startcol = 7, nrows_hdf = 30, ncols_hdf = 20</li>
     * <li>Row vector at index 5, 100 values (e.g. write all cross-section data for a time step):<br>
     * startrow = 5, startcol = 0, nrows_hdf = 1, ncols_hdf = 100</li>
     * <li>Column vector at index 5, 100 values (e.g., write time series for a cross-section):<br>
     * startrow = 0, startcol = 5, nrows_hdf = 100, ncols_hdf = 1</li>
     * </ul>
     *
     * @param datasetID H5Dataset identifier
     * @param startrow  Index of first row of subset
     * @param startcol  Index of first column of subset
     * @param nrows_hdf Number of rows in subset
     * @param ncols_hdf Number of columns in subset
     * @return 2D data array (float[][])
     * @throws H5InterfaceException HDF5 dataset exception
     */
    public static float[][] read_2D_array_float(int datasetID, long startrow, long startcol,
                                                long nrows_hdf, long ncols_hdf) throws H5InterfaceException
    {
        float[][] outArray;

        try {
            // Get dataset type. Returns data type identifier.
            int dataType = H5.H5Dget_type(datasetID);

            // Select hyperslab and allocate memory space
            Hyperspace hs = getHyperspace2D(datasetID, startrow, startcol, nrows_hdf, ncols_hdf);

            // Read entire 2D dataset
            long nrows = hs.subsetDims[0];
            long ncols = hs.subsetDims[1];
            // Note: Calling H5Dread() with a 2D array (handled as an Object) is about 70x slower
            // than calling with a 1D array and reshaping it to a 2D array.
            //float[][] hdfArray = new float[(int) nrows][(int) ncols];
            //H5.H5Dread(datasetID, dataType, hs.memspaceID, hs.dataspaceID, PDEFAULT, hdfArray);
            float[] hdfArray = new float[(int) (nrows * ncols)];
            H5.H5Dread_float(datasetID, dataType, hs.memspaceID, hs.dataspaceID, PDEFAULT, hdfArray);

            // Close resources
            H5.H5Sclose(hs.memspaceID);

            outArray = new float[(int) nrows][(int) ncols];
            int outIndex = 0;
            for (int i = 0; i < nrows; i++) {
                for (int j = 0; j < ncols; j++) {
                    outArray[i][j] = hdfArray[outIndex];
                    outIndex++;
                }
            }
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not read data from dataset", e);
        }

        return outArray;
    }

    /**
     * Read 2D array (double)
     * <p>
     * This method can read an entire dataset or a subset,
     * including row and column vectors.
     * </p>
     *
     * @param datasetID H5Dataset identifier
     * @param startrow  Index of first row of subset
     * @param startcol  Index of first column of subset
     * @param nrows_hdf Number of rows in subset
     * @param ncols_hdf Number of columns in subset
     * @return 2D data array (double[][])
     * @throws H5InterfaceException HDF5 dataset exception
     * @see #read_2D_array_float(int, long, long, long, long)
     */
    public static double[][] read_2D_array_double(int datasetID, long startrow, long startcol,
                                                  long nrows_hdf, long ncols_hdf) throws H5InterfaceException
    {
        double[][] outArray;

        try {
            // Get dataset type. Returns data type identifier.
            int dataType = H5.H5Dget_type(datasetID);

            // Select hyperslab and allocate memory space
            Hyperspace hs = getHyperspace2D(datasetID, startrow, startcol, nrows_hdf, ncols_hdf);

            // Read entire 2D dataset
            long nrows = hs.subsetDims[0];
            long ncols = hs.subsetDims[1];
            float[] hdfArray = new float[(int) (nrows * ncols)];
            H5.H5Dread(datasetID, dataType, hs.memspaceID, hs.dataspaceID, PDEFAULT, hdfArray);

            // Close resources
            H5.H5Sclose(hs.memspaceID);

            outArray = new double[(int) nrows][(int) ncols];
            int outIndex = 0;
            for (int i = 0; i < nrows; i++) {
                for (int j = 0; j < ncols; j++) {
                    outArray[i][j] = hdfArray[outIndex];
                    outIndex++;
                }
            }
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not read data from dataset", e);
        }

        return outArray;
    }

    /**
     * Read 2D array (int)
     * <p>
     * This method can read an entire dataset or a subset,
     * including row and column vectors.
     * </p>
     *
     * @param datasetID H5Dataset identifier
     * @param startrow  Index of first row of subset
     * @param startcol  Index of first column of subset
     * @param nrows_hdf Number of rows in subset
     * @param ncols_hdf Number of columns in subset
     * @return 2D data array (int[][])
     * @throws H5InterfaceException HDF5 dataset exception
     * @see #read_2D_array_float(int, long, long, long, long)
     */
    public static int[][] read_2D_array_int(int datasetID, long startrow, long startcol,
                                            long nrows_hdf, long ncols_hdf) throws H5InterfaceException
    {
        int[][] outArray;

        try {
            // Get dataset type. Returns data type identifier.
            int dataType = H5.H5Dget_type(datasetID);

            // Select hyperslab and allocate memory space
            Hyperspace hs = getHyperspace2D(datasetID, startrow, startcol, nrows_hdf, ncols_hdf);

            // Read entire 2D dataset
            long nrows = hs.subsetDims[0];
            long ncols = hs.subsetDims[1];
            int[] hdfArray = new int[(int) (nrows * ncols)];
            H5.H5Dread_int(datasetID, dataType, hs.memspaceID, hs.dataspaceID, PDEFAULT, hdfArray);

            // Close resources
            H5.H5Sclose(hs.memspaceID);

            outArray = new int[(int) nrows][(int) ncols];
            int outIndex = 0;
            for (int i = 0; i < nrows; i++) {
                for (int j = 0; j < ncols; j++) {
                    outArray[i][j] = hdfArray[outIndex];
                    outIndex++;
                }
            }
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not read data from dataset", e);
        }

        return outArray;
    }

    /**
     * Create a 2D HDF5 dataset: numerical type
     * <p>
     * Note: compression and chunking are optional
     *
     * @param locID         H5File or group identifier, where dataset will be created
     * @param datasetName   Name of dataset to create
     * @param nrows_hdf     Number of rows in HDF5 dataset (C convention: [row, column])
     * @param ncols_hdf     Number of columns in HDF5 dataset (C convention: [row, column])
     * @param datatypeID    Data type identifier for dataset
     * @param chunk_in_time If true, chunk in the time dimension, i.e., chunks will consist of
     *                      one or more HDF5 dataset rows. If false, chunk in the space dimension.
     * @param kind_nbytes   Number of bytes of floating point or integer data types
     * @param isCompressed  If true, chunk and compress the dataset and set one dimension to unlimited.
     *                      If chunk_in_time is also true, the current and maximum row dimensions will
     *                      be set to create a dataset with only one row and an unlimited number of rows.
     *                      If chunk_time is false, the current and maximum column dimensions will be set
     *                      to create a dataset with only one column and an unlimited number of columns.
     * @return H5Dataset identifier (integer)
     * @throws H5InterfaceException HDF5 dataset exception
     */
    public static int create_2D_dataset(int locID, String datasetName, long nrows_hdf, long ncols_hdf, int datatypeID,
                                        boolean chunk_in_time, int kind_nbytes, boolean isCompressed)
            throws H5InterfaceException
    {
        int datasetID;
        final int rank = 2;                       // Rank of the data set
        long[] chunkDims = new long[rank];        // Chunk dimensions
        int num_max;                              // Used to set maximum dataset size

        // Set default dimensions
        long[] datasetDims = {nrows_hdf, ncols_hdf}; // Current dataset dimensions
        long[] maxDims = {nrows_hdf, ncols_hdf};     // Maximum dataset dimensions

        // Compression and chunking constants (using same values as in Steve Piper's mod_hdf_output.for)
        int nvalues_per_chunk;                    // HDF5 chunk size (number of values in chunk)

        try {
            // Create output property list. Returns dataset creation
            // property list identifier. This corresponds to plist_id
            // in the Fortran version.
            int dcpl_id = H5.H5Pcreate(CREATE_DATASET);

            if (isCompressed) {
                // Set HDF chunk size in number of values (1MB / nbytes_per_value
                nvalues_per_chunk = NBYTES_PER_CHUNK / kind_nbytes;

                // Fudge down so rounding does not go over. For 4 byte data: 1,000,000 / 4 - 44 = 262100, which is Steve's value.
                // (1/4 MB = 256 K = 262144 bytes)
                nvalues_per_chunk = (int) ((nvalues_per_chunk / 100.0) * 100);

                // Set output dimensions (chunkDims and maxDims) for compression and chunking
                // Adapted by Todd Steissberg from Steve Piper's code
                if (chunk_in_time) {
                    // ----- Chunk in time dimension (chunks will consist of one or more sets HDF5 rows) -----

                    // Set chunk row length
                    chunkDims[1] = ncols_hdf / NUM_CHUNKS_IN_SPACE;
                    chunkDims[1] = Math.max(chunkDims[1], MIN_VALUES_IN_CHUNK); // Ensure a minimum number of values along a chunk row
                    chunkDims[1] = Math.min(chunkDims[1], ncols_hdf);              // Ensure number of values in chunk row does not exceed number of columns
                    num_max = Math.max(nvalues_per_chunk, MIN_VALUES_IN_CHUNK);  // Ensure minimum number of values

                    // Set chunk column length
                    chunkDims[0] = num_max / chunkDims[1];
                    chunkDims[0] = Math.max(chunkDims[0], 1);
                    chunkDims[0] = Math.min(chunkDims[0], nrows_hdf);

                    // Set initial dataset dimensions to one row, with an unlimited number of rows
                    datasetDims[0] = 1;
                    // datasetDims[0] = nrows_hdf;
                    maxDims[0] = UNLIMITED_DIMS;
                }
                else {
                    // ----- Chunk in space dimension (chunks will consist of one or more sets HDF5 columns) -----

                    // Set chunk column length
                    chunkDims[1] = ncols_hdf / NUM_CHUNKS_IN_TIME;
                    chunkDims[1] = Math.max(chunkDims[1], MIN_VALUES_IN_CHUNK);  // Ensure a minimum number of values along a chunk row
                    chunkDims[1] = Math.min(chunkDims[1], ncols_hdf);               // Ensure number of values in chunk row does not exceed number of columns
                    num_max = Math.max(nvalues_per_chunk, MIN_VALUES_IN_CHUNK);   // Ensure minimum number of values

                    // Set chunk row length
                    if (chunkDims[1] != 0) {
                        chunkDims[0] = num_max / chunkDims[1];
                    }
                    else {

                        chunkDims[0] = 1;
                    }
                    chunkDims[0] = Math.max(chunkDims[0], 1);
                    chunkDims[0] = Math.min(chunkDims[0], nrows_hdf);

                    // Set initial dataset dimensions to one column, with an unlimited number of columns
                    datasetDims[1] = 1;
                    // datasetDims[1] = ncols_hdf;
                    maxDims[1] = UNLIMITED_DIMS;
                }
                // Set dataset chunking. H5Dataset must be chunked for compression.
                H5.H5Pset_chunk(dcpl_id, rank, chunkDims);

                // Set ZLIB / DEFLATE Compression
                H5.H5Pset_deflate(dcpl_id, COMPRESSION_LEVEL);
            }
            else {
                // Set current and max dimensions to the specified numbers of rows and columns
                datasetDims[0] = nrows_hdf;
                datasetDims[1] = ncols_hdf;
                maxDims[0] = nrows_hdf;
                maxDims[1] = ncols_hdf;
            }

            // Create simple data space. Returns data (memory) space identifier.
            int dataspace_id = H5.H5Screate_simple(rank, datasetDims, maxDims);

            // Create data set. Returns dataset identifier.
            int lcpl_id = PDEFAULT; // Link creation property list identifier
            int dapl_id = PDEFAULT; // H5Dataset access property list identifier
            datasetID = H5.H5Dcreate(locID, datasetName, datatypeID, dataspace_id, lcpl_id, dcpl_id, dapl_id);

            // Release resources
            H5.H5Sclose(dataspace_id);
            H5.H5Pclose(dcpl_id);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not create dataset", e);
        }

        return datasetID;
    }

    /**
     * Create a 1D HDF5 dataset: numerical type
     * <p>
     * Note: compression and chunking are optional
     *
     * @param locID        H5File or group identifier, where dataset will be created
     * @param datasetName  Name of dataset to create
     * @param nvals        Number of values in HDF5 dataset
     * @param datatypeID   Data type identifier for dataset
     * @param kind_nbytes  Number of bytes of floating point or integer data types
     * @param isCompressed If true, chunk and compress the dataset. Set the current dataset dimensions
     *                     to one value and the maximum dimensions to unlimited.
     * @return H5Dataset identifier (integer)
     * @throws H5InterfaceException HDF5 dataset exception
     */
    public static int create_1D_dataset(int locID, String datasetName, long nvals, int datatypeID,
                                        int kind_nbytes, boolean isCompressed) throws H5InterfaceException
    {
        int datasetID;
        final int rank = 1;                       // Rank of the data set
        long[] maxDims = new long[rank];          // Maximum dataset dimensions (output)
        long[] datasetDims = new long[rank];      // H5Dataset dimensions (output)
        long[] chunkDims = new long[rank];        // Chunk dimensions

        // Compression and chunking constants (using same values as in Steve Piper's mod_hdf_output.for)
        int nvalues_per_chunk;                    // HDF5 chunk size (number of values in chunk)

        try {
            // Create output property list. Returns dataset creation
            // property list identifier. This corresponds to plist_id
            // in the Fortran version.
            int dcpl_id = H5.H5Pcreate(CREATE_DATASET);

            if (isCompressed) {
                // Set output dimensions (chunkDims and maxDims) for compression and chunking
                // Adapted by Todd Steissberg from Steve Piper's code

                // Set HDF chunk size in number of values (1MB / nbytes_per_value
                nvalues_per_chunk = NBYTES_PER_CHUNK / kind_nbytes;

                // Fudge down so rounding does not go over. For 4 byte data: 1,000,000 / 4 - 44 = 262100, which is Steve's value.
                // (1/4 MB = 256 K = 262144 bytes)
                nvalues_per_chunk = (int) ((nvalues_per_chunk / 100.0) * 100);

                // Set chunk row length
                chunkDims[0] = nvals / nvalues_per_chunk;
                chunkDims[0] = Math.max(chunkDims[0], MIN_VALUES_IN_CHUNK); // Ensure a minimum number of values along a chunk row
                chunkDims[0] = Math.min(chunkDims[0], nvals);               // Ensure number of values in chunk row does not exceed number of columns

                // Set initial dataset dimensions to one row, with an unlimited number of rows
                datasetDims[0] = 1;
                // datasetDims[0] = nvals;
                maxDims[0] = UNLIMITED_DIMS;

                // Set dataset chunking. H5Dataset must be chunked for compression.
                H5.H5Pset_chunk(dcpl_id, rank, chunkDims);

                // Set ZLIB / DEFLATE Compression
                H5.H5Pset_deflate(dcpl_id, COMPRESSION_LEVEL);
            }
            else {
                // Set current and max dimensions to the specified number of values
                datasetDims[0] = nvals;
                maxDims[0] = nvals;
            }

            // Create simple data space. Returns data (memory) space identifier.
            int dataspace_id = H5.H5Screate_simple(rank, datasetDims, maxDims);

            // Create data set. Return dataset identifier.
            int lcpl_id = PDEFAULT; // Link creation property list identifier
            int dapl_id = PDEFAULT; // H5Dataset access property list identifier
            datasetID = H5.H5Dcreate(locID, datasetName, datatypeID, dataspace_id, lcpl_id, dcpl_id, dapl_id);

            // Release resources
            H5.H5Sclose(dataspace_id);
            H5.H5Pclose(dcpl_id);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not create dataset", e);
        }

        return datasetID;
    }

    /**
     * Create a 1D HDF5 dataset: string type
     * <p>
     * Note: compression and chunking are optional
     *
     * @param locID        H5File or group identifier, where dataset will be created
     * @param datasetName  Name of dataset to create
     * @param nvals        Number of values in HDF5 dataset
     * @param str_len      H5Dataset string length
     * @param isCompressed If true, chunk and compress the dataset. Set the current dataset dimensions
     *                     to one value and the maximum dimensions to unlimited.
     * @return H5Dataset identifier (integer)
     * @throws H5InterfaceException HDF5 dataset exception
     */
    public static int create_1D_dataset_string(int locID, String datasetName, long nvals, int str_len,
                                               boolean isCompressed) throws H5InterfaceException
    {
        int datasetID;
        final int rank = 1;                       // Rank of the data set
        long[] datasetDims = new long[rank];      // Current dataset dimensions
        long[] maxDims = new long[rank];          // Maximum dataset dimensions

        try {
            // Create the memory data type. Return data type identifier.
            int datatypeID = H5.H5Tcopy(C_STRING); // H5T_FORTRAN_S1 did not work

            // Set the memory size using the string length
            H5.H5Tset_size(datatypeID, str_len);

            // Create output property list. Return dataset creation property list identifier
            // (this is plist_id in the Fortran version)
            int dcpl_id = H5.H5Pcreate(CREATE_DATASET);

            if (isCompressed) {
                // Set output dimensions (chunkDims and maxDims) for compression and chunking
                // Adapted by Todd Steissberg from Steve Piper's code

                // Set HDF chunk size in bytes
                int kind_nbytes = 1; // 1 character == 1 byte
                int nbytes_per_string = str_len * kind_nbytes;
                if (nbytes_per_string % 2 == 0) {
                    nbytes_per_string += 1;
                }

                // HDF5 chunk size (number of values in chunk)
                int nvalues_per_chunk = NBYTES_PER_CHUNK / nbytes_per_string;

                // Fudge down so rounding does not go over. For 4 byte data: 1,000,000 / 4 - 44 = 262100, which is Steve's value.
                // (1/4 MB = 256 K = 262144 bytes)
                nvalues_per_chunk = (int) ((nvalues_per_chunk / 100.0) * 100);

                // Set chunk dimensions
                long[] chunkDims = new long[]{nvalues_per_chunk}; // Chunk dimensions
                chunkDims[0] = Math.max(chunkDims[0], MIN_VALUES_IN_CHUNK); // Ensure a minimum number of values along a chunk row
                chunkDims[0] = Math.min(chunkDims[0], nvals); // Ensure number of values in chunk row does not exceed number of columns

                // Set initial dataset dimensions to one row, with an unlimited number of rows
                datasetDims[0] = 1;
                maxDims[0] = UNLIMITED_DIMS;

                // Set dataset chunking. H5Dataset must be chunked for compression.
                H5.H5Pset_chunk(dcpl_id, rank, chunkDims);

                // Set ZLIB / DEFLATE Compression
                H5.H5Pset_deflate(dcpl_id, COMPRESSION_LEVEL);
            }
            else {
                // Set current and max dimensions to the specified number of values
                datasetDims[0] = nvals;
                maxDims[0] = nvals;
            }

            // Create simple data space. Return data (memory) space identifier.
            int dataspaceID = H5.H5Screate_simple(rank, datasetDims, maxDims);

            // Create data set. Return dataset identifier.
            int lcpl_id = PDEFAULT;  // Link creation property list identifier
            int dapl_id = PDEFAULT;  // H5Dataset access property list identifier
            datasetID = H5.H5Dcreate(locID, datasetName, datatypeID, dataspaceID, lcpl_id, dcpl_id, dapl_id);

            // Release resources
            H5.H5Pclose(dcpl_id);
            H5.H5Tclose(datatypeID);
            H5.H5Sclose(dataspaceID);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not create dataset", e);
        }

        return datasetID;
    }

    /**
     * Read 1D array (float)
     * <p>
     * This method can read an entire dataset or a subset.
     * </p>
     * <p>
     * Example Settings:
     * <ul>
     * <li>Entire dataset, 100 values:<br>
     * startidx = 0, nvals = 100</li>
     * <li>Subset of dataset at starting at index = 5, 30 values:<br>
     * startidx = 5, nvals = 30</li>
     * </ul>
     *
     * @param datasetID H5Dataset identifier
     * @param startidx  Index of first row of subset
     * @param nvals     Number of values in 1D subset
     * @return 1D data array (float[])
     * @throws H5InterfaceException HDF5 dataset exception
     */
    public static float[] read_1D_array_float(int datasetID, long startidx, long nvals) throws H5InterfaceException
    {
        float[] hdfArray;

        try {
            // Get dataset type
            int dataType = H5.H5Dget_type(datasetID);

            // Select hyperslab and allocate memory space
            Hyperspace hs = getHyperspace1D(datasetID, startidx, nvals);

            // Read entire 2D dataset
            nvals = hs.subsetDims[0];
            hdfArray = new float[(int) (nvals)];
            H5.H5Dread_float(datasetID, dataType, hs.memspaceID, hs.dataspaceID, PDEFAULT, hdfArray);

            // Close resources
            H5.H5Sclose(hs.memspaceID);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not read data from dataset", e);
        }

        return hdfArray;
    }

    /**
     * Read 1D array (double)
     * <p>
     * This method can read an entire dataset or a subset.
     * </p>
     *
     * @param datasetID H5Dataset identifier
     * @param startidx  Index of first row of subset
     * @param nvals     Number of values in 1D subset
     * @return 1D data array (double[])
     * @throws H5InterfaceException HDF5 dataset exception
     * @see #read_1D_array_float(int, long, long)
     */
    public static double[] read_1D_array_double(int datasetID, long startidx, long nvals) throws H5InterfaceException
    {
        double[] hdfArray;

        try {
            // Get dataset type
            int dataType = H5.H5Dget_type(datasetID);

            // Select hyperslab and allocate memory space
            Hyperspace hs = getHyperspace1D(datasetID, startidx, nvals);

            // Read entire 1D dataset
            nvals = hs.subsetDims[0];
            hdfArray = new double[(int) (nvals)];
            H5.H5Dread_double(datasetID, dataType, hs.memspaceID, hs.dataspaceID, PDEFAULT, hdfArray);

            // Close resources
            H5.H5Sclose(hs.memspaceID);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not read data from dataset", e);
        }

        return hdfArray;
    }

    /**
     * Read 1D array (int)
     * <p>
     * This method can read an entire dataset or a subset.
     * </p>
     *
     * @param datasetID H5Dataset identifier
     * @param startidx  Index of first row of subset
     * @param nvals     Number of values in 1D subset
     * @return 1D data array (int[])
     * @throws H5InterfaceException HDF5 dataset exception
     * @see #read_1D_array_float(int, long, long)
     */
    public static int[] read_1D_array_int(int datasetID, long startidx, long nvals) throws H5InterfaceException
    {
        int[] hdfArray;

        try {
            // Get dataset type
            int dataType = H5.H5Dget_type(datasetID);

            // Select hyperslab and allocate memory space
            Hyperspace hs = getHyperspace1D(datasetID, startidx, nvals);

            // Read entire 1D dataset
            nvals = hs.subsetDims[0];
            hdfArray = new int[(int) (nvals)];
            H5.H5Dread_int(datasetID, dataType, hs.memspaceID, hs.dataspaceID, PDEFAULT, hdfArray);

            // Close resources
            H5.H5Sclose(hs.memspaceID);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not read data from dataset", e);
        }

        return hdfArray;
    }

    /**
     * Read 1D array (string)
     *
     * @param datasetID H5Dataset identifier
     * @param startidx  Index of first row of subset
     * @param nvals     Number of values in 1D subset
     * @param str_len   String length of dataset
     * @return 1D data array (int[])
     * @throws H5InterfaceException HDF5 dataset exception
     * @see #read_1D_array_float(int, long, long)
     */
    public static String[] read_1D_array_string(int datasetID, long startidx, long nvals, int str_len)
            throws H5InterfaceException
    {
        final int rank = 1; // H5Dataset rank
        String[] hdfArray;

        try {
            // Get dataset type
            int dataType = H5.H5Dget_type(datasetID);

            // Get the size of the data type, i.e.,
            // the string length of the stored dataset
            int dataTypeSize = H5.H5Tget_size(dataType);

            // Make sure the declared string length is long enough
            if (dataTypeSize > str_len) {
                System.out.println("Error: Specified string length is too short");
            }

            // Get data space
            int dataspaceID = H5.H5Dget_space(datasetID);

            // Get dataset dimensions
            long[] datasetDims = new long[rank];
            long[] maxDims = new long[rank];
            H5.H5Sget_simple_extent_dims(dataspaceID, datasetDims, maxDims);

            // Create the memory data type
            int memoryType = H5.H5Tcopy(FORTRAN_STRING);

            // Set the memory size using the string length
            H5.H5Tset_size(memoryType, str_len);

            // Create subset dimensions array
            long[] subsetDims = new long[]{nvals};

            // Create simple memory space, using subset dimensions
            int memspaceID = H5.H5Screate_simple(1, subsetDims, maxDims);

            // Read the data
            hdfArray = new String[(int) nvals];
            H5.H5Dread_string(datasetID, memoryType, memspaceID, dataspaceID, PDEFAULT, hdfArray);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not read data from dataset", e);
        }

        return hdfArray;
    }

    /**
     * Write 1D array (float)
     * <p>
     * This method can read an entire dataset or a subset.
     * </p>
     * <p>
     * Example Settings:
     * <ul>
     * <li>Entire dataset, 100 values:<br>
     * startidx = 0, nvals = 100</li>
     * <li>Subset of dataset at starting at index = 5, 30 values:<br>
     * startidx = 5, nvals = 30</li>
     * </ul>
     *
     * @param datasetID H5Dataset identifier
     * @param startidx  Index of first value of subset
     * @param nvals     Number of values in 1D subset
     * @param hdfArray  Input data array
     * @throws H5InterfaceException HDF5 dataset exception
     */
    public static void write_1D_array(int datasetID, long startidx, long nvals, float[] hdfArray)
            throws H5InterfaceException
    {
        try {
            // Get dataset type
            int dataType = H5.H5Dget_type(datasetID);

            // Set dataset extents to accommodate new data
            Dimensions datasetDims = getDimensions(datasetID, 1);
            long nvals_extended = startidx + nvals;
            nvals_extended = Math.max(nvals_extended, datasetDims.currentDims[0]);
	    
            long[] extendedDims = {nvals_extended};
            setExtents(datasetID, extendedDims, 1);

            // Select hyperslab and allocate memory space
            Hyperspace hs = getHyperspace1D(datasetID, startidx, nvals);

            // Write data to 1D dataset
            H5.H5Dwrite_float(datasetID, dataType, hs.memspaceID, hs.dataspaceID, PDEFAULT, hdfArray);

            // Close resources
            H5.H5Sclose(hs.memspaceID);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not write data to dataset", e);
        }
    }

    /**
     * Write 1D array (float wrapper)
     * <p>
     * This method can read an entire dataset or a subset.
     * </p>
     *
     * @param datasetID H5Dataset identifier
     * @param startidx  Index of first value of subset
     * @param nvals     Number of values in 1D subset
     * @param hdfArray  Input data array
     * @throws H5InterfaceException HDF5 dataset exception
     * @see #write_1D_array(int, long, long, float[])
     */
    public static void write_1D_array_float(int datasetID, long startidx, long nvals, float[] hdfArray)
            throws H5InterfaceException
    {
        write_1D_array(datasetID, startidx, nvals, hdfArray);
    }

    /**
     * Write 1D array (int)
     * <p>
     * This method can read an entire dataset or a subset.
     * </p>
     *
     * @param datasetID H5Dataset identifier
     * @param startrow  Index of first row of subset
     * @param nvals     Number of values in 1D subset
     * @param hdfArray  Input data array
     * @throws H5InterfaceException HDF5 dataset exception
     * @see #write_1D_array(int, long, long, float[])
     */
    public static void write_1D_array(int datasetID, long startrow, long nvals, int[] hdfArray)
            throws H5InterfaceException
    {
        try {
            // Get dataset type
            int dataType = H5.H5Dget_type(datasetID);

            // Set dataset extents to accommodate new data
            Dimensions datasetDims = getDimensions(datasetID, 1);
            long nvals_extended = startrow + nvals;
            nvals_extended = Math.max((int) nvals_extended, (int) datasetDims.currentDims[0]);
	    
            long[] extendedDims = {nvals_extended};
            setExtents(datasetID, extendedDims, 1);

            // Select hyperslab and allocate memory space
            Hyperspace hs = getHyperspace1D(datasetID, startrow, nvals);

            // Write data to 1D dataset
            H5.H5Dwrite_int(datasetID, dataType, hs.memspaceID, hs.dataspaceID, PDEFAULT, hdfArray);

            // Close resources
            H5.H5Sclose(hs.memspaceID);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not write data to dataset", e);
        }
    }

    /**
     * Write 1D array (int wrapper)
     * <p>
     * This method can read an entire dataset or a subset.
     * </p>
     *
     * @param datasetID H5Dataset identifier
     * @param startrow  Index of first row of subset
     * @param nvals     Number of values in 1D subset
     * @param hdfArray  Input data array
     * @throws H5InterfaceException HDF5 dataset exception
     * @see #write_1D_array(int, long, long, float[])
     */
    public static void write_1D_array_int(int datasetID, long startrow, long nvals, int[] hdfArray)
            throws H5InterfaceException
    {
        write_1D_array(datasetID, startrow, nvals, hdfArray);
    }

    /**
     * Write 1D array (string)
     *
     * @param datasetID H5Dataset identifier
     * @param startidx  Index of first row of subset
     * @param nvals     Number of values in 1D subset
     * @param str_len   String length of dataset
     * @param hdfArray  Input data array
     * @throws H5InterfaceException HDF5 dataset exception
     * @see #write_1D_array(int, long, long, float[])
     */
    public static void write_1D_array(int datasetID, long startidx, long nvals, int str_len, String[] hdfArray)
            throws H5InterfaceException
    {
        final int rank = 1; // H5Dataset rank

        try {
            // Set dataset extents to accommodate new data
            Dimensions datasetDims = getDimensions(datasetID, rank);
            long nvals_extended = startidx + nvals;
            nvals_extended = Math.max(nvals_extended, datasetDims.currentDims[0]);
            long[] extendedDims = {nvals_extended};
            setExtents(datasetID, extendedDims, rank);

            // Create file and memory datatypes. Save as a Fortran string,
            // which does not need space for the null terminator in the file.
            int filetypeID = H5.H5Tcopy(FORTRAN_STRING);
            H5.H5Tset_size(filetypeID, str_len - 1);
            int memtypeID = H5.H5Tcopy(C_STRING);
            H5.H5Tset_size(memtypeID, str_len);

            // Prepare data for output
            byte[][] dset_data = new byte[(int) nvals][str_len];
            StringBuffer[] str_data = new StringBuffer[(int) nvals];
            for (int i = 0; i < nvals; i++) {
                str_data[i] = new StringBuffer(hdfArray[i]);
            }

            for (int i = 0; i < nvals; i++) {
                for (int j = 0; j < str_len; j++) {
                    if (j < str_data[i].length())
                        dset_data[i][j] = (byte) str_data[i].charAt(j);
                    else
                        dset_data[i][j] = 0;
                }
            }
            
            // Select hyperslab and allocate memory space
            Hyperspace hs = getHyperspace1D(datasetID, startidx, nvals);

            // Write data to the dataset
            H5.H5Dwrite(datasetID, memtypeID, hs.memspaceID, hs.dataspaceID, PDEFAULT, dset_data);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not write data to dataset", e);
        }
    }

    /**
     * Write 1D array (string wrapper)
     *
     * @param datasetID H5Dataset identifier
     * @param startidx  Index of first row of subset
     * @param nvals     Number of values in 1D subset
     * @param str_len   String length of dataset
     * @param hdfArray  Input data array
     * @throws H5InterfaceException HDF5 dataset exception
     * @see #write_1D_array(int, long, long, float[])
     */
    public static void write_1D_array_string(int datasetID, long startidx, long nvals, int str_len, String[] hdfArray)
            throws H5InterfaceException
    {
        write_1D_array(datasetID, startidx, nvals, str_len, hdfArray);
    }

    /**
     * Write 2D array (float)
     * <p>
     * This method can write an entire dataset or a subset,
     * including row and column vectors.
     * </p>
     * <p>
     * Example Settings:
     * <ul>
     * <li>Entire dataset, 100 rows x 50 columns:<br>
     * startrow = 0, startcol = 0, nrows_hdf = 100, ncols_hdf = 50</li>
     * <li>Subset of dataset at starting at (row, col) = (5,7), 30 rows x 20 columns:<br>
     * startrow = 5, startcol = 7, nrows_hdf = 30, ncols_hdf = 20</li>
     * <li>Row vector at index 5, 100 values (e.g. write all cross-section data for a time step):<br>
     * startrow = 5, startcol = 0, nrows_hdf = 1, ncols_hdf = 100</li>
     * <li>Column vector at index 5, 100 values (e.g., write time series for a cross-section):<br>
     * startrow = 0, startcol = 5, nrows_hdf = 100, ncols_hdf = 1</li>
     * </ul>
     *
     * @param datasetID  H5Dataset identifier
     * @param startrow   Index of first row of subset
     * @param startcol   Index of first column of subset
     * @param nrows_data Number of rows in data array
     * @param ncols_data Number of columns in data array
     * @param data       2D data array to write
     * @throws H5InterfaceException HDF5 dataset exception
     */
    public static void write_2D_array(int datasetID, long startrow, long startcol,
                                      long nrows_data, long ncols_data, float[][] data)
            throws H5InterfaceException
    {
        int rank = 2;
        int nvals = (int) (nrows_data * ncols_data);
        float[] hdfArray = new float[nvals];
        int index = 0;

        // Convert 2D array to 1D array to write to dataset
        for (int i = 0; i < nrows_data; i++) {
            for (int j = 0; j < ncols_data; j++) {
                hdfArray[index] = data[i][j];
                index++;
            }
        }

        try {
            // Set dataset extents to accommodate new data
            Dimensions datasetDims = getDimensions(datasetID, rank);
            long nrows_extended = startrow + nrows_data;
            long ncols_extended = startcol + ncols_data;
            nrows_extended = Math.max(nrows_extended, datasetDims.currentDims[0]);
            ncols_extended = Math.max(ncols_extended, datasetDims.currentDims[1]);
            long[] extendedDims = {nrows_extended, ncols_extended};
            setExtents(datasetID, extendedDims, rank);

            // Select hyperslab and allocate memory space
            Hyperspace hs = getHyperspace2D(datasetID, startrow, startcol, nrows_data, ncols_data);

            // Write data to dataset (region may be a subset of the entire dataset, e.g., a row or column vector)
            int dataType = H5.H5Dget_type(datasetID); // Get data type from dataset
            H5.H5Dwrite(datasetID, dataType, hs.memspaceID, hs.dataspaceID, PDEFAULT, hdfArray);

            // Close resources
            H5.H5Sclose(hs.memspaceID);
            H5.H5Sclose(hs.dataspaceID);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not write data to dataset", e);
        }
    }

    /**
     * Write 2D array (float wrapper)
     * <p>
     * This method can write an entire dataset or a subset,
     * including row and column vectors.
     * </p>
     *
     * @param datasetID  H5Dataset identifier
     * @param startrow   Index of first row of subset
     * @param startcol   Index of first column of subset
     * @param nrows_data Number of rows in data array
     * @param ncols_data Number of columns in data array
     * @param data       2D data array to write
     * @throws H5InterfaceException HDF5 dataset exception
     * @see #write_2D_array(int, long, long, long, long, float[][])
     */
    public static void write_2D_array_float(int datasetID, long startrow, long startcol,
                                            long nrows_data, long ncols_data, float[][] data)
            throws H5InterfaceException
    {
        write_2D_array(datasetID, startrow, startcol, nrows_data, ncols_data, data);
    }

    /**
     * Write 2D array (double)
     * <p>
     * This method can write an entire dataset or a subset,
     * including row and column vectors.
     * </p>
     *
     * @param datasetID  H5Dataset identifier
     * @param startrow   Index of first row of subset
     * @param startcol   Index of first column of subset
     * @param nrows_data Number of rows in data array
     * @param ncols_data Number of columns in data array
     * @param data       2D data array to write
     * @throws H5InterfaceException HDF5 dataset exception
     * @see #write_2D_array(int, long, long, long, long, float[][])
     */
    public static void write_2D_array(int datasetID, long startrow, long startcol,
                                      long nrows_data, long ncols_data, double[][] data)
            throws H5InterfaceException
    {
        // Get dataset type
        int rank = 2;
        int nvals = (int) (nrows_data * ncols_data);
        double[] hdfArray = new double[nvals];
        int index = 0;

        // Convert 2D array to 1D array to write to dataset
        for (int i = 0; i < nrows_data; i++) {
            for (int j = 0; j < ncols_data; j++) {
                hdfArray[index] = data[i][j];
                index++;
            }
        }

        try {
            // Set dataset extents to accommodate new data
            Dimensions datasetDims = getDimensions(datasetID, rank);
            long nrows_extended = startrow + nrows_data;
            long ncols_extended = startcol + ncols_data;
            nrows_extended = Math.max(nrows_extended, datasetDims.currentDims[0]);
            ncols_extended = Math.max(ncols_extended, datasetDims.currentDims[1]);
            long[] extendedDims = {nrows_extended, ncols_extended};
            setExtents(datasetID, extendedDims, rank);

            // Select hyperslab and allocate memory space
            Hyperspace hs = getHyperspace2D(datasetID, startrow, startcol, nrows_data, ncols_data);

            // Write data to dataset (region may be a subset of the entire dataset, e.g., a row or column vector)
            int dataType = H5.H5Dget_type(datasetID); // Get data type from dataset
            H5.H5Dwrite(datasetID, dataType, hs.memspaceID, hs.dataspaceID, PDEFAULT, hdfArray);

            // Close resources
            H5.H5Sclose(hs.memspaceID);
            H5.H5Sclose(hs.dataspaceID);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not write data to dataset", e);
        }
    }

    /**
     * Write 2D array (double wrapper)
     * <p>
     * This method can write an entire dataset or a subset,
     * including row and column vectors.
     * </p>
     *
     * @param datasetID  H5Dataset identifier
     * @param startrow   Index of first row of subset
     * @param startcol   Index of first column of subset
     * @param nrows_data Number of rows in data array
     * @param ncols_data Number of columns in data array
     * @param data       2D data array to write
     * @throws H5InterfaceException HDF5 dataset exception
     * @see #write_2D_array(int, long, long, long, long, float[][])
     */
    public static void write_2D_array_double(int datasetID, long startrow, long startcol,
                                             long nrows_data, long ncols_data, double[][] data)
            throws H5InterfaceException
    {
        write_2D_array(datasetID, startrow, startcol, nrows_data, ncols_data, data);
    }

    /**
     * Write 2D array (int)
     * <p>
     * This method can write an entire dataset or a subset,
     * including row and column vectors.
     * </p>
     *
     * @param datasetID  H5Dataset identifier
     * @param startrow   Index of first row of subset
     * @param startcol   Index of first column of subset
     * @param nrows_data Number of rows in data array
     * @param ncols_data Number of columns in data array
     * @param data       2D data array to write
     * @throws H5InterfaceException HDF5 dataset exception
     * @see #write_2D_array(int, long, long, long, long, float[][])
     */
    public static void write_2D_array(int datasetID, long startrow, long startcol,
                                      long nrows_data, long ncols_data, int[][] data) throws H5InterfaceException
    {
        // Get dataset type
        int rank = 2;
        int nvals = (int) (nrows_data * ncols_data);
        int[] hdfArray = new int[nvals];
        int index = 0;

        // Convert 2D array to 1D array to write to dataset
        for (int i = 0; i < nrows_data; i++) {
            for (int j = 0; j < ncols_data; j++) {
                hdfArray[index] = data[i][j];
                index++;
            }
        }

        try {
            // Set dataset extents to accommodate new data
            Dimensions datasetDims = getDimensions(datasetID, rank);
            long nrows_extended = startrow + nrows_data;
            long ncols_extended = startcol + ncols_data;

            nrows_extended = Math.max(nrows_extended, datasetDims.currentDims[0]);
            ncols_extended = Math.max(ncols_extended, datasetDims.currentDims[1]);
            long[] extendedDims = {nrows_extended, ncols_extended};
            setExtents(datasetID, extendedDims, rank);

            // Select hyperslab and allocate memory space
            Hyperspace hs = getHyperspace2D(datasetID, startrow, startcol, nrows_data, ncols_data);

            // Write data to dataset (region may be a subset of the entire dataset, e.g., a row or column vector)
            int dataType = H5.H5Dget_type(datasetID); // Get data type from dataset
            H5.H5Dwrite(datasetID, dataType, hs.memspaceID, hs.dataspaceID, PDEFAULT, hdfArray);

            // Close resources
            H5.H5Sclose(hs.memspaceID);
            H5.H5Sclose(hs.dataspaceID);
        }
        catch (Exception e) {
            throw new H5InterfaceException("Could not write data to dataset", e);
        }
    }


    /**
     * Write 2D array (int wrapper)
     * <p>
     * This method can write an entire dataset or a subset,
     * including row and column vectors.
     * </p>
     *
     * @param datasetID  H5Dataset identifier
     * @param startrow   Index of first row of subset
     * @param startcol   Index of first column of subset
     * @param nrows_data Number of rows in data array
     * @param ncols_data Number of columns in data array
     * @param data       2D data array to write
     * @throws H5InterfaceException HDF5 dataset exception
     * @see #write_2D_array(int, long, long, long, long, float[][])
     */
    public static void write_2D_array_int(int datasetID, long startrow, long startcol,
                                          long nrows_data, long ncols_data, int[][] data)
            throws H5InterfaceException
    {
        write_2D_array(datasetID, startrow, startcol, nrows_data, ncols_data, data);
    }

}
