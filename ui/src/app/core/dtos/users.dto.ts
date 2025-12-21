export interface UsersDto {
  userId?: number;

  name?: string;
  lastName?: string;
  email?: string;
  password?: string;

  isActive?: boolean;
  createdDate?: string | Date;

  userRoleId?: number;
  departmentId?: number;
  userRoleName?: string;
  departmentName?: string;
}
