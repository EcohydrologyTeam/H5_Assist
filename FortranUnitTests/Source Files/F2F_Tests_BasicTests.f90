!DEC$ FREEFORM
!* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
! HDF5 Unit Tests - Basic Tests
!* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
!
! Created by Todd Steissberg, 2017
!
!------------------------------------------------------------------------------------------------
! See notes in F2F_Tests_Globals.f90
!------------------------------------------------------------------------------------------------
!
module F2F_Tests_BasicTests
  !
  use, non_intrinsic  :: h5_basictests
  use, non_intrinsic  :: h5_files
  use, non_intrinsic  :: F2F_Tests_Globals
  !
  implicit none
  !
  contains
  !
  ! ...............................................................................................
  !
  ! Provide a simple test
  ! ...............................................................................................
  integer function test_add_two(x)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_add_two" :: test_add_two
    integer, intent(in) :: x
    test_add_two = x + 2
    return
  end function
  !
  ! ...............................................................................................
  !
  ! Provide a simple test
  ! ...............................................................................................
  integer function test_add_one(x)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_add_one" :: test_add_one
    integer, intent(in) :: x
    test_add_one = add_one(x)
    return
  end function
  !
  ! ...............................................................................................
  !
  ! Test passing a 2D array: Real type
  ! ...............................................................................................
  logical function test_pass_2d_float_array_two_way() result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_pass_2d_float_array_two_way" :: test_pass_2d_float_array_two_way
    logical :: is_ok
    integer, parameter :: nrows = 5
    integer, parameter :: ncols = 7
    integer :: i, j
    real(kind=4), dimension(nrows, ncols) :: myarray
    !
    success = .false.
    !
    is_ok = pass_2d_float_array_two_way(myarray, nrows, ncols)
    if (.not. is_ok) return
    !
    do i = 1, nrows
      do j = 1, ncols
        write(*,*) i, j, ": ", myarray(i, j)
      end do
    end do
    success = .true.
  end function
  !
  ! ...............................................................................................
  !
  ! Test passing a 2D array: Integer type
  ! ...............................................................................................
  logical function test_pass_2d_int_array_two_way() result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_pass_2d_int_array_two_way" :: test_pass_2d_int_array_two_way
    logical :: is_ok
    integer, parameter :: nrows = 5
    integer, parameter :: ncols = 7
    integer :: i, j
    integer(kind=4), dimension(nrows, ncols) :: myarray
    !
    success = .false.
    !
    is_ok = pass_2d_int_array_two_way(myarray, nrows, ncols)
    if (.not. is_ok) return
    !
    do i = 1, nrows
      do j = 1, ncols
        write(*,*) i, j, ": ", myarray(i, j)
      end do
    end do
    success = .true.
  end function
  !
  ! ...............................................................................................
  !
  ! Test initializing and terminating HDF5
  ! ...............................................................................................
  logical function test_h5_init_and_terminate() result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_init_and_terminate" :: test_h5_init_and_terminate
    logical :: is_ok
    !
    success = .false.
    !
    is_ok = h5_init()
    if (.not. is_ok) return
    !
    is_ok = h5_terminate()
    if (.not. is_ok) return
    !
    success = .true.
  end function


  ! 
end module