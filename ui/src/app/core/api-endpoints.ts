export class ApiEndpoints {

  static readonly AddOrUpdateUser = '/User/AddOrUpdateUser';
  static readonly GetAllUsers = '/User/GetAllUsers';
  static readonly DeleteUser = '/User/DeleteUser';
  static readonly GetUserById = '/User/GetUserById'

  static readonly GetAllUserRoles = '/UserRole/GetAllUserRoles';

  static readonly AddOrUpdateTask = '/Task/AddOrUpdateTask';
  static readonly GetAllTasks = '/Task/GetAllTasks';
  static readonly GetTaskByUserId = '/Task/GetTaskByUserId';
  static readonly DeleteTask = '/Task/DeleteTask';

  static readonly GetAllLeaveTypes = '/LeaveType/GetAllLeaveTypes';

  static readonly GetLeaveRequestByUserId = '/LeaveRequest/GetLeaveRequestByUserId';
  static readonly AddOrUpdateLeaveRequest = '/LeaveRequest/AddOrUpdate';
  static readonly GetAllLeaveRequest = '/LeaveRequest/GetAllLeaveRequest'
  static readonly CheckLeaveRequest = '/LeaveRequest/CheckLeaveRequest'

  static readonly GetAllDepartments = '/Department/GetAllDepartments';
  static readonly GetDepartmentById = '/Department/GetDepartmentById';
  static readonly AddOrUpdateDepartment = '/Department/AddOrUpdateDepartment';
  static readonly DeleteDepartment = '/Department/DeleteDepartment';

  static readonly GetAllTaskStatuses = '/TaskStatus/GetAllTaskStatuses';
}