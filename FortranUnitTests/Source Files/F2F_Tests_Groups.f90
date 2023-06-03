!DEC$ FREEFORM
!* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
! HDF5 Unit Tests - Group Tests
!* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
!
! Created by Todd Steissberg, 2017
!
!------------------------------------------------------------------------------------------------
! See notes in F2F_Tests_Globals.f90
!------------------------------------------------------------------------------------------------
!
module F2F_Tests_Groups
  !
  use, non_intrinsic :: hdf5
  use, non_intrinsic :: h5_files
  use, non_intrinsic :: h5_globals
  use, non_intrinsic :: h5_groups
  use, non_intrinsic :: F2F_Tests_Globals
  !
  implicit none
  !
  contains
  !
  ! ...............................................................................................
  !
  ! Test if group exists
  ! ...............................................................................................
  logical function test_h5_group_exists(file_id) result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_group_exists" :: test_h5_group_exists
    integer, intent(inout)              :: file_id
    logical                             :: is_ok
    logical                             :: link_exists
    integer                             :: file_access_flag = READ_ONLY
    character(len=STD_STR_LEN)          :: infile
    character(len=STD_STR_LEN)          :: group_name
    !
    success = .false.
    file_id = 0
    !
    infile = HDF_INFILE
    group_name = "Results/Unsteady/Output/Output Blocks/Base Output/Unsteady Time Series"
    !
    is_ok = h5_open_file(infile, file_access_flag, file_id)
    if (.not. is_ok) return
    !
    is_ok = h5_group_exists(file_id, group_name, link_exists)
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
  ! Test getting number of groups in file
  ! ...............................................................................................
  logical function test_h5_num_groups(num_groups, file_id) result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_num_groups" :: test_h5_num_groups
    integer, intent(inout)              :: file_id
    integer, intent(out)                :: num_groups
    logical                             :: is_ok
    integer                             :: file_access_flag = READ_ONLY
    character(len=STD_STR_LEN)          :: infile
    character(len=STD_STR_LEN)          :: group_name
    !
    success = .true.
    num_groups = -1
    file_id = 0
    !
    infile = HDF_INFILE
    group_name = "/Results/Unsteady/Output/Output Blocks/Base Output"
    !
    is_ok = h5_open_file(infile, file_access_flag, file_id)
    if (.not. is_ok) return
    !
    is_ok = h5_num_groups(file_id, group_name, num_groups)
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
  ! Test opening and closing a group
  ! ...............................................................................................
  logical function test_h5_open_and_close_group(file_id, group_id) result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_open_and_close_group" :: test_h5_open_and_close_group
    integer, intent(inout)              :: file_id
    integer, intent(inout)              :: group_id
    logical                             :: link_exists
    integer                             :: file_access_flag = READ_ONLY
    character(len=STD_STR_LEN)          :: infile
    character(len=STD_STR_LEN)          :: group_name
    logical                             :: is_ok
    !
    success = .false.
    file_id = 0
    group_id = 0
    !
    infile = HDF_INFILE
    group_name = "/Results/Unsteady/Output/Output Blocks/Base Output"
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
  ! Test creating a group
  ! ...............................................................................................
  logical function test_h5_create_group(file_id, group_id) result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_create_group" :: test_h5_create_group
    integer, intent(inout)              :: file_id
    integer, intent(inout)              :: group_id
    logical                             :: is_ok
    logical                             :: link_exists
    integer                             :: file_access_flag = READ_WRITE ! Ensures we have write access!
    character(len=STD_STR_LEN)          :: infile
    character(len=STD_STR_LEN)          :: group_name
    !
    success = .false.
    file_id = 0
    group_id = 0
    !
    infile = HDF_INFILE_COPY2 ! Using a copy of the original to avoid file writing conflicts
    group_name = "F2F My New Group"
    !
    is_ok = h5_open_file(infile, file_access_flag, file_id)
    if (.not. is_ok) return
    !
    ! If the group already exists, delete (unlink) it
    is_ok = h5_group_exists(file_id, group_name, link_exists)
    if (link_exists) then
      is_ok = h5_delete_group(file_id, group_name)
    end if
    !
    is_ok = h5_create_group(file_id, group_name, group_id)
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
end module