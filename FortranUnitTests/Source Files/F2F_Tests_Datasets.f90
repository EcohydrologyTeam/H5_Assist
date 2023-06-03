!DEC$ FREEFORM
!* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
! HDF5 Unit Tests - Dataset Tests
!* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
!
! Created by Todd Steissberg, 2017
!
!------------------------------------------------------------------------------------------------
! See notes in F2F_Tests_Globals.f90
!------------------------------------------------------------------------------------------------
!
module F2F_Tests_Datasets
  !
  use, non_intrinsic  :: hdf5
  use, non_intrinsic  :: h5_globals
  use, non_intrinsic  :: h5_utilities
  use, non_intrinsic  :: h5_files
  use, non_intrinsic  :: h5_groups
  use, non_intrinsic  :: h5_datasets
  use, non_intrinsic  :: F2F_Tests_Globals
  use, intrinsic      :: iso_c_binding, only : c_int, c_float
  use, intrinsic      :: ISO_FORTRAN_ENV
  !
  implicit none
  !
  contains
  !
  ! ...............................................................................................
  !
  ! Test if dataset exists in group
  ! ...............................................................................................
  logical function test_h5_dataset_exists_in_group_id(file_id) result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_dataset_exists_in_group_id" :: test_h5_dataset_exists_in_group_id
    integer(hid_t), intent(out)         :: file_id
    integer(hid_t)                      :: file_access_flag = READ_ONLY
    integer(hid_t)                      :: group_id
    character(len=STD_STR_LEN)          :: infile
    character(len=STD_STR_LEN)          :: dataset_name
    character(len=STD_STR_LEN)          :: group_name
    logical                             :: is_ok
    logical                             :: link_exists
    !
    success = .false.
    !
    group_id = 0
    file_id = 0
    infile = HDF_INFILE
    group_name = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections"
    dataset_name = "Flow"
    !
    ! Initialize HDF5 interface
    is_ok = h5_init()
    if (.not. is_ok) return
    !
    is_ok = h5_open_file(infile, file_access_flag, file_id)
    if (.not. is_ok) return
    !
    is_ok = h5_open_group(file_id, group_name, group_id)
    if (.not. is_ok) return
    !
    is_ok = h5_dataset_exists_in_group_id(group_id, dataset_name, link_exists)
    if (.not. is_ok) return
    !
    is_ok = h5_close_group(group_id)
    if (.not. is_ok) return

    is_ok = h5_close_file(file_id)
    if (.not. is_ok) return
    !
    is_ok = h5_terminate()
    if (.not. is_ok) return
    !
    success = .true.
  end function
  !
  ! ...............................................................................................
  !
  ! Test if dataset exists in file
  ! ...............................................................................................
  logical function test_h5_dataset_exists_in_groupname(file_id) result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_dataset_exists_in_groupname" :: test_h5_dataset_exists_in_groupname
    integer(hid_t), intent(out) :: file_id
    integer(hid_t)                      :: file_access_flag = READ_ONLY
    character(len=STD_STR_LEN)          :: infile
    character(len=STD_STR_LEN)          :: dataset_name
    character(len=STD_STR_LEN)          :: group_name
    logical                             :: is_ok
    logical                             :: link_exists
    !
    success = .false.
    file_id = 0
    !
    infile = HDF_INFILE
    group_name = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections"
    dataset_name = "Flow"
    !
    ! Initialize HDF5 interface
    is_ok = h5_init()
    if (.not. is_ok) return
    !
    is_ok = h5_open_file(infile, file_access_flag, file_id)
    if (.not. is_ok) return
    !
    is_ok = h5_dataset_exists_in_groupname(file_id, group_name, dataset_name, link_exists)
    if (.not. is_ok) return
    !
    is_ok = h5_close_file(file_id)
    if (.not. is_ok) return
    !
    is_ok = h5_terminate()
    if (.not. is_ok) return
    !
    success = .true.
  end function
  !
  ! ...............................................................................................
  !
  ! Test opening and closing a dataset
  ! ...............................................................................................
  logical function test_h5_open_and_close_dataset(file_id) result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_open_and_close_dataset" :: test_h5_open_and_close_dataset
    integer(hid_t), intent(out)         :: file_id
    integer(hid_t)                      :: file_access_flag = READ_ONLY
    integer(hid_t)                      :: group_id
    integer(hid_t)                      :: dataset_id
    character(len=STD_STR_LEN)          :: infile
    character(len=STD_STR_LEN)          :: group_name
    character(len=STD_STR_LEN)          :: dataset_name
    logical                             :: is_ok
    logical                             :: link_exists
    !
    success = .false.
    !
    file_id = 0
    group_id = 0
    dataset_id = 0
    infile = HDF_INFILE
    group_name = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections"
    dataset_name = "Flow"
    !
    ! Initialize HDF5 interface
    is_ok = h5_init()
    if (.not. is_ok) return
    !
    is_ok = h5_open_file(infile, file_access_flag, file_id)
    if (.not. is_ok) return
    !
    is_ok = h5_group_exists(file_id, group_name, link_exists)
    if (.not. is_ok) return
    !
    is_ok = h5_open_group(file_id, group_name, group_id)
    if (.not. is_ok) return
    !
    is_ok = h5_dataset_exists_in_group_id(group_id, dataset_name, link_exists)
    !
    is_ok = h5_close_group(group_id)
    if (.not. is_ok) return
    !
    is_ok = h5_close_file(file_id)
    if (.not. is_ok) return
    !
    is_ok = h5_terminate()
    if (.not. is_ok) return
    !
    success = .true.
  end function
  !
  ! ...............................................................................................
  !
  ! Test opening and closing a dataset
  ! ...............................................................................................
  logical function test_h5_get_dataset_dimensions(file_id) result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_get_dataset_dimensions" :: test_h5_get_dataset_dimensions
    integer(hid_t), intent(out)         :: file_id
    integer(hid_t)                      :: file_access_flag = READ_ONLY
    integer(hid_t)                      :: group_id
    integer(hid_t)                      :: dataset_id
    character(len=STD_STR_LEN)          :: infile
    character(len=STD_STR_LEN)          :: group_name
    character(len=STD_STR_LEN)          :: dataset_name
    integer, parameter                  :: rank = 2
    integer(hsize_t), &
      dimension(1:rank)                 :: data_dims
    integer(hsize_t), &
      dimension(1:rank)                 :: max_dims
    logical                             :: is_ok
    logical                             :: link_exists
    !
    success = .false.
    !
    file_id = 0
    group_id = 0
    dataset_id = 0
    infile = HDF_INFILE
    group_name = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections"
    dataset_name = "Flow"
    !
    ! Initialize HDF5 interface
    is_ok = h5_init()
    if (.not. is_ok) return
    !
    is_ok = h5_open_file(infile, file_access_flag, file_id)
    if (.not. is_ok) return
    !
    is_ok = h5_group_exists(file_id, group_name, link_exists)
    if (.not. is_ok) return
    !
    is_ok = h5_open_group(file_id, group_name, group_id)
    if (.not. is_ok) return
    !
    is_ok = h5_open_dataset(group_id, dataset_name, dataset_id)
    if (.not. is_ok) return
    !
    is_ok = h5_get_dataset_dimensions(dataset_id, data_dims, max_dims, rank)
    if (.not. is_ok) return
    !
    is_ok = h5_close_group(group_id)
    if (.not. is_ok) return
    !
    is_ok = h5_close_file(file_id)
    if (.not. is_ok) return
    !
    is_ok = h5_terminate()
    if (.not. is_ok) return
    !
    success = .true.
  end function
  !
  ! ...............................................................................................
  !
  ! Test reading full 2D dataset : Real type
  ! ...............................................................................................
  logical function test_h5_read_dataset_2d_array_r(file_id) result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_read_dataset_2d_array_r" :: test_h5_read_dataset_2d_array_r
    integer(hid_t), intent(out)         :: file_id
    integer(hid_t)                      :: file_access_flag = READ_ONLY
    integer(hid_t)                      :: group_id
    integer(hid_t)                      :: dataset_id
    character(len=STD_STR_LEN)          :: infile
    character(len=STD_STR_LEN)          :: group_name
    character(len=STD_STR_LEN)          :: dataset_name
    integer(hsize_t), parameter         :: nrows_hdf = 2953
    integer(hsize_t), parameter         :: ncols_hdf = 334
    integer(hsize_t)                    :: startrow
    integer(hsize_t)                    :: startcol
    real(kind=c_float), &
      dimension(ncols_hdf, nrows_hdf)   :: hdf_array
    logical                             :: is_ok
    logical                             :: link_exists
    !
    success = .false.
    !
    file_id = 0
    group_id = 0
    dataset_id = 0
    infile = HDF_INFILE
    group_name = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections"
    dataset_name = "Flow"
    !
    ! Initialize HDF5 interface
    is_ok = h5_init()
    if (.not. is_ok) return
    !
    is_ok = h5_open_file(infile, file_access_flag, file_id)
    if (.not. is_ok) return
    !
    is_ok = h5_group_exists(file_id, group_name, link_exists)
    if (.not. is_ok) return
    !
    is_ok = h5_open_group(file_id, group_name, group_id)
    if (.not. is_ok) return
    !
    is_ok = h5_open_dataset(group_id, dataset_name, dataset_id)
    if (.not. is_ok) return
    !
    ! Read data using 2D array
    startrow = 0
    startcol = 0
    is_ok = h5_read_dataset_2d_array_r(dataset_id, startrow, startcol, nrows_hdf, ncols_hdf, hdf_array)
    if (.not. is_ok) return
    !
    is_ok = h5_close_dataset(dataset_id)
    if (.not. is_ok) return
    !
    is_ok = h5_close_group(group_id)
    if (.not. is_ok) return
    !
    is_ok = h5_close_file(file_id)
    if (.not. is_ok) return
    !
    is_ok = h5_terminate()
    if (.not. is_ok) return
    !
    success = .true.
  end function
  !
  ! ...............................................................................................
  !
  ! Test reading one row of a 2D dataset : Real type
  ! ...............................................................................................
  logical function test_h5_read_dataset_2d_array_r_by_row(file_id) result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_read_dataset_2d_array_r_by_row" :: test_h5_read_dataset_2d_array_r_by_row
    integer(hid_t), intent(out)         :: file_id
    integer(hid_t)                      :: file_access_flag = READ_ONLY
    integer(hid_t)                      :: group_id
    integer(hid_t)                      :: dataset_id
    character(len=STD_STR_LEN)          :: infile
    character(len=STD_STR_LEN)          :: group_name
    character(len=STD_STR_LEN)          :: dataset_name
    integer(hsize_t), parameter         :: nrows_hdf = 2953
    integer(hsize_t), parameter         :: ncols_hdf = 334
    integer(hsize_t), parameter         :: nrows_subset = 1
    integer(hsize_t), parameter         :: ncols_subset = ncols_hdf
    integer(hsize_t)                    :: startrow
    integer(hsize_t)                    :: startcol
    real(kind=c_float), &
      dimension(ncols_hdf, nrows_hdf)   :: hdf_array
    logical                             :: is_ok
    logical                             :: link_exists
    !
    success = .false.
    !
    file_id = 0
    group_id = 0
    dataset_id = 0
    infile = HDF_INFILE
    group_name = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections"
    dataset_name = "Flow"
    !
    ! Initialize HDF5 interface
    is_ok = h5_init()
    if (.not. is_ok) return
    !
    is_ok = h5_open_file(infile, file_access_flag, file_id)
    if (.not. is_ok) return
    !
    is_ok = h5_group_exists(file_id, group_name, link_exists)
    if (.not. is_ok) return
    !
    is_ok = h5_open_group(file_id, group_name, group_id)
    if (.not. is_ok) return
    !
    is_ok = h5_open_dataset(group_id, dataset_name, dataset_id)
    if (.not. is_ok) return
    !
    ! Read data using 2D array
    startrow = 5
    startcol = 0
    is_ok = h5_read_dataset_2d_array_r(dataset_id, startrow, startcol, nrows_subset, ncols_subset, hdf_array)
    if (.not. is_ok) return
    !
    is_ok = h5_close_dataset(dataset_id)
    if (.not. is_ok) return
    !
    is_ok = h5_close_group(group_id)
    if (.not. is_ok) return
    !
    is_ok = h5_close_file(file_id)
    if (.not. is_ok) return
    !
    is_ok = h5_terminate()
    if (.not. is_ok) return
    !
    success = .true.
  end function
  !
  ! ...............................................................................................
  !
  ! Test reading 2D dataset : Integer type
  ! ...............................................................................................
  logical function test_h5_read_dataset_2d_array_i(file_id) result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_read_dataset_2d_array_i" :: test_h5_read_dataset_2d_array_i
    integer(hid_t), intent(out)         :: file_id
    integer(hid_t)                      :: file_access_flag = READ_ONLY
    integer(hid_t)                      :: group_id = 0
    integer(hid_t)                      :: dataset_id = 0
    character(len=STD_STR_LEN)          :: infile
    character(len=STD_STR_LEN)          :: group_name
    character(len=STD_STR_LEN)          :: dataset_name
    integer(hsize_t), parameter         :: nrows_hdf = 334
    integer(hsize_t), parameter         :: ncols_hdf = 4
    integer(hsize_t)                    :: startrow
    integer(hsize_t)                    :: startcol
    integer(kind=c_int), &
      dimension(ncols_hdf, nrows_hdf)   :: hdf_array
    logical                             :: is_ok
    logical                             :: link_exists
    !
    success = .false.
    file_id = 0
    !
    infile = HDF_INFILE
    group_name = "/Geometry/Cross Sections"
    dataset_name = "Polyline Info"
    !
    ! Initialize HDF5 interface
    is_ok = h5_init()
    if (.not. is_ok) return
    !
    is_ok = h5_open_file(infile, file_access_flag, file_id)
    if (.not. is_ok) return
    !
    is_ok = h5_group_exists(file_id, group_name, link_exists)
    if (.not. is_ok) return
    !
    is_ok = h5_open_group(file_id, group_name, group_id)
    if (.not. is_ok) return
    !
    is_ok = h5_open_dataset(group_id, dataset_name, dataset_id)
    if (.not. is_ok) return
    !
    ! Read entire dataset using 2D array
    startrow = 0 ! Note: this must start at zero to read the whole dataset
    startcol = 0 ! Note: this must start at zero to read the whole dataset
    is_ok = h5_read_dataset_2d_array_i(dataset_id, startrow, startcol, nrows_hdf, ncols_hdf, hdf_array)
    if (.not. is_ok) return
    !
    is_ok = h5_close_dataset(dataset_id)
    if (.not. is_ok) return
    !
    is_ok = h5_close_group(group_id)
    if (.not. is_ok) return
    !
    is_ok = h5_close_file(file_id)
    if (.not. is_ok) return
    !
    is_ok = h5_terminate()
    if (.not. is_ok) return
    !
    success = .true.
  end function
  !
  ! ...............................................................................................
  !
  ! Test reading 2D dataset : Integer type
  ! ...............................................................................................
  logical function test_h5_read_dataset_2d_array_i_by_row(file_id) result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_read_dataset_2d_array_i_by_row" :: test_h5_read_dataset_2d_array_i_by_row
    integer(hid_t), intent(out)         :: file_id
    integer(hid_t)                      :: file_access_flag = READ_ONLY
    integer(hid_t)                      :: group_id = 0
    integer(hid_t)                      :: dataset_id = 0
    character(len=STD_STR_LEN)          :: infile
    character(len=STD_STR_LEN)          :: group_name
    character(len=STD_STR_LEN)          :: dataset_name
    integer(hsize_t), parameter         :: nrows_hdf = 334
    integer(hsize_t), parameter         :: ncols_hdf = 4
    integer(hsize_t)                    :: nrows_subset = 1
    integer(hsize_t)                    :: ncols_subset = ncols_hdf
    integer(hsize_t)                    :: startrow
    integer(hsize_t)                    :: startcol
    integer(kind=c_int), &
      dimension(ncols_hdf, nrows_hdf)   :: hdf_array
    logical                             :: is_ok
    logical                             :: link_exists
    !
    success = .false.
    file_id = 0
    !
    infile = HDF_INFILE
    group_name = "/Geometry/Cross Sections"
    dataset_name = "Polyline Info"
    !
    ! Initialize HDF5 interface
    is_ok = h5_init()
    if (.not. is_ok) return
    !
    is_ok = h5_open_file(infile, file_access_flag, file_id)
    if (.not. is_ok) return
    !
    is_ok = h5_group_exists(file_id, group_name, link_exists)
    if (.not. is_ok) return
    !
    is_ok = h5_open_group(file_id, group_name, group_id)
    if (.not. is_ok) return
    !
    is_ok = h5_open_dataset(group_id, dataset_name, dataset_id)
    if (.not. is_ok) return
    !
    ! Read data using 2D array
    startrow = 5
    startcol = 0
    is_ok = h5_read_dataset_2d_array_i(dataset_id, startrow, startcol, nrows_subset, ncols_subset, hdf_array)
    if (.not. is_ok) return
    !
    is_ok = h5_close_dataset(dataset_id)
    if (.not. is_ok) return
    !
    is_ok = h5_close_group(group_id)
    if (.not. is_ok) return
    !
    is_ok = h5_close_file(file_id)
    if (.not. is_ok) return
    !
    is_ok = h5_terminate()
    if (.not. is_ok) return
    !
    success = .true.
  end function
  !
  ! ...............................................................................................
  !
  ! Test reading 1D array: String type
  ! ...............................................................................................
  logical function test_h5_read_dataset_1d_array_c(file_id) result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_read_dataset_1d_array_c" :: test_h5_read_dataset_1d_array_c
    integer(hid_t), intent(out)         :: file_id
    integer(hid_t)                      :: file_access_flag = READ_ONLY
    integer(hid_t)                      :: group_id = 0
    integer(hid_t)                      :: dataset_id = 0
    character(len=STD_STR_LEN)          :: infile
    character(len=STD_STR_LEN)          :: group_name
    character(len=STD_STR_LEN)          :: dataset_name
    integer(hsize_t), parameter         :: nvals = 334
    integer(hsize_t)                    :: startrow
    integer, parameter                  :: str_len = 512
    character(len=str_len), &
      dimension(nvals)                  :: hdf_array
    logical                             :: is_ok
    logical                             :: link_exists
    !
    success = .false.
    !
    file_id = 0
    infile = HDF_INFILE
    group_name = "/Geometry/Cross Sections"
    dataset_name = "Node Descriptions"
    startrow = 0
    !
    ! Initialize HDF5 interface
    is_ok = h5_init()
    if (.not. is_ok) return
    !
    is_ok = h5_open_file(infile, file_access_flag, file_id)
    if (.not. is_ok) return
    !
    is_ok = h5_group_exists(file_id, group_name, link_exists)
    if (.not. is_ok) return
    !
    is_ok = h5_open_group(file_id, group_name, group_id)
    if (.not. is_ok) return
    !
    is_ok = h5_open_dataset(group_id, dataset_name, dataset_id)
    if (.not. is_ok) return
    !
    is_ok = h5_read_dataset_1d_array_c(dataset_id, startrow, nvals, str_len, hdf_array)
    if (.not. is_ok) return
    !
    is_ok = h5_close_dataset(dataset_id)
    if (.not. is_ok) return
    !
    is_ok = h5_close_group(group_id)
    if (.not. is_ok) return
    !
    is_ok = h5_close_file(file_id)
    if (.not. is_ok) return
    !
    is_ok = h5_terminate()
    if (.not. is_ok) return
    !
    success = .true.
  end function
  !
  ! ...............................................................................................
  !
  ! Test creating compressed 2D datasets, chunked in time
  ! ...............................................................................................
  logical function test_h5_create_compressed_2d_datasets_chunked_in_time(infile_id) result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_create_compressed_2d_datasets_chunked_in_time" :: test_h5_create_compressed_2d_datasets_chunked_in_time
    integer(hid_t), intent(out)         :: infile_id
    integer(hid_t)                      :: file_access_flag = READ_ONLY
    integer(hid_t)                      :: outfile_id = 0           ! Output file ID
    integer(hid_t)                      :: ingroup_id = 0           ! Input group ID
    integer(hid_t)                      :: outgroup_id = 0          ! Output group ID
    integer(hid_t)                      :: indataset_id = 0         ! Input dataset ID
    integer(hid_t)                      :: outdataset_id = 0        ! Output dataset ID
    character(len=STD_STR_LEN)          :: infile                   ! Input file
    character(len=STD_STR_LEN)          :: outfile                  ! Output file
    character(len=STD_STR_LEN)          :: ingroup_name             ! Input group name
    character(len=STD_STR_LEN)          :: outgroup_name            ! Output group name
    character(len=STD_STR_LEN)          :: indataset_name           ! Input dataset name
    character(len=STD_STR_LEN)          :: outdataset_name          ! Output dataset name
    integer, parameter                  :: nrows_hdf = 2953         ! # Fortran columns, # HDF rows
    integer, parameter                  :: ncols_hdf = 334          ! # Fortran rows, # HDF columns
    logical                             :: compressed               ! Chunk and compress dataset
    integer(hid_t)                      :: data_type                ! Data type ID
    logical, parameter                  :: chunk_in_time = .true.   ! Chunk dataset in time
    integer, parameter                  :: kind_nbytes = 4
    integer(hsize_t)                    :: nrows_subset = 1
    integer(hsize_t)                    :: ncols_subset = ncols_hdf
    integer(hsize_t)                    :: startrow
    integer(hsize_t)                    :: startcol
    logical                             :: is_ok
    logical                             :: link_exists
    !
    success = .false.
    !
    infile_id       = 0
    outfile_id      = 0
    ingroup_id      = 0
    outgroup_id     = 0
    indataset_id    = 0
    outdataset_id   = 0
    infile          = HDF_INFILE
    outfile         = "F2F_h5_create_compressed_2d_dataset_chunked_in_time.h5"
    ingroup_name    = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections"
    outgroup_name   = "/Results"
    indataset_name  = "Flow"
    outdataset_name = "Flow"
    compressed = .true.
    data_type = TEST_H5T_NATIVE_REAL
    !
    ! Initialize HDF5 interface
    is_ok = h5_init()
    if (.not. is_ok) return
    !
    ! Create output file
    is_ok = h5_create_file(outfile, TRUNCATE, outfile_id)
    if (.not. is_ok) return
    !
    ! Create output group
    is_ok = h5_create_group(outfile_id, outgroup_name, outgroup_id)
    if (.not. is_ok) return
    !
    ! Create compressed 2D output dataset
    is_ok = h5_create_2d_dataset(outgroup_id, outdataset_name, nrows_hdf, ncols_hdf, data_type, chunk_in_time, kind_nbytes, compressed, outdataset_id)
    if (.not. is_ok) return
    !
    is_ok = h5_close_dataset(outdataset_id)
    if (.not. is_ok) return
    !
    is_ok = h5_close_group(outgroup_id)
    if (.not. is_ok) return
    !
    is_ok = h5_close_file(outfile_id)
    if (.not. is_ok) return
    !
    ! Close HDF5 interface
    is_ok = h5_terminate()
    if (.not. is_ok) return
    !
    success = .true.
  end function
  !
  ! ...............................................................................................
  !
  ! Test creating compressed 2D datasets, chunked in space
  ! ...............................................................................................
  logical function test_h5_create_compressed_2d_datasets_chunked_in_space(file_id) result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_create_compressed_2d_datasets_chunked_in_space" :: test_h5_create_compressed_2d_datasets_chunked_in_space
    integer(hid_t), intent(out)         :: file_id
    integer(hid_t)                      :: file_access_flag = READ_ONLY
    integer(hid_t)                      :: infile_id = 0            ! Input file ID
    integer(hid_t)                      :: outfile_id = 0           ! Output file ID
    integer(hid_t)                      :: ingroup_id = 0           ! Input group ID
    integer(hid_t)                      :: outgroup_id = 0          ! Output group ID
    integer(hid_t)                      :: indataset_id = 0         ! Input dataset ID
    integer(hid_t)                      :: outdataset_id = 0        ! Output dataset ID
    character(len=STD_STR_LEN)          :: infile                   ! Input file
    character(len=STD_STR_LEN)          :: outfile                  ! Output file
    character(len=STD_STR_LEN)          :: ingroup_name             ! Input group name
    character(len=STD_STR_LEN)          :: outgroup_name            ! Output group name
    character(len=STD_STR_LEN)          :: indataset_name           ! Input dataset name
    character(len=STD_STR_LEN)          :: outdataset_name          ! Output dataset name
    integer(hsize_t), parameter         :: nrows_hdf = 2953         ! # Fortran columns, # HDF rows
    integer(hsize_t), parameter         :: ncols_hdf = 334          ! # Fortran rows, # HDF columns
    logical                             :: compressed               ! Chunk and compress dataset
    integer(hid_t)                      :: data_type                ! Data type ID
    logical, parameter                  :: chunk_in_time = .false.  ! Chunk dataset in space
    integer, parameter                  :: kind_nbytes = 4
    integer(hsize_t)                    :: startrow
    integer(hsize_t)                    :: startcol
    integer(hsize_t)                    :: nrows_subset
    integer(hsize_t)                    :: ncols_subset
    logical                             :: is_ok
    logical                             :: link_exists
    !
    success = .false.
    file_id = 0
    !
    infile_id       = 0
    outfile_id      = 0
    ingroup_id      = 0
    outgroup_id     = 0
    indataset_id    = 0
    outdataset_id   = 0
    infile          = HDF_INFILE
    outfile         = "F2F_h5_create_compressed_2d_dataset_chunked_in_space.h5"
    ingroup_name    = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections"
    outgroup_name   = "/Results"
    indataset_name  = "Flow"
    outdataset_name = "Flow"
    compressed = .true.
    data_type = TEST_H5T_NATIVE_REAL
    !
    ! Initialize HDF5 interface
    is_ok = h5_init()
    if (.not. is_ok) return
    !
    ! Create output file
    is_ok = h5_create_file(outfile, TRUNCATE, outfile_id)
    if (.not. is_ok) return
    !
    ! Create output group
    is_ok = h5_create_group(outfile_id, outgroup_name, outgroup_id)
    if (.not. is_ok) return
    !
    ! Create compressed 2D output dataset
    is_ok = h5_create_2d_dataset(outgroup_id, outdataset_name, nrows_hdf, ncols_hdf, data_type, chunk_in_time, kind_nbytes, compressed, outdataset_id)
    if (.not. is_ok) return
    !
    is_ok = h5_close_dataset(outdataset_id)
    if (.not. is_ok) return
    !
    is_ok = h5_close_group(outgroup_id)
    if (.not. is_ok) return
    !
    is_ok = h5_close_file(outfile_id)
    if (.not. is_ok) return
    !
    ! Close HDF5 interface
    is_ok = h5_terminate()
    if (.not. is_ok) return
    !
    success = .true.
  end function
  !
  ! ...............................................................................................
  !
  ! Test writing whole 2D dataset: Real type
  ! ...............................................................................................
  logical function test_h5_write_dataset_2d_array_r(file_id) result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_write_dataset_2d_array_r" :: test_h5_write_dataset_2d_array_r
    integer(hid_t), intent(out)         :: file_id
    integer(hid_t)                      :: file_access_flag = READ_ONLY ! File access flag for input
    integer(hid_t)                      :: infile_id                ! Input file ID
    integer(hid_t)                      :: outfile_id               ! Output file ID
    integer(hid_t)                      :: ingroup_id               ! Input group ID
    integer(hid_t)                      :: outgroup_id              ! Output group ID
    integer(hid_t)                      :: indataset_id             ! Input dataset ID
    integer(hid_t)                      :: outdataset_id            ! Output dataset ID
    character(len=STD_STR_LEN)          :: infile                   ! Input file
    character(len=STD_STR_LEN)          :: outfile                  ! Output file
    character(len=STD_STR_LEN)          :: ingroup_name             ! Input group name
    character(len=STD_STR_LEN)          :: outgroup_name            ! Output group name
    character(len=STD_STR_LEN)          :: indataset_name           ! Input dataset name
    character(len=STD_STR_LEN)          :: outdataset_name          ! Output dataset name
    integer(hsize_t), parameter         :: nrows_hdf = 2953         ! # Fortran columns, # HDF rows
    integer(hsize_t), parameter         :: ncols_hdf = 334          ! # Fortran rows, # HDF columns
    integer(hsize_t)                    :: startrow                 ! Starting row
    integer(hsize_t)                    :: startcol                 ! Starting column 
    logical                             :: compressed               ! Chunk and compress dataset
    real(kind=c_float), &
      dimension(ncols_hdf, nrows_hdf) :: hdf_array                  ! Data array
    real(kind=c_float), &
      dimension(ncols_hdf, nrows_hdf) :: hdf_array_in               ! Data array
    real(kind=c_float), &
      dimension(ncols_hdf, nrows_hdf) :: hdf_array_out              ! Data array
    integer(hid_t)                      :: data_type                ! Data type ID
    logical, parameter                  :: chunk_in_time = .true.   ! Chunk dataset in time
    integer, parameter                  :: kind_nbytes = 4          ! Type precision (number of bytes)
    integer                             :: i, j                     ! Loop variables
    logical                             :: is_ok                    ! Function return condition
    logical                             :: link_exists              ! Function return condition
    !
    success = .false.
    file_id = 0
    !
    infile_id       = 0
    outfile_id      = 0
    ingroup_id      = 0
    outgroup_id     = 0
    indataset_id    = 0
    outdataset_id   = 0
    infile          = HDF_INFILE
    outfile         = "F2F_h5_write_dataset_2d_array_r.h5"
    ingroup_name    = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections"
    outgroup_name   = "/Results"
    indataset_name  = "Flow"
    outdataset_name = "Flow"
    compressed = .true.
    data_type = TEST_H5T_NATIVE_REAL
    !
    ! Open input file
    is_ok = h5_open_file(infile, file_access_flag, infile_id)
    if (.not. is_ok) return
    !
    ! Check if group exists
    is_ok = h5_group_exists(infile_id, ingroup_name, link_exists)
    if (.not. is_ok) return
    !
    ! Open existing group
    is_ok = h5_open_group(infile_id, ingroup_name, ingroup_id)
    if (.not. is_ok) return
    !
    ! Open existing dataset
    is_ok = h5_open_dataset(ingroup_id, indataset_name, indataset_id)
    if (.not. is_ok) return
    !
    ! Create output file
    is_ok = h5_create_file(outfile, TRUNCATE, outfile_id)
    if (.not. is_ok) return
    !
    ! Create output group
    is_ok = h5_create_group(outfile_id, outgroup_name, outgroup_id)
    if (.not. is_ok) return
    !
    ! Create compressed 2D output dataset
    is_ok = h5_create_2d_dataset(outgroup_id, outdataset_name, nrows_hdf, ncols_hdf, data_type, chunk_in_time, kind_nbytes, compressed, outdataset_id)
    if (.not. is_ok) return
    !
    ! Read entire dataset and write entire dataset as a whole
    startrow = 0
    startcol = 0
    is_ok = h5_read_dataset_2d_array_r(indataset_id, startrow, startcol, nrows_hdf, ncols_hdf, hdf_array)
    if (.not. is_ok) return
    is_ok = h5_write_dataset_2d_array_r(outdataset_id, startrow, startcol, nrows_hdf, ncols_hdf, hdf_array)
    if (.not. is_ok) return
    !    
    ! Close both files
    is_ok = h5_close_dataset(indataset_id)
    is_ok = h5_close_group(ingroup_id)
    is_ok = h5_close_file(infile_id)
    is_ok = h5_close_dataset(outdataset_id)
    is_ok = h5_close_group(outgroup_id)
    is_ok = h5_close_file(outfile_id)
    !
    ! Reopen both files
    ! Note: The output file was in write/truncate mode. Now it will be opened read-only mode.
    is_ok = h5_open_file(infile, file_access_flag, infile_id)
    is_ok = h5_open_file(outfile, file_access_flag, outfile_id)
    is_ok = h5_open_group(infile_id, ingroup_name, ingroup_id)
    is_ok = h5_open_group(outfile_id, outgroup_name, outgroup_id)
    is_ok = h5_open_dataset(ingroup_id, indataset_name, indataset_id)
    is_ok = h5_open_dataset(outgroup_id, outdataset_name, outdataset_id)
    !
    ! Close input and output datasets
    is_ok = h5_close_dataset(indataset_id)
    if (.not. is_ok) return
    !
    is_ok = h5_close_dataset(outdataset_id)
    if (.not. is_ok) return
    !
    ! Close input and output groups
    is_ok = h5_close_group(ingroup_id)
    if (.not. is_ok) return
    !
    is_ok = h5_close_group(outgroup_id)
    if (.not. is_ok) return
    !
    ! Close input and output files
    is_ok = h5_close_file(infile_id)
    if (.not. is_ok) return
    !
    is_ok = h5_close_file(outfile_id)
    if (.not. is_ok) return
    !
    ! Close HDF5 interface
    is_ok = h5_terminate()
    if (.not. is_ok) return
    !
    success = .true.
  end function
  !
  ! ...............................................................................................
  !
  ! Test writing whole 2D dataset as row vectors: Real type
  ! ...............................................................................................
  logical function test_h5_write_dataset_2d_array_r_by_row(file_id) result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_write_dataset_2d_array_r_by_row" :: test_h5_write_dataset_2d_array_r_by_row
    integer(hid_t), intent(out)         :: file_id
    integer(hid_t)                      :: file_access_flag = READ_ONLY ! File access flag for input
    integer(hid_t)                      :: infile_id                ! Input file ID
    integer(hid_t)                      :: outfile_id               ! Output file ID
    integer(hid_t)                      :: ingroup_id               ! Input group ID
    integer(hid_t)                      :: outgroup_id              ! Output group ID
    integer(hid_t)                      :: indataset_id             ! Input dataset ID
    integer(hid_t)                      :: outdataset_id            ! Output dataset ID
    character(len=STD_STR_LEN)          :: infile                   ! Input file
    character(len=STD_STR_LEN)          :: outfile                  ! Output file
    character(len=STD_STR_LEN)          :: ingroup_name             ! Input group name
    character(len=STD_STR_LEN)          :: outgroup_name            ! Output group name
    character(len=STD_STR_LEN)          :: indataset_name           ! Input dataset name
    character(len=STD_STR_LEN)          :: outdataset_name          ! Output dataset name
    integer(hsize_t), parameter         :: nrows_hdf = 2953         ! # Fortran columns, # HDF rows
    integer(hsize_t), parameter         :: ncols_hdf = 334          ! # Fortran rows, # HDF columns
    integer(hsize_t), parameter         :: nrows_subset = 1         ! # Fortran columns, # HDF rows
    integer(hsize_t), parameter         :: ncols_subset = ncols_hdf ! # Fortran rows, # HDF columns
    integer(hsize_t)                    :: startrow                 
    integer(hsize_t)                    :: startcol                 
    logical                             :: compressed               ! Chunk and compress dataset
    real(kind=c_float), &
      dimension(ncols_subset, nrows_subset) :: hdf_array            ! Data array
    real(kind=c_float), &
      dimension(ncols_subset, nrows_subset) :: hdf_array_in         ! Data array
    real(kind=c_float), &
      dimension(ncols_subset, nrows_subset) :: hdf_array_out        ! Data array
    integer(hid_t)                      :: data_type                ! Data type ID
    logical, parameter                  :: chunk_in_time = .true.   ! Chunk dataset in time
    integer, parameter                  :: kind_nbytes = 4          ! Type precision (number of bytes)
    integer                             :: i, j                     ! Loop variables
    logical                             :: is_ok                    ! Function return condition
    logical                             :: link_exists              ! Function return condition
    !
    success = .false.
    file_id = 0
    !
    infile_id       = 0
    outfile_id      = 0
    ingroup_id      = 0
    outgroup_id     = 0
    indataset_id    = 0
    outdataset_id   = 0
    infile          = HDF_INFILE
    outfile         = "F2F_h5_write_dataset_2d_array_r_by_row.h5"
    ingroup_name    = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections"
    outgroup_name   = "/Results"
    indataset_name  = "Flow"
    outdataset_name = "Flow"
    compressed = .true.
    data_type = TEST_H5T_NATIVE_REAL
    !
    ! Open input file
    is_ok = h5_open_file(infile, file_access_flag, infile_id)
    if (.not. is_ok) return
    !
    ! Check if group exists
    is_ok = h5_group_exists(infile_id, ingroup_name, link_exists)
    if (.not. is_ok) return
    !
    ! Open existing group
    is_ok = h5_open_group(infile_id, ingroup_name, ingroup_id)
    if (.not. is_ok) return
    !
    ! Open existing dataset
    is_ok = h5_open_dataset(ingroup_id, indataset_name, indataset_id)
    if (.not. is_ok) return
    !
    ! Create output file
    is_ok = h5_create_file(outfile, TRUNCATE, outfile_id)
    if (.not. is_ok) return
    !
    ! Create output group
    is_ok = h5_create_group(outfile_id, outgroup_name, outgroup_id)
    if (.not. is_ok) return
    !
    ! Create compressed 2D output dataset
    is_ok = h5_create_2d_dataset(outgroup_id, outdataset_name, nrows_hdf, ncols_hdf, data_type, chunk_in_time, kind_nbytes, compressed, outdataset_id)
    if (.not. is_ok) return
    !
    ! Read dataset from input file by row and write to output file by row
    do i = 1, nrows_hdf
      startrow = i - 1
      startcol = 0
      is_ok = h5_read_dataset_2d_array_r(indataset_id, startrow, startcol, nrows_subset, ncols_subset, hdf_array)
      if (.not. is_ok) return
      is_ok = h5_write_dataset_2d_array_r(outdataset_id, startrow, startcol, nrows_subset, ncols_subset, hdf_array)
      if (.not. is_ok) return
    end do
    !    
    ! Close both files
    is_ok = h5_close_dataset(indataset_id)
    is_ok = h5_close_group(ingroup_id)
    is_ok = h5_close_file(infile_id)
    is_ok = h5_close_dataset(outdataset_id)
    is_ok = h5_close_group(outgroup_id)
    is_ok = h5_close_file(outfile_id)
    !
    ! Reopen both files
    ! Note: The output file was in write/truncate mode. Now it will be opened read-only mode.
    is_ok = h5_open_file(infile, file_access_flag, infile_id)
    is_ok = h5_open_file(outfile, file_access_flag, outfile_id)
    is_ok = h5_open_group(infile_id, ingroup_name, ingroup_id)
    is_ok = h5_open_group(outfile_id, outgroup_name, outgroup_id)
    is_ok = h5_open_dataset(ingroup_id, indataset_name, indataset_id)
    is_ok = h5_open_dataset(outgroup_id, outdataset_name, outdataset_id)
    !
    ! Close input and output datasets
    is_ok = h5_close_dataset(indataset_id)
    if (.not. is_ok) return
    !
    is_ok = h5_close_dataset(outdataset_id)
    if (.not. is_ok) return
    !
    ! Close input and output groups
    is_ok = h5_close_group(ingroup_id)
    if (.not. is_ok) return
    !
    is_ok = h5_close_group(outgroup_id)
    if (.not. is_ok) return
    !
    ! Close input and output files
    is_ok = h5_close_file(infile_id)
    if (.not. is_ok) return
    !
    is_ok = h5_close_file(outfile_id)
    if (.not. is_ok) return
    !
    ! Close HDF5 interface
    is_ok = h5_terminate()
    if (.not. is_ok) return
    !
    success = .true.
  end function
  !
  ! ...............................................................................................
  !
  ! Test writing whole 2D dataset: Integer type
  ! ...............................................................................................
  logical function test_h5_write_dataset_2d_array_i(infile_id) result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_write_dataset_2d_array_i" :: test_h5_write_dataset_2d_array_i
    integer(hid_t), intent(out)         :: infile_id                ! Input file ID
    integer(hid_t)                      :: file_access_flag = READ_ONLY ! File access flag for input
    integer(hid_t)                      :: outfile_id               ! Output file ID
    integer(hid_t)                      :: ingroup_id               ! Input group ID
    integer(hid_t)                      :: outgroup_id              ! Output group ID
    integer(hid_t)                      :: indataset_id             ! Input dataset ID
    integer(hid_t)                      :: outdataset_id            ! Output dataset ID
    character(len=STD_STR_LEN)          :: infile                   ! Input file
    character(len=STD_STR_LEN)          :: outfile                  ! Output file
    character(len=STD_STR_LEN)          :: ingroup_name             ! Input group name
    character(len=STD_STR_LEN)          :: outgroup_name            ! Output group name
    character(len=STD_STR_LEN)          :: indataset_name           ! Input dataset name
    character(len=STD_STR_LEN)          :: outdataset_name          ! Output dataset name
    integer(hsize_t), parameter         :: nrows_hdf = 334          ! # Fortran columns, # HDF rows
    integer(hsize_t), parameter         :: ncols_hdf = 4            ! # Fortran rows, # HDF columns
    integer(hsize_t)                    :: startrow                 ! Row number (zero-based indexing)
    integer(hsize_t)                    :: startcol                 ! Row number (zero-based indexing)
    logical                             :: compressed               ! Chunk and compress dataset
    integer(kind=c_int), &
      dimension(ncols_hdf, nrows_hdf) :: hdf_array                  ! Data array
    integer(kind=c_int), &
      dimension(ncols_hdf, nrows_hdf) :: hdf_array_in               ! Data array
    integer(kind=c_int), &
      dimension(ncols_hdf, nrows_hdf) :: hdf_array_out              ! Data array
    integer(hid_t)                      :: data_type                ! Data type ID
    logical, parameter                  :: chunk_in_time = .true.   ! Chunk dataset in time
    integer, parameter                  :: kind_nbytes = 4          ! Type precision (number of bytes)
    integer                             :: i, j                     ! Loop variables
    integer(hid_t)                      :: hdf_error
    logical                             :: is_ok                    ! Function return condition
    logical                             :: link_exists              ! Function return condition
    !
    success = .false.
    !
    infile_id       = 0
    outfile_id      = 0
    ingroup_id      = 0
    outgroup_id     = 0
    indataset_id    = 0
    outdataset_id   = 0
    infile          = HDF_INFILE
    outfile         = "F2F_h5_write_whole_dataset_2d_array_i.h5"
    ingroup_name    = "/Geometry/Cross Sections"
    outgroup_name   = "/Geometry"
    indataset_name  = "Polyline Info"
    outdataset_name = "Polyline Info"
    compressed = .true.
    data_type = TEST_H5T_NATIVE_INTEGER
    !
    ! Open input file
    is_ok = h5_open_file(infile, file_access_flag, infile_id)
    if (.not. is_ok) return
    !
    ! Check if group exists
    is_ok = h5_group_exists(infile_id, ingroup_name, link_exists)
    if (.not. is_ok) return
    !
    ! Open existing group
    is_ok = h5_open_group(infile_id, ingroup_name, ingroup_id)
    if (.not. is_ok) return
    !
    ! Open existing dataset
    is_ok = h5_open_dataset(ingroup_id, indataset_name, indataset_id)
    if (.not. is_ok) return
    !
    ! Create output file
    is_ok = h5_create_file(outfile, TRUNCATE, outfile_id)
    if (.not. is_ok) return
    !
    ! Create output group
    is_ok = h5_create_group(outfile_id, outgroup_name, outgroup_id)
    if (.not. is_ok) return
    !
    ! Create compressed 2D output dataset
    is_ok = h5_create_2d_dataset(outgroup_id, outdataset_name, nrows_hdf, ncols_hdf, data_type, chunk_in_time, kind_nbytes, compressed, outdataset_id)
    if (.not. is_ok) return
    !
    ! Read whole dataset from input file and write whole dataset to output file
    startrow = 0
    startcol = 0
    is_ok = h5_read_dataset_2d_array_i(indataset_id, startrow, startcol, nrows_hdf, ncols_hdf, hdf_array)
    if (.not. is_ok) return
    is_ok = h5_write_dataset_2d_array_i(outdataset_id, startrow, startcol, nrows_hdf, ncols_hdf, hdf_array)
    if (.not. is_ok) return
    !    
    ! Close both files
    is_ok = h5_close_dataset(indataset_id)
    is_ok = h5_close_group(ingroup_id)
    is_ok = h5_close_file(infile_id)
    is_ok = h5_close_dataset(outdataset_id)
    is_ok = h5_close_group(outgroup_id)
    is_ok = h5_close_file(outfile_id)
    !
    ! Reopen both files
    ! Note: The output file was in write/truncate mode. Now it will be opened read-only mode.
    is_ok = h5_open_file(infile, file_access_flag, infile_id)
    is_ok = h5_open_file(outfile, file_access_flag, outfile_id)
    is_ok = h5_open_group(infile_id, ingroup_name, ingroup_id)
    is_ok = h5_open_group(outfile_id, outgroup_name, outgroup_id)
    is_ok = h5_open_dataset(ingroup_id, indataset_name, indataset_id)
    is_ok = h5_open_dataset(outgroup_id, outdataset_name, outdataset_id)
    !
    ! Now verify the output data written to file - one row at a time
    do i = 1, nrows_hdf
      startrow = i - 1
      startcol = 0
      is_ok = h5_read_dataset_2d_array_i(indataset_id, startrow, startcol, nrows_hdf, ncols_hdf, hdf_array_in)  ! Reading from the original input file
      is_ok = h5_read_dataset_2d_array_i(outdataset_id, startrow, startcol, nrows_hdf, ncols_hdf, hdf_array_out) ! Reading from the output file produced above
      do j = 1, ncols_hdf
        is_ok = are_equal_i(hdf_array_in(j,1), hdf_array_out(j,1))
        if (.not. is_ok) return
      end do
    end do
    !
    ! Close input and output datasets
    is_ok = h5_close_dataset(indataset_id)
    if (.not. is_ok) return
    !
    is_ok = h5_close_dataset(outdataset_id)
    if (.not. is_ok) return
    !
    ! Close input and output groups
    is_ok = h5_close_group(ingroup_id)
    if (.not. is_ok) return
    !
    is_ok = h5_close_group(outgroup_id)
    if (.not. is_ok) return
    !
    ! Close input and output files
    is_ok = h5_close_file(infile_id)
    if (.not. is_ok) return
    !
    is_ok = h5_close_file(outfile_id)
    if (.not. is_ok) return
    !
    ! Close HDF5 interface
    is_ok = h5_terminate()
    if (.not. is_ok) return
    !
    success = .true.
  end function
  !
  ! ...............................................................................................
  !
  ! Test writing whole 2D dataset: Integer type
  ! ...............................................................................................
  logical function test_h5_write_dataset_2d_array_i_by_row(infile_id) result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_write_dataset_2d_array_i_by_row" :: test_h5_write_dataset_2d_array_i_by_row
    integer(hid_t), intent(out)         :: infile_id                ! Input file ID
    integer(hid_t)                      :: file_access_flag = READ_ONLY ! File access flag for input
    integer(hid_t)                      :: outfile_id               ! Output file ID
    integer(hid_t)                      :: ingroup_id               ! Input group ID
    integer(hid_t)                      :: outgroup_id              ! Output group ID
    integer(hid_t)                      :: indataset_id             ! Input dataset ID
    integer(hid_t)                      :: outdataset_id            ! Output dataset ID
    character(len=STD_STR_LEN)          :: infile                   ! Input file
    character(len=STD_STR_LEN)          :: outfile                  ! Output file
    character(len=STD_STR_LEN)          :: ingroup_name             ! Input group name
    character(len=STD_STR_LEN)          :: outgroup_name            ! Output group name
    character(len=STD_STR_LEN)          :: indataset_name           ! Input dataset name
    character(len=STD_STR_LEN)          :: outdataset_name          ! Output dataset name
    integer(hsize_t), parameter         :: nrows_hdf = 334          ! # Fortran columns, # HDF rows
    integer(hsize_t), parameter         :: ncols_hdf = 4            ! # Fortran rows, # HDF columns
    integer(hsize_t), parameter         :: nrows_subset = 1         ! # Fortran columns, # HDF rows
    integer(hsize_t), parameter         :: ncols_subset = ncols_hdf ! # Fortran rows, # HDF columns
    integer(hsize_t)                    :: startrow                 ! Row number (zero-based indexing)
    integer(hsize_t)                    :: startcol                 ! Row number (zero-based indexing)
    logical                             :: compressed               ! Chunk and compress dataset
    integer(kind=c_int), &
      dimension(ncols_subset, nrows_subset) :: hdf_array            ! Data array
    integer(kind=c_int), &
      dimension(ncols_subset, nrows_subset) :: hdf_array_in         ! Data array
    integer(kind=c_int), &
      dimension(ncols_subset, nrows_subset) :: hdf_array_out        ! Data array
    integer(hid_t)                      :: data_type                ! Data type ID
    logical, parameter                  :: chunk_in_time = .true.   ! Chunk dataset in time
    integer, parameter                  :: kind_nbytes = 4          ! Type precision (number of bytes)
    integer                             :: i, j                     ! Loop variables
    integer(hid_t)                      :: hdf_error
    logical                             :: is_ok                    ! Function return condition
    logical                             :: link_exists              ! Function return condition
    !
    success = .false.
    !
    infile_id       = 0
    outfile_id      = 0
    ingroup_id      = 0
    outgroup_id     = 0
    indataset_id    = 0
    outdataset_id   = 0
    infile          = HDF_INFILE
    outfile         = "F2F_h5_write_whole_dataset_2d_array_i_by_row.h5"
    ingroup_name    = "/Geometry/Cross Sections"
    outgroup_name   = "/Geometry"
    indataset_name  = "Polyline Info"
    outdataset_name = "Polyline Info"
    compressed = .true.
    data_type = TEST_H5T_NATIVE_INTEGER
    !
    ! Open input file
    is_ok = h5_open_file(infile, file_access_flag, infile_id)
    if (.not. is_ok) return
    !
    ! Check if group exists
    is_ok = h5_group_exists(infile_id, ingroup_name, link_exists)
    if (.not. is_ok) return
    !
    ! Open existing group
    is_ok = h5_open_group(infile_id, ingroup_name, ingroup_id)
    if (.not. is_ok) return
    !
    ! Open existing dataset
    is_ok = h5_open_dataset(ingroup_id, indataset_name, indataset_id)
    if (.not. is_ok) return
    !
    ! Create output file
    is_ok = h5_create_file(outfile, TRUNCATE, outfile_id)
    if (.not. is_ok) return
    !
    ! Create output group
    is_ok = h5_create_group(outfile_id, outgroup_name, outgroup_id)
    if (.not. is_ok) return
    !
    ! Create compressed 2D output dataset
    is_ok = h5_create_2d_dataset(outgroup_id, outdataset_name, nrows_hdf, ncols_hdf, data_type, chunk_in_time, kind_nbytes, compressed, outdataset_id)
    if (.not. is_ok) return
    !
    ! Read dataset from input file by row and write to output file by row
    do i = 1, nrows_hdf
      startrow = i - 1
      startcol = 0
      is_ok = h5_read_dataset_2d_array_i(indataset_id, startrow, startcol, nrows_subset, ncols_subset, hdf_array)
      if (.not. is_ok) return
      is_ok = h5_write_dataset_2d_array_i(outdataset_id, startrow, startcol, nrows_subset, ncols_subset, hdf_array)
      if (.not. is_ok) return
    end do
    !    
    ! Close both files
    is_ok = h5_close_dataset(indataset_id)
    is_ok = h5_close_group(ingroup_id)
    is_ok = h5_close_file(infile_id)
    is_ok = h5_close_dataset(outdataset_id)
    is_ok = h5_close_group(outgroup_id)
    is_ok = h5_close_file(outfile_id)
    !
    ! Reopen both files
    ! Note: The output file was in write/truncate mode. Now it will be opened read-only mode.
    is_ok = h5_open_file(infile, file_access_flag, infile_id)
    is_ok = h5_open_file(outfile, file_access_flag, outfile_id)
    is_ok = h5_open_group(infile_id, ingroup_name, ingroup_id)
    is_ok = h5_open_group(outfile_id, outgroup_name, outgroup_id)
    is_ok = h5_open_dataset(ingroup_id, indataset_name, indataset_id)
    is_ok = h5_open_dataset(outgroup_id, outdataset_name, outdataset_id)
    !
    ! Now verify the output data written to file - one row at a time
    do i = 1, nrows_hdf
      startrow = i - 1
      startcol = 0
      is_ok = h5_read_dataset_2d_array_i(indataset_id, startrow, startcol, nrows_subset, ncols_subset, hdf_array_in)  ! Reading from the original input file
      is_ok = h5_read_dataset_2d_array_i(outdataset_id, startrow, startcol, nrows_subset, ncols_subset, hdf_array_out) ! Reading from the output file produced above
      do j = 1, ncols_hdf
        is_ok = are_equal_i(hdf_array_in(j,1), hdf_array_out(j,1))
        if (.not. is_ok) return
      end do
    end do
    !
    ! Close input and output datasets
    is_ok = h5_close_dataset(indataset_id)
    if (.not. is_ok) return
    !
    is_ok = h5_close_dataset(outdataset_id)
    if (.not. is_ok) return
    !
    ! Close input and output groups
    is_ok = h5_close_group(ingroup_id)
    if (.not. is_ok) return
    !
    is_ok = h5_close_group(outgroup_id)
    if (.not. is_ok) return
    !
    ! Close input and output files
    is_ok = h5_close_file(infile_id)
    if (.not. is_ok) return
    !
    is_ok = h5_close_file(outfile_id)
    if (.not. is_ok) return
    !
    ! Close HDF5 interface
    is_ok = h5_terminate()
    if (.not. is_ok) return
    !
    success = .true.
  end function
  !
  ! ...............................................................................................
  !
  ! Test writing 1D string array
  ! ...............................................................................................
  logical function test_h5_write_dataset_1d_array_c(infile_id) result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_write_dataset_1d_array_c" :: test_h5_write_dataset_1d_array_c
    integer(hid_t), intent(out)         :: infile_id
    integer(hid_t)                      :: file_access_flag     ! File access flag for input
    integer(hid_t)                      :: outfile_id           ! Output file ID
    integer(hid_t)                      :: ingroup_id           ! Input group ID
    integer(hid_t)                      :: outgroup_id          ! Output group ID
    integer(hid_t)                      :: indataset_id         ! Input dataset ID
    integer(hid_t)                      :: outdataset_id        ! Output dataset ID
    character(len=STD_STR_LEN)          :: infile               ! Input file
    character(len=STD_STR_LEN)          :: outfile              ! Output file
    character(len=STD_STR_LEN)          :: ingroup_name         ! Input group name
    character(len=STD_STR_LEN)          :: outgroup_name        ! Output group name
    character(len=STD_STR_LEN)          :: indataset_name       ! Input dataset name
    character(len=STD_STR_LEN)          :: outdataset_name      ! Output dataset name
    integer(hsize_t), parameter         :: nvals = 334          ! Array length
    integer, parameter                  :: str_len = 512        ! Dataset string length
    integer(hsize_t)                    :: startrow
    character(len=512), dimension(nvals) :: hdf_array           ! String array
    integer(hid_t)                      :: data_type            ! Data type ID
    logical, parameter                  :: chunk_in_time = .true.  ! Chunk dataset in time
    integer, parameter                  :: kind_nbytes = 4      ! Type precision (number of bytes)
    integer(hid_t)                      :: hdf_error            ! HDF5 error flag
    logical                             :: is_ok                ! Function return condition
    logical                             :: link_exists          ! Function return condition
    logical                             :: compressed = .true.  ! Chunk and compress dataset
    !
    success = .false.
    !
    infile_id       = 0
    outfile_id      = 0
    ingroup_id      = 0
    outgroup_id     = 0
    indataset_id    = 0
    outdataset_id   = 0
    infile          = HDF_INFILE
    outfile = "F2F_h5_write_dataset_1d_array_c.h5"
    ingroup_name = "/Geometry/Cross Sections"
    indataset_name = "Node Descriptions"
    outgroup_name = "/Geometry"
    outdataset_name = "Node Descriptions"
    data_type = -99 ! Defined with call to h5_get_string_datatype() below
    startrow = 0
    !
    ! Make object large enough to hold a Fortran array of strings:
    ! Character(len=str_len), dimension(nvals)
    ! Character*str_len(nvals)
    !
    ! Read data
    !
    file_access_flag = READ_ONLY
    is_ok = h5_open_file(infile, file_access_flag, infile_id)
    if (.not. is_ok) return
    !
    is_ok = h5_open_group(infile_id, ingroup_name, ingroup_id)
    if (.not. is_ok) return
    !
    is_ok = h5_open_dataset(ingroup_id, indataset_name, indataset_id)
    if (.not. is_ok) return
    !
    is_ok = h5_read_dataset_1d_array_c(indataset_id, startrow, nvals, str_len, hdf_array)
    if (.not. is_ok) return
    !
    ! Write data
    !
    ! Create output file
    file_access_flag = TRUNCATE
    is_ok = h5_create_file(outfile, file_access_flag, outfile_id)
    if (.not. is_ok) return
    !
    ! Create output group
    is_ok = h5_create_group(outfile_id, outgroup_name, outgroup_id)
    if (.not. is_ok) return
    !
    ! Get string data type
    is_ok = h5_get_string_datatype(str_len, data_type)
    if (.not. is_ok) return
    !
    ! Create output dataset
    !is_ok = h5_create_compressed_1d_dataset(outgroup_id, outdataset_name, nvals, data_type, kind_nbytes, outdataset_id)
    is_ok = h5_create_1d_dataset_c(outgroup_id, outdataset_name, nvals, str_len, compressed, outdataset_id)
    if (.not. is_ok) return
    !
    ! Write dataset
    is_ok = h5_write_dataset_1d_array_c(outdataset_id, startrow, nvals, str_len, hdf_array)
    if (.not. is_ok) return
    !
    is_ok = h5_close_dataset(outdataset_id)
    is_ok = h5_close_group(outgroup_id)
    is_ok = h5_close_file(outfile_id) 
    ! 
    success = .true.
  end function
  !
  !
  ! ...............................................................................................
  !
  ! HDF Group Example: reading and writing a compound dataset, Fortran 2003 version
  ! ...............................................................................................
  !
  ! This example shows how to create a compound data type,
  ! write an array which has the compound data type to the file,
  ! and read back fields' subsets.
  !
  logical function test_compound_example_f2003() result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_compound_example_f2003" :: test_compound_example_f2003
    use hdf5
    use iso_c_binding
    implicit none
    !
    ! Kind parameters
    integer, parameter :: int_k1 = selected_int_kind(1)  ! This should map to INTEGER*1 on most modern processors
    integer, parameter :: int_k2 = selected_int_kind(4)  ! This should map to INTEGER*2 on most modern processors
    integer, parameter :: int_k4 = selected_int_kind(8)  ! This should map to INTEGER*4 on most modern processors
    integer, parameter :: int_k8 = selected_int_kind(16) ! This should map to INTEGER*8 on most modern processors
    integer, parameter :: r_k4 = selected_real_kind(5)   ! This should map to REAL*4 on most modern processors
    integer, parameter :: r_k8 = selected_real_kind(10)  ! This should map to REAL*8 on most modern processors
    !
    ! Files
    character(len=*), parameter :: h5file_name = "SDScompound.h5"
    character(len=*), parameter :: datasetname = "ArrayOfStructures"
    !
    ! Dimensions
    integer, parameter :: length = 10
    integer, parameter :: rank = 1
    integer, parameter :: str_len = 13
    !
    !----------------------------------------------------------------
    ! First derived-type and dataset
    type s1_t
      character(len=1), dimension(1:str_len) :: chr
      integer(kind=int_k1) :: a
      real(kind=r_k4) :: b
      real(kind=r_k8) :: c
    end type s1_t
    !
    type(s1_t), dimension(length), target :: s1
    integer(hid_t) :: s1_tid  ! File datatype identifier
    !
    !----------------------------------------------------------------
    ! Second derived-type (subset of s1_t) and dataset
    type s2_t
       character(len=1), dimension(1:str_len) :: chr
       real(kind=r_k8) :: c
       integer(kind=int_k1) :: a
    end type s2_t
    !
    type(s2_t), dimension(length), target :: s2
    integer(hid_t) :: s2_tid    ! Memory datatype handle
    !
    !----------------------------------------------------------------
    ! Third "derived-type" (will be used to read float field of s1)
    integer(hid_t) :: s3_tid   ! memory datatype handle
    real(kind=r_k4), target :: s3(length)
    integer :: i
    integer(hid_t) :: file, dataset, space
    integer(hsize_t) :: dim(1) = (/length/)   ! dataspace dimensions
    integer(size_t) :: type_size  ! size of the datatype
    integer(size_t) :: offset, sizeof_compound
    integer :: hdf_error
    type(c_ptr) :: f_ptr
    integer(size_t) :: type_size_i  ! size of the integer datatype 
    integer(size_t) :: type_size_r  ! size of the real datatype 
    integer(size_t) :: type_size_d  ! size of the double datatype 
    integer(hid_t) :: tid3          ! /* nested array datatype id	*/
    integer(hsize_t), dimension(1) :: tdims1=(/str_len/)
    !
    success = .false.
    !
    ! Initialize Fortran interface
    call h5open_f(hdf_error)
    !
    ! Initialize the data
    do i = 1, length
       s1(i)%chr(1)(1:1)    = 'a'
       s1(i)%chr(2)(1:1)    = 'b'
       s1(i)%chr(3)(1:1)    = 'c'
       s1(i)%chr(4:12)(1:1) = ' '
       s1(i)%chr(13)(1:1)   = 'd'
       s1(i)%a = i
       s1(i)%b = i*i
       s1(i)%c = 1./real(i)
    end do
    !
    ! Create the data space
    call h5screate_simple_f(rank, dim, space, hdf_error)
    if (hdf_error < 0) return
    !
    ! Create the file
    call h5fcreate_f(h5file_name, H5F_ACC_TRUNC_F, file, hdf_error)
    if (hdf_error < 0) return
    !
    ! Create the memory data type
    offset = h5offsetof(c_loc(s1(1)), c_loc(s1(2)))
    call h5tcreate_f(H5T_COMPOUND_F, offset, s1_tid, hdf_error)
    if (hdf_error < 0) return
    !
    call h5tarray_create_f(H5T_NATIVE_CHARACTER, 1, tdims1, tid3, hdf_error)
    if (hdf_error < 0) return
    !
    offset = h5offsetof(c_loc(s1(1)), c_loc(s1(1)%chr))
    call h5tinsert_f(s1_tid, "chr_name", offset, tid3, hdf_error)
    if (hdf_error < 0) return
    !
    offset = h5offsetof(c_loc(s1(1)), c_loc(s1(1)%a))
    call h5tinsert_f(s1_tid, "a_name", offset, h5kind_to_type(int_k1, H5_INTEGER_KIND), hdf_error)
    if (hdf_error < 0) return
    !
    offset = h5offsetof(c_loc(s1(1)),c_loc(s1(1)%c))
    call h5tinsert_f(s1_tid, "c_name", offset, h5kind_to_type(r_k8, H5_REAL_KIND), hdf_error)
    if (hdf_error < 0) return
    !
    offset = h5offsetof(c_loc(s1(1)),c_loc(s1(1)%b))
    call h5tinsert_f(s1_tid, "b_name", offset, h5kind_to_type(r_k4, H5_REAL_KIND), hdf_error)
    if (hdf_error < 0) return
    !
    ! Create the dataset
    call h5dcreate_f(file, datasetname, s1_tid, space, dataset, hdf_error)
    if (hdf_error < 0) return
    !
    ! Write data to the dataset
    f_ptr = c_loc(s1(1))
    call h5dwrite_f(dataset, s1_tid, f_ptr, hdf_error)
    if (hdf_error < 0) return
    !
    ! Release resources
    call h5tclose_f(s1_tid, hdf_error)
    if (hdf_error < 0) return
    call h5sclose_f(space, hdf_error)
    if (hdf_error < 0) return
    call h5dclose_f(dataset, hdf_error)
    if (hdf_error < 0) return
    call h5fclose_f(file, hdf_error)
    if (hdf_error < 0) return
    !
    ! Open the file and the dataset
    call h5fopen_f(h5file_name, H5F_ACC_RDONLY_F, file, hdf_error)
    if (hdf_error < 0) return
    call h5dopen_f(file, datasetname, dataset, hdf_error)
    if (hdf_error < 0) return
    !
    ! Create a data type for s2
    offset = h5offsetof(c_loc(s2(1)), c_loc(s2(2)))
    call h5tcreate_f(h5t_compound_f, offset, s2_tid, hdf_error)
    if (hdf_error < 0) return
    !
    offset = h5offsetof(c_loc(s2(1)), c_loc(s2(1)%chr))
    call h5tinsert_f(s2_tid, "chr_name", offset, tid3, hdf_error)
    if (hdf_error < 0) return
    !
    offset = h5offsetof(c_loc(s2(1)), c_loc(s2(1)%c))
    call h5tinsert_f(s2_tid, "c_name", offset, h5kind_to_type(r_k8, H5_REAL_KIND), hdf_error)
    if (hdf_error < 0) return
    !
    offset = h5offsetof(c_loc(s2(1)), c_loc(s2(1)%a))
    call h5tinsert_f(s2_tid, "a_name", offset, h5kind_to_type(int_k1, H5_INTEGER_KIND), hdf_error)
    if (hdf_error < 0) return
    !
    ! Read two fields c and a from s1 dataset. Fields in the file
    ! are found by their names "c_name" and "a_name".
    s2(:)%c = -1
    s2(:)%a = -1
    f_ptr = c_loc(s2(1))
    !
    call h5dread_f(dataset, s2_tid, f_ptr, hdf_error)
    if (hdf_error < 0) return
    !
    ! Display the fields
    do i = 1, length
      write(*,'(/,A,/,999(A,1X))') "Field chr :", s2(i)%chr(1:str_len)(1:1)
    enddo
    write(*,'(/,A,/,999(F8.4,1X))') "Field c :", s2(:)%c
    write(*,'(/,A,/,999(I0,1X))') "Field a :", s2(:)%a
    !
    ! Create a data type for s3
    offset = h5offsetof(c_loc(s3(1)), c_loc(s3(2)))
    call h5tcreate_f(H5T_COMPOUND_F, offset, s3_tid, hdf_error)
    if (hdf_error < 0) return
    !
    call h5tinsert_f(s3_tid, "b_name", 0_size_t, h5kind_to_type(r_k4, H5_REAL_KIND), hdf_error)
    if (hdf_error < 0) return
    !
    ! Read field b from s1 dataset. Field in the file is found by its name.
    s3(:) = -1
    f_ptr = c_loc(s3(1))
    call h5dread_f(dataset, s3_tid, f_ptr, hdf_error)
    if (hdf_error < 0) return
    !
    ! Display the field
    write(*,'(/,A,/,999(F8.4,1X))') "Field b :",s3(:)
    !
    ! Release resources
    call h5tclose_f(s2_tid, hdf_error)
    if (hdf_error < 0) return
    call h5tclose_f(s3_tid, hdf_error)
    if (hdf_error < 0) return
    call h5dclose_f(dataset, hdf_error)
    if (hdf_error < 0) return
    call h5fclose_f(file, hdf_error)
    if (hdf_error < 0) return
    !
    success = .true.
  end function test_compound_example_f2003
  !
  !
  ! ...............................................................................................
  !
  ! HDF Group Example: reading and writing a compound dataset, Fortran 90/95 version
  ! ...............................................................................................
  !
  ! This program creates a dataset that is one dimensional array of
  ! structures  {
  !                 character*2
  !                 integer
  !                 double precision
  !                 real
  !                                   }
  ! Data is written and read back by fields.
  !
  logical function test_compound_example_f95() result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_compound_example_f95" :: test_compound_example_f95
    use iso_c_binding
    implicit none
    !
    character(len=11), parameter       :: filename = "compound.h5" ! file name
    character(len=8), parameter        :: dsetname = "compound"     ! dataset name
    integer, parameter                 :: nrows = 6       ! Size of the dataset
    integer(hid_t)                     :: file_id         ! File identifier
    integer(hid_t)                     :: dataset_id      ! Dataset identifier
    integer(hid_t)                     :: dspace_id       ! Dataspace identifier
    integer(hid_t)                     :: dtype_id        ! Compound datatype identifier
    integer(hid_t)                     :: mtype_id_c      ! Memory datatype identifier (for character field)
    integer(hid_t)                     :: mtype_id_i      ! Memory datatype identifier (for integer field)
    integer(hid_t)                     :: mtype_id_d      ! Memory datatype identifier (for double precision field)
    integer(hid_t)                     :: mtype_id_r      ! Memory datatype identifier (for real field)
    integer(hid_t)                     :: mtype_id_char   ! Memory datatype identifier
    integer(hid_t)                     :: plist_id        ! Dataset transfer property
    ! Specify number of characters to write to each field. 
    ! Setting num_characters = 2 will result in: BB, CC, DD, EE, FF, GG
    ! Setting num_characters = 3 will result in: BBC, CDD, EEF, FGG, ___, ___
    integer(size_t), parameter         :: num_characters = 2
    integer(hsize_t), dimension(1)     :: dims = (/nrows/) ! Dataset dimensions
    integer                            :: rank = 1        ! Dataset rank
    integer                            :: hdf_error       ! HDF error flag
    integer(size_t)                    :: type_size_compound ! Size of the compound datatype (bytes)
    integer(size_t)                    :: type_size_c     ! Size of the character datatype (bytes)
    integer(size_t)                    :: type_size_i     ! Size of the integer datatype (bytes)
    integer(size_t)                    :: type_size_d     ! Size of the double precision datatype (bytes)
    integer(size_t)                    :: type_size_r     ! Size of the real datatype (bytes)
    integer(size_t)                    :: offset          ! Member's offset
    character(len=2), dimension(nrows) :: char_member
    character(len=2), dimension(nrows) :: char_member_out ! Buffer to read data out
    integer, dimension(nrows)          :: int_member
    double precision, dimension(nrows) :: double_member
    real, dimension(nrows)             :: real_member
    integer                            :: i
    integer(hsize_t), dimension(1)     :: data_dims
    !
    success = .false.
    !
    data_dims(1) = nrows
    !
    ! Create data to be written to HDF5
    do i = 1, nrows
      char_member(i)(1:1) = char(65+i)
      char_member(i)(2:2) = char(65+i)
      char_member_out(i)(1:1) = char(65)
      char_member_out(i)(2:2) = char(65)
      int_member(i) = i
      double_member(i) = 2.* i
      real_member(i) = 3. * i
    enddo
    !
    ! Initialize Fortran interface
    call h5open_f(hdf_error)
    if (hdf_error < 0) return
    !
    ! Create properties list for (raw) dataset transfer
    call h5pcreate_f(H5P_DATASET_XFER_F, plist_id, hdf_error)
    if (hdf_error < 0) return
    !
    ! Set dataset transfer property to preserve partially initialized fields
    ! during write/read to/from dataset with compound datatype.
    !
    ! Note: This function is deprecated as it no longer has any effect; compound 
    !  datatype field preservation is now core functionality in the HDF5 Library.
    !  Source: https://support.hdfgroup.org/HDF5/doc/RM/RM_H5P.html#Property-Create
    call h5pset_preserve_f(plist_id, .TRUE., hdf_error)
    if (hdf_error < 0) return
    !
    ! Create a new file using default properties
    call h5fcreate_f(filename, H5F_ACC_TRUNC_F, file_id, hdf_error)
    if (hdf_error < 0) return
    !
    ! Create the dataspace
    ! In this example, rank = 1, dims = nrows = 6
    call h5screate_simple_f(rank, dims, dspace_id, hdf_error)
    if (hdf_error < 0) return
    !
    ! --- Create compound datatype ---
    !
    ! First calculate total size by calculating sizes of each member
    call h5tcopy_f(H5T_NATIVE_CHARACTER, mtype_id_char, hdf_error)
    if (hdf_error < 0) return
    !
    call h5tset_size_f(mtype_id_char, num_characters, hdf_error)
    if (hdf_error < 0) return
    !
    call h5tget_size_f(mtype_id_char, type_size_c, hdf_error)
    if (hdf_error < 0) return
    !
    call h5tget_size_f(H5T_NATIVE_INTEGER, type_size_i, hdf_error)
    if (hdf_error < 0) return
    !
    call h5tget_size_f(H5T_NATIVE_DOUBLE, type_size_d, hdf_error)
    if (hdf_error < 0) return
    !
    call h5tget_size_f(H5T_NATIVE_REAL, type_size_r, hdf_error)
    if (hdf_error < 0) return
    type_size_compound = type_size_c + type_size_i + type_size_d + type_size_r
    !
    call h5tcreate_f(H5T_COMPOUND_F, type_size_compound, dtype_id, hdf_error)
    if (hdf_error < 0) return
    !
    ! --- Insert members ---
    !
    ! Character*2 member
    offset = 0
    call h5tinsert_f(dtype_id, "char_field", offset, mtype_id_char, hdf_error)
    if (hdf_error < 0) return
    !
    ! Integer member
    offset = offset + type_size_c ! Offset of the second memeber is 2
    call h5tinsert_f(dtype_id, "integer_field", offset, H5T_NATIVE_INTEGER, hdf_error)
    if (hdf_error < 0) return
    !
    ! Double Precision member
    offset = offset + type_size_i  ! Offset of the third memeber is 6
    call h5tinsert_f(dtype_id, "double_field", offset, H5T_NATIVE_DOUBLE, hdf_error)
    if (hdf_error < 0) return
    !
    ! Real member
    offset = offset + type_size_d  ! Offset of the last member is 14
    call h5tinsert_f(dtype_id, "real_field", offset, H5T_NATIVE_REAL, hdf_error)
    if (hdf_error < 0) return
    !
    ! Create the dataset with compound datatype.
    call h5dcreate_f(file_id, dsetname, dtype_id, dspace_id, dataset_id, hdf_error)
    if (hdf_error < 0) return
    !
    ! Create memory types. We have to create a compound datatype
    ! for each member we want to write.
    ! The order of insertion will determine the order of the fields. They may
    ! then be written in any order.
    offset = 0 ! Note: this will be zero for each inserted type
    !
    ! Create and insert: character
    call h5tcreate_f(H5T_COMPOUND_F, type_size_c, mtype_id_c, hdf_error)
    if (hdf_error < 0) return
    call h5tinsert_f(mtype_id_c, "char_field", offset, mtype_id_char, hdf_error)
    if (hdf_error < 0) return
    !
    ! Create and insert: integer
    call h5tcreate_f(H5T_COMPOUND_F, type_size_i, mtype_id_i, hdf_error)
    if (hdf_error < 0) return
    call h5tinsert_f(mtype_id_i, "integer_field", offset, H5T_NATIVE_INTEGER, hdf_error)
    if (hdf_error < 0) return
    !
    ! Create and insert: double
    call h5tcreate_f(H5T_COMPOUND_F, type_size_d, mtype_id_d, hdf_error)
    if (hdf_error < 0) return
    call h5tinsert_f(mtype_id_d, "double_field", offset, H5T_NATIVE_DOUBLE, hdf_error)
    if (hdf_error < 0) return
    !
    ! Create and insert: real
    call h5tcreate_f(H5T_COMPOUND_F, type_size_r, mtype_id_r, hdf_error)
    if (hdf_error < 0) return
    call h5tinsert_f(mtype_id_r, "real_field", offset, H5T_NATIVE_REAL, hdf_error)
    if (hdf_error < 0) return
    !
    ! Write data by fields in the datatype. Field order is not important.
    ! Note: the order will be character, integer, double, real, but they
    ! are written as real, character, double, integer below:
    call h5dwrite_f(dataset_id, mtype_id_r, real_member, data_dims, hdf_error, xfer_prp = plist_id)
    if (hdf_error < 0) return
    !
    call h5dwrite_f(dataset_id, mtype_id_c, char_member, data_dims, hdf_error, xfer_prp = plist_id)
    if (hdf_error < 0) return
    !
    call h5dwrite_f(dataset_id, mtype_id_d, double_member, data_dims, hdf_error, xfer_prp = plist_id)
    if (hdf_error < 0) return
    !
    call h5dwrite_f(dataset_id, mtype_id_i, int_member, data_dims, hdf_error, xfer_prp = plist_id)
    if (hdf_error < 0) return
    !
    ! End access to the dataset and release resources used by it
    call h5dclose_f(dataset_id, hdf_error)
    if (hdf_error < 0) return
    !
    ! Terminate access to the data space
    call h5sclose_f(dspace_id, hdf_error)
    if (hdf_error < 0) return
    !
    ! Terminate access to the datatype
    call h5tclose_f(dtype_id, hdf_error)
    if (hdf_error < 0) return
    !
    ! Close data types
    call h5tclose_f(mtype_id_c, hdf_error)
    if (hdf_error < 0) return
    call h5tclose_f(mtype_id_i, hdf_error)
    if (hdf_error < 0) return
    call h5tclose_f(mtype_id_d, hdf_error)
    if (hdf_error < 0) return
    call h5tclose_f(mtype_id_r, hdf_error)
    if (hdf_error < 0) return
    call h5tclose_f(mtype_id_char, hdf_error)
    if (hdf_error < 0) return
    !
    ! Close the file
    call h5fclose_f(file_id, hdf_error)
    if (hdf_error < 0) return
    !
    !
    ! ------ Reading the compound datatype -------
    !
    ! Open the file
    call h5fopen_f (filename, H5F_ACC_RDWR_F, file_id, hdf_error)
    if (hdf_error < 0) return
    !
    ! Open the dataset
    call h5dopen_f(file_id, dsetname, dataset_id, hdf_error)
    if (hdf_error < 0) return
    !
    ! Create memory datatype to read character member of the compound datatype
    ! Why are we using the integer type identifier then????
    call h5tcopy_f(H5T_NATIVE_CHARACTER, mtype_id_i, hdf_error)
    !call h5tcopy_f(H5T_NATIVE_CHARACTER, mtype_id_char, hdf_error)
    if (hdf_error < 0) return
    !
    type_size_compound = 2
    call h5tset_size_f(mtype_id_i, type_size_compound, hdf_error)
    !call h5tset_size_f(mtype_id_char, type_size_compound, hdf_error)
    if (hdf_error < 0) return
    !
    call h5tget_size_f(mtype_id_i, type_size_compound, hdf_error)
    !call h5tget_size_f(mtype_id_char, type_size_compound, hdf_error)
    if (hdf_error < 0) return
    !
    call h5tcreate_f(H5T_COMPOUND_F, type_size_compound, mtype_id_c, hdf_error)
    !call h5tcreate_f(H5T_COMPOUND_F, type_size_compound, dtype_id, hdf_error)
    if (hdf_error < 0) return
    !
    offset = 0
    call h5tinsert_f(mtype_id_c, "char_field", offset, mtype_id_i, hdf_error)
    !call h5tinsert_f(dtype_id, "char_field", offset, mtype_id_char, hdf_error) ! Why mtype_id_char instead of mtype_id_c?
    if (hdf_error < 0) return
    !
    ! Read part of the datatset and display it
    call h5dread_f(dataset_id, mtype_id_c, char_member_out, data_dims, hdf_error)
    !call h5dread_f(dataset_id, mtype_id_c, char_member_out, data_dims, hdf_error)
    if (hdf_error < 0) return
    !
    write(*,*) (char_member_out(i), i=1, nrows)
    !
    ! Close all open objects
    call h5dclose_f(dataset_id, hdf_error)
    if (hdf_error < 0) return
    !
    call h5tclose_f(mtype_id_c, hdf_error)
    if (hdf_error < 0) return
    !
    call h5tclose_f(mtype_id_i, hdf_error)
    if (hdf_error < 0) return
    !
    call h5fclose_f(file_id, hdf_error)
    if (hdf_error < 0) return
    !
    ! Close Fortran interface
    call h5close_f(hdf_error)
    if (hdf_error < 0) return
    !
    success = .true.
    return
  end function test_compound_example_f95
  !
  !
  ! ...............................................................................................
  !
  ! Test reading integer column (field) from compound dataset
  ! ...............................................................................................
  logical function test_h5_read_compound_integer result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_read_compound_integer" :: test_h5_read_compound_integer
    !
    character(len=*), parameter     :: h5file_name = "compound.h5"  ! Input HDF5 filename
    character(len=*), parameter     :: dataset_name = "compound"    ! Input dataset
    integer(hid_t)                  :: file_id                      ! File identifier
    integer(hid_t)                  :: dataset_id                   ! Dataset identifier
    integer, parameter              :: nrows = 6                    ! Number of rows in dataset
    integer, parameter              :: ncols = 4                    ! Number of columns in dataset
    integer, parameter              :: column = 2                   ! Number of column to read (one-based)
    type(h5compound), allocatable, &
      dimension(:)                  :: h5compound_arr               ! Array of compound data types
    integer                         :: hdf_error                    ! HDF5 error flag
    logical                         :: is_ok                        ! Did the function return without error?
    !
    success = .false.
    !
    ! Open the file and the dataset
    call h5fopen_f(h5file_name, H5F_ACC_RDONLY_F, file_id, hdf_error)
    if (hdf_error < 0) return
    call h5dopen_f(file_id, dataset_name, dataset_id, hdf_error)
    if (hdf_error < 0) return
    !
    allocate(h5compound_arr(ncols))
    is_ok = h5_read_compound_integer(dataset_id, "integer_field", h5compound_arr(column)%int_arr, nrows)
    !
    success = .true.
  end function test_h5_read_compound_integer
  !
  !
  ! ...............................................................................................
  !
  ! Test reading real column (field) from compound dataset
  ! ...............................................................................................
  logical function test_h5_read_compound_real result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_read_compound_real" :: test_h5_read_compound_real
    !
    character(len=*), parameter     :: h5file_name = "compound.h5"  ! Input HDF5 filename
    character(len=*), parameter     :: dataset_name = "compound"    ! Input dataset
    integer(hid_t)                  :: file_id                      ! File identifier
    integer(hid_t)                  :: dataset_id                   ! Dataset identifier
    integer, parameter              :: nrows = 6                    ! Number of rows in dataset
    integer, parameter              :: ncols = 4                    ! Number of columns in dataset
    integer, parameter              :: column = 4                   ! Number of column to read (one-based)
    type(h5compound), allocatable, &
      dimension(:)                  :: h5compound_arr               ! Array of compound data types
    integer                         :: hdf_error                    ! HDF5 error flag
    logical                         :: is_ok                        ! Did the function return without error?
    !
    success = .false.
    !
    ! Open the file and the dataset
    call h5fopen_f(h5file_name, H5F_ACC_RDONLY_F, file_id, hdf_error)
    if (hdf_error < 0) return
    call h5dopen_f(file_id, dataset_name, dataset_id, hdf_error)
    if (hdf_error < 0) return
    !
    allocate(h5compound_arr(ncols))
    !allocate(h5compound_arr(column)%real_arr(nrows))
    is_ok = h5_read_compound_real(dataset_id, "real_field", h5compound_arr(column)%real_arr, nrows)
    !
    success = .true.
  end function test_h5_read_compound_real
  !
  !
  ! ...............................................................................................
  !
  ! Test reading double column (field) from compound dataset
  ! ...............................................................................................
  logical function test_h5_read_compound_double result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_read_compound_double" :: test_h5_read_compound_double
    !
    character(len=*), parameter     :: h5file_name = "compound.h5"  ! Input HDF5 filename
    character(len=*), parameter     :: dataset_name = "compound"    ! Input dataset
    integer(hid_t)                  :: file_id                      ! File identifier
    integer(hid_t)                  :: dataset_id                   ! Dataset identifier
    integer, parameter              :: nrows = 6                    ! Number of rows in dataset
    integer, parameter              :: ncols = 4                    ! Number of columns in dataset
    integer, parameter              :: column = 3                   ! Number of column to read (one-based)
    type(h5compound), allocatable, &
      dimension(:)                  :: h5compound_arr               ! Array of compound data types
    integer                         :: hdf_error                    ! HDF5 error flag
    logical                         :: is_ok                        ! Did the function return without error?
    !
    success = .false.
    !
    ! Open the file and the dataset
    call h5fopen_f(h5file_name, H5F_ACC_RDONLY_F, file_id, hdf_error)
    if (hdf_error < 0) return
    call h5dopen_f(file_id, dataset_name, dataset_id, hdf_error)
    if (hdf_error < 0) return
    !
    allocate(h5compound_arr(ncols))
    !allocate(h5compound_arr(column)%double_arr(nrows))
    is_ok = h5_read_compound_double(dataset_id, "double_field", h5compound_arr(column)%double_arr, nrows)
    !
    success = .true.
  end function test_h5_read_compound_double
  !
  !
  ! ...............................................................................................
  !
  ! Test reading string column (field) from compound dataset
  ! ...............................................................................................
  logical function test_h5_read_compound_string result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_read_compound_string" :: test_h5_read_compound_string
    !
    character(len=*), parameter     :: h5file_name = "compound.h5"  ! Input HDF5 filename
    character(len=*), parameter     :: dataset_name = "compound"    ! Input dataset
    integer(hid_t)                  :: file_id                      ! File identifier
    integer(hid_t)                  :: dataset_id                   ! Dataset identifier
    integer, parameter              :: nrows = 6                    ! Number of rows in dataset
    integer, parameter              :: ncols = 4                    ! Number of columns in dataset
    integer, parameter              :: column = 1                   ! Number of column to read (one-based)
    type(h5compound), allocatable, &
      dimension(:)                  :: h5compound_arr               ! Array of compound data types
    integer                         :: hdf_error                    ! HDF5 error flag
    logical                         :: is_ok                        ! Did the function return without error?
    !
    success = .false.
    !
    ! Open the file and the dataset
    call h5fopen_f(h5file_name, H5F_ACC_RDONLY_F, file_id, hdf_error)
    if (hdf_error < 0) return
    call h5dopen_f(file_id, dataset_name, dataset_id, hdf_error)
    if (hdf_error < 0) return
    !
    allocate(h5compound_arr(ncols))
    is_ok = h5_read_compound_string(dataset_id, "char_field", h5compound_arr(column)%char_arr, nrows)
    !
    success = .true.
  end function test_h5_read_compound_string
  !
  !
  ! ...............................................................................................
  !
  ! Test reading individual fields from a compound dataset
  ! ...............................................................................................
  logical function test_h5_read_compound_field result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_read_compound_field" :: test_h5_read_compound_field
    !
    character(len=*), parameter     :: h5file_name = "compound.h5"  ! Input HDF5 filename
    character(len=*), parameter     :: dataset_name = "compound"    ! Input dataset
    integer(hid_t)                  :: file_id                      ! File identifier
    integer(hid_t)                  :: dataset_id                   ! Dataset identifier
    integer, parameter              :: nrows = 6                    ! Number of rows in dataset
    integer, parameter              :: ncols = 4                    ! Number of columns in dataset
    integer                         :: column                       ! Column index in Fortran array
    type(h5compound), allocatable, &
      dimension(:)                  :: h5compound_arr               ! Array of compound data types
    integer                         :: hdf_error                    ! HDF5 error flag
    logical                         :: is_ok                        ! Did the function return without error?
    character(len=:), allocatable, &
      dimension(:)                  :: member_names                 ! Names of members (fields, columns)
    !
    success = .false.
    !
    member_names(1) = "char_field"
    member_names(2) = "int_field"
    member_names(3) = "double_field"
    member_names(4) = "real_field"
    !
    ! ************************************
    ! ************************************
    ! ************************************
    ! NOTE: THIS TEST CALLS A FUNCTION THAT APPEARS TO NO LONGER EXIST. CHECK
    ! OLDER SOURCES
    ! ************************************
    ! ************************************
    
    !! Open the file and the dataset
    !call h5fopen_f(h5file_name, H5F_ACC_RDONLY_F, file_id, hdf_error)
    !if (hdf_error < 0) return
    !call h5dopen_f(file_id, dataset_name, dataset_id, hdf_error)
    !if (hdf_error < 0) return
    !!
    !allocate(h5compound_arr(ncols))
    !is_ok = h5_read_compound_field(dataset_id, trim(member_names(1)), h5compound_arr(1)%char_arr, nrows)
    !if (.not. is_ok) return
    !is_ok = h5_read_compound_field(dataset_id, trim(member_names(2)), h5compound_arr(2)%int_arr, nrows)
    !if (.not. is_ok) return
    !is_ok = h5_read_compound_field(dataset_id, trim(member_names(3)), h5compound_arr(3)%double_arr, nrows)
    !if (.not. is_ok) return
    !is_ok = h5_read_compound_field(dataset_id, trim(member_names(4)), h5compound_arr(4)%real_arr, nrows)
    !if (.not. is_ok) return
    !!
    !success = .true.
  end function test_h5_read_compound_field
  !
  !
  ! ...............................................................................................
  !
  ! Test reading a compound dataset
  ! ...............................................................................................
  logical function test_h5_read_compound_dataset result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_read_compound_dataset" :: test_h5_read_compound_dataset
    !
    character(len=*), parameter     :: h5file_name = "compound.h5"  ! Input HDF5 filename
    character(len=*), parameter     :: dataset_name = "compound"    ! Input dataset
    integer(hid_t)                  :: file_id                      ! File identifier
    integer(hid_t)                  :: dataset_id                   ! Dataset identifier
    integer                         :: nrows                        ! Number of rows in dataset
                                                                    ! Note: this has been changed 
                                                                    ! to an output parameter in the 
                                                                    ! function below, which should return 
                                                                    ! nrows = 6
    type(h5compound), allocatable, &
      dimension(:)                  :: h5compound_arr               ! Array of compound data types
    integer                         :: hdf_error                    ! HDF5 error flag
    logical                         :: is_ok                        ! Did the function return without error?
    !
    success = .false.
    !
    ! Open the file and the dataset
    call h5fopen_f(h5file_name, H5F_ACC_RDONLY_F, file_id, hdf_error)
    if (hdf_error < 0) return
    call h5dopen_f(file_id, dataset_name, dataset_id, hdf_error)
    if (hdf_error < 0) return
    !
    is_ok = h5_read_compound_dataset(dataset_id, h5compound_arr, nrows)
    !
    success = .true.
  end function test_h5_read_compound_dataset
  !
  !
  ! ...............................................................................................
  !
  ! Test writing a compound dataset
  ! ...............................................................................................
  logical function test_h5_write_compound_dataset result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_write_compound_dataset" :: test_h5_write_compound_dataset
    !
    character(len=*), parameter         :: h5file_name = "compound_write.h5"  ! Input HDF5 filename
    character(len=*), parameter         :: dataset_name = "compound_write"    ! Input dataset
    integer(hid_t)                      :: file_id                      ! File identifier
    integer, parameter                  :: ncols = 4                    ! Number of columns (members) in dataset
    integer, parameter                  :: nrows = 6                    ! Number of rows in dataset
    type(h5compound), allocatable, &
      dimension(:)                      :: h5compound_arr               ! Array of compound data types
    integer                             :: hdf_error                    ! HDF5 error flag
    logical                             :: is_ok                        ! Did the function return without error?
    integer                             :: i                            ! Loop index
    character(len=2), dimension(nrows)  :: char_member
    integer, dimension(nrows)           :: int_member
    double precision, dimension(nrows)  :: double_member
    real, dimension(nrows)              :: real_member
    character(len=:), allocatable, &
      dimension(:)                      :: member_names
    integer                             :: str_len = 2
    integer                             :: member_name_len = 15
    !
    success = .false.
    !
    allocate(character(member_name_len) :: member_names(ncols)) ! Note special allocation for array of strings
    member_names(1) = "char_field"
    member_names(2) = "int_field"
    member_names(3) = "double_field"
    member_names(4) = "real_field"
    !
    ! Create data to be written to HDF5
    do i = 1, nrows
      char_member(i)(1:1) = char(65+i)
      char_member(i)(2:2) = char(65+i)
      int_member(i) = i
      double_member(i) = 2.* i
      real_member(i) = 3. * i
    enddo
    !
    ! Assign arrays to the compound datatype array
    allocate(h5compound_arr(ncols))
    h5compound_arr(1)%char_arr = char_member
    h5compound_arr(2)%int_arr = int_member
    h5compound_arr(3)%double_arr = double_member
    h5compound_arr(4)%real_arr = real_member
    h5compound_arr(1)%is_character = .true.
    h5compound_arr(2)%is_integer = .true.
    h5compound_arr(3)%is_double = .true.
    h5compound_arr(4)%is_real = .true.
    
    ! Initialize Fortran interface
    call h5open_f(hdf_error)
    if (hdf_error < 0) return
    !
    ! Create a new file using default properties
    call h5fcreate_f(h5file_name, H5F_ACC_TRUNC_F, file_id, hdf_error)
    if (hdf_error < 0) return
    !
    is_ok = h5_write_compound_dataset(file_id, trim(dataset_name), h5compound_arr, nrows, member_names)
    !
    ! Close the file
    call h5fclose_f(file_id, hdf_error)
    if (hdf_error < 0) return
    !
    success = .true.
  end function test_h5_write_compound_dataset
  !
  ! ...............................................................................................
  !
  ! Test writing a compound dataset
  ! ...............................................................................................
  logical function test_h5_write_compound_dataset_with_more_columns result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_write_compound_dataset_with_more_columns" :: test_h5_write_compound_dataset_with_more_columns
    !
    character(len=*), parameter         :: h5file_name = "compound_write_more_fields.h5"  ! Input HDF5 filename
    character(len=*), parameter         :: dataset_name = "compound_write"  ! Input dataset
    integer(hid_t)                      :: file_id                      ! File identifier
    integer, parameter                  :: ncols = 8                    ! Number of columns (members) in dataset
    integer, parameter                  :: nrows = 10                   ! Number of rows in dataset
    type(h5compound), allocatable, &
      dimension(:)                      :: h5compound_arr               ! Array of compound data types
    integer                             :: hdf_error                    ! HDF5 error flag
    logical                             :: is_ok                        ! Did the function return without error?
    integer                             :: i                            ! Loop index
    integer, parameter                  :: str_len = 20
    character(len=str_len), &
      dimension(nrows)                  :: char_member1, char_member2
    integer, dimension(nrows)           :: int_member1, int_member2
    double precision, dimension(nrows)  :: double_member1, double_member2
    real, dimension(nrows)              :: real_member1, real_member2
    character(len=:), allocatable, &
      dimension(:)                      :: member_names
    integer                             :: member_name_len = 15
    !
    success = .false.
    !
    allocate(character(member_name_len) :: member_names(ncols)) ! Note special allocation for array of strings
    member_names(1) = "char field1"
    member_names(2) = "real field1"
    member_names(3) = "char field2"
    member_names(4) = "double field1"
    member_names(5) = "int field1"
    member_names(6) = "double field2"
    member_names(7) = "real field2"
    member_names(8) = "int field2"

    ! Create data to be written to HDF5
    char_member1 = ["first one", "second one", "third one", "fourth one", "fifth one", "sixth one", "seventh one", "eighth one", "ninth one", "tenth one"]
    char_member2 = ["first two", "second two", "third two", "fourth two", "fifth two", "sixth two", "seventh two", "eighth two", "ninth two", "tenth two"]
    do i = 1, nrows
      int_member1(i) = i
      int_member2(i) = 2*i
      double_member1(i) = 30000.0 * i
      double_member2(i) = 40000.0 * i
      real_member1(i) = 3.0 * i
      real_member2(i) = 4.0 * i
    enddo
    !
    ! Assign arrays to the compound datatype array
    allocate(h5compound_arr(ncols))
    member_names(1) = "char field 1"
    member_names(2) = "real field 1"
    member_names(3) = "char field 2"
    member_names(4) = "double field 1"
    member_names(5) = "int field 1"
    member_names(6) = "double field 2"
    member_names(7) = "real field 2"
    member_names(8) = "int field 2"
    h5compound_arr(1)%char_arr = char_member1
    h5compound_arr(1)%is_character = .true.
    h5compound_arr(2)%real_arr = real_member1
    h5compound_arr(2)%is_real = .true.
    h5compound_arr(3)%char_arr = char_member2
    h5compound_arr(3)%is_character = .true.
    h5compound_arr(4)%double_arr = double_member1
    h5compound_arr(4)%is_double = .true.
    h5compound_arr(5)%int_arr = int_member1
    h5compound_arr(5)%is_integer = .true.
    h5compound_arr(6)%double_arr = double_member2
    h5compound_arr(6)%is_double = .true.
    h5compound_arr(7)%real_arr = real_member2
    h5compound_arr(7)%is_real = .true.
    h5compound_arr(8)%int_arr = int_member2
    h5compound_arr(8)%is_integer = .true.
    
    ! Initialize Fortran interface
    call h5open_f(hdf_error)
    if (hdf_error < 0) return
    !
    ! Create a new file using default properties
    call h5fcreate_f(h5file_name, H5F_ACC_TRUNC_F, file_id, hdf_error)
    if (hdf_error < 0) return
    !
    is_ok = h5_write_compound_dataset(file_id, trim(dataset_name), h5compound_arr, nrows, member_names)
    !
    ! Close the file
    call h5fclose_f(file_id, hdf_error)
    if (hdf_error < 0) return
    !
    success = .true.
  end function test_h5_write_compound_dataset_with_more_columns

  !
  ! ...............................................................................................
  !
  ! Test adding a row to a dataset: Real type
  ! ...............................................................................................
  logical function test_h5_add_row_2d_dataset_r(file_id) result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_add_row_2d_dataset_r" :: test_h5_add_row_2d_dataset_r
    integer(hid_t), intent(out)         :: file_id
    integer(hid_t)                      :: file_access_flag = READ_ONLY ! File access flag for input
    integer(hid_t)                      :: infile_id                ! Input file ID
    integer(hid_t)                      :: outfile_id               ! Output file ID
    integer(hid_t)                      :: ingroup_id               ! Input group ID
    integer(hid_t)                      :: outgroup_id              ! Output group ID
    integer(hid_t)                      :: indataset_id             ! Input dataset ID
    integer(hid_t)                      :: outdataset_id            ! Output dataset ID
    character(len=STD_STR_LEN)          :: infile                   ! Input file
    character(len=STD_STR_LEN)          :: outfile                  ! Output file
    character(len=STD_STR_LEN)          :: ingroup_name             ! Input group name
    character(len=STD_STR_LEN)          :: outgroup_name            ! Output group name
    character(len=STD_STR_LEN)          :: indataset_name           ! Input dataset name
    character(len=STD_STR_LEN)          :: outdataset_name          ! Output dataset name
    integer(hsize_t), parameter         :: nrows_hdf = 2934         ! # Fortran columns, # HDF rows
    integer(hsize_t), parameter         :: ncols_hdf = 334          ! # Fortran rows, # HDF columns
    integer(hsize_t), parameter         :: nrows_subset = 1         ! # Fortran columns, # HDF rows
    integer(hsize_t), parameter         :: ncols_subset = ncols_hdf ! # Fortran rows, # HDF columns
    integer(hsize_t)                    :: startrow                 
    integer(hsize_t)                    :: startcol                 
    logical                             :: compressed               ! Chunk and compress dataset
    real(kind=c_float), &
      dimension(ncols_subset, nrows_subset) :: hdf_array            ! 2D Data array
    integer(hid_t)                      :: data_type                ! Data type ID
    logical, parameter                  :: chunk_in_time = .true.   ! Chunk dataset in time
    integer, parameter                  :: kind_nbytes = 4          ! Type precision (number of bytes)
    integer                             :: i, j                     ! Loop variables
    logical                             :: is_ok                    ! Function return condition
    logical                             :: link_exists              ! Function return condition
    !
    success = .false.
    file_id = 0
    !
    infile_id       = 0
    outfile_id      = 0
    ingroup_id      = 0
    outgroup_id     = 0
    indataset_id    = 0
    outdataset_id   = 0
    infile          = HDF_INFILE
    outfile         = "F2F_h5_add_row_2d_dataset_r.h5"
    ingroup_name    = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections"
    outgroup_name   = "/Results"
    indataset_name  = "Flow"
    outdataset_name = "Flow"
    compressed = .true.
    data_type = TEST_H5T_NATIVE_REAL
    
    ! Open input file
    is_ok = h5_open_file(infile, file_access_flag, infile_id)
    if (.not. is_ok) return
    !
    ! Check if group exists
    is_ok = h5_group_exists(infile_id, ingroup_name, link_exists)
    if (.not. is_ok) return
    !
    ! Open existing group
    is_ok = h5_open_group(infile_id, ingroup_name, ingroup_id)
    if (.not. is_ok) return
    !
    ! Open existing dataset
    is_ok = h5_open_dataset(ingroup_id, indataset_name, indataset_id)
    if (.not. is_ok) return
    !
    ! Create output file
    is_ok = h5_create_file(outfile, TRUNCATE, outfile_id)
    if (.not. is_ok) return
    !
    ! Create output group
    is_ok = h5_create_group(outfile_id, outgroup_name, outgroup_id)
    if (.not. is_ok) return
    !
    ! Create compressed 2D output dataset (size: 1 x 1)
    !is_ok = h5_create_2d_dataset(outgroup_id, outdataset_name, nrows_hdf, ncols_hdf, data_type, chunk_in_time, kind_nbytes, compressed, outdataset_id)
    is_ok = h5_create_2d_dataset(outgroup_id, outdataset_name, 1, 1, data_type, chunk_in_time, kind_nbytes, compressed, outdataset_id)
    if (.not. is_ok) return
    !
    ! Read dataset from input file by row and write to output file, adding one row at a time
    startcol = 0
    do i = 1, nrows_hdf
      startrow = i - 1
      is_ok = h5_read_dataset_2d_array_r(indataset_id, startrow, startcol, nrows_subset, ncols_subset, hdf_array)
      if (.not. is_ok) return
      !is_ok = h5_write_dataset_2d_array_r(outdataset_id, startrow, startcol, nrows_subset, ncols_subset, hdf_array(:, startrow))
      is_ok = h5_add_row_2d_dataset_r(outdataset_id, hdf_array(:, startrow))
      if (.not. is_ok) return
    end do
    !    
    ! Close both files
    is_ok = h5_close_dataset(indataset_id)
    is_ok = h5_close_group(ingroup_id)
    is_ok = h5_close_file(infile_id)
    is_ok = h5_close_dataset(outdataset_id)
    is_ok = h5_close_group(outgroup_id)
    is_ok = h5_close_file(outfile_id)
    !
    ! Close HDF5 interface
    is_ok = h5_terminate()
    if (.not. is_ok) return
    !
    success = .true.
  end function
  !
end module
