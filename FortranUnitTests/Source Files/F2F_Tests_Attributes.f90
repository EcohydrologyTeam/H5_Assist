!DEC$ FREEFORM
!* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
! HDF5 Unit Tests - Attribute Tests
!* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
!
! Created by Todd Steissberg, 2017
!
!------------------------------------------------------------------------------------------------
! See notes in F2F_Tests_Globals.f90
!------------------------------------------------------------------------------------------------
!
module F2F_Tests_Attributes
  !
  use, non_intrinsic :: hdf5
  use, non_intrinsic :: h5_globals
  use, non_intrinsic :: h5_utilities
  use, non_intrinsic :: h5_basictests
  use, non_intrinsic :: h5_files
  use, non_intrinsic :: h5_groups
  use, non_intrinsic :: h5_datasets
  use, non_intrinsic :: h5_attributes
  use :: F2F_Tests_Globals
  !
  implicit none
  !
  contains
  !
  ! ...............................................................................................
  !
  ! Test opening and closing a dataset attribute
  ! ...............................................................................................
  logical function test_h5_open_and_close_dataset_attribute(file_id, group_id, dataset_id, attri_id) result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_open_and_close_dataset_attribute" :: test_h5_open_and_close_dataset_attribute
    integer(hid_t), intent(inout)       :: file_id
    integer(hid_t), intent(inout)       :: group_id
    integer(hid_t), intent(inout)       :: dataset_id
    integer(hid_t), intent(inout)       :: attri_id
    logical                             :: is_ok
    logical                             :: link_exists
    integer(hid_t)                      :: file_access_flag = READ_ONLY
    character(len=STD_STR_LEN)          :: infile
    character(len=STD_STR_LEN)          :: group_name
    character(len=STD_STR_LEN)          :: dataset_name
    character(len=STD_STR_LEN)          :: attri_name
    integer(hsize_t), parameter         :: nvals = 300000
    !
    success = .false.
    file_id = 0
    group_id = 0
    dataset_id = 0
    attri_id = 0
    !
    infile = HDF_INFILE
    group_name = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections"
    dataset_name = "Flow"
    attri_name = "Maximum Value of Data Set"
    !
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
    is_ok = h5_open_attribute(dataset_id, attri_name, attri_id)
    if (.not. is_ok) return
    !
    is_ok = h5_close_attribute(attri_id)
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
  ! Test opening and closing a group attribute
  ! ...............................................................................................
  logical function test_h5_open_and_close_group_attribute(file_id, group_id, attri_id) result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_open_and_close_group_attribute" :: test_h5_open_and_close_group_attribute
    integer(hid_t), intent(inout)       :: file_id
    integer(hid_t), intent(inout)       :: group_id
    integer(hid_t), intent(inout)       :: attri_id
    logical                             :: is_ok
    logical                             :: link_exists
    integer(hid_t)                      :: file_access_flag = READ_ONLY
    character(len=STD_STR_LEN)          :: infile
    character(len=STD_STR_LEN)          :: group_name
    character(len=STD_STR_LEN)          :: attri_name
    integer(hsize_t), parameter         :: nvals = 300000
    !
    success = .false.
    file_id = 0
    group_id = 0
    attri_id = 0
    !
    infile = HDF_INFILE
    group_name = "/Plan Data/Plan Parameters"
    attri_name = "HDF Compression"
    !
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
    is_ok = h5_open_attribute(group_id, attri_name, attri_id)
    if (.not. is_ok) return
    !
    is_ok = h5_close_attribute(attri_id)
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
  ! Test reading a scalar attribute: Real type
  ! ...............................................................................................
  logical function test_h5_read_attri_scalar_r(file_id, group_id, dataset_id, attri_id) result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_read_attri_scalar_r" :: test_h5_read_attri_scalar_r
    integer(hid_t), intent(inout)       :: file_id
    integer(hid_t), intent(inout)       :: group_id
    integer(hid_t), intent(inout)       :: dataset_id
    integer(hid_t), intent(inout)       :: attri_id
    logical                             :: is_ok
    logical                             :: link_exists
    integer(hid_t)                      :: file_access_flag = READ_ONLY
    character(len=STD_STR_LEN)          :: infile
    character(len=STD_STR_LEN)          :: group_name
    character(len=STD_STR_LEN)          :: dataset_name
    character(len=STD_STR_LEN)          :: attri_name
    integer(hsize_t), parameter         :: nvals = 300000
    real(kind=C_FLOAT)                  :: attri_value
    !
    success = .false.
    file_id = 0
    group_id = 0
    dataset_id = 0
    attri_id = 0
    !
    infile = HDF_INFILE
    group_name = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections"
    dataset_name = "Flow"
    attri_name = "Maximum Value of Data Set"
    !
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
    is_ok = h5_open_attribute(dataset_id, attri_name, attri_id)
    if (.not. is_ok) return
    !
    is_ok = h5_read_attri_scalar_r(attri_id, attri_value)
    if (.not. is_ok) return
    !
    is_ok = h5_close_attribute(attri_id)
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
    is_ok = are_equal_r(810.03796, attri_value, 0.0002)
    if (.not. is_ok) return
    !
    success = .true.
  end function
  !
  ! ...............................................................................................
  !
  ! Test reading a scalar attribute: Integer type
  ! ...............................................................................................
  logical function test_h5_read_attri_scalar_i(file_id, group_id, dataset_id, attri_id) result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_read_attri_scalar_i" :: test_h5_read_attri_scalar_i
    integer(hid_t), intent(inout)       :: file_id
    integer(hid_t), intent(inout)       :: group_id
    integer(hid_t), intent(inout)       :: dataset_id
    integer(hid_t), intent(inout)       :: attri_id
    logical                             :: is_ok
    logical                             :: link_exists
    integer(hid_t)                      :: file_access_flag = READ_ONLY
    character(len=STD_STR_LEN)          :: infile
    character(len=STD_STR_LEN)          :: group_name
    character(len=STD_STR_LEN)          :: attri_name
    integer(hsize_t), parameter         :: nvals = 300000
    integer(kind=C_INT)                 :: attri_value
    !
    success = .false.
    file_id = 0
    group_id = 0
    dataset_id = 0
    attri_id = 0
    !
    infile = HDF_INFILE
    group_name = "/Plan Data/Plan Parameters"
    attri_name = "HDF Compression"
    !
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
    is_ok = h5_open_attribute(group_id, attri_name, attri_id)
    if (.not. is_ok) return
    !
    is_ok = h5_read_attri_scalar_i(attri_id, attri_value)
    if (.not. is_ok) return
    !
    is_ok = h5_close_attribute(attri_id)
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
    is_ok = are_equal_i(1, attri_value)
    if (.not. is_ok) return
    !
    success = .true.
    !
  end function
  !
  ! ...............................................................................................
  !
  ! Test reading a scalar attribute: Character type
  ! ...............................................................................................
  logical function test_h5_read_attri_scalar_c(file_id, group_id, dataset_id, attri_id) result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_read_attri_scalar_c" :: test_h5_read_attri_scalar_c
    integer(hid_t), intent(inout)       :: file_id
    integer(hid_t), intent(inout)       :: group_id
    integer(hid_t), intent(inout)       :: dataset_id
    integer(hid_t), intent(inout)       :: attri_id
    logical                             :: is_ok
    logical                             :: link_exists
    integer(hid_t)                      :: file_access_flag = READ_ONLY
    character(len=STD_STR_LEN)          :: infile
    character(len=STD_STR_LEN)          :: group_name
    character(len=STD_STR_LEN)          :: dataset_name
    character(len=STD_STR_LEN)          :: attri_name
    integer, parameter                  :: str_len = 16
    character(kind=C_CHAR, len=str_len) :: attri_value
    !
    success = .false.
    file_id = 0
    group_id = 0
    dataset_id = 0
    attri_id = 0
    !
    infile = HDF_INFILE
    group_name = "/Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series/Cross Sections"
    dataset_name = "Flow Lateral"
    attri_name = "Lateral Flow"
    !
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
    is_ok = h5_open_attribute(dataset_id, attri_name, attri_id)
    if (.not. is_ok) return
    !
    is_ok = h5_read_attri_scalar_c(attri_id, attri_value)
    if (.not. is_ok) return
    !
    is_ok = h5_close_attribute(attri_id)
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
    is_ok = are_equal_c("m3/s", attri_value)
    if (.not. is_ok) return
    !
    success = .true.
  end function
  !
  ! ...............................................................................................
  !
  ! Test reading an attribute array: Real type
  ! ...............................................................................................
  logical function test_h5_read_attri_1d_array_r(file_id, group_id, dataset_id, attri_id) result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_read_attri_1d_array_r" :: test_h5_read_attri_1d_array_r
    integer(hid_t), intent(inout)       :: file_id
    integer(hid_t), intent(inout)       :: group_id
    integer(hid_t), intent(inout)       :: dataset_id
    integer(hid_t), intent(inout)       :: attri_id
    logical                             :: is_ok
    logical                             :: link_exists
    integer(hid_t)                      :: file_access_flag = READ_ONLY
    character(len=STD_STR_LEN)          :: infile
    character(len=STD_STR_LEN)          :: group_name
    character(len=STD_STR_LEN)          :: dataset_name
    character(len=STD_STR_LEN)          :: attri_name
    integer(hsize_t), parameter         :: nvals = 10
    real(kind=C_FLOAT), dimension(nvals) :: hdf_array
    real(kind=C_FLOAT), dimension(nvals) :: expected = (/1.1, 2.2, 3.3, 4.4, 5.5, 6.6, 7.7, 8.8, 9.9, 10.10/)
    integer                             :: i
    !
    success = .false.
    file_id = 0
    group_id = 0
    dataset_id = 0
    attri_id = 0
    !
    infile = HDF_INFILE
    group_name = "/Todd"
    dataset_name = "MyData"
    attri_name = "FloatAttribute"
    !
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
    is_ok = h5_open_attribute(dataset_id, attri_name, attri_id)
    if (.not. is_ok) return
    !
    is_ok = h5_read_attri_1d_array_r(attri_id, hdf_array, nvals)
    if (.not. is_ok) return
    !
    is_ok = h5_close_attribute(attri_id)
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
    do i = 1, nvals
      is_ok = are_equal_r(expected(i), hdf_array(i), 0.01)
      if (.not. is_ok) return
    end do
    !
    success = .true.
  end function
  !
  ! ...............................................................................................
  !
  ! Test reading an attribute array: Integer type
  ! ...............................................................................................
  logical function test_h5_read_attri_1d_array_i(file_id, group_id, dataset_id, attri_id) result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_read_attri_1d_array_i" :: test_h5_read_attri_1d_array_i
    integer(hid_t), intent(inout)       :: file_id
    integer(hid_t), intent(inout)       :: group_id
    integer(hid_t), intent(inout)       :: dataset_id
    integer(hid_t), intent(inout)       :: attri_id
    logical                             :: is_ok
    logical                             :: link_exists
    integer(hid_t)                      :: file_access_flag = READ_ONLY
    character(len=STD_STR_LEN)          :: infile
    character(len=STD_STR_LEN)          :: group_name
    character(len=STD_STR_LEN)          :: dataset_name
    character(len=STD_STR_LEN)          :: attri_name
    integer(hsize_t), parameter         :: nvals = 10
    integer(kind=C_INT), &
      dimension(nvals)                   :: hdf_array
    integer(kind=C_INT), &
      dimension(nvals)                   :: expected = (/1, 2, 3, 4, 5, 6, 7, 8, 9, 10/)
    integer                             :: i
    !
    success = .false.
    file_id = 0
    group_id = 0
    dataset_id = 0
    attri_id = 0
    !
    infile = HDF_INFILE
    group_name = "/Todd"
    dataset_name = "MyData"
    attri_name = "IntAttribute"
    !
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
    is_ok = h5_open_attribute(dataset_id, attri_name, attri_id)
    if (.not. is_ok) return
    !
    is_ok = h5_read_attri_1d_array_i(attri_id, hdf_array, nvals)
    if (.not. is_ok) return
    !
    is_ok = h5_close_attribute(attri_id)
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
    do i = 1, nvals
      is_ok = are_equal_i(expected(i), hdf_array(i))
      if (.not. is_ok) return
    end do
    !
    success = .true.
  end function
  !
  ! ...............................................................................................
  !
  ! Test reading an attribute array: Real type
  ! ...............................................................................................
  logical function test_h5_read_attri_1d_array_c(file_id, group_id, dataset_id, attri_id) result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_read_attri_1d_array_c" :: test_h5_read_attri_1d_array_c
    integer(hid_t), intent(inout)       :: file_id
    integer(hid_t), intent(inout)       :: group_id
    integer(hid_t), intent(inout)       :: dataset_id
    integer(hid_t), intent(inout)       :: attri_id
    logical                             :: is_ok
    logical                             :: link_exists
    integer(hid_t)                      :: file_access_flag = READ_ONLY
    character(len=STD_STR_LEN)          :: infile
    character(len=STD_STR_LEN)          :: group_name
    character(len=STD_STR_LEN)          :: dataset_name
    character(len=STD_STR_LEN)          :: attri_name
    integer, parameter                  :: str_len = 10
    integer(hsize_t), parameter         :: nvals = 3
    character(len=str_len), &
      dimension(nvals)                   :: hdf_array
    character(len=str_len), &
      dimension(nvals)                   :: expected = (/"LengthLOB", "LengthChan", "LengthROB"/)
    integer                             :: i
    !
    success = .false.
    file_id = 0
    !
    infile = HDF_INFILE
    group_name = "/Geometry/Cross Sections"
    dataset_name = "Lengths"
    attri_name = "Column"
    !
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
    is_ok = h5_open_attribute(dataset_id, attri_name, attri_id)
    if (.not. is_ok) return
    !
    is_ok = h5_read_attri_1d_array_c(attri_id, hdf_array, str_len, nvals);
    if (.not. is_ok) return
    !
    is_ok = h5_close_attribute(attri_id)
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
    do i = 1, nvals
      is_ok = are_equal_c(expected(i), hdf_array(i))
      if (.not. is_ok) return
    end do
    !
    success = .true.
  end function
  !
  ! ...............................................................................................
  !
  ! Test writing a scalar attribute: Real type
  ! ...............................................................................................
  logical function test_h5_write_attri_scalar_r(outfile_id, outgroup_id, outdataset_id, outattri_id) result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_write_attri_scalar_r" :: test_h5_write_attri_scalar_r
    integer(hid_t), intent(inout)       :: outfile_id
    integer(hid_t), intent(inout)       :: outgroup_id
    integer(hid_t), intent(inout)       :: outdataset_id
    integer(hid_t), intent(inout)       :: outattri_id
    logical                             :: is_ok
    logical                             :: link_exists
    integer(hid_t)                      :: file_access_flag = READ_ONLY
    character(len=STD_STR_LEN)          :: outfile
    character(len=STD_STR_LEN)          :: outgroup_name
    character(len=STD_STR_LEN)          :: outdataset_name
    character(len=STD_STR_LEN)          :: outattri_name
    integer(hsize_t), parameter         :: nvals_dataset = 10
    integer(hsize_t), parameter         :: nvals_attri = 1
    real(kind=c_float)                  :: outattri_value
    integer(hid_t)                      :: data_type
    integer                             :: kind_nbytes
    integer                             :: rank
    !
    success = .false.
    outfile_id = 0
    outgroup_id = 0
    outdataset_id = 0
    outattri_id = 0
    !
    outfile = "F2F_test_write_attri_scalar_r.h5"
    outgroup_name = "/Results"
    outdataset_name = "Flow"
    outattri_name = "Maximum Value of Data Set"
    data_type = TEST_H5T_NATIVE_REAL
    kind_nbytes = 4
    rank = 1
    !
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
    ! Create compressed 1D output dataset
    is_ok = h5_create_compressed_1d_dataset(outgroup_id, outdataset_name, nvals_dataset, data_type, kind_nbytes, outdataset_id)
    if (.not. is_ok) return
    !
    ! Create attribute
    is_ok = h5_create_attribute(outdataset_id, outattri_name, rank, data_type, nvals_attri, outattri_id)
    !
    ! Write attribute
    outattri_value = 42.0
    is_ok = h5_write_attri_scalar_r(outattri_id, outattri_value)
    !
    is_ok = h5_close_attribute(outattri_id)
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
    is_ok = h5_terminate()
    if (.not. is_ok) return
    !
    success = .true.
  end function
  !
  ! ...............................................................................................
  !
  ! Test writing a scalar attribute: Integer type
  ! ...............................................................................................
  logical function test_h5_write_attri_scalar_i(outfile_id, outgroup_id, outdataset_id, outattri_id) result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_write_attri_scalar_i" :: test_h5_write_attri_scalar_i
    integer(hid_t), intent(inout)       :: outfile_id
    integer(hid_t), intent(inout)       :: outgroup_id
    integer(hid_t), intent(inout)       :: outdataset_id
    integer(hid_t), intent(inout)       :: outattri_id
    logical                             :: is_ok
    logical                             :: link_exists
    integer(hid_t)                      :: file_access_flag = READ_ONLY
    character(len=STD_STR_LEN)          :: outfile
    character(len=STD_STR_LEN)          :: outgroup_name
    character(len=STD_STR_LEN)          :: outdataset_name
    character(len=STD_STR_LEN)          :: outattri_name
    integer(hsize_t), parameter         :: nvals_dataset = 10
    integer(hsize_t), parameter         :: nvals_attri = 1
    integer(kind=c_int)                 :: outattri_value
    integer(hid_t)                      :: data_type
    integer                             :: kind_nbytes
    integer                             :: rank
    !
    success = .false.
    outfile_id = 0
    outgroup_id = 0
    outdataset_id = 0
    outattri_id = 0
    !
    outfile = "F2F_test_write_attri_scalar_i.h5"
    outgroup_name = "/Results"
    outdataset_name = "Flow"
    outattri_name = "Maximum Value of Data Set"
    data_type = TEST_H5T_NATIVE_REAL
    kind_nbytes = 4
    rank = 1
    !
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
    ! Create compressed 1D output dataset
    is_ok = h5_create_compressed_1d_dataset(outgroup_id, outdataset_name, nvals_dataset, data_type, kind_nbytes, outdataset_id)
    if (.not. is_ok) return
    !
    ! Create attribute
    is_ok = h5_create_attribute(outdataset_id, outattri_name, rank, data_type, nvals_attri, outattri_id)
    !
    ! Write attribute
    outattri_value = 42
    is_ok = h5_write_attri_scalar_r(outattri_id, outattri_value)
    !
    is_ok = h5_close_attribute(outattri_id)
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
    is_ok = h5_terminate()
    if (.not. is_ok) return
    !
    success = .true.
  end function

end module